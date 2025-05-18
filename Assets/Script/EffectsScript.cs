using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsScript : MonoBehaviour
{
    private AudioSource keyCollectedInTimeSound;
    private AudioSource keyCollectedOutOfTimeSound;
    private AudioSource batteryCollectedSound;
    void Start()
    {
          AudioSource[] audioSources = GetComponents<AudioSource>();
          keyCollectedInTimeSound = audioSources[0]; //indexing in order of
          batteryCollectedSound = audioSources[1]; //declaration (in inspector)
          keyCollectedOutOfTimeSound =  audioSources[2];
          GameEventSystem.Subscribe(OnGameEvent);
          GameState.AddListener(OnGameStateChanged);
          OnGameStateChanged(nameof(GameState.effectsVolume));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGameEvent(GameEvent gameEvent) {
        if (gameEvent.sound != null) {
            switch (gameEvent.sound) {
                case EffectsSounds.batteryCollected: batteryCollectedSound.Play(); break;
                case EffectsSounds.keyCollectedOutOfTime: keyCollectedOutOfTimeSound.Play(); break;
                default: keyCollectedInTimeSound.Play(); break;
            }
        }
    }
    private void OnGameStateChanged(string fieldName) {
        if (fieldName == nameof(GameState.effectsVolume)) {
            keyCollectedInTimeSound.volume = 
                batteryCollectedSound.volume =
                    keyCollectedOutOfTimeSound.volume = GameState.effectsVolume;
        }
    }
    private void OnDestroy() {
        GameEventSystem.Unsubscribe(OnGameEvent);
        GameState.RemoveListener(OnGameStateChanged);
    }
}
