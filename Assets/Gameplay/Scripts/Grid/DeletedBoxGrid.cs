using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletedBoxGrid : MonoBehaviour
{
    public List<Cube> listCube = new List<Cube>();
    public Cube lastMove;
    public Vector3 lastPos;
    [SerializeField] private Transform ReleaseBox;
    private int count;
    public int add;

    [SerializeField] private AddGrid addGrid;
    private void Start()
    {
        Messenger.AddListener(GlobalStrings.EventCall.START_GAME, StartGame);
        Messenger.AddListener(GlobalStrings.EventCall.SURE_END_GAME, KillThemAll);
    }

    public void AddCube(Cube cube)
    {
        MusicManager.Instance.PlayOneShot(MusicDB.Instance.SFX_hit);
        lastMove = cube;
        cube.from = cube.transform.position;
        cube.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        cube.move = 0;
        cube.killed = false;
        cube.SpriteRenderer.sortingOrder = 2;
        lastPos = cube.transform.position;
        bool found = false;
        for (int i = 0; i < listCube.Count; i++)
        {
            if (listCube[i].TypeCube == cube.TypeCube && listCube[i].status)
            {
                // check listCube i+ 1
                found = true;

                if (i < listCube.Count - 1 && listCube[i + 1].TypeCube == cube.TypeCube && listCube[i + 1].status)
                {
                    // move to i + 2
                    lastMove = null;
                    listCube.Insert(i + 2, cube);
                    count -= 2;
                    listCube[i].status = false;
                    listCube[i + 1].status = false;
                    cube.status = false;
                    StartCoroutine(WaitToKill(cube));
                    break;
                }
                else
                {
                    listCube.Insert(i + 1, cube);
                    count++;
                    break;
                }
            }
        }
        if (!found)
        {
            listCube.Add(cube);
            count++;
        }
        if (count >= (7 + add))
        {
            Debug.Log("YOU LOSE");
            Messenger.Broadcast(GlobalStrings.EventCall.END_GAME);
            MusicManager.Instance.PlayOneShot(MusicDB.Instance.SFX_Lose);
        }

        if ((GridManager.Instance.rerolled || GridManager.Instance.emited) && !GridManager.Instance.winable)
        {
            if (GridManager.Instance.listCube.Count + count <= 50)
            {
                int x = Random.Range(0, 3);
                if (x == 0)
                {
                    GridManager.Instance.winable = true;
                    GridManager.Instance.CreateAListSuffer();
                    Debug.Log("WINABLE");
                }
            }
        }
    }

    private void StartGame()
    {
        add = 0;
        count = 0;
        listCube.Clear();
        addGrid.gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < listCube.Count; i++)
        {
            if (!listCube[i].killed)
            {
                listCube[i].transform.position = Vector3.Lerp(listCube[i].from, transform.position + new Vector3(1.8f, 0, 0) * i, listCube[i].move += (Time.deltaTime * 2));
            }
        }
    }

    private IEnumerator WaitToKill(Cube cube)
    {
        while (true)
        {
            yield return null;
            List<Cube> _tempCube = new List<Cube>();
            if (Mathf.Abs(cube.transform.position.y - transform.position.y) < 0.01f)
            {
                for (int z = 0; z < listCube.Count; z++)
                {
                    if (listCube[z].TypeCube == cube.TypeCube && listCube[z].status == false)
                    {
                        _tempCube.Add(listCube[z]);
                    }
                }
                foreach (var item in _tempCube)
                {
                    item.transform.DOMove(_tempCube[0].transform.position, 0.5f).OnComplete(() =>
                    {
                        MusicManager.Instance.PlayOneShot(MusicDB.Instance.SFX_Delete);
                        item.killed = true;
                        item.transform.DORotate(new Vector3(0, 360, 0), .5f).SetLoops(3, LoopType.Incremental).SetRelative(true);
                        item.transform.DOScale(new Vector3(0, 0, 0), 0.3f).SetDelay(0.2f).OnComplete(() =>
                        {
                            item.gameObject.SetActive(false);
                            listCube.Remove(item);
                        });
                    });
                }

                yield break;
            }
        }
    }

    private void KillThemAll()
    {
        for (int i = 0; i < listCube.Count; i++)
        {
            listCube[i].gameObject.SetActive(false);
        }
        listCube.Clear();
    }


    public void Undo()
    {
        if (lastMove != null)
        {
            for (int z = listCube.Count - 1; z >= 0; z--)
            {
                if (listCube[z] == lastMove)
                {
                    listCube.RemoveAt(z);
                    count--;
                    break;
                }
            }
            lastMove.InitCube();
            StartCoroutine(UndoMove(lastMove.transform, lastPos));
            lastMove = null;
            GridManager.Instance.undoed = true;
        }
    }


    private IEnumerator UndoMove(Transform trans, Vector3 lastpos)
    {
        float t = 0;
        while (t < .5f)
        {
            t += Time.deltaTime / 2f;
            trans.position = Vector3.Lerp(trans.position, lastpos, 2 * t);
            yield return null;
        }
        trans.position = lastpos;
    }

    public void Emit()
    {
        int x = 0;
        Vector3 pos;
        int depth = 0;
        for (int i = listCube.Count - 1; i >= 0; i--)
        {
            if (listCube[i].status)
            {
                if (x == 0)
                {
                    pos = GridManager.Instance.grid.GetNearestPointOnGrid(ReleaseBox.transform.position);
                    depth = GridManager.Instance.CheckGridReverse(pos) + 1;

                    listCube[i].transform.position = pos + new Vector3(0, 0, depth * -0.05f);
                    listCube[i].InitCubeAgain(pos, depth);
                    listCube.RemoveAt(i);
                    count--;
                    x++;
                }
                else if (x == 1)
                {
                    pos = GridManager.Instance.grid.GetNearestPointOnGrid(ReleaseBox.transform.position + new Vector3(-2, 0, 0));
                    depth = GridManager.Instance.CheckGridReverse(pos) + 1;
                    listCube[i].transform.position = pos + new Vector3(0, 0, depth * -0.05f);
                    listCube[i].InitCubeAgain(pos, depth);
                    listCube.RemoveAt(i);
                    count--;
                    x++;
                }
                else if (x == 2)
                {
                    pos = GridManager.Instance.grid.GetNearestPointOnGrid(ReleaseBox.transform.position + new Vector3(2, 0, 0));
                    depth = GridManager.Instance.CheckGridReverse(pos) + 1;
                    listCube[i].transform.position = pos + new Vector3(0, 0, depth * -0.05f);
                    listCube[i].InitCubeAgain(pos, depth);
                    listCube.RemoveAt(i);
                    count--;
                    x++;
                }
            }
        }
    }

    private IEnumerator EmitMove(Transform trans, Vector3 lastpos)
    {
        float t = 0;
        while (t < .5f)
        {
            t += Time.deltaTime / 2f;
            trans.position = Vector3.Lerp(trans.position, lastpos, 2 * t);
            yield return null;
        }
        trans.position = lastpos;
    }

    public void RemoveAddBtn()
    {
        addGrid.gameObject.SetActive(false);
    }
}
