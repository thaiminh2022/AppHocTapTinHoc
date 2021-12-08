using UnityEngine;
using System.Collections.Generic;

public class LeanTweenManagers : MonoBehaviour
{
    // The lean master will be added everytime you want some lean in yor world


    // staitc instace
    public static LeanTweenManagers instance;

    [Header("Do when everytime a world load")]
    [Header("Scale", order = 1)]
    [SerializeField] List<Transform> scalingEntryTransforms = new List<Transform>();
    [SerializeField] float leenScaleStartTime = .3f;
    [SerializeField] LeanTweenType leenTweenScaleStartType;

    [Header("Move", order = 1)]
    [SerializeField] List<movingEntryTransform> movingEntryTrasnforms = new List<movingEntryTransform>();
    [SerializeField] float leanMoveStartTime = .3f;
    [SerializeField] LeanTweenType leanTweenMoveStartType;

    [Header("Do when asked")]
    [Header("Scale", order = 1)]
    [SerializeField] float leanScaleTime = .3f;
    [SerializeField] float leanScaleExit = .5f;
    [SerializeField] LeanTweenType leanTweenScaleType;
    [SerializeField] LeanTweenType leanTweenScaleExitType;

    [Header("Move", order = 1)]
    [SerializeField] float leanMoveTime = .3f;
    [SerializeField] LeanTweenType leanTweenMoveType;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        ScalingEntryOnStart();
        MovingEntryStart();
    }

    #region ScallingEntries

    // Loop thorugh all the objects and make a scale entry
    private void ScalingEntryOnStart()
    {
        foreach (var item in scalingEntryTransforms)
        {
            Vector3 normalScale = item.localScale;
            item.localScale = Vector3.zero;

            item.LeanScale(normalScale, leenScaleStartTime).setEase(leenTweenScaleStartType);
        }
    }
    public void ScalingEntryOnCall(Transform entryTransform)
    {
        Vector3 normalScale = entryTransform.localScale;
        entryTransform.localScale = Vector3.zero;

        entryTransform.LeanScale(normalScale, leanScaleTime).setEase(leanTweenScaleType);
    }
    public void ScalingExitOnCall(Transform exitTransform)
    {
        Vector3 normalScale = exitTransform.localScale;

        exitTransform.LeanScale(Vector3.zero, leanScaleExit).setEase(leanTweenScaleExitType).setOnComplete(() =>
        {
            exitTransform.gameObject.SetActive(false);
            exitTransform.localScale = normalScale;
        });


    }
    #endregion

    #region MovingEntries
    private void MovingEntryStart()
    {
        foreach (var item in movingEntryTrasnforms)
        {
            Vector3 defaultPosition = item.objectTrasnform.localPosition;

            switch (item.moveDirEntry)
            {
                case MoveDirEntry.Top:
                    // Set the object position to a new postion to lean to
                    Vector3 startPositionTop = new Vector3(
                        item.objectTrasnform.localPosition.x,
                        item.objectTrasnform.localPosition.y + item.moveLength,
                        item.objectTrasnform.localPosition.z);

                    item.objectTrasnform.localPosition = startPositionTop;

                    // Lean the object 
                    item.objectTrasnform.LeanMoveLocal(defaultPosition, leanMoveStartTime).setEase(leanTweenMoveStartType);

                    break;
                case MoveDirEntry.Bottom:
                    // Set the object position to a new postion to lean to
                    Vector3 startPositionBot = new Vector3(
                        item.objectTrasnform.localPosition.x,
                        item.objectTrasnform.localPosition.y - item.moveLength,
                        item.objectTrasnform.localPosition.z);

                    item.objectTrasnform.localPosition = startPositionBot;

                    // Lean the object 
                    item.objectTrasnform.LeanMoveLocal(defaultPosition, leanMoveStartTime).setEase(leanTweenMoveStartType);
                    break;

                case MoveDirEntry.Right:

                    // Set the object position to a new postion to lean to
                    Vector3 startPositionRight = new Vector3(
                        item.objectTrasnform.localPosition.x + item.moveLength,
                        item.objectTrasnform.localPosition.y,
                        item.objectTrasnform.localPosition.z);

                    item.objectTrasnform.localPosition = startPositionRight;

                    // Lean the object 
                    item.objectTrasnform.LeanMoveLocal(defaultPosition, leanMoveStartTime).setEase(leanTweenMoveStartType);
                    break;
                case MoveDirEntry.Left:

                    // Set the object position to a new postion to lean to
                    Vector3 startPositionLeft = new Vector3(
                        item.objectTrasnform.localPosition.x - item.moveLength,
                        item.objectTrasnform.localPosition.y,
                        item.objectTrasnform.localPosition.z);

                    item.objectTrasnform.localPosition = startPositionLeft;

                    // Lean the object 
                    item.objectTrasnform.LeanMoveLocal(defaultPosition, leanMoveStartTime).setEase(leanTweenMoveStartType);

                    break;

            }
        }
    }
    private void MovingEntryOnCall(movingEntryTransform item)
    {

        Vector3 defaultPosition = item.objectTrasnform.localPosition;

        switch (item.moveDirEntry)
        {
            case MoveDirEntry.Top:
                // Set the object position to a new postion to lean to
                Vector3 startPositionTop = new Vector3(
                    item.objectTrasnform.localPosition.x,
                    item.objectTrasnform.localPosition.y + item.moveLength,
                    item.objectTrasnform.localPosition.z);

                item.objectTrasnform.localPosition = startPositionTop;

                // Lean the object 
                item.objectTrasnform.LeanMoveLocal(defaultPosition, leanMoveTime).setEase(leanTweenMoveType);

                break;
            case MoveDirEntry.Bottom:
                // Set the object position to a new postion to lean to
                Vector3 startPositionBot = new Vector3(
                    item.objectTrasnform.localPosition.x,
                    item.objectTrasnform.localPosition.y - item.moveLength,
                    item.objectTrasnform.localPosition.z);

                item.objectTrasnform.localPosition = startPositionBot;

                // Lean the object 
                item.objectTrasnform.LeanMoveLocal(defaultPosition, leanMoveTime).setEase(leanTweenMoveType);
                break;

            case MoveDirEntry.Right:

                // Set the object position to a new postion to lean to
                Vector3 startPositionRight = new Vector3(
                    item.objectTrasnform.localPosition.x + item.moveLength,
                    item.objectTrasnform.localPosition.y,
                    item.objectTrasnform.localPosition.z);

                item.objectTrasnform.localPosition = startPositionRight;

                // Lean the object 
                item.objectTrasnform.LeanMoveLocal(defaultPosition, leanMoveTime).setEase(leanTweenMoveType);
                break;
            case MoveDirEntry.Left:

                // Set the object position to a new postion to lean to
                Vector3 startPositionLeft = new Vector3(
                    item.objectTrasnform.localPosition.x - item.moveLength,
                    item.objectTrasnform.localPosition.y,
                    item.objectTrasnform.localPosition.z);

                item.objectTrasnform.localPosition = startPositionLeft;

                // Lean the object 
                item.objectTrasnform.LeanMoveLocal(defaultPosition, leanMoveTime).setEase(leanTweenMoveType);

                break;

        }
    }
    public void AddMovingEntryOncall(Transform thisTransform, MoveDirEntry thisMoveDirEntry, float thisMoveLength = 3000)
    {

        var movingEntryTransform = new movingEntryTransform
        {
            objectTrasnform = thisTransform,
            moveDirEntry = thisMoveDirEntry,
            moveLength = thisMoveLength,
        };

        MovingEntryOnCall(movingEntryTransform);
    }

    #endregion

    #region GetSet
    public void AddScallingEntryTransform(Transform transform)
    {
        scalingEntryTransforms.Add(transform);
    }


    #endregion
}

[System.Serializable]
public class movingEntryTransform
{
    public Transform objectTrasnform;
    public MoveDirEntry moveDirEntry;
    public float moveLength = 3000;
}