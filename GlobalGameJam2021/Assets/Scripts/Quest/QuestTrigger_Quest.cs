using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger_Quest : QuestTrigger
{
    [SerializeField] private Quest watchedQuest = null;
    [SerializeField] private QuestTriggerOption triggerOnState = QuestTriggerOption.SetCompleted;

    private void Start()
    {
        switch (triggerOnState)
        {
            case QuestTriggerOption.SetActive:
                watchedQuest.OnActive += Trigger;
                break;
            case QuestTriggerOption.SetAvailable:
                watchedQuest.OnAvailable += Trigger;
                break;
            case QuestTriggerOption.SetCompleted:
                watchedQuest.OnComplete += Trigger;
                break;
            case QuestTriggerOption.SetFailed:
                watchedQuest.OnFailed += Trigger;
                break;
        }
    }

    private void OnDestroy()
    {
        switch (triggerOnState)
        {
            case QuestTriggerOption.SetActive:
                watchedQuest.OnActive -= Trigger;
                break;
            case QuestTriggerOption.SetAvailable:
                watchedQuest.OnAvailable -= Trigger;
                break;
            case QuestTriggerOption.SetCompleted:
                watchedQuest.OnComplete -= Trigger;
                break;
            case QuestTriggerOption.SetFailed:
                watchedQuest.OnFailed -= Trigger;
                break;
        }
    }
}
