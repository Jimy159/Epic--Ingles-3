using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance;

    public int actualScore = 0;

    public TMP_Text scoreText;


    [Header("Life")]
    public int lifePoints = 10;
    public GameObject canvasGameOver;
    public GameObject objectAll;

    public List<GameObject> hearth = new List<GameObject>();
    
    void Start()
    {
        Instance = this;
        scoreText.text = "0";
    }

    private void Update()
    {
        if(lifePoints<=0)
        {
            canvasGameOver.SetActive(true);
            objectAll.SetActive(false);
        }
    }
    public void AddPoints()
    {
        actualScore += 100;
        UpdateUiText();
    }

    public void RestarPoints()
    {
        actualScore -= 20;
        lifePoints -= 1;
        RemoveLastHeart();
        UpdateUiText();
    }

    public void UpdateUiText()
    {
        scoreText.text = actualScore.ToString();
    }

    private void RemoveLastHeart()
    {
        if (hearth.Count == 0) return;

        // Obtener el último corazón
        GameObject lastHeart = hearth[hearth.Count - 1];

        // Desaparecerlo
        lastHeart.SetActive(false);

        // Opcional: quitarlo de la lista
        hearth.RemoveAt(hearth.Count - 1);
    }
}
