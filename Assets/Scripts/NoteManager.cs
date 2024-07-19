using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public GameObject notePrefab; // ��Ʈ ������
    public GameObject redNotePrefab; // ��Ʈ ������
    public Transform[] spawnPoints; // ��Ʈ ���� ��ġ
    public Rolling rolling;
    // ��Ʈ ���� �Լ�
    // rolling�� move left move right ����. �̰ſ� ���� instantiate�� �������� �����̼�
    //y ���� ��ȯ. 
    public void SpawnNoteLeftTop()
    {
        
        Instantiate(notePrefab, spawnPoints[0].position, spawnPoints[0].rotation);
        
    }
    public void SpawnNoteRightTop()
    {

        Instantiate(notePrefab, spawnPoints[1].position, spawnPoints[1].rotation);
        
    }
    public void SpawnNoteLeftBottom()
    {

        Instantiate(notePrefab, spawnPoints[2].position, spawnPoints[2].rotation);
        
    }
    public void SpawnNoteRightBottom()
    {

        Instantiate(notePrefab, spawnPoints[3].position, spawnPoints[3].rotation);
        
    }
    public void SpawnRedNote()
    {

        Instantiate(redNotePrefab, spawnPoints[4].position, spawnPoints[4].rotation);
        
    }


}
