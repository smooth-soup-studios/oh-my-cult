using UnityEngine;

[CreateAssetMenu(fileName = "new Note", menuName = "OhMyCult/Items/new NoteContents", order = 0)]
public class NoteContents : ScriptableObject {
	public Sprite Sprite;
	public string Title = "Torn note";
	[TextArea] public string Content;
}