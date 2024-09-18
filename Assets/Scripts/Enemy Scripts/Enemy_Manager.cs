using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    public List<Tile> spawnTiles;
    public List<GameObject> EnemiesInPlay;
    [SerializeField] GameObject EnemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        spawnTiles = new List<Tile>();
        EnemiesInPlay = new List<GameObject>();

        Tile[] tempTileList = FindObjectsOfType<Tile>();
        foreach(var ob in tempTileList)
        {
            if (ob.EnemySpawner)
                spawnTiles.Add(ob);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            foreach(var tile in spawnTiles)
            {
                if(tile.SpawnNumEnemies > 0)
                {
                    var new_positon = tile.transform.position;
                    new_positon.y += 1;
                    var temp = Instantiate(EnemyPrefab, tile.transform.position, tile.transform.rotation);
                    temp.GetComponent<Enemy_Base>().EnemyInit();
                    EnemiesInPlay.Add(temp);
                }
            }
        }

        foreach(var obj in EnemiesInPlay)
        {
            obj.GetComponent<Enemy_Base>().Enemy_Update();
        }
    }
}
