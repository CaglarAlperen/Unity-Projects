using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] GameObject player1, player2;
    GameObject activePlayer;
    GameObject activePiece;
    ArrayList moves = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        activePiece = null;
        ResetMoves();
        SetActivePlayer(player1);
    }
    private void Update()
    {

    }

    private void SetActivePlayer(GameObject player)
    {
        activePlayer = player;
        for (int i = 0; i < 3; i++)
        {
            activePlayer.transform.GetChild(i).gameObject.GetComponent<Piece>().SetPlayable(true);
        }
        
        if (activePlayer == player1)
        {
            for (int i = 0; i < 3; i++)
            {
                player2.transform.GetChild(i).gameObject.GetComponent<Piece>().SetPlayable(false);
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                player1.transform.GetChild(i).gameObject.GetComponent<Piece>().SetPlayable(false);
            }
        }
    }

    private void ResetMoves()
    {
        moves.Add(1);
        moves.Add(2);
        moves.Add(3);
    }

    private void OnMouseDown()
    {
        if (!activePiece) { return; }

        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 gridPos = SnapToGrid(worldPos);

        if (isLegalMove(gridPos))
        {
            Vector2 activePiecePos = new Vector2(activePiece.transform.position.x, activePiece.transform.position.y);
            int step = (int)Mathf.Abs(activePiecePos.x - gridPos.x) + (int)Mathf.Abs(activePiecePos.y - gridPos.y);
            moves.Remove(step);
            MoveActivePieceToPoint(gridPos);
            activePiece.GetComponent<Piece>().SetPlayable(false);
        }
        
        activePiece = null;
        DestroyGreenDots();

        if (moves.Count == 0)
        {
            if (activePlayer == player1)
            {
                SetActivePlayer(player2);
                ResetMoves();
            }
            else
            {
                SetActivePlayer(player1);
                ResetMoves();
            }
        }
    }

    private bool isLegalMove(Vector2 pos)
    {
        var greenPoints = FindObjectsOfType<GreenDot>();
        foreach (var point in greenPoints)
        {
            if (point.transform.position.x == pos.x && point.transform.position.y == pos.y)
                return true;
        }
        return false;
    }

    public void DestroyGreenDots()
    {
        var greenDots = FindObjectsOfType<GreenDot>();
        foreach(var dot in greenDots)
        {
            dot.DestroyDot();
        }
    }

    private void MoveActivePieceToPoint(Vector2 vector2)
    {
        activePiece.transform.position = vector2;
    }

    private Vector2 SnapToGrid(Vector2 mousePos)
    {
        Vector2 gridPos = new Vector2(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y));
        return gridPos;
    }

    public void SetActivePiece(GameObject piece)
    {
        activePiece = piece;
        DestroyGreenDots();
    }

    public ArrayList GetMoves()
    {
        return moves;
    }

    public GameObject GetActivePiece()
    {
        return activePiece;
    }
}
