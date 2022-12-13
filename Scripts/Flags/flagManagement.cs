using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagManagement : MonoBehaviour
{
    private List<Tile> neighbours;
    private Tile tileFlag;
    private BaseUnit unit;
    private int turno;
    public GameObject bandera_IA;
    public GameObject bandera_Jugador;
    private Transform positionFlag;

    // Start is called before the first frame update
    void Start()
    {
        positionFlag = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        neighbours = Grid.Instance.GetNeighboursUnit(tileFlag);
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

                    turno = 0;
                    Instantiate(bandera_IA, positionFlag.position + new Vector3(0, 0, 2), Quaternion.identity);
                    Destroy(this);    
                }
                break;

            case "Bandera_IA":
                if (turno == 3 && unit.player == Player.Human)
                {
                    turno = 0;
                    Instantiate(bandera_Jugador, positionFlag.position + new Vector3(0, 0, 2), Quaternion.identity);
                    Destroy(this);
                }
                break;

            default:
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Tile_Walkable")
        {
            tileFlag = collision.transform.gameObject.GetComponent<Tile>();
        }
    }

}
