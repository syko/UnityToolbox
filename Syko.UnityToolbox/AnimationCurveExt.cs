using System;
using UnityEngine;

namespace Syko.UnityToolbox.Util
{
  public static class AnimationCurveExt
  {
    public static float InverseEvaluate(this AnimationCurve curve, float value, float threshold = 0.01f)
    {
      float min = 0f;
      float max = 1f;
      float time = 0f;

      if (value <= min) return min;
      if (value >= max) return max;

      for (int i = 0; i < 9999; i++)
      {
        time = min + (max - min) / 2f;
        float v = curve.Evaluate(time);
        if (Math.Abs(v - value) <= threshold) return time;
        if (value < v) max = time;
        else min = time;
      }
      return time;
    }
  }
}
