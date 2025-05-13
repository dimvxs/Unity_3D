using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KeyScript : MonoBehaviour
{
 

    [SerializeField] private int keyNumber = 1;
    [SerializeField] private float timeout = 5.0f;
    [SerializeField] private string description = "matching";
    private GameObject content;
    private Image indicatorImage;
    private float leftTime;
    private bool isInTime = true;
    void Start()
    {
        content = transform.Find("Content").gameObject;
        indicatorImage = transform.Find("Indicator/Canvas/Foreground").GetComponent<Image>();
        indicatorImage.fillAmount = 1.0f;
        leftTime = timeout;
        // GameState.isKey1InTime = true;
    }

    
    void Update()
    {
        content.transform.Rotate(0, Time.deltaTime * 20f, 0);
        if (leftTime > 0)
        {
            indicatorImage.fillAmount = leftTime / timeout;
            leftTime -= Time.deltaTime;
            indicatorImage.color = new Color(
                Mathf.Clamp01(2.0f * (1.0f - indicatorImage.fillAmount)),
                Mathf.Clamp01(2.0f * indicatorImage.fillAmount),
                0.0f
            );

            if (leftTime < 0)
            {
                // GameState.SetProperty($"isKey{keyNumber}InTime", false);
                isInTime = false;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            // var prop = typeof(GameState).GetProperty("isKey1Collected", System.Reflection.BindingFlags.Static  | System.Reflection.BindingFlags.Public);
            // prop.SetValue(null, true);
            // GameState.isKey1Collected = true;
            GameState.SetProperty($"isKey{keyNumber}Collected", true);
            GameEventSystem.EmitEvent(new GameEvent
                {
                    type = $"Key{keyNumber}Collected",
                    payload = isInTime,
                    toast = $"You found {keyNumber} key, now you can open {description} doors",
                    sound = isInTime
                    ? EffectsSounds.keyCollectedInTime
                    : EffectsSounds.keyCollectedOutOfTime,
                });
            Destroy(this.gameObject);
        }
    }

}
