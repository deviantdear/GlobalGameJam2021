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
    /// Marks the quest as available
    /// </summary>
    bool Available { get; set; }
    /// <summary>
    /// Marks the quest as completed
    /// </summary>
    bool Completed { get; set; }
    /// <summary>
    /// Triggers when quest is begun
    /// </summary>
    Action OnBegin { get; set; }
    /// <summary>
    /// Triggers when the quest becomes available
    /// </summary>
    Action OnAvailable { get; set; }
    /// <summary>
    /// Triggers when the quest completes. Bool property is true if the quest was completed successfully.
    /// True = Quest completed
    /// False = Failed quest
    /// </summary>
    Action<bool> OnComplete { get; set; }
    
}
