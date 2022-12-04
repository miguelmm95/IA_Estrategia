using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _unitColor;
    [SerializeField] private GameObject _banderaIA;
    [SerializeField] private GameObject _banderaJugador;
    [SerializeField] private GameObject _nonWalkable;
    [HideInInspector] public int posX;
    [HideInInspector] public int posY;
    [HideInInspector] public bool unitStay = false;


    public bool unitCanMoveTo = false;
    public bool isWalkeable;
    private int maxRange;

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
        if(occupiedUnit != null){
            if(occupiedUnit.player == Player.Human){
                UnitManager.Instance.SelectedHero((BaseHumanUnit)occupiedUnit);
                UnitManager.selectedHumanUnit.UnitHighlightDisable(UnitManager.vecinosAntiguos);
                UnitManager.vecinosAntiguos.Clear();
                if (occupiedUnit.type == Type.Heavy)
                {
                    maxRange = 1;
                }
                else if (occupiedUnit.type == Type.Ranged)
                {
                    maxRange = 3;
                }
                else
                {
                    maxRange = 2;
                }
                UnitManager.vecinosAntiguos = UnitManager.selectedHumanUnit.UnitHighlight(maxRange, this);          
                Debug.Log(occupiedUnit);
            }
            else
            {
                if(UnitManager.selectedHumanUnit != null)
                {
                    //Ataque
                    Debug.Log("HIT");
                }
            }
        }
        else
        {
            UnitManager.selectedHumanUnit.UnitHighlightDisable(UnitManager.vecinosAntiguos);
            UnitManager.selectedHumanUnit = null;
            UnitManager.vecinosAntiguos.Clear();

        }
    }

    public void SetUnitHighlight()
    {
        _unitColor.SetActive(true);
    }

    public void DisableUnitHighlight()
    {
        _unitColor.SetActive(false);
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
