using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : ScanObject
{
    public float[] enemyProbs = { 50.0f, 50.0f };
    public float spawnTerm = 1.0f;
    public int spawnCnt = 0;
    public int level;

    public List<Enemy> enemies;
    private bool isUsed;
    // Start is called before the first frame update
    void Start()
    {
        
        spawnTerm = 1.0f;
        spawnCnt = 10;
        level = 1;
        isUsed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Component()
    {
        if (isUsed == true) return;

        isUsed = true;
        int random = Util.Choose(enemyProbs);
        enemies = new List<Enemy>();
        //EnemyType에 따라 소환
        //코루틴 실행
        StartCoroutine(Spawn((Enemy.Type)random));

    }
    IEnumerator Spawn(Enemy.Type et) 
    {
        int cnt = 0;
        while (spawnCnt > cnt)
        {
            //enemy prefab 생성
            GameObject obj = Instantiate(Resources.Load("Prefabs/" + et.ToString()) as GameObject);
            Enemy enemy = obj.GetComponent<Enemy>();

            //주변 랜덤 좌표에 생성
            //아직 안바꿈
            enemy.gameObject.transform.position = transform.position+Vector3.up*2;


            enemies.Add(enemy);

            //enemy 처치 이벤트 리스너 등록
            enemy.OnDie += RemoveEnemyList;

            cnt++;
            yield return new WaitForSeconds(spawnTerm);
        }
        yield return 0;
    }
    void RemoveEnemyList(Enemy enemy)
    {
        enemies.Remove(enemy);
        enemy.OnDie-= RemoveEnemyList;
    }
}
