using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GuestData", order = 1)]
public class GuestData : ScriptableObject
{
    public string guestName;
    public bool isGuest; // if false, then they are a ghost

    // object
    public GameObject guestSprite;
    public GameObject letterSprite;
}
