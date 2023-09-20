using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionDropdownController : MonoBehaviour
{
    [Header("Button Gameobject")]
    public Image openMenuButton;
    public GameObject actionGroup;
    public GameObject moveDestination;
    public float actionMenuDropSpeed;

    //value
    private bool IsMenuOpen;
    private Vector2 actionInitialPos;
    // Start is called before the first frame update
    private void Start()
    {
        actionInitialPos = actionGroup.transform.position;
    }
    public void OnClikedOpenMenu()
    {
        AudioController.PlayMusic("ButtonClicked");
        if (!IsMenuOpen)
        {
            actionGroup.SetActive(true);
            LeanTween.rotateAround(openMenuButton.gameObject, Vector3.forward, 180f, 0.5f);
            LeanTween.move(actionGroup, moveDestination.transform.position, actionMenuDropSpeed);
            IsMenuOpen = true;
        }
        else
        {
            LeanTween.rotateAround(openMenuButton.gameObject, Vector3.back, 180f, 0.5f);
            LeanTween.move(actionGroup, actionInitialPos, actionMenuDropSpeed).setOnComplete(() => {
                actionGroup.SetActive(false);
            });
            IsMenuOpen = false;
        }
    }
}
