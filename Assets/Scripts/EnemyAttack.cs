using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class EnemyAttack : MonoBehaviour
{
    [BoxGroup("Attack Type")]
    [EnumToggleButtons]
    public AttackType attackType;
    [TabGroup("Attack Origins")]
    [EnableIf("@this.attackType == AttackType.Range")]
    [Required]
    public Transform firePoint;
    [TabGroup("Attack Origins")]
    [EnableIf("@this.attackType == AttackType.Range")]
    [Required]
    public GameObject bullet;
    [TabGroup("Attack Origins")]
    [EnableIf("@this.attackType == AttackType.Meele")]
    [Required]
    public GameObject hitbox;


    [TabGroup("Values")]
    public float minDmg;
    [TabGroup("Values")]
    public float maxDmg;
    [TabGroup("Values")]
    [EnableIf("@this.attackType == AttackType.Range")]
    public float projectileSpeed;
    [TabGroup("Values")]
    public float bulletLife;
    [TabGroup("Values")]
    public float attackDelay;
    [TabGroup("Values")]
    public string attackAnim;

    [TabGroup("Special")]
    public bool isBoss;


    public enum AttackType
    {
        Meele,
        Range
    }

    private float randmg, time;
    private Delusion d;
    private Animator a;
    private bool animating;

    // Start is called before the first frame update
    void Start()
    {
        d = FindObjectOfType<Delusion>();
        a = GetComponent<Animator>();

        if (!isBoss)
        {
            SetAnimLayer(attackType);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > attackDelay)
        {
            switch (attackType)
            {
                case AttackType.Range:
                    Shoot();
                    time = 0f;
                    break;

                case AttackType.Meele:
                    MeleeAttack();
                    time = 0f;
                    break;

                default:
                    Destroy(gameObject);
                    break;
            }
        }
    }

    public float CalculateDamage(float min, float max)
    {
        float result;

        result = Random.Range(min, max);
        
        return result;
    }

    void Shoot()
    {
        // Create a new projectile at the fire point's position and rotation
        GameObject projectile = Instantiate(bullet, firePoint.position, firePoint.rotation);

        // Get the Rigidbody component of the projectile and apply force to it
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * projectileSpeed;
        AnimationPlay();

        Destroy(projectile, bulletLife);
    }

    void MeleeAttack()
    {
        GameObject attackBox = Instantiate(hitbox, transform.position, transform.rotation);
        AnimationPlay();

        Destroy(attackBox, bulletLife);
    }

    void AnimationPlay()
    {
        a.SetTrigger(attackAnim);
        animating = true;
    }

    void SetAnimLayer(AttackType type)
    {
        switch (type)
        {
            case AttackType.Range:
                a.SetLayerWeight(0, 1f);
                a.SetLayerWeight(1, 0f);
                a.SetInteger("type", 1);
                break;
            case AttackType.Meele:
                a.SetLayerWeight(0, 0f);
                a.SetLayerWeight(1, 1f);
                a.SetInteger("type", 0);
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }

    bool DisableAnim()
    {
        animating = false;

        return animating;
    }

    public bool GetAnimBool()
    {
        return animating;
    }

    public float ResetTime()
    {
        time = 0f;
        return time;
    }
}
