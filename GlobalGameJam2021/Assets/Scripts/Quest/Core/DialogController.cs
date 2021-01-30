using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    [SerializeField] public QuestDialogUI dialogUIPrefab = null;
    Queue<IQuest> questDialogQueue = new Queue<IQuest>();
    private QuestDialogUI _dialogUI = null;
    private IQuest _current = null;
    private void Start()
    {
        QuestTracker.Instance.onQuestUpdated += DialogWatcher;
    }

    void DialogWatcher(QuestTracker.QuestUpdateArgs args)
    {
        foreach (var item in args.Activated)
        {
            if (item.Active)
                OpenDialog(item);
            else
                CloseDialog(item);
        }

        foreach (var item in args.Completed)
        {
            CloseDialog(item);
        }

        foreach (var item in args.Failed)
        {
            CloseDialog(item);
        }
    }

    public void OpenDialog(IQuest quest)
    {
        if (_dialogUI == null)
            _dialogUI = Instantiate(dialogUIPrefab);

        if (!dialogUIPrefab.isActiveAndEnabled)
        {
            _current = quest;
            _dialogUI.Set(quest);
        }
        else
            questDialogQueue.Enqueue(quest);
    }

    public void CloseDialog(IQuest quest)
    {
        // Remove dialog from the queue
        if (questDialogQueue.Contains(quest))
        {
            Queue<IQuest> temp = new Queue<IQuest>();
            while (questDialogQueue.Count > 0)
            {
                var item = questDialogQueue.Dequeue();
                if (item == quest)
                    continue;
                temp.Enqueue(item);
            }
            questDialogQueue = temp;
        }
        
        // Open next quest, or close the panel if none others are available.
        if (_current == quest)
        {
            if (questDialogQueue.Count > 0)
            {
                var nextQuest = questDialogQueue.Dequeue();
                _current = nextQuest;
                _dialogUI.Set(nextQuest);
            }
            else
            {
                _current = null;
                _dialogUI.gameObject.SetActive(false);
            }
        }
    }
}
