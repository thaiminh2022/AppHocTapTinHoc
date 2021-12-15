using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AddQuestiontoAssest))]
public class QuestionMenueditor : Editor
{
    public override void OnInspectorGUI()
    {
        // ! Always add this on top
        base.OnInspectorGUI();
        AddQuestiontoAssest addQuestiontoAssest = (AddQuestiontoAssest)target;



        if (GUILayout.Button("Add Floder"))
        {
            addQuestiontoAssest.CreateFloder();
        }
        if (GUILayout.Button("Add Scriptable Object"))
        {
            addQuestiontoAssest.AddScriptableObjects();
        }

        if (GUILayout.Button("Add Folder and Scriptable Object"))
        {
            addQuestiontoAssest.CreateFloder();
            addQuestiontoAssest.AddScriptableObjects();
        }
        if (GUILayout.Button("Reset"))
        {
            addQuestiontoAssest.ResetFolders();
        }
    }
}
