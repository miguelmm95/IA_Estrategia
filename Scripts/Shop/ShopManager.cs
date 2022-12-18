using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ShopManager : MonoBehaviour
{

    [SerializeField] private GameObject _shopUI;

    public int[,] Items = new int[6, 6];
    public string[] ItemNames = new string[6];
    public float Money;
    public TMP_Text MoneyTxt;
    public TMP_Text ElementQ;
    int tempID;
    public string Last;
    int r;
    public string[] UnitList = { "melee", "tank", "ranged" };
    public int AIMoney;
    public int Round;
    public List<string> playerList;
    public List<string> AIList;


    public Dictionary<string, int> Elements = new Dictionary<string, int>()
    {
        {"melee", 1 },
        {"tank", 2 },
        {"ranged", 3 }
    };
    public Dictionary<string, int> Quantity = new Dictionary<string, int>()
    {
        {"melee", 0 },
        {"tank", 0 },
        {"ranged", 0 }
    };

    void Awake()
    {
        GameManager.onGameStateChanged += GameManageronGameStateChanged;
    }

    void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameManageronGameStateChanged;
    }

    private void GameManageronGameStateChanged(GameState state)
    {
        _shopUI.SetActive(state == GameState.PlayerShop);
    }

    void Start()
    {
        Round = 0;
        r = Random.Range(0, 2);
        MoneyTxt.text = "Money: " + Money.ToString();
        for (int i = 1; i < 6; i++)
        {
            tempID = Random.Range(0, 3); 
            Items[1, i] = tempID;
            Items[2, i] = Elements.ElementAt(tempID).Value;
            ItemNames[i] = Elements.ElementAt(tempID).Key;

        }
    }

    
    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (Money >= Items[2, ButtonRef.GetComponent<button>().ID])
        {
            Money -= Items[2, ButtonRef.GetComponent<button>().ID];
            MoneyTxt.text = "Money: " + Money.ToString();
            Quantity[ItemNames[ButtonRef.GetComponent<button>().ID]] += 1;
            Last = ItemNames[ButtonRef.GetComponent<button>().ID];
            UnitManager.contadorUnidades++;
            UnitManager._playerUnits.Add(Last);
            Round++;
            IAShop(Last, r);
            UpdateShop(ButtonRef.GetComponent<button>().ID);
        }
    }

    public void UpdateShop(int element)
    {
        tempID = Random.Range(0, 3);
        Items[1, element] = tempID;
        Items[2, element] = Elements.ElementAt(tempID).Value;
        ItemNames[element] = Elements.ElementAt(tempID).Key;
    }
    public void Update()
    {
        ElementQ.text = "Melee: " + Quantity["melee"] + "\n" + "Tank: " + Quantity["tank"] + "\n" + "Ranged: " + Quantity["ranged"];
        if (Round % 5 == 0)
        {
            r = Random.Range(0, 2);
        }
    }

    public void IAShop(string playerLast, int r)
    {
        if (r == 0)
        {
            string rUnit = UnitList[Random.Range(0, UnitList.Length)];
            UnitManager._AIUnits.Add(rUnit);
            AIMoney -= Elements[rUnit];
        }
        else
        {
            switch (playerLast)
            {
                case "melee":
                    UnitManager._AIUnits.Add("ranged");
                    AIMoney -= Elements["ranged"];
                    break;
                case "tank":
                    UnitManager._AIUnits.Add("melee");
                    AIMoney -= Elements["melee"];
                    break;
                default:
                    UnitManager._AIUnits.Add("tank");
                    AIMoney -= Elements["tank"];
                    break;
            }
        }
        Debug.Log(UnitManager._AIUnits.Count);
    }
}
