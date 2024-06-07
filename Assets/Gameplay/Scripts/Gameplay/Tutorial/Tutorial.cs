using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : LocalSingleton<Tutorial>
{
    // Start is called before the first frame update
    public bool _tutorial = true;

    [SerializeField] private RectTransform rect;
    [SerializeField] private Text textTutorial;
    private int first;
    private int second;
    private Cube selected;

    private int count0;
    private int count1;
    private int count2;

    public Cube Selected => selected;

    private void Start()
    {
        _tutorial = true;
        Messenger.AddListener(GlobalStrings.EventCall.START_GAME, StartGame);
    }

    public void StartGame()
    {
        if (GameSave.TotalPlayCount == 1 && _tutorial)
        {
            // start tutorial
            count0 = 0;
            count1 = 0;
            count2 = 0;
            MainUi.Instance.PlayPanel1.SetActive(false);
            Tutorial1();
        }
        else
        {
            _tutorial = false;
        }
    }

    public void Tutorial1()
    {
        textTutorial.gameObject.SetActive(true);
        rect.gameObject.SetActive(true);
        if (Application.systemLanguage == SystemLanguage.Vietnamese)
        {
            textTutorial.text = "Ấn để di chuyển ô vào khay.";
            textTutorial.font = GridManager.Instance.Data.Vi;
        }
        else
        {
            textTutorial.text = "Click to move tile to Box.";
            textTutorial.font = GridManager.Instance.Data.En;
        }
        foreach (var item in GridManager.Instance.listCube)
        {
            if (item.Depth == -1)
            {
                if (item.TypeCube == 0)
                {
                    count0++;
                }
                else if (item.TypeCube == 1)
                {
                    count1++;
                }
                else if (item.TypeCube == 2)
                {
                    count2++;
                }
            }
        }

        if (count0 > 3)
        {
            first = 0;
        }
        else if (count0 > 1)
        {
            second = 0;
        }

        if (count1 > 3)
        {
            first = 1;
        }
        else if (count1 > 1)
        {
            second = 1;
        }

        if (count2 > 3)
        {
            first = 2;
        }
        else if (count2 > 1)
        {
            second = 2;
        }
        foreach (var item in GridManager.Instance.listCube)
        {
            if (item.Depth == -1 && item.TypeCube == first)
            {
                Vector3 root = Camera.main.WorldToScreenPoint(item.transform.position);
                root.z = 0;
                rect.transform.position = root;
                selected = item;
                StartCoroutine(WaitTutorial1());
                break;
            }
        }
    }

    public IEnumerator WaitTutorial1()
    {
        while (true)
        {
            if (GridManager.Instance.DeletedBoxGrid.listCube.Contains(selected))
            {
                Tutorial2();
                yield break;
            }
            yield return null;
        }
    }

    public void Tutorial2()
    {
        textTutorial.gameObject.SetActive(true);
        rect.gameObject.SetActive(true);
        if (Application.systemLanguage == SystemLanguage.Vietnamese)
        {
            textTutorial.text = "Ấn để di chuyển ô vào khay.";
            textTutorial.font = GridManager.Instance.Data.Vi;
        }
        else
        {
            textTutorial.text = "Click to move tile to Box.";
            textTutorial.font = GridManager.Instance.Data.En;
        }
        foreach (var item in GridManager.Instance.listCube)
        {
            if (item.Depth == -1 && item.TypeCube == first)
            {
                Vector3 root = Camera.main.WorldToScreenPoint(item.transform.position);
                root.z = 0;
                rect.transform.position = root;
                selected = item;
                StartCoroutine(WaitTutorial2());
                break;
            }
        }
    }

    public IEnumerator WaitTutorial2()
    {
        while (true)
        {
            if (GridManager.Instance.DeletedBoxGrid.listCube.Contains(selected))
            {
                Tutorial3();
                yield break;
            }
            yield return null;
        }
    }

    public void Tutorial3()
    {
        textTutorial.gameObject.SetActive(true);
        rect.gameObject.SetActive(true);
        if (Application.systemLanguage == SystemLanguage.Vietnamese)
        {
            textTutorial.text = "Ấn để di chuyển ô vào khay.";
            textTutorial.font = GridManager.Instance.Data.Vi;
        }
        else
        {
            textTutorial.text = "Click to move tile to Box.";
            textTutorial.font = GridManager.Instance.Data.En;
        }
        foreach (var item in GridManager.Instance.listCube)
        {
            if (item.Depth == -1 && item.TypeCube == second)
            {
                Vector3 root = Camera.main.WorldToScreenPoint(item.transform.position);
                root.z = 0;
                rect.transform.position = root;
                selected = item;
                StartCoroutine(WaitTutorial3());
                break;
            }
        }
    }

    public IEnumerator WaitTutorial3()
    {
        while (true)
        {
            if (GridManager.Instance.DeletedBoxGrid.listCube.Contains(selected))
            {
                Tutorial4();
                yield break;
            }
            yield return null;
        }
    }

    public void Tutorial4()
    {
        textTutorial.gameObject.SetActive(true);
        rect.gameObject.SetActive(true);
        if (Application.systemLanguage == SystemLanguage.Vietnamese)
        {
            textTutorial.text = "Khi có 3 ô giống nhau trong khay, chúng sẽ bị xóa.";
            textTutorial.font = GridManager.Instance.Data.Vi;
        }
        else
        {
            textTutorial.text = "When have same 3 tile in Box, they will be delete.";
            textTutorial.font = GridManager.Instance.Data.En;
        }
        foreach (var item in GridManager.Instance.listCube)
        {
            if (item.Depth == -1 && item.TypeCube == first)
            {
                Vector3 root = Camera.main.WorldToScreenPoint(item.transform.position);
                root.z = 0;
                rect.transform.position = root;
                selected = item;
                StartCoroutine(WaitTutorial4());
                break;
            }
        }
    }

    public IEnumerator WaitTutorial4()
    {
        while (true)
        {
            if (GridManager.Instance.DeletedBoxGrid.listCube.Contains(selected))
            {
                textTutorial.gameObject.SetActive(false);
                rect.gameObject.SetActive(false);
                _tutorial = false;
                MainUi.Instance.PlayPanel1.SetActive(true);
                yield break;
            }
            yield return null;
        }
    }
}
