using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentType : MonoBehaviour
{
    public enum Type
    {
        a,
        b,
        c,
        d,
        e,
        f,
        g,
        h,
        i,
        j,
        k,
        l,
        m,
        n,
        o,
    }
    public Type type;
    public SpatialInteractable interactable;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    public SpatialPointOfInterest pointOfInterest;

    private void Awake()
    {
        interactable = GetComponent<SpatialInteractable>();
        pointOfInterest = GetComponent<SpatialPointOfInterest>();
    }
    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void ResetTransform()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}
