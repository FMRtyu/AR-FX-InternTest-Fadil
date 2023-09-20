using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARMenuManager : MonoBehaviour
{
    [Header("Helicopter")]
    public GameObject helicopterGameobject;
    public float helicopterTimeLimit;
    public Transform[] flyPoint;
    public Transform[] flyPointFirstTime;

    [Header("Fade Option")]
    public GameObject canvasGameobject;
    public CanvasGroup canvasGroupGameObject;
    public float fadeTime;

    [Header("Trivia")]
    public GameObject triviaCanvas;
    public Transform triviaPos;
    //value
    private LTSpline cr;
    private LTSpline cr2;
    private Animator HeliAnimation;
    private Vector3 InitialPosition;
    public Vector3 triviaInitialPos;
    private Quaternion InitialRotation;
    private bool isTriviaOpen;
    // Start is called before the first frame update
    private void OnEnable()
    {
        HeliAnimation = helicopterGameobject.GetComponentInChildren<Animator>();
        Vector3[] flyPointPosition = new Vector3[flyPoint.Length];
        Vector3[] flyPointPositionFirst = new Vector3[flyPointFirstTime.Length];

        for (int i = 0; i < flyPoint.Length; i++)
        {
            flyPointPosition[i] = flyPoint[i].position;
        }
        for (int i = 0; i < flyPointFirstTime.Length; i++)
        {
            flyPointPositionFirst[i] = flyPointFirstTime[i].position;
        }

        cr = new LTSpline(flyPointPosition);
        cr2 = new LTSpline(flyPointPositionFirst);
    }
    private void Start()
    {
        AudioController.Play("HeliSFX");
        AudioController.Play("BGM1");
        HeliAnimation.SetBool("IsTakeOff", true);
        LeanTween.moveSpline(helicopterGameobject, cr2, 5).setOnComplete(StopBladeAndResetFirst);
        LeanTween.alphaCanvas(canvasGroupGameObject, to: 0, fadeTime);
        canvasGameobject.SetActive(false);
    }
    public void OpenCloseTrivia()
    {
        AudioController.Play("ButtonClicked");
        if (!isTriviaOpen)
        {
            triviaCanvas.SetActive(true);
            LeanTween.move(triviaCanvas, new Vector3(triviaPos.position.x, triviaPos.position.y + 1, triviaPos.position.z), 1);
            isTriviaOpen = true;
        }
        else
        {
            LeanTween.move(triviaCanvas, triviaInitialPos, 1).setOnComplete(() => {
                triviaCanvas.SetActive(false);
                isTriviaOpen = false;
            });
        }
    }
    public void TakeOff()
    {
        AudioController.Play("HeliSFX");
        HeliAnimation.SetBool("IsTakeOff", true);
        LeanTween.moveSpline(helicopterGameobject, cr, helicopterTimeLimit).setOrientToPath(true).setOnComplete(StopBladeAndReset);
        //AudioController.PlayMusic("ButtonClicked");
    }
    void StopBladeAndReset()
    {
        AudioController.StopCategory("helicopter", 2);
        helicopterGameobject.transform.position = InitialPosition;
        helicopterGameobject.transform.rotation = InitialRotation;
        HeliAnimation.SetBool("IsTakeOff", false);
    }
    void StopBladeAndResetFirst()
    {
        triviaInitialPos = triviaCanvas.transform.position;
        InitialPosition = helicopterGameobject.transform.position;
        InitialRotation = helicopterGameobject.transform.rotation;
        helicopterGameobject.GetComponentInChildren<HelicopterController>().isFlying = false;
        AudioController.StopCategory("helicopter", 2);
        HeliAnimation.SetBool("IsTakeOff", false);
    }

    public void OnClikedExitToMenu()
    {
        AudioController.Play("ButtonClicked");
        canvasGameobject.SetActive(true);
        LeanTween.alphaCanvas(canvasGroupGameObject, to: 1, fadeTime).setOnComplete(BackToMainMenu);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
