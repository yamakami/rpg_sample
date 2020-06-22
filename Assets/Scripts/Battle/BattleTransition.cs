using UnityEngine;

public class BattleTransition : MonoBehaviour
{
    public BattleSystem battleSystem;
    public Material mt;
    float MinStep = 0f;
    float transitionEnd = 1f;

    void Update()
    {
        SceneStartFadeIn();
    }

    void SceneStartFadeIn()
    {
        if (MinStep <= transitionEnd)
        {
            mt.SetFloat("Vector1_5B039E63", transitionEnd);
            transitionEnd -= Time.deltaTime;
        }
        else
        {
            battleSystem.transitionFadeInEnd = true;
            Destroy(gameObject.transform.root.gameObject);
        }
    }
}
