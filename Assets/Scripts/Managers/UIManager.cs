using UnityEngine;
using UnityEngine.UIElements;

namespace Managers {
    public class UIManager : MonoBehaviour {
        public static UIManager Instance { get; private set; }

        private VisualElement _root;
        private VisualElement _healthBarValue;

        private float _debugHealth = .5f;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                _root = GetComponent<UIDocument>().rootVisualElement;
                _healthBarValue = _root.Q<VisualElement>("health-bar-value");
            }
            else {
                Destroy(gameObject);
            }
        }

        private void Update() {
            _healthBarValue.style.width = new StyleLength(new Length(_debugHealth * 100, LengthUnit.Percent));
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

        public void DebugCycleHealth() {
            _debugHealth = 1 - ((1 - _debugHealth + .1f) % 1);
        }
    }
}