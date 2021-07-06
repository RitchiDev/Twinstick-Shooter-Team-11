using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum DoorType
{
    normalDoor = 0,
    purpleKeyCardDoor,
    yellowKeyCardDoor,
    blueKeyCardDoor,
    greenKeyCardDoor,
}

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private DoorType m_DoorType = DoorType.normalDoor; //Te veel null reference waarschuwingen daarom dit erbij gezet :)
    [SerializeField] private List<Door> m_Doors = new List<Door>();

    [SerializeField] private float m_DoorTimerDelay = 0.5f;
    private bool m_DoorIsOpen;
    private string m_DoorColor;
    private float m_InteractInput;
    private float m_DoorTimer;

    private void Update()
    {
        if(m_DoorTimer > 0)
        {
            m_DoorTimer = Mathf.Clamp(m_DoorTimer - Time.deltaTime, 0, m_DoorTimerDelay);
            //print(m_DoorTimer);
        }
    }

    private void Start()
    {
        m_DoorTimer = m_DoorTimerDelay;
        m_DoorIsOpen = false;
        if (m_DoorType == DoorType.purpleKeyCardDoor)
        {
            m_DoorColor = "Purple";
        }
        else if (m_DoorType == DoorType.yellowKeyCardDoor)
        {
            m_DoorColor = "Yellow";
        }
        else if (m_DoorType == DoorType.blueKeyCardDoor)
        {
            m_DoorColor = "Blue";
        }
        else if (m_DoorType == DoorType.greenKeyCardDoor)
        {
            m_DoorColor = "Green";
        }
    }

    private void OnTriggerStay(Collider collision)
    {

        if (collision.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if (m_DoorType == DoorType.normalDoor)
            {
                if (m_InteractInput != 0) //Check of de deur in de PlayerInput zit :)
                {
                    if(m_DoorTimer <= 0)
                    {
                        if (m_DoorIsOpen)
                        {
                            for (int d = 0; d < m_Doors.Count; d++)
                            {
                                m_Doors[d].CloseDoor();
                                m_DoorIsOpen = false;
                            }
                        }
                        else
                        {
                            for(int d = 0; d < m_Doors.Count; d++)
                            {
                                m_Doors[d].OpenDoor();
                                m_DoorIsOpen = true;
                            }
                        }
                        m_DoorTimer = m_DoorTimerDelay;
                    }
                }
            }
            else
            {
                for (int i = 0; i < player.GetInventory().Count; i++) //Kijkt door de inventory van de speler
                {
                    if(player.GetInventory()[i] == m_DoorColor) //Als er een KeyCard met hetzelfde kleur als de deur in de speler zijn inventory zit
                    {
                        if (m_InteractInput != 0) //Check of de deur in de PlayerInput zit :)
                        {
                            if (m_DoorTimer <= 0)
                            {
                                if (m_DoorIsOpen)
                                {
                                    for (int d = 0; d < m_Doors.Count; d++)
                                    {
                                        m_Doors[d].CloseDoor();
                                        m_DoorIsOpen = false;
                                    }
                                }
                                else
                                {
                                    for (int d = 0; d < m_Doors.Count; d++)
                                    {
                                        m_Doors[d].OpenDoor();
                                        m_DoorIsOpen = true;
                                    }
                                }
                                m_DoorTimer = m_DoorTimerDelay;
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(m_DoorIsOpen)
            {
                if(m_DoorTimer <= 0)
                {
                    for (int d = 0; d < m_Doors.Count; d++)
                    {
                        m_Doors[d].CloseDoor();
                        m_DoorIsOpen = false;
                    }
                }
            }
        }
    }

    public void GetInteractInput(InputAction.CallbackContext context)
    {
        m_InteractInput = context.ReadValue<float>();
    }
}
