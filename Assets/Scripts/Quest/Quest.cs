using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest 
{
    public enum QuestType { Gathering, Talk, Reach };
    public enum QuestStatus { Waiting, Accepted, Completed };

    public string questName;
    public QuestType questType;
    public QuestStatus questStatus;
}
