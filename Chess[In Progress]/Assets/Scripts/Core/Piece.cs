using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public Piece(GameObject obj)
    {
        position = obj.transform.position;
    }

    protected Vector3 position;
    public abstract void CalcValidMoves();
    public abstract void MoveTo(Vector2 pos);
    private void OnMouseDown()
    {
        Board.Instance.SetActivePiece(this);
        CalcValidMoves();
    }
}
