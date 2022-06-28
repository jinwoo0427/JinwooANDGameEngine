using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IAgentInput
{
    public UnityEvent<Vector3> OnMovementKeyPress { get; set; }
    
    public UnityEvent OnFireButtonPress { get; set; }
    public UnityEvent OnFireButtonRelease { get; set; }
    
}
