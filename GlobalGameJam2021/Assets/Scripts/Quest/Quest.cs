using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Quest : MonoBehaviour, IQuest
{
    #region InspectorFields
    
    [SerializeField] private string summary;
    [SerializeField] private string subject;
    [SerializeField] private string description;
    [SerializeField] private bool available;
    [SerializeField] private bool completed;
    [SerializeField] private bool active;
    [SerializeField] private bool failed;
    #endregion

    #region PropertyContainers
    
    private Action _onAvailable;
    private Action _onBegin;
    private Action _onComplete;
    private Action _onActive;
    private List<IQuestOption> _questOptions = new List<IQuestOption>();
    private Action _onFailed;
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
                if(available)
                    OnAvailable?.Invoke();
            }
        }

        public bool Active
        {
            get => active;
            set
            {
                active = value;
                if (active)
                    OnActive?.Invoke();
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
                if(completed)
                    OnComplete?.Invoke();
            }
        }

        public bool Failed
        {
            get => failed;
            set 
            {
                failed = value;
                Active = false;
                if(failed)
                    OnFailed?.Invoke();
            }
        }

        public Action OnAvailable
        {
            get => _onAvailable;
            set => _onAvailable = value;
        }

        public Action OnActive
        {
            get => _onActive;
            set => _onActive = value;
        }

        public Action OnComplete
        {
            get => _onComplete;
            set => _onComplete = value;
        }

        public Action OnFailed
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
