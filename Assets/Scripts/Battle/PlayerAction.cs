using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : BattleAction
{
    public float luckMin = 0.7f;
    public float luckMax = 1.2f;
    public CharacterInfo playerInfo;
    public Image attackedColor;

    int attackValue;
    int maxAttack;
    int defenceValue;

    void Start()
    {
        hp = playerInfo.statusInfo.hp;
        mp = playerInfo.statusInfo.mp;
    }

    public int MaxAttack { get => maxAttack; }

    public override string CharacterName()
    {
        return playerInfo.characterName;
    }

    public override int Attack()
    {
        float luck = DetermineLuck(luckMin, luckMax);
        int playerAttack = playerInfo.statusInfo.attack;

        attackValue = AttackCalculation(playerAttack, luck);
        maxAttack = AttackCalculation(playerAttack, luckMax);
        return attackValue;
    }

    public override int Defence()
    {
        float luck = DetermineLuck(luckMin, luckMax);
        int playerDefence = playerInfo.statusInfo.defence;

        defenceValue = AttackCalculation(playerDefence, luck);
        return defenceValue;
    }

    public override void AttackAction() { attackActionEnd = true; }

    public override void DamageAction()
    {
        StartCoroutine(Attacked());
    }

    IEnumerator Attacked()
    {
        damageActionEnd = false;

        attackedColor.color = new Color(0.5f, 0f, 0f, 0.5f);

        var pos = Camera.main.transform.localPosition;
        var elapsed = 0f;
        while (elapsed < 0.25f)
        {
            var x = pos.x + Random.Range(-1f, 1f) * 0.1f;
            var y = pos.y + Random.Range(-1f, 1f) * 0.1f;

            Camera.main.transform.localPosition = new Vector3(x, y, pos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(0.3f);
        attackedColor.color = new Color(0f, 0f, 0f, 0f);

        Camera.main.transform.localPosition = pos;

        damageActionEnd = true;
    }
}
