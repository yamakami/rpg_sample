using UnityEngine;

[CreateAssetMenu(fileName = "New CharactorInfo", menuName = "ScriptableObject/CharactorInfo")]
public class CharacterInfo : ScriptableObject
{
    public InputInfo playerInput;
    public string characterName;
    public float moveSpeed = 2f;
    public bool talking;
    public bool backFromBattle;
    public MoveInfo moveInfo;
    public NpcTouching npcTouching;
    public CurrentLocationInfo currentLocationInfo;
    public StatusInfo statusInfo;
    public MagicInfo[] magicInfo;
    public Monster[] monsterList;
}

public struct InputInfo
{
    public Vector2 moveVector;
    public bool fire;
}

[System.Serializable]
public struct StatusInfo
{
    public int level;
    public int hp;
    public int attack;
    public int defence;
    public int mp;
    public int exp;
    public int gold;
}

[System.Serializable]
public struct MagicInfo
{
    public string magicName;

}

[System.Serializable]
public struct MoveInfo
{
    public bool freeze;
    public Vector2 lastMove;
}

[System.Serializable]
public struct CurrentLocationInfo
{
    public string locationName;
    public Vector2 position;
    public int area;
}

[System.Serializable]
public struct NpcTouching
{
    public bool touching;
}