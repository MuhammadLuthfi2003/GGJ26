using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class GuestSpawner : MonoBehaviour
{
    public Transform spawnLocation;
    public Transform checkPoint;
    public Transform endPoint;

    public List<GuestData> guestData = new List<GuestData>();
    private List<GuestData> copy;

    private GameManager gameManager;
    public float speed;

    private GameObject spawnedObject;

    public GameObject fire;

    public enum states
    {
        None,
        GuestIn,
        Check,
        GuestAccepted,
        GuestRejected,
    }

    public states CurrentState = states.None;
    public float time = 0;

    private bool hasRun = false;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameManager.Instance;
        copy = new List<GuestData>(guestData);
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentState == states.None)
        {
            time = 0;
            spawnedObject = null;
        }
        // guest in transition
        else if (CurrentState == states.GuestIn)
        {
            if (gameManager.currentGuestInCheck && spawnedObject)
            {
                time += Time.deltaTime;
                spawnedObject.transform.position = Vector3.MoveTowards(
                    spawnedObject.transform.position,
                    checkPoint.position,
                    speed * Time.deltaTime
                );
                if (Vector3.Distance(spawnedObject.transform.position, checkPoint.position) <= 0.1)
                {
                    // MoveTowards is finished
                    CurrentState = states.Check;
                }
            }
        }
        else if (CurrentState == states.Check)
        {
            if (gameManager.currentGuestInCheck)
            {
                time = 0;
                if (gameManager.currentGuestInCheck.hasInvitationLetter)
                {
                    gameManager.SpawnInvitationLetter();
                    
                }
            }
        }
        else if (CurrentState == states.GuestAccepted)
        {
            if (gameManager.currentGuestInCheck && spawnedObject)
            {
                if (!hasRun)
                {
                    if (gameManager.currentGuestInCheck.isFemale)
                    {
                        SFXManager.Instance.PlaySFX("tamu_cewe");
                    }
                    else if (gameManager.currentGuestInCheck.isTung)
                    {
                        SFXManager.Instance.PlaySFX("tung_sahur");
                    }
                    else
                    {
                        SFXManager.Instance.PlaySFX("tamu_cowo");
                    }
                    hasRun = true;
                }

                time += Time.deltaTime;
                spawnedObject.transform.position = Vector3.MoveTowards(
                    spawnedObject.transform.position,
                    endPoint.position,
                    speed * Time.deltaTime
                );
                if (Vector3.Distance(spawnedObject.transform.position, endPoint.position) <= 0.1)
                {
                    // MoveTowards is finished
                    if (spawnedObject != null)
                    {
                        Destroy(spawnedObject);
                    }
                    CurrentState = states.None;
                    VerifyGuest(true);
                }
            }
        }
        else if (CurrentState == states.GuestRejected)
        {
            if (gameManager.currentGuestInCheck && spawnedObject)
            {
                if (!hasRun)
                {
                    if (!gameManager.currentGuestInCheck.isGuest)
                    {
                        SpawnFire();
                        SFXManager.Instance.PlaySFX("fire");
                        SFXManager.Instance.PlaySFX("iblis_kena_salib");
                    }
                    else
                    {
                        if (gameManager.currentGuestInCheck.isFemale)
                        {
                            SFXManager.Instance.PlaySFX("cewe_kena_salib");
                        }
                        else
                        {
                            SFXManager.Instance.PlaySFX("cowo_kena_salib");
                        }
                    }
                    hasRun = true;
                }

                time += Time.deltaTime;
                spawnedObject.transform.position = Vector3.MoveTowards(
                    spawnedObject.transform.position,
                    spawnLocation.position,
                    speed * Time.deltaTime
                );
                if (Vector3.Distance(spawnedObject.transform.position, spawnLocation.position) <= 0.1)
                {
                    // MoveTowards is finished
                    if (spawnedObject != null)
                    {
                        Destroy(spawnedObject);
                    }
                    CurrentState = states.None;
                    VerifyGuest(false);
                }
            }
        }
    }

    public void SpawnGuest()
    {
        // pick random element from the list
        if (guestData.Count > 0 && spawnedObject == null && CurrentState == states.None)
        {
            int randNum = Random.Range(0, guestData.Count);
            GuestData pickedGuest = guestData[randNum];

            // remove
            gameManager.currentGuestInCheck = pickedGuest;

            spawnedObject = Instantiate(pickedGuest.guestSprite, spawnLocation.position, Quaternion.identity);
            gameManager.SetInvitationLetterName(gameManager.currentGuestInCheck.guestName);
            guestData.Remove(pickedGuest);

            if (gameManager.currentGuestInCheck.isFemale)
            {
                SFXManager.Instance.PlaySFX("tamu_cewe");
            }
            else if (gameManager.currentGuestInCheck.isTung)
            {
                SFXManager.Instance.PlaySFX("tung_sahur");
            }
            else
            {
                SFXManager.Instance.PlaySFX("tamu_cowo");
            }

            CurrentState = states.GuestIn;
        }
        else if (guestData.Count <= 0)
        {
            gameManager.Win();
        }
    }

    void SpawnFire()
    {
        if (spawnedObject)
        {
            GameObject fireobj = Instantiate(fire, spawnedObject.transform.position, Quaternion.identity);
            fireobj.transform.parent = spawnedObject.transform;
        }
    }

    void VerifyGuest(bool isAccept)
    {
        hasRun = false;
        if (!gameManager.currentGuestInCheck) { return; }

        // get guest data isGuest
        bool isGuestReal = gameManager.currentGuestInCheck.isGuest;

        if (isGuestReal)
        {
            // correctly accept guest
            if (isAccept) 
            {
                print("guest verified");
                // spawns next guest
                CurrentState = states.None;
                spawnedObject = null;
                StartCoroutine(SpawnNextGuest(1));
            }
            // wrongly decline guest
            else
            {
                print("you declined the guest!, how could you!!!");
                gameManager.HP--;

                if (gameManager.HP > 0)
                {
                    gameManager.ShowWarning();
                    CurrentState = states.None;
                    spawnedObject = null;
                    StartCoroutine(SpawnNextGuest(1));
                }
            }
        }
        else
        {
            // wrongly accept impostor
            if (isAccept)
            {
                // gameover
                gameManager.HP = 0;
                gameManager.ShowWarning();
            }
            // correctly decline impostor
            else
            {
                // spawn next 
                StartCoroutine(SpawnNextGuest(1));
            }

            CurrentState = states.None;
            spawnedObject = null;
        }
    }

    IEnumerator SpawnNextGuest(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SpawnGuest();
    }

    public void ResetGuestData()
    {
        CurrentState = states.None;
        guestData = new List<GuestData>(copy);
    }
}
