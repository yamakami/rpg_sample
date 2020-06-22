using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAction : BattleAction
{
    public Monster monster;
    public float luckMin = 0.5f;
    public float luckMax = 1f;

    int attackValue;
    int defenceValue;

    public void AttachMonsterDarta(Monster monster)
    {
        this.monster = monster;
        GetComponent<SpriteRenderer>().sprite = monster.monsterSplite;
        hp = monster.mosterStatus.hp;
        mp = monster.mosterStatus.mp;
    }

    public override string CharacterName()
    {
        return monster.monsterName;
    }

    public override int Attack()
    {
        float luck = DetermineLuck(luckMin, luckMax);
        int monsterAttack = monster.mosterStatus.attack;

        attackValue = AttackCalculation(monsterAttack, luck);
        return attackValue;
    }

    public override int Defence()
    {
        float luck = DetermineLuck(luckMin, luckMax);
        int monsterDefence = monster.mosterStatus.defence;

        defenceValue = AttackCalculation(monsterDefence, luck);
        return defenceValue;
    }


    public override void AttackAction()
    {
        StartCoroutine(PositionBack());
    }

    IEnumerator PositionBack()
    {
        attackActionEnd = false;

        var pos = transform.position;

        var newPos = new Vector3(pos.x, pos.y, pos.z - 1.5f);

        transform.position = newPos;
        yield return new WaitForSeconds(0.45f);
        transform.position = pos;

        attackActionEnd = true;
    }

    public override void DamageAction()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        sp.color = new Color32(170, 44, 44, 255);
        StartCoroutine(BackToColorWhite(sp));
    }

    IEnumerator BackToColorWhite(SpriteRenderer sp)
    {
        damageActionEnd = false;

        yield return new WaitForSeconds(0.45f);
        sp.color = new Color32(255, 255, 255, 255);

        damageActionEnd = true;
    }
}
