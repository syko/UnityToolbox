using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Syko.UnityToolbox.UI
{
  public enum AnimatedButtonTweenType
  {
    linear, easeOutQuad, easeInQuad, easeInOutQuad, easeInCubic, easeOutCubic, easeInOutCubic, easeInQuart, easeOutQuart, easeInOutQuart,
    easeInQuint, easeOutQuint, easeInOutQuint, easeInSine, easeOutSine, easeInOutSine, easeInExpo, easeOutExpo, easeInOutExpo, easeInCirc, easeOutCirc, easeInOutCirc,
    easeInBounce, easeOutBounce, easeInOutBounce, easeInBack, easeOutBack, easeInOutBack, easeInElastic, easeOutElastic, easeInOutElastic, easeSpring, easeShake, punch
  }

  /**
   * A custom Button parent class that allows to tween properties and assign different durations and easings for
   * each state change.
   *
   * Goes together with AnimatedButtonEditor.
   */
  public abstract class AnimatedButton : UnityEngine.UI.Selectable, IPointerClickHandler
  {
    [FormerlySerializedAs("OnClick")]
    public UnityEvent onClick;

    [Space()]
    [Header("Highlighted")]
    [Range(0f, 3f)] public float highlightedScale = 1f;
    [Range(0f, 3f)] public float highlightDuration = 0.1f;
    [Range(0f, 3f)] public float unhighlightDuration = 0.5f;
    public AnimatedButtonTweenType highlightEasing = AnimatedButtonTweenType.linear;
    public AnimatedButtonTweenType unhighlightEasing = AnimatedButtonTweenType.linear;

    [Space()]
    [Header("Pressed")]
    [Range(0f, 3f)] public float pressedScale = 1f;
    [Range(0f, 3f)] public float pressDuration = 0.1f;
    [Range(0f, 3f)] public float unpressDuration = 0.5f;
    public AnimatedButtonTweenType pressEasing = AnimatedButtonTweenType.linear;
    public AnimatedButtonTweenType unpressEasing = AnimatedButtonTweenType.linear;

    [Space()]
    [Header("Selected")]
    public bool selectOnClick = false;
    [Range(0f, 3f)] public float selectedScale = 1f;
    [Range(0f, 3f)] public float selectDuration = 0.1f;
    [Range(0f, 3f)] public float deselectDuration = 0.1f;
    public AnimatedButtonTweenType selectEasing = AnimatedButtonTweenType.linear;
    public AnimatedButtonTweenType deselectEasing = AnimatedButtonTweenType.linear;

    [Space()]
    [Header("Disabled")]
    [Range(0f, 3f)] public float disabledScale = 1f;
    [Range(0f, 3f)] public float disableDuration = 0.1f;
    [Range(0f, 3f)] public float enableDuration = 0.1f;
    public AnimatedButtonTweenType disableEasing = AnimatedButtonTweenType.linear;
    public AnimatedButtonTweenType enableEasing = AnimatedButtonTweenType.linear;

    private bool ready = false; // DoStateTransition is called alot at start so delay animations a bit
    private SelectionState lastState;

    protected override void Start()
    {
      base.Start();
      transition = Transition.None;
      ready = true;
    }

    public virtual void HighlightOn()
    {
      CancelAllTweens();
      LeanTween.scale(gameObject, Vector3.one * highlightedScale, highlightDuration)
          .setEase((LeanTweenType)highlightEasing + 1);
    }

    public virtual void HighlightOff()
    {
      CancelAllTweens();
      LeanTween.scale(gameObject, Vector3.one, unhighlightDuration)
          .setEase((LeanTweenType)unhighlightEasing + 1);
    }

    public virtual void SelectedOn()
    {
      CancelAllTweens();
      LeanTween.scale(gameObject, Vector3.one * selectedScale, selectDuration)
          .setEase((LeanTweenType)selectEasing + 1);
    }

    public virtual void PressedOn()
    {
      CancelAllTweens();
      LeanTween.scale(gameObject, Vector3.one * pressedScale, pressDuration)
          .setEase((LeanTweenType)pressEasing + 1);
    }

    public virtual void PressedOff()
    {
      CancelAllTweens();
      LeanTween.scale(gameObject, Vector3.one * highlightedScale, unpressDuration)
          .setEase((LeanTweenType)unpressEasing + 1);
    }

    public virtual void DisabledOn()
    {
      CancelAllTweens();
      LeanTween.scale(gameObject, Vector3.one * disabledScale, disableDuration)
          .setEase((LeanTweenType)disableEasing + 1);
    }

    protected virtual void CancelAllTweens()
    {
      LeanTween.cancel(gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
      if (interactable) onClick?.Invoke();
    }

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
      base.DoStateTransition(state, instant);
#if UNITY_EDITOR
        if (!EditorApplication.isPlaying) return;
#endif
      if (!ready) return;
      // Debug.Log("DoStateTransition" + " state: " + state + " instant: " + instant + " ready: " + ready + " :" + EditorApplication.isPlaying);
      if (!interactable && state != SelectionState.Disabled) return;
      switch (state)
      {
        case SelectionState.Normal:
          HighlightOff();
          break;
        case SelectionState.Highlighted:
          if (lastState == SelectionState.Pressed) PressedOff();
          else HighlightOn();
          break;
        case SelectionState.Selected:
          if (lastState == SelectionState.Pressed) PressedOff();
          else if (lastState == SelectionState.Highlighted && !selectOnClick)
          {
            DoStateTransition(SelectionState.Normal, true);
            return;
          }
          else SelectedOn();
          break;
        case SelectionState.Pressed:
          PressedOn();
          break;
        case SelectionState.Disabled:
          DisabledOn();
          break;
      }
      lastState = currentSelectionState;
    }
  }
}
