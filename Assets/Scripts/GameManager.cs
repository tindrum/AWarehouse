using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Unity.Mathematics;

public class GameManager : MonoBehaviour
{
    // Unity Events
    //public UnityEvent m_displayHeading = new UnityEvent();
    //public UnityEvent m_displayA1 = new UnityEvent();
    //public UnityEvent m_displayA2 = new UnityEvent();
    //public UnityEvent m_displayB1 = new UnityEvent();
    //public UnityEvent m_displayB2 = new UnityEvent();
    // more display boxes if needed
    public static Personifier Instance;


    // My scripts to listen to
    [SerializeField] private GameObject ArrivalDockGameObject;
    [SerializeField] private ArrivalDock m_arrivalDock;

    // And Objects to listen to
    [SerializeField] private BinManager m_PirateBin;
    [SerializeField] private BinManager m_UkraineBin;
    [SerializeField] private BinManager m_TunisiaBin;
    [SerializeField] private BinManager m_IndiaBin;
    [SerializeField] private BinManager m_BrazilBin;


    // My Events
    // Create a Unity Event 
    public UnityEvent m_ShipBoxes = new UnityEvent();
    public UnityEvent m_GameOver = new UnityEvent();
    public BoxShippingEvent m_shipEvent = new BoxShippingEvent();

    // Game Global Variables
    public int boxesCreated;
    public int boxesShipped;
    public float shippedPercent;
    public int boxThresholdEventCount; // how many boxes created causes a score update event
    public int shipThreshodEventCount; // how many boxes created causes a shipping event
    public int levelUpThreshold; // increase speed of box arrival when multiple of this many created
    public float speedIncreaseMultiplier; // make slightly less than 1.0f
    
    private float previousRoundSuccessRate; // save how many were delivered last round
    public int minimumRoundCount;
    public float minimumPercentageShippedAllowed; // lose game if shipping percent drops below this
    private int roundCount = 0;

    private float AverageSuccessRate;
    [SerializeField] private GameObject lightGameObject;
    private bool gameOver;

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
    [SerializeField] public TMP_Text headline;
    [SerializeField] public TMP_Text firedReason;
    [SerializeField] public TMP_Text boxCountDisplay;
    [SerializeField] public TMP_Text rightTopHeading;
    [SerializeField] public TMP_Text BrazilPercent;
    [SerializeField] public TMP_Text IndiaPercent;
    [SerializeField] public TMP_Text TunisiaPercent;
    [SerializeField] public TMP_Text UkrainePercent;
    [SerializeField] public TMP_Text RejectedAccuracy;

    [SerializeField] public TMP_Text data1;
    [SerializeField] public TMP_Text data2;
    [SerializeField] public TMP_Text data3;
    [SerializeField] public TMP_Text data4;


    // Start is called before the first frame update
    void Start()
    {
        // get the Arrival Dock object
        // ArrivalDockGameObject = GameObject.Find("ArrivalDock");
        if (ArrivalDockGameObject == null)
        {
//            Debug.Log("No arrival dock found");
        }
        else { 
 //           Debug.Log("Arrival Dock found."); 
        }

        // initialize globals
        if (boxThresholdEventCount == 0)
        {
            boxThresholdEventCount = 20;
//            Debug.Log("box theshold count has default value");
        }
        if (shipThreshodEventCount == 0)
        {
            shipThreshodEventCount = 100;
//            Debug.Log("ship theshold count has default value");
        }
        //boxesCreated = 0;
        previousRoundSuccessRate = 100.0f;
        shippedPercent = 100.0f;
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
        m_IndiaBin.m_shipEvent.AddListener(ShippingEvent);
        m_TunisiaBin.m_shipEvent.AddListener(ShippingEvent);
        m_BrazilBin.m_shipEvent.AddListener(ShippingEvent);
    }

    void DisconnectListenerEvents()
    {
        m_arrivalDock.m_pushPackageOut.RemoveListener(IncrementGlobalBoxCount);
        m_PirateBin.m_shipEvent.RemoveListener(ShippingEvent);
        m_UkraineBin.m_shipEvent.RemoveListener(ShippingEvent);
        m_IndiaBin.m_shipEvent.RemoveListener(ShippingEvent);
        m_TunisiaBin.m_shipEvent.RemoveListener(ShippingEvent);
        m_BrazilBin.m_shipEvent.RemoveListener(ShippingEvent);

    }

    void IncrementGlobalBoxCount()
    {
//        Debug.Log("IncGlobalBoxCount in GameManager");
        boxesCreated++;
        if (boxesCreated % boxThresholdEventCount == 0)
        {
            // ArrivalDockGameObject.increaseArrivalSpeed();
            UpdateScores();
        }
        if (boxesCreated % shipThreshodEventCount == 0)
        {
//            Debug.Log("m_ShipBoxes.Invoke()");
            m_ShipBoxes.Invoke();
        }
        if (boxesCreated % levelUpThreshold == 0)
        {
            LevelUp();
        }

    }

    public void ShippingEvent(string destination, string intendedCountry)
    {
        boxesShipped++;
        //Debug.Log("=========================================");
        //Debug.Log("Shipping Event is subscribed to a few bins");
        //Debug.Log("     and has been called.");
        //Debug.Log("==========================================");
        switch (intendedCountry)
        {
            case "Tunisia":
                TunisiaIntended++;
                break;
            case "Ukraine":
                UkraineIntended++;
                break;
            case "India":
                IndiaIntended++;
                break;
            case "Brazil":
                BrazilIntended++;
                break;
            case "Pirate":
                
                break;
            default:
                Debug.Log("Shouldn't get here");
                break;
        }

        switch (destination)
        {
            case "Tunisia":
                if (intendedCountry == "Tunisia")
                {
                    TunisiaDelivered++;
                }
                break;
            case "Ukraine":
                if (intendedCountry == "Ukraine")
                {
                    UkraineDelivered++;
                }
                break;
            case "India":
                if (intendedCountry == "India")
                {
                    IndiaDelivered++;
                }
                break;
            case "Brazil":
                if (intendedCountry == "Brazil")
                {
                    BrazilDelivered++;
                }
                break;
            case "Pirate":
                if (intendedCountry == "Pirate")
                {
                    PirateDelivered++;
                } else
                {
                    // You burnt up a good kid's present
                    headline.text = "You're Fired.";
                    firedReason.text = "A nice kid's present went into the naughty bin.";

                    EndGame();
                }
                break;
            default:
                break;
        }

    }

    public void UpdateScores()
    {
        roundCount += 1; // don't let the game end on the first UpdateScores() call
        shippedPercent = ( (float)boxesShipped / (float)boxesCreated) * 100.0f;
        float TunisiaSuccessRate = 0.0f;
        float BrazilSuccessRate = 0.0f;
        float IndiaSuccessRate = 0.0f;
        float UkraineSuccessRate = 0.0f;

        float AverageSuccessRate = 100.0f;
        boxCountDisplay.text = boxesCreated.ToString();
        rightTopHeading.text = "Packages Received";

        if (TunisiaIntended > 0) {
             TunisiaSuccessRate = ((float)TunisiaDelivered / (float)TunisiaIntended) * 100.0f;
        } 
        if (BrazilIntended > 0) {
            BrazilSuccessRate = ((float)BrazilDelivered / (float)BrazilIntended) * 100.0f;
        }
        if (IndiaIntended > 0) {
            IndiaSuccessRate = ((float)IndiaDelivered / (float)IndiaIntended) * 100.0f;
        }
        if (UkraineIntended > 0) {
            UkraineSuccessRate = ((float)UkraineDelivered / (float)UkraineIntended) * 100.0f;
        }

        AverageSuccessRate = (TunisiaSuccessRate + BrazilSuccessRate + IndiaSuccessRate + UkraineSuccessRate) / 4.0f;
        if (shippedPercent < minimumPercentageShippedAllowed && roundCount > minimumRoundCount)
        {
            headline.text = "We're letting you go.";
            firedReason.text = "You work too slowly, and not very accurately.";

            EndGame();
        }

        int rejectedTotal = TunisiaPirates + UkrainePirates + IndiaPirates + BrazilPirates;

        //Debug.Log("****************************");
        //Debug.Log("Ukraine Delivered");
        //Debug.Log(UkraineDelivered.ToString());
        //Debug.Log(IndiaDelivered.ToString());
        //Debug.Log(BrazilDelivered.ToString());
        //Debug.Log(PirateDelivered.ToString());
        //Debug.Log("Ukraine Intended ******");
        //Debug.Log(UkraineIntended.ToString());
        //Debug.Log(TunisiaIntended.ToString());
        //Debug.Log(IndiaIntended.ToString());
        //Debug.Log(BrazilIntended.ToString());
        //Debug.Log("Ukraine Pirates**********");
        //Debug.Log(UkrainePirates.ToString());
        //Debug.Log(TunisiaPirates.ToString());
        //Debug.Log(IndiaPirates.ToString());
        //Debug.Log(BrazilPirates.ToString());
        //Debug.Log("Ukraine Success Rate*************");
        //Debug.Log(UkraineSuccessRate.ToString());
        //Debug.Log(TunisiaSuccessRate.ToString());
        //Debug.Log(BrazilSuccessRate.ToString());
        //Debug.Log(IndiaSuccessRate.ToString());
        //Debug.Log("****************************");

        // int is the way to get fewer digits
        int bsr = (int)BrazilSuccessRate;
        int isr = (int)IndiaSuccessRate;
        int tsr = (int)TunisiaSuccessRate;
        int usr = (int)UkraineSuccessRate;
        int sp = (int)shippedPercent;

        firedReason.text = "Boxes Shipped out: " + sp.ToString() + "%";
        BrazilPercent.text = bsr.ToString() + "%";
        IndiaPercent.text = isr.ToString() + "%";
        TunisiaPercent.text = tsr.ToString() + "%";
        UkrainePercent.text = usr.ToString() + "%";
        RejectedAccuracy.text = rejectedTotal.ToString();

        data1.text = BrazilDelivered.ToString();
        data2.text = IndiaDelivered.ToString();
        data3.text = TunisiaDelivered.ToString();
        data4.text = UkraineDelivered.ToString();
    }

    public void LevelUp()
    {
        // remap for player's AverageSuccessRate
        float speedUp = math.remap(100.0f, 60.0f,speedIncreaseMultiplier, 0.85f, shippedPercent);
        // display new box rate
        boxCountDisplay.text = m_arrivalDock.timerSeconds.ToString();
        rightTopHeading.text = "Box Speed";
        // call increaseArrivalSpeed on ArrivalDock
        m_arrivalDock.increaseArrivalSpeed(speedUp);
    }

    public void EndGame()
    {
        // stop delivering boxes
        m_arrivalDock.increaseArrivalSpeed(100000.0f);
        gameOver = true;
        StartCoroutine(myWaitCoroutine());
    }

    IEnumerator myWaitCoroutine()
    {

        HideAll();
        yield return new WaitForSeconds(2);// Wait for one second

        // All your Post-Delay Logic goes here:
        // Run functions
        // Set your Values
        // Or whatever else
        SceneTransitionManager.singleton.GoToSceneAsync(2);

    }

    public void HideAll()
    {
        // turning down the lights on the fired employee
        lightGameObject.GetComponent<Light>().intensity = 1.1f;
    }
    //StartCoroutine(myWaitCoroutine());

}
