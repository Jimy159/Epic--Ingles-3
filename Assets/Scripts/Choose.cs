using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choose : MonoBehaviour
{
    public ObjectType.Type ChooseType;
    public Pick pick;
    [HideInInspector] public bool completed;
    private SpatialInteractable interactable;
    private Renderer targetRenderer;

    private void Awake()
    {
        interactable = GetComponent<SpatialInteractable>();
        targetRenderer = GetComponent<Renderer>();
    }
    public void TryPlaceObject()
    {
        if (pick == null)
            return;

        if (pick.currentObject != null && pick.currentType == ChooseType)
        {
            pick.Release();
            completed = true;
            Material mat = targetRenderer.material;
            mat.EnableKeyword("_EMISSION");
            ActivateFinal.instance.AreAllComplete();
            interactable.enabled = false;
        }
    }
}
