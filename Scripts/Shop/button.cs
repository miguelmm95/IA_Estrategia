using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class button : MonoBehaviour
{
    public int ID;
    public TMP_Text Price;
    public TMP_Text Name;

    public GameObject ShopManager;
    
    void Update()
    {
        Price.text = ShopManager.GetComponent<ShopManager>().Items[2, ID].ToString();
        Name.text = ShopManager.GetComponent<ShopManager>().ItemNames[ID].ToString();
    }
}
