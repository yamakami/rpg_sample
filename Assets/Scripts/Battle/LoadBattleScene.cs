using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadBattleScene : MonoBehaviour
{
    public GameObject player;
    public Image transitionImage;
    public GameObject crossFader;
    public GameObject[] locations;

    Animator faderAnim;
    MovePlayer movePlayer;
    float encounteringRest = 0f;
    float encounteringRestInterval = 2f;

    void Start()
    {
        movePlayer = player.GetComponent<MovePlayer>();
        faderAnim = crossFader.GetComponentInChildren<Animator>();
        BackFromBattleScene();
    }

    void Update()
    {        
        if (movePlayer.playerInfo.playerInput.moveVector != Vector2.zero)
        {
            if (encounteringRestInterval < encounteringRest)
            {
                BattleStart(Random.Range(0, 101));
                encounteringRest = 0f;
            }
            encounteringRest += Time.deltaTime;
        }
    }

    void BackFromBattleScene()
    {        
        if (movePlayer.playerInfo.backFromBattle == false)
            return;

        movePlayer.playerInfo.backFromBattle = false;
        StartCoroutine(RestoreFromBattle());
    }

    IEnumerator RestoreFromBattle()
    {
        crossFader.SetActive(true);
        faderAnim.SetTrigger("end");

        foreach (GameObject g in locations)
        {
            if (g.gameObject.name == movePlayer.playerInfo.currentLocationInfo.locationName)
            {
                g.gameObject.SetActive(true);
                movePlayer.currentLocation = g.GetComponent<MonsterAreas>().areas;
            }
            else
            {
                g.gameObject.SetActive(false);
            }
        }

        player.transform.position = movePlayer.playerInfo.currentLocationInfo.position;
        player.GetComponent<MovePlayer>().move = Vector2.zero;
        player.GetComponent<MovePlayer>().lastMove = movePlayer.playerInfo.moveInfo.lastMove;

        yield return new WaitForSeconds(1);
        crossFader.SetActive(false);
        movePlayer.playerInfo.moveInfo.freeze = false;
    }


    public void BattleStart(int encounterd)
    {
        if (movePlayer.playerInfo.moveInfo.freeze == true)
            return;

        if (movePlayer.currentLocation == null)
            return;

        //70
        //Debug.Log("enc: " + encounterd);
        int areaIndex = movePlayer.playerInfo.currentLocationInfo.area;
        LivingMonsters monsters = movePlayer.currentLocation.areas[areaIndex];
        movePlayer.playerInfo.monsterList = monsters.monster;
        
        if (monsters.encounteringRatio < encounterd)
            return;

        movePlayer.playerInfo.moveInfo.freeze = true;
        movePlayer.playerInfo.currentLocationInfo.position = player.transform.position;

        StartCoroutine(TransitionFlash());
    }

    IEnumerator TransitionFlash()
    {
        transitionImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadSceneAsync("Battle");

        yield return new WaitForSeconds(1f);

        transitionImage.gameObject.SetActive(false);
    }
}
