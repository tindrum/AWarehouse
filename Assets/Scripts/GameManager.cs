using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Unity Events
    public UnityEvent m_displayHeading = new UnityEvent();
    public UnityEvent m_displayA1 = new UnityEvent();
    public UnityEvent m_displayA2 = new UnityEvent();
    public UnityEvent m_displayB1 = new UnityEvent();
    public UnityEvent m_displayB2 = new UnityEvent();
    // more display boxes if needed


    // My scripts to listen to
    [SerializeField] private GameObject ArrivalDockGameObject;
    [SerializeField] private ArrivalDock m_arrivalDock;

    // And Objects to listen to
    [SerializeField] private BinManager m_PirateBin;
    [SerializeField] private BinManager m_UkraineBin;
    [SerializeField] private BinManager m_TunisiaBin;
    [SerializeField] private GameObject m_IndiaBin;
    [SerializeField] private GameObject m_BrazilBin;


    // My Events
    // Create a Unity Event 
    public UnityEvent m_ShipBoxes = new UnityEvent();
    public UnityEvent m_GameOver = new UnityEvent();
    public BoxShippingEvent m_shipEvent = new BoxShippingEvent();

    // Game Global Variables
    public int boxesCreated;
    public int boxThresholdEventCount; // how many boxes created causes a score update event
    public int shipThreshodEventCount; // how many boxes created causes a shipping event

    // Game Counters
    private int TunisiaDelivered;
    private int UkraineDelivered;
    private int IndiaDelivered;
    private int BrazilDelivered;
    private int PirateDelivered;

    private int TunisiaIntended;
    private int UkraineIntended;
    private int IndiaIntended;
    private int BrazilIntended;

    private int TunisiaPirates;
    private int UkrainePirates;
    private int IndiaPirates;
    private int BrazilPirates;

    // Display to Player stuff
    // [SerializeField] public GameObject ScoreBoard;
    // public Canvas statsDisplay;
    [SerializeField] public TMP_Text boxCountDisplay;
    [SerializeField] public TMP_Text deletedBoxesDisplay;
    [SerializeField] public TMP_Text otherDisplay;
    [SerializeField] public TMP_Text moreDisplay;


    // Start is called before the first frame update
    void Start()
    {
        // get the Arrival Dock object
        // ArrivalDockGameObject = GameObject.Find("ArrivalDock");
        if (ArrivalDockGameObject == null)
        {
            Debug.Log("No arrival dock found");
        }
        else { 
            Debug.Log("Arrival Dock found."); 
        }

        // initialize globals
        if (boxThresholdEventCount == 0)
        {
            boxThresholdEventCount = 20;
            Debug.Log("box theshold count has default value");
        }
        if (shipThreshodEventCount == 0)
        {
            shipThreshodEventCount = 100;
            Debug.Log("ship theshold count has default value");
        }
        //boxesCreated = 0;
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
        
    }

    void ConnectListenerEvents()
    {
        // ArrivalDockGameObject.m_pushPackageOut.AddListener(IncrementGlobalBoxCount);
        m_arrivalDock.m_pushPackageOut.AddListener(IncrementGlobalBoxCount);
        m_PirateBin.m_shipEvent.AddListener(ShippingEvent);
        m_UkraineBin.m_shipEvent.AddListener(ShippingEvent);
    }

    void DisconnectListenerEvents()
    {
        m_arrivalDock.m_pushPackageOut.RemoveListener(IncrementGlobalBoxCount);
        m_PirateBin.m_shipEvent.RemoveListener(ShippingEvent);
        m_UkraineBin.m_shipEvent.RemoveListener(ShippingEvent);

    }

    void IncrementGlobalBoxCount()
    {
        Debug.Log("IncGlobalBoxCount in GameManager");
        boxesCreated++;
        if (boxesCreated % boxThresholdEventCount == 0)
        {
            // ArrivalDockGameObject.increaseArrivalSpeed();
            DisplayBoxesArrived();
        }
        if (boxesCreated % shipThreshodEventCount == 0)
        {
            m_ShipBoxes.Invoke();
        }

    }

    public void ShippingEvent(string destination, string intendedCountry)
    {
        Debug.Log("Shipping Event is subscribed to a few bins");
        switch (destination)
        {
            case "Tunisia":
                if (intendedCountry == "Tunisia")
                {
                    TunisiaDelivered++;
                    TunisiaIntended++;
                }
                else
                {
                    TunisiaIntended++;
                }
                break;
            case "Ukraine":
                if (intendedCountry == "Ukraine")
                {
                    UkraineDelivered++;
                    UkraineIntended++;
                }
                else
                {
                    UkraineIntended++;
                }
                break;
            case "India":
                if (intendedCountry == "India")
                {
                    IndiaDelivered++;
                    IndiaIntended++;
                }
                else
                {
                    IndiaIntended++;
                }
                break;
            case "Brazil":
                if (intendedCountry == "Brazil")
                {
                    BrazilDelivered++;
                    BrazilIntended++;
                }
                else
                {
                    BrazilIntended++;
                }
                break;
            case "Pirate":
                if (intendedCountry == "Pirate")
                {
                    PirateDelivered++;
                }
                else
                {
                    // You burnt up a good kid's present
                    switch (intendedCountry)
                    {
                        case "Tunisia":
                            TunisiaPirates++;
                            break;
                        case "Ukraine":
                            UkrainePirates++;
                            break;
                        case "India":
                            IndiaPirates++;
                            break;
                        case "Brazil":
                            BrazilPirates++;
                            break;
                        default:
                            Debug.Log("Shouldn't get here");
                            break;
                    }
                }
                break;
            default:
                break;
        }

    }

    public void DisplayBoxesArrived()
    {
        boxCountDisplay.text = boxesCreated.ToString();
    }
}
