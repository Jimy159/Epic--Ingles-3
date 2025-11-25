using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTaskFragments : MonoBehaviour
{
    public List<ChooseFragment> objects = new List<ChooseFragment>();
    public static ManagerTaskFragments instance;
    public SpatialQuest quest;
    public int index = 0;
    private void Start()
    {
        instance = this;
    }
    public void AreAllComplete()
    {
        foreach (ChooseFragment obj in objects)
        {
            if (obj == null) continue;

            if (!obj.completed)
                return;
        }
        quest.tasks[index].CompleteTask();
    }
}
