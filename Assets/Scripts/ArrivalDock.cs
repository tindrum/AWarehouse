using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine.Events;
using UnityEngine;

public class ArrivalDock : MonoBehaviour
{
    public float speedIncreaseMultiplier; // make slightly less than 1.0f
    public float timerSeconds;
    private float accumulatedTime;
    public GameObject arrivalSpot;
    public List<GameObject> packagePrefabList;
    public GameObject defaultBoxType;
    public bool randomRotation;
    //private Timer timer;

    // Create a Unity Event 
    public UnityEvent m_pushPackageOut = new UnityEvent();

    // Am I assigning the method on the left to the UnityEvent variable on the right?
    // public UnityEvent m_pushPackageOut => pushPackageOut;


    // Start is called before the first frame update
    void Start()
    {
        if (arrivalSpot == null)
        {
            arrivalSpot = gameObject.transform.Find("Arrival_Spot").gameObject;

        }


        if (defaultBoxType != null)
        {
            addPackagePrefab(defaultBoxType);
        }

        // Set up a system timer event system
        // Probably should be more global, but for the nonce...
        //timer = new System.Timers.Timer(timerMilliseconds);
        //timer.Start();
        // timer.Elapsed += timedPackagePush;
        accumulatedTime = 0.0f;
        m_pushPackageOut.AddListener(pushPackageOut);
     }

    // Update is called once per frame
    void Update()
    {
        accumulatedTime += Time.deltaTime;
        //Debug.Log(accumulatedTime);

        if (accumulatedTime > timerSeconds)
        {
            //Debug.Log("$$$PushpackageOut()");
            //
            // Randomly rotate packages
            if (randomRotation)
            {
                arrivalSpot.transform.rotation = Random.rotation;
            }
            m_pushPackageOut.Invoke();
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
        m_pushPackageOut.Invoke();
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
        // trigger the generation of one package
        if (packagePrefabList != null && packagePrefabList.Count > 0)
        {
            int itemIndex = Random.Range(0, packagePrefabList.Count);
            GameObject boxPrefab = packagePrefabList[itemIndex];
            // Would like boxes not to come out in a uniform rotation, but need to not change their "internal" north, 
            // just their facing in the world.
            GameObject box = Instantiate(boxPrefab, arrivalSpot.transform.position, Quaternion.identity);
        }
    }

    public void increaseArrivalSpeed()
    {
        timerSeconds *= speedIncreaseMultiplier;
    }

}
