using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages.Sensor;
using RosSharp.RosBridgeClient.Messages.Standard;

public class LaserKheperaPublisher : Publisher<Joy>
{
    public LaserRayCastSensor[] laserSensors;
    private string FrameId = "Unity";
    private bool readyToSend;

    private Joy message;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        InitializeMessage();
        readyToSend = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (readyToSend)
        {
            SendMessage();
        }
    }

    private void InitializeMessage()
    {
        message = new Joy
        {
            header = new Header()
            {
                frame_id = FrameId
            }
        };
        message.axes = new float[laserSensors.Length];
    }

    private void SendMessage()
    {
        readyToSend = false;
        UpdateMessage();
        // Debug.Log(message);
        Publish(message);
        StartCoroutine(EnablePublishing());
    }

    private void UpdateMessage()
    {
        message.header.Update();
        GetLaserMeasures();
    }

    private void GetLaserMeasures()
    {
        for (int i = 0; i < laserSensors.Length; i++)
        {
            message.axes.SetValue(laserSensors[i].GetLaserMeasure(), i);
        }
    }

    private IEnumerator EnablePublishing()
    {
        yield return new WaitForSecondsRealtime(2);
        readyToSend = true;
    }
}
