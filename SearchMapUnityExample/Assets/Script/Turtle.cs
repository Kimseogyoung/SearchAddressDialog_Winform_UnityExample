using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : Enemy
{
    public Transform attackPoint;
    private ObjectPool<Bullet> objectPool;
    protected override void Init()
    {
        base.Init();

        objectPool = new ObjectPool<Bullet>("turtleWeapon", Resources.Load("Prefabs/TurtleWeapon") as GameObject, 5, attackPoint);


    }
    protected override void Attack()
    {
        Bullet b = objectPool.Deque();
        b.Damage = stat.Damage;
        
        b.StartShoot(objectPool);

    }
}
