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
        // ť�긦 �̵���ŵ�ϴ�.
        
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        
       
    }
    
    
    
}

// collider�� ������� �����Ƿ�, �� ���̿� ������ ť��� ������ �ȉ� �� ������ ���� �ð� �� ���� �Լ� �ۼ�.
// �׷��� prefab���� ������ �����ع��� NoteManager���� �����ϴ� Prefab�� ���� �Ǿ����