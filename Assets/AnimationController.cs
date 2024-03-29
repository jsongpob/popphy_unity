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
    public Animator Look_At_Animator;

    public Animator Bar_Show;

    public static string CapturingState = "0";

    void Start()
    {

    }

    void Update()
    {
        if (CapturingState == "1")
        {
            RecommendToTakePhoto_Animator.SetTrigger("hideRecommendToTakePhoto");
            Look_At_Animator.SetTrigger("Looking");
            //ReadyForCapture_Animator.SetTrigger("showReadyForCapture");
        }

        if (CapturingState == "2")
        {
            Look_At_Animator.SetTrigger("Looking");
        }

        if (CapturingState == "3")
        {
            Bar_Show.SetTrigger("ShowBar");
            CountingDown_Animator.SetTrigger("CountingDown");
        }

        if (CapturingState == "4")
        {
            Capturing_Animator.SetTrigger("Capturing");
        }

        if (CapturingState == "5")
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
