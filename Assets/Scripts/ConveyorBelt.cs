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
        theBox.GetComponent<BoxCenterOfMass>().LowerCenterOfMass();
        //collision.gameObject.GetComponent<BoxCenterOfMass>().LowerCenterOfMass();

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
        theBox.GetComponent<BoxCenterOfMass>().ResetCenterOfMass();
        onBelt.Remove(collision.gameObject);
    }
}
