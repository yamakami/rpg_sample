using System.Collections;
using UnityEngine;
using StrOpe = StringOperationUtil.OptimizedStringOperation;

public class LocationJump : MonoBehaviour
{
    [SerializeField] Canvas faderCanvas = null;
    [SerializeField] GameObject locationTo = null;
    [SerializeField] GameObject appearSpot = null;
    [SerializeField] Vector2 facing = Vector2.down;

    Animator faderAnim;
    MovePlayer movePlayer;

    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag != "Player")
            return;

        movePlayer = player.GetComponent<MovePlayer>();
        faderAnim = faderCanvas.GetComponentInChildren<Animator>();
        movePlayer.currentLocation = locationTo.GetComponent<MonsterAreas>().areas;

        movePlayer.playerInfo.currentLocationInfo.area = 0;

        StartCoroutine(Transition(movePlayer));
    }

    IEnumerator Transition(MovePlayer movePlayer)
    {
        movePlayer.playerInfo.moveInfo.freeze = true;
        faderCanvas.gameObject.SetActive(true);

        yield return new WaitForSeconds(1);

        transform.root.gameObject.SetActive(false);
        locationTo.SetActive(true);

        movePlayer.transform.position = new Vector2(
                                                appearSpot.transform.position.x,
                                                appearSpot.transform.position.y);


        movePlayer.playerInfo.playerInput.moveVector = Vector2.zero;
        movePlayer.move = Vector2.zero;
        movePlayer.lastMove = facing;

        faderAnim.SetTrigger("end");


        Invoke("RestoreGeamobjects", 2f);
    }

    void RestoreGeamobjects()
    {
        faderCanvas.gameObject.SetActive(false);
        movePlayer.playerInfo.moveInfo.freeze = false;
    }
}