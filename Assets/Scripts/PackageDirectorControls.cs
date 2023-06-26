using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PackageDirectorControls : MonoBehaviour
{
    [SerializeField] private GameObject emergencyStopButton;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject adjustmentSlider;
    [SerializeField] private GameObject speedDial;
    [SerializeField] private GameObject controlLever;

    [SerializeField] private GameObject nearPaddle;
    [SerializeField] private GameObject farPaddle;

    [SerializeField] private float sweepScaler;
    [SerializeField] private float conveyorSpeed;

    [SerializeField] private TMP_Text displayTextOne;
    [SerializeField] private TMP_Text displayTextTwo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
