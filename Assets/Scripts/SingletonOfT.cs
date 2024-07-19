using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �̱��� �Լ� ,
/// <typeparam name="T"> �����ϴ� Ŭ���� �ڷ��� </typeparam>
public class SingletonOfT<T> : MonoBehaviour
    where T : SingletonOfT<T>
{
    /// <summary>
    /// �ν��Ͻ� ���� ������Ƽ
    /// </summary>
    public static T Instance
    {
        get
        {
            return _instance;
        }
    }

    private static T _instance;//���κ���

    /// <summary>
    /// �ʱ�ȭ �Լ�
    /// </summary>
    /// <returns></returns>
    public virtual bool Init()
    {
        if (_instance == null)//���ٸ� �߰�
        {
            _instance = (T)this;


        }
        if (_instance != (T)this)
        {
            Destroy(this.gameObject);//����
        }
        return true;
    }
}