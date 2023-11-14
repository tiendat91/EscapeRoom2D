using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public void DestroySelfAnimationEvent()
    {
        Destroy(gameObject);
        //Ending player attack
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
}
