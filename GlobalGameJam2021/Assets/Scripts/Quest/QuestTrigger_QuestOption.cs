using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger_QuestOption : QuestTrigger
{
    [SerializeField] private QuestOption questOption = null;

    private void Start()
    {
        questOption.OnSelect += Trigger;
    }

    private void OnDestroy()
    {
        questOption.OnSelect -= Trigger;
    }
}
