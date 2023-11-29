using UnityEngine;

public class CameraCheckerPlayer : MonoBehaviour
{
    public float TimerOutOfView;
    public Camera CameraMain;
    public bool IsInCameraViewValue;
    public float Timer;

    private Plane[] _cameraFrustrum;
    private Bounds _bounds;
    private Collider _collider;

    public bool IsDisqualified;

    private void Start()
    {
        CameraMain = Camera.main;
        _collider = GetComponent<Collider>();
        IsDisqualified = false;
    }

    private void Update()
    {
        _bounds = _collider.bounds;
        _cameraFrustrum = GeometryUtility.CalculateFrustumPlanes(CameraMain);
        IsInCameraViewValue = GeometryUtility.TestPlanesAABB(_cameraFrustrum, _bounds);

        if (IsInCameraViewValue == false)
        {
            if (Timer < TimerOutOfView)
            {
                Timer += Time.deltaTime;
            }
            else
            {
                DisqualifiedPlayer();
            }
        }
        else
        {
            Timer = 0;
        }
    }

    public void DisqualifiedPlayer()
    {
        IsDisqualified = true;
        if (GameManager.Instance.DisqualifiedPlayers.Contains(GetComponent<PlayerRank>()) == false)
            GameManager.Instance.DisqualifiedPlayers.Add(GetComponent<PlayerRank>());
    }
}