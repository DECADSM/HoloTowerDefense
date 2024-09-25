using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    public List<Vector3> path;
    public Enemy_Base agent;
    public float speed = 3;

    int directions = 3; //used to loop through the directions for pathfinding
    
    public void PathFindingInit()
    {
        path = new List<Vector3>();
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

            //compare all distances to add to the path and set new current tile
            if (distances.Count == 1)
            {
                path.Add(distances[0].Key.transform.position);
                pathTile = distances[0].Key;
                continue;
            }
            else if (distances.Count == 2)
            {
                if (distances[0].Value < distances[1].Value)
                {
                        path.Add(distances[0].Key.transform.position);
                        pathTile = distances[0].Key;
                }
                else
                {
                    path.Add(distances[1].Key.transform.position);
                    pathTile = distances[1].Key;
                }
            }
            else
            {
                if (distances[0].Value < distances[1].Value)
                {
                    if (distances[0].Value < distances[2].Value)
                    {
                        path.Add(distances[0].Key.transform.position);
                        pathTile = distances[0].Key;
                    }
                }
                else if (distances[1].Value < distances[2].Value)
                {
                    path.Add(distances[1].Key.transform.position);
                    pathTile = distances[1].Key;
                }
                else if (distances[2].Value < distances[0].Value)
                {
                    path.Add(distances[2].Key.transform.position);
                    pathTile = distances[2].Key;
                }
            }
            
        }
        return;
    }

    public void MoveAgent()
    {
        agent.transform.position = Vector3.MoveTowards(agent.GetCurrentTile().transform.position, agent.destination.transform.position, 3 * Time.deltaTime);
    }



    public void SetAgent(Enemy_Base host)
    {
        if (host != null)
            agent = host;
        else
            print("host is null in EnemyPathfinding in " + gameObject.name);
        
    }
}
