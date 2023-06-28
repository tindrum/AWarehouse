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
    [SerializeField] public float nearCCWSweepMax;
    [SerializeField] public float nearCWSweepMax;
    [SerializeField] public float farCCWSweepMax;
    [SerializeField] public float farCWSweepMax;

    private float degreesPerSecond;
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
    public TMP_Text labelTwo;
    public TMP_Text labelThree;
    public TMP_Text labelFour;
    public TMP_Text labelFive;
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
        degreesPerSecond = 15.0f; // maximum angular speed


    }

    // Update is called once per frame
    void Update()
    {
        diagnosticNearAngle.text = nearPaddle.transform.eulerAngles.y.ToString();
        diagnosticNearCommandedAngle.text = nearCommandedAngle.ToString();

        if (SweepAngleAchieved(nearPaddle, nearCommandedAngle))
        {
            diagnosticOne.text = "Angle OK";

        }
        else
        {
            RotatePaddle(nearPaddle);

            diagnosticOne.text = "Angle updating";
        }

    }

    public void RotatePaddle(Transform paddleObject)
    { // use Diagnostid Two, Three, Four and their labels; leave others for other status
        float spinDegrees;
 
        if (paddleObject != null && paddleObject == nearPaddle)
        {
            float angleDifference = (nearCommandedAngle - paddleObject.transform.eulerAngles.y);
            labelTwo.text = "RotPaddle: Angle difference";
            diagnosticTwo.text = angleDifference.ToString();

            if (Mathf.Abs(angleDifference) > degreesPerSecond)
            {                // just spin it at degreesPerSecond
                spinDegrees = degreesPerSecond;

            }
            else
            {
                // lower than degreesPerSecond
                spinDegrees = degreesPerSecond * 0.4f;

            }
            labelThree.text = "spinDegrees: ";
            diagnosticThree.text = spinDegrees.ToString();

            if (angleDifference >= 0.0f)
            {
                // command positive move
                nearPaddle.Rotate(new Vector3(0, spinDegrees, 0) * Time.deltaTime);

            }
            else
            {
                // command negative move
                spinDegrees *= -1.0f;
                nearPaddle.Rotate(new Vector3(0, spinDegrees, 0) * Time.deltaTime);

            }
             if (angleDifference > degreesPerSecond)
            {
                // just spin it at degreesPerSecond
                nearPaddle.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
                //nearPaddle.rotation = Quaternion.Euler(new Vector3(0.0f, nearCommandedAngle * Time.deltaTime, 0.0f));
            }
            labelFour.text = "final spinDegrees";
            diagnosticFour.text = spinDegrees.ToString();

        }

    }



    public bool SweepAngleAchieved(Transform paddleObject, float desiredAngle)
    {
        labelFive.text = "SweepAngleAchieved()";
        if (paddleObject == nearPaddle)
        {
            if (Mathf.Abs(paddleObject.transform.eulerAngles.y - desiredAngle) <= nearEnoughAngleDifference)
            {
                diagnosticFive.text = "near enough";
                return true; 
            }
            else {
                diagnosticFive.text = "not near enough";
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
