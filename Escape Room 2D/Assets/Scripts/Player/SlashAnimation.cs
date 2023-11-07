using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAnimation : MonoBehaviour
{
    public void SelfDestroy()
    {
        Destroy(gameObject);
        //Ending player actack
        GetComponentInParent<HeroController>().IsAttack = false;
    }
}
