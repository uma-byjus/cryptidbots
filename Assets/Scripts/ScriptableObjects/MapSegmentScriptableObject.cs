using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map", menuName = "ScriptableObjects/MapSegmentScriptableObject", order = 1)]
public class MapSegmentScriptableObject : ScriptableObject
{
    public int segmentId = 0;
    public GridModel[] rowOne = new GridModel[6];
    public GridModel[] rowTwo = new GridModel[6];
    public GridModel[] rowThree = new GridModel[6];
}
