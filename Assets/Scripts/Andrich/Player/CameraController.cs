using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform m_Player;
    private Vector3 m_CameraOffset;

    void Start()
    {
        m_Player = GameObject.Find("Player").GetComponent<Transform>();
        m_CameraOffset = transform.position - m_Player.position;
    }

    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        Vector3 playerPos = m_Player.transform.position;
        transform.position = playerPos + m_CameraOffset;
    }
}
