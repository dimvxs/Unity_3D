using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsScript : MonoBehaviour
{
    private AudioSource keyCollectedInTimeSound;
    private AudioSource keyCollectedOutFfTimeSound;
    private AudioSource batteryCollectedSound;
    void Start()
    {
          AudioSource[] audioSources = GetComponents<AudioSource>();
          keyCollectedInTimeSound = audioSources[0]; //indexing in order of
          batteryCollectedSound = audioSources[1]; //declaration (in inspector)
          keyCollectedOutFfTimeSound =  audioSources[2];
          GameEventSystem.Subscribe(OnGameEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGameEvent(GameEvent gameEvent)
    {
        if (gameEvent.sound != null)
        {
            switch (gameEvent.sound)
            {
                case EffectsSounds.batteryCollected: batteryCollectedSound.Play(); break;
                case EffectsSounds.keyCollectedOutOfTime: keyCollectedOutFfTimeSound.Play(); break;
                default: keyCollectedInTimeSound.Play();
                    break;
            }
          
        }
    }

    private void OnDestroy()
    {
        GameEventSystem.Unsubscribe(OnGameEvent);
    }
}
