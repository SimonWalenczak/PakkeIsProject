using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public LayerMask TargetLayer;
    public bool IsPassed;
    public bool CanBePassed;

    public int CheckpointIndex;
    
    private void OnTriggerEnter(Collider other)
    {
        if (Contains(TargetLayer, other.gameObject.layer))
        {
            PlayerRank player = other.GetComponent<PlayerRank>();
            
            if (CanBePassed && player.currentCheckpointIndex == CheckpointIndex)
            {
                IsPassed = true;
                if (player.currentCheckpointIndex == CheckpointManager.Instance.Checkpoints.Count)
                {
                    Debug.Log("Lap finished !");
                    if (player.currentNbLap == 1)
                    {
                        player.currentCheckpointIndex = 2;
                        player.currentNbLap = 2;
                    }
                    else if (player.currentNbLap == 2)
                    {
                        GameManager.Instance.FinishedPlayers.Add(player);
                    }
                }
                else if (player.currentCheckpointIndex < CheckpointManager.Instance.Checkpoints.Count)
                {
                    player.currentCheckpointIndex++;
                }
            }
        }
    }

    private static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}