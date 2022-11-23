using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unitClick : MonoBehaviour
{
    private GameObject unitGO;
    Tile tile;
    
    void Start()
    {
        
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
                }
			}
		}



        switch (unitGO.tag)
        {
            case "Infantery":
                var unitI = unitGO.GetComponent<infantry>();
                Tile tile = unitI.tile.GetComponent<Tile>();

                break;

            case "Tank":
                var unitT = unitGO.GetComponent<heavy>();
                break;

            case "Ranged":
                var unitR = unitGO.GetComponent<ranged>();
                break;

            default:
                break;
        }

    }
}
