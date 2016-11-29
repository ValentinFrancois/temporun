#pragma strict

public var cursorTexture: Texture2D;
public var cursorMode: CursorMode = CursorMode.Auto;
public var hotSpot: Vector2 = Vector2.zero;
function OnMouseEnter() {
	Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
}
function OnMouseExit() {
	Cursor.SetCursor(null, Vector2.zero, cursorMode);
}