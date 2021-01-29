using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrackerUI : MonoBehaviour
{
    [SerializeField] private QuestItemUI questItemUIPrefab = null;
    [SerializeField] private Transform activeQuestList = null;

    private void Start()
    {
        QuestTracker.Instance.onQuestUpdated += UpdateActiveQuestList;
    }

    private void OnDestroy()
    {
        QuestTracker.Instance.onQuestUpdated -= UpdateActiveQuestList;
    }

    void UpdateActiveQuestList(QuestTracker.QuestUpdateArgs updateArgs)
    {
        ClearContainer(activeQuestList);
        foreach (var item in QuestTracker.Instance.ActiveQuests)
        {
            Instantiate(questItemUIPrefab, activeQuestList).Set(item.Value);
        }
    }

    void ClearContainer(Transform container)
    {
        foreach (Transform child in container)
        {
            Destroy(child);
        }
    }
    
}
