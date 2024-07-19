using Oculus.Platform;
using System.Collections;
using TMPro;
using UnityEngine;

public class ComboCount : MonoBehaviour
{
    [SerializeField] private TMP_Text _comboCountText;

    public int comboCount;

    public void Awake()
    {
        comboCount = 0;
    }

    //노드를 베었을때 playerController에서 _comboCount를 +=로 변경했을때 UI업데이트 하도록. 같이 호출할 함수
    public void ComboCountUI()
    {
        _comboCountText.text = comboCount.ToString();
    }
    public void ComboCountPlus()
    {
        comboCount++;
    }
    //만약 노드를 베지 못했을때 콤보를 초기화하는 함수. 생각해보니 플레이어 체력이 감소할때 호출하면 될 것 같음.
    public void ComboCountReset()
    {
        comboCount = 0;
    }
}
