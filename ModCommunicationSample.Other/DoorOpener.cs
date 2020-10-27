using UnityEngine;

namespace ModCommunicationSample.Other
{
	/// <summary>
	/// Changes bay door deploy limit of the current vessel.
	/// </summary>
	[KSPAddon(KSPAddon.Startup.Flight, once: false)]
	internal class DoorOpener : MonoBehaviour
	{
		private void Start()
		{
			UtilApiWrapper.ThrottleDetectorSubscribe(UpdateDeployValue);
	}

		private void OnDestroy()
		{
			UtilApiWrapper.ThrottleDetectorUnsubscribe(UpdateDeployValue);
		}

		private void Update()
		{
			var vessel = FlightGlobals.ActiveVessel;
			if (vessel != null && _newDeployValue is float value)
			{
				foreach (var part in vessel.Parts)
					foreach (var module in part.Modules.GetModules<ModuleAnimateGeneric>())
						module.deployPercent = value;

				Debug.Log($"[ModCommunicationSample.Other] Bay doors deploy limit changed: {value:F0}%.");

				_newDeployValue = null;
			}
		}

		private void UpdateDeployValue(float value)
		{
			_newDeployValue = 100 * value;
		}

		private float? _newDeployValue;
	}
}
