using UnityEngine;
using Zenject;

public static class Events
{
    public static CrouchPressEvent CrouchPressEvent = new();
    public static LocomotionEvent LocomotionEvent = new();
    public static MouseMoveEvent MouseMoveEvent = new();
    public static SwordAttackPressEvent SwordAttackPressEvent = new();
    public static MagicAttackPressEvent MagicAttackPressEvent = new();
}

public class CrouchPressEvent : GameEvent { }
public class SwordAttackPressEvent : GameEvent { }
public class MagicAttackPressEvent : GameEvent { }
public class LocomotionEvent : GameEvent
{
    public Vector2 LocomotionInput;
}

public class MouseMoveEvent : GameEvent
{
    public Vector2 MouseInput;
}


//public class StartDialogueEvent : GameEvent
//{
//    public Dialogue Dialogue;
//    public NPC NPC;
//}

//public class EndDialogueEvent : GameEvent { }