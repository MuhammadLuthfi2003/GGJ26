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
    public SFXManager sfxManager;

    [Header("Display Invitation Letter")]
    public GameObject invitationLetterObject;
    public TextMeshProUGUI invitationLetterText;

    [Header("Warning Object")]
    public GameObject warningLetter1;
    public GameObject warningLetter2;
    public GameObject warningLetter3;

    [Header("Screen GUI")]
    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public GameObject winScreen;

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
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        SFXManager.Instance.PlaySFX("button_play");
        isGameStarted = true;
        titleScreen.SetActive(false);
        if (guestSpawner)
        {
            guestSpawner.SpawnGuest();
            HP = 3;
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

        isGameStarted = false;
        winScreen.SetActive(true);
    }

    public void GameOver()
    {
        SFXManager.Instance.sfxSource.Stop();
        SFXManager.Instance.PlaySFX("game_over");
        print("Game Over");
        isGameStarted = false;
        gameOverScreen.SetActive(true);
    }

    public void ShowWarning()
    {
        switch(HP)
        {
            case 0:
                warningLetter3.SetActive(true);
                break;
            case 1:
                warningLetter2.SetActive(true);
                break;
            case 2:
                warningLetter1.SetActive(true);
                break;
        }
    }

    public void CloseWarning1()
    {
        warningLetter1.SetActive(false);
    }
    public void CloseWarning2()
    {
        warningLetter2.SetActive(false);
    }
    public void CloseWarning3()
    {
        warningLetter3.SetActive(false);

        GameOver();
    }

    public void RestartGame()
    {
        SFXManager.Instance.sfxSource.Stop();
        winScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        guestSpawner.ResetGuestData();
        StartGame();
        SFXManager.Instance.PlaySFX("ambience suara musik indoor muffle ke luar security");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
