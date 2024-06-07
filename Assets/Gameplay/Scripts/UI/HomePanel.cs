using System.Collections;
using TMPro;
using UnityEngine;

public class HomePanel : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI textComponent2;
    public float scrollSpeed;
    private RectTransform m_textRecttransform;
    private RectTransform m_textRecttransform2;
    [SerializeField] private GameObject startPanel;
    private void Start()
    {
        m_textRecttransform = textComponent.GetComponent<RectTransform>();
        m_textRecttransform2 = textComponent2.GetComponent<RectTransform>();
        StartCoroutine(LoadNumber());
    }

    private void OnEnable()
    {
        if (textComponent.text.Equals(""))
        {
            return;
        }
        startPanel.SetActive(false);
        StartCoroutine(Run());
    }

    public IEnumerator LoadNumber()
    {
        while (PlayfabManager.Instance.TotalPlayer == 0)
        {
            yield return null;
        }
        if (Application.systemLanguage == SystemLanguage.Vietnamese)
        {
            textComponent.text = "Hôm nay có <color=red>" + (PlayfabManager.Instance.TotalPlayer * 111 + 7) + "</color> người tham gia thử thách.  Hôm nay có <color=blue>" + (Mathf.RoundToInt(PlayfabManager.Instance.TotalPlayer * 0.9f)) + "</color> người vượt qua tất cả thử thách.";
            textComponent2.text = "<mark=#999999 padding='10, 10, 0, 0'>Hôm nay có <color=red>" + (PlayfabManager.Instance.TotalPlayer * 111 + 7) + "</color> người tham gia thử thách.  Hôm nay có <color=blue>" + (Mathf.RoundToInt(PlayfabManager.Instance.TotalPlayer * 0.9f)) + "</color> người vượt qua tất cả thử thách.";
        }
        else
        {
            textComponent.text = "Today have <color=red>" + (PlayfabManager.Instance.TotalPlayer * 111 + 7) + "</color> player played.  Today have <color=blue>" + (Mathf.RoundToInt(PlayfabManager.Instance.TotalPlayer * 0.9f)) + "</color> player pass all levels.";
            textComponent2.text = "<mark=#999999 padding='10, 10, 0, 0'>Today have <color=red>" + (PlayfabManager.Instance.TotalPlayer * 111 + 7) + "</color> player played.  Today have <color=blue>" + (Mathf.RoundToInt(PlayfabManager.Instance.TotalPlayer * 0.9f)) + "</color> player pass all levels.";
        }
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        float width = textComponent.preferredWidth * 2;

        float scrollPosition = 0;

        while (true)
        {
            float remaider = scrollPosition % width;

            m_textRecttransform.anchoredPosition = new Vector3(-remaider, 0);
            m_textRecttransform2.anchoredPosition = new Vector3(-remaider, 0);
            scrollPosition += scrollSpeed * 20 * Time.deltaTime;

            yield return null;
        }

    }
}
