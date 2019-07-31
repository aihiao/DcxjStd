namespace Combat
{
#if COMBAT_CLIENT
	class ParticleAbilityParticleController : UnityEngine.MonoBehaviour
	{
		ParticleSystemSettings config;
		//UnityEngine.GameObject particle;
		public void Set(UnityEngine.GameObject particle, ParticleSystemSettings config, bool isPosition)
		{
			this.config = config;
			//this.particle = particle;
			initPosition = particle.transform.position;
			initEuler = particle.transform.eulerAngles;
			this.isPositionEffect = isPosition;
		}

		UnityEngine.Vector3 initPosition = default(UnityEngine.Vector3);
		UnityEngine.Vector3 initEuler = default(UnityEngine.Vector3);
		bool isPositionEffect = false;

		void Update()
		{
			if (!isPositionEffect)
				MangeTransform();
		}

		void LateUpdate()
		{
			if (!isPositionEffect)
				MangeTransform();
		}

		void MangeTransform()
		{
			if (config.maintainPosition)
				transform.position = initPosition;
			else if (config.useLocalRelativePosition == false)
				transform.position = transform.parent.position + config.placementWorldRelativePosition;

			if (config.maintainFacing)
				transform.eulerAngles = initEuler;
		}
	}
#endif
}
