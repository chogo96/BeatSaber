using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform을 참조

    void Update()
    {
        if (player != null)
        {
            // 적 캐릭터가 플레이어를 바라보도록 설정
            transform.LookAt(player);
            Debug.Log("Looking at player: " + player.position);
        }
        else
        {
            Debug.Log("Player reference is missing.");
        }
    }
}

