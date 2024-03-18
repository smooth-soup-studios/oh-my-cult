using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventBus : MonoBehaviour {
	[Header("Debug settings")]
	[SerializeField] private bool _showLogging = false;
	private static string _logname = "EventBus";


	private Hashtable _eventHash = new();
	private static EventBus _eventBus;
	public static EventBus Instance {
		get {
			if (!_eventBus) {
				_eventBus = FindAnyObjectByType<EventBus>();

				if (_eventBus) {
					_eventBus.Init();
				}
				else {
					Logger.Log(_logname, "No EventBus found in the scene!");
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
	/// 	<para>
	/// 		Usage:
	/// 		<example>
	/// 			<c> EventBus.Subscribe&lt;Vector2&gt;(EventType.MOVEMENT, UpdateMovement) </c>
	/// 		</example>
	/// 	</para>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="eventName"></param>
	/// <param name="listener"></param>
	public void Subscribe<T>(EventType eventName, UnityAction<T> listener) {
		UnityEvent<T> newEvent;

		string key = GetKey<T>(eventName);

		if (Instance._eventHash.ContainsKey(key)) {
			newEvent = (UnityEvent<T>)Instance._eventHash[key];
			newEvent.AddListener(listener);
			Instance._eventHash[eventName] = newEvent;
		}
		else {
			newEvent = new UnityEvent<T>();
			newEvent.AddListener(listener);
			Instance._eventHash.Add(key, newEvent);
		}
	}

	/// <summary>
	/// Subscribes a method to the specified Event.
	/// <br/>
	/// This version contains no parameters. Use <see cref="Subscribe{T}">the generic version of this method instead.</see>
	/// <para>
	/// 	Usage:
	/// 	<example>
	/// 		<c> EventBus.Subscribe(EventType.MOVEMENT, UpdateMovement) </c>
	/// 	</example>
	/// </para>
	/// </summary>
	/// <param name="eventName"></param>
	/// <param name="listener"></param>
	public void Subscribe(EventType eventName, UnityAction listener) {
		UnityEvent newEvent;

		if (Instance._eventHash.ContainsKey(eventName)) {
			newEvent = (UnityEvent)Instance._eventHash[eventName];
			newEvent.AddListener(listener);
			Instance._eventHash[eventName] = newEvent;
		}
		else {
			newEvent = new UnityEvent();
			newEvent.AddListener(listener);
			Instance._eventHash.Add(eventName, newEvent);
		}
	}

	/// <summary>
	/// Unsubscribes a method from the specified Event.
	/// <br/>
	/// The T parameter should match the type of the subscribe call
	/// <para>
	/// 	Usage:
	/// 	<example>
	/// 		<c> EventBus.Unsubscribe&lt;Vector2&gt;(EventType.MOVEMENT, UpdateMovement) </c>
	/// 	</example>
	/// </para>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="eventName"></param>
	/// <param name="listener"></param>
	public void Unsubscribe<T>(EventType eventName, UnityAction<T> listener) {
		UnityEvent<T> newEvent;
		string key = GetKey<T>(eventName);

		if (Instance._eventHash.ContainsKey(key)) {
			newEvent = (UnityEvent<T>)Instance._eventHash[key];
			newEvent.RemoveListener(listener);
		}
	}

	/// <summary>
	/// Unsubscribes a method from the specified Event.
	/// <para>
	/// 	Usage:
	/// 	<example>
	/// 		<c> EventBus.Unsubscribe(EventType.MOVEMENT, UpdateMovement) </c>
	/// 	</example>
	/// </para>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="eventName"></param>
	/// <param name="listener"></param>
	public void Unsubscribe(EventType eventName, UnityAction listener) {
		UnityEvent newEvent;

		if (Instance._eventHash.ContainsKey(eventName)) {
			newEvent = (UnityEvent)Instance._eventHash[eventName];
			newEvent.RemoveListener(listener);
		}
	}

	/// <summary>
	/// Invokes the specified Event with the supplied value.
	/// <para>
	/// 	Usage:
	/// 	<example>
	/// 		<c> EventBus.TriggerEvent&lt;Vector2&gt;(EventType.MOVEMENT) </c>
	/// 	</example>
	/// </para>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="eventName"></param>
	/// <param name="listener"></param>
	public void TriggerEvent<T>(EventType eventName, T val) {
		UnityEvent<T> newEvent;
		string key = GetKey<T>(eventName);

		if (Instance._eventHash.ContainsKey(key)) {
			newEvent = (UnityEvent<T>)Instance._eventHash[key];
			newEvent.Invoke(val);
		}
	}

	/// <summary>
	/// Invokes the specified Event.
	/// <para>
	/// 	Usage:
	/// 	<example>
	/// 		<c> EventBus.TriggerEvent(EventType.MOVEMENT) </c>
	/// 	</example>
	/// </para>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="eventName"></param>
	/// <param name="listener"></param>
	public void TriggerEvent(EventType eventName) {
		UnityEvent newEvent;

		if (Instance._eventHash.ContainsKey(eventName)) {
			newEvent = (UnityEvent)Instance._eventHash[eventName];
			newEvent.Invoke();
		}
	}

	private string GetKey<T>(EventType eventtype) {
		Type type = typeof(T);
		return $"{type}_{eventtype}";
	}
}
