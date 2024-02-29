using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{

    public Animator RecommendToTakePhoto_Animator;
    public Animator ReadyForCapture_Animator;
    public Animator CountingDown_Animator;
    public Animator Capturing_Animator;
    public Animator Captured_Animator;

    public static string CapturingState = "0";

    void Start()
    {

    }

    void Update()
    {
        if (CapturingState == "1")
        {
            RecommendToTakePhoto_Animator.SetTrigger("hideRecommendToTakePhoto");
            CountingDown_Animator.SetTrigger("CountingDown");
            //ReadyForCapture_Animator.SetTrigger("showReadyForCapture");
        }

        if (CapturingState == "2")
        {
            CountingDown_Animator.SetTrigger("CountingDown");
        }

        if (CapturingState == "3")
        {
            Capturing_Animator.SetTrigger("Capturing");
        }

        if (CapturingState == "4")
        {
            Capturing_Animator.SetTrigger("EndCapturing");
            Captured_Animator.SetTrigger("Captured");
        }

        if (CapturingState == "0")
        {
            Captured_Animator.ResetTrigger("Captured");
        }
    }
}
