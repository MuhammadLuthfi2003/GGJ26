using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DraggableUI draggable = eventData.pointerDrag
    .GetComponent<DraggableUI>();

        if (draggable == null) return;

        Debug.Log("Dropped on zone: " + draggable.gameObject.name);
        Debug.Log(draggable.gameObject.tag);

        //draggable.transform.SetParent(transform);
        //draggable.transform.localPosition = Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
