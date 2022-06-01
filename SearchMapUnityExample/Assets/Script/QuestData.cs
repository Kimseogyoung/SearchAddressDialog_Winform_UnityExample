using System.Collections;
using System.Collections.Generic;

public class QuestData
{

    public string questName;
    public int reward;

    public List<string> startDialog;
    public List<string> normalDialog;
    public List<string> endDialog;

    public int conditionId;
    public int conditionCount;
    public QuestData(string name, int re ,int targetid,int targetcount)
    {

        questName=name;
        reward = re;
        startDialog = new List<string>();
        normalDialog = new List<string>();
        endDialog = new List<string>();
        conditionId =targetid;
        conditionCount=targetcount;
}
    
    
}
