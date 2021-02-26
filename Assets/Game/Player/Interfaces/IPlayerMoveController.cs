using System;

public interface IPlayerMoveController
{
    event Action<WayPoint> onWayPointReached;
    void MoveToWayPoint(WayPoint wayPoint, float speed);
    void SetToWayPoint(WayPoint wayPoint);
}