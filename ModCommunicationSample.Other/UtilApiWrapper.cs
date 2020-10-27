using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ModCommunicationSample.Other
{
	/// <summary>
	/// Calls methods of the Util mod via Reflection using GetMethod() and Invoke().
	/// </summary>
	internal static class UtilApiWrapper
	{
		public static void ThrottleDetectorSubscribe(Action<float> throttleChanged)
		{
			if(UtilApi != null)
			{
				UtilApi
					.GetMethod(nameof(ThrottleDetectorSubscribe), BindingFlags.Public | BindingFlags.Static)
					.Invoke(null, new object[] { throttleChanged });
				Debug.Log("[ModCommunicationSample.Other] Utils are available. Subscribed.");
			}
			else
				Debug.Log("[ModCommunicationSample.Other] Utils are unavailable. Not subscribed.");
		}

		public static void ThrottleDetectorUnsubscribe(Action<float> throttleChanged)
		{
			if (UtilApi != null)
			{
				UtilApi
					.GetMethod(nameof(ThrottleDetectorUnsubscribe), BindingFlags.Public | BindingFlags.Static)
					.Invoke(null, new object[] { throttleChanged });
				Debug.Log("[ModCommunicationSample.Other] Utils are available. Unsubscribed.");
			}
			else
				Debug.Log("[ModCommunicationSample.Other] Utils are unavailable. Not unsubscribed.");
		}

		#region

		private static readonly Type UtilApi =
			AssemblyLoader.loadedAssemblies
				.FirstOrDefault(a => a.name.Equals(AssemblyName, StringComparison.Ordinal))
				?.assembly
				?.GetType(ClassName);

		private const string AssemblyName = "ModCommunicationSample.Util";
		private const string ClassName = AssemblyName + ".UtilApi";

		#endregion
	}
}
