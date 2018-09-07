using UnityEngine;
using System.Collections;
using UnityEngine.Animations;
using UnityEngine.Playables;

public abstract class ActionManagerBase : EntityBehavior<LifeBody>
{
    public const string AnimTagEnd = "End";
    public const string AnimTagBegin = "Begin";
    public const string AnimTagGap = "Gap";
    public const string AnimTagLock = "Lock";

    public abstract AnimatorControllerPlayable CurrentAnimatorPlayable { get; }

    public abstract bool ChangeAction(RuntimeAnimatorController animatorController);

    public abstract bool Move(Vector2 movement);
    public abstract bool Turn(float angle);
}
