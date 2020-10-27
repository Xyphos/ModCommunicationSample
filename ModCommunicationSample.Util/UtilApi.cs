using System;
using UnityEngine;

namespace ModCommunicationSample.Util
{
	/// <summary>
	/// A static class with a couple of methods to call from other mods.
	/// </summary>
	public static class UtilApi
	{
		public static void ThrottleDetectorSubscribe(Action<float> throttleChanged)
		{
			_throttleChanged += throttleChanged;
			Debug.Log("[ModCommunicationSample.Util] Subscribed.");
		}

		public static void ThrottleDetectorUnsubscribe(Action<float> throttleChanged)
		{
			_throttleChanged -= throttleChanged;
			Debug.Log("[ModCommunicationSample.Util] Unsubscribed.");
		}

		#region Non-public

		internal static void ThrottleChanged(float newThrottleValue) =>
			_throttleChanged?.Invoke(newThrottleValue);

		private static Action<float> _throttleChanged;

		#endregion
	}
}
