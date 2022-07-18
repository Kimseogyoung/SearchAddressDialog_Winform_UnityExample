using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI boardCntText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI currentPosText;
    public Button resetBtn;

    // Start is called before the first frame update
    void Start()
    {
        resetBtn.onClick.AddListener(delegate { GameManager.Instance.Reset(); });   
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
