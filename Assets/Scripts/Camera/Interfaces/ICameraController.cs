public interface ICameraController
{
    void ShakeCamera(float shakeAmount,float shakeDuration);
}

public enum CameraType
{
        PlayerFollow,
        Objective,
        Win
}