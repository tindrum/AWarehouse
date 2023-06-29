using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // allows other objects to subscribe to these methods


public class BinManager : MonoBehaviour
{
    [SerializeField] private TMP_Text countryName;
    [SerializeField] private TMP_Text statusLabel;
    [SerializeField] private TMP_Text statusDisplay;

    [SerializeField] private Image FlagImage; // UI object to hold picture
    [SerializeField] private Texture2D CountryFlag; // Inspector slot to drag .png
    private Sprite CountryFlagSprite; // Code image to place in UI.Image


    // Start is called before the first frame update
    void Start()
    {
        CountryFlagSprite = Sprite.Create(CountryFlag, new Rect(0, 0, CountryFlag.width, CountryFlag.height), Vector2.zero);

        FlagImage.overrideSprite = CountryFlagSprite;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShipBoxesAway()
    {

    }
}
