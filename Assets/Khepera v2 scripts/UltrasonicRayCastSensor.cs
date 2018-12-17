using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UltrasonicRayCastSensor : MonoBehaviour
{

    private float[] distance;
    private const float MAX_LENGTH_MEASURE = 2.5f;
    private Vector3[] directions;
    private bool readyToMeasure;

    // Use this for initialization
    void Start()
    {
        distance = new float[9];
        directions = new Vector3[9];
        InitializeDirections();
        readyToMeasure = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToMeasure)
        {
            UltraSonicSwept();
        }
          
    }

    private float FilterMeasure(float[] measure)
    {
        return (measure.Min() >= MAX_LENGTH_MEASURE) ? MAX_LENGTH_MEASURE : measure.Min();
    }

    public float GetUltraMeasure()
    {
        return FilterMeasure(distance);
    }

    private void InitializeDirections()
    {
        directions[0] = new Vector3(0.05f, 0, 1);
        directions[1] = new Vector3(0.05f, 0.05f, 1);
        directions[2] = new Vector3(0, 0.05f, 1);
        directions[3] = new Vector3(-0.05f, 0.05f, 1);
        directions[4] = new Vector3(-0.05f, 0, 1);
        directions[5] = new Vector3(-0.05f, -0.05f, 1);
        directions[6] = new Vector3(0, -0.05f, 1);
        directions[7] = new Vector3(0.05f, -0.05f, 1);
        directions[8] = new Vector3(0, 0, 1);
    }

    private void UltraSonicSwept()
    {
        readyToMeasure = false;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit[] hits = new RaycastHit[directions.Length]; ;
        
        for (int i = 0; i < directions.Length; i++)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(directions[i]), out hits[i], MAX_LENGTH_MEASURE, layerMask))
            {
                distance[i] = hits[i].distance;
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hits[i].distance, Color.yellow);
            }
            else
            {
                distance[i] = MAX_LENGTH_MEASURE;
                Debug.DrawRay(transform.position, transform.TransformDirection(directions[i]) * MAX_LENGTH_MEASURE, Color.blue);
            }
        }
        StartCoroutine(EnableMeasuring());
    }

    private IEnumerator EnableMeasuring()
    {
        yield return new WaitForSecondsRealtime(2);
        readyToMeasure = true;
    }
}
