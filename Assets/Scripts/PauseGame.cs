using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public bool isPaused;
    public GameObject mainHUD;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!isPaused)
            {
                isPaused = true;
                Pause();
            }
            else
            {
                isPaused = false;
                Resume();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        mainHUD.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        mainHUD.SetActive(true);
        pauseMenu.SetActive(false);
    }
}
