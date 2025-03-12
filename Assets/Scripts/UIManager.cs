using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;

    private void OnEnable()
    {
        MovementManager.OnUpdateWayPointIndex += UpdateButtons;
        MovementManager.OnMoveToWayPoint += DisableButtons;
    }

    private void OnDisable()
    {
        MovementManager.OnUpdateWayPointIndex -= UpdateButtons;
        MovementManager.OnMoveToWayPoint -= DisableButtons;
    }

    private void UpdateButtons(int index, int totalWayPoints)
    {
        leftButton.interactable = index > 0;
        rightButton.interactable = index < totalWayPoints - 1;
    }

    void DisableButtons(Vector2 vec)
    {
        leftButton.interactable = false;
        rightButton.interactable = false;
    }
}
