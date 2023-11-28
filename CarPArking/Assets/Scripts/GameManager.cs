using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Your existing GameManager code goes here...

    public void PlayAgain()
    {
        // Get the current active scene's build index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Reload the current scene using its build index
        SceneManager.LoadScene(currentSceneIndex);

        // Reload the current scene
        //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
