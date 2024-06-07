using System;
using System.Collections.Generic;
using UnityEngine;

public class GameConstant
{
    public static int[] stringToIntArray(string input)
    {
        input = input.Replace(",", "");
        List<int> _reward = new List<int>();
        string[] alls = input.Split(' ');
        foreach (var item in alls)
        {
            int _out = -1;
            if (int.TryParse(item, out _out))
            {
                _reward.Add(_out);
            }
        }

        return _reward.ToArray();
    }

    public static float[] stringToFloatArray(string input)
    {
        input = input.Replace(",", "");
        List<float> _reward = new List<float>();
        string[] alls = input.Split(' ');
        foreach (var item in alls)
        {
            float _out = -1;
            if (float.TryParse(item, out _out))
            {
                _reward.Add(_out);
            }
        }

        return _reward.ToArray();
    }


    public static string GetName(string input)
    {
        Debug.Log(input);

        if (input == null || input.Equals(""))
        {
            return "";
        }
        int index = input.IndexOf("_") + 1;
        string subString = input.Substring(index);
        return subString;
    }

    public class GameLayer
    {
        public const int PLANE = 3;
        public const int SELECTBOX = 6;
        public const int LAND = 7;
    }

    public class GameEvent
    {
        public const string POINT_CHANGE = "POINT_CHANGE";
        public const string COIN_CHANGE = "COIN_CHANGE";
        public const string THEME_CHANGE = "THEME_CHANGE";
        public const string HIGHSCORE_CHANGE = "HIGHSCORE_CHANGE";
        public const string CREATE_NEW_GAME = "CREATE_NEW_GAME";
        public const string START_GAME = "START_GAME";
        public const string END_GAME = "END_GAME";
    }

    public class GameData
    {
        public const string SCORE_DATABASE = "SCORE";
        public const string USER_ID = "uid";
        public const string HIGHSCORE = "HighScore";
        public const string ISLAND_STATIC_TILE = "Islands";
    }

    public static class GameType
    {
        public enum HexType
        {
            EMPTY,
            ROOT,
            PLACED
        }

        public enum IslandType
        {
            ROOT,
            STONE,
            GREEN,
            WATER,
            FIELD,
        }

        public static int Count
        {
            get
            {
                if (count == -1)
                {
                    count = Enum.GetNames(typeof(GameConstant.GameType.IslandType)).Length;

                }
                return count;
            }
        }
        private static int count = -1;

        public static int Root
        {
            get
            {
                if (root == -1)
                {
                    root = Convert.ToInt32(IslandType.ROOT);
                }
                return root;
            }
        }
        private static int root = -1;


        public static int Stone
        {
            get
            {
                if (stone == -1)
                {
                    stone = Convert.ToInt32(IslandType.STONE);
                }
                return stone;
            }
        }
        private static int stone = -1;

        public static int Grass
        {
            get
            {
                if (grass == -1)
                {
                    grass = Convert.ToInt32(IslandType.GREEN);
                }
                return grass;
            }
        }
        private static int grass = -1;

        public static int Water
        {
            get
            {
                if (water == -1)
                {
                    water = Convert.ToInt32(IslandType.WATER);
                }
                return water;
            }
        }
        private static int water = -1;

        public static int Field
        {
            get
            {
                if (field == -1)
                {
                    field = Convert.ToInt32(IslandType.FIELD);
                }
                return field;
            }
        }
        private static int field = -1;
    }

    public class Ads
    {
        public const string INTER_ADS_CONFIG = "InterstitialConfig";
        public const string INTER_ADS_DEFAULT = "{\"InterTime1\":45,  \"InterTime2\":35,  \"InterTime3\":35}";
        public const string REWARD_ADS_CONFIG = "RewardedAdsConfig";
        public const string REWARD_ADS_DEFAULT = "{\"DelayRebirth\":0 }";
    }

    [SerializeField]
    public class FirebaseConfigIntern
    {

    }
    [SerializeField]
    public class FirebaseConfigReward
    {

    }


    public class GameIAPID
    {
        public const string ID_NO_ADS = "com.beemob.";
    }


}
[Serializable]
public class TileIsland
{
    public int Q;
    public int R;
    public int type;
    public int skin;
    public int point;

    public TileIsland(float q, float r, int type, int skin, int point)
    {
        Q = Mathf.RoundToInt(q);
        R = Mathf.RoundToInt(r);
        this.type = type;
        this.skin = skin;
        this.point = point;
    }
}