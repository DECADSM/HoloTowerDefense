using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectHolder : MonoBehaviour
{
    // Start is called before the first frame update

    public static ObjectHolder Instance { get; private set; }

    public GameObject playArea;
    [SerializeField] GameObject[] CharacterPrefabs;
    public Tile[] tiles;
    [NonSerialized]public Camera main;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        main = Camera.main;
        //Temp setup for tile loading
        tiles = new Tile[playArea.transform.childCount];
        for(int i = 0; i < playArea.transform.childCount; i++)
        {
            Tile child = playArea.transform.GetChild(i).GetComponent<Tile>();
            child.ID = i;
            tiles[i] = child;
        }
        foreach (GameObject obj in CharacterPrefabs)
        {
            GameObject temp = Instantiate(obj);
            temp.SetActive(false);
            CharacterObjects.Instance.AddHoloCharacter(temp.GetComponent<Character>());
        }
        //CharacterObjects.Instance.Mori_Calliope = Instantiate(CharacterPrefab);
        //CharacterObjects.Instance.Mori_Calliope.SetActive(false);

    }
}
