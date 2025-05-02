using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;
    // private InputAction moveAction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // Vector2 moveValue = moveAction.ReadValue<Vector2>(); //from Unity 6
        // rb.AddForce(moveValue.x, 0f, moveValue.y); // при таком управлении учитываются световые оси Х и Y, что не является удобным при поворотах камеры

        //привязка к ориентации камеры осуществляется с помощью её векторов
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        //которые необходимо скорректировать на уклон камеры
        camForward.y = 0f; //убираем вертикальную составляющую
        
        if(camForward == Vector3.zero) // вектор был полностью вертикальным
        {
            camForward = Camera.main.transform.up; // тогда меняем его на вектор up
        }
        else{
            camForward.Normalize(); //подгоняем вектор к единичной длине
        }

        // формируем вектор силы
        Vector3 force = 
        camForward * moveValue.y +  //сигнал Y вдоль вектора исправленного forward
        camRight * moveValue.x;  //сигнал Y вдоль вектора right
        rb.AddForce(force);
    }
}
