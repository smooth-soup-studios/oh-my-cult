using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{

    [SerializeField] private List<GameObject> _borders;
	[SerializeField] private List<GameObject> _enemies;
	[SerializeField] private bool _isCleared;

	private void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player" && _enemies.Count > 0){
			foreach (GameObject border in _borders)
			{
				border.SetActive(true);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Enemy"){
			_enemies.Remove(other.gameObject);
			if (_enemies.Count <= 0){
				UnlockArea();
			}
		}
	}

	private void Start(){
		if (_isCleared){
			foreach (GameObject enemy in _enemies)
			{
				EventBus.Instance.TriggerEvent(EventType.DEATH, enemy);
				_enemies.Remove(enemy);
			}
		}
	}

	private void UnlockArea(){
		foreach (GameObject border in _borders)
			{
				border.SetActive(false);
				_isCleared = true;
			}
	}
}
