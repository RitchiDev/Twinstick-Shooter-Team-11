using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static int m_ShotsFired;
    private static int m_ShotsHit;
    private static float m_Score;
    private static int m_TimesPlayed;
    private static string m_DeathMessage = "How did you die?";

    private void Start()
    {
        //DontDestroyOnLoad(gameObject);
        m_TimesPlayed++;
        Debug.Log("DeathMessage: " + m_DeathMessage);
        Debug.Log("Times Played: " + m_TimesPlayed.ToString());
        Debug.Log("Accuracy: " + m_ShotsHit.ToString() + "/" + m_ShotsFired.ToString());
        Debug.Log("Score: " + m_Score.ToString());
    }

    public void ChangeShotHitAmount(int amount)
    {
        m_ShotsHit += amount;
    }

    public void ChangeShotsFiredAmount(int amount)
    {
        m_ShotsFired += amount;
    }

    public void ChangeScore(float amount)
    {
        m_Score = Mathf.Clamp(m_Score + amount, 0, 10000000000000000000);
        Debug.Log(m_Score);
    }

    public void ChangeDeathMessage(string text)
    {
        m_DeathMessage = text;
    }

    public float GetScore()
    {
        return m_Score;
    }

    public int GetTimesPlayed()
    {
        return m_TimesPlayed;
    }

    public int GetShotsHit()
    {
        return m_ShotsHit;
    }

    public string GetDeathMessage()
    {
        return m_DeathMessage;
    }

}
