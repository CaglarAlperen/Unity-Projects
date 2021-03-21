using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{

    [SerializeField] GameObject greenDot;
    bool playable = false;

    private void OnMouseDown()
    {
        if (!playable) { return; }

        if (isThisActivePiece())
        {
            FindObjectOfType<GameController>().SetActivePiece(null);
            FindObjectOfType<GameController>().DestroyGreenDots();
        }
        else
        {
            FindObjectOfType<GameController>().SetActivePiece(gameObject);
            ShowPossibleMoves();
        }      
    }

    private bool isThisActivePiece()
    {
        GameObject activePiece = FindObjectOfType<GameController>().GetActivePiece();
        if (!activePiece) return false;
        return activePiece.GetInstanceID() == gameObject.GetInstanceID();
    }

    private void ShowPossibleMoves()
    {
        ArrayList moves = FindAllPossibleMoves();
        foreach ((int x, int y) point in moves)
        {
            Instantiate(greenDot, new Vector3(point.x, point.y, 0), Quaternion.identity);
        }
    }

    private ArrayList FindAllPossibleMoves()
    {
        (int x, int y) = ((int)transform.position.x, (int)transform.position.y);
        var zone = SafeZone();
        ArrayList moves = new ArrayList();
        ArrayList steps = FindObjectOfType<GameController>().GetMoves();
        foreach (int step in steps)
        {
            if (zone.Contains((x + step, y)))
                moves.Add((x + step, y));
            if (zone.Contains((x - step, y)))
                moves.Add((x - step, y));
            if (zone.Contains((x, y + step)))
                moves.Add((x, y + step));
            if (zone.Contains((x, y - step)))
                moves.Add((x, y - step));
        }
        return moves;
    }

    private ArrayList SafeZone()
    {
        var pieces = FindObjectsOfType<Piece>();
        ArrayList piecePositions = new ArrayList();
        foreach (Piece piece in pieces)
        {
            piecePositions.Add(piece.GetPosition());
        }

        ArrayList zone = new ArrayList();
        (int x, int y) pos = ((int)transform.position.x, (int)transform.position.y);
        int i = 1;
        while (i <= 3 && !piecePositions.Contains((pos.x + i, pos.y)))
        {
            zone.Add((pos.x + i, pos.y));
            i++;
        }
        i = 1;
        while (i <= 3 && !piecePositions.Contains((pos.x - i, pos.y)))
        {
            zone.Add((pos.x - i, pos.y));
            i++;
        }
        i = 1;
        while (i <= 3 && !piecePositions.Contains((pos.x, pos.y + i)))
        {
            zone.Add((pos.x, pos.y + i));
            i++;
        }
        i = 1;
        while (i <= 3 && !piecePositions.Contains((pos.x, pos.y - i)))
        {
            zone.Add((pos.x, pos.y - i));
            i++;
        }

        return zone;
    }

    public (int, int) GetPosition()
    {
        return ((int)transform.position.x, (int)transform.position.y);
    }

    public void SetPlayable(bool playable)
    {
        this.playable = playable;
    }
}
