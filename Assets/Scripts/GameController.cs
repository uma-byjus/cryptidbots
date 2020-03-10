using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<IPlayerController> playerList;
    public List<ClueType> playerClues;
    public MapController mapController;
    public GridModel[,] mapGrid;

    private bool hasGameStarted = false;
    private int turnIndex = 0;
    private int roundIndex = 0;
    private bool hasGameEnded = false;
    private int forcedNoRoundCount = 2;

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

        mapGrid = mapController.CreateMap();

    }

    private void GetPlayersAnswer() {
        // Ask player for the No if 
        if (roundIndex < forcedNoRoundCount) {
            PlayerAction action = playerList[turnIndex].PlayerTurn(mapGrid);
            ProcessAction(action);
            // players[0].GetAnswer();
        }
    

        // if ()
    }

    private void ProcessAction(PlayerAction action) {
        if (action == PlayerAction.MARK) {

        } 
        if (action == PlayerAction.QUESTION_PLAYER) {

        }
    }

    private void PlayerNegativeCheck(ClueType clue, Grid grid) {
        
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
