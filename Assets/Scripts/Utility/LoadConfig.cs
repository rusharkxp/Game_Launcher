using UnityEngine;

namespace Utility
{
    [CreateAssetMenu(fileName = "Config", menuName = "Custom/Config")]
    public class LoadConfig : ScriptableObject
    {
        [field: SerializeField]
        public string AssetName { get; private set; }
        
        [field: SerializeField]
        public string SaveFileName { get; private set; }
    }
}