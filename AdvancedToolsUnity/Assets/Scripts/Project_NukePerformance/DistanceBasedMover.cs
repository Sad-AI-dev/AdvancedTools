using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is intentionally designed to be very poor on performance to push the engine
/// to its limits. Forgive me for I have written dogshit code.
/// </summary>
public class DistanceBasedMover : MonoBehaviour
{
    [SerializeField] private Transform objectHolder;

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
        foreach (Transform child in objectHolder) {
            if (Vector3.Distance(child.position, transform.position) > 1f) {
                others.Add(child);
            }
        }
        if (others.Count <= 0) { return; }
        //sort list based on distance (don't get closest!, sort entire list!)
        others.Sort((Transform t1, Transform t2) => Vector3.Distance(transform.position, t1.position).CompareTo(Vector3.Distance(transform.position, t2.position)));
        //move towards closest
        transform.position = Vector3.MoveTowards(transform.position, others[0].position, Time.deltaTime * moveSpeed);
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
