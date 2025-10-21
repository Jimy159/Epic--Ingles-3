using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothRotateZ : MonoBehaviour
{
    [SerializeField] private Vector3 rotationAxis = Vector3.forward; // Eje de rotación (por defecto Z)
    [SerializeField] private float rotationSpeed = 50f;              // Velocidad de rotación en grados por segundo

    void Update()
    {
        // Rotar suavemente sobre su propio eje
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
