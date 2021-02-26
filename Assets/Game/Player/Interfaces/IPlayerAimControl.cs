using UnityEngine;

public interface IPlayerAimControl
{
    void OnAimStarted(Vector2 touchPos);
    void OnDrag(Vector2 touchPos);
    void OnAimFinished();
}