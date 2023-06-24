using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivalDock : MonoBehaviour
{
    public float exitTimeVariance;
    public GameObject arrivalSpot;
    public List<GameObject> packagePrefabList;
    public GameObject defaultBoxType;

    // Start is called before the first frame update
    void Start()
    {
        if (arrivalSpot != null)
        {
            Debug.Log("       * Arrival Spot set beforehand. OK");
        }
        else
        {
            Debug.Log("       * finding default arrival spot object");
            // transform.Find() only finds children of gameObject.
            // Don't nest objects too deep if you hope to find them.
            arrivalSpot = gameObject.transform.Find("Arrival_Spot").gameObject;

        }

        Debug.Log("******** Arrival Dock instantiated");
        Debug.Log(arrivalSpot.name);

        Debug.Log("       * creating empty list of package prefab types");
        packagePrefabList = new List<GameObject>();

        if (defaultBoxType != null)
        {
            addPackagePrefab(defaultBoxType);
            Debug.Log("       * adding prefab to arrivalDock list");
        }

     }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addPackagePrefab(GameObject boxType)
    {
        // add a prefab to the types of packages this Arrival Dock will push out.
        packagePrefabList.Add(boxType);

    }

    public void removeAllPackagePrefabs()
    {
        // remove all box types from this Arrival Dock.
        // Effectively stops the machine from sending out any boxes.
    }

    public void pushPackageOut()
    {
        Debug.Log("********  ArrivalDock.pushPackageOut() ");
        Debug.Log("       *  ArrivalDock.pushPackageOut() called");

        // trigger the generation of one package
        if (packagePrefabList != null && packagePrefabList.Count > 0)
        {
            Debug.Log("       * Instantiate a package from packagePrefabList");
            GameObject boxPrefab = packagePrefabList[Random.Range(0, packagePrefabList.Count)];
            GameObject box = Instantiate(boxPrefab, arrivalSpot.transform.position, Quaternion.identity);
            box.transform.rotation = Random.rotation;
        }

    }

}
