using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LocationInfo", menuName = "ScriptableObject/LocationInfo")]
public class LocationInfo : ScriptableObject
{
    public GameObject location;
    public LivingMonsters[] areas;
}
