using UnityEngine;
using UnityEngine.UIElements;

namespace Managers {
    public class UIManager : MonoBehaviour {
        public static UIManager Instance { get; private set; }

        private VisualElement _root;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                _root = GetComponent<UIDocument>().rootVisualElement;
            }
            else {
                Destroy(gameObject);
            }
        }

        public void ToggleDialogBox() {
            if (_root.Q<VisualElement>("dialog-box").ClassListContains("closed")) {
                ShowDialogBox();
            }
            else {
                HideDialogBox();
            }
        }

        public void ShowDialogBox() {
            _root.Q<VisualElement>("dialog-box").RemoveFromClassList("closed");
        }

        public void HideDialogBox() {
            _root.Q<VisualElement>("dialog-box").AddToClassList("closed");
        }
    }
}