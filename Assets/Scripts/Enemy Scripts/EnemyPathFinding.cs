using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class EnemyPathFinding : MonoBehaviour
{
    public Queue<Vector3> path;
    public Enemy_Base agent;
    public float moveSpeed = 3;
    Vector3 currentPoint;

    bool CanMove = true;

    //int directions = 4; //used to loop through the directions for pathfinding

    enum Directions
    {
        NONE,
        RIGHT,
        LEFT,
        UP,
        DOWN,
    }
    
    public void PathFindingInit()
    {
        path = new Queue<Vector3>();
        agent = GetComponent<Enemy_Base>();
        if(agent.GetSpawnTile().PresetPath.Count > 0)
        {
            path.Enqueue(WaypointAdjustment(agent.GetSpawnTile().PresetPath[0].transform.position));
        }
    }

    /*
    public void MakePath()
    {
        Tile pathTile;
        if (path.Count > 0)
        {
            pathTile = agent.GetSpawnTile().PresetPath[agent.GetSpawnTile().PresetPath.Count - 1];
        }
        else
            pathTile = agent.GetCurrentTile();

        List<KeyValuePair<Tile, float>> distances = new List<KeyValuePair<Tile, float>>();
        while(pathTile != agent.destination)
        {
            //*
             Loop thorugh the directions (+ X, +/-Z) and cast down to check for a tile with the tile tag
            Furture: Raycast in (+X, +/- Z) for any obstacles
            calculate the distance from the Spawn tile to the Home tile
            calculate the distance between the raycasted tile and the home tile
            compare the distance between the two calculations above
            repeat for all directions then take the Tile closer to the Home Tile

            if(distances.Count > 0)
                distances.Clear();

            RaycastHit hit;
            Vector3 WallRaycast = pathTile.transform.position;
            WallRaycast.y = 0.4f;
            int[] openDirections = { 0, 0, 0, 0 }; //0 means open, 1 means closed; {forward, up, down, back}
            int ODPos = 0;

            for (int i = 0; i < directions; i++)
            {
                Vector3 TileRaycast = pathTile.transform.position;
                TileRaycast.y += 15;
                Vector3 direction = new Vector3();

                //Tiles are 5 units apart in X and Z
                switch (i)
                {
                    case 0:
                        TileRaycast.x -= 5; //Left to Right
                        direction = -agent.transform.right;
                        break;
                    case 1:
                        TileRaycast.z += 5; //towards the top
                        direction = agent.transform.forward;
                        ODPos = 1;
                        break;
                    case 2:
                        TileRaycast.z -= 5; //towards the bottom
                        direction = -agent.transform.forward;
                        ODPos = 2;
                        break;
                    case 3:
                        TileRaycast.x += 5; //Right to Left
                        direction = agent.transform.right;
                        ODPos = 3;
                        break;
                    default:
                        break;
                }

                if (Physics.Raycast(WallRaycast, direction * 5, out hit, 5))
                {
                    if (hit.collider.CompareTag("Wall"))
                    {
                        openDirections[ODPos] = 1; //closes off this postion
                    }
                }

                if (openDirections[ODPos] == 0)
                {
                    if (Physics.Raycast(TileRaycast, Vector3.down * 10, out hit, 20))
                    {
                        if (hit.collider.CompareTag("Tile") || hit.collider.CompareTag("Home"))
                        {
                            float temp_distance = Mathf.Abs(Vector3.Distance(hit.collider.transform.position, agent.destination.gameObject.transform.position));
                            KeyValuePair<Tile, float> tile_distance = new KeyValuePair<Tile, float>(hit.collider.GetComponent<Tile>(), temp_distance);
                            distances.Add(tile_distance);
                        }
                    }
                }
            }

            Vector3 waypoint;
            //compare all distances to add to the path and set new current tile
            if (distances.Count == 1)
            {

                waypoint = WaypointAdjustment(distances[0].Key.transform.position);
                path.Enqueue(waypoint);
                pathTile = distances[0].Key;
            }
            else if (distances.Count == 2)
            {
                if (distances[0].Value < distances[1].Value)
                {
                    waypoint = WaypointAdjustment(distances[0].Key.transform.position);
                    path.Enqueue(waypoint);
                    pathTile = distances[0].Key;
                }
                else
                {
                    waypoint = WaypointAdjustment(distances[1].Key.transform.position);
                    path.Enqueue(waypoint);
                    pathTile = distances[1].Key;
                }
            }
            else
            {
                if (distances[0].Value < distances[1].Value)
                {
                    if (distances[0].Value < distances[2].Value)
                    {
                        waypoint = WaypointAdjustment(distances[0].Key.transform.position);
                        path.Enqueue(waypoint);
                        pathTile = distances[0].Key;
                    }
                }
                else if (distances[1].Value < distances[2].Value)
                {
                    waypoint = WaypointAdjustment(distances[1].Key.transform.position);
                    path.Enqueue(waypoint);
                    pathTile = distances[1].Key;
                }
                else if (distances[2].Value < distances[0].Value)
                {
                    waypoint = WaypointAdjustment(distances[2].Key.transform.position);
                    path.Enqueue(waypoint);
                    pathTile = distances[2].Key;
                }
            }
            
        }
        return;
    }
*/

    public void UpdatePath()
    {
        Vector3 upDir = agent.transform.forward, downDir = -agent.transform.forward, leftDir = -agent.transform.right, rightDir = agent.transform.right;

        int[] openDirections = { 0, 0, 0, 0 }; //1 means open, 0 means closed; {left, up, down, right}

        Directions destDir = Directions.NONE;

        //Enemy Spawn Tiles on the right of the screen
        //+X goes right +Z goes up

        if (agent.transform.position.x > path.Peek().x)
            destDir = Directions.LEFT; //left
        else if (agent.transform.position.z > path.Peek().z)
            destDir = Directions.DOWN; //down
        else if (agent.transform.position.x < path.Peek().x)
            destDir = Directions.RIGHT; //right
        else if (agent.transform.position.z < path.Peek().z)
            destDir = Directions.UP; //up

        RaycastHit upHit, leftHit, rightHit, downHit;
        if(Physics.Raycast(path.Peek(), upDir, out upHit, 5))
        {
            if (upHit.collider.CompareTag("Wall"))
            {
                openDirections[1] = 1;
            }
        }
        if (Physics.Raycast(path.Peek(), leftDir, out leftHit, 5))
        {
            if (leftHit.collider.CompareTag("Wall"))
            {
                openDirections[0] = 1;
            }
        }
        if (Physics.Raycast(path.Peek(), rightDir, out rightHit, 5))
        {
            if (rightHit.collider.CompareTag("Wall"))
            {
                openDirections[3] = 1;
            }
        }
        if (Physics.Raycast(path.Peek(), downDir, out downHit, 5))
        {
            if (downHit.collider.CompareTag("Wall"))
            {
                openDirections[2] = 1;
            }
        }

        switch(destDir)
        {
            case Directions.UP:
                if (openDirections[1] == 1)
                {
                    if(openDirections[3] == 1 && openDirections[0] == 1)
                    {
                        //stop moving or move down
                    }
                    else if (openDirections[3] == 0)
                    {
                        //move right
                    }
                    else
                    {
                        //move left
                    }
                }
                break;
            case Directions.DOWN:
                if (openDirections[2] == 1)
                {
                    if (openDirections[3] == 1 && openDirections[0] == 1)
                    {
                        //stop moving or move up
                    }
                    else if (openDirections[3] == 0)
                    {
                        //move right
                    }
                    else
                    {
                        //move left
                    }
                }
                break;
            case Directions.LEFT:
                if (openDirections[0] == 1)
                {
                    if (openDirections[1] == 1 && openDirections[2] == 1)
                    {
                        //stop moving or move back
                    }
                    else if(openDirections[1] == 0)
                    {
                        //move up
                    }
                    else
                    {
                        //move down
                    }
                }
                break;
            case Directions.RIGHT:
                if (openDirections[3] == 1)
                {
                    if (openDirections[1] == 1 && openDirections[2] == 1)
                    {
                        //stop moving or move back
                    }
                    else if (openDirections[1] == 0)
                    {
                        //move up
                    }
                    else
                    {
                        //move down
                    }
                }
                break;
            default:
                break;
        }

        
    }

    public void MoveAgent()
    {
        if (agent.GetCurrentTile().CompareTag("Home"))
            return;

        if(agent.GetCurrentTile().CharacterOnTile != null)
        {
            Character c = agent.GetCurrentTile().CharacterOnTile;
            if (!c.MaxBlocking())
            {
                if (!c.GetDead() && c.GetBlock() > 0)
                {
                    c.AddBlocking();
                    return;
                }
            }
        }
        //grab the point in queue then get rid of it
        if ((currentPoint == Vector3.zero || Vector3.Distance(currentPoint, agent.transform.position) < 0.5f) && path.Count > 0)
        {
            currentPoint = path.Peek();
            path.Dequeue();
        }

        //redundant variable to check if it's being set right/working
        Vector3 tempPos = Vector3.MoveTowards(agent.transform.position, currentPoint, moveSpeed * Time.deltaTime);

        agent.transform.position = tempPos;
    }

    Vector3 WaypointAdjustment(Vector3 waypoint)
    {
        Vector3 result = waypoint;
        result.y = 0.4f;

        return result;
    }

    public void SetAgent(Enemy_Base host)
    {
        if (host != null)
            agent = host;
        else
            print("host is null in EnemyPathfinding in " + gameObject.name);
        
    }
}
