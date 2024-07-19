using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public GameObject notePrefab; // 노트 프리팹
    public GameObject redNotePrefab; // 노트 프리팹
    public Transform[] spawnPoints; // 노트 생성 위치
    public Rolling rolling;
    // 노트 생성 함수
    // rolling의 move left move right 변수. 이거에 따라서 instantiate할 포지션의 로테이션
    //y 값을 변환. 
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
