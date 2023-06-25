using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class ArrivalDock : MonoBehaviour
{
    public float exitTimeVariance;
    public float timerSeconds;
    private float accumulatedTime;
    public GameObject arrivalSpot;
    public List<GameObject> packagePrefabList;
    public GameObject defaultBoxType;
    //private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        if (arrivalSpot == null)
        {
            arrivalSpot = gameObject.transform.Find("Arrival_Spot").gameObject;

        }

      //  Debug.Log("******** Arrival Dock instantiated");
     //   Debug.Log(arrivalSpot.name);
//
      //  Debug.Log("       * creating empty list of package prefab types");
        // packagePrefabList = new List<GameObject>();

        if (defaultBoxType != null)
        {
            addPackagePrefab(defaultBoxType);
          //  Debug.Log("       * adding prefab to arrivalDock list");
        }

        // Set up a system timer event system
        // Probably should be more global, but for the nonce...
        //timer = new System.Timers.Timer(timerMilliseconds);
        //timer.Start();
        // timer.Elapsed += timedPackagePush;
        accumulatedTime = 0.0f;
     }

    // Update is called once per frame
    void Update()
    {
        accumulatedTime += Time.deltaTime;
        //Debug.Log(accumulatedTime);

        if (accumulatedTime > timerSeconds)
        {
            //Debug.Log("$$$PushpackageOut()");

            pushPackageOut();
            accumulatedTime -= timerSeconds;
            //Debug.Log("$$$$: " + accumulatedTime);

        }

    }

    void OnApplicationQuit()
    {
        //Debug.Log("disabling timer");
        //timer.Stop();
    }

    public void timedPackagePush(object sender, ElapsedEventArgs e)
    {
        Debug.Log("timer firing timedPackagePush");
        pushPackageOut();
    }

    public void addPackagePrefab(GameObject boxType)
    {
        // add a prefab to the types of packages this Arrival Dock will push out.
        Debug.Log("before Arrival Dock package list items: " + packagePrefabList.Count);
        packagePrefabList.Add(boxType);
        Debug.Log("after Arrival Dock package list items: " + packagePrefabList.Count);


    }

    public void removeAllPackagePrefabs()
    {
        // remove all box types from this Arrival Dock.
        // Effectively stops the machine from sending out any boxes.
    }

    public void pushPackageOut()
    {
       // Debug.Log("********  ArrivalDock.pushPackageOut() ");
       // Debug.Log("       *  ArrivalDock.pushPackageOut() called");

        // trigger the generation of one package
        if (packagePrefabList != null && packagePrefabList.Count > 0)
        {
            // Debug.Log("       * Instantiate a package from packagePrefabList");
            int itemIndex = Random.Range(0, packagePrefabList.Count);
            GameObject boxPrefab = packagePrefabList[itemIndex];
            GameObject box = Instantiate(boxPrefab, arrivalSpot.transform.position, Quaternion.identity);
            box.transform.rotation = Random.rotation;
        }

    }

}
