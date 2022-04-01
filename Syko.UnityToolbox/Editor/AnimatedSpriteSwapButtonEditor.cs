using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

namespace Syko.UnityToolbox.UI
{
  /**
   * Custom editor for AnimatedSwapButton that hides the irrelevant properties of a Selectable.
   */
  [CustomEditor(typeof(AnimatedSpriteSwapButton))]
  [CanEditMultipleObjects]
  public class AnimatedSpriteSwapButtonEditor : AnimatedButtonEditor
  {
    protected SerializedProperty normalImageProperty;
    protected SerializedProperty highlightedImageProperty;
    protected SerializedProperty pressedImageProperty;
    protected SerializedProperty selectedImageProperty;
    protected SerializedProperty disabledImageProperty;

    protected override void OnEnable()
    {
      base.OnEnable();
      normalImageProperty = serializedObject.FindProperty("normalImage");
      highlightedImageProperty = serializedObject.FindProperty("highlightedImage");
      pressedImageProperty = serializedObject.FindProperty("pressedImage");
      selectedImageProperty = serializedObject.FindProperty("selectedImage");
      disabledImageProperty = serializedObject.FindProperty("disabledImage");
    }

    public override void OnInspectorGUI()
    {
      serializedObject.Update();

      EditorGUILayout.Space();
      EditorGUILayout.PropertyField(navigationProperty);

      VisualizeButtonField();

      EditorGUILayout.Space();
      EditorGUILayout.LabelField("Normal", EditorStyles.boldLabel);

      EditorGUILayout.PropertyField(normalImageProperty);

      EditorGUILayout.Space();
      EditorGUILayout.LabelField("Highlighted", EditorStyles.boldLabel);

      EditorGUILayout.PropertyField(highlightedImageProperty);
      EditorGUILayout.PropertyField(highlightScaleProperty);
      EditorGUILayout.PropertyField(highlightDurationProperty);
      EditorGUILayout.PropertyField(unhighlightDurationProperty);
      EditorGUILayout.PropertyField(highlightEasingProperty);
      EditorGUILayout.PropertyField(unhighlightEasingProperty);

      EditorGUILayout.Space();
      EditorGUILayout.LabelField("Pressed", EditorStyles.boldLabel);

      EditorGUILayout.PropertyField(pressedImageProperty);
      EditorGUILayout.PropertyField(pressedScaleProperty);
      EditorGUILayout.PropertyField(pressDurationProperty);
      EditorGUILayout.PropertyField(unpressDurationProperty);
      EditorGUILayout.PropertyField(pressEasingProperty);
      EditorGUILayout.PropertyField(unpressEasingProperty);

      EditorGUILayout.Space();
      EditorGUILayout.LabelField("Selected", EditorStyles.boldLabel);

      EditorGUILayout.PropertyField(selectOnClickProperty);
      EditorGUILayout.PropertyField(selectedImageProperty);
      EditorGUILayout.PropertyField(selectedScaleProperty);
      EditorGUILayout.PropertyField(selectDurationProperty);
      EditorGUILayout.PropertyField(deselectDurationProperty);
      EditorGUILayout.PropertyField(selectEasingProperty);
      EditorGUILayout.PropertyField(deselectEasingProperty);

      EditorGUILayout.Space();
      EditorGUILayout.LabelField("Disabled", EditorStyles.boldLabel);

      EditorGUILayout.PropertyField(disabledImageProperty);
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
