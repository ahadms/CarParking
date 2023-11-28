using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
   public Rigidbody rb;
   public GameObject panel;
    public AudioSource audioSourcehigh;
    public AudioSource audioSourcelow;


    private void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("GAmeOVER");
            panel.SetActive(true);
            audioSourcehigh.Pause();
            audioSourcelow.Pause();
            Time.timeScale = 0f;

        }
    }
}
