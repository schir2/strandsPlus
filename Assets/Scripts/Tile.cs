using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerUpHandler
{
    [Serializable]
    public class State
    {
        public Color fillColor;
        public Color outlineColor;
        public bool isInteractive;
    }

    [Header("Tiles")] public State emptyState;
    public State selectedState;
    public State correctState;
    public State spangramState;
    public State highlightedState;

    public State CurrentState { get; private set; }
    public string Letter { get; private set; }

    private Image fill;
    private Outline outline;
    public TextMeshProUGUI text;
    private CanvasGroup canvasGroup;

    internal int RowIndex;
    internal int ColIndex;

    public static event Action<Tile, PointerEventData> OnTilePointerDownEvent;
    public static event Action<Tile, PointerEventData> OnTilePointerEnterEvent;
    public static event Action<Tile, PointerEventData> OnTilePointerUpEvent;

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

        CurrentState = emptyState;
    }

    public static Tile CreateTile(GameObject tilePrefab, Transform parent, int row, int col, string letter)
    {
        if (tilePrefab == null)
        {
            Debug.LogError("Tile prefab is not set.");
            return null;
        }

        var tileGameObject = Instantiate(tilePrefab, parent);
        tileGameObject.transform.localPosition = new Vector3(col, -row, 0);
        tileGameObject.AddComponent<Tile>();
        var tile = tileGameObject.GetComponent<Tile>();
        tile.SetLetter(letter);
        tile.RowIndex = row;
        tile.ColIndex = col;
        return tile;
    }


    public void SetLetter(string newLetter)
    {
        Letter = newLetter;
        text.text = newLetter;
    }

    public void SetState(State state)
    {
        CurrentState = state;
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

    public void SetHighlightedState()
    {
        SetState(highlightedState);
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
            OnTilePointerUpEvent?.Invoke(this, eventData);
        }
    }

    internal string GetLetter()
    {
        return Letter;
    }
}