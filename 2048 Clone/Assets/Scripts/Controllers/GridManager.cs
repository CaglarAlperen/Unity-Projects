using UnityEngine;

public class GridManager : MBSingleton<GridManager>
{
    private int[,] grid = {
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 },
        { 0, 0, 0, 0 }
    };

    public int this[int y, int x]
    {
        get => grid[y, x];
        set => grid[y, x] = value;
    }

    private void Awake()
    {
        InputController.OnDragged += Move;
    }

    private void Move(Vector2 drag)
    {
        if (!GameManager.Instance.ControlOn)
            return;

        if (HasNoMove())
        {
            GameManager.Instance.GameLost();
        }

        Vector2 direction = drag.Direction();

        int[,] gridBeforeMove = new int[4,4];
        CopyGrid(gridBeforeMove, grid);

        Push(direction);
        Merge(direction);
        Push(direction);

        if (!IsEqual(gridBeforeMove, grid))
        {
            NumberSpawner.Instance.Spawn();
        }  
    }

    private void CopyGrid(int[,] to, int[,] from)
    {
        for (int row = 0; row < 4; row++) 
        {
            for (int col = 0; col < 4; col++)
            {
                to[row,col] = from[row,col];
            }
        }
    }

    private bool IsEqual(int[,] first, int[,] second)
    {
        for (int row = 0; row < 4; row++) 
        {
            for (int col = 0; col < 4; col++)
            {
                if (first[row,col] != second[row,col])
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void Push(Vector2 direction)
    {
        bool vertical = direction.y != 0;
        if (vertical)
        {
            PushVertical(direction);
        }
        else
        {
            PushHorizontal(direction);
        }
    }

    private void PushVertical(Vector2 direction)
    {
        if (direction.y > 0)
        {
            for (int col = 0; col < 4; col++)
            {
                for (int row = 3; row >= 0; row--)
                {
                    if (grid[row, col] != 0)
                    {
                        FindObjectAt(row, col).GetComponent<NumberCell>().Push(direction);
                    }
                }
            }
        }
        else
        {
            for (int col = 0; col < 4; col++)
            {
                for (int row = 0; row < 4; row++)
                {
                    if (grid[row, col] != 0)
                    {
                        FindObjectAt(row, col).GetComponent<NumberCell>().Push(direction);
                    }
                }
            }
        }
    }

    private void PushHorizontal(Vector2 direction)
    {
        if (direction.x > 0)
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 3; col >= 0; col--)
                {
                    if (grid[row, col] != 0)
                    {
                        FindObjectAt(row, col).GetComponent<NumberCell>().Push(direction);
                    }
                }
            }
        }
        else
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (grid[row, col] != 0)
                    {
                        FindObjectAt(row, col).GetComponent<NumberCell>().Push(direction);
                    }
                }
            }
        }
    }

    private void Merge(Vector2 direction)
    {
        bool vertical = direction.y != 0;
        if (vertical)
        {
            MergeVertical(direction);
        }
        else
        {
            MergeHorizontal(direction);
        }
    }

    private void MergeVertical(Vector2 direction)
    {
        if (direction.y > 0)
        {
            for (int col = 0; col < 4; col++)
            {
                int prevValue = 0;
                for (int row = 3; row >= 0; row--)
                {
                    if (grid[row, col] == 0)
                        break;
                    if (grid[row, col] == prevValue)
                    {
                        Merge(FindObjectAt(row,col), FindObjectAt(row+1,col));
                        prevValue = 0;
                    }
                    else
                    {
                        prevValue = grid[row, col];
                    }
                }
            }
        }
        else
        {
            for (int col = 0; col < 4; col++)
            {
                int prevValue = 0;
                for (int row = 0; row < 4; row++)
                {
                    if (grid[row, col] == 0)
                        break;
                    if (grid[row, col] == prevValue)
                    {
                        Merge(FindObjectAt(row, col), FindObjectAt(row - 1, col));
                        prevValue = 0;
                    }
                    else
                    {
                        prevValue = grid[row, col];
                    }
                }
            }
        }
    }

    private void MergeHorizontal(Vector2 direction)
    {
        if (direction.x > 0)
        {
            for (int row = 0; row < 4; row++)
            {
                int prevValue = 0;
                for (int col = 3; col >= 0; col--)
                {
                    if (grid[row, col] == 0)
                        break;
                    if (grid[row, col] == prevValue)
                    {
                        Merge(FindObjectAt(row, col), FindObjectAt(row, col + 1));
                        prevValue = 0;
                    }
                    else
                    {
                        prevValue = grid[row, col];
                    }
                }
            }
        }
        else 
        {
            for (int row = 0; row < 4; row++)
            {
                int prevValue = 0;
                for (int col = 0; col < 4; col++)
                {
                    if (grid[row, col] == 0)
                        break;
                    if (grid[row, col] == prevValue)
                    {
                        Merge(FindObjectAt(row, col), FindObjectAt(row, col - 1));
                        prevValue = 0;
                    }
                    else
                    {
                        prevValue = grid[row, col];
                    }
                }
            }
        }
    }

    private void Merge(GameObject moving, GameObject stationary)
    {
        //Grid updates
        grid[(int)stationary.GetComponent<NumberCell>().GridPos.y, (int)stationary.GetComponent<NumberCell>().GridPos.x] = moving.GetComponent<NumberCell>().Value * 2;
        grid[(int)moving.GetComponent<NumberCell>().GridPos.y, (int)moving.GetComponent<NumberCell>().GridPos.x] = 0;

        moving.GetComponent<NumberCell>().GridPos = stationary.GetComponent<NumberCell>().GridPos;

        moving.GetComponent<NumberCell>().MoveTo(stationary.GetComponent<NumberCell>().GridPos);
        stationary.SetActive(false);
        Destroy(stationary.gameObject);
        moving.GetComponent<NumberCell>().Upgrade();
        GameManager.Instance.AddScore(moving.GetComponent<NumberCell>().Value);
    }

    private bool HasNoMove()
    {
        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                if (grid[row,col] == 0)
                {
                    return false;
                }
                if (HasEqualNeighbour(row, col))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool HasEqualNeighbour(int row, int col)
    {
        if (row > 0)
        {
            if (grid[row, col] == grid[row - 1, col])
                return true;
        }
        if (row < 3)
        {
            if (grid[row, col] == grid[row + 1, col])
                return true;
        }
        if (col > 0)
        {
            if (grid[row, col] == grid[row, col - 1])
                return true;
        }
        if (col < 3)
        {
            if (grid[row, col] == grid[row, col + 1])
                return true;
        }
        return false;
    }

    private GameObject FindObjectAt(int y, int x)
    {
        NumberCell[] cells = FindObjectsOfType<NumberCell>();
        foreach (NumberCell cell in cells)
        {
            if (cell.GridPos.y == y && cell.GridPos.x == x)
            {
                return cell.gameObject;
            }
        }
        return null;
    }

    public void ClearGrid()
    {
        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                grid[row, col] = 0;
            }
        }
    }
}
