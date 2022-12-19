using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed;
    float moveSpeed;

    [Header("Patrol Settings")]
    private int currentPathPoint = 0;
    private WayPoint wayPoint;

    public static Action<EnemyController> onPathFinished;

    void Awake()
    {
        wayPoint = GameObject.FindGameObjectWithTag("WayPoint").GetComponent<WayPoint>();
        moveSpeed = movementSpeed;

        Flip();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, wayPoint.GetPointPosition(currentPathPoint).position) < 0.1f)
        {
            currentPathPoint++;

            if (currentPathPoint >= wayPoint._wayPointList.Count)
            {
                ResetCurrentPathPoint();

                onPathFinished?.Invoke(this);

                gameObject.SetActive(false);
                return;
            }
            Flip();
        }
        
        Move();
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoint.GetPointPosition(currentPathPoint).position, 
            moveSpeed * Time.deltaTime);
    }

    public void ResetCurrentPathPoint()
    {
        currentPathPoint = 0;
    }

    public void Flip()
    {
        if (wayPoint.GetPointPosition(currentPathPoint).position.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void StopMovement()
    {
        moveSpeed = 0;
    }

    public void ResumeMovement()
    {
        if (!GetComponent<HealthManager>().GetEnemyisDead())
        {
            moveSpeed = movementSpeed;
        }
    }
}
