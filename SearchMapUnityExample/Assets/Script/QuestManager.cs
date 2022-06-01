using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    //오브젝트 아이디 10000:npc 1000:몬스터 100:물체 10:장소
    //               ex)튜토리얼 npc 10000, 샌드백 1000, 두번쨰 npc 20000

    Dictionary<int, QuestData> questDatas;

    void Awake()
    {
        questDatas = new Dictionary<int, QuestData>();
        generateQuest();


    }
    public void generateQuest()
    {
        //퀘스트 튜토리얼: 지정장소까지 이동하자
        questDatas.Add(1,new QuestData("튜토리얼:이동", 1000,10, 1));
        questDatas[1].startDialog.AddRange(new string[] { "처음온거야?", "wasd키를 사용해 이동하고 방향전환, space를 누르면 점프할 수 있어.", "연습하는겸 표시된 곳 까지 다녀와봐." });
        questDatas[1].normalDialog.AddRange(new string[] { "어서 움직여봐." });
        questDatas[1].endDialog.AddRange(new string[] { "수고했어. 마우스클릭으로 나한테 다시 말을 걸어봐." });

        //1퀘스트 튜토리얼: 주먹으로 공격하는법
        questDatas.Add(2,new QuestData("튜토리얼:주먹공격", 1000,1000,3));
        questDatas[2].startDialog.AddRange(new string[] { "이제 공격하는법을 알려줄게, q키를 누르면 공격할 수 있어.", "내 옆에있는 연습용 샌드백을 공격해봐!", "2번 때리면 사라질거야" });
        questDatas[2].normalDialog.AddRange(new string[] { "화이팅!" });
        questDatas[2].endDialog.AddRange(new string[] { "잘했어. 쉽지?" });


        //2퀘스트 튜토리얼: 무기체인지
        questDatas.Add(3, new QuestData("튜토리얼:무기전환", 1000, 1000, 3));
        questDatas[3].startDialog.AddRange(new string[] { "사실 주먹말고 칼로도 공격할 수 있어 ㅎㅎ.", "1키를 누르면 무기를 칼로 바꿀 수 있어. 다시 칼로 연습용 샌드백을 3마리 잡아줘!" });
        questDatas[3].normalDialog.AddRange(new string[] { "슉슉!" });
        questDatas[3].endDialog.AddRange(new string[] { "좋아!" });
        
        //3퀘스트 튜토리얼: 아이템 줍기
        questDatas.Add(4, new QuestData("튜토리얼:아이템 줍기", 1000, 100, 1));
        questDatas[4].startDialog.AddRange(new string []{ "마지막이야! 특정 물건앞에서 e키를 누르면 인벤토리에 물건을 넣을 수 있어.", "마침 잃어버린게 있는데 잘됐다!~", "내 잃어버린 모자를 찾아와줘 ㅎㅎ" });
        questDatas[4].normalDialog.AddRange(new string []{ "내가 제일 좋아하는 모자거든~ 빨리 가져와줘" });
        questDatas[4].endDialog.AddRange(new string []{ "고마워!", "이제 게임을 시작해볼까?" });


        
    }

    public string getName(int id) { return questDatas[id].questName; }
    public int getReward(int id) { return questDatas[id].reward;  }
    public int getConditionId(int id) { return questDatas[id].conditionId; }
    public int getConditionCount(int id) { return questDatas[id].conditionCount; }



    public string getTalk(int id,int num,int talkIndex)
    {
        string talktext=null;
        switch(num){
            case 0:
                if (talkIndex != questDatas[id].startDialog.Count)
                    talktext = questDatas[id].startDialog[talkIndex];
                break;
            case 1:
                if (talkIndex != questDatas[id].normalDialog.Count)
                    talktext = questDatas[id].normalDialog[talkIndex];
                break;
            case 2:
                if (talkIndex != questDatas[id].endDialog.Count)
                    talktext = questDatas[id].endDialog[talkIndex];
                break;
        }
        return talktext;
    }


}
