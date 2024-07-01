using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class testScript : MonoBehaviour, IPointerClickHandler
{
    GameObject character;
    GameObject instantiatedCharacter;

    void Start()
    {
        character = CharacterObjects.Instance.GetMori();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        instantiatedCharacter = Instantiate(character);
        //ObjectHolder.Instance.grid.grabbedObject = instantiatedCharacter;
    }
    void Update()
    {
        
    }
}
