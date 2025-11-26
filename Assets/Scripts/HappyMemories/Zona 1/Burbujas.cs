using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Burbujas : MonoBehaviour
{
    public Canvas canvas;
    public string text;
    public bool correct;
    [HideInInspector]public ManagerBurbujas manager;

    public void Question()
    {
        if(correct)
            manager.correct.Invoke();
        else
            manager.incorrect.Invoke();
    }

    private void OnEnable()
    {
        canvas.GetComponentInChildren<TextMeshProUGUI>().text = text;
    }
}
