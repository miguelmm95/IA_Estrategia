using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Win_Lose : MonoBehaviour
{

    public TextMeshProUGUI win;
    public TextMeshProUGUI lose;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (UnitManager._AIFlags.Count == 0)
        {
            win.gameObject.SetActive(true);
        }
        else if (UnitManager._playerFlags.Count == 0)
        {
            lose.gameObject.SetActive(false);
        }
    }
}
