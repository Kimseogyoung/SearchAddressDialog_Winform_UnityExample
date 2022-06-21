using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    public int id;
    public int hp;
    public int curhp;

    private void Start()
    {
        curhp = hp;
    }
    //아이템
    public void pickedup()
    {
        gameObject.SetActive(false);
    }
    





    //몬스터
    public void DieAndRespown()
    {
        gameObject.SetActive(false);
        Invoke("respown", 5.0f);
    }
    public void respown()
    {
        curhp = hp;
        gameObject.SetActive(true);

    }
}
