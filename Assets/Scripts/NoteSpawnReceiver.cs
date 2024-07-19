using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class NoteSpawnReceiver : MonoBehaviour, INotificationReceiver
{
    public NoteManager noteManager;

    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is SignalEmitter signalEmitter)
        {
            if (signalEmitter.asset.name == "LeftTopSignal")
            {
                noteManager.SpawnNoteLeftTop();
            }
            else if (signalEmitter.asset.name == "RightTopSignal")
            {
                noteManager.SpawnNoteRightTop();
            }
            else if (signalEmitter.asset.name == "LeftBottomSignal")
            {
                noteManager.SpawnNoteLeftBottom();
            }
            else if (signalEmitter.asset.name == "RightBottomSignal")
            {
                noteManager.SpawnNoteRightBottom();
            }
            else if (signalEmitter.asset.name == "RedNoteSignal")
            {
                noteManager.SpawnRedNote();
            }
        }
    }
}

