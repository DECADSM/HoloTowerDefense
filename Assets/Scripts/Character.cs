using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Character : MonoBehaviour
{
    //Hololive Identifiers
    [SerializeField] string Name;
    [SerializeField] string Generation;
    [SerializeField] string Branch;

    public string GetName() { return Name; }
    public string GetGeneration() { return Generation; }
    public string GetBranch() { return Branch; }


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
        RaycastHit hit;
        if (Physics.Raycast(transform.position, downVector, out hit, 10))
        {
            if(hit.collider.CompareTag("Tile"))
                print("There's a Tile underneath me with the name: " + hit.collider.name);
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
