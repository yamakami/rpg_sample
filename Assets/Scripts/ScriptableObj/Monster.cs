using UnityEngine;

[CreateAssetMenu(fileName = "New Monser", menuName = "ScriptableObject/Monster")]
public class Monster : ScriptableObject
{
    public string monsterName;
    public Sprite monsterSplite;
    public MonsterStatus mosterStatus;
}

[System.Serializable]
public struct MonsterStatus
{
    public int hp;
    public int attack;
    public int defence;
    public int mp;
    public string livingArea;
    //public Item[] holdingItems;
}