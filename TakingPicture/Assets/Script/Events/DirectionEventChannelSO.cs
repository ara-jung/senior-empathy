using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[CreateAssetMenu(menuName = "Events/Direction Event Channel")]
public class DirectionEventChannelSO : ScriptableObject
{
    public UnityAction<Vector3> OnEventRaised;

    public void RaiseEvent(Vector3 value)
    {
        OnEventRaised?.Invoke(value);
    }

    


}