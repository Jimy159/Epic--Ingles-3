using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFinal : MonoBehaviour
{
    public List<Choose> objects = new List<Choose>();
    public static ActivateFinal instance;
    public SpatialQuest quest;

    private void Start()
    {
        instance = this;
    }
    public void AreAllComplete()
    {
        foreach (Choose obj in objects)
        {
            if (obj == null) continue;

            if (!obj.completed)
                return;
        }

        quest.CompleteQuest();
    }
}
