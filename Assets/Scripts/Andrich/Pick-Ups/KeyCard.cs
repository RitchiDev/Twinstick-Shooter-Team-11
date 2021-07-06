using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public enum KeyCardColor
{
    purple = 0,
    green,
    yellow,
    blue,
}

public class KeyCard : MonoBehaviour
{

    [SerializeField] private KeyCardColor m_KeyCardColor = KeyCardColor.purple; //Te veel null reference waarschuwingen daarom dit erbij gezet :)
    private string m_Color;

    private void Start()
    {
        if(m_KeyCardColor == KeyCardColor.purple)
        {
            m_Color = "Purple";
        }
        else if(m_KeyCardColor == KeyCardColor.green)
        {
            m_Color = "Green";
        }
        else if (m_KeyCardColor == KeyCardColor.yellow)
        {
            m_Color = "Yellow";
        }
        else if (m_KeyCardColor == KeyCardColor.blue)
        {
            m_Color = "Blue";
        }
    }

    public string GetColor() //Returned de kleur van de KeyCard
    {
        return m_Color;
    }

}
