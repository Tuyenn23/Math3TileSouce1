using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TileData", menuName = "Tool/Create/TileData", order = 1)]
public class TileData : ScriptableObject
{
    [TableList]
    public List<Tile> listTile = new List<Tile>();

    public GameObject GetTile(GameConstant.GameType.IslandType Type, int skin)
    {
        for (int i = 0; i < listTile.Count; i++)
        {
            if (listTile[i].Type == Type && listTile[i].skin == skin)
            {
                return listTile[i].prefab;
            }
        }
        return null;
    }
}

[Serializable]
public class Tile
{
    public GameConstant.GameType.IslandType Type;
    public int skin;
    public GameObject prefab;
}
