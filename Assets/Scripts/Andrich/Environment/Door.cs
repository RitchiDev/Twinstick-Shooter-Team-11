using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator m_Animation;

    private void Start()
    {
        m_Animation = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        m_Animation.SetBool("Close", false);
        m_Animation.SetBool("Open", true);
    }

    public void CloseDoor()
    {
        m_Animation.SetBool("Open", false);
        m_Animation.SetBool("Close", true);
    }
}
