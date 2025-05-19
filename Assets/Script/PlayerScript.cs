using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;
    private static PlayerScript prevInstance = null;
    // private InputAction moveAction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (prevInstance != null) // in the start moment exists another object of this class
        {
            //скорее всего, prevInstance - обьект DontDestroyOnLoad, которыйперезодит с предыдущей сцены
            // нужно принять решение, какой обьект должен остаться: this или prevInstance
            // a)оставляем this, тогда переносим необхожимые харктеристики из prevInstance и удаляем его

            // this.rb.velocity = prevInstance.rb.velocity;
            // this.rb.angularVelocity = prevInstance.rb.angularVelocity;
            // GameObject.Destroy(prevInstance.gameObject);
            
            
            // б) оставляем prevInstance, просто удаляем this
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            prevInstance = this; //saving reference to current object in static field

        }
        
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
