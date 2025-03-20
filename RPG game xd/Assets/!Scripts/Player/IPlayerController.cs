using UnityEngine;

public interface IPlayerController
{
    public void Move(float Multiplier = 1f);
    public void EnableCrouch();
    public void DisableCrouch();
    public void Jump();
}
