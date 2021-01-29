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
    #endregion

    #region PropertyContainers
    
    private Action _onAvailable;
    private Action _onBegin;
    private Action _onComplete;
    private bool _available;
    private bool _completed;
    private bool _active;
    private bool _failed;
    private Action _onActive;
    private List<IQuestOption> _questOptions = new List<IQuestOption>();
    private Action _onFailed;

    #endregion



    #region MonoBehaviorTriggers
        void Start()
        {
            QuestTracker.AddRefrence(this);
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
            get => _available;
            set
            {
                _available = value;
                if(_available)
                    OnAvailable?.Invoke();
            }
        }

        public bool Active
        {
            get => _active;
            set
            {
                _active = value;
                if (_active)
                    OnActive?.Invoke();
            }
        }

        public bool Completed
        {
            get => _completed;
            set
            {
                _completed = value;
                if(_completed)
                    OnComplete?.Invoke();
            }
        }

        public bool Failed
        {
            get => _failed;
            set 
            {
                _failed = value;
                if(_failed)
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
