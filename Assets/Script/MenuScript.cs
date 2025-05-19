using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Image[] bagImages;
    private GameObject content;
    private Slider effectsSlider;
    private Slider musicSlider;
    private Slider gatesSlider;
    private Toggle muteToggle;
    private bool isMuted;
    private float startTimeScale;
    private float defaultMusicVolume;
    private float defaultEffectsVolume;
    private float defaultGatesVolume;
    private bool defaultIsMuted;
    void Start()
    {
        content = transform.Find("Content").gameObject;
        effectsSlider = transform.Find("Content/Sounds/EffectsSlider").GetComponent<Slider>();
        musicSlider = transform.Find("Content/Sounds/MusicSlider").GetComponent<Slider>();
        gatesSlider = transform.Find("Content/Sounds/GatesSlider").GetComponent<Slider>();
        muteToggle = transform.Find("Content/Sounds/Toggle").GetComponent<Toggle>();
        isMuted = muteToggle.isOn;

        effectsSlider.value = GameState.effectsVolume;
        musicSlider.value = GameState.musicVolume;
        gatesSlider.value = GameState.gatesVolume;
        OnMuteValueChanged(isMuted);
        startTimeScale = Time.timeScale;
        Hide();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (content.activeInHierarchy)
            {
                 Hide();
            }
            else
            {
                 Show();
            }
        }
    }

    private void Show()
    {
        startTimeScale = Time.timeScale;
        content.SetActive(true);
        Time.timeScale = 0.0f;
        for (int i = 0; i < bagImages.Length; i++)
        {
            if (GameState.bag.ContainsKey($"Key{i + 1}"))
            {
                bagImages[i].enabled = true;
            }
            else
            {
                bagImages[i].enabled = false;
            }
        }
        
    }

    private void Hide()
    {
        content.SetActive(false);
        Time.timeScale = startTimeScale;
    }
    
    public void OnEffectsVolumeValueChanged(float volume)
    {
        if(!isMuted)GameState.effectsVolume = volume;
    }

    public void OnMusicVolumeValueChanged(float volume)
    {
        if(!isMuted)GameState.musicVolume = volume;
    }
    
    public void OnGatesVolumeValueChanged(float volume)
    {
        if(!isMuted)GameState.gatesVolume = volume;
    }

    
    public void OnMuteValueChanged(bool isMute)
    {
        isMuted = isMute;
        if (isMute)
        {
            GameState.musicVolume = 0.0f;
            GameState.effectsVolume = 0.0f;
            GameState.gatesVolume = 0.0f;
        }
        else
        {
            GameState.musicVolume = musicSlider.value;
            GameState.effectsVolume = effectsSlider.value;
            GameState.gatesVolume = gatesSlider.value;
        }
     
    }

    public void OnExitClick()
    {
        //exit the program depends on the node in which its running: in the editor or itself
        //exiting the programcan close the editor without saving if the editor works.
        //on the anther hand, if exiting the editor doesnt compile to program and create a problem.
        #if UNITY_EDITOR
        //this block compiles only in editing mode
        UnityEditor.EditorApplication.isPlaying = false;
            #endif
        
            #if UNITY_STANDALONE
        //this block is running if the program compiles itself
            Application.Quit();
                #endif
    }

    private void LoadSaves()
    {
        
        if (PlayerPrefs.HasKey("effectsVolume"))
        {
            GameState.effectsVolume =
                effectsSlider.value = PlayerPrefs.GetFloat("effectsVolume");
        }
        else
        {
            effectsSlider.value = GameState.effectsVolume;
        }
        
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            GameState.musicVolume =
                musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            musicSlider.value = GameState.musicVolume;
        }
        
        if (PlayerPrefs.HasKey("gatesVolume"))
        {
            GameState.gatesVolume =
                gatesSlider.value = PlayerPrefs.GetFloat("gatesVolume");
        }
        else
        {
            gatesSlider.value = GameState.gatesVolume;
        }
        if (PlayerPrefs.HasKey("isMuted"))
        {
            
            isMuted = muteToggle.isOn = PlayerPrefs.GetInt("isMuted") == 1;
        }
        else
        {
            isMuted = muteToggle.isOn;
        }
    }

    private void GetDefaults()
    {
        // значения до того, как начнут меняться
        defaultMusicVolume = GameState.musicVolume;
        defaultEffectsVolume = GameState.effectsVolume;
        defaultGatesVolume = GameState.gatesVolume;
        defaultIsMuted = false;
    }
    public void OnDefaultsClick()
    {
        GameState.effectsVolume = effectsSlider.value = defaultEffectsVolume;
        GameState.musicVolume = musicSlider.value = defaultMusicVolume;
        GameState.gatesVolume = gatesSlider.value = defaultGatesVolume;
        isMuted = muteToggle.isOn = defaultIsMuted;
    }
    
    public void OnContinueClick()
    {
        
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("effectsVolume", effectsSlider.value);
        PlayerPrefs.SetFloat("musicVolume",  musicSlider.value);
        PlayerPrefs.SetFloat("gatesVolume", gatesSlider.value);
        PlayerPrefs.SetInt("isMuted", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }
}
