using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StrOpe = StringOperationUtil.OptimizedStringOperation;

public class BattleMenu : MonoBehaviour
{
    public Text messageDialogue;
    public Image basicCommand;
    public Image attackTarget;
    public Button[] attackTargetButtons;

    public static Text messageText;
    public static bool messageProcessing;

    BattleSystem battleSystem;
    
    void Start()
    {
        battleSystem = GetComponent<BattleSystem>();
        messageText = messageDialogue;
    }

    void Update()
    {
        EnencounteringMessage();
        OpenBasicCommand();
    }

    void EnencounteringMessage()
    {
        if (battleSystem.transitionFadeInEnd == false)
            return;

        if (battleSystem.encounteringMessageEnd == true)
            return;

        battleSystem.encounteringMessageEnd = true;

        string str = "{0}があらわれた！";
        List<string> message = new List<string>(battleSystem.monsters.Count);

        foreach(GameObject monsterObj in battleSystem.monsters)
        {
            message.Add(string.Format(str, monsterObj.GetComponent<MonsterAction>().CharacterName()));
        }

        messageDialogue.transform.root.gameObject.SetActive(true);
        StartCoroutine( LetterDisplay(string.Join("\n", message)) );
    }

    public static IEnumerator LetterDisplay(string conversation)
    {
        messageProcessing = true;

        yield return new WaitForSeconds(0.2f);

        messageText.text = null;
        foreach (char c in conversation.ToCharArray())
        {
            yield return new WaitForSeconds(0.02f);
            messageText.text = StrOpe.i + messageText.text + c;
        }
        yield return new WaitForSeconds(0.5f);
        messageProcessing = false;
    }

    void OpenBasicCommand()
    {
        if (battleSystem.encounteringMessageEnd == false)
            return;

        if (messageProcessing == true)
            return;

        if (battleSystem.basicCommandOpen == true)
            return;

        basicCommand.gameObject.SetActive(true);
        battleSystem.basicCommandOpen = true;
    }

    public void BackToBasicCommand()
    {
        attackTarget.gameObject.SetActive(false);

        Button[] commandButtons = basicCommand.GetComponentsInChildren<Button>();
        foreach (Button button in commandButtons)
            button.interactable = true;
    }

    public void ControllBasicCommandButtons(bool activete)
    {
        Button[] commandButtons = basicCommand.GetComponentsInChildren<Button>();
        foreach (Button button in commandButtons)
            button.interactable = activete;
    }


    public void OpenAttackTarget()
    {
        ControllBasicCommandButtons(false);

        messageDialogue.text = "";

        attackTarget.gameObject.SetActive(true);
        int monsterNum = battleSystem.monsters.Count;

        foreach(Button bt in attackTargetButtons)
            bt.gameObject.SetActive(false);

        for (int i = 0; i < monsterNum; i++)
        {
            attackTargetButtons[i].gameObject.SetActive(true);
            Text textBox = attackTargetButtons[i].GetComponentInChildren<Text>();
            textBox.text = battleSystem.monsters[i].GetComponent<MonsterAction>().CharacterName();
        }
        attackTarget.gameObject.SetActive(true);
    }

    public void CloseAttackTarget(int monsterNum)
    {
        attackTarget.gameObject.SetActive(false);
        battleSystem.PlayerTurnAttack(monsterNum);
    }

    public void Escape()
    {
        battleSystem.BattleEscape();
    }
}
