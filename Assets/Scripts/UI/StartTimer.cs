using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartTimer : MonoBehaviour
{

    private TMP_Text text;
    private float time = 0f;
    private int count = 3;

    public void Start()
    {
        text = GetComponent<TMP_Text>();
        Write(count.ToString());
        
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 1.4f)
        {
            count--;
            if (count == 0) Write("GO");
            else if (count < 0)
            {
                Destroy(gameObject);
            }
            else Write(count.ToString());
            time = 0f;
        }
    }

    private void Write(string texte)
    {
        text.text = texte;
    }


}

