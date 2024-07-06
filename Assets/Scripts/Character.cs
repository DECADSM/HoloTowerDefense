using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackPatterns
{

}

public class Character : MonoBehaviour
{
    Vector3 downVector;
    // Start is called before the first frame update
    void Start()
    {
        downVector = transform.TransformDirection(-Vector3.up);
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


}
