using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Emoji configuration saving in UiEmojiSlot.
/// </summary>
[Serializable]
public class UiEmojiData
{
    // Each emoji refers an atlas. In case that the atlas is not big enough to hold all of emoji sprites of UiEmojiSlot
    public UIAtlas atlas = null;
    // Char sequence using in text resolving. Matched substring of text will be replaced by emoji.
    public string sequence = string.Empty;
    // This is used in case of small dialog, do not show emoji pictures, and show this text
    public string shortName = string.Empty;

    public List<string> spriteNames = new List<string>();

    public float updateDelta = 0;
    public float timeLength = 0;

    /**
	 * 命名约定:
	 * 
	 * 如果一个表情名为 "petrified" 有40帧，则各帧应该命名为
	 * petrified 0
	 * petrified 1
	 * petrified 2
	 * ...
	 * petrified 39
	 * 
	 * 即为 emoijName + 帧Id 的形式。
	 * 各帧的名称会根据emojiName和FrameCount自动生成
	 * 
	 */
    private string emojiName = string.Empty;
    public string EmojiName
    {
        get { return emojiName; }
        set
        {
            if (emojiName != value)
            {
                int count = spriteNames.Count;
                emojiName = value;

                spriteNames.Clear();
                for (int i = 0; i < count; i++)
                {
                    spriteNames.Add(emojiName + ' ' + i);
                }

                if (spriteNames.Count > 0)
                {
                    timeLength = spriteNames.Count * updateDelta;
                }
                else
                {
                    timeLength = 0;
                }
            }
        }
    }

    [SerializeField]
    int fps = 0;
    /// <summary>
    /// 帧率
    /// 测试返现gif似乎每一帧都可以指定持续时间，即这一帧可以持续1秒，而下一帧可以只持续0.1秒
    /// 当前实现仅支持各帧之间以相同的时间间隔播放。即设置FPS之后自动计算各帧时间间隔。
    /// </summary>
    public int FPS
    {
        get { return fps; }

#if UNITY_EDITOR
        set
        {
            if (fps != value)
            {
                fps = value;

                if (fps != 0)
                {
                    updateDelta = 1f / (float)fps;
                }
                else
                {
                    updateDelta = 0;
                }

                if (spriteNames.Count > 0)
                {
                    timeLength = spriteNames.Count * updateDelta;
                }
                else
                {
                    timeLength = 0;
                }
            }
        }
#endif
    }

    /// <summary>
    /// 帧数
    /// 根据帧数和帧率将自动计算播放周期
    /// </summary>
    public int SpriteCount
    {
        get
        {
            return spriteNames == null ? 0 : spriteNames.Count;
        }

#if UNITY_EDITOR
        set
        {
            if (spriteNames.Count != value)
            {
                spriteNames.Clear();

                for (int i = 0; i < value; i++)
                {
                    spriteNames.Add(emojiName + ' ' + i);
                }

                if (spriteNames.Count > 0)
                {
                    timeLength = spriteNames.Count * updateDelta;
                }
                else
                {
                    timeLength = 0;
                }
            }
        }
#endif
    }

    public bool IsValid
    {
        get
        {
            return atlas != null && (!string.IsNullOrEmpty(sequence)) && (!string.IsNullOrEmpty(emojiName)) && spriteNames != null && spriteNames.Count > 0;
        }
    }

    /// <summary>
    /// Check if this emoij is matched.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="offset"></param>
    /// <param name="textLength"></param>
    /// <returns></returns>
    public bool Match(string text, int offset, int textLength)
    {
        int emojiLength = sequence.Length;
        if (emojiLength == 0 || emojiLength > textLength - offset)
        {
            return false;
        }

        bool match = true;
        for (int c = 0; c < emojiLength; c++)
        {
            if (text[offset + c] != sequence[c])
            {
                match = false;
                break;
            }
        }
        if (match)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// The first sprite data of emoji is used.
    /// </summary>
    /// <returns></returns>
    public UISpriteData GetFirstSpriteData()
    {
        if (!IsValid)
        {
            return null;
        }
        return atlas.GetSprite(spriteNames[0]);
    }

#if UNITY_EDITOR
    /// <summary>
    /// Check If all UVFrame is valid in atlas
    /// </summary>
    /// <returns></returns>
    public bool ValidateAllSprite()
    {
        if (!IsValid)
        {
            LoggerManager.Instance.Error("[UiEmojiData] emoji is invalid. emojiName={0}", emojiName);
            return false;
        }

        foreach (string emojiSpriteName in spriteNames)
        {
            if (atlas.GetSprite(emojiSpriteName) == null)
            {
                LoggerManager.Instance.Error("[UiEmojiData][Atlas={0} emojiSpriteName={1} emojiName={2}", atlas.name, emojiSpriteName, emojiName);
                return false;
            }
        }
        return true;
    }
#endif

}

/// <summary>
/// The class that holds all of emoji settings.
/// </summary>
public class UiEmojiSlot : MonoBehaviour
{
    public List<UiEmojiData> emojis;

    public bool IsValid
    {
        get
        {
            return emojis != null && emojis.Count > 0;
        }
    }

    /// <summary>
    /// Match emoji in this UiEmojiSlot
    /// </summary>
    /// <param name="text"></param>
    /// <param name="offset"></param>
    /// <param name="textLength"></param>
    /// <returns></returns>
    public UiEmojiData MatchEmoji(string text, int offset, int textLength)
    {
        if (emojis == null || emojis.Count == 0)
        {
            return null;
        }

        foreach (var emoji in emojis)
        {
            if (emoji.Match(text, offset, textLength))
            {
                return emoji;
            }
        }

        return null;
    }

}
