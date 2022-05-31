using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Windows.Forms;
using SA_Dialog;
public class GoogleAPI : MonoBehaviour
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

    IEnumerator Map(Locale lc)
    {
        url = "https://maps.googleapis.com/maps/api/staticmap?center="+lc.lat+","+lc.lng+"&zoom=18&size=1100x1400&key="+appkey;
        WWW www = new WWW(url);
        yield return www;
        material.mainTexture = www.texture;

    }
    // Start is called before the first frame update
    void Start()
    {
        
        FormManager.SetAPIKey("09b39522763fe51b9386d5ed328d82a5");
        FormManager fm = new FormManager();
        fm.OnExit += OnGetResult;
        fm.OpenSearchForm();
    }

    void OnGetResult(Locale lc)
    {
        Debug.Log("asd");
        StartCoroutine(Map(lc));
}
    // Update is called once per frame
    void Update()
    {

    }
}
