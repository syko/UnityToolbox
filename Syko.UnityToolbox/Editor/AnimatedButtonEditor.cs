using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

namespace Syko.UnityToolbox.UI
{
  /**
   * Custom editor for AnimatedButton that hides the irrelevant properties of a Selectable.
   */
  [CustomEditor(typeof(AnimatedButton))]
  [CanEditMultipleObjects]
  public abstract class AnimatedButtonEditor : SelectableEditor
  {
    protected GUIContent m_VisualizeNavigation2 = new GUIContent("Visualize", "Show navigation flows between selectable UI elements.");
    protected static bool s_ShowNavigation = false;
    protected static string s_ShowNavigationKey = "SelectableEditor.ShowNavigation";

    protected SerializedProperty targetGraphicProperty;
    protected SerializedProperty navigationProperty;
    protected SerializedProperty onClickProperty;

    protected SerializedProperty highlightScaleProperty;
    protected SerializedProperty highlightDurationProperty;
    protected SerializedProperty unhighlightDurationProperty;
    protected SerializedProperty highlightEasingProperty;
    protected SerializedProperty unhighlightEasingProperty;

    protected SerializedProperty pressedScaleProperty;
    protected SerializedProperty pressDurationProperty;
    protected SerializedProperty unpressDurationProperty;
    protected SerializedProperty pressEasingProperty;
    protected SerializedProperty unpressEasingProperty;

    protected SerializedProperty selectOnClickProperty;
    protected SerializedProperty selectedScaleProperty;
    protected SerializedProperty selectDurationProperty;
    protected SerializedProperty deselectDurationProperty;
    protected SerializedProperty selectEasingProperty;
    protected SerializedProperty deselectEasingProperty;

    protected SerializedProperty disabledScaleProperty;
    protected SerializedProperty disableDurationProperty;
    protected SerializedProperty enableDurationProperty;
    protected SerializedProperty disableEasingProperty;
    protected SerializedProperty enableEasingProperty;

    protected override void OnEnable()
    {
      base.OnEnable();
      s_ShowNavigation = EditorPrefs.GetBool(s_ShowNavigationKey);
      targetGraphicProperty = serializedObject.FindProperty("m_TargetGraphic");
      navigationProperty = serializedObject.FindProperty("m_Navigation");
      onClickProperty = serializedObject.FindProperty("onClick");

      highlightScaleProperty = serializedObject.FindProperty("highlightedScale");
      highlightDurationProperty = serializedObject.FindProperty("highlightDuration");
      unhighlightDurationProperty = serializedObject.FindProperty("unhighlightDuration");
      highlightEasingProperty = serializedObject.FindProperty("highlightEasing");
      unhighlightEasingProperty = serializedObject.FindProperty("unhighlightEasing");

      pressedScaleProperty = serializedObject.FindProperty("pressedScale");
      pressDurationProperty = serializedObject.FindProperty("pressDuration");
      unpressDurationProperty = serializedObject.FindProperty("unpressDuration");
      pressEasingProperty = serializedObject.FindProperty("pressEasing");
      unpressEasingProperty = serializedObject.FindProperty("unpressEasing");

      selectOnClickProperty = serializedObject.FindProperty("selectOnClick");
      selectedScaleProperty = serializedObject.FindProperty("selectedScale");
      selectDurationProperty = serializedObject.FindProperty("selectDuration");
      deselectDurationProperty = serializedObject.FindProperty("deselectDuration");
      selectEasingProperty = serializedObject.FindProperty("selectEasing");
      deselectEasingProperty = serializedObject.FindProperty("deselectEasing");

      disabledScaleProperty = serializedObject.FindProperty("disabledScale");
      disableDurationProperty = serializedObject.FindProperty("disableDuration");
      enableDurationProperty = serializedObject.FindProperty("enableDuration");
      disableEasingProperty = serializedObject.FindProperty("disableEasing");
      enableEasingProperty = serializedObject.FindProperty("enableEasing");
    }

    /**
     * Method for rendering the Selectable's navigation visualization button in the inspector.
     */
    protected void VisualizeButtonField()
    {
      EditorGUI.BeginChangeCheck();
      Rect toggleRect = EditorGUILayout.GetControlRect();
      toggleRect.xMin += EditorGUIUtility.labelWidth;
      s_ShowNavigation = GUI.Toggle(toggleRect, s_ShowNavigation, m_VisualizeNavigation2, EditorStyles.miniButton);
      if (EditorGUI.EndChangeCheck())
      {
        EditorPrefs.SetBool(s_ShowNavigationKey, s_ShowNavigation);
        SceneView.RepaintAll();
        // Hack to make the parent class notice the change in the global flag and trigger a redraw of arrows.
        // Unfortunately we don't have access to its local state or anything that does the drawing
        OnDisable();
        OnEnable();
      }
    }
  }
}
