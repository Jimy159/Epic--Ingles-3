using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

public class PlacePalabra : MonoBehaviour
{
    public TypePalabra.Type objectType;
    public PickPlaya pick;
    public float moveSpeed = 3f;
    public Transform pos;
    [HideInInspector] public bool completed;
    public FinalPlaya FinalPlaya;
    public SpatialInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<SpatialInteractable>();
    }
    public void TryPlaceObject()
    {
        if (pick == null)
            return;

        if (pick.currentObject != null && pick.currentType == objectType)
        {
            StartCoroutine(MoveToPosition(pick.currentObject, pos));
                pick.Release();
                completed = true;
            if (FinalPlaya != null)
                FinalPlaya.AreAllComplete();
            pos.gameObject.GetComponent<SpatialInteractable>().enabled = false;
        }
        else
        {
            pick.currentObject.GetComponent<TypePalabra>().Back();
            pick.Release();
            pick.interactable.enabled = true;
        }
    }

    private IEnumerator MoveToPosition(GameObject obj, Transform target)
    {
        while (obj != null && Vector3.Distance(obj.transform.position, target.position) > 0.05f)
        {
            obj.transform.position = Vector3.Lerp(
                obj.transform.position,
                target.position,
                Time.deltaTime * moveSpeed
            );

            yield return null;
        }

        if (obj != null)
        {
            obj.transform.position = target.position;
            obj.transform.SetParent(target);
            obj.transform.rotation = Quaternion.LookRotation(-target.forward, Vector3.up);
        }
    }
}
