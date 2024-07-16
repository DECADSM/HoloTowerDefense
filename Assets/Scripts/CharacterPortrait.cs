using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class CharacterPortrait : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    GameObject Character;
    string[] MythNames = { "Mori", "Kiara", "Gura", "Ina", "Ame"};
    string[] PromiseNames = { "Bae", "Fauna", "Kronii", "Mumei", "Irys"};
    string[] AdventNames = { "Nerisa", "Shiori", "FuwaMoco", "Bijou"};
    string[] JusticeNames = { "Elizabeth", "Cecilia", "Gigi", "Raora"};
    string thisPortrait;
    Camera main;
    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    private void Start()
    {
        main = Camera.main;
        thisPortrait = GetCurrentPortraitName();
        print(thisPortrait);
    }

    private void Update()
    {
        //3D Object follows mouse cursor
        Vector3 mousePos = main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -main.transform.position.z));
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
        foreach(GameObject obj in CharacterObjects.Instance.HoloCharacters)
        {
            if (obj.name.Contains(thisPortrait))
                return obj;
        }
        return null;
    }

    string GetCurrentPortraitName()
    {
        print(gameObject.name);
        foreach (string holoname in MythNames)
        {
            if (gameObject.name.Contains(holoname))
                return holoname;
        }
        foreach (string holoname in PromiseNames)
        {
            if (gameObject.name.Contains(holoname))
                return holoname;
        }
        foreach (string holoname in AdventNames)
        {
            if (gameObject.name.Contains(holoname))
                return holoname;
        }
        foreach (string holoname in JusticeNames)
        {
            if (gameObject.name.Contains(holoname))
                return holoname;
        }
        return "";
    }
}
