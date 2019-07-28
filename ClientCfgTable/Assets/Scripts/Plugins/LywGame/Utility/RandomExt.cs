using UnityEngine;
using System.Collections.Generic;

namespace LywGames
{
	/// <summary>
	/// ExtRandom is an extension of the Unity class Random. 
	/// Its main purpose is to automate the common operations which implement the Random class.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class RandomExt<T>
	{
		/// <summary>
		/// This method sets a random seed for the RNG using 2 convoluted formulas. 
		/// Using this method at any time other than during downtime is not recommended. 
		/// This method will never see use 99.9% of the time, but is available in cases where a truly random seed is required.
		/// </summary>
		public static void RandomizeSeed()
		{
			
			int sub = ((int)System.DateTime.Now.Day - (int)System.DateTime.Now.DayOfWeek * System.DateTime.Now.DayOfYear);

			if (sub < 0)
				sub = -sub;
			if (sub == 0)
				sub = 1;

			try
			{

				//// 24 1 236				Debug.Log(string.Format("randomExt  : {0} {1} {2}", (int)System.DateTime.Now.Day, (int)System.DateTime.Now.DayOfWeek, System.DateTime.Now.DayOfYear));
				//int v1 = ((int)(System.DateTime.Now.Ticks % 2147483648L) - (int)(Time.realtimeSinceStartup + 2000f)) / sub;

				//Debug.LogWarning(v1);

				//Random.seed = System.Math.Abs(v1);

				//float v2 = ((Random.value * (float)System.DateTime.Now.Ticks * (float)Random.Range(0, 2)) + (Random.value * Time.realtimeSinceStartup * Random.Range(1f, 3f))) + 1;
				//long v3 = (long)v2;

				//Debug.LogWarning(v3);

				//Random.seed = System.Math.Abs((int)(v3 % 2147483648L));

				int v1 = ((int)(System.DateTime.Now.Ticks % 2147483648L) - (int)(Time.realtimeSinceStartup * 100000 + 2000f)) / sub;
				Random.seed = System.Math.Abs(v1);
				float v2 = (Random.value * Time.realtimeSinceStartup + Random.value * System.DateTime.Now.Ticks) % 2147483648L;
				long v3 = (long)v2;
				Random.seed = System.Math.Abs((int)(v3 % 2147483648L));


			}
			catch(System.Exception ex)
			{
				Debug.Log(ex.ToString());
			}
			
		}

		public static float Chance(float inCLudeMin, int excludeMax)
		{
			return Random.Range(inCLudeMin, excludeMax);
		}

		/// <summary>
		/// This method returns either true or false with an equal chance.
		/// </summary>
		public static bool SplitChance()
		{
			return Random.Range(0, 2) == 0 ? true : false;
		}

		/// <summary>
		/// This method returns either true or false with the chance of the former derived from the parameters passed to the method.
		/// </summary>
		public static bool Chance(int nProbabilityFactor, int nProbabilitySpace)
		{
			return Random.Range(0, nProbabilitySpace) < nProbabilityFactor ? true : false;
		}

		/// <summary>
		/// This method returns a random element chosen from an array of elements.
		/// </summary>
		public static T Choice(T[] array)
		{
			return array[Random.Range(0, array.Length)];
		}

		/// <summary>
		/// This method returns a random element chosen from a list of elements.
		/// </summary>
		public static T Choice(List<T> list)
		{
			return list[Random.Range(0, list.Count)];
		}

		public static int GetChoiceId(List<T> list)
		{
			return Random.Range(0, list.Count);
		}

		/// <summary>
		/// This method returns a random element chosen from an array of elements based on the respective weights of the elements.
		/// </summary>
		public static T WeightedChoice(T[] array, int[] nWeights)
		{
			int nTotalWeight = 0;
			for (int i = 0; i < array.Length; i++)
			{
				nTotalWeight += nWeights[i];
			}
			int nChoiceIndex = Random.Range(0, nTotalWeight);
			for (int i = 0; i < array.Length; i++)
			{
				if (nChoiceIndex < nWeights[i])
				{
					nChoiceIndex = i;
					break;
				}
				nChoiceIndex -= nWeights[i];
			}

			return array[nChoiceIndex];
		}

		/// <summary>
		/// This method returns a random element chosen from a list of elements based on the respective weights of the elements.
		/// </summary>
		public static T WeightedChoice(List<T> list, int[] nWeights)
		{
			int nTotalWeight = 0;
			for (int i = 0; i < list.Count; i++)
			{
				nTotalWeight += nWeights[i];
			}
			int nChoiceIndex = Random.Range(0, nTotalWeight);
			for (int i = 0; i < list.Count; i++)
			{
				if (nChoiceIndex < nWeights[i])
				{
					nChoiceIndex = i;
					break;
				}
				nChoiceIndex -= nWeights[i];
			}

			return list[nChoiceIndex];
		}

		/// <summary>
		/// This method rearranges the elements of an array randomly and returns the rearranged array.
		/// </summary>
		public static T[] Shuffle(T[] array)
		{
			T[] shuffledArray = new T[array.Length];
			List<int> elementIndices = new List<int>(0);
			for (int i = 0; i < array.Length; i++)
			{
				elementIndices.Add(i);
			}
			int nArrayIndex;
			for (int i = 0; i < array.Length; i++)
			{
				nArrayIndex = elementIndices[Random.Range(0, elementIndices.Count)];
				shuffledArray[i] = array[nArrayIndex];
				elementIndices.Remove(nArrayIndex);
			}

			return shuffledArray;
		}

		/// <summary>
		/// This method rearranges the elements of a list randomly and returns the rearranged list.
		/// </summary>
		public static List<T> Shuffle(List<T> list)
		{
			List<T> shuffledList = new List<T>(0);
			int nListCount = list.Count;
			int nElementIndex;
			for (int i = 0; i < nListCount; i++)
			{
				nElementIndex = Random.Range(0, list.Count);
				shuffledList.Add(list[nElementIndex]);
				list.RemoveAt(nElementIndex);
			}

			return shuffledList;
		}
	}
}
