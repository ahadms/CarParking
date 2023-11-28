using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{
    private int currentLevelIndex;

    void Start()
    {
        // Get the index of the current active scene (level)
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Call this function to load the next level
    public void LoadNextLevel()
    {
        // Load the next level in the build index
        SceneManager.LoadScene(currentLevelIndex + 1);
        Time.timeScale = 1.0f;

        // If the next scene is the last scene, loop back to the first scene
        if (currentLevelIndex + 1 >= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
    }
}
