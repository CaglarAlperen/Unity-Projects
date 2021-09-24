using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberCell : MonoBehaviour
{
    private int _value;
    private int _colorIndex;
    private TextMeshProUGUI _valueText;
    private Image _image;
    public Vector3 GridPos;

    public int Value => _value;

    private void Awake()
    {
        _valueText = GetComponentInChildren<TextMeshProUGUI>();
        _image = GetComponentInChildren<Image>();
        UpdateDisplay();
    }

    public void Initialize(int value, int colorIndex)
    {
        _value = value;
        _colorIndex = colorIndex;
        GridPos = transform.position;
        UpdateDisplay();
    }

    public void Upgrade()
    {
        _value *= 2;
        _colorIndex++;
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        if (_value == 8)
        {
            _valueText.color = Color.white;
        }
        _valueText.text = _value.ToString();
        _image.color = Colors.Instance[_colorIndex];
    }

    public void Push(Vector2 direction)
    {
        Vector3 target = GridPos;
        Vector3 pos = GridPos;
        while (NextCell(pos, direction) != -1)
        {
            pos = pos.AddVector2(direction);
            if (GridManager.Instance[(int)pos.y, (int)pos.x] == 0)
            {
                target = target.AddVector2(direction);
            }
        }
        GridManager.Instance[(int)GridPos.y, (int)GridPos.x] = 0;
        GridManager.Instance[(int)target.y, (int)target.x] = Value;
        GridPos = target;
        MoveTo(target);
    }

    private int NextCell(Vector3 from, Vector2 direction)
    {
        Vector2 target = new Vector2(from.x + direction.x, from.y + direction.y);
        if (!target.IsInGrid())
        {
            return -1;
        }
        return GridManager.Instance[(int)target.y, (int)target.x];
    }

    public void MoveTo(Vector3 target) 
    {
        StopAllCoroutines();
        StartCoroutine(MoveToCoroutine(target));
    }

    IEnumerator MoveToCoroutine(Vector3 target)
    {
        if (target == transform.position)
        {
            yield break;
        }

        float stepDuration = GameSettings.MoveDuration / GameSettings.MoveSmoothFactor;
        float step = GameSettings.MoveSmoothFactor;
        Vector3 distance = target - transform.position;

        while (Vector3.Distance(transform.position, target) > GameSettings.ReachDistance)
        {
            transform.position += distance / step;
            yield return new WaitForSeconds(stepDuration);
        }

        transform.SnapToGrid();
    }
}
