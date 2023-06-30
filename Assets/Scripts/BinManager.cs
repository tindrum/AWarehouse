using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

// using UnityEngine.XR.Interaction.Toolkit; // allows other objects to subscribe to these methods


public class BinManager : MonoBehaviour
{
    [SerializeField] private TMP_Text countryName;
    [SerializeField] private TMP_Text statusLabel;
    [SerializeField] private TMP_Text statusDisplay;

    [SerializeField] private Image FlagImage; // UI object to hold picture
    [SerializeField] private Texture2D CountryFlag; // Inspector slot to drag .png
    private Sprite CountryFlagSprite; // Code image to place in UI.Image

    // Ship Boxes
    private string Destination; // set this to a country or Pirate
    [SerializeField] private GameObject shippingPlate; // boxes touching this will be shipped
    private bool shippingNow = false;
    public float shippingTimeOut = 3.0f; // how long to wait for a box before turning off shipping
    private float shippingDelay; // how long has it been since a box was shipped in this shipping cycle
    private float accumulatedTime;
    private List<GameObject> inBin; // List of items touching the bottom of the bin

    // External scripts to UnityEvent listen
    [SerializeField] private GameManager m_gameManager;
    // private BoxShippingEvent shipEvent;

    // UnityEvents
    public BoxShippingEvent shipEvent = new BoxShippingEvent();
 
    // Start is called before the first frame update
    void Start()
    {
        CountryFlagSprite = Sprite.Create(CountryFlag, new Rect(0, 0, CountryFlag.width, CountryFlag.height), Vector2.zero);

        FlagImage.overrideSprite = CountryFlagSprite;


    }

    protected void OnEnable()
    {
        ConnectListenerEvents();
    }

    protected void OnDestroy()
    {
        DisconnectListenerEvents();
    }

    // Update is called once per frame
    void Update()
    {
        if (shippingNow)
        {
            accumulatedTime += Time.deltaTime;
            if (OneBoxShipping())
            {
                accumulatedTime = 0.0f;
            }
            if (accumulatedTime > shippingTimeOut)
            {
                shippingNow = false;
            }
        }
    }

    void ConnectListenerEvents()
    {
        m_gameManager.m_ShipBoxes.AddListener(ShipBoxesAway);
    }

    void DisconnectListenerEvents()
    {
        m_gameManager.m_ShipBoxes.RemoveListener(ShipBoxesAway);

    }

    void ShippingEvent(string destination, string country)
    {

    }


    void ShipBoxesAway()
    {
        Debug.Log("***************** Shipping Boxes *******************");
        accumulatedTime = 0.0f;
        shippingNow = true; // OnUpdate will do its stuff

    }

    bool OneBoxShipping()
    {
        GameObject oneBox;
        // return true if you found something to ship and shipped it
        if (inBin.Count == 0)
        {
            return false;
        }
        else
        {
            oneBox = inBin[0];
            UpdateStats(oneBox);
            DeleteBox(oneBox);
            return true;
        }
    }

    void UpdateStats(GameObject boxShipping)
    {
        string sampleData = "India";
        // get the country data

        // 

    }

    void DeleteBox(GameObject shippedBox)
    {
        Destroy(shippedBox);

    }


    private void OnColisionEnter(Collision collision)
    {

    }

    private void OnCollisionExit(Collision collision)
    {

    }

    public void CorrectToIndia()
    {

    }

    public void CorrectToUkraine()
    {

    }

    public void CorrectToTunisia()
    {

    }

    public void CorrectToBrazil()
    {

    }

    public void CorrectToPirate()
    {

    }


}
