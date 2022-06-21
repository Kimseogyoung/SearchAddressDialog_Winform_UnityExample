using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Text playerName;
    public Text money;
    public GameObject questList;
    public GameObject noticeTap;
    public GameObject chatTap;

    public Text questName;
    public Text questCondition;

    public Text npcText;
    public Text chatText;

    public void UpdateTalkPanel(string npc, string text)
    {
        npcText.text = npc;
        chatText.text = text;
    }

    public void Updatemoney(int m)
    {
        money.text = "가진 돈 : "+m+"";
    }
    public void OpenQuestList()
    {   
        if(questList.activeSelf)
            questList.SetActive(false);
        else
            questList.SetActive(true);
    }
    public void UpdateQuestTap(int ising,string name,int cur,int target)
    {
        if (ising == 0) {
            questName.text = "no quest";
            questCondition.text ="" ;
        }
        else
        {
            questName.text = name;
            questCondition.text = cur + " / " + target;
        }
        
    }
}
