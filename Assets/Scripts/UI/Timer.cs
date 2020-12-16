using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public Action<Boolean> OnStop;

    private TMP_Text text;
    private int count;
    private float time = 0f;
    private bool playing = false;

    public void Start()
    {
        count = LevelManager.Instance.timeDuration;
        text = GetComponent<TMP_Text>();
        Write(count.ToString());
    }

    private void Update()
    {
        if (playing)
        {
            time += Time.deltaTime;
            if (time >= 1f)
            {
                count--;
                if (count < 0)
                {
                    StopTimer();
                }
                else Write(count.ToString());
                // CHangement de couleur quand peu de temps
                if (count < 40)
                {
                    text.color = Color.yellow;
                }
                if (count < 20)
                {
                    text.color = Color.red;
                }
                time = 0f;
            }
        }
        
    }

    public void StartTimer()
    {
        playing = true;
    }

    public void StopTimer()
    {
        playing = false;
        OnStop?.Invoke(true);
    }

    private void Write(string time)
    {
        text.text = "Temps restants: " + time.PadLeft(2, '0') + " s";
    }
}
