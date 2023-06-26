using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicConveyorBelt : MonoBehaviour
{

    [SerializeField]
    private float speed, conveyorSpeed;
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private List<GameObject> onBelt;

    // instance the material so it can have its own "speed" effect
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<MeshRenderer>().material.mainTextureOffset += new Vector2(0, 1) * conveyorSpeed * Time.deltaTime;
    }

    // FixedUpdate is called periodically
    void FixedUpdate()
    {
        for (int i = 0; i <= onBelt.Count - 1; i++)
        {
            onBelt[i].GetComponent<Rigidbody>().AddForce(speed * direction);
        }
    }

    // When something collides with the belt
    private void OnCollisionEnter(Collision collision)
    {
        // check if object is already in onBelt list
        if (!onBelt.Contains(collision.gameObject))
        {
            onBelt.Add(collision.gameObject);
        }
    }

    // When something leaves the belt
    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
        // add garbage collection: iterate through onBelt,
        // remove any gameObjects that have x and z out of bounds.
    }

}
