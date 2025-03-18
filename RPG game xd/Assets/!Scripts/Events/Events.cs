using UnityEngine;
using Zenject;

public static class Events
{
    public static CrouchPressEvent CrouchPressEvent = new();
    public static LocomotionEvent LocomotionEvent = new();
    public static MouseMoveEvent MouseMoveEvent = new();
    public static SwordAttackPressEvent SwordAttackPressEvent = new();
    public static MagicAttackPressEvent MagicAttackPressEvent = new();
    public static SystemsInitializedEvent SystemsInitializedEvent = new();
    public static JumpEvent JumpEvent = new();
    public static BlockPressEvent BlockPressEvent = new();
    public static RunEvent RunEvent = new();
}

public class RunEvent : GameEvent {}
public class BlockPressEvent : GameEvent { 
    public bool IsBlocking;
}
public class CrouchPressEvent : GameEvent { }
public class SwordAttackPressEvent : GameEvent { }
public class MagicAttackPressEvent : GameEvent { }
public class LocomotionEvent : GameEvent
{
    public Vector2 LocomotionInput;
}

public class JumpEvent : GameEvent
{
    
}

public class MouseMoveEvent : GameEvent
{
    public Vector2 MouseInput;
}

public class SystemsInitializedEvent : GameEvent {}

//public class StartDialogueEvent : GameEvent
//{
//    public Dialogue Dialogue;
//    public NPC NPC;
//}

//public class EndDialogueEvent : GameEvent { }