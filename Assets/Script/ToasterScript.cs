using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToasterScript : MonoBehaviour
{
    private static GameObject content;
    private static TMPro.TextMeshProUGUI text;
    private static float timeout;
    private static float showtime = 3.0f; //3 seconds
    void Start()
    {
        Transform t = this.transform.Find("Content");
        content = t.gameObject;
        text = t.Find("Text").GetComponent<TMPro.TextMeshProUGUI>();
        content.SetActive(false);
        timeout = 0.0f;
    }

    void Update()
    {
        if (timeout > 0)
        {
            timeout -= Time.deltaTime;
            if (timeout < 0)
            {
                content.SetActive(false);
            }

        }
      
    }

    public static void Toast(string message, float time = 3.0f)
    {
        content.SetActive(true);
        text.text = message;
        timeout = time == 0.0f ? showtime : time;
    }
}
