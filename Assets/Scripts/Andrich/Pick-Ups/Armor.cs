using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    [SerializeField] private float m_Armor = 5;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();

            if (player.AllowArmorPickup())
            {
                player.ChangePlayerVitality(m_Armor, "Armor");
                gameObject.SetActive(false);
            }
        }
    }
}
