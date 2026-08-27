using UnityEngine;
using Unity.Cinemachine;

public class GameModeManager : MonoBehaviour
{
    public static bool multiplayer = false;

    public GameObject jugador2;
    public CameraTargetMultiplayer cameraTarget;
    public CinemachineCamera camara;

    private void Start()
    {
        if (multiplayer)
        {
            // MULTIPLAYER
            jugador2.SetActive(true);
            cameraTarget.enabled = true;

            // La cámara sigue al punto medio
            camara.Target.TrackingTarget = cameraTarget.transform;
        }
        else
        {
            // SOLO
            jugador2.SetActive(false);
            cameraTarget.enabled = false;

            // La cámara vuelve a seguir al P1
            Transform pivotP1 = GameObject.Find("Hero_Rock/CameraPivot").transform;
            camara.Target.TrackingTarget = pivotP1;
        }
    }
}