using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackPattern
{
    Melee,
    Range
}


public class CharacterObjects : MonoBehaviour
{
    public static CharacterObjects Instance { get; private set; }
    //HoloEN Generations and Names
    [NonSerialized] public string[] MythNames = { "Mori", "Kiara", "Gura", "Ina", "Ame" };
    [NonSerialized] public string[] PromiseNames = { "Bae", "Fauna", "Kronii", "Mumei", "Irys" };
    [NonSerialized] public string[] AdventNames = { "Nerisa", "Shiori", "FuwaMoco", "Bijou" };
    [NonSerialized] public string[] JusticeNames = { "Elizabeth", "Cecilia", "Gigi", "Raora" };

    /*[NonSerialized] */ public List<Character> HoloCharacters;

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
        
    }
    
    public void AddHoloCharacter(Character obj)
    {
        HoloCharacters.Add(obj);
    }
    //This should hold all of the game objects of the characters that can be 

    //public GameObject GetMori() { return Mori_Calliope; }
}
