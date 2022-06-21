using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Board[,] boards;
    public Vector2 pos;
    public int size;
    public float spacing;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Init()
    {
        pos = new Vector2(size/2, size/2);
        boards = new Board[size,size];
        AddBoard(size / 2, size / 2);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddBoard(int r, int c, ScanObject.Type type=0)
    {
        GameObject b=Instantiate(Resources.Load("Prefabs/Board") as GameObject, transform);
        (b.GetComponent<Board>()).SetObject(type);
    }
    public void DeleteBoard(int r, int c)
    {

    }
}
