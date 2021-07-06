using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f; //for instance
    private Transform target;
    private Rigidbody rb;
    private bool m_PlayerIsFound;
    private float m_ShootTimer;
    [SerializeField] private float m_ShootDelay = 2;
    [SerializeField] Transform m_FirePoint;
    [SerializeField] private float m_ProjectileSpeed;
    [SerializeField] private GameObject m_ProjectilePrefab;

    private float waitTime;
    public float startWaitTime;

    [SerializeField] private Transform[] moveSpots; //maakt een array voor alle movespots.
    private int randomSpot; //kiest een willekeurige movespot.


    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);

        m_ShootTimer = m_ShootDelay;
    }

    private void Update()
    {
        ShootTimer();
        PatrolTimer();
    }

    void FixedUpdate()
    {
        if (m_PlayerIsFound)
        {
            ChasePlayer();
            
            if (m_ShootTimer < 0)
            {
                Shoot();
            }
        }
        else
        {
            if (Vector3.Distance(rb.position, moveSpots[randomSpot].position) > 1f)
            {
                Patrol();
            } 
        }
    }

    private void PatrolTimer()
    {
        if (Vector3.Distance(rb.position, moveSpots[randomSpot].position) < 1f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    private void Patrol()
    {
        //rb.transform.position = Vector3.MoveTowards(rb.transform.position, moveSpots[randomSpot].position, movementSpeed * Time.deltaTime);
        
        Vector3 direction = (moveSpots[randomSpot].position - rb.position).normalized;
        rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime);

        rb.MoveRotation(Quaternion.LookRotation(direction, Vector3.up));
    }

    private void ChasePlayer()
    {
        Vector3 relativePos = target.position - rb.position;
        Vector3 direction = (target.position - rb.position).normalized;

        rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(Quaternion.LookRotation(relativePos, Vector3.up));
    }

    private void ShootTimer()
    {
        if(m_PlayerIsFound)
        {
            if (m_ShootTimer > 0)
            {
                m_ShootTimer -= Time.deltaTime;
            }
        }
    }

    private void Shoot()
    {
        m_ShootTimer = m_ShootDelay;
        GameObject projectile = Instantiate(m_ProjectilePrefab, m_FirePoint.position, m_FirePoint.rotation);
        projectile.GetComponent<Rigidbody>().AddForce((target.position - rb.position).normalized * m_ProjectileSpeed, ForceMode.Impulse);
        Destroy(projectile, 2);
    }

    public void CheckForPlayer(bool checker)
    {
        m_PlayerIsFound = checker;
    }
}
