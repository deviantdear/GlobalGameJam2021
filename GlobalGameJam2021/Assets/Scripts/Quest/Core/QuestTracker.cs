using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{
    public static QuestTracker Instance;
    private Dictionary<string, IQuest> quests = new Dictionary<string, IQuest>();
    public ReadOnlyDictionary<string, IQuest> Quests
    {
        get => new ReadOnlyDictionary<string, IQuest>(quests);
    }
    
    public Dictionary<string, IQuest> AvailableQuests
    {
        get => FindAvailableQuests(quests);
    }
    
    public Dictionary<string, IQuest> ActiveQuests
    {
        get => FindActiveQuests(quests);
    }

    public Dictionary<string, IQuest> CompletedQuests
    {
        get => FindCompletedQuests(quests);
    }

    public Action onQuestUpdated = null;

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
    }

    /// <summary>
    /// Attach quest reference to the tracker
    /// </summary>
    /// <param name="quest"></param>
    public static void AddReference(IQuest quest)
    {
        if (quest == null)
            return;
        Instance.quests.Add(quest.Name, quest);
    }

    public void QuestUpdated(IQuest quest)
    {
        onQuestUpdated?.Invoke();
    }

    Dictionary<string, IQuest> FindAvailableQuests(Dictionary<string, IQuest> input)
    {
        Dictionary<string, IQuest> response = new Dictionary<string, IQuest>();
        foreach (var item in input)
        {
            if (item.Value == null)
                continue;
            if (item.Value.Available)
                response.Add(item.Key, item.Value);
        }

        return response;
    }

    Dictionary<string, IQuest> FindActiveQuests(Dictionary<string, IQuest> input)
    {
        Dictionary<string, IQuest> response = new Dictionary<string, IQuest>();
        foreach (var item in input)
        {
            if (item.Value == null)
                continue;
            if (item.Value.Active)
                response.Add(item.Key, item.Value);
        }

        return response;
    }

    Dictionary<string, IQuest> FindCompletedQuests(Dictionary<string, IQuest> input)
    {
        Dictionary<string, IQuest> response = new Dictionary<string, IQuest>();
        foreach (var item in input)
        {
            if (item.Value == null)
                continue;
            if (item.Value.Completed)
                response.Add(item.Key, item.Value);
        }

        return response;
    }

}
