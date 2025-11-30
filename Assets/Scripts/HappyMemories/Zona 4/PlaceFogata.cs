using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;

public class PlaceFogata : MonoBehaviour
{
    public TypeFogata.Type objectType;
    public PickFogata pick;
    public float moveSpeed = 3f;
    public Transform pos;
    [HideInInspector] public bool completed;
    public GameObject teleport;
    public SpatialQuest quest;

    private void Awake()
    {
    }
    public void TryPlaceObject()
    {
        if (pick == null)
            return;
        if(pick.currentObject == null)
            return ;

        if (pick.currentObject != null && pick.currentType == objectType)
        {
            StartCoroutine(MoveToPosition(pick.currentObject, pos));
            pick.Release();
            pos.gameObject.GetComponent<SpatialInteractable>().enabled = false;
            completed = true;
            teleport.SetActive(true);
            quest.tasks[3].CompleteTask();
        }
        else
        {
            pick.currentObject.GetComponent<TypeFogata>().Back();
            pick.Release();
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
