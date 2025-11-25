using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChooseFragment : MonoBehaviour
{
    public FragmentType.Type objectType;
    public PickFragment pick;
    [HideInInspector] public bool completed;
    [HideInInspector]public SpatialInteractable interactable;

    [Header("Poema")]
    [HideInInspector]public TMP_Text slotText;
    public string fragmentText;

    [Header("Sonidos")]
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip failSound;

    private void Awake()
    {
        interactable = GetComponent<SpatialInteractable>();
        slotText  = GetComponent<TMP_Text>();   
    }
    private void Start()
    {
        
        interactable.onInteractEvent.unityEvent.AddListener(TryPlaceObject);
    }

    private void Update()
    {
        if (completed)
        {
            interactable.enabled = false;
        }
    }
    public void TryPlaceObject()
    {
        if (pick == null)
            return;

        if (pick.currentObject != null && pick.currentType == objectType)
        {
            PlayCorrectSound();
            Score.Instance.AddPoints();
            pick.Release();
            slotText.text = fragmentText;
            completed = true;
            ManagerTaskFragments.instance.AreAllComplete();
            interactable.enabled = false;
        }
        if (pick.currentObject != null && pick.currentType != objectType)
        {
            PlayFailSound();
            Score.Instance.RestarPoints();
        }
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
