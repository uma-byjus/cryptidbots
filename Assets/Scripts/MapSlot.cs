using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CBot {
    public class MapSlot : MonoBehaviour
    {
        public Image TerrainType;
        public Image Region;
        public Image Structure;

        public Slot slot;

        public void SetMapSlot(Slot _slot)
        {
            slot = _slot;
            TerrainType.color = GridManager.instance.GetTerrainData(slot.terrain).tColor;
            Region.color = GridManager.instance.GetRegionData(slot.region).rColor;
            Structure.color = GridManager.instance.GetStructureData(slot.structure).color;
            Structure.sprite = GridManager.instance.GetStructureData(slot.structure).sShape;
        }
    }
}
