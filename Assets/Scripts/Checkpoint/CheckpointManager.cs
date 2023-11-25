using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public List<Checkpoint> Checkpoints;

    public Checkpoint NextcheckPoint;
    
    private void Update()
    {
        for (var i = 0; i < Checkpoints.Count; i++)
        {
            if (Checkpoints[i - 1].IsPassed == true)
            {
                NextcheckPoint = Checkpoints[i];
            }
        }
    }
}
