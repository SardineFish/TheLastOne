using UnityEngine;
using System.Collections;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class SimpleActionManager : EntityBehavior<LifeBody>
{
    public const string AnimTagEnd = "End";
    public const string AnimTagBegin = "Begin";
    public const string AnimTagGap = "Gap";
    public const string AnimTagLock = "Lock";

    public RuntimeAnimatorController DefaultMovement;

    bool init = false;
    PlayableGraph playableGraph;
    PlayableOutput playableOutput;
    AnimatorControllerPlayable[] animatorPlayables;
    AnimationMixerPlayable mixPlayable;

    int _currentPlayable = 0;
    int current
    {
        get { return _currentPlayable; }
        set { _currentPlayable = value % animatorPlayables.Length; }
    }
    int next
    {
        get { return (_currentPlayable + 1) % animatorPlayables.Length; }
    }

    public RuntimeAnimatorController CurrentAnimatorController { get; private set; }
    public RuntimeAnimatorController NextAnimatorController { get; private set; }

    private void Start()
    {
        if (init)
            return;
        init = true;

        var animator = Entity.GetComponent<Animator>();
        playableGraph = PlayableGraph.Create();
        playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
        playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", Entity.GetComponent<Animator>());
        animatorPlayables = new AnimatorControllerPlayable[2];
        animatorPlayables[0] = AnimatorControllerPlayable.Create(playableGraph, DefaultMovement);
        animatorPlayables[1] = AnimatorControllerPlayable.Create(playableGraph, null);
        mixPlayable = AnimationMixerPlayable.Create(playableGraph, 2);
        playableGraph.Connect(animatorPlayables[0], 0, mixPlayable, 0);
        playableGraph.Connect(animatorPlayables[1], 0, mixPlayable, 1);
        mixPlayable.SetInputWeight(0, 1);
        mixPlayable.SetInputWeight(1, 0);
        playableOutput.SetSourcePlayable(mixPlayable);
        playableGraph.Play();
        CurrentAnimatorController = DefaultMovement;
    }

    public bool ChangeAnimation(RuntimeAnimatorController animatorController, float time)
    {
        StopAllCoroutines();
        animatorPlayables[next] = AnimatorControllerPlayable.Create(playableGraph, animatorController);
        playableGraph.Connect(animatorPlayables[next], 0, mixPlayable, next);
        mixPlayable.SetInputWeight(current, 1);
        mixPlayable.SetInputWeight(next, 0);
        NextAnimatorController = animatorController;
        this.NumericAnimate(time, tick: (t) =>
        {
            mixPlayable.SetInputWeight(current, 1 - t);
            mixPlayable.SetInputWeight(next, t);
        }, complete: () =>
        {
            mixPlayable.SetInputWeight(current, 1);
            mixPlayable.SetInputWeight(next, 0);
            current++;
            CurrentAnimatorController = NextAnimatorController;
            NextAnimatorController = null;
        });
        return true;
    }

    public bool ChangeAction(RuntimeAnimatorController animatorController)
    {
        if (!init)
            Start();
        if (CurrentAnimatorController == animatorController)
            return true;
        var state = animatorPlayables[current].GetCurrentAnimatorStateInfo(0);
        if (state.IsTag(AnimTagEnd) || state.IsTag(AnimTagGap))
        {
            ChangeAnimation(animatorController, 0.2f);

            Debug.Log("Change To " + animatorController.name);
            return true;
        }
        return false;
    }

    public bool Move(Vector2 movement)
    {
        if (!ChangeAction(DefaultMovement))
            return false;
        Debug.Log(movement);
        if (NextAnimatorController)
        {
            animatorPlayables[next].SetFloat("x", movement.x);
            animatorPlayables[next].SetFloat("y", movement.y);
        }
        else
        {
            animatorPlayables[current].SetFloat("x", movement.x);
            animatorPlayables[current].SetFloat("y", movement.y);
        }
        return true;
    }
}
