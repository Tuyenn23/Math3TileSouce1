using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class GroupGrid : MonoBehaviour
{
    private Vector3 thisVec;
    public GlobalStrings.GroupType groupType;
    private List<Cube> cubeUp = new List<Cube>();
    private List<Cube> cubeDown = new List<Cube>();
    private bool horizon = true;

    private void OnEnable()
    {
        Messenger.AddListener<Vector3>(GlobalStrings.EventCall.DELETE_BLOCK, CheckDeleteGrid);
        Messenger.AddListener(GlobalStrings.EventCall.CREATE_END_GAME, CreateEndGame);
        Messenger.AddListener(GlobalStrings.EventCall.SURE_END_GAME, EndGame);
    }

    [Button]
    public void CreateGroup(GlobalStrings.GroupType type)
    {
        transform.position = GridManager.Instance.grid.GetNearestPointOnGrid(transform.position);
        thisVec = transform.position;
        groupType = type;
        if (groupType == GlobalStrings.GroupType.GRPLine10BotToTop)
        {
            for (int i = 0; i < 10; i++)
            {
                Vector3 grisPos = GridManager.Instance.grid.GetNearestPointOnGrid(thisVec);
                int depth = GridManager.Instance.CheckGrid(grisPos);

                GameObject _go = ObjectPooler.Instance.GetPooledObject("Cube");
                _go.transform.position = grisPos + new Vector3(0, 0.2f, 0) * i;
                _go.transform.parent = GridManager.Instance.transform;
                Cube cube = _go.GetComponent<Cube>();
                cube.InitCube(grisPos, depth - 1);
                _go.SetActive(true);
            }
        }
        else if (groupType == GlobalStrings.GroupType.GRPLine10LeftToRight)
        {
            for (int i = 0; i < 10; i++)
            {
                Vector3 grisPos = GridManager.Instance.grid.GetNearestPointOnGrid(thisVec);
                int depth = GridManager.Instance.CheckGrid(grisPos);

                GameObject _go = ObjectPooler.Instance.GetPooledObject("Cube");
                _go.transform.position = grisPos + new Vector3(0.25f, 0, 0) * i;
                _go.transform.parent = GridManager.Instance.transform;
                Cube cube = _go.GetComponent<Cube>();
                cube.InitCube(grisPos, depth - 1);
                _go.SetActive(true);
            }
        }
        else if (groupType == GlobalStrings.GroupType.GRPLine10RightToLeft)
        {
            for (int i = 0; i < 10; i++)
            {
                Vector3 grisPos = GridManager.Instance.grid.GetNearestPointOnGrid(thisVec);
                int depth = GridManager.Instance.CheckGrid(grisPos);

                GameObject _go = ObjectPooler.Instance.GetPooledObject("Cube");
                _go.transform.position = grisPos + new Vector3(-0.25f, 0, 0) * i;
                _go.transform.parent = GridManager.Instance.transform;
                Cube cube = _go.GetComponent<Cube>();
                cube.InitCube(grisPos, depth - 1);
                _go.SetActive(true);
            }
        }
        else if (groupType == GlobalStrings.GroupType.GRP3_INFINITY)
        {
            horizon = true;
            Create_three_tile(cubeUp);
            Create_three_tile(cubeDown);
        }
        else if (groupType == GlobalStrings.GroupType.GRP3_INFINITY_VER)
        {
            horizon = false;
            Create_three_tile(cubeUp);
            Create_three_tile(cubeDown);
        }
        else if (groupType == GlobalStrings.GroupType.TUTORIAL)
        {
            for (int i = 0; i < 2; i++)
            {
                Vector3 grisPos = GridManager.Instance.grid.GetNearestPointOnGrid(thisVec);
                int depth = GridManager.Instance.CheckGrid(grisPos);

                GameObject _go = ObjectPooler.Instance.GetPooledObject("Cube");
                _go.transform.position = grisPos + new Vector3(0, 0.2f, 0) * i;
                _go.transform.parent = GridManager.Instance.transform;
                Cube cube = _go.GetComponent<Cube>();
                cube.InitCube(grisPos, depth - 1);
                _go.SetActive(true);
            }
        }
    }

    public void Create_three_tile(List<Cube> list, bool endgame = false)
    {
        list.Clear();

        Vector3 grisPos = GridManager.Instance.grid.GetNearestPointOnGrid(new Vector3(thisVec.x - (horizon ? 1 : 0), thisVec.y - (horizon ? 0 : 1), 0));
        int depth = GridManager.Instance.CheckGrid(grisPos);

        GameObject _go = ObjectPooler.Instance.GetPooledObject("Cube");
        _go.transform.position = grisPos;
        _go.transform.parent = GridManager.Instance.transform;
        Cube cube = _go.GetComponent<Cube>();
        cube.InitCube(grisPos, depth - 1);
        _go.SetActive(true);
        list.Add(cube);

        grisPos = GridManager.Instance.grid.GetNearestPointOnGrid(new Vector3(thisVec.x + (horizon ? 1 : 0), thisVec.y - (horizon ? 0 : 1), 0));
        depth = GridManager.Instance.CheckGrid(grisPos);

        _go = ObjectPooler.Instance.GetPooledObject("Cube");
        _go.transform.position = grisPos;
        _go.transform.parent = GridManager.Instance.transform;
        cube = _go.GetComponent<Cube>();
        cube.InitCube(grisPos, depth - 1);
        _go.SetActive(true);
        list.Add(cube);

        grisPos = GridManager.Instance.grid.GetNearestPointOnGrid(new Vector3(thisVec.x, thisVec.y, 0));
        depth = GridManager.Instance.CheckGrid(grisPos);

        _go = ObjectPooler.Instance.GetPooledObject("Cube");
        _go.transform.position = grisPos;
        _go.transform.parent = GridManager.Instance.transform;
        cube = _go.GetComponent<Cube>();
        cube.InitCube(grisPos, depth - 1);
        _go.SetActive(true);
        list.Add(cube);
    }


    public void Create_endgame_tile()
    {
        while (GridManager.Instance.listNumber.Count > 0)
        {
            Vector3 grisPos = GridManager.Instance.grid.GetNearestPointOnGrid(new Vector3(thisVec.x - (horizon ? 1 : 0), thisVec.y - (horizon ? 0 : 1), 0));
            int depth = GridManager.Instance.CheckGrid(grisPos);

            GameObject _go = ObjectPooler.Instance.GetPooledObject("Cube");
            _go.transform.position = grisPos;
            _go.transform.parent = GridManager.Instance.transform;
            Cube cube = _go.GetComponent<Cube>();
            cube.InitCube(grisPos, depth - 1, true);
            _go.SetActive(true);

            if (GridManager.Instance.listNumber.Count > 0)
            {
                grisPos = GridManager.Instance.grid.GetNearestPointOnGrid(new Vector3(thisVec.x + (horizon ? 1 : 0), thisVec.y - (horizon ? 0 : 1), 0));
                depth = GridManager.Instance.CheckGrid(grisPos);

                _go = ObjectPooler.Instance.GetPooledObject("Cube");
                _go.transform.position = grisPos;
                _go.transform.parent = GridManager.Instance.transform;
                cube = _go.GetComponent<Cube>();
                cube.InitCube(grisPos, depth - 1, true);
                _go.SetActive(true);
            }

            if (GridManager.Instance.listNumber.Count > 0)
            {
                grisPos = GridManager.Instance.grid.GetNearestPointOnGrid(new Vector3(thisVec.x, thisVec.y, 0));
                depth = GridManager.Instance.CheckGrid(grisPos);

                _go = ObjectPooler.Instance.GetPooledObject("Cube");
                _go.transform.position = grisPos;
                _go.transform.parent = GridManager.Instance.transform;
                cube = _go.GetComponent<Cube>();
                cube.InitCube(grisPos, depth - 1, true);
                _go.SetActive(true);
            }
        }
        EndGame();
    }

    private void CheckDeleteGrid(Vector3 vec)
    {
        if (groupType == GlobalStrings.GroupType.GRP3_INFINITY || groupType == GlobalStrings.GroupType.GRP3_INFINITY_VER)
        {

            for (int i = cubeUp.Count - 1; i >= 0; i--)
            {
                if (!GridManager.Instance.listCube.Contains(cubeUp[i]))
                {
                    cubeUp.RemoveAt(i);
                }
            }
            for (int i = cubeDown.Count - 1; i >= 0; i--)
            {
                if (!GridManager.Instance.listCube.Contains(cubeDown[i]))
                {
                    cubeDown.RemoveAt(i);
                }
            }

            if (cubeDown.Count == 0)
            {
                Create_three_tile(cubeDown);
            }

            if (cubeUp.Count == 0)
            {
                Create_three_tile(cubeUp);
            }
        }
    }

    public void CreateEndGame()
    {
        if (groupType == GlobalStrings.GroupType.GRP3_INFINITY || groupType == GlobalStrings.GroupType.GRP3_INFINITY_VER)
        {
            Create_endgame_tile();
        }
    }

    private void EndGame()
    {
        Messenger.RemoveListener<Vector3>(GlobalStrings.EventCall.DELETE_BLOCK, CheckDeleteGrid);
        Messenger.RemoveListener(GlobalStrings.EventCall.CREATE_END_GAME, CreateEndGame);
        Messenger.RemoveListener(GlobalStrings.EventCall.SURE_END_GAME, EndGame);
        Destroy(gameObject);
    }
}
