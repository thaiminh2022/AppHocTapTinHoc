using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CreateUserChapters))]
public class CreateUserChapterEditor : Editor
{


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CreateUserChapters createUserChapters = (CreateUserChapters)target;

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Reset"))
        {
            createUserChapters.ResetAssetInArray();
        }
        GUILayout.EndHorizontal();
    }
}