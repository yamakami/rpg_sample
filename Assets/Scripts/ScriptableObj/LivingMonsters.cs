using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LivingMonsters", menuName = "ScriptableObject/LivingMonster")]
public class LivingMonsters : ScriptableObject
{
    public int encounteringRatio;
    public Monster[] monster;
}
