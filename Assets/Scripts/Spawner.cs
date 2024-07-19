using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    /// <summary>
    /// ť�긦 �����ϴ� �迭
    /// </summary>
    public GameObject[] cubes;
    /// <summary>
    /// �����Ǵ� ���
    /// </summary>
    public Transform[] points;

    /// <summary>
    /// ť�� ��ȯ ���� �ð�
    /// </summary>
    public float beat;
    /// <summary>
    /// �ֱ�
    /// </summary>
    private float _timer;


    private void Update()
    {
        //���� ��ȯ �ֱⰡ ��ٸ�
        if(_timer >= beat)
        {
            //������ ��ġ�� ������ ť�갡 ��ȯ�Ǵ� �Լ�
            GameObject cube = Instantiate(cubes[Random.Range(0, cubes.Length)], points[Random.Range(0,points.Length)]);
            //ť�� ��ġ �ʱ�ȭ
            cube.transform.localPosition = Vector3.zero;
            //
            _timer -= beat;
        }
        //�ð� ������
        _timer += Time.deltaTime;
    }
}
