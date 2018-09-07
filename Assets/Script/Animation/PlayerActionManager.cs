using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class PlayerActionManager : ActionManagerBase
{

    public RuntimeAnimatorController DefaultMovement;
    public SkillAction DrawSword;
    public SkillAction SheathSword;

    public override AnimatorControllerPlayable CurrentAnimatorPlayable{ get { return currentAnimatorPlayable; } }

    PlayableGraph playableGraph;
    PlayableOutput playableOutput;
    AnimatorControllerPlayable NULLAnimatorControllerPlayable;
    AnimatorControllerPlayable movementController;
    AnimatorControllerPlayable previousAnimator;
    AnimatorControllerPlayable currentAnimatorPlayable;
    AnimationMixerPlayable mixPlayable;
    Animator animator;
    RuntimeAnimatorController currentAnimatorController;
    AnimationLayerMixerPlayable layerMixer;
    bool init = false;
    bool hasFirstAnimator = false;
    float transTime = 0;
    float transTotalTime = 0;
    float weight = 1;
    int currentConnectedIdx = 1;
    float movementLayerWeight = 1;

    // Use this for initialization
    void Start ()
    {
        animator = Entity.GetComponent<Animator>();
        playableGraph = PlayableGraph.Create();
        playableGraph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
        playableOutput = AnimationPlayableOutput.Create(playableGraph, "Animation", Entity.GetComponent<Animator>());
        previousAnimator = AnimatorControllerPlayable.Create(playableGraph, null);
        currentAnimatorPlayable = AnimatorControllerPlayable.Create(playableGraph, null);
        movementController = AnimatorControllerPlayable.Create(playableGraph, DefaultMovement);
        mixPlayable = AnimationMixerPlayable.Create(playableGraph, 2);
        playableGraph.Connect(previousAnimator, 0, mixPlayable, 0);
        playableGraph.Connect(currentAnimatorPlayable, 0, mixPlayable, 1);
        //playableGraph.Connect(movementController, 0, mixPlayable, 2);

        mixPlayable.SetInputWeight(0, 0);
        mixPlayable.SetInputWeight(1, 0);
        //mixPlayable.SetInputWeight(2, 1);
        layerMixer = AnimationLayerMixerPlayable.Create(playableGraph, 2);
        playableGraph.Connect(mixPlayable, 0, layerMixer, 0);
        playableGraph.Connect(movementController, 0, layerMixer, 1);
        layerMixer.SetInputWeight(0, 1);
        layerMixer.SetInputWeight(1, 1);
        //layerMixer.SetLayerAdditive(0, true);
        playableOutput.SetSourcePlayable(layerMixer);
        playableGraph.Play();
        currentAnimatorController = null;

        init = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (!CurrentAnimatorPlayable.IsNull() && CurrentAnimatorPlayable.GetCurrentAnimatorStateInfo(0).IsTag(AnimTagLock))
        {
            movementLayerWeight -= 0.2f / Time.deltaTime;
            movementLayerWeight = movementLayerWeight < 0 ? 0 : movementLayerWeight;
            layerMixer.SetInputWeight(1, 0);
        }
        else
        {
            movementLayerWeight += 0.2f / Time.deltaTime;
            movementLayerWeight = movementLayerWeight > 1 ? 1 : movementLayerWeight;
            layerMixer.SetInputWeight(1, 1);
        }

        /*layerMixer.SetInputWeight(1, movementLayerWeight);
        layerMixer.SetInputWeight(0, 1 - movementLayerWeight);*/

        if (transTotalTime <= 0)
            weight = 1;
        else
            weight = Mathf.Clamp01(transTime / transTotalTime);
        transTime += Time.deltaTime;
        mixPlayable.SetInputWeight((currentConnectedIdx +1 )%2, 1 - weight);
        mixPlayable.SetInputWeight(currentConnectedIdx, weight);
        //weight = transTotalTime
	}

    private void ChangeAnimation(RuntimeAnimatorController animatorController,float time)
    {
        if(!init)
        {
            Start();
        }
        currentConnectedIdx = (currentConnectedIdx + 1) % 2;
        previousAnimator = currentAnimatorPlayable;
        currentAnimatorPlayable = AnimatorControllerPlayable.Create(playableGraph, animatorController);
        
        playableGraph.Disconnect(mixPlayable, currentConnectedIdx);
        //mixPlayable = AnimationMixerPlayable.Create(playableGraph, 2);
        //mixPlayable.ConnectInput(0, previousAnimator, 0);
        mixPlayable.ConnectInput(currentConnectedIdx, currentAnimatorPlayable, 0);
        //playableOutput.SetSourcePlayable(mixPlayable);
        transTime = 0;
        transTotalTime = time;
        weight = 0;
        if (currentAnimatorController)
            Debug.Log(currentAnimatorController.ToString() + "->" + animatorController.ToString());

        currentAnimatorController = animatorController;

        //playableGraph.Play();
    }

    public override bool ChangeAction(RuntimeAnimatorController controller)
    {
        if (!animator)
            animator = Entity.GetComponent<Animator>();
        if (currentAnimatorController == controller)
        {
            return true;
        }
        if(!currentAnimatorController)
        {
            ChangeAnimation(controller, 0.2f);
            Debug.Log("Change To " + controller.name);
            return true;
        }
        var state = CurrentAnimatorPlayable.GetCurrentAnimatorStateInfo(0);
        if(/*state.IsTag(AnimTagBegin) || */state.IsTag(AnimTagEnd) || state.IsTag(AnimTagGap))
        {
            ChangeAnimation(controller, 0.2f);

            Debug.Log("Change To " + controller.name);
            return true;
        }
        return false;
    }

    public bool EnableMovement()
    {
        return Entity.GetComponent<SkillController>().MovementSkill.Activate();
    }

    public override bool Move(Vector2 movement)
    {
        if (!CurrentAnimatorPlayable.IsNull() && CurrentAnimatorPlayable.GetCurrentAnimatorStateInfo(0).IsTag(AnimTagLock))
        {
            movementController.SetFloat("x", 0);
            movementController.SetFloat("y", 0);
            return false;
        } 
        else
        {
            movementController.SetFloat("x", movement.x);
            movementController.SetFloat("y", movement.y);
            return true;
        }
    }

    public override bool Turn(float angle)
    {
        if (!CurrentAnimatorPlayable.IsNull() && CurrentAnimatorPlayable.GetCurrentAnimatorStateInfo(0).IsTag(AnimTagLock))
        {
            return false;
        }
        else
        {

            Entity.transform.Rotate(0, -angle, 0, Space.Self);
            return true;
        }
    }
}
