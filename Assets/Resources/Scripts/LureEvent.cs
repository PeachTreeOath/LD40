using UnityEngine.Events;
using System;

[Serializable]
public class LureEvent : UnityEvent<string> {
    public const string ATTRACT = "attract";
    public const string CANCEL = "cancel";
}
