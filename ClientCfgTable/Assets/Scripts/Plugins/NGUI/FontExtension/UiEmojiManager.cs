#define HIDE_EMOJIS

using System;
using System.Collections.Generic;
using UnityEngine;

// EmojiDataHolder for label building. Contains data for emoji management.
public class RunTimeEmoji
{
    // Sprite which shows the UVFrame.
    public UISprite emojiDynamicSprite = null;
    // Using the first uv frame data as default data. (UV position, emoji dimensions)
    public UISpriteData sprite0Data;
    public Color colorTint = Color.white;
    // Emoji Configuration saving in UiEmoijSlot.
    public UiEmojiData emojiData;
  
    // Current UV frame index
    public int currentFrame = 0;
    // the time when uv animation is started.
    public float realStartTime = 0;
    // bottom left Vertex Position of UISprite.(set by UILabel)
    public Vector2 localBottomLeft = default(Vector2);
    // top right Vertex Position of UISprite.(set by UILabel)
    public Vector2 localTopRight = default(Vector2);
   
    // Called when pooled.
    public void ResetValueData()
    {
        currentFrame = 0;
        realStartTime = 0;
        localBottomLeft = default(Vector2);
        localTopRight = default(Vector2);
    }
}

/**
 * 已实现的功能点：
 * Label重构时播放不被打断
 * 文本Overflow是的4中处理方式支持
 * spacingX，spacingY （行距、字句调整）支持
 * Label Pivot支持
 * Label Align样式 Left，Center，Right 支持
 * 表情ColorTint支持（Label的Symbol样式为Colored）
 * 池缓存（目前每个Label使用自己的池）
 * 文本测量支持（NGUIText.CalculatePrintedSize()、NGUIText.PrintApproximateCharacterPositions等）
 * 
 * //未实现的功能点：
 *  Label Align样式 Justified不支持
 */
public class UiEmojiManager
{
    //bug hack. See OnEmoijSpriteWillSaveEditor
    public const string emojiTag = "Dynamic Emoji Sprite DontSave";
   
    //emojiPool
    public List<RunTimeEmoji> emojiPool = new List<RunTimeEmoji>();
    //emojis showing in label.
    public List<RunTimeEmoji> showingEmojis = new List<RunTimeEmoji>();
    //Prepared but haven't rendered emojis,Used for label building
    public List<RunTimeEmoji> preparedEmojis = new List<RunTimeEmoji>();
    //Label pivot layout support
    BetterList<Vector3> emojiLayoutTemp = new BetterList<Vector3>();
    
    //The label using this UIEmojiManager
    UILabel label;
    //Indicates whether the label is actually rebuilding itself.
    bool postbuild = false;

    public UiEmojiManager(UILabel label)
    {
        if (label == null)
        {
            throw new ArgumentException("label can not be null");
        }
        this.label = label;
    }

    //First step.  Prepare emojiManager for label rebuiding.
    public void BeforeMatchEmoji()
    {
        postbuild = true;
    }

    //Second Step:
    //When label is rebuilding, NGUIText processes every character of text one by one and check if a symbol or emoji matched.
    //If an emoji is matched, NGUIText will replace the matched string sequence with the matched symbol or emoji.
    //Here we can obtain the "current position" information in current label building progress(these values are localBottomLeft,localTopRight,colorTint).
    public RunTimeEmoji MatchEmoji(string text, int offset, int textLength)
    {
        if (!label.HasDynamicEmoji)
        {
            return null;
        }

        //Reuse showing emojis.
        //The most important thing is that the playing progress of emoji uv animation MUST NOT be broken after label is rebuilt.
        RunTimeEmoji matched = null;

        //Seek from showingEmojis,If matched, it will keep active and the UV playing progress will not be broken.
        for (int i = 0; i < showingEmojis.Count; i++)
        {
            var runTimeEmoji = showingEmojis[i];
            if (runTimeEmoji.emojiData.Match(text, offset, textLength))
            {
                matched = runTimeEmoji;
                showingEmojis.Remove(runTimeEmoji);
                break;
            }
        }

        //Seek from pool
        if (matched == null)
        {
            for (int i = 0; i < emojiPool.Count; i++)
            {
                var runTimeEmoji = emojiPool[i];
                if (runTimeEmoji.emojiData.Match(text, offset, textLength))
                {
                    matched = runTimeEmoji;
                    emojiPool.Remove(runTimeEmoji);
                    break;
                }
            }
        }

        if (matched == null)
        {
            //The emoji haven't been created.
            //Create new runtime emoji.
            UiEmojiData matchedEmojiData = label.EmojiSlot.MatchEmoji(text, offset, textLength);
            if (matchedEmojiData == null)
            {
                return null;
            }

#if UNITY_EDITOR
            //if sprite uvAnimation contains invalid configuration.
            if (!matchedEmojiData.ValidateAllSprite())
            {
                return null;
            }
#endif

            if (matchedEmojiData == null || (!matchedEmojiData.IsValid))
            {
                return null;
            }

            matched = new RunTimeEmoji();
            matched.emojiData = matchedEmojiData;
            matched.sprite0Data = matchedEmojiData.GetFirstSpriteData();
        }

        return matched;
    }

    //Step 3:
    //Records all the matched or unmatched emojis for post processing
    public void AddPreparedEmoji(RunTimeEmoji matched)
    {
        preparedEmojis.Add(matched);
    }

    //Step 4: Postprocess
    //Build all emoji UISprites after the building of ALL WIDGETS of the parent panel of label is complete.
    //When panel is building widgets it can not record the widgets created in building. But when building label, emojis(Sprites) will be created.
    public void PostLabelBuilding()
    {
        // Must be called ONLY if label was actually rebuilt.
        if (!postbuild)
        {
            return;
        }
        postbuild = false;

        foreach (var matched in preparedEmojis)
        {
            if (matched.emojiDynamicSprite != null)
            {
                matched.emojiDynamicSprite.color = matched.colorTint;
                continue;
            }

            //Create new emoji.
            var matchedEmojiData = matched.emojiData;
#if UNITY_EDITOR
            //Bug?..........: http://answers.unity3d.com/questions/609621/hideflagsdontsave-causes-checkconsistency-transfor.html
#if HIDE_EMOJIS
            GameObject newSprite = UnityEditor.EditorUtility.CreateGameObjectWithHideFlags("emoji " + matchedEmojiData.EmojiName, HideFlags.None);
#else
            GameObject newSprite = UnityEditor.EditorUtility.CreateGameObjectWithHideFlags("emoji " + matchedEmojiData.EmojiName, HideFlags.DontSave); 
#endif
            //bug hack. See class OnEmoijSpriteWillSaveEditor
            newSprite.tag = emojiTag;
#else
            GameObject newSprite = new GameObject("emoji " + matchedEmojiData.EmojiName);
#endif

            newSprite.layer = label.cachedGameObject.layer;
            UISpriteData sd = matchedEmojiData.atlas.GetSprite(matchedEmojiData.spriteNames[0]);
            UISprite sprite = newSprite.AddComponent<UISprite>();
            sprite.type = (sd == null || (!sd.hasBorder)) ? UISprite.Type.Simple : UISprite.Type.Sliced;
            sprite.atlas = matchedEmojiData.atlas;
            //Sync depth.
            sprite.depth = label.depth;
            //Use first UV Frame
            sprite.spriteName = matchedEmojiData.spriteNames[0];
            //Apply color tint of label
            sprite.color = matched.colorTint;
            //sprite created.
            matched.emojiDynamicSprite = sprite;

            //AttachToLabel as a child. Movement,Rotation,Scaling of label supported.	
            matched.emojiDynamicSprite.cachedTransform.parent = label.cachedTransform;
            matched.emojiDynamicSprite.cachedTransform.localScale = Vector3.one;
            matched.emojiDynamicSprite.cachedTransform.localPosition = Vector3.zero;
            matched.emojiDynamicSprite.cachedTransform.localRotation = Quaternion.identity;
        }

        //Pool unRused showEmojis. 
        foreach (var runtimeEmoji in showingEmojis)
        {
            runtimeEmoji.ResetValueData();
            runtimeEmoji.emojiDynamicSprite.cachedGameObject.SetActive(false);
            emojiPool.Add(runtimeEmoji);
        }

        showingEmojis.Clear();
        //Get Current Valid Emojis.
        showingEmojis.AddRange(preparedEmojis);
        preparedEmojis.Clear();

        //Label pivot layout calculation
        emojiLayoutTemp.Clear();

        for (int i = 0; i < showingEmojis.Count; i++)
        {
            emojiLayoutTemp.Add((showingEmojis[i].localBottomLeft + showingEmojis[i].localTopRight) / 2f);
        }
        //Apply pivot layout
        label.ApplyOffset(emojiLayoutTemp, 0);

        for (int i = 0; i < showingEmojis.Count; i++)
        {
            var runtimeEmoji = showingEmojis[i];
            if (runtimeEmoji.emojiDynamicSprite.cachedTransform.parent != label.cachedTransform)
            {
                runtimeEmoji.emojiDynamicSprite.cachedTransform.parent = label.cachedTransform;
                runtimeEmoji.emojiDynamicSprite.cachedTransform.localScale = Vector3.one;
                runtimeEmoji.emojiDynamicSprite.cachedTransform.localRotation = Quaternion.identity;
            }
            runtimeEmoji.emojiDynamicSprite.cachedTransform.localPosition = emojiLayoutTemp[i];

            // pooled is inactive.
            runtimeEmoji.emojiDynamicSprite.gameObject.SetActive(true);
            runtimeEmoji.emojiDynamicSprite.CreatePanel();
            runtimeEmoji.emojiDynamicSprite.SetDimensions((int)(runtimeEmoji.localTopRight.x - runtimeEmoji.localBottomLeft.x), (int)(runtimeEmoji.localTopRight.y - runtimeEmoji.localBottomLeft.y));
        }
    }

    //Called Every Frame
    //Update UVAnimation by realtime other than OnUpdate(depending on game frame rate)	
    public void OnEmojiUpdate()
    {
        RunTimeEmoji tempEmoji;
        for (int i = 0; i < showingEmojis.Count; i++)
        {
            tempEmoji = showingEmojis[i];
            if (tempEmoji.emojiData.SpriteCount == 0 || tempEmoji.emojiData.timeLength == 0 || tempEmoji.emojiData.updateDelta == 0)
            {
                continue;
            }

            if (tempEmoji.realStartTime == 0)
            {
                tempEmoji.realStartTime = RealTime.time;
            }

            //loop progress
            float playingProgress = (RealTime.time - tempEmoji.realStartTime) % tempEmoji.emojiData.timeLength;
            //calculate frameIndex.
            tempEmoji.currentFrame = (int)(Mathf.Clamp01(playingProgress / tempEmoji.emojiData.timeLength) * tempEmoji.emojiData.SpriteCount);
            //Display current frame.
            tempEmoji.emojiDynamicSprite.spriteName = tempEmoji.emojiData.spriteNames[tempEmoji.currentFrame];
        }
    }

    /// <summary>
    /// Release all emojis
    /// </summary>
    public void Destroy()
    {
        foreach (var temp in emojiPool)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                GameObject.DestroyImmediate(temp.emojiDynamicSprite.gameObject);
            }
            else
            {
#endif
                GameObject.Destroy(temp.emojiDynamicSprite.gameObject);
            }
        }

        foreach (var temp in showingEmojis)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                GameObject.DestroyImmediate(temp.emojiDynamicSprite.gameObject);
            }
            else
            {
#endif
                GameObject.Destroy(temp.emojiDynamicSprite.gameObject);
            }
        }

        foreach (var temp in preparedEmojis)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
            {
                GameObject.DestroyImmediate(temp.emojiDynamicSprite.gameObject);
            }
            else
            {
#endif
                GameObject.Destroy(temp.emojiDynamicSprite.gameObject);
            }
        }
    }

}