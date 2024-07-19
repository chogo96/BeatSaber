using UnityEngine;

public class PlayerHitCheck : MonoBehaviour
{
    public DamageController damageController;

    public void SetDamageController(DamageController controller)
    {
        damageController = controller;
    }
    private void OnTriggerEnter(Collider other)
    {
        // 돌(혹은 적)이 플레이어와 충돌하면
        if (other.CompareTag("Enemy") || other.CompareTag("RedEnemy"))
        {
            Debug.Log("Miss");
            damageController.PlayerDamaged();
            // 프로젝트 타일 오브젝트 제거
            Destroy(other.gameObject);
        }
        
    }
}
