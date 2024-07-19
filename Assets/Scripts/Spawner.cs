using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    /// <summary>
    /// 큐브를 저장하는 배열
    /// </summary>
    public GameObject[] cubes;
    /// <summary>
    /// 스폰되는 장소
    /// </summary>
    public Transform[] points;

    /// <summary>
    /// 큐브 소환 측정 시간
    /// </summary>
    public float beat;
    /// <summary>
    /// 주기
    /// </summary>
    private float _timer;


    private void Update()
    {
        //만약 소환 주기가 됬다면
        if(_timer >= beat)
        {
            //랜덤된 위치로 랜덤한 큐브가 소환되는 함수
            GameObject cube = Instantiate(cubes[Random.Range(0, cubes.Length)], points[Random.Range(0,points.Length)]);
            //큐브 위치 초기화
            cube.transform.localPosition = Vector3.zero;
            //
            _timer -= beat;
        }
        //시간 측정용
        _timer += Time.deltaTime;
    }
}
