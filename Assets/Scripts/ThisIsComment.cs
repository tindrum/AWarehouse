using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using static Personifier;


public class ThisIsComment : MonoBehaviour
{
    // [SerializeField] public PersonalPerson personalPerson;
    [SerializeField] private Personifier personifier;


    //https://www.codeproject.com/Tips/1208852/How-to-Add-Comments-Notes-to-a-GameObject-in-Unity
    [TextArea]
    public string Notes = "Comment Here.";  // do not place your note/comment here.
                                            // Enter your note in the Unity Editor.

    public string First;

    public string Last;

    public string Country;

    public bool Nice = true;

    private PersonalPerson thisPerson;

    void Start()
    {
        //thisPerson = new PersonalPerson();
        First = Personifier.Instance.oneFirstName();
        Country = Personifier.Instance.oneCountry();
        Nice = Personifier.Instance.oneNaughtyOrNice();
        // First = thisPerson.first;
    }
}
