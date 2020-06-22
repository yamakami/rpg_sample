using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSubAreas : MonoBehaviour
{
    public int areaInfo;

    MovePlayer movePlayer;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        movePlayer = collision.GetComponent<MovePlayer>();
        movePlayer.playerInfo.currentLocationInfo.area = areaInfo;
    }

    private void OnTriggerExit2D(Collider2D collision)       
    {
        if (collision.tag != "Player")
            return;

        movePlayer.playerInfo.currentLocationInfo.area = 0;
    }
}
