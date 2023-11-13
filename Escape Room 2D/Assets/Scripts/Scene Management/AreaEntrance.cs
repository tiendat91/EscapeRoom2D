using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;
    private void Start()
    {
        if(transitionName == SceneMangement.Instance.SceneTransitionName)
        {
            HeroController.Instance.transform.position = this.transform.position;
            Debug.Log("Da den vi tri");
        }
    }

}
