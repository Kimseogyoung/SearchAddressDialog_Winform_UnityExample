using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : ScanObject
{
    public float[] enemyProbs = { 50.0f, 50.0f };
    public float spawnTerm = 1.0f;
    public int spawnCnt = 0;
    public int spawnedCnt = 0;
    public int level;

    public List<Enemy> enemies;
    private bool isUsed;
    // Start is called before the first frame update
    void Start()
    {
        spawnedCnt = 0;
        spawnTerm = 1.0f;
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
        board.MakeWall();
        GameManager.Instance.isEnemyMode = true;

    }
    IEnumerator Spawn(Enemy.Type et) 
    {
        spawnedCnt = 0;
        int cnt = 0;
        while (spawnCnt > cnt)
        {
            //enemy prefab 생성
            GameObject obj = Instantiate(Resources.Load("Prefabs/" + et.ToString()) as GameObject);
            Enemy enemy = obj.GetComponent<Enemy>();

            //주변 원 범위에 순차 생성
            float angle = cnt * Mathf.PI * 2 / spawnCnt;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 10f;
            enemy.gameObject.transform.position = transform.position+Vector3.up+pos;


            enemies.Add(enemy);

            //enemy 처치 이벤트 리스너 등록
            enemy.OnDie += RemoveEnemyList;

            cnt++;
            spawnedCnt++;
            yield return new WaitForSeconds(spawnTerm);
        }
        yield return 0;
    }
    void RemoveEnemyList(Enemy enemy)
    {
        spawnedCnt--;
        enemies.Remove(enemy);
        enemy.OnDie-= RemoveEnemyList;
        if (spawnedCnt <= 0)
        {
            //모두 물리쳤을 때
            GameManager.Instance.Gold += 500 * level;
            board.RemoveWall();
            gameObject.SetActive(false);
            GameManager.Instance.isEnemyMode = false;
        }
    }
    public override void Destroy()
    {
        
        board.RemoveWall();
        for (int i=0; i<enemies.Count; i++)
        {
            Destroy(enemies[i].gameObject);
            spawnedCnt--;
        }
        enemies.Clear();
        GameManager.Instance.isEnemyMode = false;
        base.Destroy();
    }
}
