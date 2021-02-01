using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuest
{
    /// <summary>
    /// Quest name
    /// </summary>
    string Name { get; set; }
    /// <summary>
    /// Summary of the quest
    /// </summary>
    string Summary { get; set; }
    /// <summary>
    /// Subject of the quest
    /// </summary>
    string Subject { get; set; }
    /// <summary>
    /// Quest Description
    /// </summary>
    string Description { get; set; }
    /// <summary>
    /// Marks the quest as available
    /// </summary>
    bool Available { get; set; }
    /// <summary>
    /// Marks the quest as active
    /// </summary>
    bool Active { get; set; }
    /// <summary>
    /// Marks the quest as completed
    /// </summary>
    bool Completed { get; set; }
    /// <summary>
    /// Marks the quest as failed
    /// </summary>
    bool Failed { get; set; }
    /// <summary>
    /// Triggers when quest is active
    /// </summary>
    Action<bool> OnActive { get; set; }
    /// <summary>
    /// Triggers when the quest becomes available
    /// </summary>
    Action<bool> OnAvailable { get; set; }
    /// <summary>
    /// Triggers when the quest completes
    /// </summary>
    Action<bool> OnComplete { get; set; }
    /// <summary>
    /// Triggers when the quest is failed
    /// </summary>
    Action<bool> OnFailed { get; set; }
    /// <summary>
    /// Options added manually or later on as they are triggered.
    /// </summary>
    List<IQuestOption> QuestOptions { get; set; }
    /// <summary>
    /// Image that shows up in the dialog windows
    /// </summary>
    Sprite SubjectImage { get; set; }
    /// <summary>
    /// Object to point at
    /// </summary>
    Transform SubjectTransform { get; set; }
}
