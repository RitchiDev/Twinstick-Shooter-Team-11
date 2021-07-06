using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA : MonoBehaviour
{
    [SerializeField] private VitalityMeter m_HealthMeter = null;
    [SerializeField] private float m_MaxEnemyHealth = 3;
    private float m_EnemyHealth;
    private Vector3 m_SpawnPoint;

    [SerializeField] private Transform m_BodyBackside;
    [SerializeField] private GameObject m_DeathEffect = null;

    private void Start()
    {
        m_EnemyHealth = m_MaxEnemyHealth;
        m_SpawnPoint = transform.position;
    }

    public void EnemyTakeDamage(float amount)
    {
        m_EnemyHealth = Mathf.Clamp(m_EnemyHealth - amount, 0, m_MaxEnemyHealth);
        m_HealthMeter.UpdateMeter(m_EnemyHealth / m_MaxEnemyHealth);

        if (m_EnemyHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Vector3 rotationOffset = new Vector3(m_BodyBackside.rotation.x, m_BodyBackside.rotation.y + 90f, m_BodyBackside.rotation.z); //Offset van 90
        GameObject effect = Instantiate(m_DeathEffect, m_BodyBackside.position, Quaternion.Euler(rotationOffset));
        Destroy(effect, 2);
        gameObject.SetActive(false);
    }


    public void ResetEnemy()
    {
        print("Reset Enemy");
        m_EnemyHealth = m_MaxEnemyHealth;
        transform.position = m_SpawnPoint;
        m_HealthMeter.UpdateMeter(m_EnemyHealth / m_MaxEnemyHealth);
        gameObject.SetActive(true);
    }
}


//m_DeathEffect.transform.forward = Vector3.back;
//GameObject effect = Instantiate(m_DeathEffect, transform.position, Quaternion.identity);
//GameObject effect = Instantiate(m_DeathEffect, transform.position, Quaternion.FromToRotation(Vector3.forward, Vector3.back));