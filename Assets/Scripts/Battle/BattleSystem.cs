using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{
    public GameObject crossFader;
    [HideInInspector] public List<GameObject>  monsters = new List<GameObject>();
    [HideInInspector] public bool transitionFadeInEnd;
    [HideInInspector] public bool encounteringMessageEnd;
    [HideInInspector] public bool basicCommandOpen;
    [HideInInspector] public int playerAttackCount;

    PlayerAction playerAction;
    BattleAction attacker;
    BattleAction defender;
    BattleMenu battleMenu;
    string battleMsseage;
    int damage;
    int targetMonsterIndex;


    BATTLE_PROCESS battleProcess;
    CURRENT_TURN currentTurn;

    enum BATTLE_PROCESS
    {
        NO_BATTLE,
        MONSTER_TURN_ATTACK,
        ATTACKING_MESSAGE,
        MONSTER_ATTACK_MOVE,
        DAMAGE_REACTION,
        DAMAGED_MESSAGE,
        ATTACK_RESULT,
        PLAYER_WIN,
        PLAYER_LOSE,
        BATTLE_END,

        DROP_ITEM,
        SCENE_EXIT
    }

    enum CURRENT_TURN
    {
        PLAYER,
        MONSTER
    }

    void Start()
    {
        currentTurn = DetermineStartWith();
        if(currentTurn == CURRENT_TURN.PLAYER)
            battleProcess = BATTLE_PROCESS.NO_BATTLE;
        else
            battleProcess = BATTLE_PROCESS.MONSTER_TURN_ATTACK;

        basicCommandOpen = false;
        battleMenu = GetComponent<BattleMenu>();
        playerAction = GetComponent<PlayerAction>();
        playerAction.playerInfo.backFromBattle = false;
    }

    CURRENT_TURN DetermineStartWith()
    {
        int num =  Random.Range(0, 101);
        if (80 < num)
            return CURRENT_TURN.MONSTER;

        return CURRENT_TURN.PLAYER;
    }

    void Update()
    {
        if (encounteringMessageEnd == false)
            return;

        if (BattleMenu.messageProcessing == true)
            return;

        if (battleProcess == BATTLE_PROCESS.MONSTER_TURN_ATTACK)
            MonsterTurnAttack();
        else if (battleProcess == BATTLE_PROCESS.ATTACKING_MESSAGE)
            AttackingMessage();
        else if (battleProcess == BATTLE_PROCESS.MONSTER_ATTACK_MOVE)
            MonsterAttackMove();
        else if (battleProcess == BATTLE_PROCESS.DAMAGE_REACTION && attacker.attackActionEnd == true)
            DamageReaction();
        else if (battleProcess == BATTLE_PROCESS.DAMAGED_MESSAGE && defender.damageActionEnd == true)
            DamageMessage();
        else if (battleProcess == BATTLE_PROCESS.ATTACK_RESULT)
            AttackResult();
        else if (battleProcess == BATTLE_PROCESS.PLAYER_WIN)
            PlayerWin();
        else if (battleProcess == BATTLE_PROCESS.PLAYER_LOSE)
            PlayerLose();
        else if (battleProcess == BATTLE_PROCESS.BATTLE_END)
            BattleEnd();
    }

    public void BattleEscape()
    {
        battleProcess = BATTLE_PROCESS.BATTLE_END;
    }


    void BattleEnd()
    {
        playerAction.playerInfo.backFromBattle = true;
        StartCoroutine(BackToScene());
    }

    IEnumerator BackToScene()
    {
        crossFader.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("SampleScene");
    }

    void PlayerWin()
    {
        if (battleProcess != BATTLE_PROCESS.PLAYER_WIN)
            return;

        string message = string.Format("{0}はモンスターを退治した", attacker.CharacterName());
        StartCoroutine(BattleMenu.LetterDisplay(message));

        battleProcess = BATTLE_PROCESS.BATTLE_END;
    }

    void PlayerLose()
    {
        if (battleProcess != BATTLE_PROCESS.PLAYER_LOSE)
            return;

        string message = string.Format("{0}はモンスター討伐に失敗した", defender.CharacterName());
        StartCoroutine(BattleMenu.LetterDisplay(message));

        battleProcess = BATTLE_PROCESS.BATTLE_END;
    }

    void AttackResult()
    {
        if (currentTurn == CURRENT_TURN.MONSTER)
        {
            if (defender.hp < 1)
                battleProcess = BATTLE_PROCESS.PLAYER_LOSE;
            else
                battleProcess = MonsterSequentialAttack();
            return;
        }
        else
        {
            if (defender.hp < 1)
            {
                monsters.RemoveAt(targetMonsterIndex);
                Destroy(defender.gameObject);
            }

            if (0 < monsters.Count)
            {
                battleProcess = BATTLE_PROCESS.MONSTER_TURN_ATTACK;
                return;
            }
        }

        battleProcess = BATTLE_PROCESS.PLAYER_WIN;
    }

    BATTLE_PROCESS MonsterSequentialAttack()
    {
        int range = Random.Range(1, 101);
        if( 70 < range)
            return BATTLE_PROCESS.MONSTER_TURN_ATTACK;

        if(0 < playerAttackCount)
            battleMenu.OpenAttackTarget();
        else
            battleMenu.ControllBasicCommandButtons(true);

        return BATTLE_PROCESS.NO_BATTLE;
    }

    void DamageMessage()
    {
        defender.damageActionEnd = false;

        int attack = attacker.Attack();
        int deffence = defender.Defence();
        string defenderName = defender.CharacterName();
        damage = Mathf.Abs(deffence - attack);
        defender.hp -= damage;

        if (currentTurn == CURRENT_TURN.MONSTER)
            playerAction.playerInfo.statusInfo.hp = defender.hp;

        string damageMessage = string.Format("{0}は{1}hpのダメージを受けた", defenderName, damage);

        StartCoroutine(BattleMenu.LetterDisplay(damageMessage));

        battleProcess = BATTLE_PROCESS.ATTACK_RESULT;
    }

    void DamageReaction()
    {
        defender.DamageAction();
        battleProcess = BATTLE_PROCESS.DAMAGED_MESSAGE;
    }

    void MonsterAttackMove()
    {
        attacker.AttackAction();
        battleProcess = BATTLE_PROCESS.DAMAGE_REACTION;
    }

    void AttackingMessage()
    {
        StartCoroutine(BattleMenu.LetterDisplay(battleMsseage));

        battleProcess = BATTLE_PROCESS.MONSTER_ATTACK_MOVE;
    }

    public void PlayerTurnAttack(int monsterNum)
    {
        targetMonsterIndex = monsterNum;
        currentTurn = CURRENT_TURN.PLAYER;
        playerAttackCount += 1;

        MonsterAction monsterAction = monsters[targetMonsterIndex].GetComponent<MonsterAction>();

        string playerName = playerAction.CharacterName();
        string monsterName = monsterAction.CharacterName();

        battleMsseage = string.Format("{0}の{1}への攻撃", playerName, monsterName);
        attacker = playerAction;
        defender = monsterAction;
        battleProcess = BATTLE_PROCESS.ATTACKING_MESSAGE;
    }

    public void MonsterTurnAttack()
    {
        currentTurn = CURRENT_TURN.MONSTER;

        battleMenu.ControllBasicCommandButtons(false);

        int monsterIndex = Random.Range(0, monsters.Count);

        MonsterAction monsterAction = monsters[monsterIndex].GetComponent<MonsterAction>();

        string monsterName = monsterAction.CharacterName();

        battleMsseage = string.Format("{0}の攻撃", monsterName);
        attacker = monsterAction;
        defender = playerAction;
        battleProcess = BATTLE_PROCESS.ATTACKING_MESSAGE;
    }
}

