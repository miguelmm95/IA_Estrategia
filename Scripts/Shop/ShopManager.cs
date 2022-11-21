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
            MoneyTxt.text = Money.ToString();
            Quantity[ItemNames[ButtonRef.GetComponent<button>().ID]] += 1;
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
    }
}
