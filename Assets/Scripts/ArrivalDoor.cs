using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ArrivalDoor : MonoBehaviour
{
    public GameObject arrivalDoor;
    public ArrivalDock connectedArrivalDock;


    // for now, have a prefab box type that will arrive
    public GameObject packageType;
    private bool arrivalDockExists;


    // Start is called before the first frame update
    void Start()
    {
        arrivalDockExists = setDockPackageType();


        Debug.Log("Scheduled for 10 seconds: receiveOnePackage()");
        Invoke("receiveOnePackage", 10.0f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool setDockPackageType()
    {
        // internal use
        if (packageType != null && connectedArrivalDock != null)
        {
            if (connectedArrivalDock.packagePrefabList.Count == 0)
            {
                connectedArrivalDock.addPackagePrefab(packageType);
                Debug.Log("#### setting package type of connected Arrival Dock");

            }
            else
            {
                Debug.Log("#### arrival dock has a package type already");
                connectedArrivalDock.addPackagePrefab(packageType);
                // add one for now, to see if it can has two types
            }
        }
        else
        {
            Debug.Log("#### FAIL. couldn't set package type");

            return false;
        }
        return true;

    }

    // [MenuItem("Receive/One Package", false, 10)]
    static public void buttonPressReceived()
    {
        // Must be a static method, 
        // so can't trigger methods of an instance of this object type.
        // Not useful at this time.
        Debug.Log("#### static method triggered from button press.");
    }

    public void receiveOnePackage()
    {
        if (arrivalDockExists)
        {
            connectedArrivalDock.pushPackageOut();
            Debug.Log("#### calling ArrivalDock.pushPackageOut()");
        }
        else
        {
            // not configured, won't work
            Debug.Log("#### arrival dock not configured");
            // play annoying sound
        }
         
    }
}
