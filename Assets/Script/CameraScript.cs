using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    private Vector3 offset; 

    //cameraAnchor - точка привязки камеры в зависимости от геометрии, это может быть центр персонажа или какая-то точка над или перед ним
    [SerializeField]
    private Transform cameraAnchor;
    private float angleY;
    private float angleX;
    private float angleY0;
    private float angleX0;
    private float sensitiivityY = 5000.0f;
    private float sensitiivityX = 5000.0f;
    private float minOffset = 1.5f;
    private float maxOffset = 12f;
    // public static bool isFpv;
    private float minAngle = -10f;
    private float maxAngle = 80f;
    // private float minAngleFpv = -10f;
    // private float maxAngleFpv = 45f;
    public static bool isFixed = false;
    public static Transform fixedCameraPosition = null!;

    // private InputAction lookAction; //unity 6


    void Start()
    {
      cameraAnchor = GameObject.Find("Player").transform;
        offset = this.transform.position - cameraAnchor.position;
        // lookAction = InputSystem.action.FindAction("Look"); //unity 6
        angleY = angleY0 = this.transform.eulerAngles.y;
        angleX = angleX0 = this.transform.eulerAngles.x;
        GameState.isFpv = offset.magnitude < minOffset;
        
    }


    void Update()
    { 

      // приближение - отдаление
      Vector2 zoom = Input.mouseScrollDelta * Time.timeScale; //y: 1 приближение, -1 отдаление

      // if(zoom != Vector2.zero){
      // Debug.Log(zoom);
      // }

      if(isFixed)
      {
        this.transform.position = fixedCameraPosition.position;
        this.transform.rotation = fixedCameraPosition.rotation;
      }
      else{
           if(zoom.y > 0)
      {
        offset *= 0.9f;
        if(offset.magnitude < minOffset)
        {
          offset *= 0.01f;
          GameState.isFpv = true;
        }
      }
      else if(zoom.y < 0){
        if(GameState.isFpv)
        {
          offset *= minOffset / offset.magnitude;
          GameState.isFpv = false;
        }
       
          if(offset.magnitude < maxOffset)
        {
         offset *= 1.1f;
        }
      }

   
  

        //обороты - анализ движений мышки и поворот камеры в соответствии с их размерами, варианты: а) поворачивать в бок курсора, б) копить угол поворота. в этом примере используется вариант б

        // Vector2 lookValue = Time.deltaTime * new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
     // Vector2 lookValue = lookAction.ReadValue<Vector2>(); //unity 6

// как lookAction, так и Input.GetAxis поворачивают данные про смезение курсора, а не его позицию, если курсор мыши не двигается, то сигнал = 0
// для того, чтобы найти полный угол, необходимо накапливать все сигналы (интегрировать сигнал)
        // angleY += lookValue.x * sensitiivityY;
        // angleX -= lookValue.y * sensitiivityX;
        // this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, angleY, 0f);
        // this.transform.eulerAngles = new Vector3(angleX, angleY, 0f);
        
         //1. следование
         //идея - созранение размещения камеры и персонажа (offset)
         //при сменной позиции остального
       // transform.position = cameraAnchor.position + Quaternion.Euler(angleX - angleX0, angleY - angleY0, 0f) * offset;
     
       
       
       Vector2 lookValue = Time.deltaTime * new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

       angleY += lookValue.x * sensitiivityY;
       angleX -= lookValue.y * sensitiivityX;
       angleX = Mathf.Clamp(angleX, minAngle, maxAngle); // ограничение угла вверх/вниз

       this.transform.eulerAngles = new Vector3(angleX, angleY, 0f);

       transform.position = cameraAnchor.position + Quaternion.Euler(angleX - angleX0, angleY - angleY0, 0f) * offset;

      }
    }
}
