using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMonstrers : MonoBehaviour
{
    public GameObject unit3;
    public GameObject unit2;
    public GameObject unit1;

    List<Monster> monsterPickedUp = new List<Monster>();
    List<Monster> resourceMonsters = new List<Monster>();
    PlayerAction playerAction;
   
    void Start()
    {

        playerAction = GetComponent<PlayerAction>();
        GetMonstersFromResouses();
        CombineMonsterDataAndObj();
    }

    void GetMonstersFromResouses()
    {
        foreach (Monster m in playerAction.playerInfo.monsterList)
            resourceMonsters.Add(m);

        int maxArrayIndex = resourceMonsters.Count - 1;
        for (int i = 1; i <= AssignUnitMemberNum(3); i++)
        {
            PickUpFromResources(maxArrayIndex);
             maxArrayIndex -= 1;
        }
        resourceMonsters.Clear();
    }

    int AssignUnitMemberNum(int max = 1)
    {
        int limit = 3;
        if (limit < max)
            max = limit;

        int num = Random.Range(1, max + 1);
        return num;
    }

    void PickUpFromResources(int max)
    {
        if (resourceMonsters.Count == 0)
            return;

        int num = Random.Range(0, max + 1);
        monsterPickedUp.Add(resourceMonsters[num]);
        resourceMonsters.RemoveAt(num);
    }

    void CombineMonsterDataAndObj()
    {
        GameObject unit = unit3;
        int monsterNum = monsterPickedUp.Count;

        if (monsterNum == 2)
            unit = unit2;

        if (monsterNum == 1)
            unit = unit1;

        unit.gameObject.SetActive(true);

        BattleSystem battleSystem = GetComponent<BattleSystem>();
        Transform[] tr = unit.GetComponentsInChildren<Transform>();

        for (int i = 0; i < monsterNum; i++)
        {
            GameObject monsterObj = tr[i + 1].gameObject;
            monsterObj.GetComponent<MonsterAction>().AttachMonsterDarta(monsterPickedUp[i]);
            battleSystem.monsters.Add(monsterObj);
        }
        monsterPickedUp.Clear();
    }
}
