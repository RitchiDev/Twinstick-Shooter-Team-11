using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingA : MonoBehaviour
{

    private EnemyVisionConeA m_VisionCone;
    private Transform m_PlayerTransform;
    private Rigidbody m_Rigidbody;

    [Header("Shooting")]
    [SerializeField] Transform m_FirePoint = null;
    [SerializeField] private float m_ShootDelay = 2;
    private float m_ShootTimer;
    private Vector3 m_LookDirection;

    [Header("Projectile")]
    [SerializeField] private GameObject m_ProjectilePrefab = null;
    [SerializeField] private float m_ProjectileSpeed = 30;


    private void Start()
    {
        m_ShootTimer = m_ShootDelay / 2;
        m_VisionCone = GetComponentInChildren<EnemyVisionConeA>();
        m_PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(m_VisionCone.PlayerHasEnteredVision())
        {
            if (m_ShootTimer > 0)
            {
                m_ShootTimer -= Time.deltaTime;
            }
            else if(m_ShootTimer < 0)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        m_LookDirection = m_PlayerTransform.position - m_Rigidbody.position;
        m_ShootTimer = m_ShootDelay;
        GameObject projectile = Instantiate(m_ProjectilePrefab, m_FirePoint.position, m_FirePoint.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(m_LookDirection.normalized * m_ProjectileSpeed, ForceMode.Impulse);
        Destroy(projectile, 2);
    }
}
