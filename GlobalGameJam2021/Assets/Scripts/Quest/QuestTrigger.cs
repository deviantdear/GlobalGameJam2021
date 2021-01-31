using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    [SerializeField] internal Quest quest = null;
    
    public enum QuestTriggerOption
    {
        SetAvailable,
        SetActive,
        SetCompleted,
        SetFailed
    }

    [SerializeField] internal QuestTriggerOption triggerAction = QuestTriggerOption.SetAvailable;
    [SerializeField] internal bool setState = true;

    public void Trigger()
    {
        switch (triggerAction)
        {
            case QuestTriggerOption.SetActive:
                quest.Active = setState;
                break;
            case QuestTriggerOption.SetAvailable:
                quest.Available = setState;
                break;
            case QuestTriggerOption.SetCompleted:
                quest.Completed = setState;
                break;
            case QuestTriggerOption.SetFailed:
                quest.Failed = setState;
                break;
        }
    }
}
