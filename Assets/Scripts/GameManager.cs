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
    private GameObject ArrivalDockGameObject;
    private ArrivalDock m_arrivalDock;
 


    // Game Global Variables
    public int boxesCreated;
    int boxThresholdEventCount; // how many boxes created causes a cleanup/shipping event

    // Display to Player stuff
    public GameObject ScoreBoard;
    public Canvas statsDisplay;
    public TMP_Text boxCountDisplay;


    // Start is called before the first frame update
    void Start()
    {
        // get the Arrival Dock object
        ArrivalDockGameObject = GameObject.Find("ArrivalDock");
        ScoreBoard = GameObject.Find("MainScreen");
        statsDisplay = ScoreBoard.GetComponent<Canvas>();
        // m_arrivalDock = ArrivalDockGameObject.ArrivalDock;

        // initialize globals
        boxThresholdEventCount = 20;
        boxesCreated = 0;
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
        boxesCreated++;
        if (boxesCreated % boxThresholdEventCount == 0)
        {
            // ArrivalDockGameObject.increaseArrivalSpeed();
            DisplayBoxesArrived();

        }
    }

    public void DisplayBoxesArrived()
    {
        boxCountDisplay.text = boxesCreated.ToString();
    }
}
