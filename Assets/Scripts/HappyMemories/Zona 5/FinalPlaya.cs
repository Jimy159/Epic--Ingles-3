using System.Collections;
using System.Collections.Generic;
using SpatialSys.UnitySDK;
using UnityEngine;
using UnityEngine.Events;

public class FinalPlaya : MonoBehaviour
{
    public List<PlacePalabra> objects = new List<PlacePalabra>();
    public static FinalPlaya instance;
    public UnityEvent complete;
    public SpatialQuest quest;
    private void Start()
    {
        instance = this;
    }
    public void AreAllComplete()
    {
        foreach (PlacePalabra obj in objects)
        {
            if (obj == null) continue;


            if (!obj.completed)
                return;
        }
        complete.Invoke();
        quest.tasks[4].CompleteTask();
    }
}
