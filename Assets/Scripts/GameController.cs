using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<IPlayerController> playerList;
    public List<ClueType> playerClues;
    [SerializeField] private MapController mapController;
    [SerializeField] private float turnInterval = 1.5f;
    public GridModel[,] mapGrid;

    private bool hasGameStarted = false;
    private int turnIndex = 0;
    private int roundIndex = 0;
    private bool hasGameEnded = false;
    private bool isAnsweringQuestion = false;
    private bool isPenaltyAnswer = false;
    private int forcedNoRoundCount = 2;
    private GridModel lastTurnGridModel;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void StartGame() {
        hasGameStarted = true;
        roundIndex = 0;
        turnIndex = 0;
        hasGameEnded = false;
        isAnsweringQuestion = false;
        isPenaltyAnswer = false;

        mapGrid = mapController.CreateMap();

    }

    private void GetPlayersAnswer() {
        // Ask player for the No if 
        if (roundIndex < forcedNoRoundCount) {
            MarkGridModel mark = playerList[turnIndex].ForcedNegativeMark();
            if (ValidatePlayerAction(PlayerAction.MARK, turnIndex, mark)) {
                ProcessAction(mark);
                StartNextTurn();
            } else {
                OnValidationFail("Placing Forced Negative Mark");
            }
        } else{
            PlayerAction action = playerList[turnIndex].PlayerTurn(mapGrid);
            if (action == PlayerAction.MARK) {
                MarkGridModel mark = playerList[turnIndex].PlayerMark();
                if (ValidatePlayerAction(action, turnIndex, mark)) {
                    ProcessAction(mark);
                    if (!isAnsweringQuestion) {
                        StartNextTurn();
                    }
                } else {
                    OnValidationFail("Placing Mark");
                }
            }
            if (action == PlayerAction.QUESTION_PLAYER) {
                QuestionPlayerModel questionPlayerModel = playerList[turnIndex].QuestionOtherPlayer();
                if (ValidatePlayerAction(action, turnIndex, null, questionPlayerModel)) {
                    ProcessAction(questionPlayerModel);
                } else {
                    OnValidationFail("Questioning Player");
                }
            }
            if (action == PlayerAction.INIT_SEARCH) {
                GridIndex gridIndex = playerList[turnIndex].InitSearch();
                if (ValidatePlayerAction(action, turnIndex, null, null, gridIndex)) {
                    ProcessAction(gridIndex);
                } else {
                    OnValidationFail("While Starting Search");
                }
            }
        }

    }

    private void StartNextTurn() {
        turnIndex++;
        if (turnIndex >= playerList.Count) {
            turnIndex = 0;
            roundIndex++;
        }
        Invoke("GetPlayersAnswer", turnInterval);
        // if(!CheckGameOver()) {
        // }
    }

    private bool CheckGameOver() {
        int correctCount = 0; 
        for (int i = 0; i < lastTurnGridModel.gamePieces.Count; i++) {
            if (lastTurnGridModel.gamePieces[i].markType == MarkType.POSITIVE_MARK) {
                correctCount++;
            }
        }
        if (correctCount == playerList.Count) {
            Debug.LogWarning("Victory");
            return true;
        }
        return false;
    }

    private void OnValidationFail(string action) {
        Debug.LogError("Stopping Game");
        Debug.LogWarning("Validation Failed for " + playerList[turnIndex] + " for " + action);
        Debug.LogWarning("Player index " + turnIndex);
    }

    private void ProcessAction(MarkGridModel mark) {
        // do visual stuff
        GamePiece piece = new GamePiece();
        piece.playerId = (PlayerId)turnIndex;
        piece.markType = mark.markType;
        mapGrid[mark.gridIndex.x, mark.gridIndex.y].gamePieces.Add(piece);
        lastTurnGridModel = mapGrid[mark.gridIndex.x, mark.gridIndex.y];
    }
    private void ProcessAction(QuestionPlayerModel questionPlayer) {
        // add question visual
        MarkType mark = CheckQuestion(questionPlayer);
        MarkGridModel markGrid = new MarkGridModel();
        markGrid.markType  = mark;
        markGrid.gridIndex = questionPlayer.gridIndex;
        ProcessAction(markGrid);
        if (mark == MarkType.POSITIVE_MARK) {
            StartNextTurn();
        } else {
            MarkGridModel markNegative = playerList[turnIndex].ForcedNegativeMark();
            if (ValidatePlayerAction(PlayerAction.MARK, turnIndex, markNegative)) {
                ProcessAction(markNegative);
                StartNextTurn();
            } else {
                OnValidationFail("Placing Forced Negative Mark");
            }
        }
    }
    private void ProcessAction(GridIndex gridIndex) {
        GamePiece piece = new GamePiece();
        piece.playerId = (PlayerId)turnIndex;
        piece.markType = MarkType.POSITIVE_MARK;
        mapGrid[gridIndex.x, gridIndex.y].gamePieces.Add(piece);

        QuestionPlayerModel questionPlayer = new QuestionPlayerModel();
        questionPlayer.gridIndex = gridIndex;
        int checkIndex = turnIndex;
        int correctCount = 1;
        for (int i = 0; i < playerList.Count - 1; i++) {
            checkIndex++;
            if (checkIndex >= playerList.Count) {
                checkIndex = 0;
            }
            questionPlayer.playerId = (PlayerId)checkIndex;

            MarkType mark = CheckQuestion(questionPlayer);
            MarkGridModel markGrid = new MarkGridModel();
            markGrid.markType  = mark;
            markGrid.gridIndex = questionPlayer.gridIndex;
            if (mark == MarkType.NEGATIVE_MARK) {
                break;
            } else {
                correctCount++;
            }
        }
        if (correctCount == playerList.Count) {
            Debug.LogWarning("Game Won!!!"); 
        } else {
            MarkGridModel markNegative = playerList[turnIndex].ForcedNegativeMark();
            if (ValidatePlayerAction(PlayerAction.MARK, turnIndex, markNegative)) {
                ProcessAction(markNegative);
                StartNextTurn();
            } else {
                OnValidationFail("Placing Forced Negative Mark");
            }
        }
    }

    private bool ValidatePlayerAction(PlayerAction action, int playerIndex, MarkGridModel mark =  null, QuestionPlayerModel questionPlayer = null, GridIndex gridIndex = null) {
        if (action == PlayerAction.MARK) {
            if (mark.markType == MarkType.POSITIVE_MARK && roundIndex < forcedNoRoundCount) {
                return false;
            }
            return ValidateMark(playerIndex, mark);
        }
        if (action == PlayerAction.QUESTION_PLAYER) {
            if (roundIndex < forcedNoRoundCount) {
                return false;
            }
        }
        if (action == PlayerAction.INIT_SEARCH) {
            if (roundIndex < forcedNoRoundCount) {
                return false;
            }
        }
        return false;
    }

    private bool ValidateMark(int playerIndex, MarkGridModel mark) {
        ClueType clue = playerClues[playerIndex];
        GridModel currGrid = mapGrid[mark.gridIndex.x, mark.gridIndex.y];

        if (clue.isTwoTypeOfTerrain) {
            if (mark.markType == MarkType.POSITIVE_MARK) {
                if (currGrid.gridType == clue.typeOne || currGrid.gridType == clue.typeTwo) {
                    return true;
                }
            }
            if (mark.markType == MarkType.NEGATIVE_MARK) {
                if (currGrid.gridType != clue.typeOne && currGrid.gridType != clue.typeTwo) {
                    return true;
                }
            }
        }

        if (clue.isWithinOneSpaceOfTerrain) {
            List<GridModel> gridWithinOneSpace = GetTileWithinRadius(mark.gridIndex, 1);
            int counter = 0;
            for (int i = 0; i < gridWithinOneSpace.Count; i++) {
                if (currGrid.gridType == clue.withinOneSpaceOfTerrain) {
                    counter++;
                }
            }
            if (mark.markType == MarkType.POSITIVE_MARK && counter > 0) {
                return true;
            }
            if (mark.markType == MarkType.NEGATIVE_MARK && counter == 0) {
                return true;
            }
        }

        if (clue.isWithinOneSpaceOfTerritory) {
            List<GridModel> gridWithinOneSpace = GetTileWithinRadius(mark.gridIndex, 1);
            int counter = 0;
            for (int i = 0; i < gridWithinOneSpace.Count; i++) {
                if (currGrid.gridTerritory == clue.withinOneSpaceOfTerritory) {
                    counter++;
                }
            }
            if (mark.markType == MarkType.POSITIVE_MARK && counter > 0) {
                return true;
            }
            if (mark.markType == MarkType.NEGATIVE_MARK && counter == 0) {
                return true;
            }
        }

        if (clue.isWithinTwoSpaceOfStructure) {
            List<GridModel> gridWithinTwoSpace = GetTileWithinRadius(mark.gridIndex, 2);
            int counter = 0;
            for (int i = 0; i < gridWithinTwoSpace.Count; i++) {
                if (currGrid.gridStructure == clue.withinTwoSpaceOfStructure) {
                    counter++;
                }
            }
            if (mark.markType == MarkType.POSITIVE_MARK && counter > 0) {
                return true;
            }
            if (mark.markType == MarkType.NEGATIVE_MARK && counter == 0) {
                return true;
            }
        }

        if (clue.isWithinTwoSpaceOfTerritory) {
            List<GridModel> gridWithinTwoSpace = GetTileWithinRadius(mark.gridIndex, 2);
            int counter = 0;
            for (int i = 0; i < gridWithinTwoSpace.Count; i++) {
                if (currGrid.gridTerritory == clue.withinTwoSpaceOfTerritory) {
                    counter++;
                }
            }
            if (mark.markType == MarkType.POSITIVE_MARK && counter > 0) {
                return true;
            }
            if (mark.markType == MarkType.NEGATIVE_MARK && counter == 0) {
                return true;
            }
        }

        if (clue.isWithinThreeSpaceOfStructure) {
            List<GridModel> gridWithinThreeSpace = GetTileWithinRadius(mark.gridIndex, 3);
            int counter = 0;
            for (int i = 0; i < gridWithinThreeSpace.Count; i++) {
                if (currGrid.structureColor == clue.withinThreeSpaceOfStructure) {
                    counter++;
                }
            }
            if (mark.markType == MarkType.POSITIVE_MARK && counter > 0) {
                return true;
            }
            if (mark.markType == MarkType.NEGATIVE_MARK && counter == 0) {
                return true;
            } 
        }
        return false;
    }

    private MarkType CheckQuestion(QuestionPlayerModel questionPlayer) {
        int playerIndex = (int)questionPlayer.playerId;
        ClueType clue = playerClues[playerIndex];
        GridModel currGrid = mapGrid[questionPlayer.gridIndex.x, questionPlayer.gridIndex.y];
        
        if (clue.isTwoTypeOfTerrain) {
            if (currGrid.gridType == clue.typeOne || currGrid.gridType == clue.typeTwo) {
                return MarkType.POSITIVE_MARK;
            }
        }
        
        if (clue.isWithinOneSpaceOfTerrain) {
            List<GridModel> gridWithinOneSpace = GetTileWithinRadius(currGrid.gridIndex, 1);
            int counter = 0;
            for (int i = 0; i < gridWithinOneSpace.Count; i++) {
                if (currGrid.gridType == clue.withinOneSpaceOfTerrain) {
                    counter++;
                }
            }
            if (counter > 0) {
                return MarkType.POSITIVE_MARK;
            }
        }

        if (clue.isWithinOneSpaceOfTerritory) {
            List<GridModel> gridWithinOneSpace = GetTileWithinRadius(currGrid.gridIndex, 1);
            int counter = 0;
            for (int i = 0; i < gridWithinOneSpace.Count; i++) {
                if (currGrid.gridTerritory == clue.withinOneSpaceOfTerritory) {
                    counter++;
                }
            }
            if (counter > 0) {
                return MarkType.POSITIVE_MARK;
            }
        }

        if (clue.isWithinTwoSpaceOfStructure) {
            List<GridModel> gridWithinTwoSpace = GetTileWithinRadius(currGrid.gridIndex, 2);
            int counter = 0;
            for (int i = 0; i < gridWithinTwoSpace.Count; i++) {
                if (currGrid.gridStructure == clue.withinTwoSpaceOfStructure) {
                    counter++;
                }
            }
            if (counter > 0) {
                return MarkType.POSITIVE_MARK;
            }
        }

        if (clue.isWithinTwoSpaceOfTerritory) {
            List<GridModel> gridWithinTwoSpace = GetTileWithinRadius(currGrid.gridIndex, 2);
            int counter = 0;
            for (int i = 0; i < gridWithinTwoSpace.Count; i++) {
                if (currGrid.gridTerritory == clue.withinTwoSpaceOfTerritory) {
                    counter++;
                }
            }
            if (counter > 0) {
                return MarkType.POSITIVE_MARK;
            }
        }

        if (clue.isWithinThreeSpaceOfStructure) {
            List<GridModel> gridWithinThreeSpace = GetTileWithinRadius(currGrid.gridIndex, 3);
            int counter = 0;
            for (int i = 0; i < gridWithinThreeSpace.Count; i++) {
                if (currGrid.structureColor == clue.withinThreeSpaceOfStructure) {
                    counter++;
                }
            }
            if (counter > 0) {
                return MarkType.POSITIVE_MARK;
            }
        }
        
        return MarkType.NEGATIVE_MARK;
    }

    public List<GridModel> GetTileWithinRadius(GridIndex gridIndex, int noOfSpace) {
        return mapController.GetTileWithin(gridIndex, noOfSpace);
    } 

}

public interface IPlayerController {
    // player decide what he wants to do in his turn
    PlayerAction PlayerTurn(GridModel[,] grid);

    // Player takes turn by negative mark or answers other players turn by placing positive or negative mark
    MarkGridModel PlayerMark();

    // Ask other player 
    QuestionPlayerModel QuestionOtherPlayer();

    // Start search by calling a spot on map
    GridIndex InitSearch();

    // Place negative mark for first 2 turn & on gettting negetive answer on question
    MarkGridModel ForcedNegativeMark();
}

public class MarkGridModel {
    public GridIndex gridIndex;
    public MarkType markType;
}

public class QuestionPlayerModel {
    public GridIndex gridIndex;
    public PlayerId playerId;
}


public enum PlayerId {
    PLAYER_ONE,
    PLAYER_TWO,
    PLAYER_THREE,
    PLAYER_FOUR,
    PLAYER_FIVE
}

public enum MarkType {
    POSITIVE_MARK,
    NEGATIVE_MARK
}

public enum PlayerAction {
    MARK,
    QUESTION_PLAYER,
    INIT_SEARCH
}
