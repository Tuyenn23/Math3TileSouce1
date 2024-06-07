using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private int depth = 0;
    private Vector2 thisVec;
    [SerializeField] private SpriteRenderer spriteRenderer;
    //[SerializeField] private SpriteRenderer spriteRendererImage;
    public int TypeCube;
    public bool status;
    public bool killed;
    public Vector3 from;
    public float move;
    public Vector2 ThisVec => thisVec;

    private Color col = new Color(0.7f, 0.7f, 0.7f);
    public SpriteRenderer SpriteRenderer => spriteRenderer;

    public int Depth => depth;

    private void OnEnable()
    {
        Messenger.AddListener(GlobalStrings.EventCall.SURE_END_GAME, KillThisCubeForever);
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one * 2;
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GlobalStrings.EventCall.SURE_END_GAME, KillThisCubeForever);
    }

    public void InitCube(Vector2 vec, int _depth, bool type = false)
    {
        Messenger.AddListener(GlobalStrings.EventCall.CHECK, StartCheckGrid);
        Messenger.AddListener<Vector3>(GlobalStrings.EventCall.DELETE_BLOCK, CheckDeleteGrid);
        Messenger.AddListener(GlobalStrings.EventCall.REROLL, Reroll);
        transform.localScale = new Vector3(2, 2, 2);
        GridManager.Instance.listGrid.Add(new Vector3(vec.x, vec.y, _depth));
        GridManager.Instance.listGridPlace.Add(new Vector3(vec.x, vec.y, _depth));
        GridManager.Instance.listCube.Add(this);
        GridManager.Instance.listGrid.Add(new Vector3(vec.x - 1, vec.y, _depth));
        GridManager.Instance.listGrid.Add(new Vector3(vec.x, vec.y - 1, _depth));
        GridManager.Instance.listGrid.Add(new Vector3(vec.x - 1, vec.y - 1, _depth));
        transform.position = new Vector3(transform.position.x, transform.position.y, _depth * -0.05f);
        thisVec = vec;
        depth = _depth;
        if (type || GridManager.Instance.CurrentLevel == 0)
        {
            TypeCube = GridManager.Instance.GetNumber();
        }
        else
        {
            TypeCube = Random.Range(0, 15);
        }
        SpriteRenderer.sprite = GridManager.Instance.Data.GetListImage()[TypeCube];
        SpriteRenderer.sortingOrder = 1;
        if (_depth < -1)
        {
            TurnBlack();
        }
        else
        {
            TurnWhite();
        }
        status = true;
    }

    public void InitCubeAgain(Vector2 vec, int _depth)
    {
        Messenger.AddListener(GlobalStrings.EventCall.CHECK, StartCheckGrid);
        Messenger.AddListener<Vector3>(GlobalStrings.EventCall.DELETE_BLOCK, CheckDeleteGrid);
        Messenger.AddListener(GlobalStrings.EventCall.REROLL, Reroll);
        transform.localScale = new Vector3(2, 2, 2);
        GridManager.Instance.listGrid.Add(new Vector3(vec.x, vec.y, _depth));
        GridManager.Instance.listGridPlace.Add(new Vector3(vec.x, vec.y, _depth));
        GridManager.Instance.listCube.Add(this);
        GridManager.Instance.listGrid.Add(new Vector3(vec.x - 1, vec.y, _depth));
        GridManager.Instance.listGrid.Add(new Vector3(vec.x, vec.y - 1, _depth));
        GridManager.Instance.listGrid.Add(new Vector3(vec.x - 1, vec.y - 1, _depth));
        thisVec = vec;
        depth = _depth;
        SpriteRenderer.sortingOrder = 1;
        TurnWhite();
        status = true;
    }

    public void InitCube()
    {
        GridManager.Instance.listGrid.Add(new Vector3(thisVec.x, thisVec.y, depth));
        GridManager.Instance.listGridPlace.Add(new Vector3(thisVec.x, thisVec.y, depth));
        GridManager.Instance.listCube.Add(this);
        GridManager.Instance.listGrid.Add(new Vector3(thisVec.x - 1, thisVec.y, depth));
        GridManager.Instance.listGrid.Add(new Vector3(thisVec.x, thisVec.y - 1, depth));
        GridManager.Instance.listGrid.Add(new Vector3(thisVec.x - 1, thisVec.y - 1, depth));
        TurnWhite();
        transform.localScale = new Vector3(2, 2, 2);
        status = true;
        SpriteRenderer.sortingOrder = 1;
        Messenger.Broadcast(GlobalStrings.EventCall.CHECK);

        Messenger.AddListener(GlobalStrings.EventCall.CHECK, StartCheckGrid);
        Messenger.AddListener<Vector3>(GlobalStrings.EventCall.DELETE_BLOCK, CheckDeleteGrid);
        Messenger.AddListener(GlobalStrings.EventCall.REROLL, Reroll);
    }

    public void CheckDeleteGrid(Vector3 vec)
    {
        if (depth == vec.z)
        {
            if (CheckGridOverlap())
            {
                if (vec.x == thisVec.x && vec.y == thisVec.y)
                {
                    KillThisCube();
                    return;
                }

                if (vec.x == thisVec.x && vec.y == thisVec.y - 1)
                {
                    KillThisCube();
                    return;
                }

                if (vec.x == thisVec.x - 1 && vec.y == thisVec.y)
                {
                    KillThisCube();
                    return;
                }

                if (vec.x == thisVec.x - 1 && vec.y == thisVec.y - 1)
                {
                    KillThisCube();
                    return;
                }
            }
        }
        StartCheckGrid();
    }

    private void StartCheckGrid()
    {
        StartCoroutine(CheckGrid());
    }

    private IEnumerator CheckGrid()
    {
        yield return null;
        CheckGridOverlap();
    }

    private bool CheckGridOverlap()
    {

        for (int i = 0; i < GridManager.Instance.listGrid.Count; i++)
        {
            if (GridManager.Instance.listGrid[i].z > depth)
            {
                if (GridManager.Instance.listGrid[i].x == thisVec.x && GridManager.Instance.listGrid[i].y == thisVec.y)
                {
                    TurnBlack();
                    return false;
                }

                if (GridManager.Instance.listGrid[i].x == thisVec.x - 1 && GridManager.Instance.listGrid[i].y == thisVec.y)
                {
                    TurnBlack();
                    return false;
                }

                if (GridManager.Instance.listGrid[i].x == thisVec.x && GridManager.Instance.listGrid[i].y == thisVec.y - 1)
                {
                    TurnBlack();
                    return false;
                }

                if (GridManager.Instance.listGrid[i].x == thisVec.x - 1 && GridManager.Instance.listGrid[i].y == thisVec.y - 1)
                {
                    TurnBlack();
                    return false;
                }
            }
        }

        TurnWhite();
        return true;
    }


    private void Reroll()
    {
        TypeCube = GridManager.Instance.GetNumber();
        SpriteRenderer.sprite = GridManager.Instance.Data.GetListImage()[TypeCube];
    }



    private void KillThisCube()
    {
        if (Tutorial.Instance._tutorial && Tutorial.Instance.Selected != this)
        {
            return;
        }
        Messenger.RemoveListener<Vector3>(GlobalStrings.EventCall.DELETE_BLOCK, CheckDeleteGrid);
        Messenger.RemoveListener(GlobalStrings.EventCall.REROLL, Reroll);
        Messenger.RemoveListener(GlobalStrings.EventCall.CHECK, StartCheckGrid);

        GridManager.Instance.listGrid.Remove(new Vector3(thisVec.x, thisVec.y, depth));
        GridManager.Instance.listGridPlace.Remove(new Vector3(thisVec.x, thisVec.y, depth));
        GridManager.Instance.listCube.Remove(this);
        GridManager.Instance.listGrid.Remove(new Vector3(thisVec.x, thisVec.y - 1, depth));
        GridManager.Instance.listGrid.Remove(new Vector3(thisVec.x - 1, thisVec.y, depth));
        GridManager.Instance.listGrid.Remove(new Vector3(thisVec.x - 1, thisVec.y - 1, depth));
        GridManager.Instance.DeletedBoxGrid.AddCube(this);
    }

    [Button]
    private void KillThisCubeForever()
    {
        Messenger.RemoveListener<Vector3>(GlobalStrings.EventCall.DELETE_BLOCK, CheckDeleteGrid);
        Messenger.RemoveListener(GlobalStrings.EventCall.REROLL, Reroll);
        Messenger.RemoveListener(GlobalStrings.EventCall.CHECK, StartCheckGrid);

        GridManager.Instance.listGrid.Remove(new Vector3(thisVec.x, thisVec.y, depth));
        GridManager.Instance.listGridPlace.Remove(new Vector3(thisVec.x, thisVec.y, depth));
        GridManager.Instance.listCube.Remove(this);
        GridManager.Instance.listGrid.Remove(new Vector3(thisVec.x, thisVec.y - 1, depth));
        GridManager.Instance.listGrid.Remove(new Vector3(thisVec.x - 1, thisVec.y, depth));
        GridManager.Instance.listGrid.Remove(new Vector3(thisVec.x - 1, thisVec.y - 1, depth));

        gameObject.SetActive(false);
    }

    public void TurnBlack()
    {
        SpriteRenderer.color = col;
        //spriteRendererImage.color = Color.gray;
    }

    public void TurnWhite()
    {
        SpriteRenderer.color = Color.white;
        //spriteRendererImage.color = Color.white;
    }

}
