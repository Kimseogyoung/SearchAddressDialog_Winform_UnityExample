using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{
    public QuestManager questmanager;
    public UIManager uimanager;

    public int curQuestId = 1;
    public int curCondition = 0;
    public int questState = 0; //0안함 1하는중 2완료
    public bool isTalk = false;
    public int money = 0;

 
    public GameObject mark1;//!
    public GameObject mark2;//?

    private int talkIndex=0;
    
    private int questCount = 0;

    public void CheckQuestCondition(int objId)
    {
        if (questState==1)
        {
            if (objId == questmanager.getConditionId(curQuestId)&&(questmanager.getConditionCount(curQuestId)>curCondition))
            {
                Debug.Log("미션 하는중");
                curCondition++;
                if (IsQuestComplete()) { mark2.SetActive(true); questState = 2; }
            }
            uimanager.UpdateQuestTap(questState,questmanager.getName(curQuestId), curCondition, questmanager.getConditionCount(curQuestId));

        }
    }
    public void EnemyAttack(ObjectData objData, int power)
    {
        if (objData.id >= 1000 && objData.id<10000)
        {
            objData.curhp = objData.curhp - power;
            if (objData.curhp <= 0)
            {
                CheckQuestCondition(objData.id);
                objData.DieAndRespown();
                
            }
        }

    }
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
}
