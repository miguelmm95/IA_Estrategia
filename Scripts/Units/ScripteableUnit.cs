using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Scripteable Unit")]

public class ScripteableUnit : ScriptableObject
{
    public Type type;
    public Player player;
    public BaseUnit unitPrefab;
}

public enum Type
{
    Heavy = 0,
    Infantry = 1,
    Ranged = 2
}

public enum Player
{
    Human = 0,
    AI = 1
}