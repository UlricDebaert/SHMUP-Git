using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    TextMeshProUGUI healthText;
    TextMeshProUGUI scoreText;

    Canvas pauseMenu;
    bool paused;

    //placeholder
    public int healthPoint;
    public int scorePoint;

    public int index;
    public int menuIndex;

    private void Start()
    {
        healthText = GameObject.FindGameObjectWithTag("healthText").GetComponent<TextMeshProUGUI>();
        scoreText = GameObject.FindGameObjectWithTag("scoreText").GetComponent<TextMeshProUGUI>();

        pauseMenu = GameObject.FindGameObjectWithTag("pauseMenu").GetComponent<Canvas>();
    }

    private void Update()
    {
        PV();
        Score();

        if(Input.GetButtonDown("Cancel") && paused == false)
        {
            Pause();
        }
        else if (Input.GetButtonDown("Cancel") && paused == true)
        {
            Resume();
        }
    }

    void PV()
    {
        healthText.text = healthPoint.ToString();

        if(healthPoint > 99)
        {
            healthText.text =  "99+";
        }
    }

    void Score()
    {
        scoreText.text = scorePoint.ToString();
    }

    public void Play()
    {
        SceneManager.LoadScene(index);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.enabled = true;
        paused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.enabled = false;
        paused = false;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(menuIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

