using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class CharacterPortrait : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    GameObject character;
    Character charScript;
    string thisPortrait;
    Camera main;
    public void OnPointerDown(PointerEventData eventData)
    {
        character.SetActive(true);
        charScript.mouseDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        charScript.mouseDown = false;
        charScript.TileSet = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    private void Start()
    {
        thisPortrait = GetCurrentPortraitName();
        GetHoloCharacter();
        //print(thisPortrait);
    }

    private void Update()
    {

    }

    void GetHoloCharacter()
    {
        foreach(Character obj in CharacterObjects.Instance.HoloCharacters)
        {
            if (obj.gameObject.name.Contains(thisPortrait))
            {
                character = obj.gameObject;
                charScript = obj;
            }
        }
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
