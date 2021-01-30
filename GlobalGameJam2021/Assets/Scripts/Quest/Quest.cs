using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Quest : MonoBehaviour, IQuest
{
    #region InspectorFields
    
    [SerializeField] private string subject;
    [Multiline(4)][SerializeField] private string summary;
    [TextArea(6, 12)][SerializeField] private string description;
    [SerializeField] private bool available;
    [SerializeField] private bool completed;
    [SerializeField] private bool active;
    [SerializeField] private bool failed;
    #endregion

    #region PropertyContainers
    
    private Action<bool> _onAvailable;
    private Action<bool> _onBegin;
    private Action<bool> _onComplete;
    private Action<bool> _onActive;
    private List<IQuestOption> _questOptions = new List<IQuestOption>();
    private Action<bool> _onFailed;
    #endregion



    #region MonoBehaviorTriggers
        void Start()
        {
            QuestTracker.AddReference(this);
            if (active)
                Active = true;
            if (available)
                Available = true;
        }
    #endregion

    #region PublicProperties

        public string Name
        {
            get => name;
            set => name = value;
        }
    
        public string Summary
        {
            get => summary;
            set => summary = value;
        }

        public string Subject
        {
            get => subject;
            set => subject = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }

        public bool Available
        {
            get => available;
            set
            {
                available = value;
                OnAvailable?.Invoke(active);
            }
        }

        public bool Active
        {
            get => active;
            set
            {
                active = value;
                OnActive?.Invoke(active);
            }
        }

        public bool Completed
        {
            get => completed;
            set
            {
                completed = value;
                Active = false;
                Available = false;
                OnComplete?.Invoke(completed);
            }
        }

        public bool Failed
        {
            get => failed;
            set 
            {
                failed = value;
                Active = false;
                OnFailed?.Invoke(failed);
            }
        }

        public Action<bool> OnAvailable
        {
            get => _onAvailable;
            set => _onAvailable = value;
        }

        public Action<bool> OnActive
        {
            get => _onActive;
            set => _onActive = value;
        }

        public Action<bool> OnComplete
        {
            get => _onComplete;
            set => _onComplete = value;
        }

        public Action<bool> OnFailed
        {
            get => _onFailed;
            set => _onFailed = value;
        }

        public List<IQuestOption> QuestOptions
        {
            get => _questOptions;
            set => _questOptions = value;
        }

        #endregion

}
