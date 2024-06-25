using UnityEngine;
using System.Collections;

public class Questable : MonoBehaviour
{
    public Quest quest;

    //this script will be called after the dialogue is ended
    public void DelegateQuest()
    {
        if(quest.questStatus == Quest.QuestStatus.Waiting)
        {
            Player.instance.questList.Add(quest);
        }
        else
        {
            Debug.Log(string.Format("Quest : {0} has been accepted already!", quest.questName));
        }

    }

}
