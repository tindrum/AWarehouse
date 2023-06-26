using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class BarcodeScannerBehavior : MonoBehaviour
{
    public Transform scanPoint;
    public LayerMask scannableLayer;

    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float scannerDistanceRange = 0.5f;
    public float minAngleDifference;

    public AudioClip scanSound;
    public AudioClip scanBarcodeScanned;
    public AudioClip scanNoBarcode;

    public AudioClip scanStateOne;
    public AudioClip scanStateTwo;
    public AudioClip scanStateThree;

    public PhysicMaterial barcodeMaterial;


    private AudioSource speaker;

    // Start is called before the first frame update
    void Start()
    {
        speaker = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {

        }
    }

    public void NoScanRead()
    {
        speaker.PlayOneShot(scanNoBarcode, 0.9f);
    }

    public void BadScanRead()
    {
        speaker.PlayOneShot(scanStateThree, 0.9f);
    }

    public void ScanReadOK()
    {
            speaker.PlayOneShot(scanBarcodeScanned, 0.9f);
    }



    

    public void BarcodeScan()
    {
        // Layer mask for boxes and box blockers
        speaker.PlayOneShot(scanSound, 0.9f);


        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, scannerDistanceRange))
        {
            Debug.DrawRay(raycastOrigin.position, raycastOrigin.forward * hit.distance, Color.yellow);

            if (hit.transform.gameObject.tag == "Boxes")
            {
                // get the angle difference between the box and the scanner point of origin
                float angleDifference = Vector3.Angle(raycastOrigin.forward, hit.transform.gameObject.transform.forward);
                if (angleDifference >= minAngleDifference)
                {
                    // the angle will be up near 180 if the label is facing right at the scanner
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
}
