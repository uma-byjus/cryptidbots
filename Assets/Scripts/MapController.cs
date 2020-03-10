using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<MapSegmentScriptableObject> mapSegments = new List<MapSegmentScriptableObject>();
    public MapDataScriptableObject mapData;
    public SpriteRenderer haxSprite;
    public GameObject parentGO;
   
    [Space]
    private GridModel[,] grid = new GridModel[12,9];

    private float size = 3.6f;
    private float width;
    private float height;
    private GridView[,] gridViews = new GridView[12,9];
    

    // Start is called before the first frame update
    void Start()
    {
        width = 2 * size;
        height = Mathf.Sqrt(3f) * size;
        CreateMap();
        DrawMap();
    }

    public GridModel[,] CreateMap() {
        int topLeftIndex = mapData.segmentIds[0] - 1;
        List<MapSegmentScriptableObject> segmentCopy = new List<MapSegmentScriptableObject>(mapSegments);
        MapSegmentScriptableObject topLeftSegment = segmentCopy[topLeftIndex];
        if (mapData.isSegmentInverted[0]) {
            topLeftSegment = InvertSegment(segmentCopy[topLeftIndex]);
        }

        int topRightIndex = mapData.segmentIds[1] - 1;
        MapSegmentScriptableObject topRightSegment = segmentCopy[topRightIndex];
        if (mapData.isSegmentInverted[1]) {
            topRightSegment = InvertSegment(segmentCopy[topRightIndex]);
        }


        for (int i = 0; i < 6; i++) {
            grid[i, 0] = topLeftSegment.rowOne[i];
            grid[i, 1] = topLeftSegment.rowTwo[i];
            grid[i, 2] = topLeftSegment.rowThree[i];
        }
        for (int i = 6; i < 12; i++) {
            grid[i, 0] = topRightSegment.rowOne[i - 6];
            grid[i, 1] = topRightSegment.rowTwo[i - 6];
            grid[i, 2] = topRightSegment.rowThree[i - 6];
        }


        int midLeftIndex = mapData.segmentIds[2] - 1;
        MapSegmentScriptableObject midLeftSegment = segmentCopy[midLeftIndex];
        if (mapData.isSegmentInverted[2]) {
            midLeftSegment = InvertSegment(segmentCopy[midLeftIndex]);
        }

        int midRightIndex = mapData.segmentIds[3] - 1;
        MapSegmentScriptableObject midRightSegment = segmentCopy[midRightIndex];
        if (mapData.isSegmentInverted[3]) {
            midRightSegment = InvertSegment(segmentCopy[midRightIndex]);
        }

        for (int i = 0; i < 6; i++) {
            grid[i, 3] = midLeftSegment.rowOne[i];
            grid[i, 4] = midLeftSegment.rowTwo[i];
            grid[i, 5] = midLeftSegment.rowThree[i];
        }
        for (int i = 6; i < 12; i++) {
            grid[i, 3] = midRightSegment.rowOne[i - 6];
            grid[i, 4] = midRightSegment.rowTwo[i - 6];
            grid[i, 5] = midRightSegment.rowThree[i - 6];
        }


        int botLeftIndex = mapData.segmentIds[4] - 1;
        MapSegmentScriptableObject botLeftSegment = segmentCopy[botLeftIndex];
        if (mapData.isSegmentInverted[4]) {
            botLeftSegment = InvertSegment(segmentCopy[botLeftIndex]);
        }

        int botRightIndex = mapData.segmentIds[5] - 1;
        MapSegmentScriptableObject botRightSegment = segmentCopy[botRightIndex];
        if (mapData.isSegmentInverted[5]) {
            botRightSegment = InvertSegment(segmentCopy[botRightIndex]);
        }

        for (int i = 0; i < 6; i++) {
            grid[i, 6] = botLeftSegment.rowOne[i];
            grid[i, 7] = botLeftSegment.rowTwo[i];
            grid[i, 8] = botLeftSegment.rowThree[i];
        }
        for (int i = 6; i < 12; i++) {
            grid[i, 6] = botRightSegment.rowOne[i - 6];
            grid[i, 7] = botRightSegment.rowTwo[i - 6];
            grid[i, 8] = botRightSegment.rowThree[i - 6];
        }

        for (int i = 0;  i < mapData.structureInfo.Count; i++) {
            int x = (int)mapData.structureInfo[i].structurePos.x - 1;
            int y = (int)mapData.structureInfo[i].structurePos.y - 1;
            grid[x, y].gridStructure = mapData.structureInfo[i].structureType;
            grid[x, y].structureColor = mapData.structureInfo[i].structureColor;
        }

        return grid;
    }

    public List<GridModel> GetTileWithin(GridIndex gridIndex, int noOfSpace) {
        noOfSpace = Mathf.Clamp(noOfSpace, 1, 3);
        List<GridModel> tilesWithin = new List<GridModel>();
        float range = (size * 2f * noOfSpace * 0.75f) + size;
        Vector3 pos = gridViews[(int)gridIndex.x, (int)gridIndex.y].transform.position;

        foreach(GridView gView in gridViews) {
            float dist = Vector3.Distance(pos, gView.transform.position);
            if (dist < range) {
                tilesWithin.Add(gView.gridModel);
            }
        }
        // Debug.Log("Number of Tile in within " + fromCenter + " is " + tilesWithinOneSpace.Count);
        return tilesWithin;
    }

    private void DrawMap () {
        Debug.Log("Size " + size);
        Debug.Log("Height "+ height);
        for(int i = 0; i < 12; i++) {
            for (int j = 0; j < 9; j++) {
                SpriteRenderer gridGO = Instantiate(haxSprite);
                gridGO.transform.parent = parentGO.transform;
                Vector3 pos = Vector3.zero;
                pos.x = i * 0.75f * width;
                pos.y = -j * height;
                if (i % 2 == 1) {
                    pos.y -= height * 0.5f;
                }
                gridGO.transform.localPosition = pos;

                GridView gridView = gridGO.GetComponent<GridView>();
                gridView.Init(grid[i, j]);
                gridView.gridIndex.x = i;
                gridView.gridIndex.y = j;
                gridViews[i, j] = gridView;

                // gridGO.color = haxColor[(int)grid[i, j].gridType];
            }
        }
    }

    public List<GridModel> GetTilesWithinTwoSpace(int x, int y) {
        List<GridModel> tilesWithinTwoSpace = new List<GridModel>();
        return tilesWithinTwoSpace;
    }

    private MapSegmentScriptableObject InvertSegment(MapSegmentScriptableObject mapSeg) {
        MapSegmentScriptableObject invertedSegment = new MapSegmentScriptableObject();
        int length = mapSeg.rowOne.Length;
        Debug.Log("Array lenght "+ length);
        for (int i = 0; i < length; i++) {
            invertedSegment.rowOne[i] = mapSeg.rowThree[length - 1 - i];
            invertedSegment.rowTwo[i] = mapSeg.rowTwo[length - 1 - i];
            invertedSegment.rowThree[i] = mapSeg.rowOne[length - 1 - i];
        }
        return invertedSegment;
    }

    
}

public class GridSectionModel {
    public GridModel[] rowOne = new GridModel[9];
    public GridModel[] rowTwo = new GridModel[0];
    public GridModel[] rowThree = new GridModel[0];
    
}

[System.Serializable]
public class GridModel {
    public GridIndex gridIndex;
    public List<GamePiece> gamePieces;
    public TileModel.TileType gridType;
    public TileModel.TileTerritory gridTerritory;
    public TileModel.TileStructureType gridStructure;
    public TileModel.TileStructureColor structureColor;
}

public class GamePiece {
    public PlayerId playerId;
    public MarkType markType;
}

public class GridIndex {
    public int x;
    public int y;
}
