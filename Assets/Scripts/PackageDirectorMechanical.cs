using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PackageDirectorMechanical : MonoBehaviour
{
            
    [SerializeField] private Transform nearPaddle;
    [SerializeField] private Transform farPaddle;

    // current angles. 
    [SerializeField] private float nearCurrentAngle;
    [SerializeField] private float farCurrentAngle;

    // angle of max sweep of arms. 
    [SerializeField] private float nearCCWSweepMax;
    [SerializeField] private float nearCWSweepMax;
    [SerializeField] private float farCCWSweepMax;
    [SerializeField] private float farCWSweepMax;

    private float sweepSpeed;
    private float nearCommandedAngle;
    private float farCommandedAngle;
    private float nearEnoughAngleDifference;

    public AudioSource speaker;
    public AudioClip leverSwipeUpSound;

    // debug
    public AudioClip confirmSuspicionOne;
    public AudioClip confirmSuspicionTwo;
    public AudioClip denySuspicion;

    // text debug
    public TMP_Text diagnosticOne;
    public TMP_Text diagnosticTwo;

    


    // Start is called before the first frame update
    void Start()
    {
        nearPaddle = transform.Find("NearPaddlePivot");
        farPaddle = transform.Find("FarPaddlePivot");

        nearCommandedAngle = 0.0f;
        farCommandedAngle = 0.0f;

        nearEnoughAngleDifference = 3.0f; 
        sweepSpeed = 0.5f; // maximum angular speed



    }

    // Update is called once per frame
    void Update()
    {
        if (!SweepAngleAchieved(nearPaddle, nearCommandedAngle))
        {
            diagnosticTwo.text = "m";

            RotatePaddle(nearPaddle);
        }
        else
        {
            diagnosticTwo.text = "0";
        }
 

    }

    public void RotatePaddle(Transform paddleObject)
    {
        if (paddleObject != null && paddleObject == nearPaddle)
        {
            if ((Mathf.Abs(nearCommandedAngle) - Mathf.Abs(paddleObject.transform.eulerAngles.y)) > 0.0f)
            {
                nearPaddle.rotation = Quaternion.Euler(new Vector3(0.0f, nearCommandedAngle * Time.deltaTime, 0.0f));
            }
        }

    }



    public bool SweepAngleAchieved(Transform paddleObject, float desiredAngle)
    {
        if (paddleObject == nearPaddle)
        {
            if (Mathf.Abs(paddleObject.transform.eulerAngles.y - desiredAngle) <= nearEnoughAngleDifference)
            {
                diagnosticOne.text = "T";
                return true; 
            }
            else {
                diagnosticOne.text = "F";
                return false; 
            }
        }
        if (paddleObject == farPaddle)
        {
            if (Mathf.Abs(paddleObject.transform.eulerAngles.y - desiredAngle) <= nearEnoughAngleDifference)
            { return true; }
            else { return false; }

        }
        return true;
    }

    public void CommandSweepAngle(float angle)
    {

        speaker.PlayOneShot(leverSwipeUpSound, 0.5f);
        nearCommandedAngle = angle;
        farCommandedAngle = angle;
    }


 
  }
