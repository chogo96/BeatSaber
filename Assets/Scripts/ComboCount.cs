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

    //��带 �������� playerController���� _comboCount�� +=�� ���������� UI������Ʈ �ϵ���. ���� ȣ���� �Լ�
    public void ComboCountUI()
    {
        _comboCountText.text = comboCount.ToString();
    }
    public void ComboCountPlus()
    {
        comboCount++;
    }
    //���� ��带 ���� �������� �޺��� �ʱ�ȭ�ϴ� �Լ�. �����غ��� �÷��̾� ü���� �����Ҷ� ȣ���ϸ� �� �� ����.
    public void ComboCountReset()
    {
        comboCount = 0;
    }
}
