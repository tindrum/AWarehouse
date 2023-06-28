using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class BarcodeScannerBehavior : MonoBehaviour
{
    // raycast and angle variables
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float scannerDistanceRange = 0.5f;
    public float minAngleDifference;

    // scanner read data
    [SerializeField] private ThisIsComment commentData;

    public string lastReading_firstname;
    public string lastReading_country;
    public bool lastReading_nice;



    // Scan bleep sounds
    public AudioClip scanSound;
    public AudioClip scanBarcodeScanned;
    public AudioClip scanNoBarcode;

    // scan diagnose sounds
    public AudioClip scanStateOne;
    public AudioClip scanStateTwo;
    public AudioClip scanStateThree;

    // scanner display
    [SerializeField] private TMP_Text display_name;
    [SerializeField] private TMP_Text display_country;
    [SerializeField] private TMP_Text display_textThree;
    [SerializeField] private TMP_Text display_nice;



    public AudioSource speaker;

    // Start is called before the first frame update
    void Start()
    {
        // speaker = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Keyboard.current.spaceKey.wasPressedThisFrame)
        // {

        // }
    }

    public void NoScanRead()
    {
        speaker.PlayOneShot(scanNoBarcode, 0.5f);
    }

    public void BadScanRead()
    {
        speaker.PlayOneShot(scanStateThree, 0.5f);
    }

    public void ScanReadOK()
    {
        speaker.PlayOneShot(scanBarcodeScanned, 0.9f);
    }

    public void StartScanRead()
    {
        speaker.PlayOneShot(scanSound, 1.0f);

    }





    public void BarcodeScan()
    {
        // Layer mask for boxes and box blockers
        // speaker.PlayOneShot(scanSound, 0.9f);


        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, scannerDistanceRange))
        {
            Debug.DrawRay(raycastOrigin.position, raycastOrigin.forward * hit.distance, Color.yellow);

            if (hit.transform.gameObject.tag == "Boxes")
            {
                // get the angle difference between the box and the scanner point of origin
                // the angle will be up near 180 if the label is facing right at the scanner
                float angleDifference = Vector3.Angle(raycastOrigin.forward, hit.transform.gameObject.transform.forward);
                if (angleDifference <= minAngleDifference)
                {
                    // Get data from box
                    GetBoxData(hit);
                    DisplayBoxData();
                    ScanReadOK();
                }
            }
            else
            {
                BadScanRead();
            }
            //if (hit.collider.isTrigger)
            //{
            //    get data from box we scanned
            //    Component[] components = hit.transform.gameObject.GetComponents(typeof(Component));
            //    foreach (Component component in components)
            //    {
            //        Debug.Log(component.name);
            //    }
            //    hit.transform.gameObject.SetActive(false);
            //}
        }
        else
        {
            Debug.DrawRay(raycastOrigin.position, raycastOrigin.position * 1000, Color.red);

            NoScanRead();
        }

    }

    public void GetBoxData(RaycastHit hit)
    {
        string first = hit.transform.gameObject.GetComponent<ThisIsComment>().First;
        //lastReading_firstname = hit.transform.gameObject.commentData.First();
       // lastReading_firstname = "sue";
        lastReading_country = "Korea";
        lastReading_nice = false;

    }

    public void DisplayBoxData() {
        display_name.text = lastReading_firstname; 
        display_country.text = lastReading_country;
        if (lastReading_nice)
        {
            display_country.text = "Nice";
        }
        else
        {
            display_country.text = "Naughty";
        }
    }
}
