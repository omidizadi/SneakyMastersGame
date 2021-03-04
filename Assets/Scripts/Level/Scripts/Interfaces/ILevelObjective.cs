using System;

public interface ILevelObjective
{
    event Action<ILevelObjective> OnObjectiveDestroyed;
    bool IsDestroyed { get;}
    void ObjectiveInSight();
    void ObjectiveOutOfSight();
    void DestroyObjective();
}