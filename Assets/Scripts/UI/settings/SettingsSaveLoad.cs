using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using RPG.UI;

namespace RPG.Saving
{
    public class SettingsSaveLoad : MonoBehaviour
    {
        public string fileName = "/config.cfg";
        public PlayerSettings playerSettings;
        // Start is called before the first frame update
        void Start()
        {
            Load();
        }
        /// <summary>
        /// Saves the game to a file
        /// </summary>
        /// 

        public void Save()
        {
            
            string dest = Application.persistentDataPath + fileName;
            FileStream file;
            if (File.Exists(dest)) file = File.OpenWrite(dest);
            else file = File.Create(dest);


            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, playerSettings);
            file.Close();
        }
        /// <summary>
        /// Loads the game from a file
        /// </summary>
        void Load()
        {
            string dest = Application.persistentDataPath + fileName;
            FileStream file;
            if (File.Exists(dest)) file = File.OpenRead(dest);
            else
            {
                print("uh oh no file");
                return;
            }
            PlayerSettings saveDat = new PlayerSettings();
            BinaryFormatter bf = new BinaryFormatter();
            saveDat = (PlayerSettings)bf.Deserialize(file);
            playerSettings.SetData(saveDat);
            file.Close();
        }
        private void OnApplicationQuit()
        {
            Save();
        }

    }
}
