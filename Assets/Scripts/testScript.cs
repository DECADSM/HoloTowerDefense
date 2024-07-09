using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class testScript : MonoBehaviour, IPointerClickHandler
{
    GameObject character;
    GameObject instantiatedCharacter;
    [SerializeField] GameObject playArea;
    int tileChildren;
    bool once = true;

    void Start()
    {
        character = CharacterObjects.Instance.GetMori();
        tileChildren = playArea.transform.childCount;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        //instantiatedCharacter = Instantiate(character);
        //ObjectHolder.Instance.grid.grabbedObject = instantiatedCharacter;
    }
    void Update()
    {
        if(once)
        {
            for(int i = 0; i < tileChildren; i++)
            {
                print(playArea.transform.GetChild(i).name);
            }
            once = false;
        }
    }
}
