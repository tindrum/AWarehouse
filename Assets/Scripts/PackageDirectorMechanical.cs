using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageDirectorMechanical : MonoBehaviour
{
    enum PaddleArm
    {
        Near,
        Far
    }

    enum RotationDirection
    {
        Clockwise,
        CounterClockwise
    }
            
    [SerializeField] private Transform nearPaddle;
    [SerializeField] private Transform farPaddle;

    // current angles. 
    [SerializeField] private float nearSweepAngle;
    [SerializeField] private float farSweepAngle;

    // angle of max sweep of arms. 
    [SerializeField] private float nearCCWSweepMax;
    [SerializeField] private float nearCWSweepMax;
    [SerializeField] private float farCCWSweepMax;
    [SerializeField] private float farCWSweepMax;

    private float sweepSpeed;
    private float nearTargetSweepAngle;
    private float farTargetSweepAngle;


    // Start is called before the first frame update
    void Start()
    {
        nearPaddle = transform.Find("NearPaddlePivot");
        farPaddle = transform.Find("FarPaddlePivot");

        nearTargetSweepAngle = 0.0f;
        farTargetSweepAngle = 0.0f;
        sweepSpeed = 0.5f; // maximum angular speed

        // set the sweep max angles
        PaddleArm arm;
        RotationDirection rot;

        arm = PaddleArm.Near;
        rot = RotationDirection.Clockwise;
        SetSweepMax(arm, rot, 35.0f);
        rot = RotationDirection.CounterClockwise;
        SetSweepMax(arm, rot, -55.0f);
        arm = PaddleArm.Far;
        SetSweepMax(arm, rot, -35.0f);
        rot = RotationDirection.Clockwise;
        SetSweepMax(arm, rot, 55.0f);

    }

    // Update is called once per frame
    void Update()
    {
        float anglesNeeded;

        // compute the angles still needed for near paddle
        anglesNeeded = nearTargetSweepAngle - nearPaddle.rotation.eulerAngles.y;

        // compute the angles still needed for far paddle


        if (nearPaddle.rotation.eulerAngles.y < nearCCWSweepMax)
        {

        }
        if (farPaddle.rotation.eulerAngles.y < farCCWSweepMax)
        {

        }
        float nearRotateSpeed = sweepSpeed;
        float farRotateSpeed = sweepSpeed;

        
    }

    private float anglesNeeded(PaddleArm arm, float angleDesired)
    {
        float anglesNeeded;
        float limit;
        if (angleDesired < 0.0f)
        {
            if (arm == PaddleArm.Near)
            {
                limit = nearCCWSweepMax;
            }
            else
            {
                limit = farCCWSweepMax;
            }
            // if lim - des < 0, continue rotation
            anglesNeeded = limit - angleDesired;
            if (anglesNeeded < 0.0f)
            {
                if (Mathf.Abs(anglesNeeded) > sweepSpeed)
                {
                    return (-1.0f * sweepSpeed);
                }
                else
                {
                    return anglesNeeded * 0.8f;
                }
            }
            else
            {
                // else prevent rotation
                return 0.0f;
            }
        }
        else
        {
            if (arm == PaddleArm.Near)
            {
                limit = nearCWSweepMax;
            }
            else
            {
                limit = farCWSweepMax;
            }
            // if lim - des > 0, continue rotation
            anglesNeeded = limit - angleDesired;
            if (anglesNeeded >= 0.0f)
            {
                if (Mathf.Abs(anglesNeeded) > sweepSpeed)
                {
                    return (sweepSpeed);
                }
                else
                {
                    return anglesNeeded * 0.8f;
                }
            }
            else
            {
                // else prevent rotation
                return 0.0f;
            }

        }
    }

    public void SetSweepAngle(float angle)
    {
        if (angle < 0.0f) // counterclockwise, negative
        {
            if (angle >= nearCCWSweepMax)
            {
                nearTargetSweepAngle = angle;
            }
            else
            {
                nearTargetSweepAngle = nearCCWSweepMax;
            }
            if (angle >= farCCWSweepMax)
            {
                farTargetSweepAngle = angle;
            }
            else
            {
                farTargetSweepAngle = farCCWSweepMax;
            }
        }
        else // clockwise, positive
        {
            if (angle <= nearCWSweepMax)
            {
                nearTargetSweepAngle = angle;
            }
            else
            {
                nearTargetSweepAngle = nearCWSweepMax;
            }
            if (angle <= farCWSweepMax)
            {
                farTargetSweepAngle = angle;
            }
            else
            {
                farTargetSweepAngle = farCWSweepMax;
            }

        }
    }


    void SetSweepMax(PaddleArm arm, RotationDirection rotDir, float sweepAngleSetting)
    {
        if (arm == PaddleArm.Near)
        {
            if (rotDir == RotationDirection.Clockwise)
            {
                nearCWSweepMax = sweepAngleSetting;
            }
            else
            {
                nearCCWSweepMax = sweepAngleSetting;
            }
        }
        else
        {
            if (rotDir == RotationDirection.Clockwise)
            {
                farCWSweepMax = sweepAngleSetting;
            }
            else
            {
                farCCWSweepMax = sweepAngleSetting;
            }
        }
    }

  }
