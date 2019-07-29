using System;
using UnityEngine;
using ClientCommon;

public class LabelTagManager : AbsManager<LabelTagManager>
{
    public override void Initialize(params object[] parameters)
    {
        base.Initialize(parameters);

        UILabel.onInit = OnLabelInit;
        UILabel.setParams = SetParams;
        UILabel.getParam = GetParam;
    }

    public string GetParam(object value, uint color)
    {
        if (color != default(uint))
        {
            return SetColor(value.ToString(), color);
        }
        return value.ToString();
    }

    public void SetParams(UILabel label, params object[] values)
    {
        if (label == null)
        {
            return;
        }

        if (label.useTagLabel)
        {
            if (!label.hasTagTextInited)
            {
                OnLabelInit(label, label.tagText);
                label.hasTagTextInited = true;
            }
        }

        if (string.IsNullOrEmpty(label.tagText))
        {
            label.runTimeTagText = "{0}";
        }

        label.text = string.Format(label.runTimeTagText, values);
        //因为读表不识别的问题，只能重新替换了
        label.text = label.text.Replace("[n]", "\n");
    }

    public void SetLabelTextByTextTag(UILabel label)
    {
        OnLabelInit(label, label.tagText);
    }

    private void OnLabelInit(UILabel label, string tagText)
    {
        if (!Application.isPlaying)
        {
            return;
        }

        if (label.useTagLabel)
        {
            if (label.tagText == "{0}")
            {
                label.runTimeTagText = label.tagText;
                label.hasTagTextInited = true;
                return;
            }

            if (!string.IsNullOrEmpty(label.tagText))
            {
#if UNITY_EDITOR
                AssertHelper.Check(tagText.Trim() == tagText, string.Format("TRIM! Label: {0} , used {1} as tagText, but it has \\r or \\n, {2}", label.name, label.tagText.Replace("{", "[").Replace("}", "]"), "designer please check it, please tell others"));
#endif
                Text textVo = ConfigDataBase.TextConfig.Get(tagText);

#if UNITY_EDITOR
                AssertHelper.Check(textVo != null && !string.IsNullOrEmpty(textVo.Content), string.Format("Label: {0} , used {1} as tagText, but can't get value from table, {2}", label.name, label.tagText.Replace("{", "[").Replace("}", "]"), "designer please check it, please tell others"));
#endif

                if (textVo != null)
                {
                    string content = textVo.Content;
                    if (!string.IsNullOrEmpty(content) && content != "Null")
                    {
                        label.runTimeTagText = content;
                        label.text = content;
                    }
                    else
                    {
                        label.runTimeTagText = label.tagText;
                        label.text = label.tagText;
                    }
                }
            }
            else
            {
                label.runTimeTagText = "{0}";
            }
        }

        label.hasTagTextInited = true;
    }
    
    /// <summary>
	/// 根据两个值进行比较，如果前者小于后者，前者变红，
	/// </summary>
	/// <param name="value"></param>
	/// <param name="needValue"></param>
	/// <returns></returns>
    public string GetNeedString(int needValue, int value)
    {
        string valueStr = value.ToString();
        if (value < needValue)
        {
            valueStr = UiUtility.GetTextColorString(value.ToString(), ColorType.Red);
        }
        return string.Format("{0}/{1}", valueStr, needValue);
    }

    public string CompareValueToRedColor(int value, int needValue)
    {
        string valueStr = value.ToString();
        if (value < needValue)
        {
            valueStr = string.Format("[f34500]{0}[-]", value);
        }

        return valueStr;
    }

    private string SetColor(string value, uint color)
    {
        return "[" + Convert.ToString(color, 16) + "]" + value + "[-]";
    }

}

