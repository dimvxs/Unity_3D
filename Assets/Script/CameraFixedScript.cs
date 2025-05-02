using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFixedScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CameraScript.fixedCameraPosition = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            CameraScript.isFixed = ! CameraScript.isFixed;
            Debug.Log(CameraScript.isFixed);
        }
    }
}
