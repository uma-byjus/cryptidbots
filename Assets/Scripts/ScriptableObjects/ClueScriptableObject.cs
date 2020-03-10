using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Clue", menuName = "ScriptableObjects/ClueScriptableObject", order = 2)]
public class ClueScriptableObject : ScriptableObject {
    public ClueType clueType;
}

[System.Serializable]
public class ClueType {
    [Header("On One of two Types of terrain")]
    public bool isTwoTypeOfTerrain = false;
    public TileModel.TileType typeOne;
    public TileModel.TileType typeTwo;

    [Space]
    [Header("Within one space of a terrain")]
    public bool isWithinOneSpaceOfTerrain;
    public TileModel.TileType withinOneSpaceOfTerrain;

    [Space]
    [Header("Within one space of a animal territory")]
    public bool isWithinOneSpaceOfTerritory;
    public TileModel.TileTerritory withinOneSpaceOfTerritory;

    [Space]
    [Header("Within two space of territory")]
    public bool isWithinTwoSpaceOfTerritory;
    public TileModel.TileTerritory withinTwoSpaceOfTerritory;

    [Space]
    [Header("Within two space of structure")]
    public bool isWithinTwoSpaceOfStructure;
    public TileModel.TileStructureType withinTwoSpaceOfStructure;

    [Space]
    [Header("Within three space of structure")]
    public bool isWithinThreeSpaceOfStructure;
    public TileModel.TileStructureColor withinThreeSpaceOfStructure;

}



