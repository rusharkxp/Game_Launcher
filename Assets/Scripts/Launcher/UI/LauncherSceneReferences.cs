using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Launcher.UI
{
    public class LauncherSceneReferences : MonoBehaviour
    {
        public List<LoadConfig> Configs;

        public GameLoader GameLoaderPrefab;

        public Transform GameLoaderParent;
    }
}