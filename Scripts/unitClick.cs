using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitClick : MonoBehaviour
{
    private GameObject unitGO;
    Tile tile;
    private bool click;
    private Grid gridTiles;
    private List<Tile> neighbours;
    private List<Tile> neighbours1;
    //[SerializeField] private Tile nada;
    void Start()
    {
        GameObject gm = GameObject.FindWithTag("Grid");
        gridTiles = gm.GetComponent<Grid>();
        neighbours1 = new List<Tile>();
        //neighbours1.Add(nada);
        click = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
            
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if (hit.collider != null)
			{
                if (hit.transform.gameObject.tag == "Infantery" || 
                    hit.transform.gameObject.tag == "Tank" || 
                    hit.transform.gameObject.tag == "Ranged")
                {
                    unitGO = hit.transform.gameObject;
                    click = true;
                }
                else if (hit.transform.gameObject.tag == "Tile_Walkable" || hit.transform.gameObject.tag == "Tile_Unwalkable")
                {
                    foreach (Tile n in neighbours1)
                    {
                        if (n == null)
                        {
                            continue;
                        }
                        else
                        {
                            n.DisableUnitHighlight();
                            n.unitCanMoveTo = false;
                        }
                    }
                }
  
                
			}
		}

        if (click)
        {
            switch (unitGO.tag)
            {
                case "Infantery":
                    var unitI = unitGO.GetComponent<infantry>();
                    Tile tile = unitI.tile.GetComponent<Tile>();
                    neighbours = gridTiles.GetNeighboursUnit(tile, unitI.maxRange);
                    foreach (Tile n in neighbours1)
                    {
                        if (n == null)
                        {
                            continue;
                        }
                        else
                        {
                            n.DisableUnitHighlight();
                            n.unitCanMoveTo = false;
                        }
                    }
                    foreach (Tile n in neighbours)
                    {
                        if (n == null)
                        {
                            continue;
                        }
                        else
                        {
                            n.SetUnitHighlight();
                            n.unitCanMoveTo = true;
                        }
                    }
                    neighbours1 = neighbours;
                    break;

                case "Tank":
                    var unitT = unitGO.GetComponent<heavy>();
                    tile = unitT.tile.GetComponent<Tile>();
                    neighbours = gridTiles.GetNeighboursUnit(tile, unitT.maxRange);
                    foreach (Tile n in neighbours1)
                    {
                        if (n == null)
                        {
                            continue;
                        }
                        else
                        {
                            n.DisableUnitHighlight();
                            n.unitCanMoveTo = false;
                        }
                    }
                    foreach (Tile n in neighbours)
                    {
                        if (n == null)
                        {
                            continue;
                        }
                        else
                        {
                            n.SetUnitHighlight();
                            n.unitCanMoveTo = true;
                        }
                    }
                    neighbours1 = neighbours;
                    break;

                case "Ranged":
                    var unitR = unitGO.GetComponent<ranged>();
                    tile = unitR.tile.GetComponent<Tile>();
                    neighbours = gridTiles.GetNeighboursUnit(tile, unitR.maxRange);
                    foreach (Tile n in neighbours1)
                    {
                        if (n == null)
                        {
                            continue;
                        }
                        else
                        {
                            n.DisableUnitHighlight();
                            n.unitCanMoveTo = false;
                        }
                    }
                    foreach (Tile n in neighbours)
                    {
                        if (n == null)
                        {
                            continue;
                        }
                        else
                        {
                            n.SetUnitHighlight();
                            n.unitCanMoveTo = true;
                        }
                    }
                    neighbours1 = neighbours;
                    break;

                default:
                    break;
            }
            click = false;
        }

    }
}
