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
    public TMP_Text diagnosticThree;
    public TMP_Text diagnosticFour;
    public TMP_Text diagnosticFive;
    public TMP_Text diagnosticNearAngle;
    public TMP_Text diagnosticNearCommandedAngle;




    // Start is called before the first frame update
    void Start()
    {
        //nearPaddle = transform.Find("NearPaddlePivot");
        //farPaddle = transform.Find("FarPaddlePivot");

        nearCommandedAngle = 0.0f;
        farCommandedAngle = 0.0f;

        nearEnoughAngleDifference = 3.0f; 
        sweepSpeed = 0.5f; // maximum angular speed

        diagnosticOne.text = "Start()";

    }

    // Update is called once per frame
    void Update()
    {
        diagnosticOne.text = "Update()";
        diagnosticNearAngle.text = nearPaddle.transform.eulerAngles.y.ToString();
        diagnosticNearCommandedAngle.text = nearCommandedAngle.ToString();

        if (!SweepAngleAchieved(nearPaddle, nearCommandedAngle))
        {
            diagnosticOne.text = "Update() !SAA true";

            RotatePaddle(nearPaddle);
        }
        else
        {
            diagnosticOne.text = "Update() !SAA false";
        }

    }

    public void RotatePaddle(Transform paddleObject)
    {
        diagnosticFive.text = "RotatePaddle()";

        if (paddleObject != null && paddleObject == nearPaddle)
        {
            diagnosticThree.text = "RP notNull & nearPaddle";
            float angleDifference = (Mathf.Abs(nearCommandedAngle) - Mathf.Abs(paddleObject.transform.eulerAngles.y));
            diagnosticFour.text = angleDifference.ToString();
            if (angleDifference > 0.0f)
            {
                diagnosticThree.text = "RP rotate";
                diagnosticTwo.text = nearCommandedAngle.ToString();

                nearPaddle.rotation = Quaternion.Euler(new Vector3(0.0f, nearCommandedAngle * Time.deltaTime, 0.0f));
            }
        }

    }



    public bool SweepAngleAchieved(Transform paddleObject, float desiredAngle)
    {
        diagnosticFive.text = "SweepAngleAchieved()";
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

        // diagnosticThree.text = angle.ToString();
        speaker.PlayOneShot(leverSwipeUpSound, 0.5f);
        nearCommandedAngle = angle;
        farCommandedAngle = angle;
    }


 
  }
