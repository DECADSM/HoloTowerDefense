using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    Camera main;
    static int LEFT_MOUSE_BUTTON = 0;
    bool place = false;

    [NonSerialized] public GameObject grabbedObject;
    void Start()
    {
        main = Camera.main;
    }

    void Update()
    {
        //3D Object follows mouse cursor
        Vector3 mousePos = main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -main.transform.position.z));
        if (!place && grabbedObject)
        {
            grabbedObject.transform.position = mousePos;
            //print(transform.position);
            grabbedObject.transform.position = new Vector3(grabbedObject.transform.position.x, 1, grabbedObject.transform.position.z);
        }

        if (Input.GetMouseButton(LEFT_MOUSE_BUTTON) && grabbedObject)
        {
            place = true;
            grabbedObject.transform.position = GetMiddle(mousePos);
            //print(transform.position);
            grabbedObject.transform.position = new Vector3(grabbedObject.transform.position.x, 1, grabbedObject.transform.position.z);

        }
    }

    float GetDecimal(float num)
    {
        int int_num = (int)num;

        float deci = num - int_num;
        return deci;
    }

    Vector3 GetMiddle(Vector3 position)
    {
        Vector3 middle = position;

        float modX = position.x % 0.5f, modZ = position.z % 0.5f;

        float midX = position.x - modX, midZ = position.z - modZ;
        if (midX % 1 == 0 && midX != 0)
            middle.x = midX + .5f;
        if (midZ % 1 == 0 && midZ != 0)
            middle.z = midZ + .5f;

        return middle;
    }
}
