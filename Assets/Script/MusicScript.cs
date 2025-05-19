using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{

    private AudioSource music;
    private static MusicScript instance;
     void Start()
     {
    //     music = GetComponent<AudioSource>();
    //     GameState.AddListener(OnGameStateChanged);
    //
    //     if (instance != null)
    //     {
    //         GameObject.Destroy(this.gameObject);
    //     }
    //     else
    //     {
    //         instance = this;
    //     }
    //     music = GetComponent<AudioSource>();
    
    if (!music.isPlaying)
    {
        music.Play(); // запускаем музыку только если она ещё не играет
    }
    
     }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // уничтожаем дубликат в новых сценах
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // сохраняем музыку при переходе сцен
        music = GetComponent<AudioSource>();
        GameState.AddListener(OnGameStateChanged);
    }


    
    private void OnGameStateChanged(string fieldName)
    {
        // if(fieldName == null || fieldName == nameof(GameState.musicVolume))
        // {
        //     music.volume = GameState.musicVolume;
        // }
        
        if (music != null)
        {
            music.volume = GameState.musicVolume;
        }

    }

  


    private void OnDestroy()
    {
        if (instance == this)
        {
            GameState.RemoveListener(OnGameStateChanged);
        }
    }
}
