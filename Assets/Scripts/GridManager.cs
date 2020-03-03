using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CBot
{

    public enum TerrainType
    {
        water,
        mountain,
        forest,
        swamp,
        desert
    }

    public enum SColor
    {
        Blue,
        White,
        Green,
        Black
    }

    public enum StructureType
    {
        stone,
        shack
    }

    public enum Region {
        None,
        Cougar,
        Bear
    }

    [System.Serializable]
    public struct Slot
    {
        public int _TerrainType;
        public int structure;
        public int region;
    }
    [System.Serializable]
    public class Rule
    {
        public string RuleWording;
        public int rSpaces = -1;
        public List<int> rTerrainTypes = new List<int>();
        public List<int> rRegions = new List<int>();
        public List<int> rStructures = new List<int>();
        public bool not;
    }

    public class GridManager : MonoBehaviour
    {
        [SerializeField] GameData gameData;
        public int Col { get; } = 12;
        public int Row { get; } = 9;
        public List<Slot> MapSlots = new List<Slot>();
       
        private void Start()
        {
            foreach (PlayerData item in gameData.players)
            {
                item.playerRules.SetRules();
            }
        }
    }
}
