using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFinalBooks : MonoBehaviour
{
    public List<PlaceBook> objects = new List<PlaceBook>();
    public static ActivateFinalBooks instance;
    public SpatialQuest quest;
    public int index = 0;
    private void Start()
    {
        instance = this;
    }
    public void AreAllComplete()
    {
        foreach (PlaceBook obj in objects)
        {
            if (obj == null) continue;

            if (!obj.completed)
                return;
        }
        quest.tasks[index].CompleteTask();
    }
}
