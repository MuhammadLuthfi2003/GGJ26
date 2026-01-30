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

    private GameManager gameManager;
    public float speed;

    private GameObject spawnedObject;

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


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            SpawnGuest();
        }

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
            // listen for inputs
            if (Input.GetKeyDown(KeyCode.A))
            {
                // reset time
                time = 0;
                // accept
                CurrentState = states.GuestAccepted;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                // reset time
                time = 0;
                // decline
                CurrentState = states.GuestRejected;
            }

        }
        else if (CurrentState == states.GuestAccepted)
        {
            if (gameManager.currentGuestInCheck && spawnedObject)
            {
                time += Time.deltaTime;
                spawnedObject.transform.position = Vector3.MoveTowards(
                    spawnedObject.transform.position,
                    endPoint.position,
                    speed * Time.deltaTime
                );
                if (Vector3.Distance(spawnedObject.transform.position, endPoint.position) <= 0.1)
                {
                    // MoveTowards is finished
                    CurrentState = states.None;
                }
            }
        }
        else if (CurrentState == states.GuestRejected)
        {
            if (gameManager.currentGuestInCheck && spawnedObject)
            {
                time += Time.deltaTime;
                spawnedObject.transform.position = Vector3.MoveTowards(
                    spawnedObject.transform.position,
                    spawnLocation.position,
                    speed * Time.deltaTime
                );
                if (Vector3.Distance(spawnedObject.transform.position, spawnLocation.position) <= 0.1)
                {
                    // MoveTowards is finished
                    CurrentState = states.None;
                }
            }
        }
    }

    void SpawnGuest()
    {
        // pick random element from the list
        if (guestData.Count > 0 && spawnedObject == null && CurrentState == states.None)
        {
            int randNum = Random.Range(0, guestData.Count);
            GuestData pickedGuest = guestData[randNum];

            // remove
            guestData.Remove(pickedGuest);
            gameManager.currentGuestInCheck = pickedGuest;

            spawnedObject = Instantiate(pickedGuest.guestSprite, spawnLocation.position, Quaternion.identity);
            CurrentState = states.GuestIn;
        }
    }
}
