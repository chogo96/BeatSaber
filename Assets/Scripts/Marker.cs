using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class NoteSpawnMarker : Marker, INotification
{
    public PropertyName id { get { return new PropertyName(); } }
}
