using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrackerUI : MonoBehaviour
{
    [SerializeField] private QuestItemUI questItemUIPrefab = null;
    [SerializeField] private Transform activeQuestListContainer = null;
    [SerializeField] private Dictionary<IQuest, QuestItemUI> spawnedUi = new Dictionary<IQuest, QuestItemUI>();

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
        foreach (var item in updateArgs.Activated)
        {
            if (item.Available && !spawnedUi.ContainsKey(item))
            {
                spawnedUi.Add(
                    item, 
                    Instantiate(questItemUIPrefab, activeQuestListContainer));
                spawnedUi[item].Set(item);
            }
            else if(!item.Available && spawnedUi.ContainsKey(item))
            {
                RemoveItem(item);
            }
        }
        
        foreach (var item in updateArgs.Completed)
            RemoveItem(item);
        foreach (var item in updateArgs.Failed)
            RemoveItem(item);
    }

    void RemoveItem(IQuest item)
    {
        if(!spawnedUi.ContainsKey(item))
            return;
        Destroy(spawnedUi[item].gameObject);
        spawnedUi.Remove(item);
    }

    void ClearContainer(Transform container)
    {
        foreach (var item in spawnedUi)
        {
            Destroy(item.Value.gameObject);
        }
    }
    
}
