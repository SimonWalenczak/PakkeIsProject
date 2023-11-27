using System.Collections.Generic;
using UnityEngine;

public class PlayerRanking : MonoBehaviour
{
    public List<Transform> checkpoints;
    public List<Transform> players;

    private Dictionary<Transform, int> checkpointCounts = new Dictionary<Transform, int>();

    private void Start()
    {
        // Initialize checkpoint counts for each player
        foreach (Transform player in players)
        {
            checkpointCounts[player] = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.gameObject.layer == LayerMask.NameToLayer("Checkpoint"))
        {
            // Player entered a checkpoint trigger
            UpdatePlayerRanking(other.transform);
        }
    }

    private void UpdatePlayerRanking(Transform player)
    {
        // Increment checkpoint count for the player
        checkpointCounts[player]++;

        // Sort players based on checkpoint counts and distances to the next checkpoint
        players.Sort((p1, p2) =>
        {
            int checkpointComparison = checkpointCounts[p2].CompareTo(checkpointCounts[p1]);

            if (checkpointComparison == 0)
            {
                // If players have the same number of checkpoints passed, compare distances to the next checkpoint
                float distanceToNextCheckpointP1 = Vector3.Distance(p1.position, GetNextCheckpoint(p1).position);
                float distanceToNextCheckpointP2 = Vector3.Distance(p2.position, GetNextCheckpoint(p2).position);

                return distanceToNextCheckpointP1.CompareTo(distanceToNextCheckpointP2);
            }

            return checkpointComparison;
        });
    }

    private Transform GetNextCheckpoint(Transform player)
    {
        // Find the next checkpoint based on the player's current checkpoint count
        int currentCheckpointIndex = Mathf.Clamp(checkpointCounts[player], 0, checkpoints.Count - 1);
        return checkpoints[currentCheckpointIndex];
    }
}
