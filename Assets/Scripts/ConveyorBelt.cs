using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public GameObject thisBox;
    public float speed;
    public Vector3 direction;
    public List<GameObject> onBelt; // Hashset of items on the conveyor belt


    // Start is called before the first frame update
    void Start()
    {
        // GameObject thisBox = Instantiate
    }

    // Update is called once per frame
    void Update()
    {
 //       foreach(GameObject box in onBelt)
   //     {
     //       box.GetComponent<Rigidbody>().velocity = speed * direction * Time.deltaTime;
       // }


        foreach (GameObject box in onBelt)
        {
                box.GetComponent<Rigidbody>().velocity = speed * direction * Time.deltaTime;
        }
    }

    // When something collides with the belt
    private void OnCollisionEnter(Collision collision)
    {
        // tell object to figure out its world rotation
        // and set its center of mass lower

        // get the instance of the box object
        GameObject theBox = collision.gameObject;

        // call the method on the instantiated cardboard box object
        if (collision.gameObject.GetComponent("BoxCenterOfMass") != null)
        {
            // failing the LowerCenterOfMass() call, 
            // perhaps the method exited as a failure 
            // and therefore didn't add other types of objects to the conveyor.
            collision.gameObject.GetComponent<BoxCenterOfMass>().LowerCenterOfMass();
        }
        //collision.gameObject.GetComponent<BoxCenterOfMass>().LowerCenterOfMass();
        // this test works syntactically: !onBelt.Contains(collision.gameObject

        // sort of only want to add boxes to the conveyor, but why?
        // theBox.CompareTag("Boxes") && 
        if (!onBelt.Contains(collision.gameObject))
        {
            onBelt.Add(collision.gameObject);
        }
      }

    private void OnCollisionExit(Collision collision)
    {
        // get the instance of the box object
        GameObject theBox = collision.gameObject;

        // call the method on the instantiated cardboard box object
        collision.gameObject.GetComponent<BoxCenterOfMass>().ResetCenterOfMass();
        onBelt.Remove(collision.gameObject);
    }
}
