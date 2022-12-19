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

    void Awake()
    {
        wayPoint = GameObject.FindGameObjectWithTag("WayPoint").GetComponent<WayPoint>();
        moveSpeed = movementSpeed;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, wayPoint.GetPointPosition(currentPathPoint).position) < 0.1f)
        {
            currentPathPoint++;

            if (currentPathPoint >= wayPoint._wayPointList.Count)
            {
                ResetCurrentPathPoint();

                gameObject.SetActive(false);
                return;
            }
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
}
