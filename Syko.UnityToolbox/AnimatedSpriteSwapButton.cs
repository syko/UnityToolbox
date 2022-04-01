using UnityEngine.UI;

namespace Syko.UnityToolbox.UI
{
  /**
   * A custom Button class that allows to tween sprite swapping and assign different durations and easings for
   * each state change.
   *
   * Goes together with AnimatedSpriteSwapButtonEditor.
   */
  public class AnimatedSpriteSwapButton : AnimatedButton
  {
    public Image normalImage;
    public Image highlightedImage;
    public Image pressedImage;
    public Image selectedImage;
    public Image disabledImage;
    private Image[] images = new Image[5];

    protected override void Start()
    {
      base.Start();
      images[0] = normalImage;
      images[1] = highlightedImage;
      images[2] = pressedImage;
      images[3] = selectedImage;
      images[4] = disabledImage;
    }

    public override void HighlightOn()
    {
      base.HighlightOn();
      FadeOutAll(highlightedImage, highlightDuration, highlightEasing);
      LeanTween.alpha(highlightedImage.rectTransform, 1f, highlightDuration)
          .setEase((LeanTweenType)highlightEasing + 1);
    }

    public override void HighlightOff()
    {
      base.HighlightOff();
      FadeOutAll(normalImage, unhighlightDuration, unhighlightEasing);
      LeanTween.alpha(normalImage.rectTransform, 1f, unhighlightDuration)
          .setEase((LeanTweenType)unhighlightEasing + 1);
    }

    public override void SelectedOn()
    {
      base.SelectedOn();
      FadeOutAll(highlightedImage, selectDuration, selectEasing);
      LeanTween.alpha(selectedImage.rectTransform, 1f, selectDuration)
          .setEase((LeanTweenType)selectEasing + 1);
    }

    public override void PressedOn()
    {
      base.PressedOn();
      FadeOutAll(pressedImage, pressDuration, pressEasing);
      LeanTween.alpha(pressedImage.rectTransform, 1f, pressDuration)
          .setEase((LeanTweenType)pressEasing + 1);
    }

    public override void PressedOff()
    {
      base.PressedOff();
      FadeOutAll(highlightedImage, pressDuration, pressEasing);
      LeanTween.alpha(highlightedImage.rectTransform, 1f, pressDuration)
          .setEase((LeanTweenType)pressEasing + 1);
    }

    public override void DisabledOn()
    {
      base.DisabledOn();
      FadeOutAll(disabledImage, disableDuration, disableEasing);
      LeanTween.alpha(disabledImage.rectTransform, 1f, disableDuration)
          .setEase((LeanTweenType)disableEasing + 1);
    }

    protected override void CancelAllTweens()
    {
      base.CancelAllTweens();
      for (int i = 0; i < images.Length; i++)
      {
        LeanTween.cancel(images[i].rectTransform);
      }
    }

    private void FadeOutAll(Image exception, float duration, AnimatedButtonTweenType easing)
    {
      for (int i = 0; i < images.Length; i++)
      {
        if (images[i] == exception) continue;
        LeanTween.alpha(images[i].rectTransform, 0f, duration).setEase((LeanTweenType)easing + 1);
      }
    }
  }
}