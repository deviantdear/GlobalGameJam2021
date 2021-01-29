using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOption : MonoBehaviour, IQuestOption
{
    #region InspectorFields

    [SerializeField] private IQuest quest;
    [SerializeField] private string title;
    [SerializeField] private bool available;
    [SerializeField] private bool completeQuest = false;
    [SerializeField] private bool failQuest = false;

    #endregion
    #region PropertyContainers
    
    private Action _onSelect;

    #endregion

    #region MonoBehaviorTriggers

    private void Start()
    {
        if (available)
            quest.QuestOptions.Add(this);
    }

    #endregion

    #region Properties

    
    public string Title
    {
        get => title;
        set => title = value;
    }

    public Action OnSelect
    {
        get => _onSelect;
        set => _onSelect = value;
    }

    public IQuest Quest
    {
        get => quest;
        set => quest = value;
    }

    public bool Available
    {
        get => available;
        set
        {
             available = value;
             if (available)
             {
                 
                 if (!Quest.QuestOptions.Contains(this))
                     Quest.QuestOptions.Add(this);
             }
             else
             {
                 if (Quest.QuestOptions.Contains(this))
                     Quest.QuestOptions.Remove(this);
             }
        }
    }

    public void Select()
    {
        if (completeQuest)
            quest.Completed = true;
        if (failQuest)
            quest.Failed = true;
        OnSelect?.Invoke();
    }

    #endregion

}
