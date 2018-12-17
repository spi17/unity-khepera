using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JoyWheelController : MonoBehaviour
{
    public WheelCollider wheel;

    private static float maxMotorTorque = 25;

    private float expectedRPMSpeed;

    private static float RADIUS = 0.021f;

    public void Start()
    {
        wheel.ConfigureVehicleSubsteps(5, 12, 15);
        wheel.motorTorque = 0;
        expectedRPMSpeed = 0;
    }

    public void ControlMotor(float expectedLinearSpeed)
    {
        //Debug.Log("Input: " + expectedLinearSpeed);
        expectedRPMSpeed = (expectedLinearSpeed * 60) / (2 * RADIUS * Mathf.PI);
        //Debug.Log("Expected: " + expectedRPMSpeed);
    }

    public void FixedUpdate()
    {

        if (expectedRPMSpeed == 0)
        {
            wheel.motorTorque = 0;
            //Debug.Log("Stopped");
        }
        else if (expectedRPMSpeed > 0)
        {
            if (wheel.rpm > expectedRPMSpeed + 0.2)
            {
                //Debug.Log("Braking > 0");
                wheel.motorTorque = wheel.motorTorque >= 0 ? wheel.motorTorque - 0.1f : 0;
            }
            else if (wheel.rpm < expectedRPMSpeed - 0.2)
            {
                //Debug.Log("Accelerating > 0");
                wheel.motorTorque = wheel.motorTorque < maxMotorTorque ? wheel.motorTorque + 0.1f : maxMotorTorque;
            }
        }
        else
        {
            if (wheel.rpm < expectedRPMSpeed - 0.2)
            {
                //Debug.Log("Braking < 0");
                wheel.motorTorque = wheel.motorTorque <= 0 ? wheel.motorTorque + 0.1f : 0;
            }
            else if (wheel.rpm > expectedRPMSpeed + 0.2)
            {
                //Debug.Log("Accelerating < 0");
                wheel.motorTorque = wheel.motorTorque <= -1 * maxMotorTorque ? -1 * maxMotorTorque : wheel.motorTorque - 0.1f;
            }
        }

        //Debug.Log("Actual: " + wheel.rpm);
        //Debug.Log("Expected: " + expectedRPMSpeed);

        //Debug.Log("Motor: " + wheel.motorTorque);
    }
}