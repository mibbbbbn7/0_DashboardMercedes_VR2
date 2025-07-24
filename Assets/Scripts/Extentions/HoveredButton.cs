using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor.UI;
using UnityEditor.TerrainTools;



#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(HoveredButton), true), CanEditMultipleObjects]
public class HoverButtonEditor : ButtonEditor
{
    private SerializedProperty _onHoverProperty;

    protected override void OnEnable()
    {
        base.OnEnable();

        _onHoverProperty = serializedObject.FindProperty("OnHover");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space(20);
        EditorGUILayout.PropertyField(_onHoverProperty);
        serializedObject.ApplyModifiedProperties();
    }
}

#endif

public class HoveredButton : Button
{
    public UnityEvent OnHover;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        OnHover?.Invoke();
    }
}
