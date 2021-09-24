using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviourSingleton<Board>
{
    private char[,] board = new char[,]
    {
        { ' ',' ',' ',' ',' ',' ',' ',' ' },
        { ' ',' ',' ',' ',' ',' ',' ',' ' },
        { ' ',' ',' ',' ',' ',' ',' ',' ' },
        { ' ',' ',' ',' ',' ',' ',' ',' ' },
        { ' ',' ',' ',' ',' ',' ',' ',' ' },
        { ' ',' ',' ',' ',' ',' ',' ',' ' },
        { ' ',' ',' ',' ',' ',' ',' ',' ' },
        { ' ',' ',' ',' ',' ',' ',' ',' ' }
    };

    private Piece _activePiece;

    public char this[int y, int x]
    {
        get => board[y - 1, x - 1];
        private set => board[y - 1, x - 1] = value;
    }

    public void SetActivePiece(Piece piece)
    {
        _activePiece = piece;
    }
}
