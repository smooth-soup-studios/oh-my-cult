using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NoteInteracteble : BaseInteractable {
	[Header("Note Settings")]
	[SerializeField] protected NoteContents Note;

	private SpriteRenderer _renderer;

	private void Awake() {
		_renderer = GetComponent<SpriteRenderer>();
	}

	public override void Interact(GameObject interactor) {
		base.Interact(interactor);
		if (UIManager.Instance && Note) {
			UIManager.Instance.ShowDialogBox(Note.Title, Note.Content);
		}
	}

	public override void OnDeselect() {
		_renderer.color = Color.white;
		UIManager.Instance.HideDialogBox();
	}

	public override void OnSelect() {
		_renderer.color = Color.green;
	}

	protected new void OnValidate() {
		base.OnValidate();

		// Can be called before the renderer is initialized
		if (!_renderer) {
			_renderer = GetComponent<SpriteRenderer>();
		}
		if (Note) {
			_renderer.sprite = Note.Sprite;
		}
	}
}