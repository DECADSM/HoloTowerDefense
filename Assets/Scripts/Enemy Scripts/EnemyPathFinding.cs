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
            float distance = Mathf.Abs(Vector3.Distance(pathTile.gameObject.transform.position, agent.destination.gameObject.transform.position));

            for (int i = 0; i < directions; i++)
            {
                Vector3 raycastPos = pathTile.gameObject.transform.position;
                raycastPos.y += 15;
                
                //Tiles are 5 units apart in X and Z
                switch(i)
                {
                    case 0:
                        raycastPos.x -= 10;
                        break;
                    case 1:
                        raycastPos.z += 10;
                        break;
                    case 2:
                        raycastPos.z -= 10;
                        break;
                    default:
                        break;
                }
                
                RaycastHit hit; 
                if (Physics.Raycast(raycastPos, Vector3.down * 10, out hit, 20))
                {
                    if (hit.collider.CompareTag("Tile"))
                    {
                        float temp_distance = Mathf.Abs(Vector3.Distance(hit.collider.transform.position, agent.destination.gameObject.transform.position));
                        if (temp_distance < distance)
                        {
                            path.Add(hit.collider.transform.position);
                            pathTile = hit.collider.GetComponent<Tile>();
                        }
                        //currentTile = hit.collider.GetComponent<Tile>();
                    }
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
