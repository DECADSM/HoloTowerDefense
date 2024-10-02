using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [NonSerialized] public Character CharacterOnTile;
    [NonSerialized] public Enemy_Base enemy;
    public bool EnemySpawner = false;
    public int SpawnNumEnemies = 0;


    [NonSerialized] public int ID;

}
