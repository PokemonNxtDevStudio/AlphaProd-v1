using UnityEngine;
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
    SlideLeft,
    SlideRight,
    SlideTop,
    SlideBottom,
    HingeLeft,
    HingeRight,
    HingeTop,
    HingeBottom
}

/// <summary>
///  TODO:
///   1)Fade in and out panels based on IDS
///   
///   2) Ids are mapped to enums types
/// 
///   3) Test this script by toggling different panels on and off
///  
///   4) set panels interactable = false when they fade out
///  
///   5) Use leantween if you can 
/// 
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
    public Transform contentHolder;

    int windowID { get { return (int)windowType; } }

    public  bool isFocused;

    public static int focusedWindow;
        
    public bool fadeEnabled = true;
    public bool startHidden = true;
    public bool hideOnLostFocus = false;
    private CanvasGroup canvasGroup;
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

   
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponent<Canvas>();

        sortOrder = canvas.sortingLayerID;

        HandleStartingState();
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
            switch (transition)
            {
                case Transition.Toggle: // Toggling the window
                    switch (transitionType)
                    {
                        case TransitionType.Instant:
                            if (contentHolder.gameObject.activeSelf)
                            {
                                TransitionInstantOut();
                            }
                            else if (!contentHolder.gameObject.activeSelf)
                            {
                                TransitionInstantIn();
                            }
                            break;
                        case TransitionType.Fade:

                            break;
                        case TransitionType.Zoom:

                            break;
                        case TransitionType.SlideLeft:

                            break;
                        case TransitionType.SlideRight:

                            break;
                        case TransitionType.SlideTop:

                            break;
                        case TransitionType.SlideBottom:

                            break;
                        case TransitionType.HingeLeft:

                            break;
                        case TransitionType.HingeRight:

                            break;
                        case TransitionType.HingeTop:

                            break;
                        case TransitionType.HingeBottom:

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
                            TransitionFadeIn(speed);
                            break;
                        case TransitionType.Zoom:
                            TransitionZoomIn(speed);
                            break;
                        case TransitionType.SlideLeft:
                            TransitionSlideIn(TransitionDirection.Left, speed);
                            break;
                        case TransitionType.SlideRight:
                            TransitionSlideIn(TransitionDirection.Right, speed);
                            break;
                        case TransitionType.SlideTop:
                            TransitionSlideIn(TransitionDirection.Top, speed);
                            break;
                        case TransitionType.SlideBottom:
                            TransitionSlideIn(TransitionDirection.Bottom, speed);
                            break;
                        case TransitionType.HingeLeft:
                            TransitionHingeIn(TransitionDirection.Left, speed);
                            break;
                        case TransitionType.HingeRight:
                            TransitionHingeIn(TransitionDirection.Right, speed);
                            break;
                        case TransitionType.HingeTop:
                            TransitionHingeIn(TransitionDirection.Top, speed);
                            break;
                        case TransitionType.HingeBottom:
                            TransitionHingeIn(TransitionDirection.Bottom, speed);
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
                            TransitionFadeOut(speed);
                            break;
                        case TransitionType.Zoom:
                            TransitionZoomOut(speed);
                            break;
                        case TransitionType.SlideLeft:
                            TransitionSlideOut(TransitionDirection.Left, speed);
                            break;
                        case TransitionType.SlideRight:
                            TransitionSlideOut(TransitionDirection.Right, speed);
                            break;
                        case TransitionType.SlideTop:
                            TransitionSlideOut(TransitionDirection.Top, speed);
                            break;
                        case TransitionType.SlideBottom:
                            TransitionSlideOut(TransitionDirection.Bottom, speed);
                            break;
                        case TransitionType.HingeLeft:
                            TransitionHingeOut(TransitionDirection.Left, speed);
                            break;
                        case TransitionType.HingeRight:
                            TransitionHingeOut(TransitionDirection.Right, speed);
                            break;
                        case TransitionType.HingeTop:
                            TransitionHingeOut(TransitionDirection.Top, speed);
                            break;
                        case TransitionType.HingeBottom:
                            TransitionHingeOut(TransitionDirection.Bottom, speed);
                            break;
                    }
                    break;
            }
        }
    }
    enum TransitionDirection
    {
        Left,
        Right,
        Top,
        Bottom
    }
    void TransitionInstantIn()
    {
        contentHolder.gameObject.SetActive(true);
    }
    void TransitionFadeIn(float speed)
    {
        LeanTween.alpha(contentHolder.GetComponent<RectTransform>(), 1.0f, speed);

    }
    void TransitionZoomIn(float speed)
    {
        LeanTween.scale(contentHolder.GetComponent<RectTransform>(), new Vector3(1.0f, 1.0f), speed);
    }  
    void TransitionSlideIn(TransitionDirection transitionDirection, float speed)
    {
        switch (transitionDirection)
        {
            case TransitionDirection.Left:

                break;
            case TransitionDirection.Right:

                break;
            case TransitionDirection.Top:

                break;
            case TransitionDirection.Bottom:

                break;
        }
    }
    void TransitionHingeIn(TransitionDirection transitionDirection, float speed)
    {
        switch (transitionDirection)
        {
            case TransitionDirection.Left:

                break;
            case TransitionDirection.Right:

                break;
            case TransitionDirection.Top:

                break;
            case TransitionDirection.Bottom:

                break;
        }
    }
    void TransitionInstantOut()
    {
        contentHolder.gameObject.SetActive(false);
    }
    void TransitionFadeOut(float speed)
    {
        LeanTween.alpha(contentHolder.GetComponent<RectTransform>(), 0.0f, speed);
    }
    void TransitionZoomOut(float speed)
    {
        LeanTween.scale(contentHolder.GetComponent<RectTransform>(), new Vector3(0.05f, 0.05f), speed);
    }
    void TransitionSlideOut(TransitionDirection transitionDirection, float speed)
    {
        switch (transitionDirection)
        {
            case TransitionDirection.Left:

                break;
            case TransitionDirection.Right:

                break;
            case TransitionDirection.Top:

                break;
            case TransitionDirection.Bottom:

                break;
        }
    }
    void TransitionHingeOut(TransitionDirection transitionDirection, float speed)
    {
        switch (transitionDirection)
        {
            case TransitionDirection.Left:

                break;
            case TransitionDirection.Right:

                break;
            case TransitionDirection.Top:

                break;
            case TransitionDirection.Bottom:

                break;
        }
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