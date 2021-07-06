using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_PauseMenu;
    [SerializeField] private KeyCode m_PauseButton = KeyCode.Escape;
    private bool m_GameIsPaused;
    private float m_PauseInput;

    private void Start()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        m_PauseMenu.SetActive(false);
        m_GameIsPaused = false;
    }

    private void Update()
    {
        if (m_PauseInput != 0)
        {
            if(m_GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        m_PauseMenu.SetActive(true);
        m_GameIsPaused = true;
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public void Resume()
    {
        m_PauseMenu.SetActive(false);
        m_GameIsPaused = false;
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void Restart()
    {
        m_PauseMenu.SetActive(false);
        m_GameIsPaused = false;
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Pakt de nummer van de actieve scene (.name kan ook)
    }

    public void ReturnToTitlescreen()
    {
        m_PauseMenu.SetActive(false);
        m_GameIsPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0); //Main Menu
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GetPauseInput(InputAction.CallbackContext context)
    {
        m_PauseInput = context.ReadValue<float>();
    }
}
