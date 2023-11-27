using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
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
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            StartCinematicEnded = true;
        }

        foreach (var player in Players)
        {
            if (player.gameObject.GetComponent<InTheScreen>().IsNotDisqualified == false)
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
    }
}