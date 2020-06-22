using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MagicData", menuName = "ScriptableObject/MagicData")]
public class MagicData : ScriptableObject
{
    public List<Magic> magics = new List<Magic>();
}

[System.Serializable]
public class Magic
{
    public string magicType;
    public string MagicType { get; set; }

    public string name;
    public string Name { get; set; }

    public int consumption;
    public int Consumption { get; set; }

    public string description;
    public string Description { get; set; }
}
