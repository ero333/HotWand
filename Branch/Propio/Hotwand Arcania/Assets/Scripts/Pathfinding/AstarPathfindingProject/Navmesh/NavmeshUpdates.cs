using UnityEngine;
using System.Collections.Generic;
using Pathfinding.Util;
using Pathfinding.Serialization;
#if UNITY_5_5_OR_NEWER
using UnityEngine.Profiling;
#endif

namespace Pathfinding {
	/** Helper for navmesh cut objects.
	 * Responsible for keeping track of which navmesh cuts have moved and coordinating graph updates to account for those changes.
	 *
	 * \see \ref navmeshcutting
	 * \see #AstarPath.navmeshUpdates
	 * \see #Pathfinding.NavmeshBase.enableNavmeshCutting
	 *
	 * \astarpro
	 */
	[System.Serializable]
	public class NavmeshUpdates {
		/** How often to check if an update needs to be done (real seconds between checks).
		 * For worlds with a very large number of NavmeshCut objects, it might be bad for performance to do this check every frame.
		 * If you think this is a performance penalty, increase this number to check less often.
		 *
		 * For almost all games, this can be kept at 0.
		 *
		 * If negative, no updates will be done. They must be manually triggered using #ForceUpdate.
		 *
		 * \snippet MiscSnippets.cs NavmeshUpdates.updateInterval
		 *
		 * You can also find this in the AstarPath inspector under Settings.
		 * \shadowimage{navmeshcut_update_interval.png}
		 */
		public float updateInterval;

		internal class NavmeshUpdateSettings {
			public NavmeshUpdateSettings(NavmeshBase graph) {}
			public void OnRecalculatedTiles (NavmeshTile[] tiles) {}
		}
		internal void Update () {}
		internal void OnEnable () {}
		internal void OnDisable () {}
	}
}
