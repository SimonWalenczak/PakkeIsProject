using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    public float TimerCinematic;
    public GameObject CinematicParent;

    public List<GameObject> decompteImage;

    public List<Transform> Players;
    public Vector3 rotationCam;
    public Vector3 offset;

    public float zoomSpeed = 5.0f;

    public float maxFOV = 60f;
    public float minFOV = 20f;

    private Camera cam;

    public bool StartCinematicEnded;

    void Start()
    {
        cam = GetComponent<Camera>();

        StartCinematicEnded = false;


        StartCoroutine(WaitingForSpawn());
        StartCoroutine(WaitingForCinematic());
    }

    IEnumerator WaitingForCinematic()
    {
        yield return new WaitForSeconds(TimerCinematic);
        CinematicParent.SetActive(false);
        StartCinematicEnded = true;

        yield return new WaitForSeconds(1);
        decompteImage[0].SetActive(true);
        yield return new WaitForSeconds(1);
        decompteImage[0].SetActive(false);

        decompteImage[1].SetActive(true);
        yield return new WaitForSeconds(1);
        decompteImage[1].SetActive(false);

        decompteImage[2].SetActive(true);
        yield return new WaitForSeconds(1);
        decompteImage[2].SetActive(false);

        decompteImage[3].SetActive(true);

        foreach (var player in Players)
        {
            player.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY +
                                                                      (int)RigidbodyConstraints.FreezeRotationZ +
                                                                      (int)RigidbodyConstraints.FreezeRotationX;
        }

        yield return new WaitForSeconds(1);
        decompteImage[3].SetActive(false);
    }

    void Update()
    {
        foreach (var player in Players)
        {
            if (player.gameObject.GetComponent<CameraCheckerPlayer>().IsDisqualified == true)
            {
                Players.Remove(player);
            }
        }

        if (StartCinematicEnded)
        {
            Vector3 averagePosition = Vector3.zero;
            foreach (Transform obj in Players)
            {
                averagePosition += obj.position;
            }

            averagePosition /= Players.Count;

            float maxDistance = 0f;
            foreach (Transform obj in Players)
            {
                float distance = Vector3.Distance(obj.position, averagePosition);
                maxDistance = Mathf.Max(maxDistance, distance);
            }

            float targetFOV = Mathf.Clamp(maxDistance * 1.5f, minFOV, maxFOV) + 10;
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);

            transform.position = averagePosition + offset;

            transform.rotation = Quaternion.Euler(rotationCam);
        }
    }

    IEnumerator WaitingForSpawn()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject[] players = GameObject.FindGameObjectsWithTag("Test");
        foreach (GameObject player in players)
        {
            Players.Add(player.transform);
        }

        foreach (var player in Players)
        {
            player.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation +
                                                                      (int)RigidbodyConstraints.FreezePositionZ +
                                                                      (int)RigidbodyConstraints.FreezePositionX;
        }
    }
}