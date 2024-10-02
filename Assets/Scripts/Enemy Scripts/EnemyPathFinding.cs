using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    public Queue<Vector3> path;
    public Enemy_Base agent;
    public float moveSpeed = 3;
    Vector3 currentPoint;

    int directions = 3; //used to loop through the directions for pathfinding
    
    public void PathFindingInit()
    {
        path = new Queue<Vector3>();
        agent = GetComponent<Enemy_Base>();
        MakePath();
    }

    public void MakePath()
    {
        Tile pathTile = agent.GetCurrentTile();
        List<KeyValuePair<Tile, float>> distances = new List<KeyValuePair<Tile, float>>();
        while(pathTile != agent.destination)
        {
            /*
             Loop thorugh the directions (+ X, +/-Z) and cast down to check for a tile with the tile tag
            Furture: Raycast in (+X, +/- Z) for any obstacles
            calculate the distance from the Spawn tile to the Home tile
            calculate the distance between the raycasted tile and the home tile
            compare the distance between the two calculations above
            repeat for all directions then take the Tile closer to the Home Tile
             */
            if(distances.Count > 0)
                distances.Clear();

            for (int i = 0; i < directions; i++)
            {
                Vector3 raycastPos = pathTile.gameObject.transform.position;
                raycastPos.y += 15;
                
                //Tiles are 5 units apart in X and Z
                switch(i)
                {
                    case 0:
                        raycastPos.x -= 5; //Left to Right
                        break;
                    case 1:
                        raycastPos.z += 5; //towards the top
                        break;
                    case 2:
                        raycastPos.z -= 5; //towards the bottom
                        break;
                    default:
                        break;
                }
                
                RaycastHit hit; 
                if (Physics.Raycast(raycastPos, Vector3.down * 10, out hit, 20))
                {
                    if (hit.collider.CompareTag("Tile") || hit.collider.CompareTag("Home"))
                    {
                        float temp_distance = Mathf.Abs(Vector3.Distance(hit.collider.transform.position, agent.destination.gameObject.transform.position));
                        KeyValuePair<Tile, float> tile_distance = new KeyValuePair<Tile, float>(hit.collider.GetComponent<Tile>(), temp_distance);
                        distances.Add(tile_distance);
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
                continue;
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

    public void MoveAgent()
    {
        if (agent.GetCurrentTile().CompareTag("Home"))
            return;
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
