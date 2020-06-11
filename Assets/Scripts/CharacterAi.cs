﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAi : MonoBehaviour
{
    private static List<Character> Allys;

    public float moveSpeed = 10f;

    [Range(0f, 1f)]
    public float turnSpeed = .1f;

    public float repelRange = 5f;
    public float repelAmount = 1f;
    public float distanceToKeepWithTarget = 1f;

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

    // Use this for initialization
    void Start()
    {
        character = GetComponent<Character>();

        if (Allys == null)
        {
            Allys = new List<Character>();
        }

        //    moveSpeed *= (Progression.Growth - 1f) * 0.5f + 1f;

        Allys.Add(character);
    }

    private void OnDestroy()
    {
        Allys.Remove(character);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null)
        {
            LookForClosestEnemy();
        }
        if (target == null)
            return;

        float distance = Vector2.Distance(character.rb.position, target.rb.position);
        //if (distance > maxChaseDistance)
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        Vector2 direction = (target.rb.position - character.rb.position).normalized;
        Vector2 newPos;
        //move with force ( overmoves rn)
        if (isShooter)
        {
           //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
           ////character.rb.rotation = angle;
           //if (distance > shootDistance)
           //{
           //    newPos = MoveRegular(direction);
           //}
           //else
           //{
           //    newPos = MoveStrafing(direction);
           //}
           ////Shoot();
           //newPos -= character.rb.position;
           // character.rb.AddForce(newPos, ForceMode2D.Force);
            character.rb.AddForce(direction, ForceMode2D.Force);
        }
        else
        {
            MoveTowards();
           //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
           //character.rb.rotation = Mathf.LerpAngle(character.rb.rotation, angle, turnSpeed);
           //newPos = MoveRegular(direction);
           //character.rb.MovePosition(newPos);
        }
      //  character.rb.eulerAngles = new Vector3(0, 0, 0);
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
    void LookForClosestEnemy()
    {
        GameObject[] targetGO = GameObject.FindGameObjectsWithTag("Character");
        Debug.Log("targetGO length : " + targetGO.Length);

        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector2 currentPosition = transform.position;
        if (targetGO != null)
        {
            foreach (GameObject go in targetGO)
            {
               Character tempTarget = go.GetComponent(typeof(Character)) as Character;
                if(character.Team != tempTarget.Team)
                {
                    float distanceToTarget = Vector2.Distance(tempTarget.rb.position, currentPosition);
                    if (distanceToTarget < minDist)
                    {
                        minDist = distanceToTarget;
                        target = tempTarget;
                        Debug.Log("found enemy : " + target);
                     }
                }
            }
        }
        //target.GetType
    }
    void MoveTowards()
    {
        //simple move towards
        var step = moveSpeed * Time.deltaTime;
        if (Vector2.Distance(transform.position, target.rb.position) > distanceToKeepWithTarget)
             transform.position = Vector3.MoveTowards(transform.position, target.rb.position, step);
    }

    Vector2 MoveStrafing(Vector2 direction)
    {
        Vector2 newPos = transform.position + transform.right * Time.fixedDeltaTime * strafeSpeed;
        return newPos;
    }

    Vector2 MoveRegular(Vector2 direction)
    {
        Vector2 repelForce = Vector2.zero;
        if (GameManager.instance != null)
            if (GameManager.instance.AllCharactersInScene.Count > 0)
            {
                foreach (Character characters in GameManager.instance.AllCharactersInScene)
                {
                    if (characters == character)
                        continue;

                    if (Vector2.Distance(characters.rb.position, character.rb.position) <= repelRange)
                    {
                        Vector2 repelDir = (character.rb.position - characters.rb.position).normalized;
                        repelForce += repelDir;
                    }
                }
            }

        Vector2 newPos = transform.position + transform.up * Time.fixedDeltaTime * moveSpeed;
        newPos += repelForce * Time.fixedDeltaTime * repelAmount;

        return newPos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, maxChaseDistance);
    }
}
