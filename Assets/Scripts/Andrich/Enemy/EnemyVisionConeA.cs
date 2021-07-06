using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyVisionConeA : MonoBehaviour
{
    [SerializeField] private Transform m_Enemy;
    private Transform m_Player;
    private bool m_RayHitPlayer;
    [SerializeField] private LayerMask m_DetectionLayers;
    [SerializeField] private int m_DetectionRange = 10;


    [SerializeField] private float m_TimeBeforePlayerIsLost = 10;
    private float m_LostTimer;

    private bool m_PlayerEnteredVision;


    private void Start()
    {
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //m_Enemy = GetComponentInParent<Transform>();
    }

    private void Update()
    {
        if(m_LostTimer > 0)
        {
            m_LostTimer -= Time.deltaTime;
            m_PlayerEnteredVision = true;
        }
        else if(m_LostTimer < 0)
        {
            m_PlayerEnteredVision = false;
        }
    }

    private void FixedUpdate()
    {
        Ray direction = new Ray();
        direction.origin = m_Enemy.position;
        direction.direction = m_Player.position - m_Enemy.position;

        if(Physics.Raycast(direction, out RaycastHit hit, m_DetectionRange, m_DetectionLayers))
        {
            if(hit.collider != null)
            {
                Debug.DrawLine(direction.origin, hit.point, Color.red);
                if (hit.collider.CompareTag("Player"))
                {
                    m_RayHitPlayer = true;
                }
                else
                {
                    m_RayHitPlayer = false;
                }
            }
        }
    }

    public bool PlayerHasEnteredVision()
    {
        return m_PlayerEnteredVision;
    }

    public bool RayIsHittingPlayer()
    {
        return m_RayHitPlayer;
    }

    private void OnTriggerStay(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(m_RayHitPlayer)
            {
                m_LostTimer = m_TimeBeforePlayerIsLost;
            }
        }
        else if(collision.CompareTag("Projectile"))
        {
            m_LostTimer = m_TimeBeforePlayerIsLost;
        }
    }
}
