using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject Credits;

    private void Start()
    {
        Credits.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void GameCredits()
    {
        Credits.SetActive(true);
    }

    public void BackButton()
    {
        Credits.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
