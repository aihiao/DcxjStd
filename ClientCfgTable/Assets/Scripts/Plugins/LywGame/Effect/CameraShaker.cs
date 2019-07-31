using System.Collections;
using UnityEngine;

namespace LywGames.Effect
{
	/// <summary>
	/// Camera shake class, to provide shaking camera effect. 
	/// </summary>
	public class CameraShaker : MonoBehaviour
	{
		private bool autoDestroy;
		public bool AutoDestroy
		{
			get { return autoDestroy; }
			set { autoDestroy = value; }
		}

		private bool destroyGameObject;
		public bool DestroyGameObject
		{
			get { return destroyGameObject; }
			set { destroyGameObject = value; }
		}

		const string PARENT_NAME = "CAMERA_SHAKER";

		// Camera shake.
		protected Camera cam;
		protected float camShkTime; // Shake time.
		protected float camShkDrt; // Shake duration.
		protected float camShkLastTime;
		protected float camShkInty; // Shake intensity.
		protected float camShkIntv; // Shake interval.
		protected float camShkDelay;//ShakeStartDelayTime;
		protected Vector3 camShkOfs = Vector3.zero;
		protected Vector3 camShkStart; // Camera shake start position.
		protected bool shakeOnlyVisible = true;

		public static void Shake(GameObject gameObject, float intensity, float duration, float interval, float delay = 0, bool shakeOnlyVisible = true, GameObject sourceObject = null)
		{
			// 震动父节点而不是相机本身，因为相机同时要跟随人移动，会造成坐标混乱，画面闪屏
			if (gameObject.transform.parent == null || gameObject.transform.parent.name != PARENT_NAME)
			{
				var shakerParent = new GameObject(PARENT_NAME);
				shakerParent.transform.parent = gameObject.transform.parent;
				shakerParent.transform.localPosition = gameObject.transform.localPosition;
				shakerParent.transform.localScale = gameObject.transform.localScale;
				shakerParent.transform.localRotation = gameObject.transform.localRotation;

				ObjectUtility.AttachToParentAndResetLocalTrans(shakerParent, gameObject);
			}

			var shakerParentObj = gameObject.transform.parent.gameObject;

			var cameraShaker = shakerParentObj.GetComponent<CameraShaker>();
			if (cameraShaker == null)
			{
				cameraShaker = shakerParentObj.AddComponent<CameraShaker>();
				cameraShaker.AutoDestroy = true;
			}

			if (shakeOnlyVisible && sourceObject != null)
			{
				Vector2 viewPortPoint = gameObject.GetComponent<Camera>().WorldToViewportPoint(sourceObject.transform.position);
				if (viewPortPoint.x < 0 || viewPortPoint.x > 1 || viewPortPoint.y < 0 || viewPortPoint.y > 1)
					return;
			}

			cameraShaker.Shake(intensity, duration, interval, delay, shakeOnlyVisible);
		}

		// Shake with intensity, duration and interval.
		public void Shake(float intensity, float duration, float interval, float delay, bool shakeOnlyVisible)
		{
			if (intensity == 0)
				return;

			camShkStart = transform.localPosition;
			camShkDrt = duration;
			camShkInty = intensity;
			camShkIntv = interval;
			camShkDelay = delay;

			StopShake();

			StartCoroutine("UpdateShake");
		}

		private void StopShake()
		{
			transform.localPosition = camShkStart;

			StopCoroutine("UpdateShake");
		}

		// Adjust camera shake position.
		public void AdjustShkStart(Vector3 delta)
		{
			camShkStart += delta;
		}

		// Shake coroutine function.
		[System.Reflection.Obfuscation(Exclude = true, Feature = "renaming")]
		protected IEnumerator UpdateShake()
		{
			if (camShkDelay > 0)
				yield return new WaitForSeconds(camShkDelay);

			camShkLastTime = camShkTime = camShkDrt;

			if (camShkDelay > 0)
			{
				//Re Record startPosition

				camShkStart = transform.localPosition;
			}

			// Adjust camera position during shaking time.
			Transform cachedTrans = transform;
			while (camShkTime > 0)
			{
				camShkTime -= Time.deltaTime;

				if (camShkTime <= 0)
				{
					camShkTime = 0;
					camShkOfs = Vector3.zero;
					continue;
				}

				if (camShkLastTime - camShkTime > camShkIntv)
				{
					float r = camShkInty * (camShkTime / camShkDrt);

					float quickUp = Random.Range(-r, r);
					float quickLeft = Random.Range(-r, r);

					camShkOfs = quickUp * transform.up + quickLeft * transform.right;
					camShkLastTime = camShkTime;

					cachedTrans.localPosition = camShkStart + camShkOfs;
				}

				yield return null;
			}

			cachedTrans.localPosition = camShkStart;

			OnShakeFinished();
		}

		public void ForceToFinished()
		{
			transform.localPosition = camShkStart;
			OnShakeFinished();
		}

		private void OnShakeFinished()
		{
			// 不销毁性能更好
			//if (autoDestroy)
			//{
			//	if (destroyGameObject)
			//	{
			//		foreach (var child in GetComponentsInChildren<Transform>())
			//			if (child.parent == this.transform)
			//				child.parent = transform.parent;

			//		Object.Destroy(this.gameObject);
			//	}
			//	else
			//	{
			//		Object.Destroy(this);
			//	}
			//}
		}
	}
}
