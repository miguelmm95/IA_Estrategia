using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagManagement : MonoBehaviour
{
    private List<Tile> neighbours;
    private BaseUnit unit;
    private int turno;

    // Start is called before the first frame update
    void Start()
    {
        neighbours = Grid.Instance.GetNeighboursUnit(this.transform.parent.GetComponent<Tile>());
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Tile n in neighbours)
        {
            if (n.occupiedUnit != null)
            {
                unit = n.occupiedUnit;
                break;
            }
            else
            {
                unit = null;
            }
        }

        switch (this.tag)
        {
            case "Bandera_Jugador":
                if (turno == 3 && unit.player == Player.AI)
                {
                    this.transform.parent.GetComponent<Tile>().DisableBanderaJugador();
                    this.transform.parent.GetComponent<Tile>().SetBanderaIA();
                    turno = 0;
                }
                break;

            case "Bandera_IA":
                if (turno == 3 && unit.player == Player.Human)
                {
                    this.transform.parent.GetComponent<Tile>().DisableBanderaIA();
                    this.transform.parent.GetComponent<Tile>().SetBanderaJugador();
                    turno = 0;
                }
                break;

            default:
                break;
        }
    }


}
