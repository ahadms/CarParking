using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalMessage : MonoBehaviour
{
    public GameObject FianlMessage;
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FinalMessageDeactive());
    }

    private IEnumerator FinalMessageDeactive()
    {
      yield return new  WaitForSeconds(3f);

        FianlMessage.SetActive(false);
    }
}
