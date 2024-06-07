using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class Dashboard
{

    public Dashboard()
    {
        //InitFirebaseDatabase();
    }


    #region FirebaseDatabase
    //private void InitFirebaseDatabase()
    //{
    //    mDatabase = FirebaseDatabase.DefaultInstance.RootReference;
    //}

    //[Button]
    //public void GetDasboard()
    //{
    //    RDatabaseReference test = new RDatabaseReference(GameConstant.GameData.SCORE_DATABASE, mDatabase);
    //    //WriteNewScore("1122", 50);
    //    test.GetData((DataSnapshot data, bool status) => GetData(data, status));
    //}


    //public void GetData(DataSnapshot data, bool status)
    //{
    //    if (!status)
    //    {
    //        Debug.Log("CANT GET DATA");
    //        return;
    //    }

    //    if (data == null)
    //    {
    //        Debug.Log("DATA EMPTY");
    //        return;
    //    }

    //    string raw = data.GetRawJsonValue();

    //    //Dictionary<string, System.Object> entryValues = loadData.ToDictionary();
    //    GameController.Instance.leaderBoardEntries.Clear();
    //    foreach (var rules in data.Children) // rules
    //    {
    //        LeaderBoardEntry entry = new LeaderBoardEntry();
    //        foreach (var levels in rules.Children)         //levels
    //        {
    //            if (levels.Key.Equals(GameConstant.GameData.HIGHSCORE))
    //            {
    //                int point = 0;
    //                int.TryParse(levels.Value.ToString(), out point);
    //                entry.score = point;
    //            }
    //            else if (levels.Key.Equals(GameConstant.GameData.USER_ID))
    //            {
    //                entry.uid = levels.Value.ToString();
    //            }
    //        }  // levels

    //        GameController.Instance.leaderBoardEntries.Add(entry);
    //    } //rules
    //}


    #endregion
}

public class LeaderBoardEntry
{
    public string uid;
    public int score = 0;

    public LeaderBoardEntry()
    {
    }

    public LeaderBoardEntry(string uid, int score)
    {
        this.uid = uid;
        this.score = score;
    }

    public Dictionary<string, System.Object> ToDictionary()
    {
        Dictionary<string, System.Object> result = new Dictionary<string, System.Object>();
        result["uid"] = uid;
        result["score"] = score;

        return result;
    }
}