using UnityEngine;
using UnityEngine.UI;

public class TextControl : MonoBehaviour
{
    private Text text;
    public string TextVi;
    public string TextEn;
    private void Start()
    {
        text = GetComponent<Text>();
        if (Application.systemLanguage == SystemLanguage.Vietnamese)
        {
            text.text = TextVi.Replace("<br>", "\n");
            text.font = GridManager.Instance.Data.Vi;
        }
        else
        {
            text.text = TextEn.Replace("<br>", "\n");
            text.font = GridManager.Instance.Data.En;
        }
    }
}
