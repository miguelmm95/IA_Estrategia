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
    [HideInInspector] public int posX;
    [HideInInspector] public int posY;
    [HideInInspector] public bool unitStay = false;


    public bool unitCanMoveTo = false;

    public GameObject unit;
    public BaseUnit occupiedUnit;

   

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

    public void SetUnitHighlight()
    {
        _unitColor.SetActive(true);
    }

    private void Update()
    {
        if (unit != null)
        {
            unitStay = true;
        }
    }

    public void OnMouseDown(){
        if(GameManager.Instance.State != GameState.PlayerTurn) return;
        if(occupiedUnit != null){
            if(occupiedUnit.player == Player.Human){
                Debug.Log("E");
            }
        }
    }

    public void DisableUnitHighlight()
    {
        _unitColor.SetActive(false);
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
