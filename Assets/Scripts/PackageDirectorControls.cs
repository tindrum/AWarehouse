using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;
using UnityEngine.Events;
//using SelectLever;

public class PackageDirectorControls : MonoBehaviour
{
    
    // public UnityEvent LeverMoved;

    [SerializeField] private GameObject emergencyStopButton;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject adjustmentSlider;
    [SerializeField] private GameObject speedDial;
    [SerializeField] private GameObject controlLever;
    public Component controlLeverScript;

    [SerializeField] private float conveyorSpeed;



    // Start is called before the first frame update
    void Start()
    {
        emergencyStopButton = transform.Find("EmergencyStopPushButton").gameObject;
        startButton = transform.Find("StartPushButton").gameObject;
        adjustmentSlider = transform.Find("AdjustmentSlider").gameObject;
        speedDial = transform.Find("SpeedDial").gameObject;
        controlLever = transform.Find("SelectLever").gameObject;

        conveyorSpeed = 0.8f;

        //LeverMoved += OnLeverMoved;
    }

    private void OnEnable()
    {
        // Script controlLeverScript = controlLever.GetComponent(XRLever);
    }

    void OnDestroy()
    {
        //LeverMoved -= OnLeverMoved;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLeverMoved()
    {
        float controlValue;
        if (controlLever != null)
        {
            if (true) // (controlLever.GetComponent<XRLever>().Value == true)
            {
                // tell Mechanical to move to -90
            }
            else
            {
             // tell Mechanical to move to +90
            }
        }
       

    }

 
}
