using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            DontDestroyOnLoad(other.gameObject); //исчезает как группа, если из него удаляются все обьекты
            SceneManager.LoadScene(0);
        }
    }
}
