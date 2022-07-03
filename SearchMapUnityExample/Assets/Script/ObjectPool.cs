using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Component로 T를 제한하는 경우도 있음. Component= Monobehavior와 Transform의 부모 클래스
public class ObjectPool<T>  where T  : MonoBehaviour
{
    private string objName;
    private GameObject poolObject;
    private int objectCount;
    private int maxObjectCount;
    public Transform parent;
    private Queue<T> objectPool ;
    
    public ObjectPool(string objName, GameObject poolObject, int objectCount, Transform parent)
    {
        maxObjectCount = 5;
        this.objName = objName;
        this.poolObject = poolObject;
        this.objectCount = objectCount;
        this.parent = parent;
        objectPool = new Queue<T>();
        Allocate(this.objectCount);
    }
    
    public void Allocate(int cnt)
    {
        objectCount = cnt;
        for(int i=0; i<cnt; i++)
        {
            GameObject obj = GameObject.Instantiate(poolObject);

            obj.transform.SetParent(parent);
            obj.name = objName + i.ToString();
            obj.gameObject.SetActive(false);
            
            objectPool.Enqueue(obj.GetComponent<T>());
        }
    }

    public T Deque(bool setActive = true)
    {
        if (objectPool.Count <= 0)
        {
            Allocate(maxObjectCount);
        }
        T obj = objectPool.Dequeue();
        obj.gameObject.SetActive(setActive);
        return obj;

    }
    public void  Enqueue(T obj)
    {
        obj.gameObject.SetActive(false);
        objectPool.Enqueue(obj);
    }
}
