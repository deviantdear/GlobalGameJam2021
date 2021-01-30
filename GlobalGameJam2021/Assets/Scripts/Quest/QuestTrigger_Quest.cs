using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger_Quest : QuestTrigger
{
    [SerializeField] private Quest watchedQuest = null;
    [SerializeField] private QuestTriggerOption triggerOnAction = QuestTriggerOption.SetCompleted;
    [SerializeField] private bool triggerOnActionState = true;

    private void Start()
    {
        switch (triggerOnAction)
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

    void Trigger(bool state)
    {
        if(triggerOnActionState == state)
            Trigger();
    }

    private void OnDestroy()
    {
        switch (triggerOnAction)
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
