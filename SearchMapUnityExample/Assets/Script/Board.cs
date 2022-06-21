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

    public int scale;


    private Material material;

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
        StartCoroutine(LoadMap(lc));
    }
    IEnumerator LoadMap(Locale lc)
    {
        url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lc.lat + "," + lc.lng + "&zoom=18&size=1100x1400&key=" + appkey;

        WWW www = new WWW(url);
        yield return www;
       
        material.mainTexture = www.texture;
    }
}
