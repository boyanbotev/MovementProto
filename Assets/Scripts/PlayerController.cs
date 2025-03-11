using UnityEngine;
enum PlayerState
{
    Idle,
    Moving
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] float minDistance = 0.1f;
    [SerializeField] float speed = 5f;
    private PlayerState state = PlayerState.Idle;
    private Vector2 targetPos;

    public void MoveTo(Vector2 pos)
    {
        targetPos = pos;
        state = PlayerState.Moving;
    }

    private void OnEnable()
    {
        MovementManager.OnMoveToWayPoint += MoveTo;
    }
    private void OnDisable()
    {
        MovementManager.OnMoveToWayPoint -= MoveTo;
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
            }
        }
    }
}
