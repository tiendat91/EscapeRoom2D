using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private int maxPage;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;
    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;

    // Start is called before the first frame update
    private void Start()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
    }
    
    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
            Debug.Log("Next button clicked. Current Page: " + currentPage);
        }
    }

    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
            Debug.Log("Previous button clicked. Current Page: " + currentPage);
        }
    }

    public void MovePage()
    {
        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        Debug.Log("Vi tri: " + levelPagesRect.localPosition + " - current page: "+ currentPage);
    }
}
