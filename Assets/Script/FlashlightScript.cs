using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightScript : MonoBehaviour
{
    private GameObject player;
    private Light light;
    public static float charge;
    public static float chargeLifetime = 10.0f;
    
    //luminosity angle boundaries
    public float minAngle = 20f;
    public float maxAngle = 100f;
    public float angleStep = 2f;
    void Start()
    {
        player = GameObject.Find("Player");

        if(player == null)
        {
            Debug.Log("Flashlight script:  player not found");
        }
        light = GetComponent<Light>();
        charge = 1.0f;
    }

 
    void Update()
    {
        if(player == null) return;

        this.transform.position = player.transform.position;
        this.transform.forward = Camera.main.transform.forward;

         if(GameState.isFpv && !GameState.isDay)
         {
             light.intensity = charge;
             // charge = Mathf.Clamp01(charge - Time.deltaTime / chargeLifetime);
             charge = charge < 0 ? 0 : charge - Time.deltaTime / chargeLifetime;
         }
         
         else
         {
            light.intensity = 0.0f;
         }
         HandleBeamAngleChange();
    }
    private void HandleBeamAngleChange()
    {
        if (Input.GetKey(KeyCode.LeftBracket)) 
        {
            light.spotAngle = Mathf.Max(minAngle, light.spotAngle - angleStep * Time.deltaTime * 30f);
        }
        else if (Input.GetKey(KeyCode.RightBracket))
        {
            light.spotAngle = Mathf.Min(maxAngle, light.spotAngle + angleStep * Time.deltaTime * 30f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Battery"))
        {
            charge += 1.0f;
            GameObject.Destroy(other.gameObject);
            Debug.Log("battery collected: " + charge);
            ToasterScript.Toast($"You found battery, your charge is: {charge:F1}", 3.0f);
        }
        
        if (other.gameObject.CompareTag("LittleBattery"))
        {
            charge += 0.5f;
            GameObject.Destroy(other.gameObject);
            Debug.Log("battery collected: " + charge);
            ToasterScript.Toast($"You found little battery, your charge is: {charge:F1}", 3.0f);
        }
        
    }
}
