using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

namespace UnityEngine.XR.Content.Interaction
{
    /// <summary>
    /// Use this class to present locomotion control schemes and configuration preferences,
    /// and respond to player input in the UI to set them.
    /// </summary>

    public class PackageDirectorControls : MonoBehaviour
    {

        // public UnityEvent LeverMoved;

        [SerializeField]
        [Tooltip("Stores the GameObject reference used to Emergency Stop the machine.")]
        XRPushButton m_EmergencyStopButton;
        [SerializeField]
        [Tooltip("Stores the GameObject reference used to Start the machine.")]
        XRPushButton m_StartButton;
        [SerializeField]
        [Tooltip("Stores the GameObject reference used to Adjust machine angle stops.")]
        XRSlider m_AdjustmentSlider;
        [SerializeField]
        [Tooltip("Stores the GameObject reference used to adjust the machine's speed.")]
        XRKnob m_SpeedDial;
        [SerializeField]
        [Tooltip("Stores the GameObject reference used to move the machine's Paddles up or down.")]
        XRLever m_ControlLever;

 
        [SerializeField] List<GameObject> conveyorMachineList;

        [SerializeField] private PackageDirectorDisplay displays;
        [SerializeField] private PackageDirectorMechanical mechanicalControls;
       
        public AudioSource speaker;

        public AudioClip leverSound;
        public AudioClip startupSound;

        



        // Start is called before the first frame update
        void Start()
        {
            //emergencyStopButton = transform.Find("EmergencyStopPushButton").gameObject;
            //startButton = transform.Find("StartPushButton").gameObject;
            //adjustmentSlider = transform.Find("AdjustmentSlider").gameObject;
            //speedDial = transform.Find("SpeedDial").gameObject;
            //controlLever = transform.Find("SelectLever").gameObject;

            // conveyorSpeed = 0.8f;

            //LeverMoved += OnLeverMoved;

            
        }

        void ConnectControlEvents()
        {
            m_ControlLever.onLeverActivate.AddListener(LeverUp);
            m_ControlLever.onLeverDeactivate.AddListener(LeverDown);
            m_EmergencyStopButton.onPress.AddListener(EmergencyStop);
            // m_EmergencyStopButton.onRelease.AddListener();
            m_StartButton.onPress.AddListener(MachineStart);
            m_SpeedDial.onValueChange.AddListener(SetMachineSpeed);
            //Debug.Log("PackageDirectorControls connected");

        }

        void DisconnectControlEvents()
        {
            m_ControlLever.onLeverActivate.RemoveListener(LeverUp);
            m_ControlLever.onLeverDeactivate.RemoveListener(LeverDown);
            m_EmergencyStopButton.onPress.RemoveListener(EmergencyStop);
            // m_EmergencyStopButton.onRelease.RemoveListener();
            m_StartButton.onPress.RemoveListener(MachineStart);
            m_SpeedDial.onValueChange.RemoveListener(SetMachineSpeed);



        }

        void InitializeControls()
        {
            // sets values of control objects
            // Speed dial get speed from Mechanical, and set the dial's setting

            // Lever handle get angle from Mechanical, and sets handle position
        }

        protected void OnEnable()
        {
            ConnectControlEvents();
            InitializeControls();
            LeverUp();
        }

        protected void OnDisable()
        {
            DisconnectControlEvents();
        }

        void OnDestroy()
        {
            //LeverMoved -= OnLeverMoved;

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void LeverUp()
        {
            //speaker.PlayOneShot(leverSound, 0.2f);

            mechanicalControls.CommandSweepAngle(mechanicalControls.nearCCWSweepMax);
        }

        public void LeverDown()
        {
            //speaker.PlayOneShot(leverSound, 0.2f);

            mechanicalControls.CommandSweepAngle(mechanicalControls.nearCWSweepMax);

        }

        public void MachineStart()
        {
            speaker.PlayOneShot(startupSound, 0.2f);

        }

        public void SetMachineSpeed(float newSpeed)
        {
            float machineSpeedControlSetting = Mathf.Lerp(m_SpeedDial.minAngle, m_SpeedDial.maxAngle, newSpeed);
            // conveyorSpeed = machineSpeedControlSetting * 0.05f;
            foreach (GameObject conveyorBelt in conveyorMachineList)
            {
                // set the speed of this conveyor belt 
            }
        }

        public void EmergencyStop()
        {

        }

 

    }

}
