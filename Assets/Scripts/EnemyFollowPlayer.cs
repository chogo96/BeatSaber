using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform�� ����

    void Update()
    {
        if (player != null)
        {
            // �� ĳ���Ͱ� �÷��̾ �ٶ󺸵��� ����
            transform.LookAt(player);
            Debug.Log("Looking at player: " + player.position);
        }
        else
        {
            Debug.Log("Player reference is missing.");
        }
    }
}

