using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    public Vector3[] path;
    public Tile destination;
    public Enemy_Base agent;

    public void MakePath()
    {
        Tile pathTile = agent.GetCurrentTile();
        while(pathTile != agent.destination)
        {
            RaycastHit hit;
            Vector3 raycastPos = transform.position;
            //Tiles are 5 units apart in X and Z
            raycastPos.y += 10;

            /*
             Loop thorugh the directions (+ X, +/-Z) and cast down to check for a tile with the tile tag
            Furture: Raycast in (+X, +/- Z) for any obstacles
            calculate the distance from the Spawn tile to the Home tile
            calculate the distance between the raycasted tile and the home tile
            compare the distance between the two calculations above
            repeat for all directions then take the Tile closer to the Home Tile
             */
            
            if (Physics.Raycast(raycastPos, Vector3.down * 10, out hit, 20))
            {
                if (hit.collider.CompareTag("Tile"))
                {
                    //currentTile = hit.collider.GetComponent<Tile>();
                }
            }
        }
    }

    public void SetAgent(Enemy_Base host)
    {
        if (host != null)
            agent = host;
        else
            print("host is null in EnemyPathfinding in " + gameObject.name);
        
    }
}
