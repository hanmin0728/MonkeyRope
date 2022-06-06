using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ã�������� �����ϴ� ���
public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
                if (_instance == null)
                {
                    var _newGameobj = new GameObject(typeof(T).ToString());
                    _instance = _newGameobj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
    protected virtual void Awake()
    {
        if (_instance = null)
        {
            _instance = this as T;
        }
        //���� �ٲ� ������� �ʰ� ��ü�� ����
        //�ֻ��� ��ü�����Ѵ�
        DontDestroyOnLoad(gameObject);
    }
}