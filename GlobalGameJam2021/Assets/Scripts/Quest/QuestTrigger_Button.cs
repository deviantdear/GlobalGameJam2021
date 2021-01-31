using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTrigger_Button : QuestTrigger
{
    [SerializeField] private Button questTriggerButton = null;

    private void Start()
    {
        questTriggerButton.onClick.AddListener(Trigger);
    }

    private void OnDestroy()
    {
        questTriggerButton.onClick.RemoveListener(Trigger);
    }
}
