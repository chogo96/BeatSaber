using System.Collections;
using UnityEngine;

public class RedRockCheck : MonoBehaviour
{
    public bool DontResetCombo = false;
    private void Start()
    {
        DontResetCombo = false;
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        // ��(Ȥ�� ��)�� �÷��̾�� �浹�ϸ�
        if (other.CompareTag("RedEnemy"))
        {
            Debug.Log("Red");
            DontResetCombo = true;
            StartCoroutine(DontResetComboReset());
        }
        
    }
    //������ ����� �׻� Ȱ��ȭ �ϵ�, �������� ���� Ÿ�ֿ̹� �� �����⸦ �ϸ� �޺��� �ʱ�ȭ �Ǿ� 
    //���ظ� ������ �������. ���ظ� �ش�ȭ �ϵ��� ������ �ִ� ������ ������ �޺� ī��Ʈ�� ����ؼ�
    //�������� �ֵ��� �պð�, ������ �� ������ ���ظ� �� ����. RedRock�� ����ö� ������ �޺��� 
    //���� �ʾƾ� �ϹǷ�, ť�� ��� �߰��� �ݶ��̴��� �ϳ� �� �ھƼ� RedRock�� �����ϰ�, RedRock
    //�̸� �޺��� �������� ����� bool�� Ȱ��ȭ. �ڷ�ƾ ���� �ٽ� �ʱ�ȭ.
    IEnumerator DontResetComboReset()
    {
        yield return new WaitForSeconds(1.5f);
        DontResetCombo = false;
    }

}