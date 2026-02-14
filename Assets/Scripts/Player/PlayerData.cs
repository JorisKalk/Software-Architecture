using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int maxHP;
    public float moveSpeed;
    public float jumpForce;
    public float xpToFirstLevel;
    public float levelXpMultiplier;

    public Player CreatePlayer()
    {
        return new Player(maxHP, xpToFirstLevel, levelXpMultiplier);
    }
}

[Serializable]
public class Player
{
    public int MaxHP => maxHP;
    private int maxHP;
    public int currentHP;
    public float XpToNextLevel => xpToNextLevel;
    private float xpToNextLevel;
    public float currentXP;
    private float levelXpMultiplier;

    public Player(int pMaxHP, float pXpToFirstLevel, float pLevelXpMultiplier)
    {
        maxHP = pMaxHP;
        currentHP = pMaxHP;
        currentXP = 0;
        xpToNextLevel = pXpToFirstLevel;
        levelXpMultiplier = pLevelXpMultiplier;
    }


    public void MaxHpUp(int extraHP)
    {
        maxHP += extraHP;
        currentHP += extraHP;
    }

    public int UpdateLevel(int xp)
    {
        int amountOfLevels = 0;

        currentXP += xp;
        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            xpToNextLevel *= levelXpMultiplier;
            amountOfLevels++;
            Debug.Log("leveled up");
        }

        //if (currentXP >= xpToNextLevel)
        //{
        //    currentXP -= xpToNextLevel;
        //    xpToNextLevel *= levelXpMultiplier;
        //    Debug.Log("leveled up");
            
        //}
        
        return amountOfLevels;
    }
}
