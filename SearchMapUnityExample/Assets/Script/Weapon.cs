using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type
    {
        Normal, Sword
    }
    private Collider weaponCollider;
    [SerializeField]
    private float basepower=3;
    private float power=0;

    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private bool isRangeAttack;
    private int enemyCnt;

    private void Start()
    {
    }
    public void Init(float power)
    {
        weaponCollider = gameObject.GetComponent<Collider>();
        SetColliderOn(false);
        this.power = power;
    }
    public void SetColliderOn(bool on)
    {
        weaponCollider.enabled = on;
        enemyCnt = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!isRangeAttack && enemyCnt > 0) return;
            Enemy e = collision.gameObject.GetComponent<Enemy>();
            e.stat.CurrentHp -= (power+basepower);

            Debug.Log(e.name + " ÀÇ Ã¼·Â " + e.stat.CurrentHp);
            enemyCnt++;
        }
    }
}
