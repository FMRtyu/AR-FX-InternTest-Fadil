using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Fade Option")]
    public GameObject canvasGameobject;
    public CanvasGroup canvasGroupGameObject;
    public float fadeTime;

    [Header("Helicopter Option")]
    public Animator HeliAnimation;

    [Header("UI Button Animation")]
    public GameObject headerGameobject;
    public GameObject buttonGroupGameobject;

    public GameObject headerPos;
    public GameObject buttonPos;
    
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        LeanTween.alphaCanvas(canvasGroupGameObject, to: 0, fadeTime).setOnComplete(() => {
            AudioController.Play("Fly1");
            LeanTween.move(headerGameobject, headerPos.transform.position, 1).setOnComplete(() => {
                AudioController.Play("Fly1");
                LeanTween.move(buttonGroupGameobject, buttonPos.transform.position, 1).setOnComplete(HeliIn);
            });
        });
    }
    void HeliIn()
    {
        canvasGameobject.SetActive(false);
        HeliAnimation.SetBool("IsDoneFade", true);
    }
    public void OnClickedStartButton()
    {
        AudioController.Play("ButtonClicked");
        canvasGameobject.SetActive(true);
        HeliAnimation.SetBool("IsStart", true);
        LeanTween.alphaCanvas(canvasGroupGameObject, to: 1, fadeTime).setOnComplete(LoadScene);
    }
    void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClickedExitButton()
    {
        AudioController.PlayMusic("ButtonClicked");
        canvasGameobject.SetActive(true);
        LeanTween.alphaCanvas(canvasGroupGameObject, to: 1, fadeTime).setOnComplete(ExitGame);
    }
    void ExitGame()
    {
        Application.Quit();
    }
}
