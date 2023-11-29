using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public List<Checkpoint> Checkpoints;
    public static CheckpointManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There is already another CheckpointManager in this scene !");
        }
    }

    private void Update()
    {
        for (int i = 1; i < Checkpoints.Count; i++)
        {
            if (Checkpoints[i - 1].IsPassed)
            {
                Checkpoints[i].CanBePassed = true;
            }
        }
    }
}
