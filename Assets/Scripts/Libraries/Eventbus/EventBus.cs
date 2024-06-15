using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventBus : MonoBehaviour {
	[Header("Debug settings")]
	[SerializeField] private bool _showLogging = false;

	[SerializeField, Tooltip("Enables logging of subscription calls")] private bool _showSubscriptionLogs = false;
	[SerializeField, Tooltip("Enables logging of event triggers")] private bool _showTriggerLogs = false;

	private static string _logname = "EventBus";


	private Hashtable _eventHash = new();
	private static EventBus _eventBus;
	public static EventBus Instance {
		get {
			// Check if an instance exists. if not grab the one (which should be) present in the scene.
			if (!_eventBus) {
				_eventBus = FindAnyObjectByType<EventBus>();

				if (_eventBus) {
					_eventBus.Init();
				}
				else {
					Logger.LogError(_logname, "No EventBus found in the scene!");
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
			Logger.LogWarning(_logname, "Multiple Instances found! Exiting...");
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(Instance);
	}

	/// <summary> Subscribes a method to the specified Event with a value of type T.
	/// <br/>
	/// The method should have a parameter of the same type as T.
	/// 	<para>
	/// 		Usage:
	/// 		<example>
	/// 			<c> EventBus.Instance.Subscribe&lt;Vector2&gt;(EventType.MOVEMENT, UpdateMovement) </c>
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
			Instance._eventHash[key] = newEvent;
		}
		else {
			newEvent = new UnityEvent<T>();
			newEvent.AddListener(listener);
			Instance._eventHash.Add(key, newEvent);
		}

		if (_showSubscriptionLogs) {
			sendToLogger($"{listener.Target} subscribed to event {eventName}<{typeof(T).Name}>");
		}
	}

	/// <summary>
	/// Subscribes a method to the specified Event.
	/// <br/>
	/// This version cannot recieve values! Use <see cref="Subscribe{T}">the generic version of this method instead.</see>
	/// <para>
	/// 	Usage:
	/// 	<example>
	/// 		<c> EventBus.Instance.Subscribe(EventType.MOVEMENT, UpdateMovement) </c>
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

		if (_showSubscriptionLogs) {
			sendToLogger($"{listener} subscribed to event {eventName}");
		}
	}

	/// <summary>
	/// Unsubscribes a method from the specified Event.
	/// <br/>
	/// The type T should match the type of the subscribe call
	/// <para>
	/// 	Usage:
	/// 	<example>
	/// 		<c> EventBus.Instance.Unsubscribe&lt;Vector2&gt;(EventType.MOVEMENT, UpdateMovement) </c>
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
			Instance._eventHash[key] = newEvent;


			if (_showSubscriptionLogs) {
				sendToLogger($"{listener.Target} unsubscribed from event {eventName}<{typeof(T).Name}>");
			}
		}
	}

	/// <summary>
	/// Unsubscribes a method from the specified Event.
	/// <para>
	/// 	Usage:
	/// 	<example>
	/// 		<c> EventBus.Instance.Unsubscribe(EventType.MOVEMENT, UpdateMovement) </c>
	/// 	</example>
	/// </para>
	/// </summary>
	/// <param name="eventName"></param>
	/// <param name="listener"></param>
	public void Unsubscribe(EventType eventName, UnityAction listener) {
		UnityEvent newEvent;

		if (Instance._eventHash.ContainsKey(eventName)) {
			newEvent = (UnityEvent)Instance._eventHash[eventName];
			newEvent.RemoveListener(listener);
			Instance._eventHash[eventName] = newEvent;

			if (_showSubscriptionLogs) {
				sendToLogger($"{listener} unsubscribed from event {eventName}");
			}
		}
	}

	/// <summary>
	/// Invokes the specified Event with the supplied value.
	/// <para>
	/// 	Usage:
	/// 	<example>
	/// 		<c> EventBus.Instance.TriggerEvent&lt;Vector2&gt;(EventType.MOVEMENT) </c>
	/// 	</example>
	/// </para>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="eventName"></param>
	/// <param name="val"></param>
	public void TriggerEvent<T>(EventType eventName, T val) {
		UnityEvent<T> newEvent;
		string key = GetKey<T>(eventName);

		if (Instance._eventHash.ContainsKey(key)) {
			newEvent = (UnityEvent<T>)Instance._eventHash[key];
			newEvent.Invoke(val);

			if (_showTriggerLogs) {
				sendToLogger($"Event {eventName} was triggerd with value {typeof(T).Name}({val})");
			}
		}
	}

	/// <summary>
	/// Invokes the specified Event. <br/>
	/// Non-Generic version of this method
	/// <para>
	/// 	Usage:
	/// 	<example>
	/// 		<c> EventBus.Instance.TriggerEvent(EventType.MOVEMENT) </c>
	/// 	</example>
	/// </para>
	/// </summary>
	/// <param name="eventName"></param>
	public void TriggerEvent(EventType eventName) {
		UnityEvent newEvent;

		if (Instance._eventHash.ContainsKey(eventName)) {
			newEvent = (UnityEvent)Instance._eventHash[eventName];
			newEvent.Invoke();

			if (_showTriggerLogs) {
				sendToLogger($"Event {eventName} was triggerd");
			}
		}
	}

	private string GetKey<T>(EventType eventtype) {
		Type type = typeof(T);
		return $"{type}_{eventtype}";
	}

	private void sendToLogger(string text) {
		if (_showLogging) {
			Logger.Log(_logname, text);
		}
	}
}
