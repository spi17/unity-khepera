using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class KheperaWheels
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
}

public class KheperaController : MonoBehaviour
{
    public KheperaWheels wheels;

    public float maxMotorTorque;
    public float maxSteeringAngle;

    public void Start()
    {
        wheels.leftWheel.ConfigureVehicleSubsteps(5,12,15);
        wheels.rightWheel.ConfigureVehicleSubsteps(5, 12, 15);
    }

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        //visualWheel.transform.rotation = rotation;
        //visualWheel.transform.rotation = new Quaternion(rotation.x, rotation.x, rotation.z, rotation.w);
    }

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");

        
            if (wheels.motor)
            {
            wheels.leftWheel.motorTorque = motor;
            wheels.rightWheel.motorTorque = motor;
            }
            
            //ApplyLocalPositionToVisuals(wheels.leftWheel);
            //ApplyLocalPositionToVisuals(wheels.rightWheel);
        
    }
}