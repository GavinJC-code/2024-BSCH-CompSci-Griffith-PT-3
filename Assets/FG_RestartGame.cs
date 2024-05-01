using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FG_RestartGame : MonoBehaviour
{
    private FG_GameManager gameManager;

    public void RestartGame()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FG_GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene.");
        }

        gameManager.resetScore();

        // Reloads the current scene
        int sceneIndex = SceneManager.GetActiveScene().buildIndex; // Gets the active scene index
        SceneManager.LoadScene(sceneIndex); // Reloads the scene by index
    }
}