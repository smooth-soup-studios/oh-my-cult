using System.Collections.Generic;

public interface IDataManager {
	public GameData Load(string profileId);
	public void Save(GameData data, string profileId);
	public Dictionary<string, GameData> LoadAllSaveSlots();
}