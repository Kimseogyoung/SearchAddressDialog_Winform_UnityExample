using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI boardCntText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI currentPosText;
    public Image[] itemImages;

    public GameObject itemGridObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateBoardCnt(int cnt)
    {
        boardCntText.text = "board Count : " + cnt;
    }
    public void UpdateGold(float gold)
    {
        goldText.text = "gold : " + gold;
    }
    public void UpdateCurrentPos(Vector2 pos)
    {
        currentPosText.text = "current pos : x-" + pos.x + " y-" + pos.y;
    }
}
