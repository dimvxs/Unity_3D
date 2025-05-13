using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatesScript : MonoBehaviour
{
    
    [SerializeField] Vector3 openDirection = Vector3.back;
    [SerializeField] private float size = 1.1f;
    private float openTime;
    private float openTime1 = 3.0f;
    private float openTime2 = 10.0f;
    private bool isKeyInserted;
    private bool isKeyCollected = false;
    private bool isKey1InTime = true;
    private bool isKeyInTime = true;
    private int hitCount;
    [SerializeField] private int keyNumber = 1;
    private bool isInTime = true;
    private AudioSource openingSound1;
    private AudioSource openingSound2;
    private bool isOpened = false;

    void Start()
    {
        isKeyInserted = false;
        hitCount = 0;
        GameEventSystem.Subscribe(OnGameEvent);
        openingSound1 = GetComponent<AudioSource>();
        openingSound2 = GetComponent<AudioSource>();
        AudioSource[] openingSounds = GetComponents<AudioSource>();
        openingSound1 = openingSounds[0];
        openingSound2 = openingSounds[1];
        
    }

   
    
    void Update()
    {
        if (!isOpened && isKeyInserted && transform.localPosition.magnitude < size)
        {
            transform.Translate(size * Time.deltaTime / openTime * openDirection);

            if (transform.localPosition.magnitude >= size)
            {
                //Opening ends
                isOpened = false;
                openingSound1.Stop();
                openingSound2.Stop();
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (isKeyCollected)
            {

                if (!isKeyInserted)
                {
                    //opening begins
                    isKeyInserted = true;
                    openTime = isKey1InTime? openTime1 : openTime2;
                    (isKeyInTime ? openingSound1 : openingSound2).Play();
                    GameEventSystem.EmitEvent(new GameEvent
                    {
                        type = $"",
                        payload = isInTime,
                        toast = $"The door is opened key, now you can go"
                    });
                    
                }
             
              
            }
            else
            {
                hitCount += 1;
                if (hitCount == 1)
                {
                    ToasterScript.Toast($"You need to find key {keyNumber} to open the door");
                }
                else
                {
                    ToasterScript.Toast($"I say {hitCount} time, you need to find key {keyNumber} to open the door");
                }
            }
        }
       
        Debug.Log(collision.gameObject.name);
    }


    private void OnGameEvent(GameEvent gameEvent)
    {
        if (gameEvent.type == $"Key{keyNumber}Collected")
        {
            isKeyCollected = true;
            isKeyInTime = (bool)gameEvent.payload;
        }
    }

    private void OnDestroy()
    {
        GameEventSystem.Unsubscribe(OnGameEvent);
    }
}
