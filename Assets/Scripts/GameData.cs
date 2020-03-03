using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CBot
{

    [System.Serializable]
    public struct TerrainData
    {
        public string tName;
        public TerrainType tType;
        public Color tColor;
    }
    [System.Serializable]
    public struct RegionData
    {
        public string rName;
        public Region rType;
        public Color rColor;
    }
    [System.Serializable]
    public struct StructureData
    {
        public string sName;
        public StructureType structure;
        public SColor sColor;
        public Color color;
        public Sprite sShape;
    }
    [System.Serializable]
    public class PlayerData
    {
        public string playerName;
        public PlayerRule playerRules;
        public Color playerColor;
    }
    
    [CreateAssetMenu(fileName ="Game Data",menuName ="Create / Game Data",order = 0)]
    public class GameData : ScriptableObject
    {
        public TextAsset MapData;
        public List<TerrainData> terrains = new List<TerrainData>();
        public List<StructureData> structures = new List<StructureData>();
        public List<RegionData> regions = new List<RegionData>();
        public List<Sprite> decisionSprites = new List<Sprite>();
        public List<PlayerData> players = new List<PlayerData>();
        
    }
}
