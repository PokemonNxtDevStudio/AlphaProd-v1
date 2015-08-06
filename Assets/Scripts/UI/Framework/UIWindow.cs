using UnityEngine;
using System.Collections;
using System.Collections.Generic;






enum FadeType
{
    None,
     In,
    Out
}

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(CanvasGroup))]
public class UIWindow :MonoBehaviour
   {

         public delegate void OnShowBegin();
         public delegate void OnShowComplete();
         public delegate void OnHideBegin();
         public delegate void OnHideComplete();


       /// <summary>
       /// Each UIWindow is a group or subset of UI elements
       /// </summary>
 
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

           if(canvasGroup==null)
           canvasGroup = GetComponent<CanvasGroup>();
           if(canvas == null)
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


       
        public void Show(bool instant = false,bool bringToFront = false)
        {
            if(!this.enabled || !gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
            }
            if(instant || !fadeEnabled )
            {
                this.canvasGroup.alpha = 1;
                this.contentHolder.gameObject.SetActive(true);
            }
            else
            {
                this.contentHolder.gameObject.SetActive(true);// = true;
                //TODO FADE IN
            }
            this.isShowing = true;
        }
        private FadeType animationCurrentMethod = FadeType.None;
        private UICoroutine mFadeCoroutine;
        
             public void FadeIn(float duration)
        {
            if (!this.contentHolder.gameObject.activeSelf)
                return;

            if (this.animationCurrentMethod != FadeType.Out && this.mFadeCoroutine != null)
                this.mFadeCoroutine.Stop();

            // Start the new animation
            if (this.animationCurrentMethod != FadeType.Out)
                this.mFadeCoroutine = new UICoroutine(this, this.FadeAnimation(FadeType.Out, duration));
        }

             private IEnumerator FadeAnimation(FadeType method, float FadeDuration)
             {
                 if (this.canvasGroup == null)
                     yield break;

                 // Check if we are trying to fade in and the window is already shown
                 if (method == FadeType.In && this.canvasGroup.alpha == 1f)
                     yield break;
                 else if (method == FadeType.Out && this.canvasGroup.alpha == 0f)
                     yield break;

                 // Define that animation is in progress
                 this.animationCurrentMethod = method;

                 // Get the timestamp
                 float startTime = Time.time;

                 // Determine Fade in or Fade out
                 if (method == FadeType.In)
                 {
                     // Calculate the time we need to fade in from the current alpha
                     float internalDuration = (FadeDuration - (FadeDuration * this.canvasGroup.alpha));

                     // Update the start time
                     startTime -= (FadeDuration - internalDuration);

                     // Fade In
                     while (Time.time < (startTime + internalDuration))
                     {
                         float RemainingTime = (startTime + FadeDuration) - Time.time;
                         float ElapsedTime = FadeDuration - RemainingTime;

                         // Update the alpha by the percentage of the time elapsed
                         this.canvasGroup.alpha = (ElapsedTime / FadeDuration);

                         yield return 0;
                     }

                     // Make sure it's 1
                     this.canvasGroup.alpha = 1f;
/*
                     if (this.onShowComplete != null)
                         this.onShowComplete();*/
                 }
                 else if (method == FadeType.Out)
                 {
                     // Calculate the time we need to fade in from the current alpha
                     float internalDuration = (FadeDuration * this.canvasGroup.alpha);

                     // Update the start time
                     startTime -= (FadeDuration - internalDuration);

                     // Fade Out
                     while (Time.time < (startTime + internalDuration))
                     {
                         float RemainingTime = (startTime + FadeDuration) - Time.time;

                         // Update the alpha by the percentage of the remaing time
                         this.canvasGroup.alpha = (RemainingTime / FadeDuration);

                         yield return 0;
                     }

                     // Make sure it's 0
                     this.canvasGroup.alpha = 0f;
/*
                     if (this.onHideComplete != null)
                         this.onHideComplete();
*/
                     this.contentHolder.gameObject.SetActive(false);
                 }

                 // No longer animating
                 this.animationCurrentMethod = FadeType.None;
             }
}
    

   

    public class UICoroutine : IEnumerator
    {
        private bool stop;

        IEnumerator enumerator;
        MonoBehaviour behaviour;

        public readonly Coroutine coroutine;

        public UICoroutine(MonoBehaviour behaviour, IEnumerator enumerator)
        {
            this.behaviour = behaviour;
            this.enumerator = enumerator;
            this.coroutine = this.behaviour.StartCoroutine(this);
        }

        public object Current { get { return enumerator.Current; } }
        public bool MoveNext() { return !stop && enumerator.MoveNext(); }
        public void Reset() { enumerator.Reset(); }
        public void Stop() { stop = true; }
    }
   

