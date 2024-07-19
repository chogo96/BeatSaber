using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    //public PlayableDirector timeLine;

    void Awake()
    {
        //// 게임이 시작되면 타임라인도 재생
        //timeLine.Play();
    }

    public SlicedObjects[] slicedObjects;
    public DamageController damageController;
    public ComboCount comboCount;
    public PlayerHitCheck playerHitCheck;
   
    private void Start()
    {
        if (slicedObjects != null && damageController != null)
        {
            damageController.SetComboCount(comboCount);
            for (int i = 0; i < slicedObjects.Length; i++)
            {
                if (slicedObjects[i] != null)
                {
                    slicedObjects[i].SetDamageController(damageController);
                }
            }
        }
        playerHitCheck.SetDamageController(damageController);
        
    }
}
