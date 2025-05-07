using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LightScript : MonoBehaviour
{
  private Light[] dayLights;
  private Light[] nightLights;
  // public static bool isDay;
    void Start()
    {
         dayLights = GameObject.FindGameObjectsWithTag("Day")
        .Select(g => g.GetComponent<Light>())
        .ToArray();

         nightLights = GameObject.FindGameObjectsWithTag("Night")
        .Select(g => g.GetComponent<Light>())
        .ToArray();

            GameState.isDay = true;
            foreach(Light lights in nightLights)
                {
                      lights.intensity = 0.0f;
                }
            
                GameState.AddListener(OnGameStateChanged);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            GameState.isDay = !GameState.isDay;

            // if(GameState.isDay)
            // {
            //     foreach(Light lights in dayLights)
            //     {
            //           lights.intensity = 1.0f;
            //     }

            //       foreach(Light lights in nightLights)
            //     {
            //           lights.intensity = 0.0f;
            //     }

            //     RenderSettings.ambientIntensity = 1.0f;
            //     RenderSettings.reflectionIntensity = 1.0f;
            // }
            // else{

              
                
            //        foreach(Light lights in dayLights)
            //     {
            //           lights.intensity = 0.0f;
            //     }
                

            //       if(!GameState.isFpv){
   
            //       foreach(Light lights in nightLights)
            //     {
            //          lights.intensity = 1.0f;
            //     }
            // }

            //     RenderSettings.ambientIntensity = 0.0f;
            //     RenderSettings.reflectionIntensity = 0.0f;
            // }
        }


        // if(!GameState.isDay && !GameState.isFpv && nightLights[0].intensity == 0)
        // {
        //    foreach(Light light in nightLights)
        //         {
        //             light.intensity = 1.0f;
        //         }
        // }

        //         if(!GameState.isDay && !GameState.isFpv && nightLights[0].intensity == 1)
        // {
        //    foreach(Light light in nightLights)
        //         {
        //             light.intensity = 0.0f;
        //         }
        //         GameState.AddListener(OnGameStateChanged);
        // }
    }

  private void ToggleLights()
    {
if(GameState.isDay)
            {
                foreach(Light lights in dayLights)
                {
                      lights.intensity = 1.0f;
                }

                  foreach(Light lights in nightLights)
                {
                      lights.intensity = 0.0f;
                }

                RenderSettings.ambientIntensity = 1.0f;
                RenderSettings.reflectionIntensity = 1.0f;
            }
            else{

              
                
                   foreach(Light lights in dayLights)
                {
                      lights.intensity = 0.0f;
                }
                  if(!GameState.isFpv){
   
                  foreach(Light lights in nightLights)
                {
                     lights.intensity = 1.0f;
                }
            }

                RenderSettings.ambientIntensity = 0.0f;
                RenderSettings.reflectionIntensity = 0.0f;
    }
    }

     private void FpvChanged()
    {
      
        // if(!GameState.isDay && !GameState.isFpv && nightLights[0].intensity == 0)
        // {
        //    foreach(Light light in nightLights)
        //         {
        //             light.intensity = 1.0f;
        //         }
        // }

        //         if(!GameState.isDay && !GameState.isFpv && nightLights[0].intensity == 1)
        // {
        //    foreach(Light light in nightLights)
        //         {
        //             light.intensity = 0.0f;
        //         }
        //         GameState.AddListener(OnGameStateChanged);
        // }

         if(!GameState.isDay)
        {
           foreach(Light light in nightLights)
                {
                    light.intensity = GameState.isFpv ? 0.0f : 1.0f;
                }
        }
    }

    private void OnGameStateChanged(string fieldName)
    {
         if(fieldName == nameof(GameState.isDay))
         {
              ToggleLights();
         }

         else if(fieldName == nameof(GameState.isFpv))
         {
             FpvChanged();
         }
    }

  


    private void OnDestroy()
    {
       GameState.RemoveListener(OnGameStateChanged);
    }
}
