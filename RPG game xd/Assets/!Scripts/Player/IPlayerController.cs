using UnityEngine;

public interface IPlayerController
{
    public void Move(float Multiplier = 1f);
    public void EnableCrouch();
    public void DisableCrouch();
    public void Jump();
    public void Attack();
    public void Block(BlockPressEvent e);
    public void Magic();

    public Sword Weapon { get; }
    public Shield Shield { get; }
    public MagicSpell Spell { get; }
}
