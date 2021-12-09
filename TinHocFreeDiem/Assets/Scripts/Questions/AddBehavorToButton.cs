using UnityEngine;

public class AddBehavorToButton : MonoBehaviour
{
    [SerializeField] Transform[] scaleTransform;
    [SerializeField] movingEntryTransform[] movingTrasnform;
    [SerializeField] float moveTime, moveDelay;
    [SerializeField] LeanTweenType leanTweenType;


    public void ScaleTrasnformToButton()
    {
        foreach (var scale in scaleTransform)
        {
            LeanTweenManagers.instance.ScalingEntryOnCall(scale);
        }
    }
    public void movingTrasnformToButton()
    {
        foreach (var entry in movingTrasnform)
        {
            LeanTweenManagers.instance.AddMovingEntryDelayOncall(entry.objectTrasnform, entry.moveDirEntry, moveTime, entry.moveLength, moveDelay);
        }
    }
}