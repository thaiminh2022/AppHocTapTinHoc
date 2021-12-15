using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultipleChoiceTemplate : MonoBehaviour
{
    public TextMeshProUGUI questionBox;

    [Header("Button A")]
    public Button buttonA;
    public TextMeshProUGUI buttonAText;

    [Header("Button B")]
    public Button buttonB;
    public TextMeshProUGUI buttonBText;

    [Header("Button C")]
    public Button buttonC;
    public TextMeshProUGUI buttonCText;

    [Header("Button D")]
    public Button buttonD;
    public TextMeshProUGUI buttonDText;

    [Header("Self")]
    public CorrectAnswer correctAnswer = CorrectAnswer.A;
    //  public bool isTheCorrectButton;
}
