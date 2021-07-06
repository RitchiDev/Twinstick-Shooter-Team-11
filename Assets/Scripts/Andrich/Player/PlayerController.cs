using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private float m_ShootInput; //Ga naar de PlayerShooting script voor het wijzigen van schieten :)
    [SerializeField] private Transform m_Body;

    [Header("Movement")]
    [SerializeField] private float m_Speed = 8;
    private Vector3 m_MovementInput;


    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponentInChildren<Animator>();
    }


    private void Update()
    {
        if(m_MovementInput != Vector3.zero && m_ShootInput == 0)
        {
            m_Animator.SetBool("Moving", true);
        }
        else
        {
            m_Animator.SetBool("Moving", false);
        }

        if(m_ShootInput != 0)
        {
            m_Animator.SetBool("Shooting", true);
        }
        else
        {
            m_Animator.SetBool("Shooting", false);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer(m_MovementInput.normalized);    
    }

    public void GetMovementInput(InputAction.CallbackContext context)
    {
        m_MovementInput.x = context.ReadValue<Vector2>().x;
        m_MovementInput.z = context.ReadValue<Vector2>().y;
    }

    public void CheckShootInput(InputAction.CallbackContext context) 
    {
        //Dit is zodat ik op een simeple manier kan checken of de speler schiet :)
        //Echt schieten gebeurt in de PlayerShooting script ga daar a.u.b naartoe voor het wijzigen van schieten :)
        m_ShootInput = context.ReadValue<float>();
    }

    private void MovePlayer(Vector3 direction)
    {
        if (m_ShootInput == 0) //Als de speler niet schiet :)
        {
            m_Rigidbody.MovePosition(m_Rigidbody.position + direction * m_Speed * Time.deltaTime); //Beweeg de speler

            if(direction != Vector3.zero)
            {

                Vector3 lookDirection = Vector3.RotateTowards(m_Body.transform.forward, direction, 10 * Time.deltaTime, 0); //Check ook de rotatie snelheid in de PlayerController script :)
                m_Body.rotation = Quaternion.LookRotation(lookDirection); //Roteer alleen de model (Body) van de speler
            }
        }
    }
}



//private void InputChecker()
//{
//    m_MovementInput.x = Input.GetAxis("Horizontal");
//    m_MovementInput.z = Input.GetAxis("Vertical");

//    if(m_MovementInput != Vector3.zero)
//    {
//        m_Animator.SetBool("Moving", true);
//    }
//    else
//    {
//        m_Animator.SetBool("Moving", false);
//    }
//}

//void Update()
//{
//    InputChecker();
//}

//private void FixedUpdate()
//{
//    MovePlayer();
//}

//[SerializeField] private KeyCode m_InteractButton = KeyCode.E;

//public bool GetInteraction()
//{
//    return Input.GetKeyDown(m_InteractButton);
//}

//Vector3 newLookDirection = Vector3.RotateTowards(m_Rigidbody.transform.forward, direction, 3, 0);
//m_Rigidbody.rotation = Quaternion.LookRotation(newLookDirection);
//m_Body.LookAt(direction, Vector3.up);
//m_Body.localRotation = Quaternion.Slerp(m_Rigidbody.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 3);