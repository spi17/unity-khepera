
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages.Sensor;
using UnityEngine;

public class JoyKheperaSubscriber : Subscriber<RosSharp.RosBridgeClient.Messages.Sensor.Joy>
{

    public JoyWheelController[] wheels;

    protected override void ReceiveMessage(Joy joy)
    {
        int I = wheels.Length < joy.axes.Length ? wheels.Length : joy.axes.Length;
        for (int i = 0; i < I; i++)
        {
            if (wheels[i] != null)
            {
                // Debug.Log("Incoming wheel command: " + i + "; " + joy.axes[i]);
                wheels[i].ControlMotor(joy.axes[i]);

            }
        }
            
    }

    protected override void Start()
    {
        base.Start();
    }
}
