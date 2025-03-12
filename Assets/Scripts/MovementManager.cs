using UnityEngine;
using System;
using Unity.Cinemachine;
using System.Collections;

enum MovementState
{
    Idle,
    Moving
}
public class MovementManager : MonoBehaviour
{
    public static event Action<Vector2> OnMoveToWayPoint;
    public static event Action<int, int> OnUpdateWayPointIndex;
    [SerializeField] Transform[] wayPoints;
    [SerializeField] int currentWayPointIndex = 0;
    [SerializeField] CinemachineCamera[] virtualCameras;
    [SerializeField] PlayerController playerController;
    [SerializeField] float beforeCameraDelay = 1f;
    private MovementState state = MovementState.Idle;

    private void Start()
    {
        Application.targetFrameRate = 60;
        UpdateActiveCamera();
        OnUpdateWayPointIndex?.Invoke(currentWayPointIndex, wayPoints.Length);
    }

    private void OnEnable()
    {
        PlayerController.OnFinishMove += OnFinishMovement;
    }

    private void OnDisable()
    {
        PlayerController.OnFinishMove -= OnFinishMovement;
    }

    public void MoveToPreviousWayPoint()
    {
        if (state == MovementState.Moving) return;

        if (currentWayPointIndex > 0)
        {
            currentWayPointIndex--;
            Move();
        }
    }

    public void MoveToNextWayPoint()
    {
        if (state == MovementState.Moving) return;

        if (currentWayPointIndex < wayPoints.Length - 1)
        {
            currentWayPointIndex++;
            Move();
        }
    }

    private void Move()
    {
        playerController.MoveTo(wayPoints[currentWayPointIndex].position);
        OnMoveToWayPoint?.Invoke(wayPoints[currentWayPointIndex].position);
        StartCoroutine(CameraMoveRoutine());
        state = MovementState.Moving;
    }

    private void UpdateActiveCamera()
    {
        foreach (var cam in virtualCameras)
        {
            cam.Priority = 0;
        }

        if (currentWayPointIndex < virtualCameras.Length)
        {
            virtualCameras[currentWayPointIndex].Priority = 20;
        }
    }

   void OnFinishMovement()
    {
        OnUpdateWayPointIndex?.Invoke(currentWayPointIndex, wayPoints.Length);
        state = MovementState.Idle;
    }

    IEnumerator CameraMoveRoutine()
    {
        yield return new WaitForSeconds(beforeCameraDelay);
        UpdateActiveCamera();
    }
}
