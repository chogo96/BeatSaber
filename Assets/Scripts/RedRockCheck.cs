using System.Collections;
using UnityEngine;

public class RedRockCheck : MonoBehaviour
{
    public bool DontResetCombo = false;
    private void Start()
    {
        DontResetCombo = false;
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        // 돌(혹은 적)이 플레이어와 충돌하면
        if (other.CompareTag("RedEnemy"))
        {
            Debug.Log("Red");
            DontResetCombo = true;
            StartCoroutine(DontResetComboReset());
        }
        
    }
    //구르기 기능을 항상 활성화 하되, 적절하지 않은 타이밍에 쫄 구르기를 하면 콤보가 초기화 되어 
    //손해를 보도록 만들었음. 손해를 극대화 하도록 적에게 주는 데미지 로직에 콤보 카운트에 비례해서
    //데미지를 주도록 손봤고, 유저는 막 구르면 손해를 볼 것임. RedRock이 날라올때 구르면 콤보를 
    //잃지 않아야 하므로, 큐브 길목 중간에 콜라이더를 하나 더 박아서 RedRock만 검증하고, RedRock
    //이면 콤보를 리셋하지 말라는 bool을 활성화. 코루틴 따라서 다시 초기화.
    IEnumerator DontResetComboReset()
    {
        yield return new WaitForSeconds(1.5f);
        DontResetCombo = false;
    }

}