// using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimationControllerScript : MonoBehaviour
{
    // [SerializeField] private UnitInfo info = null;
    [SerializeField] private Animator animator = null;
    //[SerializeField] public List<string> animationList = null;
    // [SerializeField] private AnimationListScript animationListScript = null;
    // [SerializeField] private List<string> animationList = animationListScript.AnimationList;

    private string lastAnimation;

    private void Start()
    {
        if (animator == null)
            animator = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation(string animationName)
    {
        ChangeAnimation(animationName);
    }

    private void ChangeAnimation(string itemToPlay) {
        //foreach(var item in animationList) {
        //    animator.SetBool(item.ToString(), false);
        //}
        animator.SetBool(itemToPlay, true);
        lastAnimation = itemToPlay;
    }

    // public void PlayIdleAnimation() {
    //     // PlayAnimation(AnimationList.idle);
    // }
    // public void PlayWalkAnimation() {
    //     // PlayAnimation(AnimationList.walk);
    // }
    // public void PlayRunAnimation() {
    //     // PlayAnimation(AnimationList.run);
    // }
    // public void PlayWaveAnimation() {
    //     // PlayAnimation(AnimationList.wave);
    // }
}
