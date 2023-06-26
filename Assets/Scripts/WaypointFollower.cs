using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointPtr = 0;

    [SerializeField] private float speed = 2f;

    private void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointPtr].transform.position, transform.position) < .1f)
        {
            currentWaypointPtr++;
            if(currentWaypointPtr >= waypoints.Length)
            {
                currentWaypointPtr = 0;

            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointPtr].transform.position, Time.deltaTime * speed);
    }
} 
