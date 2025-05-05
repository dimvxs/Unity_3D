using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LightScript : MonoBehaviour
{
  private Light[] dayLights;
  private Light[] nightLights;
  private bool isDay;
    void Start()
    {
         dayLights = GameObject.FindGameObjectsWithTag("Day")
        .Select(g => g.GetComponent<Light>())
        .ToArray();

         nightLights = GameObject.FindGameObjectsWithTag("Night")
        .Select(g => g.GetComponent<Light>())
        .ToArray();

            isDay = true;
            foreach(Light lights in nightLights)
                {
                      lights.intensity = 0.0f;
                }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            isDay = !isDay;

            if(isDay)
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

                  foreach(Light lights in nightLights)
                {
                     lights.intensity = 1.0f;
                }

                RenderSettings.ambientIntensity = 0.0f;
                RenderSettings.reflectionIntensity = 0.0f;
            }
        }
    }
}
