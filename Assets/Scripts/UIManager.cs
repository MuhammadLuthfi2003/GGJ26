using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject GuestListUI;
    public GameObject InvitationLetterUI;

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
}
