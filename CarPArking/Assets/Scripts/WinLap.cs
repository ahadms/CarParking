using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLap : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject winpanel;
    public AudioSource audioSourcehigh;
    public AudioSource audioSourcelow;
    public Texture newAlbedoTexture; // The new albedo texture you want to apply to the object


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WinLap"))
        {
            UnlockNewLevel();
            StartCoroutine(WinPanelActivation());

            // Change the albedo texture of the collided object
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.mainTexture = newAlbedoTexture;
            }
        }
    }

    private IEnumerator WinPanelActivation() {

       

        yield return new WaitForSeconds(2f);

        Debug.Log("Youwin");
        winpanel.SetActive(true);
        audioSourcehigh.Pause();
        audioSourcelow.Pause();
        Time.timeScale = 0f;
    }

    void UnlockNewLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex",SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlokedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}
