using System;
using UnityEngine;

namespace _GameFolders.Scripts.Data
{
    public static class DatabaseManager
    {
        public static void SaveData<T>(string key, T data)
        {
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogError("SaveData key cannot be null or empty");
                return;
            }

            var type = typeof(T);

            if (type == typeof(int))
                PlayerPrefs.SetInt(key, Convert.ToInt32(data));
            else if (type == typeof(float))
                PlayerPrefs.SetFloat(key, Convert.ToSingle(data));
            else if (type == typeof(string))
                PlayerPrefs.SetString(key, Convert.ToString(data));
            else if (type == typeof(bool))
                PlayerPrefs.SetInt(key, Convert.ToBoolean(data) ? 1 : 0);
            else
                throw new NotSupportedException($"Type {type} is not supported by DatabaseManager");

            PlayerPrefs.Save();
        }

        public static T LoadData<T>(string key, T defaultValue = default)
        {
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogError("LoadData key cannot be null or empty");
                return defaultValue;
            }

            var type = typeof(T);

            if (!PlayerPrefs.HasKey(key))
                return defaultValue;

            if (type == typeof(int))
                return (T)(object)PlayerPrefs.GetInt(key);
            else if (type == typeof(float))
                return (T)(object)PlayerPrefs.GetFloat(key);
            else if (type == typeof(string))
                return (T)(object)PlayerPrefs.GetString(key);
            else if (type == typeof(bool))
                return (T)(object)(PlayerPrefs.GetInt(key) == 1);
            else
                throw new NotSupportedException($"Type {type} is not supported by DatabaseManager");
        }
        
        public static void DeleteData(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogError("DeleteData key cannot be null or empty");
                return;
            }

            if (PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.DeleteKey(key);
                PlayerPrefs.Save();
            }
        }
        
        public static void ClearAllData()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}