using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapDataset", menuName = "ScriptableObjects/MapScriptableObject", order = 1)]

public class MapDataScriptableObject : ScriptableObject
{
    public List<int> segmentIds = new List<int>();
    public List<bool> isSegmentInverted = new List<bool>();
    public List<StructureInfo> structureInfo = new List<StructureInfo>();

    [Header("Hints")]
    public MapHints mapHints;
}

[System.Serializable]
public class StructureInfo {
    public TileModel.TileStructureType structureType;
    public TileModel.TileStructureColor structureColor;
    public Vector2 structurePos;
}

[System.Serializable]
public class MapHints {
    public List<int> alpha = new List<int>();
    public List<int> beta = new List<int>();
    public List<int> gamma = new List<int>();
    public List<int> delta = new List<int>();
    public List<int> elipson = new List<int>();
    public List<int> openToAll = new List<int>();
}
