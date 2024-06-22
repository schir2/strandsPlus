
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    [System.Serializable]
    public class State
    {
        public Color fillColor;
        public Color outlineColor;
        public bool isInteractive;
    }

    [Header("Tiles")]
    public State emptyState;
    public State selectedState;
    public State correctState;
    public State spangramState;

    public State currentState { get; private set; }
    public string letter { get; private set; }

    private Image fill;
    private Outline outline;
    public TextMeshProUGUI text;
    private CanvasGroup canvasGroup;

    internal int rowIndex;
    internal int colIndex;

    public static event Action<Tile, PointerEventData> OnTilePointerDownEvent;
    public static event Action<Tile, PointerEventData> OnTilePointerEnterEvent;
    public static event Action<Tile> OnTilePointerUpEvent;

    private void Awake()
    {
        fill = GetComponent<Image>();
        outline = GetComponent<Outline>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public static Tile CreateTile(GameObject tilePrefab, Transform parent, int row, int col, string letter)
    {
        if (tilePrefab == null)
        {
            Debug.LogError("Tile prefab is not set.");
            return null;
        }

        GameObject tileGameObject = Instantiate(tilePrefab, parent);
        tileGameObject.transform.localPosition = new Vector3(col, -row, 0); // Adjust for your layout
        Tile tile = tileGameObject.GetComponent<Tile>();
        tile.SetLetter(letter);
        tile.rowIndex = row;
        tile.colIndex = col;
        return tile;
    }


    public void SetLetter(string newLetter)
    {
        letter = newLetter;
        text.text = newLetter;
    }

    public void SetState(State state)
    {
        this.currentState = state;
        fill.color = state.fillColor;
        outline.effectColor = state.outlineColor;
        canvasGroup.interactable = state.isInteractive;
    }

    public void SetEmptyState()
    {
        SetState(emptyState);
    }

    public void SetSelectedState()
    {
        SetState(selectedState);
    }

    public void SetCorrectState()
    {
        SetState(correctState);
    }

    public void SetSpangramState()
    {
        SetState(spangramState);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (canvasGroup.interactable)
        {
            OnTilePointerDownEvent?.Invoke(this, eventData);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (canvasGroup.interactable)
        {
            OnTilePointerEnterEvent?.Invoke(this, eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (canvasGroup.interactable)
        {
            OnTilePointerUpEvent?.Invoke(this);
        }
    }

    internal string GetLetter()
    {
        return this.letter.ToString();
    }
}