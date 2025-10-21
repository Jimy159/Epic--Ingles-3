using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick : MonoBehaviour
{
    public ObjectType.Type currentType;
    public GameObject currentObject;

    private void OnEnable() => Box.OnPrizeSelectedEvent += OnBoxPrize;
    private void OnDisable() => Box.OnPrizeSelectedEvent -= OnBoxPrize;

    private void OnBoxPrize(GameObject prize, ObjectType.Type type)
    {
        currentObject = prize;
        currentType = type;

        var ot = prize?.GetComponent<ObjectType>();
    }

    public void Release()
    {
        if (currentObject == null) return;
        Box.instance.ObjectCompleted();
        currentObject = null;
    }
}
