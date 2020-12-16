using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeTile
{
    Cow,
    Duck,
    Pig,
    Horse,
    Chick
}
public class Tile : MonoBehaviour
{

    public TypeTile type;

    private List<GameObject> collisionedTiles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsNeighbours(GameObject other)
    {
        return collisionedTiles.Contains(other);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.GetComponent<Tile>() != null)
        {
            collisionedTiles.Add(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Tile>() != null)
        {
            if (IsNeighbours(col.gameObject)) collisionedTiles.Remove(col.gameObject);
        }
    }
}
