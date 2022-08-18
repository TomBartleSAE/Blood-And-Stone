using System;
using System.Collections;
using System.Collections.Generic;
using Tom;
using UnityEngine;

public class VampireModel : MonoBehaviour
{
    public ClickMovement movement;
    
    public ParticleSystem burnParticle;

    public float feedRange = 0.6f; // Slightly larger than distance when touching to ensure feeding is triggered
    public int bloodGain = 10;

    public Animator anim;

    public event Action VampireFeedingEvent;

    private void Start()
    {
        LevelTimer.Instance.TimerFinishedEvent += BurnVampire;
    }

    private void OnDestroy()
    {
        LevelTimer.Instance.TimerFinishedEvent -= BurnVampire;
    }

    private void Update()
    {
        if (movement.target != null)
        {
            if (Vector3.Distance(transform.position, movement.target.position) < feedRange)
            {
                if (movement.target.GetComponent<VillagerModel>())
                {
                    Feed(movement.target.gameObject);
                }

                //Added for guard conversion interaction - AM
                if (movement.target != null && movement.target.GetComponent<GuardModel>())
                {
                    //won't allow to convert if investigating/chasing
                    if (movement.target.GetComponent<GuardModel>().isAlert || movement.target.GetComponent<GuardModel>().hasTarget)
                    {
                        return;
                    }
                    
                    ConvertGuard(movement.target.gameObject);
                }
            }
        }
    }

    public void Feed(GameObject victim)
    {
        StartCoroutine(FeedRoutine(victim));
    }

    public IEnumerator FeedRoutine(GameObject victim)
    {
        // TODO: Move any view code here to a separate script
        movement.clickMovementActive = false;
        // Play animations on vampire and victim
        movement.target = null;
        //victim.transform.position = transform.position;
        victim.GetComponent<Health>().ChangeHealth(-1f, gameObject); // Kills the victim
        //following line moved to bottom of function; wouldn't work otherwise - AM
        PlayerManager.Instance.ChangeBlood(bloodGain);
        anim.SetTrigger("Attack");
        VampireFeedingEvent?.Invoke();
        yield return new WaitForSeconds(3f);
        movement.clickMovementActive = true;
    }

    //Guard Conversion Interaction - AM
    public void ConvertGuard(GameObject victim)
    {
        movement.target = null;
        victim.GetComponent<Health>().ChangeHealth(-1f, gameObject);
    }

    public void BurnVampire()
    {
        ParticleSystem newParticle = Instantiate(burnParticle, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        newParticle.Play();
        Destroy(gameObject);
    }
}
