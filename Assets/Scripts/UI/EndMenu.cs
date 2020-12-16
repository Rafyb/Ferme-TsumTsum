using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{

    public TMP_Text textName;
    public TMP_Text textScore;
    public TMP_Text textVictory;

    public GameObject btnReplay;
    public GameObject btnNext;

    public Image starOne;
    public Image starTwo;
    public Image starThree;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(int score)
    {
        textName.text = LevelManager.Instance.levelName;

        if(score < LevelManager.Instance.scoreOneStar)
        {
            // Lose
            textVictory.text = "Défaite";
            textVictory.color = Color.red;
            btnNext.SetActive(false);

        } else
        {
            // Win
            textVictory.text = "Victoire";
            textVictory.color = Color.green;
        }

        int scoreManquant = 0;

        if (score >= LevelManager.Instance.scoreThreeStar) starThree.color = Color.white;
        else scoreManquant = LevelManager.Instance.scoreThreeStar - score;
        if (score >= LevelManager.Instance.scoreTwoStar) starTwo.color = Color.white;
        else scoreManquant = LevelManager.Instance.scoreTwoStar - score;
        if (score >= LevelManager.Instance.scoreOneStar)starOne.color = Color.white;
        else scoreManquant = LevelManager.Instance.scoreOneStar - score;

        textScore.text = "Score : " + score.ToString();
        if (scoreManquant > 0) textScore.text += "\nManquant pour l'étoile suivant : " + scoreManquant.ToString();


        btnReplay.GetComponent<Button>().onClick.AddListener(LevelManager.Instance.Restart);
        btnNext.GetComponent<Button>().onClick.AddListener(LevelManager.Instance.NextLevel);
    }


}
