/// <summary>
///
/// </summary>
public enum EventType {
	// Player input events
	MOVEMENT,
	DASH,
	INTERACT,
	USE_PRIMARY,
	USE_SECONDARY,
	HOTBAR_SELECT,
	HOTBAR_SWITCH,
	PAUSE,

	// State Events
	DEATH,
	HIT,
	INV_ADD,
	INV_REMOVE,

	// Audio Events
	AUDIO_PLAY,
	AUDIO_STOP,
	AUDIO_STOP_ALL,

	// Map events
	PLAYER_ENTER_BUILDING_COVER,
	PLAYER_EXIT_BUILDING_COVER,
	ENTER_CHURCH,
	ENTER_VILLAGE,

}