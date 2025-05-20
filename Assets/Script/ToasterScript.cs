using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToasterScript : MonoBehaviour
{
    private CanvasGroup contentGroup;
    private GameObject content;
    private TMPro.TextMeshProUGUI text;
    private float timeout;
    private static ToasterScript instance;
    private float showtime = 3.0f; // 3 seconds
    private Queue<ToastMessage> messageQueue = new Queue<ToastMessage>();
    private float deltaTime;
    void Start()
    {
        //сохраняем минимальное не нулевое время между фреймами
        deltaTime = 0f;
        instance = this;
        Transform t = this.transform.Find("Content");
        content = t.gameObject;
        contentGroup = content.GetComponent<CanvasGroup>();
        text = t.Find("Text").GetComponent<TMPro.TextMeshProUGUI>();
        content.SetActive(false);
        timeout = 0.0f;
        GameState.AddListener(OnGameStateChanged);
        GameEventSystem.Subscribe(OnGameEvent);
        Debug.Log($"targetFrameRate: {Application.targetFrameRate}, vSyncCount: {QualitySettings.vSyncCount}, Screen: {Screen.currentResolution.refreshRateRatio}");
    
    }

    void Update()
    {
        
        if(deltaTime == 0f || deltaTime > Time.deltaTime && Time.deltaTime > 0f) deltaTime = Time.deltaTime;
        // проблема: на паузе не исчезают уведомления
        // решение: повторить методику расчета fps / времени
        // между фреймами при условии нулевого масштаба времени
        if (timeout > 0)
        {
            float dt = Time.timeScale > 0f ? Time.deltaTime 
                
                : this.deltaTime > 0f ? this.deltaTime

                : QualitySettings.vSyncCount > 0 ? QualitySettings.vSyncCount / (float)Screen.currentResolution.refreshRateRatio.value

                : Application.targetFrameRate > 0 ? 1.0f / Application.targetFrameRate

                : 0.016f;

            Debug.Log(dt);
            timeout -= dt;
            contentGroup.alpha = Mathf.Clamp01(timeout * 2.0f);
            if (timeout < 0)
            {
                content.SetActive(false);
            }

        }
        
        else if (messageQueue.Count > 0)
        {
            var msgToast = messageQueue.Dequeue();
            content.SetActive(true);
            text.text = msgToast.message;
            timeout = msgToast.time;
        }
      
    }
    
    private void OnGameStateChanged(string fieldName)
    {

        if (fieldName == nameof(GameState.isDay))
        {
            Toast(GameState.isDay ? "the day has come" : "the night has come");
        }
        // if (fieldName == nameof(GameState.isKey1Collected))
        // {
        //     Toast("You found blue key, now you can open blue doors");
        // }
        // else if (fieldName == nameof(GameState.isKey2Collected))
        // {
        //     Toast("You found green key, now you can open green doors");
        // }
        //
        // else if (fieldName == nameof(GameState.isKey3Collected))
        // {
        //     Toast("You found red key, now you can open red doors");
        // }
    }
    
   
    public static void Toast(string message, float time = 3.0f)
    {
        // instance.content.SetActive(true);
        // instance.text.text = message;
        // instance.timeout = time == 0.0f ? instance.showtime : time;
        instance.messageQueue.Enqueue(
            new ToastMessage {
            message = message,
            time = time > 0.0f ? instance.showtime : time
        });
    }
    
    private void OnGameEvent(GameEvent gameEvent)
    {
        if (gameEvent.toast is string msg)
        {
            Toast(msg);
        }
    }

    private void OnDestroy()
    {
       GameState.RemoveListener(OnGameStateChanged);
       GameEventSystem.Unsubscribe(OnGameEvent);
    }

    private class ToastMessage
    {
        public string message {get; set;}
        public float time {get; set;}
    }
}
