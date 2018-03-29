using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class ActionManager : EntityBehavior<LifeBody>
{
    public const string AnimTagEnd = "End";
    public const string AnimTagBegin = "Begin";
    public const string AnimTagGap = "Gap";

    public RuntimeAnimatorController DefaultMovement;

    public AnimatorControllerPlayable CurrentAnimatorController{ get { return currentAnimator; } }

    PlayableGraph playableGraph;
    PlayableOutput playableOutput;
    AnimatorControllerPlayable previousAnimator;
    AnimatorControllerPlayable currentAnimator;
    AnimationMixerPlayable mixPlayable;
    Animator animator;
    RuntimeAnimatorController currentAnimatorController;
    float transTime = 0;
    float transTotalTime = 0;
    float weight = 1;
    int currentConnected = 1;

    // Use this for initialization
    void Start ()
    {
        animator = Entity.GetComponent<Animator>();
        playableGraph = PlayableGraph.Create();
        playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", Entity.GetComponent<Animator>());
        previousAnimator = AnimatorControllerPlayable.Create(playableGraph, DefaultMovement);
        currentAnimator = AnimatorControllerPlayable.Create(playableGraph, DefaultMovement);
        mixPlayable = AnimationMixerPlayable.Create(playableGraph, 2);
        playableGraph.Connect(previousAnimator, 0, mixPlayable, 0);
        playableGraph.Connect(currentAnimator, 0, mixPlayable, 1);

        playableOutput.SetSourcePlayable(mixPlayable);
        playableGraph.Play();
        GraphVisualizerClient.Show(playableGraph, "PlayerGraph");
        currentAnimatorController = DefaultMovement;
    }
	
	// Update is called once per frame
	void Update () {
        if (transTotalTime <= 0)
            weight = 1;
        else
            weight = Mathf.Clamp01(transTime / transTotalTime);
        transTime += Time.deltaTime;
        mixPlayable.SetInputWeight((currentConnected +1 )%2, 1 - weight);
        mixPlayable.SetInputWeight(currentConnected, weight);
        //weight = transTotalTime
	}

    private void ChangeAnimation(RuntimeAnimatorController animatorController,float time)
    {
        currentConnected = (currentConnected + 1) % 2;
        previousAnimator = currentAnimator;
        currentAnimator = AnimatorControllerPlayable.Create(playableGraph, animatorController);
        playableGraph.Disconnect(mixPlayable, currentConnected);
        //mixPlayable = AnimationMixerPlayable.Create(playableGraph, 2);
        //mixPlayable.ConnectInput(0, previousAnimator, 0);
        mixPlayable.ConnectInput(currentConnected, currentAnimator, 0);
        //playableOutput.SetSourcePlayable(mixPlayable);
        transTime = 0;
        transTotalTime = time;
        weight = 0;
        Debug.Log(currentAnimatorController.ToString() + "->" + animatorController.ToString());

        currentAnimatorController = animatorController;

        //playableGraph.Play();
    }

    public bool ChangeAction(RuntimeAnimatorController controller)
    {
        if (!animator)
            return false;
        if (currentAnimatorController == controller)
            return true;
        var state = CurrentAnimatorController.GetCurrentAnimatorStateInfo(0);
        if(/*state.IsTag(AnimTagBegin) || */state.IsTag(AnimTagEnd) || state.IsTag(AnimTagGap))
        {
            ChangeAnimation(controller, 0.2f);

            return true;
        }
        return false;
    }

    public bool EnableMovement()
    {
        return Entity.GetComponent<SkillController>().MovementSkill.Activate();
    }
}
