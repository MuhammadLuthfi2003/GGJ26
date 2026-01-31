using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        DraggableUI draggable = eventData.pointerDrag.GetComponent<DraggableUI>();

        GameManager gameManager = GameManager.Instance;

        if (gameManager.guestSpawner.CurrentState == GuestSpawner.states.Check)
        {
            if (draggable == null) return;

            if (draggable.gameObject.CompareTag("Stamp"))
            {
                gameManager.guestSpawner.CurrentState = GuestSpawner.states.GuestAccepted;
                gameManager.DisableInvitationLetter();
                UIManager.Instance.CloseInvitationLetterUI();
                SFXManager.Instance.PlaySFX("stamp");
            }
            else if (draggable.gameObject.CompareTag("Cross"))
            {
                gameManager.guestSpawner.CurrentState = GuestSpawner.states.GuestRejected;
                gameManager.DisableInvitationLetter();
                UIManager.Instance.CloseInvitationLetterUI();
                //SFXManager.Instance.PlaySFX("cross");
            }

            //draggable.transform.SetParent(transform);
            //draggable.transform.localPosition = Vector3.zero;
        }
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
