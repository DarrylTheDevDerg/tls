using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DealDamage : MonoBehaviour
{
    [SerializeField] string searchedTag, animationTag;
    [SerializeField] float dealDmg;
    [SerializeField] bool destroyOnHit = false, shouldAnim, isEnemy;
    [SerializeField] UnityEvent onHit;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(searchedTag))
        {
            if (isEnemy)
            {
                other.GetComponent<Delusion>().LoseHP(dealDmg);
            }

            else
            {
                other.GetComponent<HealthSystem>().TakeDamage(dealDmg);
                other.GetComponent<EnemyAttack>().ResetTime();
            }

            if (shouldAnim)
            {
                Animator animator = other.GetComponent<Animator>();
                animator.SetTrigger(animationTag);
            }

            if (destroyOnHit)
            {
                onHit?.Invoke();
                Destroy(this.gameObject);
            }

        }
    }

    public void Spawn(GameObject go)
    {
        Instantiate(go, this.transform.parent);
    }

    public float GetDMG()
    {
        return dealDmg;
    }

    public string RetrieveSearchTag()
    {
        return searchedTag;
    }
}
