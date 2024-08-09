using System.Collections.Generic;
using Launcher.Utility;
using UnityEngine;

namespace Launcher.UI
{
    public class LauncherSceneReferences : MonoBehaviour
    {
        public List<GameLoaderConfig> Configs;

        public GameLoader GameLoaderPrefab;

        public Transform GameLoaderParent;
    }
}