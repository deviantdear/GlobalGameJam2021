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
    /// Triggers when quest is active
    /// </summary>
    Action OnActive { get; set; }
    /// <summary>
    /// Triggers when the quest becomes available
    /// </summary>
    Action OnAvailable { get; set; }
    /// <summary>
    /// Triggers when the quest completes
    /// </summary>
    Action OnComplete { get; set; }
}
