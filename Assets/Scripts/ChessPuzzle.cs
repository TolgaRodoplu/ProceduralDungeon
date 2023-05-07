using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChessPuzzle : MonoBehaviour
{
    [SerializeField] private int puzzleID;

    [SerializeField] private Transform[] pieceOrder;

    [SerializeField] private Transform[] squareOrder;

    private Transform currentSquare, currentPiece;
    
    int moveCounter = 0;

    void Start()
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

    private void Trigger(object sender, int puzzleID)
    {
        if(this.puzzleID == puzzleID) 
        {
            Debug.Log("triggered");
            Destroy(currentPiece.GetComponent<Key>());
            Destroy(currentPiece.GetComponent<PuzzleTrigger>());
            moveCounter++;
            currentPiece = pieceOrder[moveCounter];
            currentSquare = squareOrder[moveCounter];
            var key = currentPiece.gameObject.AddComponent(typeof(Key)) as Key;
            var trigger = currentSquare.gameObject.AddComponent(typeof(PuzzleTrigger)) as PuzzleTrigger;
            key.keyID = this.puzzleID;
            trigger.puzzleID = this.puzzleID;
            trigger.keyID = this.puzzleID;
        }
        
    }

    
}
