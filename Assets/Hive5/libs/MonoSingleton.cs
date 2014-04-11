using UnityEngine;
using System.Collections;

namespace Hive5 
{
	/// <summary>
	/// Mono singleton.
	/// </summary>
	public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;
		
		private static object _lock = new object();

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static T Instance
		{
			get
			{
				if (applicationIsQuitting) {
					Debug.LogWarning("[MonoSingleton] Instance '"+ typeof(T) +
					                 "' already destroyed on application quit." +
					                 " Won't create again - returning null.");
					return null;
				}
				
				lock(_lock)
				{
					if (_instance == null)
					{
						_instance = (T) FindObjectOfType(typeof(T));
						
						if ( FindObjectsOfType(typeof(T)).Length > 1 )
						{
							Debug.LogError("[MonoSingleton] Something went really wrong " +
							               " - there should never be more than 1 singleton!" +
							               " Reopenning the scene might fix it.");
							return _instance;
						}
						
						if (_instance == null)
						{
							GameObject singleton = new GameObject();
							_instance = singleton.AddComponent<T>();
							singleton.name = "(MonoSingleton) "+ typeof(T).ToString();
							
							DontDestroyOnLoad(singleton);
							
							Debug.Log("[MonoSingleton] An instance of " + typeof(T) + 
							          " is needed in the scene, so '" + singleton +
							          "' was created with DontDestroyOnLoad.");
						} else {
	//						Debug.Log("[MonoSingleton] Using instance already created: " +
	//						          _instance.gameObject.name);
						}
					}
					
					return _instance;
				}
			}
		}
		
		private static bool applicationIsQuitting = false;

		public void OnDestroy () {
			applicationIsQuitting = true;
		}
	}
}