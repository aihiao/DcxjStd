using System.Text;
using System.Reflection;
using UnityEngine;
using LywGames.Effect;

namespace LywGames
{
	/// <summary>
	/// 动画事件响应器, 可以用于响应Animation上绑定的时间, 也可以通过SendMessage直接函数调用
	/// 每个事件都对应一个ParameterBuilder定义了事件的参数, 用于构造和解析参数
	/// </summary>
	public class AnimationEventHandler : MonoBehaviour
	{
		public static IGameplayFunctionCall AnimEntStaticGameplayCaller = null;

		public class ParameterBuilder
		{
			protected static string BuildParameter(params object[] parameters)
			{
				StringBuilder sb = null;
				for (int i = 0; i < parameters.Length; ++i)
					sb = AppendParameter(sb, parameters[i]);
				return sb != null ? sb.ToString() : "";
			}

			private static StringBuilder AppendParameter(StringBuilder sb, object param)
			{
				if (sb == null)
					sb = new StringBuilder();
				else
					sb.Append(",");

				sb.Append(param.ToString());

				return sb;
			}
		}

		#region 播放音效事件
		public class PlaySoundParameter : ParameterBuilder
		{
			public static string Build(string soundName, float volume, float delay)
			{
				return BuildParameter(soundName, volume, delay);
			}

			public static void Parse(string parameter, out string soundName, out float volume, out float delay)
			{
				int idx = 0;
				string[] parameters = parameter.Split(',');
				soundName = StrParser.ParseStr(parameters.Length > idx ? parameters[idx] : null, ""); ++idx;
				volume = StrParser.ParseFloat(parameters.Length > idx ? parameters[idx] : null, 1f); ++idx;
				delay = StrParser.ParseFloat(parameters.Length > idx ? parameters[idx] : null, 0f); ++idx;
			}
		}

		[Obfuscation(Exclude = true, Feature = "renaming")]
		public void PlaySound(string parameter)
		{
			string soundName;
			float volume;
			float delay;
			PlaySoundParameter.Parse(parameter, out soundName, out volume, out delay);
			//	AudioManager.Instance.PlaySound(soundName, volume: volume, delay: delay);
			AnimEntStaticGameplayCaller.PlaySound(soundName, volume: volume, delay: delay);
		}
		#endregion

		#region 播放音乐事件
		public class PlayMusicParameter : ParameterBuilder
		{
			public static string Build(string audioName, float volume, float delay)
			{
				return BuildParameter(audioName, volume, delay);
			}

			public static void Parse(string parameter, out string audioName, out float delay, out float fadeTime)
			{
				int idx = 0;
				string[] parameters = parameter.Split(',');
				audioName = StrParser.ParseStr(parameters.Length > idx ? parameters[idx] : null, ""); ++idx;
				delay = StrParser.ParseFloat(parameters.Length > idx ? parameters[idx] : null, 1f); ++idx;
				fadeTime = StrParser.ParseFloat(parameters.Length > idx ? parameters[idx] : null, 0f); ++idx;
			}
		}

		[Obfuscation(Exclude = true, Feature = "renaming")]
		public void PlayMusic(string parameter)
		{
			string audioName;
			float delay;
			float fadeTime;
			PlayMusicParameter.Parse(parameter, out audioName, out delay, out fadeTime);
			if (AnimEntStaticGameplayCaller.IsMusicPlaying(audioName) == false)
				AnimEntStaticGameplayCaller.PlayMusic(audioName, true, delay, fadeTime: fadeTime);
		}
		#endregion

		#region 停止音乐事件
		public class StopMusicParameter : ParameterBuilder
		{
			public static string Build(float fadeTime)
			{
				return BuildParameter(fadeTime);
			}

			public static void Parse(string parameter, out float fadeTime)
			{
				int idx = 0;
				string[] parameters = parameter.Split(',');
				fadeTime = StrParser.ParseFloat(parameters.Length > idx ? parameters[idx] : null, 0f); ++idx;
			}
		}

		[Obfuscation(Exclude = true, Feature = "renaming")]
		public void StopMusic(string parameter)
		{
			float fadeTime;
			StopMusicParameter.Parse(parameter, out fadeTime);
			AnimEntStaticGameplayCaller.StopMusic(fadeTime);
		}
		#endregion

		[Obfuscation(Exclude = true, Feature = "renaming")]
		public void StepMusicToNormalState(string parameter)
		{
			AnimEntStaticGameplayCaller.StepMusicToNormalState(parameter);
		}

		#region 改变动画速度事件
		public class SetAnimationSpeedParameter : ParameterBuilder
		{
			public static string Build(float speed)
			{
				return BuildParameter(speed);
			}

			public static void Parse(string parameter, out float speed)
			{
				int idx = 0;
				string[] parameters = parameter.Split(',');
				speed = StrParser.ParseFloat(parameters.Length > idx ? parameters[idx] : null, 1f); ++idx;
			}
		}

		[Obfuscation(Exclude = true, Feature = "renaming")]
		public void SetAnimationSpeed(string parameter)
		{
			float speed;
			SetAnimationSpeedParameter.Parse(parameter, out speed);
			var animation = gameObject.GetComponent<Animation>();
			foreach (AnimationState animState in animation)
				if (animation.IsPlaying(animState.name))
					animState.speed = speed;
		}
		#endregion

		#region 振动主摄像机
		public class ShakeMainCameraParameter : ParameterBuilder
		{
			public static string Build(float intensity, float duration, float interval)
			{
				return BuildParameter(intensity, duration, interval);
			}

			public static void Parse(string parameter, out float intensity, out float duration, out float interval, out bool shakeOnlyVisible)
			{
				int idx = 0;
				string[] parameters = parameter.Split(',');
				intensity = StrParser.ParseFloat(parameters.Length > idx ? parameters[idx] : null, 1f); ++idx;
				duration = StrParser.ParseFloat(parameters.Length > idx ? parameters[idx] : null, 1f); ++idx;
				interval = StrParser.ParseFloat(parameters.Length > idx ? parameters[idx] : null, 1f); ++idx;
				shakeOnlyVisible = StrParser.ParseBool(parameters.Length > idx ? parameters[idx] : null, false); ++idx;
			}
		}

		[Obfuscation(Exclude = true, Feature = "renaming")]
		public void ShakeMainCamera(string parameter)
		{
			AnimEntStaticGameplayCaller.ShakeMainCamera(parameter);
		}
		#endregion

		#region 振动物体本身
		public class ShakeSelfParameter : ParameterBuilder
		{
			public static string Build(float intensity, float duration, float interval)
			{
				return BuildParameter(intensity, duration, interval);
			}

			public static void Parse(string parameter, out float intensity, out float duration, out float interval)
			{
				int idx = 0;
				string[] parameters = parameter.Split(',');
				intensity = StrParser.ParseFloat(parameters.Length > idx ? parameters[idx] : null, 1f); ++idx;
				duration = StrParser.ParseFloat(parameters.Length > idx ? parameters[idx] : null, 1f); ++idx;
				interval = StrParser.ParseFloat(parameters.Length > idx ? parameters[idx] : null, 1f); ++idx;
			}
		}

		[Obfuscation(Exclude = true, Feature = "renaming")]
		public void ShakeSelf(string parameter)
		{
			float intensity, duration, interval;
			ShakeSelfParameter.Parse(parameter, out intensity, out duration, out interval);
			CameraShaker.Shake(this.gameObject, intensity, duration, interval, sourceObject: this.gameObject);
		}
		#endregion

		#region 播放特效

		public class PlayFxParameter : ParameterBuilder
		{
			public static string Build(string effectName, string parentGameObjectName)
			{
				return BuildParameter(effectName, parentGameObjectName);
			}

			public static void Parse(string parameter, out string effectName, out string parentGameObjectName)
			{
				int idx = 0;
				string[] parameters = parameter.Split(',');
				effectName = StrParser.ParseStr(parameters.Length > idx ? parameters[idx] : null, ""); ++idx;
				parentGameObjectName = StrParser.ParseStr(parameters.Length > idx ? parameters[idx] : null, ""); ++idx;
			}
		}


		[Obfuscation(Exclude = true, Feature = "renaming")]
		public void PlayFx(string parameter)
		{
			AnimEntStaticGameplayCaller.PlayFx(parameter);
		}
		#endregion

		#region PlayAbilityPfx

		[Obfuscation(Exclude = true, Feature = "renaming")]
		public void PlayAbilityPfx(string xmlParameter)
		{
			AnimEntStaticGameplayCaller.PlayAbilityPfx(xmlParameter, this.gameObject);
		}

		#endregion

		#region 自动销毁
		[Obfuscation(Exclude = true, Feature = "renaming")]
		public void AutoDestroy()
		{
			GameObject.Destroy(this.gameObject);
		}
		#endregion

		#region 自定义事件
		public delegate void UserEventDelegate(string eventName, object userData);
		public UserEventDelegate userEventDelegate;
		public object userData;

		[Obfuscation(Exclude = true, Feature = "renaming")]
		public void UserDefinedFunction(string eventName)
		{
			if (userEventDelegate != null)
			{
				userEventDelegate(eventName, userData);
			}
		}
		#endregion

	}
}
