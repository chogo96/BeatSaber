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
        // ��(Ȥ�� ��)�� �÷��̾�� �浹�ϸ�
        if (other.CompareTag("Enemy") || other.CompareTag("RedEnemy"))
        {
            Debug.Log("Miss");
            damageController.PlayerDamaged();
            // ������Ʈ Ÿ�� ������Ʈ ����
            Destroy(other.gameObject);
        }
        
    }
}
