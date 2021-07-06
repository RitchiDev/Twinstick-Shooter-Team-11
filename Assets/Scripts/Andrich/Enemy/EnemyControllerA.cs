using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class EnemyControllerA : MonoBehaviour
{
    //private Rigidbody m_Rigidbody;
    private NavMeshAgent m_NavMeshAgent;
    private EnemyVisionConeA m_VisionCone;
    private Transform m_Player;

    [Header("Movement")]
    [SerializeField] private float m_ChaseSpeed = 4;
    [SerializeField] private float m_PatrolSpeed = 3.5f;
    [SerializeField] private float m_StopDistance = 4f;
    [SerializeField] private float m_SlowDownDistance = 4f;
    [SerializeField] private GameObject[] m_PatrolPoints;
    private int m_WhichPatrolPoint = 0;

    [Header("Shooting")]
    [SerializeField] Transform m_FirePoint = null;
    [SerializeField] private float m_ShootDelay = 2;
    private float m_ShootTimer;
    private float m_ShootDelayOffset = 0.5f;

    [Header("Projectile")]
    [SerializeField] private GameObject m_ProjectilePrefab = null;
    [SerializeField] private float m_ProjectileSpeed = 20;


    private void Start()
    {
        m_ShootTimer = m_ShootDelay * m_ShootDelayOffset;
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_VisionCone = GetComponentInChildren<EnemyVisionConeA>();

        m_NavMeshAgent = GetComponent<NavMeshAgent>();

        for (int i = 0; i < m_PatrolPoints.Length; i++)
        {
            Destroy(m_PatrolPoints[i].GetComponent<MeshRenderer>());
        }

        SetPatrolPoint();
    }


    private void Update()
    {
        ChangePatrolPoint();
    }


    private void ChangePatrolPoint()
    {
        if(m_VisionCone.PlayerHasEnteredVision())
        {
            if((m_Player.position - transform.position).sqrMagnitude < m_SlowDownDistance)
            {
                if((m_Player.position - transform.position).sqrMagnitude < m_StopDistance)
                {
                    m_NavMeshAgent.speed = 0;
                }
                else
                {
                    m_NavMeshAgent.speed = 1;
                }
            }
            else
            {
                m_NavMeshAgent.speed = m_ChaseSpeed;
            }

            ShootTimer();
        }
        else
        {
            m_NavMeshAgent.speed = m_PatrolSpeed;

            if((m_PatrolPoints[m_WhichPatrolPoint].transform.position - transform.position).sqrMagnitude < 1) //SquareMagnitude is efficienter dan Vector3 Distance()
            {
                m_WhichPatrolPoint = Mathf.Clamp(m_WhichPatrolPoint + 1, 0, m_PatrolPoints.Length);

                if (m_WhichPatrolPoint >= m_PatrolPoints.Length && m_PatrolPoints.Length > 1)
                {
                    m_WhichPatrolPoint = 0;
                }
            }
        }

        SetPatrolPoint();
    }


    private void SetPatrolPoint()
    {
        Vector3 patrolPoint;
        if(m_VisionCone.PlayerHasEnteredVision())
        {
            patrolPoint = m_Player.position;
        }
        else
        {
            patrolPoint = m_PatrolPoints[m_WhichPatrolPoint].transform.position;
        }
        m_NavMeshAgent.SetDestination(patrolPoint);
    }


    private void ShootTimer()
    {
        if(m_VisionCone.RayIsHittingPlayer()) //Als de ray niet de speler raakt
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
        else
        {
            m_ShootTimer = m_ShootDelay * m_ShootDelayOffset;
        }
    }


    private void Shoot()
    {
        m_ShootTimer = m_ShootDelay;
        Vector3 shootDirection = m_Player.position - transform.position;
        GameObject projectile = Instantiate(m_ProjectilePrefab, m_FirePoint.position, m_FirePoint.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(shootDirection.normalized * m_ProjectileSpeed, ForceMode.Impulse);
        Destroy(projectile, 2);
    }
}



//if (Physics.Raycast(transform.position, directionToPlayer.normalized, Mathf.Infinity, m_DetectionLayers))
//{
//    m_PlayerIsSeen = true;
//}

//Vector3 directionToPlayer = m_Player.position - transform.position;
//RaycastHit hit = Physics.Raycast(transform.position, transform.position + directionToPlayer.normalized, m_DetectionLayers);
//Debug.DrawRay(transform.position, directionToPlayer.normalized, Color.red, Mathf.Infinity);

//private Rigidbody m_Rigidbody;
//private Transform m_PlayerTransform;
//private EnemyVisionConeA m_VisionCone;

//[Header( "Movement")]
//[SerializeField] private float m_MovementSpeed = 5;
//[SerializeField] private float m_RotateSpeed = 3;

//[SerializeField] private Transform[] m_PatrolPoints = null;
//private int m_WhichPatrolPoint;


//private void Start()
//{
//    m_PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
//    m_Rigidbody = GetComponent<Rigidbody>();
//    m_VisionCone = GetComponentInChildren<EnemyVisionConeA>();
//}

//void FixedUpdate()
//{
//    if (m_VisionCone.GetIfPlayerIsFound())
//    {
//        ChasePlayer();
//    }
//    else
//    {
//        if (Vector3.Distance(m_Rigidbody.position, m_PatrolPoints[m_WhichPatrolPoint].position) > 1)
//        {
//            MoveToPatrolPoint();
//        }
//    }

//    if (Vector3.Distance(m_Rigidbody.position, m_PatrolPoints[m_WhichPatrolPoint].position) < 1)
//    {
//        m_WhichPatrolPoint++;
//        if (m_WhichPatrolPoint >= m_PatrolPoints.Length)
//        {
//            m_WhichPatrolPoint = 0;
//        }
//    }
//}

//private void MoveToPatrolPoint()
//{
//    Vector3 moveDirection = (m_PatrolPoints[m_WhichPatrolPoint].position - m_Rigidbody.position).normalized;
//    moveDirection.y = 0;
//    m_Rigidbody.MovePosition(m_Rigidbody.position + moveDirection * m_MovementSpeed * Time.fixedDeltaTime);

//    Vector3 lookDirection = m_PatrolPoints[m_WhichPatrolPoint].position - m_Rigidbody.position;
//    lookDirection.y = 0;
//    Vector3 newLookDirection = Vector3.RotateTowards(m_Rigidbody.transform.forward, lookDirection, m_RotateSpeed * Time.fixedDeltaTime, 0.0f);
//    m_Rigidbody.transform.rotation = Quaternion.LookRotation(newLookDirection);
//}

//private void ChasePlayer()
//{
//    Vector3 moveDirection = (m_PlayerTransform.position - m_Rigidbody.position).normalized;
//    moveDirection.y = 0;
//    m_Rigidbody.MovePosition(m_Rigidbody.position + moveDirection * m_MovementSpeed * Time.fixedDeltaTime);

//    Vector3 lookDirection = m_PlayerTransform.position - m_Rigidbody.position;
//    lookDirection.y = 0;
//    Vector3 newLookDirection = Vector3.RotateTowards(m_Rigidbody.transform.forward, lookDirection, m_RotateSpeed + 5 * Time.fixedDeltaTime, 0.0f);
//    m_Rigidbody.transform.rotation = Quaternion.LookRotation(newLookDirection);
//if (Vector3.Distance(m_Rigidbody.position, m_PatrolPoints[m_WhichPatrolPoint].transform.position) < 1)
