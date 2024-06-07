using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : LocalSingleton<GridManager>
{
    public List<Vector3> listGrid = new List<Vector3>();
    public List<Vector3> listGridPlace = new List<Vector3>();
    public List<Cube> listCube = new List<Cube>();
    [SerializeField] private DataLevel data;
    [SerializeField] private DeletedBoxGrid deletedBoxGrid;
    public Grid grid;
    [SerializeField] private Transform left, right;

    public bool Playable = false;
    private bool Reroll = false;

    public DataLevel Data => data;
    public int CurrentLevel;
    public DeletedBoxGrid DeletedBoxGrid => deletedBoxGrid;
    [SerializeField] private Transform RollBox;

    [SerializeField] private GameObject prefab;

    private int layer = 1 << 3;
    private int layerCube = 1 << 6;
    public int nowImg;

    #region forCounting
    public List<int> listNumber;
    #endregion

    #region Play
    public bool Revival;
    public bool emited;
    public bool undoed;
    public bool rerolled;
    public bool winable;
    #endregion

    private void Start()
    {
        StartCoroutine(FixScrean());
        MusicManager.Instance.PlayMusic(MusicDB.Instance.Music_BG);
        GameSave.SessionRewardCount = 0;
    }



    [Button]
    public void StartGame()
    {
        GameTracking.LogEvent("Start_game");
        DateTime date = DateTime.Now;
        int dayOfYear = date.DayOfYear;
        listGrid.Clear();
        listGridPlace.Clear();
        listCube.Clear();

        if (dayOfYear != GameSave.Date)
        {
            if (GameSave.PlayCount != 0)
            {
                GameTracking.LogEvent("GamePlay_" + GameSave.PlayCount);
            }
            GameSave.PlayCount = 1;
            GameSave.Date = dayOfYear;
        }
        else
        {
            GameSave.PlayCount++;
        }
        GameSave.TotalPlayCount++;
        CurrentLevel = 0;
        Revival = true;
        listNumber = new List<int>() { 0, 0, 0, 1, 1, 1, 2, 2, 2, 0, 0, 0, 1, 1, 1, 2, 2, 2 };
        if (GameSave.TotalPlayCount < 6)
        {
            nowImg = 0;
        }
        else if (GameSave.TotalPlayCount < 11)
        {
            nowImg = 1;
        }
        else
        {
            nowImg = UnityEngine.Random.Range(0, 4);
        }
        NextGame();
    }


    [Button]
    public void NextGame()
    {
        GameTracking.Log_Level_PlayCount(CurrentLevel == 0 ? 0 : 1);
        Level lev = data.GetLevel(CurrentLevel);

        for (int i = 0; i < lev.listGridPlace.Count; i++)
        {
            PlaceBox(lev.listGridPlace[i].x, lev.listGridPlace[i].y, lev.listGridPlace[i].z);
        }

        for (int i = 0; i < lev.listGrp.Count; i++)
        {
            PlaceGroup(lev.listGrp[i].place.x, lev.listGrp[i].place.y, lev.listGrp[i].groupType);
        }

        Messenger.Broadcast(GlobalStrings.EventCall.START_GAME);
        if (CurrentLevel != 0)
        {
            Messenger.Broadcast(GlobalStrings.EventCall.START_GAME_2);
        }
        Playable = true;
        emited = false;
        rerolled = false;
        undoed = false;
        winable = false;
    }


    private void Update()
    {
        if (!Playable)
        {
            return;
        }

        if (listCube.Count == 0 && deletedBoxGrid.listCube.Count == 0)
        {
            // end game
            Playable = false;
            if (CurrentLevel == 0)
            {
                DateTime date = DateTime.Now;
                CurrentLevel = date.DayOfYear % 7 + 1;
                MainUi.Instance.WinGame1();
            }
            else
            {
                // win game
                GameSave.WinCount++;
                MainUi.Instance.WinGame();
                GameTracking.LogEvent("Play_win");
            }
        }
        if (!Reroll)
        {
            //if (Input.GetMouseButtonDown(1))
            //{
            //    Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //    RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, layer);
            //    if (hit.collider)
            //    {
            //        PlaceBox(hit.point);
            //    }
            //}

            if (Input.GetMouseButtonDown(0))
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity, layerCube);
                if (hit.collider)
                {
                    Cube cube = hit.collider.GetComponent<Cube>();
                    if (cube != null)
                    {
                        DeleteBox(cube.ThisVec);
                    }
                    else
                    {
                        AddGrid add = hit.collider.GetComponent<AddGrid>();
                        if (add != null)
                        {
                            MainUi.Instance.mainPlay.Add1();
                        }
                    }
                }
            }
        }
    }

    private void PlaceBox(Vector2 pos)
    {
        Vector3 grisPos = grid.GetNearestPointOnGrid(pos);
        int depth = CheckGrid(grisPos);

        GameObject _go = ObjectPooler.Instance.GetPooledObject("Cube");
        _go.transform.position = grisPos;
        _go.transform.parent = transform;
        Cube cube = _go.GetComponent<Cube>();
        cube.InitCube(grisPos, depth - 1);
        _go.SetActive(true);
    }


    private void DeleteBox(Vector2 pos)
    {
        Vector3 grisPos = grid.GetNearestDownPointOnGrid(pos);
        DeleteTile(grisPos);
    }



    public void PlaceBox(float x, float y, float depth)
    {
        Vector3 grisPos = grid.GetNearestPointOnGrid(x, y, 0);
        GameObject _go = ObjectPooler.Instance.GetPooledObject("Cube");
        _go.transform.position = grisPos;
        _go.transform.parent = transform;
        Cube cube = _go.GetComponent<Cube>();
        cube.InitCube(grisPos, Mathf.RoundToInt(depth - 1));
        _go.SetActive(true);
    }

    public void PlaceGroup(float x, float y, GlobalStrings.GroupType type)
    {
        Vector3 grisPos = grid.GetNearestPointOnGrid(x, y, 0);
        GameObject _go = ObjectPooler.Instance.GetPooledObject("Group");
        _go.SetActive(true);
        _go.transform.position = grisPos;
        _go.transform.parent = transform;
        GroupGrid grp = _go.GetComponent<GroupGrid>();
        grp.CreateGroup(type);
    }

    public int GetNumber()
    {
        int rand = UnityEngine.Random.Range(0, listNumber.Count);

        int num = listNumber[rand];
        //Debug.Log(rand + " " + listNumber.Count + " " + num);

        listNumber.RemoveAt(rand);
        return num;
    }

    public int CheckGrid(Vector2 vec)
    {
        float depth = 0;
        for (int i = 0; i < listGrid.Count; i++)
        {
            if (listGrid[i].x == vec.x && listGrid[i].y == vec.y)
            {
                if (depth > listGrid[i].z)
                {
                    depth = listGrid[i].z;
                }
            }

            if (listGrid[i].x == vec.x - 1 && listGrid[i].y == vec.y)
            {
                if (depth > listGrid[i].z)
                {
                    depth = listGrid[i].z;
                }
            }

            if (listGrid[i].x == vec.x && listGrid[i].y == vec.y - 1)
            {
                if (depth > listGrid[i].z)
                {
                    depth = listGrid[i].z;
                }
            }

            if (listGrid[i].x == vec.x - 1 && listGrid[i].y == vec.y - 1)
            {
                if (depth > listGrid[i].z)
                {
                    depth = listGrid[i].z;
                }
            }
        }

        return Mathf.RoundToInt(depth);
    }

    public int CheckGridReverse(Vector2 vec)
    {
        float depth = -9999;
        for (int i = 0; i < listGrid.Count; i++)
        {
            if (listGrid[i].x == vec.x && listGrid[i].y == vec.y)
            {
                if (depth < listGrid[i].z)
                {
                    depth = listGrid[i].z;
                }
            }

            if (listGrid[i].x == vec.x - 1 && listGrid[i].y == vec.y)
            {
                if (depth < listGrid[i].z)
                {
                    depth = listGrid[i].z;
                }
            }

            if (listGrid[i].x == vec.x && listGrid[i].y == vec.y - 1)
            {
                if (depth < listGrid[i].z)
                {
                    depth = listGrid[i].z;
                }
            }

            if (listGrid[i].x == vec.x - 1 && listGrid[i].y == vec.y - 1)
            {
                if (depth < listGrid[i].z)
                {
                    depth = listGrid[i].z;
                }
            }
        }



        return Mathf.RoundToInt(depth);
    }

    public void DeleteTile(Vector2 vec)
    {
        float depth = int.MinValue;
        for (int i = 0; i < listGrid.Count; i++)
        {
            if (listGrid[i].x == vec.x && listGrid[i].y == vec.y)
            {
                if (listGrid[i].z > depth)
                {
                    depth = listGrid[i].z;
                }
            }
        }

        Messenger.Broadcast(GlobalStrings.EventCall.DELETE_BLOCK, new Vector3(vec.x, vec.y, depth));
    }

    public IEnumerator RetryGame()
    {
        Messenger.Broadcast(GlobalStrings.EventCall.SURE_END_GAME);
        yield return null;
        yield return new WaitForSeconds(0.1f);
        StartGame();
    }


    [Button]
    public void CreateAListSuffer()
    {
        int x;
        listNumber.Clear();
        for (int i = 0; i < 15; i++)
        {
            x = 0;
            for (int z = 0; z < listCube.Count; z++)
            {
                if (listCube[z].TypeCube == i)
                {
                    x++;
                }
            }

            for (int z = 0; z < deletedBoxGrid.listCube.Count; z++)
            {
                if (deletedBoxGrid.listCube[z].TypeCube == i)
                {
                    x++;
                }
            }

            if (x % 3 == 1)
            {
                listNumber.Add(i);
                listNumber.Add(i);
            }
            else
            if (x % 3 == 2)
            {
                listNumber.Add(i);
            }
        }

        // Shuffle the elements of the list randomly.
        System.Random rnd = new System.Random();
        for (int i = listNumber.Count - 1; i > 0; i--)
        {
            int j = rnd.Next(i + 1);
            int temp = listNumber[i];
            listNumber[i] = listNumber[j];
            listNumber[j] = temp;
        }

        Messenger.Broadcast(GlobalStrings.EventCall.CREATE_END_GAME);
    }

    #region Reroll

    public void RerollAllBox()
    {
        if (!Reroll)
        {
            listNumber.Clear();
            foreach (var item in listCube)
            {
                listNumber.Add(item.TypeCube);
            }
            Reroll = true;
            rerolled = true;
            StartCoroutine(ReRollAll());
        }
    }

    private IEnumerator ReRollAll()
    {
        int x = 1;
        int y = 1;
        List<Vector3> originalPos = new List<Vector3>();
        List<Vector3> EndPos = new List<Vector3>();
        for (int i = 0; i < listCube.Count; i++)
        {
            originalPos.Add(listCube[i].transform.position);
            x *= y;
            y *= -1;
            EndPos.Add(RollBox.position + new Vector3(x, y, 0));
        }
        float t = 0;
        while (t < .6f)
        {
            t += Time.deltaTime / 2f;
            for (int i = 0; i < listCube.Count; i++)
            {
                listCube[i].transform.position = Vector3.Lerp(originalPos[i], EndPos[i], 2 * t);
            }
            yield return null;
        }
        for (int i = 0; i < listCube.Count; i++)
        {
            listCube[i].transform.parent = RollBox;
        }

        RollBox.DOShakeRotation(1.5f, new Vector3(90, 90, 90)).SetEase(Ease.OutQuad);
        yield return new WaitForSeconds(.6f);
        Messenger.Broadcast(GlobalStrings.EventCall.REROLL);
        yield return new WaitForSeconds(1f);
        t = 0;
        while (t < .5f)
        {
            t += Time.deltaTime / 2f;
            for (int i = 0; i < listCube.Count; i++)
            {
                listCube[i].transform.position = Vector3.Lerp(listCube[i].transform.position, originalPos[i], 2 * t);
            }
            yield return null;
        }

        for (int i = 0; i < listCube.Count; i++)
        {
            listCube[i].transform.position = originalPos[i];
            listCube[i].transform.parent = transform;
        }
        Reroll = false;
    }

    #endregion

    public IEnumerator FixScrean()
    {
        float speed = 0.5f;
        while (Camera.main.WorldToViewportPoint(left.position).x < 0)
        {
            //cam.orthographicSize = cam.orthographicSize + 0.1f;
            Camera.main.orthographicSize = Camera.main.orthographicSize + speed;
            speed += 0.001f;
            yield return null;
        }

        while (Camera.main.WorldToViewportPoint(right.position).x > 1)
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize + speed;
            speed += 0.005f;
            yield return null;
        }
    }

    private void OnApplicationQuit()
    {
        GameTracking.LogEvent("RewardView" + GameSave.SessionRewardCount);
    }
}
