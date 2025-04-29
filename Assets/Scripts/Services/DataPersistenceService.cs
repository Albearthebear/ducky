using System.IO;
using UnityEngine;
using DuckFriend.Data;

namespace DuckFriend.Services
{
    /// <summary>
    /// Service for saving and loading user data from persistent storage.
    /// </summary>
    public static class DataPersistenceService
    {
        private static readonly string FileName = "userdata.json";

        private static string FilePath => Path.Combine(Application.persistentDataPath, FileName);

        /// <summary>
        /// Saves the provided user data to disk as JSON.
        /// </summary>
        /// <param name="data">The user data to save.</param>
        public static void SaveUserData(UserData data)
        {
            try
            {
                string json = JsonUtility.ToJson(data, prettyPrint: true);
                File.WriteAllText(FilePath, json);
                Debug.Log($"UserData saved to {FilePath}");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to save UserData: {ex}");
            }
        }

        /// <summary>
        /// Loads user data from disk; if none exists returns a new default UserData.
        /// </summary>
        /// <returns>The loaded or default user data.</returns>
        public static UserData LoadUserData()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);
                    var data = JsonUtility.FromJson<UserData>(json);
                    if (data != null)
                    {
                        Debug.Log($"UserData loaded from {FilePath}");
                        return data;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Failed to load UserData: {ex}");
            }

            Debug.Log("Creating new UserData instance");
            return new UserData();
        }
    }
} 