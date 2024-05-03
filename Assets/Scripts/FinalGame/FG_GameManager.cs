using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


using UnityEngine;


public class FG_GameManager : MonoBehaviour
{
    private bool gameActive = false;
    public float score =0;
    public Transform spawnPoint;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
        Time.timeScale = 0;
        DeactivatePauseScreen();
        //find the Canvas and its child Panel and set it to false
    

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))  // KeyCode.Return is the Enter key on most keyboards
            {
                DeactivateStartScreen();
            }

       if (Input.GetKeyDown(KeyCode.Escape))
        {
            DeactivatePauseScreen();
            SetGameActive(!gameActive);
        }
    }

    public void AddScore(float scoreToAdd)
    {
        score += scoreToAdd;
    }
    public void resetScore(){
        score = 0;
    }

    // if gameActive is false, the game should be paused
    public void SetGameActive(bool active)
    {
        gameActive = active;
        Time.timeScale = active ? 1 : 0;
    }

    void DeactivateStartScreen()
    {
        SetGameActive(true);
        // Find the ScreensCanvas GameObject
        GameObject screensCanvas = GameObject.Find("ScreensCanvas");

        // Check if the ScreensCanvas is found
        if (screensCanvas != null)
        {
            // Get all child components, but only activate those that are inactive
            foreach (Transform child in screensCanvas.transform)
            {
                // if (!child.gameObject.activeSelf)
                // {
                    child.gameObject.SetActive(false);
                // }
            }
        }
        else
        {
            Debug.LogError("ScreensCanvas not found in the scene.");
        }
    }

    void DeactivatePauseScreen()
    {
        
        // Find the ScreensCanvas GameObject
        GameObject pauseCanvas = GameObject.Find("PauseCanvas");

        // Check if the ScreensCanvas is found
        if (pauseCanvas != null)
        {
            // Get all child components, but only activate those that are inactive
            foreach (Transform child in pauseCanvas.transform)
            {
                // if (!child.gameObject.activeSelf)
                // {
                    child.gameObject.SetActive(!child.gameObject.activeSelf);
                // }
            }
        }
        else
        {
            Debug.LogError("PausesCanvas not found in the scene.");
        }
    }

    public void ActivateEndScreen()
    {
        // Find the ScreensCanvas GameObject
        GameObject endCanvas = GameObject.Find("EndCanvas");

        // Check if the ScreensCanvas is found
        if (endCanvas != null)
        {
            // Get all child components, but only activate those that are inactive
            foreach (Transform child in endCanvas.transform)
            {
                if (!child.gameObject.activeSelf)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            Debug.LogError("EndCanvas not found in the scene.");
        }
    }

      public void ActivateWinScreen()
    {
        // Find the ScreensCanvas GameObject
        GameObject winCanvas = GameObject.Find("WinCanvas");

        // Check if the ScreensCanvas is found
        if (winCanvas != null)
        {
            // Get all child components, but only activate those that are inactive
            foreach (Transform child in winCanvas.transform)
            {
                if (!child.gameObject.activeSelf)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            Debug.LogError("WinCanvas not found in the scene.");
        }
    }
   
    

   
}
