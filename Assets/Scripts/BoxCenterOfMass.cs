using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCenterOfMass : MonoBehaviour
{
    private Vector3 originalCenterOfMass;
    public Rigidbody rb;
    private Vector3 tempCenterOfMass;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LowerCenterOfMass()
    {
        // make a fake center of mass for testing
        tempCenterOfMass = new Vector3(0.0f, -0.1f, 0.0f);

        // get the transform rotation of this box
        Quaternion boxRotation = gameObject.transform.rotation;

        Debug.Log("Quaternion of angle of this box: " + boxRotation);
        Debug.Log("this object needs to have its center of mass lowered");
        // rb = GetComponent<Rigidbody>();
        originalCenterOfMass = rb.centerOfMass;
        Debug.Log("saving original CoM");
        Debug.Log(rb.centerOfMass);
        Debug.Log("Setting to new CoM: " + tempCenterOfMass.x);
        Debug.Log(tempCenterOfMass.y);
        Debug.Log(tempCenterOfMass.z);
        rb.centerOfMass = tempCenterOfMass;

    }

    public void ResetCenterOfMass()
    {
        Debug.Log("************************************");
        Debug.Log("Center of mass reset to default");
        // rb = GetComponent<Rigidbody>();
        Vector3 originalCoM = new Vector3(0.0f, 0.0f, 0.0f);

        rb.centerOfMass = originalCoM;
        Debug.Log(rb.centerOfMass);
 //       Debug.Log(rb.CenterOfMass.x);
   //     Debug.Log(rb.CenterOfMass.y);
     //   Debug.Log(rb.CenterOfMass.z);
        Debug.Log("************************************");

    }
}
