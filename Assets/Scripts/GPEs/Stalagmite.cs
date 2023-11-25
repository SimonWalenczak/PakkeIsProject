using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Stalagmite : MonoBehaviour
{
    public LayerMask TargetLayer;

    public float offsetThrowing;
    public float duration;
    public int numberOfTurns;

    public float TimeBeforeBeingVulnerable;
    
    private void OnTriggerEnter(Collider other)
    {
        if (Contains(TargetLayer, other.gameObject.layer))
        {
            if (other.GetComponent<GPEActor>().IsInvincible == false)
            {
                print("Banana");
                other.GetComponent<GPEActor>().IsInvincible = true;
                StartCoroutine(Banana(other.gameObject));
            }
        }
    }

    private static bool Contains(LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }

    IEnumerator Banana(GameObject other)
    {
        //Player can't move
        print("freeze position");
        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

        //play animation with Dotween
        other.transform.DOMove(other.transform.position + other.transform.forward * offsetThrowing, duration)
            .SetEase(Ease.Linear);
        other.transform.DORotate(new Vector3(0, 360 * numberOfTurns, 0), duration, RotateMode.FastBeyond360)
            .SetEase(Ease.InQuint).SetEase(Ease.OutQuint)
            .SetLoops(1);
        yield return new WaitForSeconds(duration); // time of animation

        //Player can move
        print("unfreeze position");
        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;

        //Time before next Banana
        yield return new WaitForSeconds(TimeBeforeBeingVulnerable);
        other.GetComponent<GPEActor>().IsInvincible = false;
    }
}