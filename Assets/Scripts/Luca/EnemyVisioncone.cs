using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisioncone : MonoBehaviour
{ 
    private EnemyController m_EnemyController;
    private bool CheckColliding;

    private void Start()
    {
        CheckColliding = false;
        m_EnemyController = GetComponentInParent<EnemyController>();
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        CheckColliding = true;
    //        m_EnemyController.AllowMovement(CheckColliding);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CheckColliding = true;
            m_EnemyController.CheckForPlayer(CheckColliding);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CheckColliding = false;
            m_EnemyController.CheckForPlayer(CheckColliding);
        }
    }
}
