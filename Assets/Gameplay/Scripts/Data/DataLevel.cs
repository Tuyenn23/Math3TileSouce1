using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Create/Data", order = 1)]
public class DataLevel : SerializedScriptableObject
{
    public Font Vi;
    public Font En;

    [OdinSerialize]
    public List<List<Sprite>> listDataImage = new List<List<Sprite>>();
    public List<Level> listLevel;
    public Level GetLevel(int i)
    {
        for (int x = 0; x < listLevel.Count; x++)
        {
            if (listLevel[x].level == i)
            {
                return listLevel[x];
            }
        }
        return null;
    }

    public List<Sprite> GetListImage()
    {
        return listDataImage[GridManager.Instance.nowImg];
    }
}

[Serializable]
public class Level
{
    public int level;
    public List<Vector3> listGridPlace = new List<Vector3>();
    public List<Group> listGrp = new List<Group>();

#if UNITY_EDITOR
    [Button]
    private void Increase()
    {
        for (int i = 0; i < listGridPlace.Count; i++)
        {
            listGridPlace[i] = new Vector3(listGridPlace[i].x, listGridPlace[i].y, listGridPlace[i].z + 1);
        }
    }

    [Button]
    private void Decrease()
    {
        for (int i = 0; i < listGridPlace.Count; i++)
        {
            listGridPlace[i] = new Vector3(listGridPlace[i].x, listGridPlace[i].y - 1, listGridPlace[i].z);
        }
    }
#endif
}
[Serializable]
public class Group
{
    public GlobalStrings.GroupType groupType;
    public Vector2 place;
}