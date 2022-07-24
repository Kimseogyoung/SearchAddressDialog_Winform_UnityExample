using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private float Hp;
    [SerializeField]
    private float currentHp;
    public float CurrentHp
    {
        get { return currentHp; }
        set
        {
            currentHp = value;
            if (currentHp > Hp)
            {
                currentHp = Hp;
            }
            if (currentHp <= 0)
            {
                currentHp = 0;
                OnDie?.Invoke();

            }
            OnChanged?.Invoke();

        }

    }
    [SerializeField]
    private float damage;
    private float extraDamage;

    [SerializeField]
    private float speed;
    private float extraSpeed;

    public float attackSpeed;

    public float originalSpeed
    {
        get { return speed; }
    }
    public float Speed
    {
        get { return Mathf.Max(0, speed + extraSpeed); }
    }
    public float Damage
    {
        get { return Mathf.Max(0, damage + extraDamage); }
    }

    public delegate void StatHandler();
    public event StatHandler OnDie;//죽었을 때
    public event StatHandler OnChanged;//hp가 변경되었을 때
    public void Init()
    {
        CurrentHp = Hp;
        extraSpeed = 0;
        extraDamage = 0;
    }
    public void AddExtraSpeed(float sp) {
        if (sp == 0) extraSpeed = 0;
        else extraSpeed += sp;
    }
    public float GetMaxHP()
    {
        return Hp;
    }
}
