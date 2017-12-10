using UnityEngine;
using System.Collections;

public class UnitySingleton<T> : MonoBehaviour
	where T : Component
{
	private static T _instance;
	public static T Instance {
		get {
			if (_applicationIsQuitting) {
				Debug.LogWarning("Application is quitting cannot access Singleton '"+ typeof(T) +"'.");
				return null;
			}
			
			if (_instance == null) {
				
				T[] objs = FindObjectsOfType<T>();
				
				if (objs.Length > 0) {
					_instance = objs[0];
				}
				
				if (objs.Length > 1) {
					Debug.LogError ("There is more than one " + typeof(T).Name + " in the scene.");
				}
				
				if (_instance == null) {
					GameObject obj = new GameObject ();
					obj.hideFlags = HideFlags.HideAndDontSave;
					_instance = obj.AddComponent<T> ();
				}
			}
			return _instance;
		}
	}
	
	private static bool _applicationIsQuitting = false;

	public void OnApplicationQuit () {
		_applicationIsQuitting = true;
	}
}