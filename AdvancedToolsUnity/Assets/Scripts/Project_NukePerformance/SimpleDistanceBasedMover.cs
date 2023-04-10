using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is intentionally designed to be very poor on performance to push the engine
/// to its limits. It is a slightly simpler variation on the Distance Based Mover.
/// </summary>
public class SimpleDistanceBasedMover : MonoBehaviour
{
    [Header("Individual Settings")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Vector3 maxOffset;

    private void Update()
    {
        MoveTowardsNearestObject();
        MoveRandomOffset();
    }

    private void MoveTowardsNearestObject()
    {
        List<Transform> others = new List<Transform>();
        foreach (Transform child in transform.parent) {
            if (Vector3.Distance(child.position, transform.position) > 1f) {
                others.Add(child);
            }
        }
        if (others.Count <= 0) { return; }
        //move towards closest
        transform.position = Vector3.MoveTowards(transform.position, GetClosestTransform(others).position, Time.deltaTime * moveSpeed);
    }
    private Transform GetClosestTransform(List<Transform> others)
    {
        int closest = 0;
        float smallestDistance = Vector3.Distance(transform.position, others[0].position);
        //start at 1, since 0 has already been recorded
        for (int i = 1; i < others.Count; i++) {
            float dist = Vector3.Distance(transform.position, others[i].position);
            if (dist < smallestDistance) {
                //new closest, record
                smallestDistance = dist;
                closest = i;
            }
        }
        return others[closest];
    }

    private void MoveRandomOffset()
    {
        transform.position += new Vector3(
            Random.Range(-maxOffset.x, maxOffset.x),
            Random.Range(-maxOffset.y, maxOffset.y),
            Random.Range(-maxOffset.z, maxOffset.z)
        );
    }
}
