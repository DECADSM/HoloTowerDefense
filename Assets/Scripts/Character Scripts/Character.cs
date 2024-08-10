using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;



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

    [NonSerialized] public bool mouseDown = false;
    [NonSerialized] public bool TileSet = false;
    GameObject baseTile;

    private void OnMouseDown()
    {
        gameObject.SetActive(true);
        mouseDown = true;
    }

    private void OnMouseUp()
    {
        mouseDown = false;
        TileSet = true;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        downVector = transform.TransformDirection(-Vector3.up);
        rangeAtk = GetComponent<BoxCollider>();
        EnemyQueue = new Queue<GameObject>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
        Vector3 mousePos = ObjectHolder.Instance.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, ObjectHolder.Instance.main.nearClipPlane));
        //print(mousePos);
        if (gameObject.activeSelf && mouseDown)
        {

            gameObject.transform.position = mousePos;
            //print(baseTile.name);
            if(baseTile)
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, baseTile.transform.position.y + 1, gameObject.transform.position.z);
            else
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z);
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, downVector, out hit, 20))
        {
            if (hit.collider.CompareTag("Tile"))
            {
                baseTile = hit.collider.gameObject;
                
                TileSet = false;
                //print(baseTile.name);
            }
        }
        if(!mouseDown)
        {
            transform.parent = baseTile.transform;
            transform.localPosition = new Vector3(0, 1, 0);
        }
    }

    public virtual void Attack()
    {

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, downVector * 20);
    }
}
