using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;

/*
    Written in whole or in part by Princess Apollo
    Licensed in whole according to the license found at https://static.princessapollo.se/licenses/mit-t.txt
*/

namespace PrincessApollo
{
    namespace Controls
    {
        public static class Controls
        {
            public static ControlScheme DefaultScheme = new ControlScheme(new Dictionary<string, string>(){
                {"PlayerOne-Forward", "W"},
                {"PlayerOne-Back", "A"},
                {"PlayerOne-Left", "S"},
                {"PlayerOne-Right", "D"},
                {"PlayerOne-Punch", "Space"},
                {"PlayerTwo-Forward", "UpArrow"},
                {"PlayerTwo-Back", "DownArrow"},
                {"PlayerTwo-Left", "LeftArrow"},
                {"PlayerTwo-Right", "RightArrow"},
                {"PlayerTwo-Punch", "Return"},
            });
            public static ControlScheme Scheme
            {
                get
                {
                    if (scheme != null) return scheme;
                    else
                    {
                        var path = UnityEngine.Application.dataPath + "/controls.ctrl";
                        if (!File.Exists(path))
                        {
                            File.WriteAllText(path, DefaultScheme.ToString());
                            Debug.Log(path + " has been created");
                        }
                        scheme = JsonConvert.DeserializeObject<ControlScheme>(File.ReadAllText(path));
                        return scheme;
                    }

                }
                internal set
                {
                    scheme = value;
                }
            }
            private static ControlScheme scheme;
        }
        [System.Serializable]
        public class ControlScheme
        {
            [JsonProperty]
            public Dictionary<string, string> keys;
            public ControlScheme(Dictionary<string, string> keys) => this.keys = keys;
            public override string ToString() => JsonConvert.SerializeObject(this);
            public KeyCode GetCodeFromKey(string key) => (KeyCode)System.Enum.Parse(typeof(KeyCode), keys[key]);
        }
    }
}