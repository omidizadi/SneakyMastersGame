using System;

public interface IPlayerMoveController
{
    event Action<LevelCommand> onWayPointReached;
    void MoveToWayPoint(LevelCommand levelCommand, float speed);
    void SetToWayPoint(LevelCommand levelCommand);
}