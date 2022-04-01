using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

namespace Syko.UnityToolbox.UI
{
  /**
   * Custom editor for AnimatedTintButton that hides the irrelevant properties of a Selectable..
   */
  [CustomEditor(typeof(AnimatedTintButton))]
  [CanEditMultipleObjects]
  public class AnimatedTintButtonEditor : AnimatedButtonEditor
  {

    protected SerializedProperty normalColorProperty;
    protected SerializedProperty highlightedColorProperty;
    protected SerializedProperty pressedColorProperty;
    protected SerializedProperty selectedColorProperty;
    protected SerializedProperty disabledColorProperty;

    protected override void OnEnable()
    {
      base.OnEnable();

      normalColorProperty = serializedObject.FindProperty("normalColor");
      highlightedColorProperty = serializedObject.FindProperty("highlightedColor");
      pressedColorProperty = serializedObject.FindProperty("pressedColor");
      selectedColorProperty = serializedObject.FindProperty("selectedColor");
      disabledColorProperty = serializedObject.FindProperty("disabledColor");
    }

    public override void OnInspectorGUI()
    {
      serializedObject.Update();

      EditorGUILayout.Space();
      EditorGUILayout.PropertyField(navigationProperty);

      VisualizeButtonField();

      EditorGUILayout.Space();
      EditorGUILayout.PropertyField(targetGraphicProperty);

      EditorGUILayout.Space();
      EditorGUILayout.LabelField("Normal", EditorStyles.boldLabel);

      EditorGUILayout.PropertyField(normalColorProperty);

      EditorGUILayout.Space();
      EditorGUILayout.LabelField("Highlighted", EditorStyles.boldLabel);

      EditorGUILayout.PropertyField(highlightedColorProperty);
      EditorGUILayout.PropertyField(highlightScaleProperty);
      EditorGUILayout.PropertyField(highlightDurationProperty);
      EditorGUILayout.PropertyField(unhighlightDurationProperty);
      EditorGUILayout.PropertyField(highlightEasingProperty);
      EditorGUILayout.PropertyField(unhighlightEasingProperty);

      EditorGUILayout.Space();
      EditorGUILayout.LabelField("Pressed", EditorStyles.boldLabel);

      EditorGUILayout.PropertyField(pressedColorProperty);
      EditorGUILayout.PropertyField(pressedScaleProperty);
      EditorGUILayout.PropertyField(pressDurationProperty);
      EditorGUILayout.PropertyField(unpressDurationProperty);
      EditorGUILayout.PropertyField(pressEasingProperty);
      EditorGUILayout.PropertyField(unpressEasingProperty);

      EditorGUILayout.Space();
      EditorGUILayout.LabelField("Selected", EditorStyles.boldLabel);

      EditorGUILayout.PropertyField(selectOnClickProperty);
      EditorGUILayout.PropertyField(selectedColorProperty);
      EditorGUILayout.PropertyField(selectedScaleProperty);
      EditorGUILayout.PropertyField(selectDurationProperty);
      EditorGUILayout.PropertyField(deselectDurationProperty);
      EditorGUILayout.PropertyField(selectEasingProperty);
      EditorGUILayout.PropertyField(deselectEasingProperty);

      EditorGUILayout.Space();
      EditorGUILayout.LabelField("Disabled", EditorStyles.boldLabel);

      EditorGUILayout.PropertyField(disabledColorProperty);
      EditorGUILayout.PropertyField(disabledScaleProperty);
      EditorGUILayout.PropertyField(disableDurationProperty);
      EditorGUILayout.PropertyField(enableDurationProperty);
      EditorGUILayout.PropertyField(disableEasingProperty);
      EditorGUILayout.PropertyField(enableEasingProperty);


      EditorGUILayout.Space();
      EditorGUILayout.PropertyField(onClickProperty);

      serializedObject.ApplyModifiedProperties();
    }
  }
}
