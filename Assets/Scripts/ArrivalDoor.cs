using System.Collections;
using System.Collections.Generic;
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
            }
            else
            {
                Debug.Log("arrival dock has a package type already");
            }
        }
        else
        {
            return false;
        }
        return true;

    }

    public void receiveOnePackage()
    {
        if (arrivalDockExists)
        {
            connectedArrivalDock.pushPackageOut();
        }
        else
        {
            // not configured, won't work
            Debug.Log("arrival dock not configured");
            // play annoying sound
        }
         
    }
}
