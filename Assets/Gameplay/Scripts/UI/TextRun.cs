using System.Collections;
using TMPro;
using UnityEngine;


public class TextRun : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public float scrollSpeed;

    private RectTransform m_textRecttransform;

    //private TextMeshProUGUI m_CloneTextObject;
    private RectTransform m_textRectTransform;


    // Start is called before the first frame update
    private void Awake()
    {
        m_textRecttransform = textComponent.GetComponent<RectTransform>();
        //m_CloneTextObject = Instantiate(textComponent);
        //RectTransform cloneRectTransform = m_CloneTextObject.GetComponent<RectTransform>();
        //cloneRectTransform.SetParent(m_textRecttransform);
        //cloneRectTransform.anchoredPosition = new Vector2(0, 0);
        //cloneRectTransform.anchorMin = new Vector2(1.0f, 0.5f);
        //cloneRectTransform.localScale = Vector3.one;
    }

    private IEnumerator Start()
    {
        float width = textComponent.preferredWidth * 4;
        Vector3 startPosition = m_textRecttransform.position;

        float scrollPosition = 0;
        Debug.Log("Width" + width);

        while (true)
        {
            float remaider = scrollPosition % width;
            Debug.Log(remaider);

            m_textRecttransform.anchoredPosition = new Vector3(-remaider, 0);

            scrollPosition += scrollSpeed * 20 * Time.deltaTime;

            yield return null;
        }

    }
}