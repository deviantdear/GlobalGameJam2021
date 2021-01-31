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

    public Action<QuestUpdateArgs> onQuestUpdated = null;

    public struct QuestUpdateArgs
    {
        public List<IQuest> Activated;
        public List<IQuest> Available;
        public List<IQuest> Completed;
        public List<IQuest> Failed;

        public bool UpdateAvailable()
        {
            return Activated.Count > 0 || Available.Count > 0 || Completed.Count > 0 || Failed.Count > 0;
        }

        public void Reset()
        {
            Activated = new List<IQuest>();
            Available = new List<IQuest>();
            Completed = new List<IQuest>();
            Failed = new List<IQuest>();
        }
    }

    private QuestUpdateArgs currentUpdates;
    

    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
        currentUpdates = new QuestUpdateArgs();
        currentUpdates.Reset();
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

        quest.OnActive += (state)=>Instance.ActiveUpdate(quest);
        quest.OnAvailable += (state) => Instance.AvailableUpdate(quest);
        quest.OnComplete += (state) => Instance.CompletedUpdate(quest);
        quest.OnFailed += (state) => Instance.FailedUpdate(quest);
    }

    void ActiveUpdate(IQuest quest)
    {
        if (currentUpdates.Activated == null)
            currentUpdates.Activated = new List<IQuest>();
        currentUpdates.Activated.Add(quest);
    }
    void AvailableUpdate(IQuest quest)
    {
        if (currentUpdates.Available == null)
            currentUpdates.Available = new List<IQuest>();
        currentUpdates.Available.Add(quest);
    }
    void CompletedUpdate(IQuest quest)
    {
        if (currentUpdates.Completed == null)
            currentUpdates.Completed = new List<IQuest>();
        currentUpdates.Completed.Add(quest);
    }
    void FailedUpdate(IQuest quest)
    {
        if (currentUpdates.Failed == null)
            currentUpdates.Failed = new List<IQuest>();
        currentUpdates.Failed.Add(quest);
    }

    private void LateUpdate()
    {
        if (currentUpdates.UpdateAvailable())
        {
            onQuestUpdated?.Invoke(currentUpdates);
            currentUpdates.Reset();
        }
        
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
