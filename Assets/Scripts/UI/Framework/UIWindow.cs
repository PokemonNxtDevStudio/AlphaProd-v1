﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Transition
{
    Toggle,
     In,
    Out
}

public enum TransitionType
{
    Instant,
    Fade,
    Zoom,
}

/// <summary>
///  TODO:
/// 
///   3) Test this script by toggling different panels on and off
///  
///   4) set panels interactable = false when they fade out
/// </summary>
[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(CanvasGroup))]
public class UIWindow : MonoBehaviour
{
        

    //events
    public delegate void OnShowBegin();
    public delegate void OnShowComplete();
    public delegate void OnHideBegin();
    public delegate void OnHideComplete();

    public delegate void GenericEvent();
        /*
        * public class Someotherclass
        * {
        *     UIwindow.getType(Bottompanel).OnShowComplete  = new(someotherMethod);  //connecting this button to  function 1-1 mapping  //registering the button to method
        *     
        * 
        * 
        * public someotherMethod(String text)
        * {
        * 
        * }
        *   
        * public ShowComplete()
        * {
        *   
        *  // Show Complete gets triggered when UIwindow "pushes the button" 
        *
        * }
        * 
        * 
        */


    /// <summary>
    /// Each UIWindow is a group or subset of UI elements
    /// </summary>

    #region variables
    private static Dictionary<int,UIWindow> mWindows = new Dictionary<int,UIWindow> ();

    public WindowType windowType = WindowType.UndefinedUI;
    public GameObject contentHolder;
    public CanvasGroup canvasGroup;
    bool canTransition = true;

    int windowID { get { return (int)windowType; } }

    public  bool isFocused;

    public static int focusedWindow;
        
    public bool fadeEnabled = true;
    public bool startHidden = true;
    public bool hideOnLostFocus = false;   
    private Canvas canvas;
    private int sortOrder;
        
    const int FrontOrder = 99;
    const int BackOrder = -99;

    bool isShowing = false;
    #endregion

    public bool IsVisible
    {
        get { return ( this.gameObject.activeInHierarchy && this.canvasGroup.alpha!=1);}
    }

    void Awake()
    {
        if (mWindows.ContainsKey(this.windowID))
        {
            mWindows[this.windowID] = this;
        }
        else
        {
            mWindows.Add(this.windowID,this);
        }
        if(isShowing)
        isShowing = false;
        if (sortOrder > 0)
            sortOrder = 0;
    }

    void Start()
    {
        //canvasGroup = contentHolder.GetComponent<CanvasGroup>();
        canvas = GetComponent<Canvas>();

        sortOrder = canvas.sortingLayerID;

        //HandleStartingState();
    }

    void HandleStartingState()
    {

        if (startHidden)
        {
            if (canvasGroup != null)
                this.canvasGroup.alpha = 0f;
            contentHolder.gameObject.SetActive(false);
            this.isShowing = false;
        }
        else
        {
            if (canvasGroup != null)
                this.canvasGroup.alpha = 1f;
            contentHolder.gameObject.SetActive(true);
            this.isShowing = true;

        }

    }

    public static UIWindow GetWindow(WindowType windowType)
    {
        int id = (int)windowType;
        if (mWindows.ContainsKey(id))
            return mWindows[id];
        return null;
    }

    public static UIWindow GetWindow(int id)
    {
        UIWindow window;

        if (mWindows.TryGetValue(id, out window))
            return window;

        return window;
    }

    public static void FocusWindow(int id)
    {
        if (focusedWindow ==id)
            return;
        //  if(GetWindow(id)!=null)
            //TODO
    }
    public static void FocusWindow(WindowType windowType)
    {
        FocusWindow((int)windowType);
    }
    public void Focus()
    {

        if (isFocused)
            return;
        this.isFocused = true;
        this.canvas.sortingLayerID = FrontOrder;
    }

    /// <summary>
    /// Activate or de-active a window
    /// </summary>
    /// <param name="transition"></param>
    /// <param name="type"></param>
    /// <param name="speed"></param>
    public void ActiveWindow(Transition transition = Transition.Toggle, TransitionType transitionType = TransitionType.Fade, float speed = 0.5f)
    {
        if (contentHolder != null)
        {
            if (canTransition)
            {
                switch (transition)
                {
                    case Transition.Toggle: // Toggling the window
                        switch (transitionType)
                        {
                            case TransitionType.Instant:
                                if (contentHolder.activeSelf)
                                {
                                    TransitionInstantOut();
                                }
                                else if (!contentHolder.activeSelf)
                                {
                                    TransitionInstantIn();
                                }
                                break;
                            case TransitionType.Fade:

                                if (contentHolder.activeSelf)
                                {
                                    StartCoroutine(TransitionFadeOut(speed));
                                }
                                else if (!contentHolder.activeSelf)
                                {
                                    StartCoroutine(TransitionFadeIn(speed));
                                }
                                break;
                            case TransitionType.Zoom:
                                if (contentHolder.activeSelf)
                                {
                                    StartCoroutine(TransitionZoomOut(speed));
                                }
                                else if (!contentHolder.activeSelf)
                                {
                                    StartCoroutine(TransitionZoomIn(speed));
                                }
                                break;
                        }
                        break;
                    case Transition.In: // Making the window go in
                        switch (transitionType)
                        {
                            case TransitionType.Instant:
                                TransitionInstantIn();
                                break;
                            case TransitionType.Fade:
                                StartCoroutine(TransitionFadeIn(speed));
                                break;
                            case TransitionType.Zoom:
                                StartCoroutine(TransitionZoomIn(speed));
                                break;
                        }
                        break;
                    case Transition.Out: // Making the window go out
                        switch (transitionType)
                        {
                            case TransitionType.Instant:
                                TransitionInstantOut();
                                break;
                            case TransitionType.Fade:
                                StartCoroutine(TransitionFadeOut(speed));
                                break;
                            case TransitionType.Zoom:
                                StartCoroutine(TransitionZoomOut(speed));
                                break;
                        }
                        break;
                }
            }
        }
    }

    void TransitionInstantIn() // Instantly transition in
    {
        contentHolder.SetActive(true);
    }
    void TransitionInstantOut() // Instantly transition out
    {
        contentHolder.SetActive(false);
    }
    IEnumerator TransitionFadeIn(float speed) // Fade transition in
    {
        canTransition = false;
        contentHolder.SetActive(true);
        LeanTween.value(contentHolder, SetAlpha, 0.0f, 1.0f, speed);
        yield return new WaitForSeconds(speed);
        canvasGroup.interactable = true;
        canTransition = true;
    }
    IEnumerator TransitionFadeOut(float speed) // Fade transition out
    {
        canTransition = false;
        canvasGroup.interactable = false;
        LeanTween.value(contentHolder, SetAlpha, 1.0f, 0.0f, speed);
        yield return new WaitForSeconds(speed);
        contentHolder.SetActive(false);
        canTransition = true;
    }
    IEnumerator TransitionZoomIn(float speed) // Zoom transition in
    {
        canTransition = false;
        contentHolder.SetActive(true);
        LeanTween.scale(contentHolder.GetComponent<RectTransform>(), new Vector3(1.0f, 1.0f), speed);
        yield return new WaitForSeconds(speed);
        canvasGroup.interactable = true;
        canTransition = true;
    }     
    IEnumerator TransitionZoomOut(float speed) // Zoom transition out
    {
        canTransition = false;
        canvasGroup.interactable = false;
        LeanTween.scale(contentHolder.GetComponent<RectTransform>(), new Vector3(0.01f, 0.01f), speed);
        yield return new WaitForSeconds(speed);
        contentHolder.SetActive(false);
        canTransition = true;
    }
    void SetAlpha(float val) // Change the alpha for fading
    {
        canvasGroup.alpha = val;
    }

#region old 
//    public void Show(bool instant = false,bool bringToFront = false)
//    {
//        if(!this.enabled || !gameObject.activeSelf)
//        {
//            this.gameObject.SetActive(true);
//        }
//        if(instant || !fadeEnabled )
//        {
//            this.canvasGroup.alpha = 1;
//            this.contentHolder.gameObject.SetActive(true);
//        }
//        else
//        {
//            this.gameObject.SetActive(true);// = true;
//            LeanTween.alpha(this.gameObject, 1.0f, 0.3f);
//        }
//        this.isShowing = true;
//    }

//    private Transition animationCurrentMethod = Transition.Toggle;
//    private UICoroutine mFadeCoroutine;
        
//    public void FadeIn(float duration)
//    {
//        if (!this.contentHolder.gameObject.activeSelf)
//            return;

//        if (this.animationCurrentMethod != Transition.Out && this.mFadeCoroutine != null)
//            this.mFadeCoroutine.Stop();

//        // Start the new animation
//        if (this.animationCurrentMethod != Transition.Out)
//            this.mFadeCoroutine = new UICoroutine(this, this.FadeAnimation(Transition.Out, duration));
//    }

//    private IEnumerator FadeAnimation(Transition method, float FadeDuration)
//    {
//        if (this.canvasGroup == null)
//            yield break;

//        // Check if we are trying to fade in and the window is already shown
//        if (method == Transition.In && this.canvasGroup.alpha == 1f)
//            yield break;
//        else if (method == Transition.Out && this.canvasGroup.alpha == 0f)
//            yield break;

//        // Define that animation is in progress
//        this.animationCurrentMethod = method;

//        // Get the timestamp
//        float startTime = Time.time;

//        // Determine Fade in or Fade out
//        if (method == Transition.In)
//        {
//            // Calculate the time we need to fade in from the current alpha
//            float internalDuration = (FadeDuration - (FadeDuration * this.canvasGroup.alpha));

//            // Update the start time
//            startTime -= (FadeDuration - internalDuration);

//            // Fade In
//            while (Time.time < (startTime + internalDuration))
//            {
//                float RemainingTime = (startTime + FadeDuration) - Time.time;
//                float ElapsedTime = FadeDuration - RemainingTime;

//                // Update the alpha by the percentage of the time elapsed
//                this.canvasGroup.alpha = (ElapsedTime / FadeDuration);

//                yield return 0;
//            }

//            // Make sure it's 1
//            this.canvasGroup.alpha = 1f;
///*
//            if (this.onShowComplete != null)
//                this.onShowComplete();*/
//        }
//        else if (method == Transition.Out)
//        {
//            // Calculate the time we need to fade in from the current alpha
//            float internalDuration = (FadeDuration * this.canvasGroup.alpha);

//            // Update the start time
//            startTime -= (FadeDuration - internalDuration);

//            // Fade Out
//            while (Time.time < (startTime + internalDuration))
//            {
//                float RemainingTime = (startTime + FadeDuration) - Time.time;

//                // Update the alpha by the percentage of the remaing time
//                this.canvasGroup.alpha = (RemainingTime / FadeDuration);

//                yield return 0;
//            }

//            // Make sure it's 0
//            this.canvasGroup.alpha = 0f;
///*
//            if (this.onHideComplete != null)
//                this.onHideComplete();
//*/
//            this.contentHolder.gameObject.SetActive(false);
//        }

//        // No longer animating
//        this.animationCurrentMethod = Transition.Toggle;
//    }
}
    
//public class UICoroutine : IEnumerator
//{
//    private bool stop;

//    IEnumerator enumerator;
//    MonoBehaviour behaviour;

//    public readonly Coroutine coroutine;

//    public UICoroutine(MonoBehaviour behaviour, IEnumerator enumerator)
//    {
//        this.behaviour = behaviour;
//        this.enumerator = enumerator;
//        this.coroutine = this.behaviour.StartCoroutine(this);
//    }

//    public object Current { get { return enumerator.Current; } }
//    public bool MoveNext() { return !stop && enumerator.MoveNext(); }
//    public void Reset() { enumerator.Reset(); }
//    public void Stop() { stop = true; }
//}
#endregion