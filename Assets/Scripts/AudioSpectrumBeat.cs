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
    public float threshold = 0.1f; // ���ڸ� ������ ����Ʈ�� �� �Ӱ谪
    public float spawnInterval = 0.5f; // ���ڰ� �����Ǵ� ���� (��)
    public NoteManager noteManager;

    private float timeSinceLastSpawn = 0f;

    void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
        if (m_MyAudioSource == null)
        {
            Debug.LogError("AudioSource ������Ʈ�� ã�� �� �����ϴ�!");
        }
    }

    void Update()
    {
        if (m_MyAudioSource == null) return;

        //����Ʈ�� �����͸� �м��� �迭�� �ִ� �Լ�
        m_MyAudioSource.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        // Ư�� ���ļ� �뿪�� ��հ� ��� (���⼭�� 0~10�� �ε����� ���ļ� �뿪 ���)
        float averageSpectrum = 0f;
        for (int i = 0; i < 10; i++)
        {
            averageSpectrum += spectrum[i];
        }
        averageSpectrum /= 10;

        timeSinceLastSpawn += Time.deltaTime;

        // ��� ����Ʈ�� ���� �Ӱ谪�� �Ѱ�, ���� ������ ������ �� ���� ����
        if (averageSpectrum >= threshold && timeSinceLastSpawn >= spawnInterval)
        {
            SpawnCube();
            timeSinceLastSpawn = 0f; // ���� Ÿ�̸� �ʱ�ȭ
        }

        //����Ʈ�� �ð�ȭ (����� ����)
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
        // ���ο� ���ڸ� ������ ��ġ�� ȸ���� �����մϴ�.
        Vector3 spawnPosition = noteManager.spawnPoints[Random.Range(0, 2)].position;
        Vector3 spawnPosition2 = noteManager.spawnPoints[Random.Range(2, 4)].position;
        Quaternion spawnRotation = noteManager.spawnPoints[Random.Range(0,4)].rotation;

        // ���ڸ� �����մϴ�.
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
