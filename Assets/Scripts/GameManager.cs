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
    [SerializeField] private GameObject PirateBin;

    // My Events
    // Create a Unity Event 
    public UnityEvent m_ShipBoxes = new UnityEvent();
    public UnityEvent m_GameOver = new UnityEvent();

    // Game Global Variables
    public int boxesCreated;
    public int boxThresholdEventCount; // how many boxes created causes a score update event
    public int shipThreshodEventCount; // how many boxes created causes a shipping event

    // Display to Player stuff
    // [SerializeField] public GameObject ScoreBoard;
    // public Canvas statsDisplay;
    [SerializeField] public TMP_Text boxCountDisplay;


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
    }

    void DisconnectListenerEvents()
    {
        m_arrivalDock.m_pushPackageOut.RemoveListener(IncrementGlobalBoxCount);

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

    public void DisplayBoxesArrived()
    {
        boxCountDisplay.text = boxesCreated.ToString();
    }
}
