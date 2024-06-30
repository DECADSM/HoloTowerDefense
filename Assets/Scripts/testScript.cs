using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    Camera main;
    void Start()
    {
        main = Camera.main;
    }

    void Update()
    {
        //3D Object follows mouse cursor
        Vector3 mousePos = main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -main.transform.position.z));
        transform.position = GetMiddle(mousePos);
        print(transform.position);
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
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
        /*
        if (GetDecimal(position.x) - .5f > 0)
        {
            middle.x = position.x - GetDecimal(position.x);
        }
        else
        {
            middle.x = position.x + Mathf.Abs(GetDecimal(position.x));
        }

        if (GetDecimal(position.z) - .5f > 0)
        {
            middle.z = position.z - GetDecimal(position.z);
        }
        else
        {
            middle.z = position.z + Mathf.Abs(GetDecimal(position.z));
        }
         //*/

        return middle;
    }
}
