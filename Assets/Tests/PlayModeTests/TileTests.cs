using System.Collections;
using System.Collections.Generic;
using Gameplay;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileTests
{
    private GameObject tilePrefab;
    private Tile tile;

    [SetUp]
    public void Setup()
    {
        // Create a new Tile GameObject with the necessary components
        tilePrefab = new GameObject("TilePrefab");
        tilePrefab.AddComponent<Image>();
        tilePrefab.AddComponent<Outline>();
        var textObject = new GameObject("Text");
        textObject.transform.SetParent(tilePrefab.transform);
        textObject.AddComponent<TextMeshProUGUI>();
        tilePrefab.AddComponent<CanvasGroup>();
        tilePrefab.AddComponent<Tile>();

        // Instantiate the Tile
        tile = Tile.CreateTile(tilePrefab, null, 0, 0, "A");
    }

    [TearDown]
    public void Teardown()
    {
        // Clean up after each test
        Object.DestroyImmediate(tilePrefab);
    }

    [Test]
    public void TestSetLetter()
    {
        // Arrange
        string expectedLetter = "B";

        // Act
        tile.SetLetter(expectedLetter);

        // Assert
        Assert.AreEqual(expectedLetter, tile.letter);
        Assert.AreEqual(expectedLetter, tile.GetComponentInChildren<TextMeshProUGUI>().text);
    }

    [Test]
    public void TestSetState()
    {
        // Arrange
        Tile.State state = new Tile.State
        {
            fillColor = Color.red,
            outlineColor = Color.blue,
            isInteractive = true
        };

        // Act
        tile.SetState(state);

        // Assert
        Assert.AreEqual(state, tile.currentState);
        Assert.AreEqual(state.fillColor, tile.GetComponent<Image>().color);
        Assert.AreEqual(state.outlineColor, tile.GetComponent<Outline>().effectColor);
        Assert.AreEqual(state.isInteractive, tile.GetComponent<CanvasGroup>().interactable);
    }

    [Test]
    public void TestOnPointerDown()
    {
        // Arrange
        bool eventTriggered = false;
        Tile.OnTilePointerDownEvent += (t, e) => eventTriggered = true;

        // Act
        var eventData = new PointerEventData(EventSystem.current);
        tile.OnPointerDown(eventData);

        // Assert
        Assert.IsTrue(eventTriggered);

        // Cleanup
        Tile.OnTilePointerDownEvent -= (t, e) => eventTriggered = true;
    }

    [Test]
    public void TestOnPointerEnter()
    {
        // Arrange
        bool eventTriggered = false;
        Tile.OnTilePointerEnterEvent += (t, e) => eventTriggered = true;

        // Act
        var eventData = new PointerEventData(EventSystem.current);
        tile.OnPointerEnter(eventData);

        // Assert
        Assert.IsTrue(eventTriggered);

        // Cleanup
        Tile.OnTilePointerEnterEvent -= (t, e) => eventTriggered = true;
    }

    [Test]
    public void TestOnPointerUp()
    {
        // Arrange
        bool eventTriggered = false;
        Tile.OnTilePointerUpEvent += (t, e) => eventTriggered = true;

        // Act
        var eventData = new PointerEventData(EventSystem.current);
        tile.OnPointerUp(eventData);

        // Assert
        Assert.IsTrue(eventTriggered);

        // Cleanup
        Tile.OnTilePointerUpEvent -= (t, e) => eventTriggered = true;
    }
}