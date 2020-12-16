using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Composants utiles
    public GameObject EndPanel;
    public Timer timer;
    public ScoreBoard score;


    // Données du jeu
    private Color selectedColor = new Color(0.3f, 0.6f, 0.6f);
    private bool locked = false;
    private GameObject lastTile;
    private List<GameObject> selectedTiles = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        EndPanel.SetActive(false);
        setLocked(true);
        StartCoroutine("Instaciation", LevelManager.Instance.nbTiles);

    }

    IEnumerator Instaciation(int nb)
    {
        for (int i = 0; i < nb ; i++)
        {
            LevelManager.Instance.SpawnTile();
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(.4f);

        // Ca commence
        setLocked(false);
        timer.StartTimer();
        timer.OnStop += End;
        
    }

    public void setLocked(bool b)
    {
        locked = b;
    }

    // Update is called once per frame
    void Update()
    {
        if (locked) return;

        if (Input.GetMouseButton(0))
        {
            if (lastTile == null) OnDragStart();
            else OnDragging();
        }
        else if (Input.GetMouseButtonUp(0) && lastTile != null)
        {
            OnDragEnd();
        }
    }

    void End(bool fini)
    {
        if (fini)
        {
            setLocked(true);
            EndPanel.SetActive(true);
            EndPanel.GetComponent<EndMenu>().Show(score.GetScore());
        }
        
    }

    GameObject GetTileTouched()
    {
        // On récupère l'objet à la position de la souris
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);


        if (hit.collider != null) // Si on touche un objet
        {
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.GetComponent<Tile>() != null) // Si l'objet est une tuile
            {
                return hitObject; // On retourne la tuile
            }
        }
        return null;
    }

    void OnDragStart()
    {
        GameObject tile = GetTileTouched();
        if (tile != null)
        {
            tile.GetComponent<SpriteRenderer>().color = selectedColor;
            lastTile = tile;
            selectedTiles.Add(tile);
        }
    }

    void OnDragging()
    {
        GameObject tile = GetTileTouched();

        if (tile != null)
        {
            if (selectedTiles.Contains(tile))
            {
                if(selectedTiles.IndexOf(tile) == (selectedTiles.Count - 2) )
                {
                    selectedTiles[selectedTiles.Count - 1].GetComponent<SpriteRenderer>().color = Color.white;
                    selectedTiles.RemoveAt(selectedTiles.Count-1);
                }
            }
            else if (lastTile.GetComponent<Tile>().type == tile.GetComponent<Tile>().type && lastTile.GetComponent<Tile>().IsNeighbours(tile))
            {
                tile.GetComponent<SpriteRenderer>().color = selectedColor;
                lastTile = tile;
                selectedTiles.Add(tile);
            }
        }
    }

    void OnDragEnd()
    {
        int size = selectedTiles.Count;
        if ( size >= 3) // Suppression des tuiles
        {
            setLocked(true);
            for(int i = 0; i < size; i++)
            {
                GameObject tile = selectedTiles[i];
                tile.transform.DOScale(0.0f, 0.1f).SetDelay(0.1f * i);
                Destroy(tile, (0.1f * i)+0.1f);
            }
            OnAllDelete(size);
        }
        else // Deselection des tuiles
        {
            for (int i = 0; i < size; i++)
            {
                selectedTiles[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        lastTile = null;
        selectedTiles.Clear();
    }

    void OnAllDelete(int nb)
    {
        StartCoroutine("Instaciation", nb);
        score.SetScore(nb);
    }
}
