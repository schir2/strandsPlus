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
    public Tile.State emptyState;
    public Tile.State selectedState;
    public Tile.State correctState;
    public Tile.State spangramState;

    public State currentState { get; private set; }
    public char letter { get; private set; }

    private Image fill;
    private Outline outline;
    private TextMeshProUGUI text;
    private Board board;
    private CanvasGroup canvasGroup;

    internal int rowIndex;
    internal int colIndex;

    private void Awake()
    {
        fill = GetComponent<Image>();
        outline = GetComponent<Outline>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        board = GetComponentInParent<Board>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void SetLetter(char letter)
    {
        this.letter = letter;
        text.text = letter.ToString();
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
            board.OnTilePointerDown(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (canvasGroup.interactable)
        {
            board.OnTilePointerEnter(this);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (canvasGroup.interactable)
        {
            board.OnTilePointerUp();
        }
    }

    internal string GetLetter()
    {
        return this.letter.ToString();
    }
}