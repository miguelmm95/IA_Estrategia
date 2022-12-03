using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagManagement : MonoBehaviour
{
    private GameObject gm;
    private Grid gridTiles;
    private List<Tile> neighbours;
    private GameObject unit;
    private int turno;
    private bool capturado;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gm = GameObject.FindWithTag("Grid");
        gridTiles = gm.GetComponent<Grid>();
        neighbours = gridTiles.GetNeighboursUnit(this.transform.parent.GetComponent<Tile>());
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Tile n in neighbours)
        {
            if (n.unitStay)
            {
                capturado = true;
                unit = n.unit; 
            }
        }

        switch (this.tag)
        {
            case "Bandera_Jugador":
                if (turno == 3)
                {

                }
                break;

            case "Bandera_IA":

                break;

            default:
                break;
        }
    }


}
