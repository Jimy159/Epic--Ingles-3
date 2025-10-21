using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] private List<GameObject> items = new List<GameObject>();
    public int currentIndexObject = -1;
    [SerializeField] private bool autoPopulateFromChildren = true;

    [Header("Cycle settings")]
    [SerializeField] private float cycleDuration = 2.0f; // tiempo total del 'giro' en segundos
    [SerializeField] private float startDelay = 0.05f;   // tiempo entre cambios al inicio
    [SerializeField] private float endDelay = 0.25f;     // tiempo entre cambios al final (se hace más lento)
    [SerializeField] private AnimationCurve speedCurve = null; // opcional para controlar la velocidad

    [Header("Movement while animating")]
    [SerializeField] private float raiseAmount = 1.0f;           // cuánto sube durante la animación
    [SerializeField] private bool useLocalPosition = true;       // usar localPosition o world position
    [SerializeField] private bool keepRaisedAfterCycle = true;   // si se queda elevado al terminar
    [SerializeField] private AnimationCurve moveCurve = null;    // curva opcional para la subida

    private Vector3 originalPosition;
    private Coroutine cycleCoroutine = null;
    public GameObject rawImage;
    public GameObject interactable;

    public static event System.Action<GameObject, ObjectType.Type> OnPrizeSelectedEvent;

    public static Box instance;
    private void Awake()
    {
        instance = this;
        if (items.Count == 0 && autoPopulateFromChildren)
        {
            foreach (Transform t in transform) items.Add(t.gameObject);
        }

        originalPosition = useLocalPosition ? transform.localPosition : transform.position;

        Desactivate();
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //OpenBox();
        }
    }
    public void OpenBox()
    {
        Desactivate();
        if (items == null || items.Count == 0) return;

        // Si ya hay una rutina corriendo, detenerla y reiniciar posición
        if (cycleCoroutine != null)
        {
            StopCoroutine(cycleCoroutine);
            cycleCoroutine = null;
            ResetPosition();
        }

        cycleCoroutine = StartCoroutine(CycleAndSelect());
    }

    private IEnumerator CycleAndSelect()
    {
        float elapsed = 0f;
        float t = 0f;
        float lastChange = 0f;
        float delay = startDelay;
        int currentIndex = -1;

        Vector3 targetUp = originalPosition + Vector3.up * raiseAmount;

        while (elapsed < cycleDuration)
        {
            t = elapsed / cycleDuration; // 0..1
            // si hay speedCurve, la usamos para interpolar delay entre startDelay y endDelay
            if (speedCurve != null)
                delay = Mathf.Lerp(startDelay, endDelay, speedCurve.Evaluate(t));
            else
                delay = Mathf.Lerp(startDelay, endDelay, t);

            if (Time.time - lastChange >= delay)
            {
                // activar un elemento aleatorio (o podrías recorrer secuencialmente)
                int next = Random.Range(0, items.Count);

                // evitar repetir el mismo inmediatamente (opcional)
                if (items.Count > 1)
                {
                    int tries = 0;
                    while (next == currentIndex && tries < 5)
                    {
                        next = Random.Range(0, items.Count);
                        tries++;
                    }
                }

                // activar next y desactivar el anterior
                if (currentIndex >= 0 && currentIndex < items.Count && items[currentIndex] != null)
                    items[currentIndex].SetActive(false);

                if (items[next] != null)
                    items[next].SetActive(true);

                currentIndex = next;
                lastChange = Time.time;
            }

            // Movimiento vertical interpolado según moveCurve (si existe) o lineal
            float moveT = Mathf.Clamp01(elapsed / cycleDuration);
            float curveT = moveCurve != null ? moveCurve.Evaluate(moveT) : moveT;
            Vector3 newPos = Vector3.Lerp(originalPosition, targetUp, curveT);
            SetPosition(newPos);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Al final, elegir el premio definitivo (puede ser el currentIndex o uno nuevo)
        int finalIndex = Random.Range(0, items.Count);

        // Desactivar todos y activar el elegido final
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null) items[i].SetActive(i == finalIndex);
        }
        currentIndexObject = finalIndex;
        // Ajustar posición final según keepRaisedAfterCycle
        if (keepRaisedAfterCycle)
            SetPosition(originalPosition + Vector3.up * raiseAmount);
        else
            ResetPosition();

        OnPrizeSelected(finalIndex);
        rawImage.SetActive(true);// activar imagen
        cycleCoroutine = null;
    }

    private void OnPrizeSelected(int index)
    {
        var obj = items[index];
        var typeComp = obj != null ? obj.GetComponent<ObjectType>() : null;
        var t = typeComp != null ? typeComp.type : default;
        OnPrizeSelectedEvent?.Invoke(obj, t);
    }

    private void Desactivate()
    {
        foreach (var it in items) if (it) it.SetActive(false);
        rawImage.SetActive(false);
    }

    public void ResetPosition()
    {
        SetPosition(originalPosition);
    }

    private void SetPosition(Vector3 pos)
    {
        if (useLocalPosition) transform.localPosition = pos;
        else transform.position = pos;
    }

    public void ObjectCompleted()
    {
        if (currentIndexObject < 0 || currentIndexObject >= items.Count)
        {
            Debug.LogWarning("ObjectCompleted: index fuera de rango o inválido.");
            return;
        }
        Desactivate();
        items.RemoveAt(currentIndexObject);
        currentIndexObject = -1;
        if (items.Count <= 0)
        {
            interactable.SetActive(false);
        }
    }
}
