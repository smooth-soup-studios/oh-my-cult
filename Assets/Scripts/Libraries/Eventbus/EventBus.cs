using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventBus : MonoBehaviour {
	[Header("Debug settings")]
	[SerializeField] private bool _showLogging = false;
	private string _logname = "EventBus";


	private Hashtable _eventHash = new();
	private static EventBus _eventBus;
	public static EventBus Instance {
		get {
			if (!_eventBus) {
				_eventBus = FindAnyObjectByType<EventBus>();

				if (!_eventBus) {
					Logger.Log("EventBus", "No EventBus found in the scene!");
				}
				else {
					_eventBus.Init();
				}
			}
			return _eventBus;
		}
	}

	private void Init() {
		_eventBus._eventHash ??= new Hashtable();
	}

	private void Awake() {
		if (_eventBus == null || _eventBus == this) {
			_eventBus = this;
		}
		else {
			Logger.LogWarning(_logname, "Multiple Instances found! Exiting..");
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(Instance);
	}

	/// <summary>
	/// Subscribes a method to the specified Event with parameter T.
	/// <br/>
	/// The method should have a parameter of the same type as T.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="eventName"></param>
	/// <param name="listener"></param>
	public static void Subscribe<T>(EventType eventName, UnityAction<T> listener) {
		UnityEvent<T> thisEvent;

		string key = GetKey<T>(eventName);

		if (Instance._eventHash.ContainsKey(key)) {
			thisEvent = (UnityEvent<T>)Instance._eventHash[key];
			thisEvent.AddListener(listener);
			Instance._eventHash[eventName] = thisEvent;
		}
		else {
			thisEvent = new UnityEvent<T>();
			thisEvent.AddListener(listener);
			Instance._eventHash.Add(key, thisEvent);
		}
	}

	/// <summary>
	/// Subscribes a method to the specified Event.
	/// <br/>
	/// This version contains no parameters. Use <see cref="Subscribe{T}">
	/// Subscribe &lt;T&gt;
	/// </see>
	/// </summary>
	/// <param name="eventName"></param>
	/// <param name="listener"></param>
	public static void Subscribe(EventType eventName, UnityAction listener) {
		UnityEvent thisEvent;

		if (Instance._eventHash.ContainsKey(eventName)) {
			thisEvent = (UnityEvent)Instance._eventHash[eventName];
			thisEvent.AddListener(listener);
			Instance._eventHash[eventName] = thisEvent;
		}
		else {
			thisEvent = new UnityEvent();
			thisEvent.AddListener(listener);
			Instance._eventHash.Add(eventName, thisEvent);
		}
	}

	public static void Unsubscribe<T>(EventType eventName, UnityAction<T> listener) {
		UnityEvent<T> thisEvent;
		string key = GetKey<T>(eventName);

		if (Instance._eventHash.ContainsKey(key)) {
			thisEvent = (UnityEvent<T>)Instance._eventHash[key];
			thisEvent.RemoveListener(listener);
		}
	}

	public static void Unsubscribe(EventType eventName, UnityAction listener) {
		UnityEvent thisEvent;

		if (Instance._eventHash.ContainsKey(eventName)) {
			thisEvent = (UnityEvent)Instance._eventHash[eventName];
			thisEvent.RemoveListener(listener);
		}
	}

	public static void TriggerEvent<T>(EventType eventName, T val) {
		UnityEvent<T> thisEvent;
		string key = GetKey<T>(eventName);

		if (Instance._eventHash.ContainsKey(key)) {
			thisEvent = (UnityEvent<T>)Instance._eventHash[key];
			thisEvent.Invoke(val);
		}
	}

	public static void TriggerEvent(EventType eventName) {
		UnityEvent thisEvent;

		if (Instance._eventHash.ContainsKey(eventName)) {
			thisEvent = (UnityEvent)Instance._eventHash[eventName];
			thisEvent.Invoke();
		}
	}

	public static string GetKey<T>(EventType eventtype) {
		Type type = typeof(T);
		return $"{type}_{eventtype}";
	}
}
