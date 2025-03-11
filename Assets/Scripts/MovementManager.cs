using UnityEngine;
using System;
using Unity.Cinemachine;
using System.Collections;

public class MovementManager : MonoBehaviour
{
    public static event Action<Vector2> OnMoveToWayPoint;
    [SerializeField] Transform[] wayPoints;
    [SerializeField] int currentWayPointIndex = 0;
    [SerializeField] CinemachineCamera[] virtualCameras;

    private void Start()
    {
        Application.targetFrameRate = 60;
        UpdateActiveCamera();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) MoveToPreviousWayPoint();
        if (Input.GetKeyDown(KeyCode.RightArrow)) MoveToNextWayPoint();
    }

    public void MoveToPreviousWayPoint()
    {
        if (currentWayPointIndex > 0)
        {
            currentWayPointIndex--;
            Move();
        }
    }

    public void MoveToNextWayPoint()
    {
        if (currentWayPointIndex < wayPoints.Length - 1)
        {
            currentWayPointIndex++;
            Move();
        }
    }

    private void Move()
    {
        OnMoveToWayPoint?.Invoke(wayPoints[currentWayPointIndex].position);

        StartCoroutine(CameraMoveRoutine());
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

    IEnumerator CameraMoveRoutine()
    {
        yield return new WaitForSeconds(1f);
        UpdateActiveCamera();
    }
}
