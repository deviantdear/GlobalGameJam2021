using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOption : MonoBehaviour, IQuestOption
{
    #region InspectorFields

    [SerializeField] private Quest quest;
    [SerializeField] private string title;
    [SerializeField] private bool available;
    [SerializeField] private bool completeQuest = false;
    [SerializeField] private bool failQuest = false;
    [SerializeField] private bool cancelQuest = false;
    [SerializeField] private bool setQuestUnavailable = false;

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

    public virtual void Select()
    {
        if (completeQuest)
            quest.Completed = true;
        if (failQuest)
            quest.Failed = true;
        if (cancelQuest)
            quest.Active = false;
        if (setQuestUnavailable)
            quest.Available = false;
        OnSelect?.Invoke();
    }

    #endregion

}
