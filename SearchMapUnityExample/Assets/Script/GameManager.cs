using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : Singleton<GameManager>
{
    public BoardManager boardManager;

    public StoreUI storeUI;
    public GameUI gameUI;
    

    private int boardCnt;
    private float gold;
    private Vector2 pos;

    public bool isEnemyMode;
    

    public float Gold
    {
        get { return gold; }
        set { 
            gold = value;
            gameUI.UpdateGold(gold);
        }
    }
    public int BoardCnt
    {
        get { return boardCnt; }
        set
        {
            boardCnt = value;
            gameUI.UpdateBoardCnt(boardCnt);
        }
    }
    public Vector2 Pos
    {
        get { return pos; }
        set
        {
            pos = value;
            gameUI.UpdateCurrentPos(pos);
        }
    }

    private void Awake()
    {
        Init();
        boardManager.Init();
    }
    private void Init()
    {
        isEnemyMode = false;
        Gold = 0;
        BoardCnt = 0;
    }
    public void Reset()
    {
        boardManager.Reset();
        Player.Instance.transform.position = new Vector3(0, 2, 0);
        Init();
    }
    /*
    public UIManager uimanager;

    public int curQuestId = 1;
    public int curCondition = 0;
    public int questState = 0; //0안함 1하는중 2완료
    public bool isTalk = false;
    public int money = 0;

 
    public void EnemyAttack(ObjectData objData, int power)
    {
        if (objData.id >= 1000 && objData.id<10000)
        {
            objData.curhp = objData.curhp - power;
            if (objData.curhp <= 0)
            {
               // CheckQuestCondition(objData.id);
                objData.DieAndRespown();
                
            }
        }

    }
    */
    /*
    public void NpcTalk(ObjectData objData)
    {
        if (objData.id % 10000 == 0)
        {

            string talktext = questmanager.getTalk(curQuestId, questState,talkIndex);
            if (talktext == null)
            {
                
                isTalk = false;
                talkIndex = 0;
                if (questState == 0) { questState = 1; mark1.SetActive(false); }
                if (questState == 2) {//퀘스트완료 대화 후 실행

                    curCondition = 0;
                    mark2.SetActive(false);
                    money += questmanager.getReward(curQuestId);//보상
                    questState = 0; 
                    curQuestId++;
                    mark1.SetActive(true);
                }
                uimanager.Updatemoney(money);
                uimanager.UpdateQuestTap(questState, questmanager.getName(curQuestId), curCondition, questmanager.getConditionCount(curQuestId));


            }
            else
            {
                isTalk = true;
                talkIndex++;
            }
            uimanager.UpdateTalkPanel(objData.gameObject.name, talktext);
            uimanager.chatTap.SetActive(isTalk);
        }

    }
    public bool IsQuestComplete()
    {
        if (questmanager.getConditionCount(curQuestId) <= curCondition)
            return true;
        else
             return false;
    }
    */
}
