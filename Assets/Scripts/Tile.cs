using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Character CharacterOnTile;
    public List<Enemy_Base> enemies = new List<Enemy_Base>();
    public bool EnemySpawner = false;
    public int SpawnNumEnemies = 0;

    public List<Tile> PresetPath;


    [NonSerialized] public int ID;


    public void SetCharacter(Character talent) { CharacterOnTile = talent;  }
    public void AddEnemy(Enemy_Base enemy) { enemies.Add(enemy); }
    public void ClearEnemies() { enemies.Clear(); }
}
