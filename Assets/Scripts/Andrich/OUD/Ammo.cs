using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private float m_Ammo = 10;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerShooting player = collision.GetComponent<PlayerShooting>();

            //if(player.AllowAmmoPickup())
            //{
            //    player.ChangeAmmoValue(m_Ammo);
            //    //gameObject.SetActive(false);
            //}
        }
    }
}
