using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileA : MonoBehaviour
{
    [SerializeField] private GameObject m_HitEffect;
    [SerializeField] private float m_ProjectileDamage = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().ChangePlayerVitality(m_ProjectileDamage, "Damage");
        }

        GameObject effect = Instantiate<GameObject>(m_HitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.9f);
        Destroy(gameObject);
    }
}
