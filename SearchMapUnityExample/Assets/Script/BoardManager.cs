using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA_Dialog;

public class BoardManager : MonoBehaviour
{
    private Vector2 pos;
    public Vector2 Pos
    {
        //get { return pos; }
        set { 
            pos = value;
            GameManager.Instance.Pos = pos;
        }
    }

    public Board[,] boards;
    public float scale;

    public int size;
    public float spacing;
    public float targetSpace;
    public Locale centerLc;
    public Locale globalLc;

    public int[,] aroundB;//상하좌우 보드

    private Vector2[] dirs;
    private Vector2 preBoardPos;//이전에설치된 preBoard 위치
    private GameObject preBoard;//이전에설치된 preBoard 

    //  0.00272, 0.00345
    // Start is called before the first frame update
    void Start()
    {

    }
    public void Init()
    {
        Pos = new Vector2(0, 0);

        centerLc = new Locale("", 0, 0);
        //위도 경도 거리
        globalLc = new Locale("", 0.00272f, 0.00345f);

        preBoardPos = new Vector2(-1, -1);//초기화
        boards = new Board[size, size];
        aroundB = new int[3, 3];

        dirs = new Vector2[4];
        dirs[0] = Vector2.up;
        dirs[1] = Vector2.down;
        dirs[2] = Vector2.left;
        dirs[3] = Vector2.right;

        AddBoard(size / 2, size / 2);
        SetCurrentPos(new Vector2(size / 2, size / 2));
        Player.Instance.OnChangedPos += SetCurrentPos;
    }
    public void Reset()
    {
        for (int i=0; i<size; i++)
        {
            for(int j=0; j<size; j++)
            {
                if(boards[i, j] != null)
                {
                    boards[i, j].Destroy();
                }
            }
        }
        for (int i=0; i<3; i++)
        {
            for(int j=0; j<3; j++)
            {
                aroundB[i, j] = 0;
            }
        }
        Pos = new Vector2(0, 0);

        centerLc.lat = 0;
        centerLc.lng = 0;

        preBoardPos.x = -1;
        preBoardPos.y = -1;

        dirs[0] = Vector2.up;
        dirs[1] = Vector2.down;
        dirs[2] = Vector2.left;
        dirs[3] = Vector2.right;

        AddBoard(size / 2, size / 2);
        SetCurrentPos(new Vector2(size / 2, size / 2));
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {//확인버튼
            if (preBoard != null)
            {//preBoard가 존재한다면 보드 설치
                AddBoard();
            }
        }
        if (Player.Instance != null)
        {
            //1. 근접한 방향계산 
            Vector3 dir_p = Player.Instance.transform.forward;
            dir_p.y = 0;
            float min = Vector3.Distance(Vector3.forward, dir_p);
            Vector3 dir = Vector3.forward;

            if (min >= Vector3.Distance(Vector3.back, dir_p))
            {
                min = Vector3.Distance(Vector3.back, dir_p);
                dir = Vector3.back;
            }
            if (min >= Vector3.Distance(Vector3.left, dir_p))
            {
                min = Vector3.Distance(Vector3.left, dir_p);
                dir = Vector3.left;
            }
            if (min >= Vector3.Distance(Vector3.right, dir_p))
            {
                min = Vector3.Distance(Vector3.right, dir_p);
                dir = Vector3.right;
            }

            //2. 해당 방향 거리계산
            //size초과한  보드라면
            int state = aroundB[(int)dir.x + 1, (int)dir.z + 1];
            if (state == -1) return;

            //빈칸이거나, 이미 추가된 보드가 있다면
            //거리 계산
            //  만약 dir이 x=0 z=1이면 상향 방향인데, 이때 z축 기준으로만 거리를 계산해야함.
            //  = 방향 벡터에서 1인 값 참고하여 거리계산 = 벡터 내적 연산
            int plus = (int)Vector3.Dot(Vector3.one, dir);
            float target = plus * Vector3.Dot(boards[(int)pos.y, (int)pos.x].transform.position, dir) +
                ((scale / 2) + spacing) * plus;
            float current = plus * Vector3.Dot(Player.Instance.transform.position, dir);

            if (state == 0 && Mathf.Abs(target - current) <= spacing + targetSpace)
            {//해당 방향 보드가 빈칸이라면 && 특정 거리 안이라면
                CreatePreBoard((int)(-dir.z + pos.y), (int)(dir.x + pos.x));
            }
            else if (state == 1)
            {//있다면 
                //해당 보드로 넘어가는지 체크

            }
            else
            {
                if (preBoard != null)
                {//초기화되지않음
                    Destroy(preBoard);
                    preBoardPos.x = -1;
                    preBoardPos.y = -1;
                }
            }


        }


    }
    public void SetCurrentPos(Vector2 v)
    {
        Pos = v;
        int r = (int)v.y;
        int c = (int)v.x;

        if (r < 0 || c < 0 || r >= size || c >= size)
        {//범위 밖을 설정하려고 할 때 
            return;
        }

        int newr = r;
        int newc = c;

        foreach (Vector2 d in dirs)
        {           //주변 네개 어떤지 보고
                    //없는칸이라면 예외 -1
                    //이미 있는 칸이라면 표시 1
                    //빈칸이라면 0
            newr = r - (int)d.y;
            newc = c + (int)d.x;


            if (newr < 0 || newr >= size || newc < 0 || newc >= size)
            {
                aroundB[(int)d.x + 1, (int)d.y + 1] = -1;
            }
            else if (boards[newr, newc] != null)
            {
                aroundB[(int)d.x + 1, (int)d.y + 1] = 1;
            }
            else
            {
                aroundB[(int)d.x + 1, (int)d.y + 1] = 0;
            }
            //Debug.Log("방향" + ((int)d.x + 1) + " " + ((int)d.y + 1) + "=" + aroundB[(int)d.x + 1, (int)d.y + 1]
            //    +"좌표"+ newr+" "+ newc);
        }

    }
    public void CreatePreBoard(int r, int c)
    {
        if (GameManager.Instance.isEnemyMode == true) return; //싸움중이라면 리턴
        if (preBoardPos.x == c && preBoardPos.y == r) return;//같은위치라면 리턴
        if (preBoard != null)
        {
            Debug.Log("삭제");
            Destroy(preBoard);
        }
        Debug.Log("생성");
        preBoardPos.x = c;
        preBoardPos.y = r;
        preBoard = Instantiate(Resources.Load("Prefabs/BoardPre") as GameObject, transform);
        preBoard.transform.position = new Vector3(scale * (c - size / 2) + spacing * (c - size / 2), 0,
            -(scale * (r - size / 2) + spacing * (r - size / 2)));
    }
    public void AddBoard(int r, int c, ScanObject.Type type = 0)
    {//특정 위치에 보드 생성할 때, ex) 센터 보드
        GameObject obj = Instantiate(Resources.Load("Prefabs/Board") as GameObject, transform);


        obj.transform.position = new Vector3(scale * (c - size / 2) + spacing * (c - size / 2), 0,
            -(scale * (r - size / 2) + spacing * (r - size / 2)));


        Board b = (obj.GetComponent<Board>());
        b.SetObject(type);
        b.OnChanged += ReSetLocale;


        //공통부분
        b.Pos = new Vector2(c, r);
        boards[r, c] = b;
        GameManager.Instance.BoardCnt += 1;
    }
    public void AddBoard()
    {//PreBoard가 있을때
        GameObject obj = Instantiate(Resources.Load("Prefabs/Board") as GameObject, transform);

        if (preBoard != null)
        {
            obj.transform.position = preBoard.transform.position;
            Destroy(preBoard);

            //있는 칸으로 표시
            aroundB[(int)(preBoardPos.x - pos.x + 1), (int)(pos.y - preBoardPos.y + 1)] = 1;
            Debug.Log((int)(preBoardPos.x - pos.x + 1) + " " + (int)(pos.y - preBoardPos.y + 1));

            Board b = (obj.GetComponent<Board>());
            //맵 세팅
            
            b.SetMap(centerLc.lat - globalLc.lat * (preBoardPos.y - size / 2),
                centerLc.lng + globalLc.lng * (preBoardPos.x - size / 2));


            //공통 부분
            b.Pos = new Vector2((int)(preBoardPos.x), (int)(preBoardPos.y));
            boards[(int)(preBoardPos.y), (int)(preBoardPos.x)] = b;
            GameManager.Instance.BoardCnt += 1;
            b.SetObject((ScanObject.Type)Util.Choose(new float[] {0f,5f,10f,0f }));



            preBoardPos.x = -1;
            preBoardPos.y = -1;

            //랜덤 오브젝트 생성
            //b.SetObject(type);

        }
    }
    public void ReSetLocale(Locale lc)
    {
        centerLc = lc;

        //센터 제외 나머지 보드 삭제
        //관련 변수 초기화
    }
    public void DeleteBoard(int r, int c)
    {

    }
    
}
