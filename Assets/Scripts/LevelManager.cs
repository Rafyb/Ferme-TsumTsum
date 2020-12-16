using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Level Settings")]
    public string levelName;
    public int timeDuration = 90;

    [Header("Objectif Settings")]
    public int scoreOneStar;
    public int scoreTwoStar;
    public int scoreThreeStar;

    [Header("Spawn Settings")]
    public int nbTiles;
    public GameObject[] prefabs;
    public float[] tauxSpawn;
    public Transform spawnZoneMin;
    public Transform spawnZoneMax;

    [Header("Scores Settings")]
    public int ptPerTile = 50;
    public int ptPerLine = 100;
    public float multiplicateur = 2f;
    public float durationBonus = 5f;

    static public LevelManager Instance;


    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTile()
    {
        float rnd = 0f;
        float taux = 0f;
        int idx = 0;

        rnd = Random.Range(0.01f,100f);
        for(int i = 0; i < tauxSpawn.Length; i++)
        {
            taux += tauxSpawn[i];
            //Debug.Log("Random : "+rnd.ToString()+"| Taux actuel : "+taux);
            if (rnd <= taux)
            {
                idx = i % prefabs.Length;
                break;
            }
            
        }

        GameObject random_prefab = prefabs[idx];

        float posX = Random.Range(spawnZoneMin.position.x, spawnZoneMax.position.x);
        float posY = Random.Range(spawnZoneMin.position.y, spawnZoneMax.position.y);
        Vector3 pos = new Vector3(posX, posY, 0.0f);

        Instantiate(random_prefab, pos, Quaternion.identity);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
