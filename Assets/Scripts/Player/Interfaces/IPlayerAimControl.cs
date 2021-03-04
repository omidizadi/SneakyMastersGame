using UnityEngine;

public interface IPlayerAimControl
{
    void SetVerticalAimConstraints(Vector2 angle);
    void SetHorizontalAimConstraints(Vector2 angle);
    void OnAimStarted(Vector2 touchPos);
    void OnDrag(Vector2 touchPos);
}