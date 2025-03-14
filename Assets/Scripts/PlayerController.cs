using System;
using UnityEngine;
enum PlayerState
{
    Idle,
    Moving
}

public class PlayerController : MonoBehaviour
{
    public static event Action OnFinishMove;
    [SerializeField] float minDistance = 0.1f;
    [SerializeField] float speed = 5f;
    private PlayerState state = PlayerState.Idle;
    private Vector2 targetPos;

    public void MoveTo(Vector2 pos)
    {
        targetPos = pos;
        state = PlayerState.Moving;
    }

    void Update()
    {
        if (state == PlayerState.Moving)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, targetPos) < minDistance)
            {
                state = PlayerState.Idle;
                transform.position = targetPos;
                OnFinishMove?.Invoke();
            }
        }
    }
}
