using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPEActor : MonoBehaviour
{
    public bool IsOnDebrit;
    public bool IsInvincible;
    [HideInInspector] public Vector3 actualVelocity;
    public List<Debrit> Debrits;

    private void Update()
    {
        if (Debrits.Count == 0 && IsOnDebrit)
        {
            IsOnDebrit = false;
            GetComponent<Rigidbody>().velocity = actualVelocity;
        }
    }
}
