using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GuestData currentGuestInCheck;

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
}
