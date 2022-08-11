using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField]
    private GameObject poolingObjectPrefab;

    private Queue<knight> poolingObjectQueue = new Queue<knight> ();

    private void Awake()
    {
        Instance = this;

        Initialize(20);
    }

    private knight CreateNewKnight()
    {
        var newObject = Instantiate(poolingObjectPrefab, transform).GetComponent<knight>();
        newObject.gameObject.SetActive(false);
        return newObject;
    }

    private void Initialize(int count)      //�Է¹��� ������ŭ �켱 ������Ʈ Ǯ�� ��������
    {
        for(int i = 0; i < count; i++)
        {
            poolingObjectQueue.Enqueue(CreateNewKnight());
        }
    }
    
    public static knight GetObject()
    {
        if(Instance.poolingObjectQueue.Count>0)
        {
            var obj = Instance.poolingObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewKnight();
            newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }

    public static void ReturnObject(knight knight)  //������� ������Ʈ�� �����޴� �Լ�
    {
        knight.gameObject.SetActive(false);
        knight.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueue.Enqueue(knight);
    }
    
}
