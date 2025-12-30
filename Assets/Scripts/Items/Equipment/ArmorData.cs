using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ArmorData", menuName = "Scriptable Objects/Equipment/ArmorData")]
public class ArmorData : ScriptableObject
{
    public int defense;
    public int health;
}
