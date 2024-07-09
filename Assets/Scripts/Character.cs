using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Character : MonoBehaviour
{
    Vector3 downVector;
    private AttackPattern atkPattern;
    private BoxCollider rangeAtk;
    private Queue<GameObject> EnemyQueue; //Change to Enemy Script

    // Start is called before the first frame update
    void Start()
    {
        downVector = transform.TransformDirection(-Vector3.up);
        rangeAtk = GetComponent<BoxCollider>();
        EnemyQueue = new Queue<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //if left click == held
        if (Physics.Raycast(transform.position, downVector, 10))
        {
            print("There's an Object underneath me!");
        }
    }

    void SetAttackPatter(AttackPattern pattern)
    {
        atkPattern = pattern;
    }

    private void OnTriggerEnter(Collider other)
    {
        //change to enemy Script
        EnemyQueue.Enqueue(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        EnemyQueue.Dequeue();
    }


}
