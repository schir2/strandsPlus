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
    }

    public State state { get; private set; }
    public char letter { get; private set; }

    private Image fill;
    private Outline outline;
    private TextMeshProUGUI text;
    private Board board;
    internal int rowIndex;
    internal int colIndex;

    private void Awake()
    {
        fill = GetComponent<Image>();
        outline = GetComponent<Outline>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        board = GetComponentInParent<Board>();
    }

    public void SetLetter(char letter)
    {
        this.letter = letter;
        text.text = letter.ToString();
    }

    public void SetState(State state)
    {
        this.state = state;
        fill.color = state.fillColor;
        outline.effectColor = state.outlineColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        board.OnTilePointerDown(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        board.OnTilePointerEnter(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        board.OnTilePointerUp();
    }

    internal string GetLetter()
    {
        return this.letter.ToString();
    }
}