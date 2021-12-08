using UnityEngine;

public class LeanTweenManagers : MonoBehaviour
{
    public static LeanTweenManagers instance;

    [Header("Speed")]
    [SerializeField] private float scallingEntrySpeed = 1f;
    [SerializeField] private LeanTweenType leanTweenType;

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

    public void ScalingEntry(Transform target)
    {
        Vector3 defaultScale = target.localScale;
        target.localScale = Vector3.zero;

        target.LeanScale(defaultScale, scallingEntrySpeed).setEase(leanTweenType);
    }
}