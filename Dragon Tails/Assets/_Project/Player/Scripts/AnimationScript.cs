using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{

    public enum AnimationState { Idle, Fly, Fire, Attack, TakeHit };
    AnimationState CurrentAnimation;
    public Animation anim;

    // Use this for initialization
    void Start()
    {
        CurrentAnimation = AnimationState.Idle;
        //anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void ApplyAnimation()
    {
        switch (CurrentAnimation)
        {
            case AnimationState.Fire:
                anim.CrossFadeQueued("sj001_skill2", 0.3f, QueueMode.PlayNow);
                break;

            case AnimationState.Fly:
                anim.CrossFadeQueued("sj001_run", 0.3f, QueueMode.CompleteOthers);
                break;

            case AnimationState.Idle:
                anim.CrossFadeQueued("sj001_wait", 0.3f, QueueMode.CompleteOthers);
                break;
        }
    }

    public void SetAnimation(AnimationState state)
    {
        this.CurrentAnimation = state;
        ApplyAnimation();
    }
}
