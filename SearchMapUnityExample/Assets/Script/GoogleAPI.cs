using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA_Dialog;
public class GoogleAPI : ScanObject
{ 
    [SerializeField]
    private string appkey;

    public Material material;
    string url;
    public float lat;
    public float lon;

    LocationInfo li;

    public int zoom = 14;
    public int mapWidth = 640;
    public int mapHeight = 640;
    public enum mapType { roadmap, satellite, hybrid, terrain }
    public mapType mapSelected;
    public int scale;
    private FormManager fm;

    IEnumerator LoadMap(Locale lc)
    {
        url = "https://maps.googleapis.com/maps/api/staticmap?center="+lc.lat+","+lc.lng+"&zoom=18&size=1100x1400&key="+appkey;
        
        WWW www = new WWW(url);
        yield return www;
        material.mainTexture = www.texture;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        fm = new FormManager();
        fm.OnExit += OnGetResult;
    }
    public override void Component()
    {
        if(fm!=null)
            fm.OpenSearchForm();
        else
            fm = new FormManager();
    }
    void OnGetResult(Locale lc)
    {
        StartCoroutine(LoadMap(lc));
    }
    // Update is called once per frame
    void Update()
    {

    }
}
