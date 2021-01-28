using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Quest : MonoBehaviour, IQuest
{
    [SerializeField] private string summary;
    private Action _onAvailable;
    private Action _onBegin;
    private Action<bool> _onComplete;
    private bool _available;
    private bool _completed;
    /// <summary>
    /// The quests that will be enabled when player completes this one
    /// </summary>
    [SerializeField] private List<IQuest> nextQuests = new List<IQuest>();

    #region MonoBehaviorTriggers

        // Start is called before the first frame update
        void Start()
        {
            QuestTracker.AddRefrence(this);
        }
    
        // Update is called once per frame
        void Update()
        {
            
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

        public bool Available
        {
            get => _available;
            set => _available = value;
        }

        public bool Completed
        {
            get => _completed;
            set => _completed = value;
        }

        public Action OnAvailable
        {
            get => _onAvailable;
            set => _onAvailable = value;
        }

        public Action OnBegin
        {
            get => _onBegin;
            set => _onBegin = value;
        }

        public Action<bool> OnComplete
        {
            get => _onComplete;
            set => _onComplete = value;
        }
        
    #endregion

}
