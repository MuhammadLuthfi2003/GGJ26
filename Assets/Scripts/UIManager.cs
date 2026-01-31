using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject GuestListUI;
    public GameObject InvitationLetterUI;

    public Image DropZone;

    // A public static property to access the single instance
    public static UIManager Instance { get; private set; }
    void Awake()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            // If so, destroy this new object
            Destroy(this.gameObject);
        }
        else
        {
            // Otherwise, set this object as the single instance
            Instance = this;
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

    public void OpenGuestListUI()
    {
        GuestListUI.SetActive(true);
    }

    public void OpenInvitationLetterUI()
    {
        InvitationLetterUI.SetActive(true);
    }

    public void CloseGuestList()
    {
        GuestListUI.SetActive(false);
    }

    public void CloseInvitationLetterUI()
    {
        InvitationLetterUI.SetActive(false);
    }

    public void setDropZoneColor()
    {
        DropZone.enabled = true;
        DropZone.color = new Color(0f, 0.6586814f, 1f, 0.5f); // red, 50% transparent
    }

    public void stopDropZone()
    {
        DropZone.enabled = false;
    }
}
