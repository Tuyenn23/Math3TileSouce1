using UnityEngine;
using UnityEngine.UI;

public class MainPlay : MonoBehaviour
{
    [SerializeField] private Button drop3;
    [SerializeField] private Button undo;
    [SerializeField] private Button reroll;

    [SerializeField] private GameObject drop3Panel;
    [SerializeField] private GameObject undoPanel;
    [SerializeField] private GameObject rerollPanel;


    [SerializeField] private GameObject Add1Slot;

    private void Awake()
    {
        drop3.onClick.AddListener(OpenPopup);
        undo.onClick.AddListener(OpenPopup);
        reroll.onClick.AddListener(OpenPopup);
    }

    public void OpenPopup()
    {
        GridManager.Instance.Playable = false;
    }


    public void HideAll()
    {
        GridManager.Instance.Playable = true;
        drop3Panel.SetActive(false);
        undoPanel.SetActive(false);
        rerollPanel.SetActive(false);
        Add1Slot.SetActive(false);
    }

    public void Add1()
    {
        GridManager.Instance.Playable = false;
        Add1Slot.SetActive(true);
    }
}
