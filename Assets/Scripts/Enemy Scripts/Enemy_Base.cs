using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Base : MonoBehaviour
{
    [SerializeField] Tile currentTile;
    [SerializeField] Tile SpawnTile;
    [NonSerialized] public int AttackRange; // >= 1
    public Tile destination;
    EnemyPathFinding pathFinder;

    int health;
    int damage;
    bool isDead;

    int Cores;

    float atkSpeed;

    Vector3 downVector;

    //temp
    /*
    private void Start()
    {
        EnemyInit();
    }
    //*/
    public void EnemyInit()
    { 
        downVector = transform.TransformDirection(-Vector3.up);
        pathFinder = GetComponent<EnemyPathFinding>();
        destination = GetDestination();

        //look at Home Tile
        transform.LookAt(destination.transform.position);

        RaycastHit hit;
        var raycast_position = transform.position;
        raycast_position.y += 10;
        bool test = Physics.Raycast(raycast_position, downVector, out hit, 20);
        if (test)
        {
            if (hit.collider.CompareTag("Tile") || hit.collider.CompareTag("EnemySpawn") || hit.collider.CompareTag("Home"))
            {
                currentTile = SpawnTile = hit.collider.GetComponent<Tile>();
            }
        }

        pathFinder.PathFindingInit();
    }

    private Tile GetDestination()
    {
       foreach(var tile in ObjectHolder.Instance.tiles)
        {
            if (tile.CompareTag("Home"))
                return tile;

        }
        return null;
    }

    virtual public void Enemy_Update()
    {
        pathFinder.MoveAgent();
    }

    virtual public void Attack()
    {
        Character character = ObjectHolder.Instance.tiles[currentTile.ID + AttackRange].CharacterOnTile;

    }
    public void TakeDamage(int amt)
    {
        health -= amt;
        if (health <= 0)
        {
            isDead = true;
            Dead();
        }
    }

    public void Dead()
    {

    }

    public Tile GetCurrentTile()
    {
        return currentTile;
    }
    public Tile GetSpawnTile()
    {
        return SpawnTile;
    }
    protected virtual void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, downVector, out hit, 20))
        {
            if (hit.collider.CompareTag("Tile") || hit.collider.CompareTag("EnemySpawn") || hit.collider.CompareTag("Home"))
            {
                currentTile = hit.collider.GetComponent<Tile>();
            }
        }
    }
}
