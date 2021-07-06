using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private float m_DamageAmount = 3;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().ChangePlayerVitality(m_DamageAmount, "Damage");
            //gameObject.SetActive(false);
        }
    }
}
