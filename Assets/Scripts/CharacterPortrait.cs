using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class CharacterPortrait : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    GameObject Character;
    string thisPortrait;
    Camera main;
    bool mouseDown = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        Character.SetActive(true);
        mouseDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        mouseDown = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    private void Start()
    {
        main = Camera.main;
        thisPortrait = GetCurrentPortraitName();
        Character = GetHoloCharacter();
        //print(thisPortrait);
    }

    private void Update()
    {
        //3D Object follows mouse cursor
        Vector3 mousePos = main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -main.transform.position.z));
        if (Character.activeSelf && mouseDown)
        {
            Character.transform.position = mousePos;
            //print(transform.position);
            Character.transform.position = new Vector3(Character.transform.position.x, 1, Character.transform.position.z);
        }
        /*
        if (Character.activeSelf)
        {
            Character.transform.position = mousePos;
            //print(transform.position);
            Character.transform.position = new Vector3(Character.transform.position.x, 1, Character.transform.position.z);
        }
        //*/
    }

    GameObject GetHoloCharacter()
    {
        foreach(Character obj in CharacterObjects.Instance.HoloCharacters)
        {
            if (obj.gameObject.name.Contains(thisPortrait))
                return obj.gameObject;
        }
        return null;
    }

    string GetCurrentPortraitName()
    {
        print(gameObject.name);
        foreach (string holoname in CharacterObjects.Instance.MythNames)
        {
            if (gameObject.name.Contains(holoname))
                return holoname;
        }
        foreach (string holoname in CharacterObjects.Instance.PromiseNames)
        {
            if (gameObject.name.Contains(holoname))
                return holoname;
        }
        foreach (string holoname in CharacterObjects.Instance.AdventNames)
        {
            if (gameObject.name.Contains(holoname))
                return holoname;
        }
        foreach (string holoname in CharacterObjects.Instance.JusticeNames)
        {
            if (gameObject.name.Contains(holoname))
                return holoname;
        }
        return "";
    }
}
