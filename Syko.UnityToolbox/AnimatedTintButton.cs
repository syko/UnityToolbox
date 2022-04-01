using UnityEngine;
using UnityEngine.UI;

namespace Syko.UnityToolbox.UI
{
  /**
   * A custom Button class that allows to tween colors and assign different durations and easings for
   * each state change.
   *
   * Goes together with AnimatedTintButtonEditor.
   */
  public class AnimatedTintButton : AnimatedButton
  {

    public Color normalColor = Color.white * 0.8f;
    public Color highlightedColor = Color.white;
    public Color pressedColor = Color.white;
    public Color selectedColor = Color.white;
    public Color disabledColor = Color.gray;

    protected override void Start()
    {
      base.Start();
      ColorBlock cb = new ColorBlock();
      cb.normalColor = normalColor;
      cb.highlightedColor = highlightedColor;
      cb.pressedColor = pressedColor;
      cb.selectedColor = selectedColor;
      cb.disabledColor = disabledColor;
      colors = cb;
      // Cannot tween CanvasRenderer's color because something is resetting it when the interactable flag is flipped
      targetGraphic.color = colors.normalColor;
    }

    public override void HighlightOn()
    {
      base.HighlightOn();
      LeanTween.value(targetGraphic.gameObject, targetGraphic.color, colors.highlightedColor, highlightDuration)
          .setOnUpdate(v => targetGraphic.color = v)
          .setEase((LeanTweenType)highlightEasing + 1);
    }

    public override void HighlightOff()
    {
      base.HighlightOff();
      LeanTween.value(targetGraphic.gameObject, targetGraphic.color, colors.normalColor, unhighlightDuration)
          .setOnUpdate(v => targetGraphic.color = v)
          .setEase((LeanTweenType)unhighlightEasing + 1);
    }

    public override void SelectedOn()
    {
      base.SelectedOn();
      LeanTween.value(targetGraphic.gameObject, targetGraphic.color, colors.selectedColor, selectDuration)
          .setOnUpdate(v => targetGraphic.color = v)
          .setEase((LeanTweenType)selectEasing + 1);
    }

    public override void PressedOn()
    {
      base.PressedOn();
      LeanTween.value(targetGraphic.gameObject, targetGraphic.color, colors.pressedColor, pressDuration)
          .setOnUpdate(v => targetGraphic.color = v)
          .setEase((LeanTweenType)pressEasing + 1);
    }

    public override void PressedOff()
    {
      base.PressedOff();
      LeanTween.value(targetGraphic.gameObject, targetGraphic.color, colors.highlightedColor, unpressDuration)
          .setOnUpdate(v => targetGraphic.color = v)
          .setEase((LeanTweenType)unpressEasing + 1);
    }

    public override void DisabledOn()
    {
      base.DisabledOn();
      LeanTween.value(targetGraphic.gameObject, targetGraphic.color, colors.disabledColor, disableDuration)
          .setOnUpdate(v => targetGraphic.color = v)
          .setEase((LeanTweenType)disableEasing + 1);
    }

    protected override void CancelAllTweens()
    {
      base.CancelAllTweens();
      LeanTween.cancel(targetGraphic.rectTransform);
      LeanTween.cancel(targetGraphic.gameObject);
    }

  }
}
