using UnityEngine;
using System.Collections;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class SimpleActionManager : ActionManagerBase
{

    public RuntimeAnimatorController DefaultMovement;
    public override AnimatorControllerPlayable CurrentAnimatorPlayable
    {
        get
        {
            if (transit)
                return animatorPlayables[next];
            else
                return animatorPlayables[current];
        }
    }

    bool init = false;
    bool transit = false;
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

    RuntimeAnimatorController currentAnimatorController;
    RuntimeAnimatorController nextAnimatorController;
    RuntimeAnimatorController activatedAnimatorController => transit ? nextAnimatorController : currentAnimatorController;
    

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
        currentAnimatorController = DefaultMovement;
    }

    public bool ChangeAnimation(RuntimeAnimatorController animatorController, float time)
    {
        StopAllCoroutines();
        animatorPlayables[next] = AnimatorControllerPlayable.Create(playableGraph, animatorController);
        playableGraph.Disconnect(mixPlayable, next);
        playableGraph.Connect(animatorPlayables[next], 0, mixPlayable, next);
        mixPlayable.SetInputWeight(current, 1);
        mixPlayable.SetInputWeight(next, 0);
        nextAnimatorController = animatorController;
        transit = true;
        this.NumericAnimate(time, tick: (t) =>
        {
            //Debug.Log(t);
            mixPlayable.SetInputWeight(current, 1 - t);
            mixPlayable.SetInputWeight(next, t);
        }, complete: () =>
        {
            mixPlayable.SetInputWeight(current, 0);
            mixPlayable.SetInputWeight(next, 1);
            current++;
            currentAnimatorController = nextAnimatorController;
            nextAnimatorController = null;
            transit = false;
        });
        return true;
    }

    public override bool ChangeAction(RuntimeAnimatorController animatorController)
    {
        if (!init)
            Start();

        else if (activatedAnimatorController == animatorController)
            return true;
        var state = CurrentAnimatorPlayable.GetCurrentAnimatorStateInfo(0);
        if (state.IsTag(AnimTagEnd) || state.IsTag(AnimTagGap))
        {
            ChangeAnimation(animatorController, 0.2f);

            Debug.Log("Change To " + animatorController.name);
            return true;
        }
        return false;
    }

    public override bool Move(Vector2 movement)
    {
        if (!ChangeAction(DefaultMovement))
            return false;

        CurrentAnimatorPlayable.SetFloat("x", movement.x);
        CurrentAnimatorPlayable.SetFloat("y", movement.y);
        return true;
    }

    public override bool Turn(float angle)
    {
        if (!ChangeAction(DefaultMovement))
            return false;
        
        Entity.transform.Rotate(0, -angle, 0, Space.Self);
        return true;
    }
}
