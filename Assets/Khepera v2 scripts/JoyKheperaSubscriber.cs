
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages.Sensor;

public class JoyKheperaSubscriber : Subscriber<RosSharp.RosBridgeClient.Messages.Sensor.Joy>
{

    public JoyWheelController[] wheels;

    protected override void ReceiveMessage(Joy joy)
    {
        int I = wheels.Length < joy.axes.Length ? wheels.Length : joy.axes.Length;
        for (int i = 0; i < I; i++)
            if (wheels[i] != null)
                wheels[i].ControlMotor(joy.axes[i]);
    }

    protected override void Start()
    {
        base.Start();
    }
}
