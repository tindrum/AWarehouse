using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public GameObject thisBox;
    public float speed;
    public Vector3 direction;
    public List<GameObject> onBelt; // List of items on the conveyor belt
    public TMP_Text boxCountText;


     [SerializeField] public GameObject testBeeper;
    public AudioClip soundAdded;
    public AudioClip soundRemoved;
    public AudioClip hasCoMsoundAdded;
    public AudioClip hasCoMsoundRemoved;

    // belt material animation
    public float directionVector1;
    public float directionVector2;
    private Material material;
    public float materialAnimationSpeed;



    // Start is called before the first frame update
    void Start()
    {
        // GameObject thisBox = Instantiate
        // belt material animation
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject box in onBelt)
        {
                box.GetComponent<Rigidbody>().velocity = speed * direction * Time.deltaTime;
        }
        // belt material animation
        GetComponent<MeshRenderer>().material.mainTextureOffset += new Vector2(1, 0) * materialAnimationSpeed * Time.deltaTime;
    }

    // When something collides with the belt
    private void OnCollisionEnter(Collision collision)
    {
        // tell object to figure out its world rotation
        // and set its center of mass lower

        // call the method on the instantiated cardboard box object
        if (collision.gameObject.GetComponent("BoxCenterOfMass") != null)
        {
            // failing the LowerCenterOfMass() call, 
            // perhaps the method exited as a failure 
            // and therefore didn't add other types of objects to the conveyor.
            if (testBeeper != null)
            {
                testBeeper.GetComponent<AudioSource>().PlayOneShot(hasCoMsoundAdded, 0.3f);
            }
            // collision.gameObject.GetComponent<BoxCenterOfMass>().LowerCenterOfMass();
        }
        //collision.gameObject.GetComponent<BoxCenterOfMass>().LowerCenterOfMass();
        // this test works syntactically: !onBelt.Contains(collision.gameObject

        // sort of only want to add boxes to the conveyor, but why?
        // theBox.CompareTag("Boxes") && 
        if (!onBelt.Contains(collision.gameObject))
        {
            if (testBeeper != null)
            {
                testBeeper.GetComponent<AudioSource>().PlayOneShot(soundAdded, 0.3f);
            }

            onBelt.Add(collision.gameObject);
        }
        if (boxCountText != null)
        {
            DisplayBoxCount();
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        // call the method on the instantiated cardboard box object
        if (collision.gameObject.GetComponent("BoxCenterOfMass") != null)
        {
            if (testBeeper != null)
            {
                testBeeper.GetComponent<AudioSource>().PlayOneShot(hasCoMsoundRemoved, 0.4f);
            }

            // collision.gameObject.GetComponent<BoxCenterOfMass>().ResetCenterOfMass();
        }
        if (testBeeper != null)
        {
            testBeeper.GetComponent<AudioSource>().PlayOneShot(soundRemoved, 0.3f);
        }

        onBelt.Remove(collision.gameObject);
        if (boxCountText != null)
        {
            DisplayBoxCount();
        }
    }

    public void DisplayBoxCount()
    {
        boxCountText.text = onBelt.Count.ToString();
    }
}
