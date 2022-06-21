using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SA_Dialog;
public class AddressSearcher : ScanObject
{ 

    private FormManager fm;


    // Start is called before the first frame update
    void Start()
    {
        FormManager.SetAPIKey("09b39522763fe51b9386d5ed328d82a5");
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
        if(board!=null)
            board.SetMap(lc);   
    }
    // Update is called once per frame
    void Update()
    {

    }
}
