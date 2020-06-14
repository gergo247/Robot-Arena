using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAi : MonoBehaviour
{
    private static List<Character> Allys;
    public Animator animator;

    public float repelRange = 5f;
    public float repelAmount = 1f;
    public float startMaxChaseDistance = 20f;
    private float maxChaseDistance;

    [Header("Shooting")]
    public bool isShooter = false;
    public float strafeSpeed = 1f;
    public float shootDistance = 5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    private float nextTimeToFire = .5f;
    // private Rigidbody2D rb;
    private Character character;
    private Vector3 velocity;
    private Character target;
    bool canAttack;
    float autoAttackCurrentTime;
    AudioSource audioSource;

    void Start()
    {
        character = GetComponent<Character>();
        if (Allys == null)
        {
            Allys = new List<Character>();
        }
        Allys.Add(character);


        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.2f;
    }

    private void OnDestroy()
    {
        if (Allys != null)
          if (Allys.Contains(character))
              Allys.Remove(character);
    }
    void FixedUpdate()
    {
        if (character.dead)
        {
            return;
        }

        if (target == null)
        {
            LookForClosestEnemy();
        }
        if (target == null)
        {
            if (animator != null)
            animator.SetFloat("Speed", 0);

            return;
        }
        if (animator != null)
            animator.SetFloat("Speed", character.characterClass.moveSpeed);


        float distance = Vector2.Distance(character.rb.position, target.rb.position);
        Vector2 direction = (target.rb.position - character.rb.position).normalized;
        //TODO: bool solution, this is hack
        if (direction.x > 0)
        {
            transform.localScale = new Vector2(-1,1);
        }
        else
        {
            transform.localScale = new Vector2(1,1);
        }
        Vector2 newPos;
        if (distance < character.characterClass.attackRange)
            canAttack = true;
        else
            canAttack = false;

        //moving towards target
        MoveTowards();
        Attack();
        if (isShooter)
        {
            character.rb.AddForce(direction, ForceMode2D.Force);
        }
    }

    void Shoot()
    {
        if (Time.time >= nextTimeToFire)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Destroy(bullet, 30f);

            nextTimeToFire = Time.time + 1f / fireRate;
        }
    }
    void Attack()
    {
        if (canAttack)
            if (autoAttackCurrentTime < character.characterClass.autoAttackCooldown)
            {
                autoAttackCurrentTime += Time.deltaTime;
            }
            else
            {
                //attack animations
                int randomInt = Random.Range(0, character.characterClass.attakcAnimations.Count-1);
                //animation calls damage too
                Debug.Log("random : " + randomInt + "size 1: " + character.characterClass.attakcAnimations.Count + "Size 2:" + character.characterClass.attakcSounds.Count);
                if (character.characterClass.attakcAnimations.Count > 0)
                { 
                animator.Play(character.characterClass.attakcAnimations[randomInt].name);
                
                    if (audioSource != null && character.characterClass.attakcSounds.Count > randomInt)
                        audioSource.PlayOneShot(character.characterClass.attakcSounds[randomInt]);
                }


                autoAttackCurrentTime = 0;
            }
    }

    public void DamageTargetFromAnimation()
    {
        if (target != null)
          target.TakeDamage(character.characterClass.damage);
    }
    void LookForClosestEnemy()
    {
        GameObject[] targetGO = GameObject.FindGameObjectsWithTag("Character");
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector2 currentPosition = transform.position;
        if (targetGO != null)
        {
            foreach (GameObject go in targetGO)
            {
               Character tempTarget = go.GetComponent(typeof(Character)) as Character;
                if(character.team != tempTarget.team)
                {
                    float distanceToTarget = Vector2.Distance(tempTarget.rb.position, currentPosition);
                    if (distanceToTarget < minDist)
                    {
                        minDist = distanceToTarget;
                        target = tempTarget;
                     }
                }
            }
        }
    }
    void MoveTowards()
    {
        //simple move towards
        Vector2 newPos = Vector2.zero;
        var step = character.characterClass.moveSpeed * Time.deltaTime;
        if (Vector2.Distance(transform.position, target.rb.position) > character.characterClass.distanceToKeepWithTarget)
        { 
        transform.position = Vector2.MoveTowards(transform.position, target.rb.position, step);
        }
        else
            return;
        Vector2 repelForce = Vector2.zero;
    }
}
