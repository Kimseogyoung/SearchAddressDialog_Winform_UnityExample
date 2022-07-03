using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : ScanObject
{
    enum Treasure
    {
        Gold,Item
    }
    public float[] treasureProbs = {80.0f,20.0f};
    public float[] goldProbs = { 50f, 40f, 30f, 20f, 10f};



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Component()
    {
        int random= (int)Util.Choose(treasureProbs);
        if (random == (int)Treasure.Gold)
        {
            random= (int)Util.Choose(goldProbs);
            GameManager.Instance.Gold += (random +1)* 100;
        }
        else if(random == (int)Treasure.Item)
        {

        }

        Destroy(gameObject);
    }
   
}
