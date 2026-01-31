using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Camera uiCamera;
    private Vector3 dragOffset;

    public bool isReturnAfterDrag = false;
    private Vector2 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        // Important for World Space canvas
        uiCamera = canvas.worldCamera;
        originalPos = rectTransform.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //// Optional: bring to front
        //rectTransform.SetAsLastSibling();
        Vector3 worldPoint;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            rectTransform,
            eventData.position,
            uiCamera,
            out worldPoint))
        {
            // Difference between UI position and grab point
            dragOffset = rectTransform.position - worldPoint;

            if (gameObject.CompareTag("Stamp"))
            {
                SFXManager.Instance.PlaySFX("stamp");
            }
            else if (gameObject.CompareTag("Cross"))
            {
                SFXManager.Instance.PlaySFX("cross");
            }
        }


        if (isReturnAfterDrag)
        {
            UIManager.Instance.setDropZoneColor();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            rectTransform,
            eventData.position,
            uiCamera,
            out worldPos))
        {
            rectTransform.position = worldPos + dragOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Optional: snap / clamp / validate position
        if (isReturnAfterDrag)
        {
            rectTransform.anchoredPosition = originalPos;
            UIManager.Instance.stopDropZone();
        }
    }
}
