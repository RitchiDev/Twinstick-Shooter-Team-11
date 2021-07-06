using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject m_HitEffect;
    [SerializeField] private float m_ProjectileDamage = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyA>().EnemyTakeDamage(m_ProjectileDamage);
        }
        
        GameObject effect = Instantiate<GameObject>(m_HitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.9f);
        Destroy(gameObject);
    }
}
