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

    // scan country bleep sounds
    public AudioClip BrazilBleep;
    public AudioClip IndiaBleep;
    public AudioClip TunisiaBleep;
    public AudioClip UkraineBleep;
    public AudioClip PirateBleep;

    public AudioClip errorBleep;

    // scanner display
    [SerializeField] private TMP_Text display_name;
    [SerializeField] private TMP_Text display_country;
    [SerializeField] private TMP_Text display_textThree;
    [SerializeField] private TMP_Text display_nice;
    // image display
    [SerializeField] private Image FlagImage;
    [SerializeField] private Texture2D IndiaFlag;
    [SerializeField] private Texture2D UkraineFlag;
    [SerializeField] private Texture2D TunisiaFlag;
    [SerializeField] private Texture2D BrazilFlag;
    [SerializeField] private Texture2D PirateFlag;
    private Sprite IndiaFlagSprite;
    private Sprite TunisiaFlagSprite;
    private Sprite UkraineFlagSprite;
    private Sprite BrazilFlagSprite;
    private Sprite PirateFlagSprite;



    public AudioSource speaker;

    // Start is called before the first frame update
    void Start()
    {
        // speaker = GetComponent<AudioSource>();
        IndiaFlagSprite = Sprite.Create(IndiaFlag, new Rect(0, 0, IndiaFlag.width, IndiaFlag.height), Vector2.zero);
        TunisiaFlagSprite = Sprite.Create(TunisiaFlag, new Rect(0, 0, TunisiaFlag.width, TunisiaFlag.height), Vector2.zero);
        BrazilFlagSprite = Sprite.Create(BrazilFlag, new Rect(0, 0, BrazilFlag.width, BrazilFlag.height), Vector2.zero);
        UkraineFlagSprite = Sprite.Create(UkraineFlag, new Rect(0, 0, UkraineFlag.width, UkraineFlag.height), Vector2.zero);
        PirateFlagSprite = Sprite.Create(PirateFlag, new Rect(0, 0, PirateFlag.width, PirateFlag.height), Vector2.zero);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Keyboard.current.spaceKey.wasPressedThisFrame)
        // {

        // }
    }

    public void CountryScanBleep(string country)
    {
        if (lastReading_nice)
        {
            switch (lastReading_country)
            {
                case "Tunisia":
                    speaker.PlayOneShot(TunisiaBleep, 0.5f);
                    break;
                case "Ukraine":
                    speaker.PlayOneShot(UkraineBleep, 0.5f);
                    break;
                case "India":
                    speaker.PlayOneShot(IndiaBleep, 0.5f);
                    break;
                case "Brazil":
                    speaker.PlayOneShot(BrazilBleep, 0.5f);
                    break;
                default:
                    speaker.PlayOneShot(errorBleep, 0.5f);
                    break;
            }
        }
        else
        {
            // bad kids get pirate flag
            speaker.PlayOneShot(PirateBleep, 0.5f);
        }
    }

    public void NoScanRead()
    {
        speaker.PlayOneShot(scanNoBarcode, 0.5f);
    }

    public void BadScanRead()
    {
        speaker.PlayOneShot(errorBleep, 0.5f);
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
                    CountryScanBleep(lastReading_country);
                    // ScanReadOK();
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
            BadScanRead();
            // NoScanRead();
        }

    }

    public void GetBoxData(RaycastHit hit)
    {
        lastReading_firstname = hit.transform.gameObject.GetComponent<ThisIsComment>().First;
        lastReading_country = hit.transform.gameObject.GetComponent<ThisIsComment>().Country;
        lastReading_nice = hit.transform.gameObject.GetComponent<ThisIsComment>().Nice;


    }

    public void Bleep(RaycastHit hit)
    {

    }

    public void DisplayBoxData() {
        display_name.text = lastReading_firstname; 
        display_country.text = lastReading_country;
        // set flag image
        if (lastReading_nice)
        {
            switch (lastReading_country)
            {
                case "Tunisia":
                    FlagImage.overrideSprite = TunisiaFlagSprite;
                    break;
                case "Ukraine":
                    FlagImage.overrideSprite = UkraineFlagSprite;
                    break;
                case "India":
                    FlagImage.overrideSprite = IndiaFlagSprite;
                    break;
                case "Brazil":
                    FlagImage.overrideSprite = BrazilFlagSprite;
                    break;
                default:
                    FlagImage.overrideSprite = PirateFlagSprite;
                    break;
            }
        }
        else
        {
            // bad kids get pirate flag
            FlagImage.overrideSprite = PirateFlagSprite;
        }

        if (lastReading_nice)
        {
            display_nice.text = "Nice";
        }
        else
        {
            display_nice.text = "Naughty";
        }
    }
}
