using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class OrderedSequence : MonoBehaviour
{
    [Header("Orden correcto (1-based: usa 1..N si prefieres)")]
    public List<int> correctOrder = new List<int>(); // por ejemplo: 1,5,3,4,6,2

    [Header("Renderers de los pilares (el índice corresponde al 0-based en la lista 'pillars')")]
    public List<Renderer> pillars = new List<Renderer>();

    [Header("Sonidos")]
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip failSound;

    [Header("Evento al completar")]
    public UnityEvent OnCompleted;

    private int currentIndex = 0;
    public bool isCompleted;

    public SpatialQuest quest;
    public int indexQuest;

    // internals
    private bool correctOrderNormalized = false;

    void Start()
    {
        NormalizeCorrectOrderIfNeeded();
    }

    // Normaliza la lista correctOrder de 1-based a 0-based una sola vez
    void NormalizeCorrectOrderIfNeeded()
    {
        if (correctOrder == null) return;
        if (correctOrderNormalized) return;

        for (int i = 0; i < correctOrder.Count; i++)
        {
            // si ya parece 0-based (ej. contiene un 0) asumimos que está ya normalizado
            if (correctOrder[i] == 0)
            {
                correctOrderNormalized = true;
                return;
            }
        }

        for (int i = 0; i < correctOrder.Count; i++)
        {
            correctOrder[i] = correctOrder[i] - 1; // convierte 1->0, 2->1, etc.
        }

        correctOrderNormalized = true;
    }

    // buttonIndex llega 1-based (según tu intención). Convertimos a 0-based aquí.
    public void RegisterPress(int buttonIndex)
    {
        if (!correctOrderNormalized) NormalizeCorrectOrderIfNeeded();

        int idx = buttonIndex - 1; // convertir a 0-based
        if (idx < 0 || idx >= pillars.Count)
        {
            Debug.LogWarning($"OrderedSequence: buttonIndex {buttonIndex} (convertido a {idx}) fuera de rango (pillars.Count = {pillars.Count}).");
            // reseteamos para evitar desincronía, opcional:
            // ResetSequence();
            return;
        }

        if (currentIndex < 0 || currentIndex >= correctOrder.Count) currentIndex = 0;

        // seguridad: comprobar que correctOrder[currentIndex] esté en rango
        if (correctOrder[currentIndex] < 0 || correctOrder[currentIndex] >= pillars.Count)
        {
            Debug.LogWarning($"OrderedSequence: correctOrder contiene un índice fuera de rango: {correctOrder[currentIndex]} (pillar count {pillars.Count}).");
            ResetSequence();
            return;
        }

        if (idx == correctOrder[currentIndex])
        {
            currentIndex++;
            PlayCorrectSound();

            if (currentIndex >= correctOrder.Count)
            {
                Score.Instance.AddPoints();
                OnCompleted?.Invoke();
                isCompleted = true;
                if (quest != null)
                {
                    quest.tasks[indexQuest].CompleteTask();
                }
            }
        }
        else
        {
            Score.Instance.RestarPoints();
            ResetSequence();
            PlayFailSound();
        }
    }




    public void ResetSequence()
    {
        currentIndex = 0;
    }

    void PlayCorrectSound()
    {
        if (audioSource != null && correctSound != null)
            audioSource.PlayOneShot(correctSound);
    }

    void PlayFailSound()
    {
        if (audioSource != null && failSound != null)
            audioSource.PlayOneShot(failSound);
    }
}
