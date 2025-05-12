using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gates1Script : MonoBehaviour
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
    
    
    void Start()
    {
        isKeyInserted = false;
        hitCount = 0;
        GameState.AddListener(OnGameStateChanged);
    }

   
    
    void Update()
    {
        if (isKeyInserted && transform.localPosition.magnitude < size)
        {
            transform.Translate(size * Time.deltaTime / openTime * openDirection);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (GameState.isKey1Collected)
            {
               isKeyInserted = true;
               openTime = isKey1InTime? openTime1 : openTime2;
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
    
    private void OnGameStateChanged(string fieldName)
    {
        if (fieldName == $"isKey{keyNumber}Collected")
        {
            isKeyCollected = true;
        }
        else if (fieldName == $"isKey{keyNumber}InTime")
        {
            isKeyCollected = false;
        }
    }

    private void OnDestroy()
    {
        GameState.RemoveListener(OnGameStateChanged);
    }
}
