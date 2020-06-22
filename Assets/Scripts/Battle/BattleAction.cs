using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleAction : MonoBehaviour
{
    public int hp;
    public int mp;
    public bool attackActionEnd;
    public bool damageActionEnd;

    public abstract int Attack();
    public abstract int Defence();
    public abstract void AttackAction();
    public abstract void DamageAction();
    public abstract string CharacterName();

    protected int AttackCalculation(int attack, float luck)
    {
        return (int)Mathf.Round(attack * luck);
    }

    protected float DetermineLuck(float min, float max)
    {
        return Random.Range(min, max);
    }
}
