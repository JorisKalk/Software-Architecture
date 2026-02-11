using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int maxHP;
    public float moveSpeed;
    public float jumpForce;

    public Player CreatePlayer()
    {
        return new Player(maxHP);
    }
}

[Serializable]
public class Player
{
    public int MaxHP => maxHP;
    private int maxHP;
    public int currentHP;
    public int currentXP;

    public Player(int pMaxHP)
    {
        maxHP = pMaxHP;
        currentHP = pMaxHP;
        currentXP = 0;
    }
}
