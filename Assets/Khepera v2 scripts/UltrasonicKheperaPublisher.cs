using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Messages.Sensor;
using RosSharp.RosBridgeClient.Messages.Standard;

public class UltrasonicKheperaPublisher : Publisher<Joy>
{

    public UltrasonicRayCastSensor[] ultraSensors;
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
        message.axes = new float[ultraSensors.Length];
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
        for (int i = 0; i < ultraSensors.Length; i++)
        {
            message.axes.SetValue(ultraSensors[i].GetUltraMeasure(), i);
        }
    }

    private IEnumerator EnablePublishing()
    {
        yield return new WaitForSecondsRealtime(4);
        readyToSend = true;
    }
}
