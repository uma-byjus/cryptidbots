using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public int terrain;
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
        public static GridManager instance;
        [SerializeField] GameData gameData;
        public int Col { get; } = 12;
        public int Row { get; } = 9;
        [SerializeField] Transform MapPanel;
        [SerializeField] GameObject MapSlotPrefab;
        public List<List<MapSlot>> MapSlots = new List<List<MapSlot>>();
        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            foreach (PlayerData item in gameData.players)
            {
                item.playerRules.SetRules();
            }
            GenerateMap();
        }

        public TerrainData GetTerrainData (int index)
        {
            return gameData.terrains[index];
        }

        public RegionData GetRegionData (int index)
        {
            return (index != -1) ? gameData.regions[index] : new RegionData();
           
        }

        public StructureData GetStructureData(int index)
        {
            return (index != -1) ? gameData.structures[index]:new StructureData();
        }

        void GenerateMap()
        {
            List<string> sData = gameData.MapData.text.Split("\n"[0]).ToList();
            int k = 0;
            for (int i = 0; i < Row; i++)
            {
                List<MapSlot> slots = new List<MapSlot>();
                for (int j = 0; j < Col; j++)
                {
                    List<string> slotValues = sData[k].Split(","[0]).ToList();
                    Slot s = new Slot();
                    s.terrain = int.Parse(slotValues[0]);
                    s.structure = slotValues.Count > 1 ? int.Parse(slotValues[1]) : -1;
                    s.region = slotValues.Count > 2 ? int.Parse(slotValues[2]) : -1;
                    MapSlot ms = Instantiate(MapSlotPrefab, MapPanel).GetComponent<MapSlot>();
                    ms.gameObject.name = i.ToString() + " X " + j.ToString();
                    ms.SetMapSlot(s);
                    slots.Add(ms);
                    k++;
                }
                MapSlots.Add(slots);
            }
        }
    }
}
