using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ModCommunicationSample.Other
{
	/// <summary>
	/// Calls methods of the Util mod via Reflection using CreateDelegate().
	/// Has a slightly better performance than UtilApiWrapper.
	/// </summary>
	internal static class UtilApiAlternativeWrapper
	{
		public static readonly MethodDelegate ThrottleDetectorSubscribe =
			_ => Debug.Log("[ModCommunicationSample.Other] Utils are unavailable. Not subscribed.");

		public static readonly MethodDelegate ThrottleDetectorUnsubscribe =
			_ => Debug.Log("[ModCommunicationSample.Other] Utils are unavailable. Not unsubscribed.");

		#region

		public delegate void MethodDelegate(Action<float> throttleChanged);

		static UtilApiAlternativeWrapper()
		{
			var utilApi = AssemblyLoader.loadedAssemblies
				.FirstOrDefault(a => a.name.Equals(AssemblyName, StringComparison.Ordinal))
				?.assembly
				?.GetType(ClassName);

			if(utilApi != null)
			{
				Debug.Log("[ModCommunicationSample.Other] Utils are available.");

				ThrottleDetectorSubscribe = (MethodDelegate) Delegate.CreateDelegate(
					typeof(MethodDelegate), utilApi, nameof(ThrottleDetectorSubscribe));

				ThrottleDetectorUnsubscribe = (MethodDelegate) Delegate.CreateDelegate(
					typeof(MethodDelegate), utilApi, nameof(ThrottleDetectorUnsubscribe));
			}
			else
				Debug.Log("[ModCommunicationSample.Other] Utils are unavailable.");
		}

		private const string AssemblyName = "ModCommunicationSample.Util";
		private const string ClassName = AssemblyName + ".UtilApi";

		#endregion
	}
}
