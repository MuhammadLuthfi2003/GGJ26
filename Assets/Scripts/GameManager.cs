using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameStarted = false;

    public GuestData currentGuestInCheck;
    public int HP = 3;

    public GuestSpawner guestSpawner;

    [Header("Display Invitation Letter")]
    public GameObject invitationLetterObject;
    public TextMeshProUGUI invitationLetterText;

    // A public static property to access the single instance
    public static GameManager Instance { get; private set; }
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
            // Optional: Keep the object alive across scene loads
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            // show killscreen
            GameOver();
            isGameStarted = false;
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        if (guestSpawner)
        {
            guestSpawner.SpawnGuest();
        }
    }

    public void SpawnInvitationLetter()
    {
        invitationLetterObject.SetActive(true);
    }

    public void DisableInvitationLetter()
    {
        invitationLetterObject.SetActive(false);
    }

    public void SetInvitationLetterName(string guestName)
    {
        invitationLetterText.text = guestName;
    }

    public void Win()
    {
        print("Wins");
    }

    public void GameOver()
    {
        print("Game Over");
    }
}
