using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Collider swordCollider;
    private float power;

    private void Start()
    {
    }
    public void Init(float power)
    {
        swordCollider = gameObject.GetComponent<Collider>();
        this.power = power;
    }
    public void SetColliderOn(bool on)
    {
        swordCollider.enabled = on;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy e = collision.gameObject.GetComponent<Enemy>();
            e.stat.CurrentHp -= power;
            Debug.Log(e.name + " ÀÇ Ã¼·Â " + e.stat.CurrentHp);
        }
    }
}
