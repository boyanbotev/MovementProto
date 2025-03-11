using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject leftButton;
    [SerializeField] GameObject rightButton;

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
        leftButton.GetComponent<Button>().interactable = index > 0;
        rightButton.GetComponent<Button>().interactable = index < totalWayPoints - 1;
    }

    void DisableButtons(Vector2 vec)
    {
        leftButton.GetComponent<Button>().interactable = false;
        rightButton.GetComponent<Button>().interactable = false;
    }
}
