using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA_Dialog;

[System.Serializable]
public class Board : MonoBehaviour
{
    [SerializeField]
    private string appkey;
    private ScanObject mainObject;

    public Locale locale;

    public float size;
    public bool isCleared;


    string url;
    public float lat;
    public float lon;

    public int zoom = 14;
    public int mapWidth = 640;
    public int mapHeight = 640;

    public  Vector2 pos;
    public Vector2 Pos{
        get{ return pos; }
        set{ pos = value; }
    }


    private Material material;

    public delegate void MapHandler(Locale lc);//center board가 맵설정되었을때 호출
    public event MapHandler OnChanged;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void SetObject(ScanObject.Type type)
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/"+type.ToString()) as GameObject);
        obj.transform.position = transform.position;
        obj.transform.Translate(Vector3.up*5);
        obj.transform.SetParent(transform);

        mainObject = obj.GetComponent<ScanObject>();
        mainObject.SetBoard(this);
    }
    public void SetMap(Locale lc)
    {
        locale = lc;
        StartCoroutine(LoadMap(lc));
        OnChanged?.Invoke(locale);
    }
    public void SetMap(float lat,float lng)
    {
        locale = new Locale("name",lat,lng);
        
        StartCoroutine(LoadMap(locale));
    }
    IEnumerator LoadMap(Locale lc)
    {
        url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lc.lat + "," + lc.lng + "&zoom=18&size=1100x1400&key=" + appkey;
        Debug.Log(lc.lat + " " + lc.lng);
        WWW www = new WWW(url);
        yield return www;
        //37.61300 127.00700                
        //37.61572 127.01045      
        material.mainTexture = www.texture;
    }
}
