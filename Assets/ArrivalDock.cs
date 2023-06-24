using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivalDock : MonoBehaviour
{
    public float exitTimeVariance;
    public GameObject arrivalSpot;
    private GameObject[] packagePrefabList;

    // Start is called before the first frame update
    void Start()
    {
        if (arrivalSpot != null)
        {
            Debug.Log("Arrival Spot set beforehand. OK");
        }
        else
        {
            Debug.Log("finding default arrival spot object");
            // transform.Find() only finds children of gameObject.
            // Don't nest objects too deep if you hope to find them.
            arrivalSpot = gameObject.transform.Find("Arrival_Spot").gameObject;

        }

        Debug.Log("*** Arrival Dock instantiated");
        Debug.Log(arrivalSpot.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addPackagePrefab()
    {
        // add a prefab to the types of packages this Arrival Dock will push out.

    }

    public void removeAllPackagePrefabs()
    {
        // remove all box types from this Arrival Dock.
        // Effectively stops the machine from sending out any boxes.
    }

    public void pushPackageOut()
    {
        // trigger the generation of one package
        int packageSizeChoices = packagePrefabList.Length;
        if (packagePrefabList != null && packageSizeChoices > 0)
        {
            Debug.Log("Instantiate a package from packagePrefabList");
        }

    }

}
