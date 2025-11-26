using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ManagerBurbujas : MonoBehaviour
{
    public List<Burbujas> burbujas = new List<Burbujas>();
    public UnityEvent correct;
    public UnityEvent incorrect;

    private void Start()
    {
        for (int i = 0; i < burbujas.Count; i++)
        {
            burbujas[i].manager = this;
            burbujas[i].gameObject.SetActive(false);
        }
    }

    public void InitMission()
    {
        for(int i = 0;i < burbujas.Count;i++)
        {
            burbujas[i].gameObject.SetActive(true);
        }
    }
}
