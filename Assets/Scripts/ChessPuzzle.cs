using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChessPuzzle : Puzzle
{

    [SerializeField] private Transform[] pieceOrder;

    [SerializeField] private Transform[] squareOrder;

    private Transform currentSquare, currentPiece;
    
    int moveCounter = 0;

    protected override void Start()
    {
        EventSystem.instance.puzzleTriggered += Trigger;
        currentPiece = pieceOrder[moveCounter];
        currentSquare = squareOrder[moveCounter];
        var key = currentPiece.gameObject.AddComponent(typeof(Key)) as Key;
        var trigger = currentSquare.gameObject.AddComponent(typeof(PuzzleTrigger)) as PuzzleTrigger;
        key.keyID = this.puzzleID;
        trigger.puzzleID = this.puzzleID;
        trigger.keyID = this.puzzleID;
    }

    protected override void Trigger(object sender, int puzzleID)
    {
        if(this.puzzleID == puzzleID) 
        {
            Destroy(currentPiece.GetComponent<Key>());
            Destroy(currentPiece.GetComponent<PuzzleTrigger>());
            moveCounter++;
            if(moveCounter == squareOrder.Length)
            {
                Destroy(currentPiece.GetComponent<Key>());
                Destroy(currentPiece.GetComponent<PuzzleTrigger>());
                AnimateObjects();
            }
            else
            {
                currentPiece = pieceOrder[moveCounter];
                currentSquare = squareOrder[moveCounter];
                Debug.Log(currentPiece.name + "    " + currentSquare.name);
                var key = currentPiece.gameObject.AddComponent(typeof(Key)) as Key;
                var trigger = currentSquare.gameObject.AddComponent(typeof(PuzzleTrigger)) as PuzzleTrigger;
                key.keyID = this.puzzleID;
                trigger.puzzleID = this.puzzleID;
                trigger.keyID = this.puzzleID;
            }
           
        }
        
    }

    
}
