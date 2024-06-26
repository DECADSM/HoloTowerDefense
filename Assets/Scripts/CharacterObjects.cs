using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterObjects : MonoBehaviour
{
    public static CharacterObjects Instance { get; private set; }

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

    //This should hold all of the game objects of the characters that can be spawned
    [SerializeField] public GameObject Mori_Calliope;

    public GameObject GetMori() { return Mori_Calliope; }
}
