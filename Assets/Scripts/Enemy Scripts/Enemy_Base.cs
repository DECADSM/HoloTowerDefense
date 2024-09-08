using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Base : MonoBehaviour
{
    [SerializeField]Tile currentTile;
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
    //*
    private void Start()
    {
        EnemyInit();
    }
    //*/
    void EnemyInit()
    {
        downVector = transform.TransformDirection(-Vector3.up);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, downVector, out hit, 20))
        {
            if (hit.collider.CompareTag("Tile"))
            {
                currentTile = hit.collider.GetComponent<Tile>();
            }
        }
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
    protected virtual void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, downVector, out hit, 20))
        {
            if (hit.collider.CompareTag("Tile"))
            {
                currentTile = hit.collider.GetComponent<Tile>();
            }
        }
    }
}
