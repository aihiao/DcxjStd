using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClientCommon
{
    public class SplitColumnValueParser
    {
        /// <summary>
        /// 拆分范围值, 只支持整数形式。例如: 1~20, 把1~20加入到集合返回。
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<int> splitRangeElement(string value)
        {
            int index = value.IndexOf("~");
            if (index == -1)
            {
                return null;
            }

            try
            {
                string begin = value.Substring(0, index);
                string end = value.Substring(index + 1, value.Length - 1 - index);

                List<int> result = new List<int>();
                for (int i = int.Parse(begin); i <= int.Parse(end); i++)
                {
                    result.Add(i);
                }

                return result;
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }

            return null;
        }

        /// <summary>
        /// faction_tree_mission表的gain_data列: (1;0;41)
        /// faction_tree_mission表的rewards列: 920000001;20
        /// </summary>
        public List<string> splitDiffElement2List(string value)
        {
            List<string> result = new List<string>();
            int begin = 0;
            int level = 0;

            char[] charArr = value.ToCharArray();
            for (int i = 0; i < charArr.Length; i++)
            {
                if (charArr[i] == '(')
                {
                    level++;
                }
                else if (charArr[i] == ')')
                {
                    level--;
                }
                else if (charArr[i] == ';' && level == 0)
                {
                    begin = i + 1;
                    result.Add(value.Substring(begin, i - begin).Trim());
                }
            }

            string split = value.Substring(begin);
            if (!string.IsNullOrEmpty(split))
            {
                result.Add(split.Trim());
            }

            return result;
        }

        /// <summary>
        /// league_boss表的dungeon_weight列: 1;1|2;1|3;1|4;1|5;1|6;1
        /// league_boss表的ligeance_show列: 1|2|3|4|5|6|7|8|9|10|11|12
        /// league_boss表的activity_reward列: 900000001;10000|400000203;2000
        /// </summary>
        public List<string> splitMultiValue2List(string value)
        {
            List<string> result = new List<string>();
            int begin = 0;
            int level = 0;

            char[] charArr = value.ToCharArray();
            for (int i = 0; i < charArr.Length; i++)
            {
                if (charArr[i] == '(')
                {
                    level++;
                }
                else if (charArr[i] == ')')
                {
                    level--;
                }
                else if ((charArr[i] == '|' || charArr[i] == ',') && level == 0)
                {
                    string split = value.Substring(begin, i - begin);
                    begin = i + 1;
                    if (split.StartsWith("("))
                    {
                        if (split.Length < 2)
                        {
                            split = "";
                            Debug.LogError("Load data error: missing...");
                        }
                        else
                        {
                            split = split.Substring(1, split.Length - 2);
                        }
                    }
                    result.Add(split);
                }
            }

            string splitEnd = value.Substring(begin);
            if (!string.IsNullOrEmpty(splitEnd))
            {
                if (splitEnd.StartsWith("("))
                {
                    if (splitEnd.Length < 2)
                    {
                        splitEnd = "";
                        Debug.LogError("Load data error: missing...");
                    }
                    else
                    {
                        splitEnd = splitEnd.Substring(1, splitEnd.Length - 2);
                    }
                }
                result.Add(splitEnd.Trim());
            }

            return result;
        }


    }
}
