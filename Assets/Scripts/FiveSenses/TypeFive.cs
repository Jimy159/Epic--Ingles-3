using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

public class TypeFive : MonoBehaviour
{
    public enum Type
    {
        Gusto,
        Olfato,
        Vista,
        Oido,
        Tacto
    }

    public Type type;
    public SpatialInteractable interactable;

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    public void Back()
    {
        transform.localPosition = initialPosition;
    }
}
