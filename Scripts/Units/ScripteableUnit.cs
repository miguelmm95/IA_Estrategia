using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Scripteable Unit")]

public class ScripteableUnit : ScriptableObject
{
    public Type type;
    public Player player;
    public State state;
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

public enum State
{
    humanUnselected = 0,
    humanSelected = 1,
    humanMoved = 2,
    humanAttacking = 3,
    humanWaiting = 4,
    AIMoving = 5,
    AIMoved = 6,
    AIDefending = 7,
    AIAttacking = 8,
    AIWaiting = 9
}