using System.Collections;
using UnityEngine;

public class CubeMoves : MonoBehaviour
{
    public float speed = 1f;
    public Transform cubeTransform;
    public Transform playerTransform;
    public Rolling rolling;
    void Start()
    {
        
        
    }
    void Update()
    {
        // 큐브를 이동시킵니다.
        
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        
       
    }
    
    
    
}

// collider가 사라졌다 켜지므로, 그 사이에 지나간 큐브는 삭제가 안됄 수 있으니 일정 시간 후 삭제 함수 작성.
// 그러나 prefab까지 스스로 삭제해버려 NoteManager에서 생성하는 Prefab이 널이 되어버림