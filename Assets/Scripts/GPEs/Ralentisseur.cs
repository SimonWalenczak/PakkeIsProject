using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ralentisseur : MonoBehaviour
{
    public LayerMask TargetLayer;

    private void OnTriggerEnter(Collider other)
    {
        if (Contains(TargetLayer, other.gameObject.layer))
        {
            if (other.GetComponentInParent<CharacterMultiplayerManager>().IsInvincible == false)
            {
                print("Banana");
                other.GetComponentInParent<CharacterMultiplayerManager>().IsInvincible = true;   
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
        yield return new WaitForSeconds(3); // time of animation
        
        
        //Player can move
        print("unfreeze position");
        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;

        //Time before next Banana
        yield return new WaitForSeconds(3);
        other.GetComponentInParent<CharacterMultiplayerManager>().IsInvincible = false;   
    }
}