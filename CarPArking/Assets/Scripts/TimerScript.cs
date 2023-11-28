using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{

    public float Currenttime = 180f;
    public Text TimerCountText;
    public GameObject TimeEndPanel;
    // Start is called before the first frame update

    private void Update()
    {
        StartCOunt();
    }

    public void StartCOunt()
    {
        if(Currenttime > 0)
        {
            Currenttime -= Time.deltaTime;
            UpdateTime(Currenttime);

        }else
        {
            Console.WriteLine("GAmeOver");
            TimeEndPanel.SetActive(true);
            Time.timeScale = 0f;

        }
    }


    private void UpdateTime(float time)
    {
        int minitue = Mathf.FloorToInt(time / 60);
        int second = Mathf.FloorToInt(time % 60);

        TimerCountText.text = minitue + " : " + second;
    }


}
