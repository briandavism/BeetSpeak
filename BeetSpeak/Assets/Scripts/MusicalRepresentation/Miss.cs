using UnityEngine;
using UnityEngine.UI;

namespace MusicalRepresentation
{
	public class Miss : MonoBehaviour
	{
		public Image missIcon;
	
		private float _hitTime;

		private void Awake()
		{
			missIcon.enabled = false;
		}

		public void Init(float hitTime, Color color)
		{
			_hitTime = hitTime;
			missIcon.enabled = true;
			missIcon.color = color;
		}
	}
}

