using UnityEngine;
using UnityEditor;

public class AddQuestiontoAssest : MonoBehaviour
{
    public QuestionPerChapters[] questionPerChapters;

    public string questionPlace = "Assets/QuesitionAssest";
    public void CreateFloder()
    {
        for (int i = 0; i < questionPerChapters.Length; i++)
        {
            if (AssetDatabase.IsValidFolder(questionPlace + $"/Câu {i + 1}") == false)
            {
                AssetDatabase.CreateFolder(questionPlace, $"Câu {i + 1}");
            }
        }
    }

    public void AddScriptableObjects()
    {
        for (int i = 0; i < questionPerChapters.Length; i++)
        {
            for (int u = 0; u < questionPerChapters[i].numberOfQuestions; u++)
            {

                if (AssetDatabase.IsValidFolder(questionPlace + $"/Câu {i + 1}") == true)
                {
                    var a = MultipleChoiceQuestion.CreateInstance("MultipleChoiceQuestion");

                    AssetDatabase.CreateAsset(a, questionPlace + $"/Câu {i + 1}/Question {u + 1}.asset");
                }
            }
        }
    }

    public void ResetFolders()
    {
        string[] unusedFolder = { questionPlace };

        foreach (var asset in AssetDatabase.FindAssets("", unusedFolder))
        {
            string path = AssetDatabase.GUIDToAssetPath(asset);

            AssetDatabase.DeleteAsset(path);

        }
    }
}

[System.Serializable]
public struct QuestionPerChapters
{
    public int numberOfQuestions;
}