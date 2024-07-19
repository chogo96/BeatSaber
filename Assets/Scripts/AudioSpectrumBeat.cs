using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static ONSPPropagationMaterial;
[RequireComponent(typeof(AudioSource))]
public class AudioSpectrumBeat : MonoBehaviour
{
    private AudioSource m_MyAudioSource;
    private float[] spectrum = new float[256];
    public GameObject[] cubePrefab;
    public float threshold = 0.1f; // 상자를 스폰할 스펙트럼 값 임계값
    public float spawnInterval = 0.5f; // 상자가 스폰되는 간격 (초)
    public NoteManager noteManager;

    private float timeSinceLastSpawn = 0f;

    void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
        if (m_MyAudioSource == null)
        {
            Debug.LogError("AudioSource 컴포넌트를 찾을 수 없습니다!");
        }
    }

    void Update()
    {
        if (m_MyAudioSource == null) return;

        //스펙트럽 데이터를 분석해 배열에 넣는 함수
        m_MyAudioSource.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        // 특정 주파수 대역의 평균값 계산 (여기서는 0~10번 인덱스의 주파수 대역 사용)
        float averageSpectrum = 0f;
        for (int i = 0; i < 10; i++)
        {
            averageSpectrum += spectrum[i];
        }
        averageSpectrum /= 10;

        timeSinceLastSpawn += Time.deltaTime;

        // 평균 스펙트럼 값이 임계값을 넘고, 스폰 간격이 지났을 때 상자 생성
        if (averageSpectrum >= threshold && timeSinceLastSpawn >= spawnInterval)
        {
            SpawnCube();
            timeSinceLastSpawn = 0f; // 스폰 타이머 초기화
        }

        //스펙트럼 시각화 (디버깅 목적)
        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
           Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
        }
    }

    void SpawnCube()
    {
        int RedOrNormal = Random.Range(0, 100);
        // 새로운 상자를 스폰할 위치와 회전을 설정합니다.
        Vector3 spawnPosition = noteManager.spawnPoints[Random.Range(0, 2)].position;
        Vector3 spawnPosition2 = noteManager.spawnPoints[Random.Range(2, 4)].position;
        Quaternion spawnRotation = noteManager.spawnPoints[Random.Range(0,4)].rotation;

        // 상자를 생성합니다.
        if (RedOrNormal < 50 )
        {
            Instantiate(cubePrefab[0], spawnPosition, spawnRotation);
            Instantiate(cubePrefab[0], spawnPosition2, spawnRotation);

        }
        else if (RedOrNormal < 73)
        {
            Instantiate(cubePrefab[0], spawnPosition, spawnRotation);
        }
        else if(RedOrNormal < 97 ||  RedOrNormal >=73)
            Instantiate(cubePrefab[0], spawnPosition2, spawnRotation);

        if (RedOrNormal >= 97)
            Instantiate(cubePrefab[1], noteManager.spawnPoints[4].position, spawnRotation);
    }
}
