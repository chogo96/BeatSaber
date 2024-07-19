using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    //public PlayableDirector timeLine;

    void Awake()
    {
        //// ������ ���۵Ǹ� Ÿ�Ӷ��ε� ���
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
