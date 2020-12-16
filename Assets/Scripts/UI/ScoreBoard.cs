using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    private int score;
    private bool bonusActif;
    private TMP_Text text;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        time = 0f;
        bonusActif = false;
        text = GetComponent<TMP_Text>();
        text.text = "Scores : " + score.ToString().PadLeft(5, '0');
    }

    void Update()
    {
        if (bonusActif)
        {
            time += Time.deltaTime;
            if(time >= LevelManager.Instance.durationBonus)
            {
                bonusActif = false;
            }
        }
    }


    public void SetScore(int nb)
    {
        // Si 5 ou plus = bonus
        if (nb >= 5)
        {
            bonusActif = true;
            time = 0f;
        }

        // Calcul des points
        int points = LevelManager.Instance.ptPerLine + (LevelManager.Instance.ptPerTile * nb);
        if (bonusActif) points = (int)Mathf.Round(points * LevelManager.Instance.multiplicateur);

        // Ajouts des points
        score += points;
        text.text = "Scores : " + score.ToString().PadLeft(5, '0');
    }

    public int GetScore()
    {
        return score;
    }
}
