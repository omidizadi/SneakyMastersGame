using System;

public interface IWeapon
{
    event Action OnShootingDone;
    void StartAiming();
    void FinishAiming();
}