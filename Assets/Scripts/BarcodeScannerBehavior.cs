using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarcodeScannerBehavior : MonoBehaviour
{
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float scannerDistanceRange = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BarcodeScan()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, scannerDistanceRange))
        {
            if (hit.transform.CompareTag("AddressLabel"))
            {
                // get data from box we scanned
                Component[] components = hit.transform.gameObject.GetComponents(typeof(Component));
                foreach (Component component in components)
                {
                   // Debug.Log(component.ToString);
                    Debug.Log(component.name);
                }
                hit.transform.gameObject.SetActive(false);
            }
        }

    }
}
