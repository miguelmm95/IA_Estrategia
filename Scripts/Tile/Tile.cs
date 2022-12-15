using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _highlightAtack;
    [SerializeField] private GameObject _unitColor;
    [SerializeField] private GameObject _banderaIA;
    [SerializeField] private GameObject _banderaJugador;
    [SerializeField] private GameObject _nonWalkable;
    [SerializeField] private BaseUnit Melee;
    [SerializeField] private BaseUnit Tank;
    [SerializeField] private BaseUnit Ranged;
    [HideInInspector] public int posX;
    [HideInInspector] public int posY;
    [HideInInspector] public bool unitStay = false;


    public bool unitCanMoveTo = false;
    public bool isWalkeable;
    public bool hasAFlag = false;
    private int maxRange;
    private static Tile lastTile;

    public BaseUnit occupiedUnit;
    public GameObject unit;

    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
    }

    private void OnMouseEnter()
    {
        _highlight.SetActive(true); 
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    

    private void Update()
    {
        if (occupiedUnit != null)
        {
            unitStay = true;
        }     
    }

    public void OnMouseDown(){

        if(GameManager.Instance.State != GameState.PlayerTurn) return;
        if (occupiedUnit != null){
            lastTile = this;
            if (occupiedUnit.player == Player.Human){

                if(occupiedUnit.state == State.humanUnselected)
                {
                    UnitManager.Instance.SelectedHero((BaseHumanUnit)occupiedUnit);
                    UnitManager.selectedHumanUnit.UnitHighlightDisable(UnitManager.vecinosAntiguos);
                    UnitManager.selectedHumanUnit.state = State.humanSelected;
                    UnitManager.vecinosAntiguos.Clear();

                    UnitManager.vecinosAntiguos = UnitManager.selectedHumanUnit.UnitHighlight(occupiedUnit.GetMaxRange(occupiedUnit.type), this);
                    //Debug.Log(occupiedUnit);
                }

                if(occupiedUnit.state == State.humanMoved)
                {
                    UnitManager.Instance.SelectedHero((BaseHumanUnit)occupiedUnit);
                    UnitManager.selectedHumanUnit.UnitHighlightDisableAtack(UnitManager.vecinosAntiguos);
                    UnitManager.selectedHumanUnit.state = State.humanAttacking;
                    UnitManager.vecinosAntiguos.Clear();

                    UnitManager.vecinosAntiguos = UnitManager.selectedHumanUnit.UnitHighlightAtack(occupiedUnit.GetRangeAtack(occupiedUnit.type), this);
                }

            }
            else
            {
                if(UnitManager.selectedHumanUnit != null && UnitManager.selectedHumanUnit.state == State.humanAttacking)
                {
                    //Ataque
                    UnitManager.selectedHumanUnit.Attack(UnitManager.selectedHumanUnit.damage, occupiedUnit.GetComponent<BaseAIUnit>());
                    
                    if (occupiedUnit.GetComponent<BaseAIUnit>().actualHealth <=0)
                    {
                        UnitManager._AIUnitsObjects.Remove(occupiedUnit.GetComponent<BaseAIUnit>());
                        Destroy(occupiedUnit.gameObject);
                    }
                }
                UnitManager.selectedHumanUnit.state = State.humanWaiting;
                UnitManager.selectedHumanUnit.UnitHighlightDisableAtack(UnitManager.vecinosAntiguos);
            }
        }
        else
        {
            if (unitCanMoveTo)
            {
                UnitManager.selectedHumanUnit.occupiedTile = this;
                UnitManager.selectedHumanUnit.state = State.humanMoved;
                this.occupiedUnit = UnitManager.selectedHumanUnit;
                lastTile.occupiedUnit.transform.position = this.transform.position;
                lastTile.occupiedUnit = null;
            }

            switch (DisplayUnit.SelectedUnit)
            {
                case "melee":
                    if (UnitManager._playerUnits.Contains("melee") && this.isWalkeable)
                    {
                        Instantiate(Melee, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
                        UnitManager._playerUnits.Remove("melee");
                        this.occupiedUnit = Melee;
                        this.occupiedUnit.player = Player.Human;
                        this.occupiedUnit.type = Type.Infantry;
                    }
                    break;
                case "tank":
                    if (UnitManager._playerUnits.Contains("tank") && this.isWalkeable)
                    {
                        Instantiate(Tank, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
                        UnitManager._playerUnits.Remove("tank");
                        this.occupiedUnit = Tank;
                        this.occupiedUnit.player = Player.Human;
                        this.occupiedUnit.type = Type.Heavy;
                    }
                    break;
                case "ranged":
                    if (UnitManager._playerUnits.Contains("ranged") && this.isWalkeable)
                    {
                        Instantiate(Ranged, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
                        UnitManager._playerUnits.Remove("ranged");
                        this.occupiedUnit = Ranged;
                        this.occupiedUnit.player = Player.Human;
                        this.occupiedUnit.type = Type.Ranged;
                    }
                    break;
                default:
                    break;
            }
            if(UnitManager.vecinosAntiguos.Count != 0)
            {
                UnitManager.selectedHumanUnit.UnitHighlightDisable(UnitManager.vecinosAntiguos);
                UnitManager.selectedHumanUnit.UnitHighlightDisableAtack(UnitManager.vecinosAntiguos);
            }
            UnitManager.selectedHumanUnit = null;
            UnitManager.vecinosAntiguos.Clear();
        }
    }

    public void SetUnitHighlight()
    {
        _unitColor.SetActive(true);
    }

    public void SetUnitHighlightAtack()
    {
        _highlightAtack.SetActive(true);
    }

    public void DisableUnitHighlight()
    {
        _unitColor.SetActive(false);
    }

    public void DisableUnitHighlightAtack()
    {
        _highlightAtack.SetActive(false);
    }

    public void SetNonWalkable()
    {
        _nonWalkable.SetActive(true);
    }

    public void SetBanderaIA()
    {
        _banderaIA.SetActive(true);
    }

    public void DisableBanderaIA()
    {
        _banderaIA.SetActive(false);
    }

    public void SetBanderaJugador()
    {
        _banderaJugador.SetActive(true);
    }

    public void DisableBanderaJugador()
    {
        _banderaJugador.SetActive(false);
    }

    public void SetCoord(int x, int y)
    {
        posX = x;
        posY = y;
    }
}
