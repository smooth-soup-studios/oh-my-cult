using UnityEngine;

[CreateAssetMenu(fileName = "new Note", menuName = "OhMyCult/Items/new NoteContents", order = 0)]
public class NoteContents : ScriptableObject {
	public Sprite NoteSprite;
	public string NoteHeader = "Torn note";
	[TextArea] public string NoteText;
}