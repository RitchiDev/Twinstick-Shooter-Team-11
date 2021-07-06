using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] private float m_HealAmount = 1;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();

            if(player.AllowHealing())
            {
                player.ChangePlayerVitality(m_HealAmount, "Heal");
                gameObject.SetActive(false);
            }
        }
    }
}
