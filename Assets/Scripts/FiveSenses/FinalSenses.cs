using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;
using UnityEngine.Events;

public class FinalSenses : MonoBehaviour
{
    public List<PlaceFive> objects = new List<PlaceFive>();
    public static FinalSenses instance;
    public SpatialQuest quest;
    private void Start()
    {
        instance = this;
    }
    public void AreAllComplete()
    {
        foreach (PlaceFive obj in objects)
        {
            if (obj == null) continue;


            if (!obj.completed)
                return;
        }
        quest.tasks[0].CompleteTask();
    }
}
