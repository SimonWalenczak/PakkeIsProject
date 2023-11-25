using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Debrit : MonoBehaviour
{
    public LayerMask TargetLayer;

    public float SpeedDecreased;

    private void OnTriggerEnter(Collider other)
    {
        if (Contains(TargetLayer, other.gameObject.layer))
        {
            print(other.GetComponent<GPEActor>().gameObject.name);
            other.GetComponent<GPEActor>().Debrits.Add(this);

            if (other.GetComponent<GPEActor>().IsOnDebrit == false)
            {
                other.GetComponent<GPEActor>().IsOnDebrit = true;
                other.GetComponent<GPEActor>().actualVelocity = other.GetComponent<Rigidbody>().velocity;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Contains(TargetLayer, other.gameObject.layer))
        {
            other.GetComponent<Rigidbody>().velocity = other.GetComponent<GPEActor>().actualVelocity * SpeedDecreased;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Contains(TargetLayer, other.gameObject.layer))
        {
            other.GetComponent<GPEActor>().Debrits.Remove(this);
        }
    }

    private static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}