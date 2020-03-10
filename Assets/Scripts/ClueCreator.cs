using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ClueCreator : MonoBehaviour
{
    public ClueScriptableObject scriptableObject;
    private int numberOfTwoTypeOfTerrainClues = 10;
    private int numberOFWithinOneSpaceOfTerrainClues = 5;
    private int numberOfWithinOneSpaceOfTerritoryClues = 3;
    private int numberOfWithinTwoSpaceOfTerritoryClues = 3;
    private int numberOfWithinTwoSpaceOfStructureClues = 3;
    private int numberOfWithinThreeSpaceOfStructureClues = 4;
    string savePath = "Assets/Clues/";
    string fileExt = ".asset";

    // Start is called before the first frame update
    void Start() {

    }

    [ContextMenu("Create Two Types of Clues")]
    void CreateTwoTypeOfTerrainClues() {
        int typeOneIndex = 4;
        int typeTwoIndex = typeOneIndex - 1;
        for (int i = 0; i < numberOfTwoTypeOfTerrainClues ; i++) {
            ClueScriptableObject clueObj = Instantiate(scriptableObject);
            if (typeTwoIndex >= 0) {
                clueObj.clueType.isTwoTypeOfTerrain = true;
                clueObj.clueType.typeOne = (TileModel.TileType)typeOneIndex;
                clueObj.clueType.typeTwo = (TileModel.TileType)typeTwoIndex;
                
                string clueName = "On the " + clueObj.clueType.typeOne.ToString() + " or " + clueObj.clueType.typeTwo;
                AssetDatabase.CreateAsset(clueObj, savePath + clueName + ".asset");
                
                typeTwoIndex--;
            } else {
                typeOneIndex--;
                typeTwoIndex = typeOneIndex - 1;

                clueObj.clueType.isTwoTypeOfTerrain = true;
                clueObj.clueType.typeOne = (TileModel.TileType)typeOneIndex;
                clueObj.clueType.typeTwo = (TileModel.TileType)typeTwoIndex;
                
                string clueName = "On the " + clueObj.clueType.typeOne.ToString() + " or " + clueObj.clueType.typeTwo;
                AssetDatabase.CreateAsset(clueObj, savePath + clueName  + fileExt);
                
                typeTwoIndex--;
            }
        }
        AssetDatabase.SaveAssets();
    }

    [ContextMenu("Create Within One Space of Terrain Clues")]
    void CreateWithinOneSpaceOfTerrainClues() {
        for (int i = 0; i < numberOFWithinOneSpaceOfTerrainClues; i++) {
            ClueScriptableObject clueObj = Instantiate(scriptableObject);
            clueObj.clueType.isWithinOneSpaceOfTerrain = true;
            clueObj.clueType.withinOneSpaceOfTerrain = (TileModel.TileType)i;

            string clueName = "Within one space of " + clueObj.clueType.withinOneSpaceOfTerrain.ToString();
            AssetDatabase.CreateAsset(clueObj, savePath + clueName + fileExt);
        }
        AssetDatabase.SaveAssets();
    }

    [ContextMenu("Create Within One Space of Territory Clues")]
    void CreateWithinOneSpaceOfTerritoryClues() {
        for (int i = 0; i < numberOfWithinOneSpaceOfTerritoryClues; i++) {
            if (TileModel.TileTerritory.NONE == (TileModel.TileTerritory)i) {
                continue;
            }
            ClueScriptableObject clueObj = Instantiate(scriptableObject);
            clueObj.clueType.isWithinOneSpaceOfTerritory = true;
            clueObj.clueType.withinOneSpaceOfTerritory = (TileModel.TileTerritory)i;

            string clueName = "Within one space of " + clueObj.clueType.withinOneSpaceOfTerritory.ToString();
            AssetDatabase.CreateAsset(clueObj, savePath + clueName + fileExt);
        }
        AssetDatabase.SaveAssets();
    }
    
    [ContextMenu("Create Within Two Space of Territory Clues")]
    void CreateWithinTwoSpaceOfTerritoryClues() {
        for (int i = 0 ; i < numberOfWithinTwoSpaceOfTerritoryClues; i++) {
            Debug.Log("i = "+ i);
            if (TileModel.TileTerritory.NONE == (TileModel.TileTerritory)i) {
                continue;
            }
            ClueScriptableObject clueObj = Instantiate(scriptableObject);
            clueObj.clueType.isWithinTwoSpaceOfTerritory = true;
            clueObj.clueType.withinTwoSpaceOfTerritory = (TileModel.TileTerritory)i;

            string clueName = "Within two space of " + clueObj.clueType.withinTwoSpaceOfTerritory.ToString();
            Debug.Log("Clue Name " + clueName);
            AssetDatabase.CreateAsset(clueObj, savePath + clueName + fileExt);
        }
        AssetDatabase.SaveAssets();
    }

    [ContextMenu("Create Within Two Space of Structure Type")]
    void CreateWithinTwoSpaceOfStructureClues() {
        for (int i = 0; i < numberOfWithinTwoSpaceOfStructureClues; i++) {
            if (TileModel.TileStructureType.NONE == (TileModel.TileStructureType)i) {
                continue;
            }
            ClueScriptableObject clueObj = Instantiate(scriptableObject);
            clueObj.clueType.isWithinTwoSpaceOfStructure = true;
            clueObj.clueType.withinTwoSpaceOfStructure = (TileModel.TileStructureType)i;

            string clueName = "Within two space of " + clueObj.clueType.withinTwoSpaceOfStructure.ToString();
            AssetDatabase.CreateAsset(clueObj, savePath + clueName + fileExt);
        }
        AssetDatabase.SaveAssets();
    }

    [ContextMenu("Create Within Three Space of Structure")]
    void CreateWithinThreeSpaceOfStructureClues() {
        for (int i = 0; i < numberOfWithinThreeSpaceOfStructureClues; i++) {
            ClueScriptableObject clueObj = Instantiate(scriptableObject);
            clueObj.clueType.isWithinThreeSpaceOfStructure = true;
            clueObj.clueType.withinThreeSpaceOfStructure = (TileModel.TileStructureColor)i;

            string clueName = "Within three space of " + clueObj.clueType.withinThreeSpaceOfStructure.ToString();
            AssetDatabase.CreateAsset(clueObj, savePath + clueName + fileExt);
        }
    }

}
