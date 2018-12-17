using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltrasonicSensorController : MonoBehaviour
{

    public GameObject referencia;

    private float distance;

    private Vector3 distanceVector;

    private int nearestColliderId;

    // Use this for initialization
    void Start()
    {
        distance = 999;
        nearestColliderId = -1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (CalculateDistance(other.transform.position) < distance)
        {
            Debug.Log("Nuevo objeto cercano detectado con id: " + other.GetInstanceID());
            nearestColliderId = other.GetInstanceID();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (nearestColliderId == other.GetInstanceID())
        {
            distance = CalculateDistance(other.transform.position);
            Debug.Log("Distancia relativa al objeto más cercano: " + distance);
        }
        else if (CalculateDistance(other.transform.position) < distance)
        {
            Debug.Log("Nuevo objeto más cercano detectado con id: " + other.GetInstanceID());
            distance = CalculateDistance(other.transform.position);
            nearestColliderId = other.GetInstanceID();
            Debug.Log("Distancia relativa al objeto más cercano: " + distance);
        }
        else
        {
            Debug.Log("Ignorar ecos lejanos");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (nearestColliderId == other.GetInstanceID())
        {
            Debug.Log("Objeto cercano saliendo de rango con id " + other.GetInstanceID());
            nearestColliderId = -1;
            distance = 999;
        }
    }

    private float CalculateDistance(Vector3 objectDetectedPosition)
    {
        Vector3 newObjectDetected = new Vector3(objectDetectedPosition.x - referencia.transform.position.x, objectDetectedPosition.y - referencia.transform.position.y, objectDetectedPosition.z - referencia.transform.position.z);
        return newObjectDetected.sqrMagnitude;
    }

    private float FilterMeasure(float measure)
    {
        return (measure == 999 || measure > 1) ? 0 : measure;
    }
}
