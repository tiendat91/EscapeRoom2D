using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public void DestroySelfAnimationEvent()
    {
        Destroy(gameObject);
        //Ending player attack
        GetComponentInParent<HeroController>().IsAttack = false;
    }
        
    /// <summary>
    /// This checks if the player can attack once from begin to end animation
    /// </summary>
    public void BeginAttackAnimation()
    {
        GetComponentInParent<HeroController>().CanAttackAgain = false;
    }

    public void EndAttackAnimation()
    {
        GetComponentInParent<HeroController>().CanAttackAgain = true;
    }
}
