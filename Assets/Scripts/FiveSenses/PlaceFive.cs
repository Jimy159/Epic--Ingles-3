using System;
using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;
using UnityEngine.UIElements;

public class PlaceFive : MonoBehaviour
{
    public TypeFive.Type objectType;
    public PickFive pick;
    public float moveSpeed = 3f;
    [HideInInspector] public bool completed;
    public List<TransformList> positions = new List<TransformList>();
    public SpatialInteractable interactable;
    public GameObject bien;
    public GameObject mal;
    public GameObject word;
    public List<GameObject> finish = new List<GameObject>();
    public void TryPlaceObject()
    {
        if (pick == null)
            return;
        if(pick.currentObject == null) return;

        if (pick.currentObject != null && pick.currentType == objectType)
        {
            StartCoroutine(MoveToPosition(pick.currentObject));
            pick.Release();
        }
        else if (pick.currentObject != null)
        {
            bien.SetActive(false);
            mal.SetActive(false);
            mal.SetActive(true);
            pick.typeFive.Back();
            pick.Release();
        }
    }
    public void TransformOccuped()
    {
        bien.SetActive(false);
        mal.SetActive(false);
        bien.SetActive(true);
        bool todosOcupados = true;
        for (int i = 0; i < positions.Count; i++)
        {
            if (!positions[i].occuped)   // Si uno NO está ocupado
            {
                todosOcupados = false;
                break;                   // Ya no hace falta seguir revisando
            }
        }

        if (todosOcupados)
        {
            completed = true;
            gameObject.SetActive(false); word.SetActive(false);
            FinalSenses.instance.AreAllComplete();
            for (int i = 0; i < finish.Count; i++)
            {
                finish[i].gameObject.SetActive(true);
            }
            for (int i = 0; i < positions.Count; i++)
            {
                positions[i].transform.gameObject.SetActive(false);
            }
            bien.SetActive(false);
            mal.SetActive(false);
        }
    }
    private IEnumerator MoveToPosition(GameObject obj)
    {
        Transform target = transform;

        for (int i = 0; 0 < positions.Count; i++)
        {
            if (!positions[i].occuped)
            {
                target = positions[i].transform;
                positions[i].occuped = true;
                break;
            }
        }

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
        TransformOccuped();
    }
}
[Serializable]
public class TransformList
{
    public Transform transform;
    public bool occuped;
}

