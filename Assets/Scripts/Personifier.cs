using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Personifier : MonoBehaviour
{
    // [SerializeField] public PersonalPerson personalPerson;

    public static Personifier Instance;
    // This will give boxes a personal touch:
    // First Name
    // Last Name
    // Country
    // Naughty or Nice?
    private static List<string> First = new List<string>();
    private static List<string> Country = new List<string>();

    // Include access to ThisIsComment

    // Start is called before the first frame update
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

    
    void Start()
    {
        makeFirstNameList();
        makeCountryList();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void makeFirstNameList()
    {
        First = new List<string>(new string[] {
            "Jan",
            "David",
            "Anne",
            "Mark",
            "Mike",
            "Devin",
            "Melanie",
            "Patricia",
            "Tierney",
            "Dominic",
            "Lauren",
            "Whitney"
        });
    }

    void makeCountryList()
    {
        Country = new List<string>(new string[] {
            "Peru",
            "Bolivia",
            "Costa Rica",
            "Norway"
        });
    }

    public string oneFirstName()
    {
        string firstName = string.Empty;
        firstName = new string(First[UnityEngine.Random.Range(0, First.Count)]);
        if (!string.IsNullOrEmpty(firstName))
        {
            return firstName;
        }
        else
        {
            Debug.Log("The string is not an object");
            return null;
        }
        // return First[UnityEngine.Random.Range(0, First.Count)];
    }

}
