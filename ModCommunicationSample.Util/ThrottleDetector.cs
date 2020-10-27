using UnityEngine;

namespace ModCommunicationSample.Util
{
	/// <summary>
	/// Reports throttle value changes of the current vassel.
	/// </summary>
	[KSPAddon(KSPAddon.Startup.Flight, once: false)]
	internal class ThrottleDetector : MonoBehaviour
	{
		private void Update()
		{
			var controlState = FlightInputHandler.state;
			if(controlState != null)
			{
				var throttleValue = controlState.mainThrottle;
				if (throttleValue != previousThrottleValue)
				{
					Debug.Log($"[ModCommunicationSample.Util] Throttle changed: {throttleValue:F2}.");

					UtilApi.ThrottleChanged(throttleValue);

					previousThrottleValue = throttleValue;
				}
			}
		}

		private float? previousThrottleValue;
	}
}
