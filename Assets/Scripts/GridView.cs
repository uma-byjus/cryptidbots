using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridView : MonoBehaviour
{
    public SpriteRenderer bearSprite;
    public SpriteRenderer cougarSprite;
    public SpriteRenderer standingStoneSprite;
    public SpriteRenderer abandonedShackSprite;
    
    [Space]
    public Color[] haxColor = new Color[5];
    public Color[] structureColor = new Color[4];
    public GridModel gridModel;
    public Vector2 gridIndex;

    private SpriteRenderer gridSprite;
    // Start is called before the first frame update
    void Awake() {
        gridSprite = GetComponent<SpriteRenderer>();
    }

    public void Init(GridModel model) {
        gridSprite.color = haxColor[(int)model.gridType];
        
        if (model.gridTerritory == TileModel.TileTerritory.BEAR_TERRITORY) {
            bearSprite.gameObject.SetActive(true);
        }
        if (model.gridTerritory == TileModel.TileTerritory.COUGAR_TERRITORY) {
            cougarSprite.gameObject.SetActive(true);
        }

        if (model.gridStructure == TileModel.TileStructureType.ABANDONED_SHACK) {
            abandonedShackSprite.color = structureColor[(int)model.structureColor];
            abandonedShackSprite.gameObject.SetActive(true);
        }

        if (model.gridStructure == TileModel.TileStructureType.STANDING_STONE) {
            standingStoneSprite.color = structureColor[(int)model.structureColor];
            standingStoneSprite.gameObject.SetActive(true);
        }
    }



}
