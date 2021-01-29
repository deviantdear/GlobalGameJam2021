using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{
    public static QuestTracker Instance { get; set; }
    private Dictionary<string, IQuest> quests = new Dictionary<string, IQuest>();
    public ReadOnlyDictionary<string, IQuest> Quests
    {
        get => new ReadOnlyDictionary<string, IQuest>(quests);
    }
    
    public Dictionary<string, IQuest> AvailableQuests
    {
        get => FindAvailableQuests(quests);
    }

    public Dictionary<string, IQuest> CompletedQuests
    {
        get => FindCompletedQuests(quests);
    }
    
    void Awake()
    {
        if (!QuestTracker.Instance)
            Instance = this;
        else
            Destroy(this);
    }

    /// <summary>
    /// Attach quest refrence to the tracker
    /// </summary>
    /// <param name="quest"></param>
    public static void AddRefrence(IQuest quest)
    {
        if (quest == null)
            return;
        Instance.quests.Add(quest.Name, quest);
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
