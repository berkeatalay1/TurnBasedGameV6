using Actions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button actionButton;


    public void SetBaseAction(BaseAction action)
    {
        textMeshPro.text = action.GetActionName().ToUpper();
        actionButton.onClick.AddListener((() => { UnitActionSystem.Instance.SetSelectedAction(action); }));
    }

    private void MoveAction_onClick(BaseAction action)
    {
        
    }
}
