using System;
using UnityEngine;

namespace ClientCommon
{
	public partial class ConstValue
	{
		/// <summary>
		/// 伤害属性价值
		/// </summary>
		private static float attributeValueAP;
		public static float AttributeValueAP { get { return attributeValueAP; } }

		/// <summary>
		/// 攻击速度属性价值
		/// </summary>
		private static float attributeValueASP;
		public static float AttributeValueASP { get { return attributeValueASP; } }

		/// <summary>
		/// 会心伤害属性价值
		/// </summary>
		private static float attributeValueCDP;
		public static float AttributeValueCDP { get { return attributeValueCDP; } }

		/// <summary>
		/// 会心属性价值
		/// </summary>
		private static float attributeValueCP;
		public static float AttributeValueCP { get { return attributeValueCP; } }

		/// <summary>
		/// 韧性属性价值
		/// </summary>
		private static float _AttributeValueCRP;
		public static float AttributeValueCRP { get { return _AttributeValueCRP; } }

		/// <summary>
		/// 躲闪属性价值
		/// </summary>
		private static float _AttributeValueDGP;
		public static float AttributeValueDGP { get { return _AttributeValueDGP; } }

		/// <summary>
		/// 命中属性价值
		/// </summary>
		private static float _AttributeValueHITP;
		public static float AttributeValueHITP { get { return _AttributeValueHITP; } }

		/// <summary>
		/// 气血属性价值
		/// </summary>
		private static float _AttributeValueHP;
		public static float AttributeValueHP { get { return _AttributeValueHP; } }

		/// <summary>
		/// 内功破防属性价值
		/// </summary>
		private static float _AttributeValueIABP;
		public static float AttributeValueIABP { get { return _AttributeValueIABP; } }

		/// <summary>
		/// 内功防御属性价值
		/// </summary>
		private static float _AttributeValueIDP;
		public static float AttributeValueIDP { get { return _AttributeValueIDP; } }

		/// <summary>
		/// 外功破防属性价值
		/// </summary>
		private static float _AttributeValueOABP;
		public static float AttributeValueOABP { get { return _AttributeValueOABP; } }

		/// <summary>
		/// 外功防御属性价值
		/// </summary>
		private static float _AttributeValueODP;
		public static float AttributeValueODP { get { return _AttributeValueODP; } }

		/// <summary>
		/// 移动速度属性价值
		/// </summary>
		private static float _AttributeValueSP;
		public static float AttributeValueSP { get { return _AttributeValueSP; } }

		/// <summary>
		/// 免伤属性价值
		/// </summary>
		private static float _AttriubteValueAVOP;
		public static float AttriubteValueAVOP { get { return _AttriubteValueAVOP; } }

		/// <summary>
		/// 战斗中声音的最大传播距离
		/// </summary>
		private static float _AudioRolloffMaxDistance;
		public static float AudioRolloffMaxDistance { get { return _AudioRolloffMaxDistance; } }

		/// <summary>
		/// 战斗中声音开始衰减的起始距离
		/// </summary>
		private static float _AudioRolloffMinDistance;
		public static float AudioRolloffMinDistance { get { return _AudioRolloffMinDistance; } }

		/// <summary>
		/// 排行榜活跃度系数
		/// </summary>
		private static float _BaseRankActiveLevel;
		public static float BaseRankActiveLevel { get { return _BaseRankActiveLevel; } }

		/// <summary>
		/// 当没有目标时，点切换目标后最大的搜索范围
		/// </summary>
		private static float _BattleChangeTargetMaxSearchDistance;
		public static float BattleChangeTargetMaxSearchDistance { get { return _BattleChangeTargetMaxSearchDistance; } }

		private static float _BattleFarawayRoleAttackDistance;
		/// <summary>
		/// 远程的攻击距离，用于移动停止后的判断
		/// </summary>
		public static float BattleFarawayRoleAttackDistance { get { return _BattleFarawayRoleAttackDistance; } }

		private static float _BattleMaxDistanceForMoveToEnemyWhenStop;
		/// <summary>
		/// 玩家,停止移动后，如果超过这距离，就不会再自动去向敌人移动
		/// </summary>
		public static float BattleMaxDistanceForMoveToEnemyWhenStop { get { return _BattleMaxDistanceForMoveToEnemyWhenStop; } }

		private static float _BattleNearRoleAttackDistance;
		/// <summary>
		/// 近程的攻击距离，用于移动停止后的判断
		/// </summary>
		public static float BattleNearRoleAttackDistance { get { return _BattleNearRoleAttackDistance; } }

		private static float _CameraMaxDistance;
		/// <summary>
		/// 镜头最远距离
		/// </summary>
		public static float CameraMaxDistance { get { return _CameraMaxDistance; } }

		private static float _CameraMaxDistanceBattle;
		/// <summary>
		/// 战斗中的摄像机最大距离
		/// </summary>
		public static float CameraMaxDistanceBattle { get { return _CameraMaxDistanceBattle; } }

		private static float _CameraMinDistance;
		/// <summary>
		/// 镜头最近距离
		/// </summary>
		public static float CameraMinDistance { get { return _CameraMinDistance; } }

		private static float _CameraMinDistanceBattle;
		/// <summary>
		/// 战斗中的摄像机最小距离
		/// </summary>
		public static float CameraMinDistanceBattle { get { return _CameraMinDistanceBattle; } }

		private static float _CameraTraceDistance;
		/// <summary>
		/// 场景中摄像机与角色的距离
		/// </summary>
		public static float CameraTraceDistance { get { return _CameraTraceDistance; } }

		private static float _CameraTraceDistanceBattle;
		/// <summary>
		/// 战斗中的摄像机默认距离
		/// </summary>
		public static float CameraTraceDistanceBattle { get { return _CameraTraceDistanceBattle; } }

		private static float _CameraXScaleToLoadingOtherPlayerAsset;
		/// <summary>
		/// 摄像机宽度倍数，其他玩家在这个摄像机范围内则加载模型
		/// </summary>
		public static float CameraXScaleToLoadingOtherPlayerAsset { get { return _CameraXScaleToLoadingOtherPlayerAsset; } }

		private static float _CameraYScaleToLoadingOtherPlayerAsset;
		/// <summary>
		/// 摄像机高度倍数，其他玩家在这个摄像机范围内则加载模型
		/// </summary>
		public static float CameraYScaleToLoadingOtherPlayerAsset { get { return _CameraYScaleToLoadingOtherPlayerAsset; } }

		private static float _ClickGroundEffectYOffset;
		/// <summary>
		/// 点地特效在Y轴上的偏移, 防止被地面遮住
		/// </summary>
		public static float ClickGroundEffectYOffset { get { return _ClickGroundEffectYOffset; } }

		private static float _CollectDistance;
		/// <summary>
		/// 拾取物品的范围
		/// </summary>
		public static float CollectDistance { get { return _CollectDistance; } }

		private static float _CombatFormationRadius;
		/// <summary>
		/// 战斗中的阵型，队员与队长的距离（单位米，可以填小数）
		/// </summary>
		public static float CombatFormationRadius { get { return _CombatFormationRadius; } }

		private static float _CombatReSelectTargetCurrentAddation;
		/// <summary>
		/// 重新寻找一次敌人以找到更近的时，应该更优先选中当前的target，这个值就是给当前的target的加成
		/// </summary>
		public static float CombatReSelectTargetCurrentAddation { get { return _CombatReSelectTargetCurrentAddation; } }

		private static float _CombatReSelectTargetDistance;
		/// <summary>
		/// 当移动攻击时，如果距离目标超过这个值，应该重新寻找一次敌人以找到更近的
		/// </summary>
		public static float CombatReSelectTargetDistance { get { return _CombatReSelectTargetDistance; } }

		private static float _ConnectGateServerTimeOutJudgmentTime;
		/// <summary>
		/// 连接网关服务器超时的时间判定，单位秒
		/// </summary>
		public static float ConnectGateServerTimeOutJudgmentTime { get { return _ConnectGateServerTimeOutJudgmentTime; } }

		private static float _ConstF;
		/// <summary>
		/// 公式中的常数F
		/// </summary>
		public static float ConstF { get { return _ConstF; } }

		private static float _DefaultPotentialTargetRange;
		/// <summary>
		/// 默认索敌范围
		/// </summary>
		public static float DefaultPotentialTargetRange { get { return _DefaultPotentialTargetRange; } }

		private static float _DistanceToNpcDialog;
		/// <summary>
		/// 距离npc多远时打开对话菜单
		/// </summary>
		public static float DistanceToNpcDialog { get { return _DistanceToNpcDialog; } }

		private static float _DropGravityYAcceleratedSpeed;
		/// <summary>
		/// 物品掉落时重力加速度, 为负值
		/// </summary>
		public static float DropGravityYAcceleratedSpeed { get { return _DropGravityYAcceleratedSpeed; } }

		private static float _DropInitYSpeed;
		/// <summary>
		/// 物品掉落时初始的向上的速度, 为正值
		/// </summary>
		public static float DropInitYSpeed { get { return _DropInitYSpeed; } }

		private static float _DropMaxTime;
		/// <summary>
		/// 战斗中物品掉到地面的最长时间
		/// </summary>
		public static float DropMaxTime { get { return _DropMaxTime; } }

		private static float _DropMinTime;
		/// <summary>
		/// 战斗中物品掉到地面的最短时间
		/// </summary>
		public static float DropMinTime { get { return _DropMinTime; } }

		private static float _DropRadius;
		/// <summary>
		/// 怪物掉落物品的范围, 以它自身位置为圆心
		/// </summary>
		public static float DropRadius { get { return _DropRadius; } }

		private static float _DungeonGuideArrowVisibleDistance;
		/// <summary>
		/// 寻路引导 目标到达指定距离内则不显示箭头
		/// </summary>
		public static float DungeonGuideArrowVisibleDistance { get { return _DungeonGuideArrowVisibleDistance; } }

		private static float _KLightUpdateTimeSlice;
		/// <summary>
		/// 受伤闪白的效果的更新时间片
		/// </summary>
		public static float KLightUpdateTimeSlice { get { return _KLightUpdateTimeSlice; } }

		private static float _KUILightUpdateTimeSlice;
		/// <summary>
		/// UI冷却结束闪白的效果的更新时间片
		/// </summary>
		public static float KUILightUpdateTimeSlice { get { return _KUILightUpdateTimeSlice; } }

		private static float _MirrorArenaSyncNeedFightAddition;
		/// <summary>
		/// 是否提示进攻阵容是否同步到防守阵容条件（进战斗力比防多出5%）
		/// </summary>
		public static float MirrorArenaSyncNeedFightAddition { get { return _MirrorArenaSyncNeedFightAddition; } }

		private static float _MoveSpeedInCity;
		/// <summary>
		/// 主城中其他角色的移动速度
		/// </summary>
		public static float MoveSpeedInCity { get { return _MoveSpeedInCity; } }

		private static float _MusicVolume;
		/// <summary>
		/// 音乐的音量，取值区间（0~1）
		/// </summary>
		public static float MusicVolume { get { return _MusicVolume; } }

		private static float _New1v1ScoreCoefficient;
		/// <summary>
		/// 新1v1假人系数(用于积分计算)
		/// </summary>
		public static float New1v1ScoreCoefficient { get { return _New1v1ScoreCoefficient; } }

		private static float _OnHoldingNearDistance;
		/// <summary>
		/// 持续点击的时候，点击点与人物近距离判断的距离
		/// </summary>
		public static float OnHoldingNearDistance { get { return _OnHoldingNearDistance; } }

		private static float _OnHoldingNearTimeSection;
		/// <summary>
		/// 持续点击的时候，近距离的情况，发送位置的时间间隔(单位秒)
		/// </summary>
		public static float OnHoldingNearTimeSection { get { return _OnHoldingNearTimeSection; } }

		private static float _OnHoldingSendPositionDeltaTime;
		/// <summary>
		/// 战斗移动时，手指Hold时向服务器发送位置的时间间隔
		/// </summary>
		public static float OnHoldingSendPositionDeltaTime { get { return _OnHoldingSendPositionDeltaTime; } }

		private static float _PvPInnerDamageScaler;
		/// <summary>
		/// 人打人时内功伤害缩放比例,1为100%不处理
		/// </summary>
		public static float PvPInnerDamageScaler { get { return _PvPInnerDamageScaler; } }

		private static float _PvPOuterdamageScaler;
		/// <summary>
		/// 人打人时外功伤害缩放比例,1为100%不处理
		/// </summary>
		public static float PvPOuterdamageScaler { get { return _PvPOuterdamageScaler; } }

		private static float _RunAnimMovementSpeed;
		/// <summary>
		/// 战斗奔跑动作对应的标准移动速度（米/秒）
		/// </summary>
		public static float RunAnimMovementSpeed { get { return _RunAnimMovementSpeed; } }

		private static float _SoundVolume;
		/// <summary>
		/// 音效的音量，取值区间（0~1）
		/// </summary>
		public static float SoundVolume { get { return _SoundVolume; } }

		private static float _TeamDungeonRefreshCoolTime;
		/// <summary>
		/// 多人副本刷新队伍冷却时间
		/// </summary>
		public static float TeamDungeonRefreshCoolTime { get { return _TeamDungeonRefreshCoolTime; } }

		private static float _TestFloat;
		/// <summary>
		/// 0
		/// </summary>
		public static float TestFloat { get { return _TestFloat; } }

		private static float _UnitFollowDistance;
		/// <summary>
		/// 战斗中跟随移动的最远距离
		/// </summary>
		public static float UnitFollowDistance { get { return _UnitFollowDistance; } }

		private static float _WorldBossHurtErrantryFactor;
		/// <summary>
		/// 伤害奖励侠义值系数
		/// </summary>
		public static float WorldBossHurtErrantryFactor { get { return _WorldBossHurtErrantryFactor; } }

		private static float _WorldBossHurtGameMoneyFactor;
		/// <summary>
		/// 伤害奖励银两系数
		/// </summary>
		public static float WorldBossHurtGameMoneyFactor { get { return _WorldBossHurtGameMoneyFactor; } }

		private static float _WorldBossRankErrantryFactor;
		/// <summary>
		/// 排行奖励侠义值系数
		/// </summary>
		public static float WorldBossRankErrantryFactor { get { return _WorldBossRankErrantryFactor; } }

		private static int _AttributeValueAPColor;
		/// <summary>
		/// 伤害属性对应显示颜色
		/// </summary>
		public static int AttributeValueAPColor { get { return _AttributeValueAPColor; } }

		private static int _AttributeValueASPColor;
		/// <summary>
		/// 攻击速度属性对应显示颜色
		/// </summary>
		public static int AttributeValueASPColor { get { return _AttributeValueASPColor; } }

		private static int _AttributeValueCDPColor;
		/// <summary>
		/// 会心伤害属性对应显示颜色
		/// </summary>
		public static int AttributeValueCDPColor { get { return _AttributeValueCDPColor; } }

		private static int _AttributeValueCPColor;
		/// <summary>
		/// 会心属性对应显示颜色
		/// </summary>
		public static int AttributeValueCPColor { get { return _AttributeValueCPColor; } }

		private static int _AttributeValueCRPColor;
		/// <summary>
		/// 韧性属对应显示颜色
		/// </summary>
		public static int AttributeValueCRPColor { get { return _AttributeValueCRPColor; } }

		private static int _AttributeValueDGPColor;
		/// <summary>
		/// 躲闪属性对应显示颜色
		/// </summary>
		public static int AttributeValueDGPColor { get { return _AttributeValueDGPColor; } }

		private static int _AttributeValueHITPColor;
		/// <summary>
		/// 命中属性对应显示颜色
		/// </summary>
		public static int AttributeValueHITPColor { get { return _AttributeValueHITPColor; } }

		private static int _AttributeValueHPColor;
		/// <summary>
		/// 气血属性对应显示颜色
		/// </summary>
		public static int AttributeValueHPColor { get { return _AttributeValueHPColor; } }

		private static int _AttributeValueIABPColor;
		/// <summary>
		/// 内功破防属性对应显示颜色
		/// </summary>
		public static int AttributeValueIABPColor { get { return _AttributeValueIABPColor; } }

		private static int _AttributeValueIDPColor;
		/// <summary>
		/// 内功防御属性对应显示颜色
		/// </summary>
		public static int AttributeValueIDPColor { get { return _AttributeValueIDPColor; } }

		private static int _AttributeValueOABPColor;
		/// <summary>
		/// 外功破防属性对应显示颜色
		/// </summary>
		public static int AttributeValueOABPColor { get { return _AttributeValueOABPColor; } }

		private static int _AttributeValueODPColor;
		/// <summary>
		/// 外功防御属性对应显示颜色
		/// </summary>
		public static int AttributeValueODPColor { get { return _AttributeValueODPColor; } }

		private static int _AttributeValueSPColor;
		/// <summary>
		/// 移动速度属性对应显示颜色
		/// </summary>
		public static int AttributeValueSPColor { get { return _AttributeValueSPColor; } }

		private static int _AttriubteValueAVOPColor;
		/// <summary>
		/// 免伤属性对应显示颜色
		/// </summary>
		public static int AttriubteValueAVOPColor { get { return _AttriubteValueAVOPColor; } }

		private static int _AutoFightUseLevel;
		/// <summary>
		/// 自动战斗开启等级
		/// </summary>
		public static int AutoFightUseLevel { get { return _AutoFightUseLevel; } }

		private static int _BaseRankConditionLevel;
		/// <summary>
		/// 进入排行榜的等级限制
		/// </summary>
		public static int BaseRankConditionLevel { get { return _BaseRankConditionLevel; } }

		private static int _BaseRankConditionLimit;
		/// <summary>
		/// 榜的显示人数
		/// </summary>
		public static int BaseRankConditionLimit { get { return _BaseRankConditionLimit; } }

		private static int _BaseRankFightValue;
		/// <summary>
		/// 进入排行榜的战斗力要求
		/// </summary>
		public static int BaseRankFightValue { get { return _BaseRankFightValue; } }

		private static int _BaseRankRankSize;
		/// <summary>
		/// 显示排行的上下区间范围
		/// </summary>
		public static int BaseRankRankSize { get { return _BaseRankRankSize; } }

		private static int _BattleAvatarTemplateId;
		/// <summary>
		/// 0
		/// </summary>
		public static int BattleAvatarTemplateId { get { return _BattleAvatarTemplateId; } }

		private static int _BattleEndShowingEffectTime;
		/// <summary>
		/// 战斗结束之后慢动作并使用镜头效果的时间（毫秒）
		/// </summary>
		public static int BattleEndShowingEffectTime { get { return _BattleEndShowingEffectTime; } }

		private static int _BattleEnvPlayerTemplateId;
		/// <summary>
		/// 战斗中创建的场景玩家模板
		/// </summary>
		public static int BattleEnvPlayerTemplateId { get { return _BattleEnvPlayerTemplateId; } }

		private static int _BattlePlayerTemplateId;
		/// <summary>
		/// 战斗中用于创建玩家的模板Id
		/// </summary>
		public static int BattlePlayerTemplateId { get { return _BattlePlayerTemplateId; } }

		private static int _CentryCitySceneId;
		/// <summary>
		/// 主城关卡名，请注意，要和sceneConfig里的id一致
		/// </summary>
		public static int CentryCitySceneId { get { return _CentryCitySceneId; } }

		private static int _ChatInputMaxCount;
		/// <summary>
		/// 0
		/// </summary>
		public static int ChatInputMaxCount { get { return _ChatInputMaxCount; } }

		private static int _ChatListLimitCount;
		/// <summary>
		/// 世界聊天最大显示条目
		/// </summary>
		public static int ChatListLimitCount { get { return _ChatListLimitCount; } }

		private static int _CityAvatarTemplateId;
		/// <summary>
		/// 0
		/// </summary>
		public static int CityAvatarTemplateId { get { return _CityAvatarTemplateId; } }

		private static int _CombatAfterGameEndTimeOutTime;
		/// <summary>
		/// 战斗结束后，战斗服延迟销毁的时间
		/// </summary>
		public static int CombatAfterGameEndTimeOutTime { get { return _CombatAfterGameEndTimeOutTime; } }

		private static int _CombatAllConnectionLosedTimeOutTime;
		/// <summary>
		/// 所有玩家掉线后，战斗服等待时间，超时则结束
		/// </summary>
		public static int CombatAllConnectionLosedTimeOutTime { get { return _CombatAllConnectionLosedTimeOutTime; } }

		private static int _CombatBeforeStartGameTimeOutTime;
		/// <summary>
		/// 开始战斗前战斗服的超时时间（要考虑玩家的加载时间）
		/// </summary>
		public static int CombatBeforeStartGameTimeOutTime { get { return _CombatBeforeStartGameTimeOutTime; } }

		private static int _CombatFormationDeltaDegrees;
		/// <summary>
		/// 战斗中的阵型，队员之间散开的夹角（单位度，不要超过90度则第三个小弟（召唤的怪物）会站到前面去）
		/// </summary>
		public static int CombatFormationDeltaDegrees { get { return _CombatFormationDeltaDegrees; } }

		private static int _CombatMultiDungeonTimeLimit;
		/// <summary>
		/// 多人副本战斗限时
		/// </summary>
		public static int CombatMultiDungeonTimeLimit { get { return _CombatMultiDungeonTimeLimit; } }

		private static int _CombatRealTime1v1TimeLimit;
		/// <summary>
		/// 1v1战斗限时
		/// </summary>
		public static int CombatRealTime1v1TimeLimit { get { return _CombatRealTime1v1TimeLimit; } }

		private static int _CombatWaitOtherPlayerConnectTimeOutTime;
		/// <summary>
		/// 有人连上战斗服，有人没连上时，等待他人连接的等待时间
		/// </summary>
		public static int CombatWaitOtherPlayerConnectTimeOutTime { get { return _CombatWaitOtherPlayerConnectTimeOutTime; } }

		private static int _ConcentrateFireAbilityId;
		/// <summary>
		/// 集火技能Id
		/// </summary>
		public static int ConcentrateFireAbilityId { get { return _ConcentrateFireAbilityId; } }

		private static int _CreateAvatarTemplateId;
		/// <summary>
		/// 0
		/// </summary>
		public static int CreateAvatarTemplateId { get { return _CreateAvatarTemplateId; } }

		private static int _DamageTextOffsetY;
		/// <summary>
		/// 伤害飘字偏移
		/// </summary>
		public static int DamageTextOffsetY { get { return _DamageTextOffsetY; } }

		private static int _DartPeriod;
		/// <summary>
		/// 寻访持续时间（单位：小时）(作废）
		/// </summary>
		public static int DartPeriod { get { return _DartPeriod; } }

		private static int _DefaultWeaponInCityID;
		/// <summary>
		/// 主城中，默认的武器ID
		/// </summary>
		public static int DefaultWeaponInCityID { get { return _DefaultWeaponInCityID; } }

		private static int _DefaultWeaponInDungeonID;
		/// <summary>
		/// 副本中默认的武器
		/// </summary>
		public static int DefaultWeaponInDungeonID { get { return _DefaultWeaponInDungeonID; } }

		private static int _DemonstrationBattlePlayerDummyGroupId;
		/// <summary>
		/// 演示关卡玩家数据假人组Id
		/// </summary>
		public static int DemonstrationBattlePlayerDummyGroupId { get { return _DemonstrationBattlePlayerDummyGroupId; } }

		private static int _DemonstrationBattlePlayerGirlDummyGroupId;
		/// <summary>
		/// 演示关假人组Id（女性角色）
		/// </summary>
		public static int DemonstrationBattlePlayerGirlDummyGroupId { get { return _DemonstrationBattlePlayerGirlDummyGroupId; } }

		private static int _DialogEableSendLevel;
		/// <summary>
		/// 聊天中允许发送的最小等级
		/// </summary>
		public static int DialogEableSendLevel { get { return _DialogEableSendLevel; } }

		private static int _DialogEableSendVipLevel;
		/// <summary>
		/// 聊天中允许发送的最小的VIP等级
		/// </summary>
		public static int DialogEableSendVipLevel { get { return _DialogEableSendVipLevel; } }

		private static int _DialogItemLimitCountFaction;
		/// <summary>
		/// 聊天帮派频道最大数目
		/// </summary>
		public static int DialogItemLimitCountFaction { get { return _DialogItemLimitCountFaction; } }

		private static int _DialogItemLimitCountGM;
		/// <summary>
		/// GM频道最大的聊天数目
		/// </summary>
		public static int DialogItemLimitCountGM { get { return _DialogItemLimitCountGM; } }

		private static int _DialogItemLimitCountSystem;
		/// <summary>
		/// 聊天系统频道消息最大条数
		/// </summary>
		public static int DialogItemLimitCountSystem { get { return _DialogItemLimitCountSystem; } }

		private static int _DialogItemLimitCountTotal;
		/// <summary>
		/// 聊天综合频道最大数目
		/// </summary>
		public static int DialogItemLimitCountTotal { get { return _DialogItemLimitCountTotal; } }

		private static int _DialogItemLimitCountWorld;
		/// <summary>
		/// 聊天世界频道最大条数
		/// </summary>
		public static int DialogItemLimitCountWorld { get { return _DialogItemLimitCountWorld; } }

		private static int _DungeonFightConditionAutoHideTime;
		/// <summary>
		/// 剧情副本完美通关条件自动隐藏时间（单位：秒）
		/// </summary>
		public static int DungeonFightConditionAutoHideTime { get { return _DungeonFightConditionAutoHideTime; } }

		private static int _DungeonGuideArrowRotationSpeed;
		/// <summary>
		/// 寻路引导 箭头旋转到方向的时间（毫秒）
		/// </summary>
		public static int DungeonGuideArrowRotationSpeed { get { return _DungeonGuideArrowRotationSpeed; } }

		private static int _DungeonViewerAvatarTemplateId;
		/// <summary>
		/// 0
		/// </summary>
		public static int DungeonViewerAvatarTemplateId { get { return _DungeonViewerAvatarTemplateId; } }

		private static int _EmailClientSize;
		/// <summary>
		/// 0
		/// </summary>
		public static int EmailClientSize { get { return _EmailClientSize; } }

		private static int _EmailMaxSize;
		/// <summary>
		/// 0
		/// </summary>
		public static int EmailMaxSize { get { return _EmailMaxSize; } }

		private static int _EquipmentBuyBelt;
		/// <summary>
		/// 装备快捷购买--腰带
		/// </summary>
		public static int EquipmentBuyBelt { get { return _EquipmentBuyBelt; } }

		private static int _EquipmentBuyCloth;
		/// <summary>
		/// 装备快捷购买--衣服
		/// </summary>
		public static int EquipmentBuyCloth { get { return _EquipmentBuyCloth; } }

		private static int _EquipmentBuyHat;
		/// <summary>
		/// 装备快捷购买--头盔
		/// </summary>
		public static int EquipmentBuyHat { get { return _EquipmentBuyHat; } }

		private static int _EquipmentBuyNecklace;
		/// <summary>
		/// 装备快捷购买--项链
		/// </summary>
		public static int EquipmentBuyNecklace { get { return _EquipmentBuyNecklace; } }

		private static int _EquipmentBuyRing;
		/// <summary>
		/// 装备快捷购买--戒指
		/// </summary>
		public static int EquipmentBuyRing { get { return _EquipmentBuyRing; } }

		private static int _EquipmentBuyWeapon;
		/// <summary>
		/// 装备快捷购买--武器
		/// </summary>
		public static int EquipmentBuyWeapon { get { return _EquipmentBuyWeapon; } }

		private static int _EquipmentPutonMinLevelBelt;
		/// <summary>
		/// 装备最低穿戴等级--腰带
		/// </summary>
		public static int EquipmentPutonMinLevelBelt { get { return _EquipmentPutonMinLevelBelt; } }

		private static int _EquipmentPutonMinLevelCloth;
		/// <summary>
		/// 装备最低穿戴等级--衣服
		/// </summary>
		public static int EquipmentPutonMinLevelCloth { get { return _EquipmentPutonMinLevelCloth; } }

		private static int _EquipmentPutonMinLevelHat;
		/// <summary>
		/// 装备最低穿戴等级--头盔
		/// </summary>
		public static int EquipmentPutonMinLevelHat { get { return _EquipmentPutonMinLevelHat; } }

		private static int _EquipmentPutonMinLevelNecklace;
		/// <summary>
		/// 装备最低穿戴等级--项链
		/// </summary>
		public static int EquipmentPutonMinLevelNecklace { get { return _EquipmentPutonMinLevelNecklace; } }

		private static int _EquipmentPutonMinLevelRing;
		/// <summary>
		/// 装备最低穿戴等级--戒指
		/// </summary>
		public static int EquipmentPutonMinLevelRing { get { return _EquipmentPutonMinLevelRing; } }

		private static int _EquipmentPutonMinLevelWeapon;
		/// <summary>
		/// 装备最低穿戴等级--武器
		/// </summary>
		public static int EquipmentPutonMinLevelWeapon { get { return _EquipmentPutonMinLevelWeapon; } }

		private static int _EquipmentRecastBeginCondition;
		/// <summary>
		/// 装备重铸开始条件（关卡Id）
		/// </summary>
		public static int EquipmentRecastBeginCondition { get { return _EquipmentRecastBeginCondition; } }

		private static int _EquipmentRefineBeginCondition;
		/// <summary>
		/// 装备精炼开始条件（关卡Id）
		/// </summary>
		public static int EquipmentRefineBeginCondition { get { return _EquipmentRefineBeginCondition; } }

		private static int _EquipmentRefineMaxLevel;
		/// <summary>
		/// 装备精炼最大等级(废弃)
		/// </summary>
		public static int EquipmentRefineMaxLevel { get { return _EquipmentRefineMaxLevel; } }

		private static int _EquipmentReinforceBeginCondition;
		/// <summary>
		/// 装备强化开始条件（关卡ID）
		/// </summary>
		public static int EquipmentReinforceBeginCondition { get { return _EquipmentReinforceBeginCondition; } }

		private static int _EquipmentSlotCount;
		/// <summary>
		/// 装备宝石的最大数量
		/// </summary>
		public static int EquipmentSlotCount { get { return _EquipmentSlotCount; } }

		private static int _Errantry;
		/// <summary>
		/// 侠义值ID
		/// </summary>
		public static int Errantry { get { return _Errantry; } }

		private static int _Exp;
		/// <summary>
		/// 经验ID
		/// </summary>
		public static int Exp { get { return _Exp; } }

		private static int _FastCleanDungeon;
		/// <summary>
		/// 副本快速扫荡次数
		/// </summary>
		public static int FastCleanDungeon { get { return _FastCleanDungeon; } }

		private static int _FirstAcupointId;
		/// <summary>
		/// 第一个穴位id
		/// </summary>
		public static int FirstAcupointId { get { return _FirstAcupointId; } }

		private static int _FirstFakeBattleSceneId;
		/// <summary>
		/// 演示关sceneid
		/// </summary>
		public static int FirstFakeBattleSceneId { get { return _FirstFakeBattleSceneId; } }

		private static int _FriendsBlessDailyCount;
		/// <summary>
		/// 每日允许祝福的最大好友数
		/// </summary>
		public static int FriendsBlessDailyCount { get { return _FriendsBlessDailyCount; } }

		private static int _FriendsBlessGainStaminCount;
		/// <summary>
		/// 好友祝福后获得的体力
		/// </summary>
		public static int FriendsBlessGainStaminCount { get { return _FriendsBlessGainStaminCount; } }

		private static int _FriendsCdTime;
		/// <summary>
		/// 好友邀请cd时间
		/// </summary>
		public static int FriendsCdTime { get { return _FriendsCdTime; } }

		private static int _FriendsChatLimitCount;
		/// <summary>
		/// 好友聊天的条数限制(作废，与FriendsMsgCount相同的功能）
		/// </summary>
		public static int FriendsChatLimitCount { get { return _FriendsChatLimitCount; } }

		private static int _FriendShipUpLimit;
		/// <summary>
		/// 好友度的等级上限
		/// </summary>
		public static int FriendShipUpLimit { get { return _FriendShipUpLimit; } }

		private static int _FriendsLimitCount;
		/// <summary>
		/// 好友数量限制
		/// </summary>
		public static int FriendsLimitCount { get { return _FriendsLimitCount; } }

		private static int _FriendsMsgCount;
		/// <summary>
		/// 好友私聊记录数量
		/// </summary>
		public static int FriendsMsgCount { get { return _FriendsMsgCount; } }

		private static int _FriendsPushLevel;
		/// <summary>
		/// 好友推送的最低等级
		/// </summary>
		public static int FriendsPushLevel { get { return _FriendsPushLevel; } }

		private static int _GameMoney;
		/// <summary>
		/// 银币ID
		/// </summary>
		public static int GameMoney { get { return _GameMoney; } }

		private static int _GrowthFundBuyNeedRealMoney;
		/// <summary>
		/// 成长基金的售价，元宝
		/// </summary>
		public static int GrowthFundBuyNeedRealMoney { get { return _GrowthFundBuyNeedRealMoney; } }

		private static int _GrowthFundBuyNeedVip;
		/// <summary>
		/// 可购买成长基金所需的VIP等级
		/// </summary>
		public static int GrowthFundBuyNeedVip { get { return _GrowthFundBuyNeedVip; } }

		private static int _HeartPowerActiveLevel;
		/// <summary>
		/// 第二个心法激活等级
		/// </summary>
		public static int HeartPowerActiveLevel { get { return _HeartPowerActiveLevel; } }

		private static int _HeroFightMaxNum;
		/// <summary>
		/// 最大上阵英雄数（废弃）
		/// </summary>
		public static int HeroFightMaxNum { get { return _HeroFightMaxNum; } }

		private static int _IdleOnceTimeSection;
		/// <summary>
		/// Idle状态下播放IdleOnce动画时间间隔（秒）
		/// </summary>
		public static int IdleOnceTimeSection { get { return _IdleOnceTimeSection; } }

		private static int _InitialAttributeLevel;
		/// <summary>
		/// 0
		/// </summary>
		public static int InitialAttributeLevel { get { return _InitialAttributeLevel; } }

		private static int _InitRankGradId;
		/// <summary>
		/// 排行榜的段位称号起始id
		/// </summary>
		public static int InitRankGradId { get { return _InitRankGradId; } }

		private static int _InvalidConfigValue;
		/// <summary>
		/// 无效的策划配置值
		/// </summary>
		public static int InvalidConfigValue { get { return _InvalidConfigValue; } }

		private static int _LeagueApplyCount;
		/// <summary>
		/// 每日申请公会次数限制
		/// </summary>
		public static int LeagueApplyCount { get { return _LeagueApplyCount; } }

		private static int _LeagueCreateLevel;
		/// <summary>
		/// 创建公会的等级限制
		/// </summary>
		public static int LeagueCreateLevel { get { return _LeagueCreateLevel; } }

		private static int _LeagueCreateRealMoney;
		/// <summary>
		/// 创建公会需要花费的元宝数量
		/// </summary>
		public static int LeagueCreateRealMoney { get { return _LeagueCreateRealMoney; } }

		private static int _LeagueInviteCD;
		/// <summary>
		/// 公会邀请的CD时间（秒）
		/// </summary>
		public static int LeagueInviteCD { get { return _LeagueInviteCD; } }

		private static int _LeagueMaxLevel;
		/// <summary>
		/// 公会的最大等级
		/// </summary>
		public static int LeagueMaxLevel { get { return _LeagueMaxLevel; } }

		private static int _LootExpItemId;
		/// <summary>
		/// 经验的物品ID, 用于掉落（废弃，使用Exp）
		/// </summary>
		public static int LootExpItemId { get { return _LootExpItemId; } }

		private static int _LootMoneyItemId;
		/// <summary>
		/// 游戏钱币的物品ID, 用于掉落（废弃，使用GameMoney）
		/// </summary>
		public static int LootMoneyItemId { get { return _LootMoneyItemId; } }

		private static int _LootRealMoneyItemId;
		/// <summary>
		/// 元宝的物品ID（废弃，使用RealMoney）
		/// </summary>
		public static int LootRealMoneyItemId { get { return _LootRealMoneyItemId; } }

		private static int _LootReputationItemId;
		/// <summary>
		/// 声望的物品ID, 用于掉落（废弃，使用Reputation）
		/// </summary>
		public static int LootReputationItemId { get { return _LootReputationItemId; } }

		private static int _MatrixCentreSlotCount;
		/// <summary>
		/// 阵眼宝石最大槽数
		/// </summary>
		public static int MatrixCentreSlotCount { get { return _MatrixCentreSlotCount; } }

		private static int _MaxComeOnFxCount;
		/// <summary>
		/// 最多同时存在的点地移动特效数量
		/// </summary>
		public static int MaxComeOnFxCount { get { return _MaxComeOnFxCount; } }

		private static int _MaxConfigHint;
		/// <summary>
		/// 策划统一把配置中用于线索的最大值配成该值
		/// </summary>
		public static int MaxConfigHint { get { return _MaxConfigHint; } }

		private static int _MaxInScreenShowingAvatarCount;
		/// <summary>
		/// 同屏最多显示的玩家数量，包括主角
		/// </summary>
		public static int MaxInScreenShowingAvatarCount { get { return _MaxInScreenShowingAvatarCount; } }

		private static int _MaxLevel;
		/// <summary>
		/// 最大等级
		/// </summary>
		public static int MaxLevel { get { return _MaxLevel; } }

		private static int _MaxStamina;
		/// <summary>
		/// 最大体力值
		/// </summary>
		public static int MaxStamina { get { return _MaxStamina; } }

		private static int _MirrorArenaBattleClearCoolCost;
		/// <summary>
		/// 镜像竞技场_清除冷却元宝花费
		/// </summary>
		public static int MirrorArenaBattleClearCoolCost { get { return _MirrorArenaBattleClearCoolCost; } }

		private static int _MirrorArenaBattleCoolTime;
		/// <summary>
		/// 镜像竞技场_挑战冷却时间（秒）
		/// </summary>
		public static int MirrorArenaBattleCoolTime { get { return _MirrorArenaBattleCoolTime; } }

		private static int _MirrorArenaBattleCount;
		/// <summary>
		/// 镜像竞技场_挑战次数上限
		/// </summary>
		public static int MirrorArenaBattleCount { get { return _MirrorArenaBattleCount; } }

		private static int _MirrorArenaBattleCountDownTime;
		/// <summary>
		/// 镜像竞技场战前倒计时时长
		/// </summary>
		public static int MirrorArenaBattleCountDownTime { get { return _MirrorArenaBattleCountDownTime; } }

		private static int _MirrorArenaBattleLimitTime;
		/// <summary>
		/// 镜像竞技场_竞技场战斗时长限制（秒）
		/// </summary>
		public static int MirrorArenaBattleLimitTime { get { return _MirrorArenaBattleLimitTime; } }

		private static int _MirrorArenaBattleRecordCount;
		/// <summary>
		/// 镜像竞技场_战斗报告保存条目数量
		/// </summary>
		public static int MirrorArenaBattleRecordCount { get { return _MirrorArenaBattleRecordCount; } }

		private static int _MirrorArenaBattleSceneId;
		/// <summary>
		/// 镜像竞技场_战斗场景（sence_config ID）
		/// </summary>
		public static int MirrorArenaBattleSceneId { get { return _MirrorArenaBattleSceneId; } }

		private static int _MirrorArenaBuyBattleCountCost;
		/// <summary>
		/// 购买战斗次数消耗(作废)
		/// </summary>
		public static int MirrorArenaBuyBattleCountCost { get { return _MirrorArenaBuyBattleCountCost; } }

		private static int _MirrorArenaEachDayRewardCount;
		/// <summary>
		/// 镜像竞技场_每日排名奖励名次数量
		/// </summary>
		public static int MirrorArenaEachDayRewardCount { get { return _MirrorArenaEachDayRewardCount; } }

		private static int _MirrorArenaNormalRefreshCost;
		/// <summary>
		/// 镜像竞技场_普通刷新元宝花费
		/// </summary>
		public static int MirrorArenaNormalRefreshCost { get { return _MirrorArenaNormalRefreshCost; } }

		private static int _MirrorArenaNormalRefreshCount;
		/// <summary>
		/// 镜像竞技场_普通刷新每日免费次数
		/// </summary>
		public static int MirrorArenaNormalRefreshCount { get { return _MirrorArenaNormalRefreshCount; } }

		private static int _MirrorArenaNormalRefreshRange;
		/// <summary>
		/// 镜像竞技场_普通刷新比自己高的名次范围
		/// </summary>
		public static int MirrorArenaNormalRefreshRange { get { return _MirrorArenaNormalRefreshRange; } }

		private static int _MirrorArenaVipRefreshCost;
		/// <summary>
		/// 镜像竞技场_VIP刷新元宝花费
		/// </summary>
		public static int MirrorArenaVipRefreshCost { get { return _MirrorArenaVipRefreshCost; } }

		private static int _MirrorArenaVipRefreshRange;
		/// <summary>
		/// 镜像竞技场_VIP刷新比自己高的名次范围
		/// </summary>
		public static int MirrorArenaVipRefreshRange { get { return _MirrorArenaVipRefreshRange; } }

		private static int _MonsterCreaterTemplateId;
		/// <summary>
		/// 怪物生成器template Id, 程序用的
		/// </summary>
		public static int MonsterCreaterTemplateId { get { return _MonsterCreaterTemplateId; } }

		private static int _MonsterPatrolIdleMs;
		/// <summary>
		/// 战斗中怪物巡逻间隔时的idle时间，单位毫秒
		/// </summary>
		public static int MonsterPatrolIdleMs { get { return _MonsterPatrolIdleMs; } }

		private static int _MonthcardBuyCountLimit;
		/// <summary>
		/// 月卡最大持有期数限制
		/// </summary>
		public static int MonthcardBuyCountLimit { get { return _MonthcardBuyCountLimit; } }

		private static int _MultiDungeonMaxShowGroupCount;
		/// <summary>
		/// 客户端多人副本最多显示队伍数
		/// </summary>
		public static int MultiDungeonMaxShowGroupCount { get { return _MultiDungeonMaxShowGroupCount; } }

		private static int _MultiDungeonResetAwardCount;
		/// <summary>
		/// 多人副本可获得奖励次数每日重置值
		/// </summary>
		public static int MultiDungeonResetAwardCount { get { return _MultiDungeonResetAwardCount; } }

		private static int _NameMaxLength;
		/// <summary>
		/// 0
		/// </summary>
		public static int NameMaxLength { get { return _NameMaxLength; } }

		private static int _New1v1ArenaSceneId;
		/// <summary>
		/// 新手1v1战斗场景id(废弃)
		/// </summary>
		public static int New1v1ArenaSceneId { get { return _New1v1ArenaSceneId; } }

		private static int _New1v1FirstCombatDummyId;
		/// <summary>
		/// 首次战斗假人ID
		/// </summary>
		public static int New1v1FirstCombatDummyId { get { return _New1v1FirstCombatDummyId; } }

		private static int _New1v1FixPerTime;
		/// <summary>
		/// 匹配扩展单位时间（单位秒）
		/// </summary>
		public static int New1v1FixPerTime { get { return _New1v1FixPerTime; } }

		private static int _New1v1FloorEmailId;
		/// <summary>
		/// 新1v1保底奖励邮件id
		/// </summary>
		public static int New1v1FloorEmailId { get { return _New1v1FloorEmailId; } }

		private static int _New1v1FloorScore;
		/// <summary>
		/// 新1v1保底积分底限
		/// </summary>
		public static int New1v1FloorScore { get { return _New1v1FloorScore; } }

		private static int _New1v1LoseScore;
		/// <summary>
		/// 新1v1战斗失败积分
		/// </summary>
		public static int New1v1LoseScore { get { return _New1v1LoseScore; } }

		private static int _New1v1MatchTime;
		/// <summary>
		/// 新1v1匹配活人等待时间，单位秒
		/// </summary>
		public static int New1v1MatchTime { get { return _New1v1MatchTime; } }

		private static int _New1v1PerTimeFixLevel;
		/// <summary>
		/// 匹配单位时间扩展级别范围
		/// </summary>
		public static int New1v1PerTimeFixLevel { get { return _New1v1PerTimeFixLevel; } }

		private static int _New1v1RankMinScore;
		/// <summary>
		/// 新1v1排名积分底限
		/// </summary>
		public static int New1v1RankMinScore { get { return _New1v1RankMinScore; } }

		private static int _New1v1SceneId;
		/// <summary>
		/// 新1v1战斗场景id 
		/// </summary>
		public static int New1v1SceneId { get { return _New1v1SceneId; } }

		private static int _New1v1WinScore;
		/// <summary>
		/// 新1v1战斗胜利积分
		/// </summary>
		public static int New1v1WinScore { get { return _New1v1WinScore; } }

		private static int _NpcCollectionTemplateId;
		/// <summary>
		/// 0
		/// </summary>
		public static int NpcCollectionTemplateId { get { return _NpcCollectionTemplateId; } }

		private static int _NpcTemplateId;
		/// <summary>
		/// 0
		/// </summary>
		public static int NpcTemplateId { get { return _NpcTemplateId; } }

		private static int _NullDataValue;
		/// <summary>
		/// 0
		/// </summary>
		public static int NullDataValue { get { return _NullDataValue; } }

		private static int _OnevsoneArenaCooldown;
		/// <summary>
		/// 1v1竞技场_冷却时间（秒）
		/// </summary>
		public static int OnevsoneArenaCooldown { get { return _OnevsoneArenaCooldown; } }

		private static int _OnevsoneArenaLimitTime;
		/// <summary>
		/// 1v1竞技场_战斗限时（秒）
		/// </summary>
		public static int OnevsoneArenaLimitTime { get { return _OnevsoneArenaLimitTime; } }

		private static int _OnevsoneArenaMatchRangeFix;
		/// <summary>
		/// 1v1竞技场_每秒搜索范围扩大值（积分分数）
		/// </summary>
		public static int OnevsoneArenaMatchRangeFix { get { return _OnevsoneArenaMatchRangeFix; } }

		private static int _OnevsoneArenaMatchTimeMax;
		/// <summary>
		/// 1v1竞技场_最大匹配时长（秒）
		/// </summary>
		public static int OnevsoneArenaMatchTimeMax { get { return _OnevsoneArenaMatchTimeMax; } }

		private static int _OnevsoneArenaMatchTimeNormal;
		/// <summary>
		/// 1v1竞技场_精准匹配时长（秒）
		/// </summary>
		public static int OnevsoneArenaMatchTimeNormal { get { return _OnevsoneArenaMatchTimeNormal; } }

		private static int _OnevsoneArenaMaxRecord;
		/// <summary>
		/// 1v1竞技场_最大日志数量
		/// </summary>
		public static int OnevsoneArenaMaxRecord { get { return _OnevsoneArenaMaxRecord; } }

		private static int _OnevsoneArenaOpenEmail;
		/// <summary>
		/// 1v1竞技场_开启邮件ID
		/// </summary>
		public static int OnevsoneArenaOpenEmail { get { return _OnevsoneArenaOpenEmail; } }

		private static int _OnevsoneArenaRankShow;
		/// <summary>
		/// 1v1竞技场_最大排行榜排名
		/// </summary>
		public static int OnevsoneArenaRankShow { get { return _OnevsoneArenaRankShow; } }

		private static int _OnevsoneArenaSceneId;
		/// <summary>
		/// 1v1竞技场_战斗场景ID
		/// </summary>
		public static int OnevsoneArenaSceneId { get { return _OnevsoneArenaSceneId; } }

		private static int _OnevsoneArenaScoreInit;
		/// <summary>
		/// 1v1竞技场_初始积分
		/// </summary>
		public static int OnevsoneArenaScoreInit { get { return _OnevsoneArenaScoreInit; } }

		private static int _OnevsoneArenaScoreMax;
		/// <summary>
		/// 1v1竞技场_最大积分
		/// </summary>
		public static int OnevsoneArenaScoreMax { get { return _OnevsoneArenaScoreMax; } }

		private static int _OnevsoneArenaScoreMin;
		/// <summary>
		/// 1v1竞技场_最小积分
		/// </summary>
		public static int OnevsoneArenaScoreMin { get { return _OnevsoneArenaScoreMin; } }

		private static int _OnevsoneArenaSeasonDuration;
		/// <summary>
		/// 1v1竞技场_赛季长度（天）
		/// </summary>
		public static int OnevsoneArenaSeasonDuration { get { return _OnevsoneArenaSeasonDuration; } }

		private static int _OnevsoneArenaStraightBroadcast;
		/// <summary>
		/// 1v1竞技场_广播连胜次数/终结连胜次数
		/// </summary>
		public static int OnevsoneArenaStraightBroadcast { get { return _OnevsoneArenaStraightBroadcast; } }

		private static int _OnevsOneCanMeetDummyCount;
		/// <summary>
		/// 1v1竞技场_每日挑战假人次数
		/// </summary>
		public static int OnevsOneCanMeetDummyCount { get { return _OnevsOneCanMeetDummyCount; } }

		private static int _OneVsOneClearCoolTimeCost;
		/// <summary>
		/// 1v1竞技场清除冷却时间消耗元宝
		/// </summary>
		public static int OneVsOneClearCoolTimeCost { get { return _OneVsOneClearCoolTimeCost; } }

		private static int _OnevsOneOpenNextSeasonBreakTime;
		/// <summary>
		/// 1v1关闭上赛季到开启下赛季的间隔时间（单位分钟）
		/// </summary>
		public static int OnevsOneOpenNextSeasonBreakTime { get { return _OnevsOneOpenNextSeasonBreakTime; } }

		private static int _OpenChangeHero;
		/// <summary>
		/// 上阵功能开启条件（通关关卡Id）
		/// </summary>
		public static int OpenChangeHero { get { return _OpenChangeHero; } }

		private static int _OpenFriends;
		/// <summary>
		/// 好友系统开启条件（通关关卡Id）
		/// </summary>
		public static int OpenFriends { get { return _OpenFriends; } }

		private static int _OpenHeartPower;
		/// <summary>
		/// 心法系统开启条件（通关关卡Id）
		/// </summary>
		public static int OpenHeartPower { get { return _OpenHeartPower; } }

		private static int _OpenHire;
		/// <summary>
		/// 招募开启限制条件（通关关卡Id）
		/// </summary>
		public static int OpenHire { get { return _OpenHire; } }

		private static int _OpenJewelCompose;
		/// <summary>
		/// 宝石合成开启条件（通关关卡Id）
		/// </summary>
		public static int OpenJewelCompose { get { return _OpenJewelCompose; } }

		private static int _OpenRealm;
		/// <summary>
		/// 境界系统开启条件（通关关卡Id）
		/// </summary>
		public static int OpenRealm { get { return _OpenRealm; } }

		private static int _OpenStore;
		/// <summary>
		/// 商店开启条件（通关关卡Id）
		/// </summary>
		public static int OpenStore { get { return _OpenStore; } }

		private static int _OpenTimeLimitDungeon;
		/// <summary>
		/// 限时副本开启条件通（关关卡Id）
		/// </summary>
		public static int OpenTimeLimitDungeon { get { return _OpenTimeLimitDungeon; } }

		private static int _OpenTreasure;
		/// <summary>
		/// 江湖奇宝功能开启条件（通关关卡Id）
		/// </summary>
		public static int OpenTreasure { get { return _OpenTreasure; } }

		private static int _OpenTutor;
		/// <summary>
		/// 拜师开启限制条件（通关关卡Id）
		/// </summary>
		public static int OpenTutor { get { return _OpenTutor; } }

		private static int _OpenWorldChatLevel;
		/// <summary>
		/// 世界聊天开启等级
		/// </summary>
		public static int OpenWorldChatLevel { get { return _OpenWorldChatLevel; } }

		private static int _OpenWorldChatVIPLevel;
		/// <summary>
		/// 世界聊天开vip启等级
		/// </summary>
		public static int OpenWorldChatVIPLevel { get { return _OpenWorldChatVIPLevel; } }

		private static int _PropertyRateDivisionFactor;
		/// <summary>
		/// f, 算属性百分比时的一个公式参数，比如 GetRateKnowing里调用
		/// </summary>
		public static int PropertyRateDivisionFactor { get { return _PropertyRateDivisionFactor; } }

		private static int _RandomMissionCanDoneCount;
		/// <summary>
		/// 江湖缉拿默认每天能完成的次数
		/// </summary>
		public static int RandomMissionCanDoneCount { get { return _RandomMissionCanDoneCount; } }

		private static int _RandomMissionReceiveCount;
		/// <summary>
		/// 江湖缉拿接受任务数量
		/// </summary>
		public static int RandomMissionReceiveCount { get { return _RandomMissionReceiveCount; } }

		private static int _RandomMissionRefreshApart;
		/// <summary>
		/// 江湖缉拿刷新间隔时间(秒)
		/// </summary>
		public static int RandomMissionRefreshApart { get { return _RandomMissionRefreshApart; } }

		private static int _RandomMissionRefreshCost;
		/// <summary>
		/// 江湖缉拿刷新消耗元宝
		/// </summary>
		public static int RandomMissionRefreshCost { get { return _RandomMissionRefreshCost; } }

		private static int _RandomMissionRefreshCount;
		/// <summary>
		/// 江湖缉拿刷新任务数量
		/// </summary>
		public static int RandomMissionRefreshCount { get { return _RandomMissionRefreshCount; } }

		private static int _RandomMissionRefreshFree;
		/// <summary>
		/// 江湖缉拿免费刷新次数
		/// </summary>
		public static int RandomMissionRefreshFree { get { return _RandomMissionRefreshFree; } }

		private static int _RandomMissionRefreshTimesLimit;
		/// <summary>
		/// 江湖缉拿刷新限定出现次数
		/// </summary>
		public static int RandomMissionRefreshTimesLimit { get { return _RandomMissionRefreshTimesLimit; } }

		private static int _RealMoney;
		/// <summary>
		/// 金币ID
		/// </summary>
		public static int RealMoney { get { return _RealMoney; } }

		private static int _RealTime1v1BattleCountDownTime;
		/// <summary>
		/// 实时1v1竞技场战前倒计时时长
		/// </summary>
		public static int RealTime1v1BattleCountDownTime { get { return _RealTime1v1BattleCountDownTime; } }

		private static int _ReConnectAutoActionCutdownTime;
		/// <summary>
		/// 弹出断线重连后，多长时间后程序自动为玩家重连，毫秒
		/// </summary>
		public static int ReConnectAutoActionCutdownTime { get { return _ReConnectAutoActionCutdownTime; } }

		private static int _RedPacketsDailyPickCount;
		/// <summary>
		/// 玩家每日可领取的红包数量限制
		/// </summary>
		public static int RedPacketsDailyPickCount { get { return _RedPacketsDailyPickCount; } }

		private static int _RenameItemId;
		/// <summary>
		/// 改名道具id
		/// </summary>
		public static int RenameItemId { get { return _RenameItemId; } }

		private static int _Repution;
		/// <summary>
		/// 声望ID
		/// </summary>
		public static int Repution { get { return _Repution; } }

		private static int _ResourceFloatingTextInterval;
		/// <summary>
		/// 拾取资源(经验,声望)冒字时间间隔, 毫秒
		/// </summary>
		public static int ResourceFloatingTextInterval { get { return _ResourceFloatingTextInterval; } }

		private static int _RoleGameMoneyMax;
		/// <summary>
		/// 角色最大游戏币上限
		/// </summary>
		public static int RoleGameMoneyMax { get { return _RoleGameMoneyMax; } }

		private static int _ServiceControl;
		/// <summary>
		/// 客服按钮控制显示
		/// </summary>
		public static int ServiceControl { get { return _ServiceControl; } }

		private static int _SignGoToNpcId;
		/// <summary>
		/// 签到寻路NPC的ID
		/// </summary>
		public static int SignGoToNpcId { get { return _SignGoToNpcId; } }

		private static int _SkillViewerBattleAvatarTemplateId;
		/// <summary>
		/// 技能查看器创建角色用ID
		/// </summary>
		public static int SkillViewerBattleAvatarTemplateId { get { return _SkillViewerBattleAvatarTemplateId; } }

		private static int _StaminaId;
		/// <summary>
		/// 体力id
		/// </summary>
		public static int StaminaId { get { return _StaminaId; } }

		private static int _StaminaRecoverInterval;
		/// <summary>
		/// 自然恢复体力的间隔时间(单位：秒)
		/// </summary>
		public static int StaminaRecoverInterval { get { return _StaminaRecoverInterval; } }

		private static int _StaminaRecoverQuantity;
		/// <summary>
		/// 每次自然恢复体力的量
		/// </summary>
		public static int StaminaRecoverQuantity { get { return _StaminaRecoverQuantity; } }

		private static int _StoreBlackRefreshCost;
		/// <summary>
		/// 黑店手动刷新花费
		/// </summary>
		public static int StoreBlackRefreshCost { get { return _StoreBlackRefreshCost; } }

		private static int _StoreBlackUnlock;
		/// <summary>
		/// 黑店开启条件，VIP等级 (废弃)
		/// </summary>
		public static int StoreBlackUnlock { get { return _StoreBlackUnlock; } }

		private static int _StoreDeluxeBlackRefreshCost;
		/// <summary>
		/// 高级黑店手动刷新花费
		/// </summary>
		public static int StoreDeluxeBlackRefreshCost { get { return _StoreDeluxeBlackRefreshCost; } }

		private static int _StoreDeluxeBlackUnlock;
		/// <summary>
		/// 高级黑店开启条件，VIP等级 (废弃)
		/// </summary>
		public static int StoreDeluxeBlackUnlock { get { return _StoreDeluxeBlackUnlock; } }

		private static int _StoreNormalRefreshCost;
		/// <summary>
		/// 商店手动刷新花费
		/// </summary>
		public static int StoreNormalRefreshCost { get { return _StoreNormalRefreshCost; } }

		private static int _StoreNormalUnlock;
		/// <summary>
		/// 普通商店开启条件，VIP等级 (废弃)
		/// </summary>
		public static int StoreNormalUnlock { get { return _StoreNormalUnlock; } }

		private static int _StoreRefreshCountResetDayOfWeek;
		/// <summary>
		/// 0123456(日一二三四五六)
		/// </summary>
		public static int StoreRefreshCountResetDayOfWeek { get { return _StoreRefreshCountResetDayOfWeek; } }

		private static int _StoryCompaignFightTimes;
		/// <summary>
		/// 剧情副本扫荡次数
		/// </summary>
		public static int StoryCompaignFightTimes { get { return _StoryCompaignFightTimes; } }

		private static int _TalkContentRefreshTimeSecond;
		/// <summary>
		/// 禁言生效的最小说话间隔
		/// </summary>
		public static int TalkContentRefreshTimeSecond { get { return _TalkContentRefreshTimeSecond; } }

		private static int _TalkForbidEndTimeSecond;
		/// <summary>
		/// 被禁言持续时间
		/// </summary>
		public static int TalkForbidEndTimeSecond { get { return _TalkForbidEndTimeSecond; } }

		private static int _TalkNoForbidVipLevel;
		/// <summary>
		/// 高于此VIP将不受禁言文字限制，但仍受敏感字限制
		/// </summary>
		public static int TalkNoForbidVipLevel { get { return _TalkNoForbidVipLevel; } }

		private static int _TimeLimitDungeonEmail;
		/// <summary>
		/// 限时副本邮件ID
		/// </summary>
		public static int TimeLimitDungeonEmail { get { return _TimeLimitDungeonEmail; } }

		private static int _TimeLimitDungeonFailedLimit;
		/// <summary>
		/// 限时副本失败限制次数
		/// </summary>
		public static int TimeLimitDungeonFailedLimit { get { return _TimeLimitDungeonFailedLimit; } }

		private static int _TimeLimitDungeonFirstDefaultId;
		/// <summary>
		/// 限时副本默认开始解锁关卡id
		/// </summary>
		public static int TimeLimitDungeonFirstDefaultId { get { return _TimeLimitDungeonFirstDefaultId; } }

		private static int _TimeLimitDungeonMax;
		/// <summary>
		/// 限时副本最高层数
		/// </summary>
		public static int TimeLimitDungeonMax { get { return _TimeLimitDungeonMax; } }

		private static int _TimeLimitDungeonRankCount;
		/// <summary>
		/// 限时副本排名人数
		/// </summary>
		public static int TimeLimitDungeonRankCount { get { return _TimeLimitDungeonRankCount; } }

		private static int _TotemNotDamageableTemplateId;
		/// <summary>
		/// 图腾（不可攻击）模板ID
		/// </summary>
		public static int TotemNotDamageableTemplateId { get { return _TotemNotDamageableTemplateId; } }

		private static int _TowerBattleTimesEachDay;
		/// <summary>
		/// 守塔任务每日可进入的战斗次数
		/// </summary>
		public static int TowerBattleTimesEachDay { get { return _TowerBattleTimesEachDay; } }

		private static int _TowerFreeRefreshCount;
		/// <summary>
		/// 守塔任务每日免费刷新次数
		/// </summary>
		public static int TowerFreeRefreshCount { get { return _TowerFreeRefreshCount; } }

		private static int _TowerGoldRefreshCost;
		/// <summary>
		/// 守塔任务每日消耗元宝数
		/// </summary>
		public static int TowerGoldRefreshCost { get { return _TowerGoldRefreshCost; } }

		private static int _TowerRefreshTime;
		/// <summary>
		/// 守塔任务自动刷新的时间间隔
		/// </summary>
		public static int TowerRefreshTime { get { return _TowerRefreshTime; } }

		private static int _TrialBossFailedRefreshMoney;
		/// <summary>
		/// 试炼boss失败刷新元宝数
		/// </summary>
		public static int TrialBossFailedRefreshMoney { get { return _TrialBossFailedRefreshMoney; } }

		private static int _TrialBossKilledRefreshMoney;
		/// <summary>
		/// 试炼boss成功刷新元宝数
		/// </summary>
		public static int TrialBossKilledRefreshMoney { get { return _TrialBossKilledRefreshMoney; } }

		private static int _TrialCount;
		/// <summary>
		/// 每日挑战次数
		/// </summary>
		public static int TrialCount { get { return _TrialCount; } }

		private static int _TrialFailedCooldown;
		/// <summary>
		/// 战斗失败刷新时间(秒)
		/// </summary>
		public static int TrialFailedCooldown { get { return _TrialFailedCooldown; } }

		private static int _TrialKillCombatCooldown;
		/// <summary>
		/// 战斗过程强杀刷新时间(秒)
		/// </summary>
		public static int TrialKillCombatCooldown { get { return _TrialKillCombatCooldown; } }

		private static int _UIPnlUseBoxMaxNum;
		/// <summary>
		/// 礼包单次使用最大数量限制
		/// </summary>
		public static int UIPnlUseBoxMaxNum { get { return _UIPnlUseBoxMaxNum; } }

		private static int _UIRoleInfoIgnoreCastingTime;
		/// <summary>
		/// 吟唱时间少于多少的话不显示吟唱条. 毫秒
		/// </summary>
		public static int UIRoleInfoIgnoreCastingTime { get { return _UIRoleInfoIgnoreCastingTime; } }

		private static int _UIRoleInfoIgnoreControlledTime;
		/// <summary>
		/// 受控时间少于多少的话不显示受控进度条. 毫秒
		/// </summary>
		public static int UIRoleInfoIgnoreControlledTime { get { return _UIRoleInfoIgnoreControlledTime; } }

		private static int _VoiceDialogEnableLength;
		/// <summary>
		/// 语音聊天允许发送的最小时间，单位毫秒（ms）
		/// </summary>
		public static int VoiceDialogEnableLength { get { return _VoiceDialogEnableLength; } }

		private static int _VoiceDialogEnableMaxLength;
		/// <summary>
		/// 语音聊天允许发送的最大时间，单位毫秒（ms）
		/// </summary>
		public static int VoiceDialogEnableMaxLength { get { return _VoiceDialogEnableMaxLength; } }

		private static int _WorldBossBattleCD;
		/// <summary>
		/// 世界BOSS_挑战冷却时间（秒
		/// </summary>
		public static int WorldBossBattleCD { get { return _WorldBossBattleCD; } }

		private static int _WorldBossBattleCDClearCost;
		/// <summary>
		/// 世界BOSS_清除冷却花费元宝
		/// </summary>
		public static int WorldBossBattleCDClearCost { get { return _WorldBossBattleCDClearCost; } }

		private static int _WorldBossBattleSence;
		/// <summary>
		/// 世界BOSS_BOSS阶段战斗场景	Sence_Config ID
		/// </summary>
		public static int WorldBossBattleSence { get { return _WorldBossBattleSence; } }

		private static int _WorldBossBattleTimeLimit;
		/// <summary>
		/// 世界BOSS_BOSS阶段战斗限时(秒)
		/// </summary>
		public static int WorldBossBattleTimeLimit { get { return _WorldBossBattleTimeLimit; } }

		private static int _WorldBossBeforeBattleTime;
		/// <summary>
		/// 世界BOSS_正式打boss前的时间
		/// </summary>
		public static int WorldBossBeforeBattleTime { get { return _WorldBossBeforeBattleTime; } }

		private static int _WorldBossDefaultBossId;
		/// <summary>
		/// 世界BOSS默认boss id
		/// </summary>
		public static int WorldBossDefaultBossId { get { return _WorldBossDefaultBossId; } }

		private static int _WorldBossLastRankNum;
		/// <summary>
		/// 世界BOSS_BOSS上一次挑战显示的排名数量
		/// </summary>
		public static int WorldBossLastRankNum { get { return _WorldBossLastRankNum; } }

		private static int _WorldBossPrepareBattleSence;
		/// <summary>
		/// 世界BOSS_筹备阶段战斗场景 Sence_Config ID
		/// </summary>
		public static int WorldBossPrepareBattleSence { get { return _WorldBossPrepareBattleSence; } }

		private static int _WorldBossPrepareBattleTimeLimit;
		/// <summary>
		/// 世界BOSS_筹备阶段战斗限时(秒)
		/// </summary>
		public static int WorldBossPrepareBattleTimeLimit { get { return _WorldBossPrepareBattleTimeLimit; } }

		private static int _WorldBossPrepareMaxNum;
		/// <summary>
		/// 世界BOSS_筹备阶段怪最大数量
		/// </summary>
		public static int WorldBossPrepareMaxNum { get { return _WorldBossPrepareMaxNum; } }

		private static int _WorldBossPrepareMinParticipant;
		/// <summary>
		/// 世界BOSS_筹备阶段最小参与人数
		/// </summary>
		public static int WorldBossPrepareMinParticipant { get { return _WorldBossPrepareMinParticipant; } }

		private static int _WorldBossStoreRefreshCost;
		/// <summary>
		/// 世界BOSS_侠义商店刷新花费元宝
		/// </summary>
		public static int WorldBossStoreRefreshCost { get { return _WorldBossStoreRefreshCost; } }

		private static int _WorldChatCDTime;
		/// <summary>
		/// 世界聊天的cd时间
		/// </summary>
		public static int WorldChatCDTime { get { return _WorldChatCDTime; } }

		private static int _WorldMapAnimLimitLevel;
		/// <summary>
		/// 出城按钮动画显示限制等级
		/// </summary>
		public static int WorldMapAnimLimitLevel { get { return _WorldMapAnimLimitLevel; } }

		private static int _WorldMapInertiaReduceFrames;
		/// <summary>
		/// 大地图, 当停止滑动后，惯性滑动m帧，在这些帧内，滑动值根据 inertiaMovementX 里的最大值获取，并递减
		/// </summary>
		public static int WorldMapInertiaReduceFrames { get { return _WorldMapInertiaReduceFrames; } }

		private static string _AutoFightEffect;
		/// <summary>
		/// 自动战斗特效
		/// </summary>
		public static string AutoFightEffect { get { return _AutoFightEffect; } }

		private static string _AvatarInformationMenu;
		/// <summary>
		/// 主界面角色按钮打开界面后的左侧标签
		/// </summary>
		public static string AvatarInformationMenu { get { return _AvatarInformationMenu; } }

		private static string _AvatarMoveEffectName;
		/// <summary>
		/// 角色跑动的时候的特效名称
		/// </summary>
		public static string AvatarMoveEffectName { get { return _AvatarMoveEffectName; } }

		private static string _BackBuildingModeName;
		/// <summary>
		/// 回城建筑模型
		/// </summary>
		public static string BackBuildingModeName { get { return _BackBuildingModeName; } }

		private static string _BackBuildingPos;
		/// <summary>
		/// 回出城建筑坐标
		/// </summary>
		public static string BackBuildingPos { get { return _BackBuildingPos; } }

		private static string _BagPages;
		/// <summary>
		/// 背包分页材料类型表
		/// </summary>
		public static string BagPages { get { return _BagPages; } }

		private static string _BaseRankRefreshWeeklyTime;
		/// <summary>
		/// 排行榜每周刷新时间
		/// </summary>
		public static string BaseRankRefreshWeeklyTime { get { return _BaseRankRefreshWeeklyTime; } }

		private static string _CentryCityName;
		/// <summary>
		/// 主城关卡名，请注意，要和sceneConfig里的id一致
		/// </summary>
		public static string CentryCityName { get { return _CentryCityName; } }

		private static string _ChatMessageSelfColor;
		/// <summary>
		/// 聊天的自己的颜色
		/// </summary>
		public static string ChatMessageSelfColor { get { return _ChatMessageSelfColor; } }

		private static string _ClickGroundEffect;
		/// <summary>
		/// 点地特效
		/// </summary>
		public static string ClickGroundEffect { get { return _ClickGroundEffect; } }

		private static string _ClientEquipmentLevelUpSuccessEffect;
		/// <summary>
		/// 装备升级成功特效
		/// </summary>
		public static string ClientEquipmentLevelUpSuccessEffect { get { return _ClientEquipmentLevelUpSuccessEffect; } }

		private static string _ClientEquipmentStarUpFailedEffect;
		/// <summary>
		/// 装备升星失败特效
		/// </summary>
		public static string ClientEquipmentStarUpFailedEffect { get { return _ClientEquipmentStarUpFailedEffect; } }

		private static string _ClientTreasureDescomposeSuccessEffect;
		/// <summary>
		/// 奇宝分解特效
		/// </summary>
		public static string ClientTreasureDescomposeSuccessEffect { get { return _ClientTreasureDescomposeSuccessEffect; } }

		private static string _ClientTreasureRepairedFailedEffect;
		/// <summary>
		/// 奇宝修复失败特效
		/// </summary>
		public static string ClientTreasureRepairedFailedEffect { get { return _ClientTreasureRepairedFailedEffect; } }

		private static string _ClientTreasureRepairedSuccessEffect;
		/// <summary>
		/// 奇宝修复成功特效
		/// </summary>
		public static string ClientTreasureRepairedSuccessEffect { get { return _ClientTreasureRepairedSuccessEffect; } }

		private static string _ClientTreasureSelectedEffect;
		/// <summary>
		/// 奇宝选中特效
		/// </summary>
		public static string ClientTreasureSelectedEffect { get { return _ClientTreasureSelectedEffect; } }

		private static string _CollectItemText;
		/// <summary>
		/// 拾取物品飘字的prefab资源名
		/// </summary>
		public static string CollectItemText { get { return _CollectItemText; } }

		private static string _CollectResourceText;
		/// <summary>
		/// 拾取(经验, 声望)头顶飘字的prefab资源名
		/// </summary>
		public static string CollectResourceText { get { return _CollectResourceText; } }

		private static string _DailyBenefitEffect;
		/// <summary>
		/// 每日福利可领取特效
		/// </summary>
		public static string DailyBenefitEffect { get { return _DailyBenefitEffect; } }

		private static string _DailyBenefitNpcId;
		/// <summary>
		/// 每日福利npcid
		/// </summary>
		public static string DailyBenefitNpcId { get { return _DailyBenefitNpcId; } }

		private static string _DailyMissionLivenessAndReward;
		/// <summary>
		/// 每日活跃度积分以及档位奖励id(eg:25;xId|50;xx?Id|75;xxxId|100;xxxxId)必须是4个档位
		/// </summary>
		public static string DailyMissionLivenessAndReward { get { return _DailyMissionLivenessAndReward; } }

		private static string _DailyMissionRefreshTime;
		/// <summary>
		/// 活跃度每日任务刷新时间
		/// </summary>
		public static string DailyMissionRefreshTime { get { return _DailyMissionRefreshTime; } }

		private static string _DamageParticleBone;
		/// <summary>
		/// 伤害特效绑定的骨骼名
		/// </summary>
		public static string DamageParticleBone { get { return _DamageParticleBone; } }

		private static string _DamageTextAbsorb;
		/// <summary>
		/// 显示吸收的飘字资源
		/// </summary>
		public static string DamageTextAbsorb { get { return _DamageTextAbsorb; } }

		private static string _DamageTextBlock;
		/// <summary>
		/// 显示免伤的飘字资源
		/// </summary>
		public static string DamageTextBlock { get { return _DamageTextBlock; } }

		private static string _DamageTextDurDamageEnemy;
		/// <summary>
		/// 对敌对的持续伤害
		/// </summary>
		public static string DamageTextDurDamageEnemy { get { return _DamageTextDurDamageEnemy; } }

		private static string _DamageTextDurDamageHero;
		/// <summary>
		/// 对玩家的持续伤害
		/// </summary>
		public static string DamageTextDurDamageHero { get { return _DamageTextDurDamageHero; } }

		private static string _DamageTextDurHeal;
		/// <summary>
		/// 持续加血
		/// </summary>
		public static string DamageTextDurHeal { get { return _DamageTextDurHeal; } }

		private static string _DamageTextHeal;
		/// <summary>
		/// 治疗飘字的prefab名
		/// </summary>
		public static string DamageTextHeal { get { return _DamageTextHeal; } }

		private static string _DamageTextHealCritical;
		/// <summary>
		/// 治疗暴击飘字的prefab名
		/// </summary>
		public static string DamageTextHealCritical { get { return _DamageTextHealCritical; } }

		private static string _DamageTextMiss;
		/// <summary>
		/// Miss飘字的prefab名
		/// </summary>
		public static string DamageTextMiss { get { return _DamageTextMiss; } }

		private static string _DamageTextReduce;
		/// <summary>
		/// 显示免伤的飘字资源
		/// </summary>
		public static string DamageTextReduce { get { return _DamageTextReduce; } }

		private static string _DamageTextReflect;
		/// <summary>
		/// 被反弹伤害的飘字资源
		/// </summary>
		public static string DamageTextReflect { get { return _DamageTextReflect; } }

		private static string _DamageTextSkill;
		/// <summary>
		/// 技能飘字的prefab名
		/// </summary>
		public static string DamageTextSkill { get { return _DamageTextSkill; } }

		private static string _DamageTextSkillCritical;
		/// <summary>
		/// 技能暴击飘字的prefab名
		/// </summary>
		public static string DamageTextSkillCritical { get { return _DamageTextSkillCritical; } }

		private static string _DamageTextSkillInterrupted;
		/// <summary>
		/// 技能被打断
		/// </summary>
		public static string DamageTextSkillInterrupted { get { return _DamageTextSkillInterrupted; } }

		private static string _DamageTextWeapon;
		/// <summary>
		/// 普攻飘字的prefab名
		/// </summary>
		public static string DamageTextWeapon { get { return _DamageTextWeapon; } }

		private static string _DamageTextWeaponCritical;
		/// <summary>
		/// 普攻暴击飘字的prefab名
		/// </summary>
		public static string DamageTextWeaponCritical { get { return _DamageTextWeaponCritical; } }

		private static string _DartDescription;
		/// <summary>
		/// 运镖说明
		/// </summary>
		public static string DartDescription { get { return _DartDescription; } }

		private static string _DungeonGuideArrowResName;
		/// <summary>
		/// 寻路引导 箭头的资源名
		/// </summary>
		public static string DungeonGuideArrowResName { get { return _DungeonGuideArrowResName; } }

		private static string _DungeonResetTime;
		/// <summary>
		/// 关卡重置时间
		/// </summary>
		public static string DungeonResetTime { get { return _DungeonResetTime; } }

		private static string _EmailDefaultSender;
		/// <summary>
		/// 0
		/// </summary>
		public static string EmailDefaultSender { get { return _EmailDefaultSender; } }

		private static string _EmailDefaultSenderIcon;
		/// <summary>
		/// 0
		/// </summary>
		public static string EmailDefaultSenderIcon { get { return _EmailDefaultSenderIcon; } }

		private static string _ExchangePage;
		/// <summary>
		/// 易物界面标签
		/// </summary>
		public static string ExchangePage { get { return _ExchangePage; } }

		private static string _FriendsBlessMessage;
		/// <summary>
		/// 好友祝福语
		/// </summary>
		public static string FriendsBlessMessage { get { return _FriendsBlessMessage; } }

		private static string _GemComposeSuccessEffect;
		/// <summary>
		/// 宝石合成成功特效
		/// </summary>
		public static string GemComposeSuccessEffect { get { return _GemComposeSuccessEffect; } }

		private static string _GoodsYuanBao;
		/// <summary>
		/// 充值: 元宝
		/// </summary>
		public static string GoodsYuanBao { get { return _GoodsYuanBao; } }

		/// <summary>
		/// 充值: 月卡
		/// </summary>
		private static string _GoodsYueKa;
		public static string GoodsYueKa { get { return _GoodsYueKa; } }

		private static string _GoodsZengSong;
		/// <summary>
		/// 充值：赠送
		/// </summary>
		public static string GoodsZengSong { get { return _GoodsZengSong; } }

		private static string _GraduateHigh;
		/// <summary>
		/// 出师推荐战斗力百分比最高阶段（这个是百分比100以上）
		/// </summary>
		public static string GraduateHigh { get { return _GraduateHigh; } }

		private static string _GraduateMiddle;
		/// <summary>
		/// 出师推荐战斗力百分比中间阶段（这个是百分比50-100）
		/// </summary>
		public static string GraduateMiddle { get { return _GraduateMiddle; } }

		private static string _GraduatePrimary;
		/// <summary>
		/// 出师推荐战斗力百分比最低阶段（这个是百分比0-50
		/// </summary>
		public static string GraduatePrimary { get { return _GraduatePrimary; } }

		private static string _IDamageFormula;
		/// <summary>
		/// 外攻伤害公式
		/// </summary>
		public static string IDamageFormula { get { return _IDamageFormula; } }

		private static string _KShaderMixFactorName;
		/// <summary>
		/// 受伤闪白时更改的shader变量，一般不要改
		/// </summary>
		public static string KShaderMixFactorName { get { return _KShaderMixFactorName; } }

		private static string _LeagueBossDungeonOpenTime;
		/// <summary>
		/// 帮派boss副本活动时间
		/// </summary>
		public static string LeagueBossDungeonOpenTime { get { return _LeagueBossDungeonOpenTime; } }

		private static string _LevelUpEffect;
		/// <summary>
		/// 主城主角升级特效
		/// </summary>
		public static string LevelUpEffect { get { return _LevelUpEffect; } }

		private static string _LightFactorByTime;
		/// <summary>
		/// 受伤闪白在每个时间片里的闪白值，这个字符串也决定了闪白的总时间
		/// </summary>
		public static string LightFactorByTime { get { return _LightFactorByTime; } }

		private static string _LoginAndSelectAreaMusicName;
		/// <summary>
		/// 登陆和选区的时候的背景音乐名
		/// </summary>
		public static string LoginAndSelectAreaMusicName { get { return _LoginAndSelectAreaMusicName; } }

		private static string _LootDropHeapCountPercent;
		/// <summary>
		/// 掉落分队的默认几率, 格式: 数量|几率|数量|几率
		/// </summary>
		public static string LootDropHeapCountPercent { get { return _LootDropHeapCountPercent; } }

		private static string _MentalTypeUpEffect;
		/// <summary>
		/// 心法进阶特效
		/// </summary>
		public static string MentalTypeUpEffect { get { return _MentalTypeUpEffect; } }

		private static string _MirrorArenaBattleResetTime;
		/// <summary>
		/// 镜像竞技场_挑战次数重置时间
		/// </summary>
		public static string MirrorArenaBattleResetTime { get { return _MirrorArenaBattleResetTime; } }

		private static string _MirrorArenaDefaultDummyGroupRank;
		/// <summary>
		/// 镜像竞技场_默认假人排名
		/// </summary>
		public static string MirrorArenaDefaultDummyGroupRank { get { return _MirrorArenaDefaultDummyGroupRank; } }

		private static string _MirrorArenaEachWeekRewardCalcTime;
		/// <summary>
		/// 镜像竞技场_每周奖励结算时间
		/// </summary>
		public static string MirrorArenaEachWeekRewardCalcTime { get { return _MirrorArenaEachWeekRewardCalcTime; } }

		private static string _MonsterCreaterActiveWaveAnimClip;
		/// <summary>
		/// 怪物生成器刷怪时的动画名
		/// </summary>
		public static string MonsterCreaterActiveWaveAnimClip { get { return _MonsterCreaterActiveWaveAnimClip; } }

		private static string _MonsterCreaterActiveWaveByOtherDeath;
		/// <summary>
		/// 怪物生成器提前刷新时的文本索引
		/// </summary>
		public static string MonsterCreaterActiveWaveByOtherDeath { get { return _MonsterCreaterActiveWaveByOtherDeath; } }

		private static string _MonsterCreaterIdleAnimClip;
		/// <summary>
		/// 怪物生成器idle时的动画名
		/// </summary>
		public static string MonsterCreaterIdleAnimClip { get { return _MonsterCreaterIdleAnimClip; } }

		private static string _New1v1LevelSection;
		/// <summary>
		/// 新1v1级别段分段及对应积分(！！！该字段作废！！！)
		/// </summary>
		public static string New1v1LevelSection { get { return _New1v1LevelSection; } }

		private static string _New1v1LevelSubtractScore;
		/// <summary>
		/// 新1v1等级差对应积分
		/// </summary>
		public static string New1v1LevelSubtractScore { get { return _New1v1LevelSubtractScore; } }

		private static string _New1v1OpenTimeRange;
		/// <summary>
		/// 新1v1开启关闭时间(必须是当天的，不能跨天)
		/// </summary>
		public static string New1v1OpenTimeRange { get { return _New1v1OpenTimeRange; } }

		private static string _New1v1SendRewardTime;
		/// <summary>
		/// 新1v1奖励结算时间
		/// </summary>
		public static string New1v1SendRewardTime { get { return _New1v1SendRewardTime; } }

		private static string _ODamageFormula;
		/// <summary>
		/// 内攻伤害公式
		/// </summary>
		public static string ODamageFormula { get { return _ODamageFormula; } }

		private static string _OnevsoneArenaChallengeTime;
		/// <summary>
		/// 1v1竞技场_挑战时段
		/// </summary>
		public static string OnevsoneArenaChallengeTime { get { return _OnevsoneArenaChallengeTime; } }

		private static string _OnevsoneArenaScoreFix;
		/// <summary>
		/// 1v1竞技场_积分差对应修正值(向上取)
		/// </summary>
		public static string OnevsoneArenaScoreFix { get { return _OnevsoneArenaScoreFix; } }

		private static string _OnevsOneDummyDetail;
		/// <summary>
		/// 1v1竞技场_假人组配置(玩家积分区间1;假人积区间1;假人组1,假人组2,假人组3|玩家积分区间2;假人积分区间2;假人组4,假人组5,假人组6|……)
		/// </summary>
		public static string OnevsOneDummyDetail { get { return _OnevsOneDummyDetail; } }

		private static string _OnevsOneMeetDummyCountRefreshTime;
		/// <summary>
		/// 1v1竞技场_挑战假人次数刷新时间
		/// </summary>
		public static string OnevsOneMeetDummyCountRefreshTime { get { return _OnevsOneMeetDummyCountRefreshTime; } }

		private static string _PlayerGuideList;
		/// <summary>
		/// 玩家指引顺序列表
		/// </summary>
		public static string PlayerGuideList { get { return _PlayerGuideList; } }

		private static string _QualityColorFive;
		/// <summary>
		/// 名字品质颜色5品质
		/// </summary>
		public static string QualityColorFive { get { return _QualityColorFive; } }

		private static string _QualityColorFour;
		/// <summary>
		/// 名字品质颜色4品质
		/// </summary>
		public static string QualityColorFour { get { return _QualityColorFour; } }

		private static string _QualityColorOne;
		/// <summary>
		/// 名字品质颜色1品质
		/// </summary>
		public static string QualityColorOne { get { return _QualityColorOne; } }

		private static string _QualityColorThree;
		/// <summary>
		/// 名字品质颜色3品质
		/// </summary>
		public static string QualityColorThree { get { return _QualityColorThree; } }

		private static string _QualityColorTwo;
		/// <summary>
		/// 名字品质颜色2品质
		/// </summary>
		public static string QualityColorTwo { get { return _QualityColorTwo; } }

		private static string _QualityColorZero;
		/// <summary>
		/// 名字品质颜色0品质
		/// </summary>
		public static string QualityColorZero { get { return _QualityColorZero; } }

		private static string _RandomMissionResetTime;
		/// <summary>
		/// 江湖缉拿每日重置时间
		/// </summary>
		public static string RandomMissionResetTime { get { return _RandomMissionResetTime; } }

		private static string _RoleDefaultShader;
		/// <summary>
		/// 英雄默认shader
		/// </summary>
		public static string RoleDefaultShader { get { return _RoleDefaultShader; } }

		private static string _RoleLevelRewardDetail;
		/// <summary>
		/// 新手等级礼包,等级;奖励包id|等级;奖励包id
		/// </summary>
		public static string RoleLevelRewardDetail { get { return _RoleLevelRewardDetail; } }

		private static string _RoleOutLineShader;
		/// <summary>
		/// 英雄描边shader
		/// </summary>
		public static string RoleOutLineShader { get { return _RoleOutLineShader; } }

		private static string _SelectDungonBGM;
		/// <summary>
		/// 选择副本场景的背景音乐
		/// </summary>
		public static string SelectDungonBGM { get { return _SelectDungonBGM; } }

		private static string _SelectedAura;
		/// <summary>
		/// 选中时脚下的光环
		/// </summary>
		public static string SelectedAura { get { return _SelectedAura; } }

		private static string _SkillLevelUpEffect;
		/// <summary>
		/// 技能升级特效
		/// </summary>
		public static string SkillLevelUpEffect { get { return _SkillLevelUpEffect; } }

		private static string _StaminaBuyResetTime;
		/// <summary>
		/// 体力购买重置时间点
		/// </summary>
		public static string StaminaBuyResetTime { get { return _StaminaBuyResetTime; } }

		private static string _StoreBlackDailyRefreshTime;
		/// <summary>
		/// 黑店每日刷新时间(|分割)
		/// </summary>
		public static string StoreBlackDailyRefreshTime { get { return _StoreBlackDailyRefreshTime; } }

		private static string _StoreCommonDailyRefreshTime;
		/// <summary>
		/// 普通商店每日刷新时间(|分割)
		/// </summary>
		public static string StoreCommonDailyRefreshTime { get { return _StoreCommonDailyRefreshTime; } }

		private static string _StoreDailyRefreshTime;
		/// <summary>
		/// 商店每日刷新时间(废弃)
		/// </summary>
		public static string StoreDailyRefreshTime { get { return _StoreDailyRefreshTime; } }

		private static string _StoreDeluxeBlackRefreshTime;
		/// <summary>
		/// 高级黑店每日刷新时间(|分割)
		/// </summary>
		public static string StoreDeluxeBlackRefreshTime { get { return _StoreDeluxeBlackRefreshTime; } }

		private static string _TestString;
		/// <summary>
		/// 0
		/// </summary>
		public static string TestString { get { return _TestString; } }

		private static string _TextSkillEffect;
		/// <summary>
		/// 技能升品界面标签
		/// </summary>
		public static string TextSkillEffect { get { return _TextSkillEffect; } }

		private static string _TextSkillNextLevelEffect;
		/// <summary>
		/// 技能升级界面标签
		/// </summary>
		public static string TextSkillNextLevelEffect { get { return _TextSkillNextLevelEffect; } }

		private static string _TextSkillNextTypeEffect;
		/// <summary>
		/// 技能升品界面标签
		/// </summary>
		public static string TextSkillNextTypeEffect { get { return _TextSkillNextTypeEffect; } }

		private static string _TimeLimitDungeonDesc;
		/// <summary>
		/// 限时副本描述文字
		/// </summary>
		public static string TimeLimitDungeonDesc { get { return _TimeLimitDungeonDesc; } }

		private static string _TimeLimitDungeonEmailContent;
		/// <summary>
		/// 限时副本邮件奖励正文
		/// </summary>
		public static string TimeLimitDungeonEmailContent { get { return _TimeLimitDungeonEmailContent; } }

		private static string _TimeLimitDungeonRefreshTime;
		/// <summary>
		/// 限时副本每日刷新时间
		/// </summary>
		public static string TimeLimitDungeonRefreshTime { get { return _TimeLimitDungeonRefreshTime; } }

		private static string _TimeZone;
		/// <summary>
		/// 时区啊时区
		/// </summary>
		public static string TimeZone { get { return _TimeZone; } }

		private static string _TitleBlock;
		/// <summary>
		/// 称号组
		/// </summary>
		public static string TitleBlock { get { return _TitleBlock; } }

		private static string _TrialRefreshTime;
		/// <summary>
		/// 击杀boss刷新时间
		/// </summary>
		public static string TrialRefreshTime { get { return _TrialRefreshTime; } }

		private static string _UILightFactorByTime;
		/// <summary>
		/// UI冷却结束闪白在每个时间片里的闪白值，这个字符串也决定了闪白的总时间
		/// </summary>
		public static string UILightFactorByTime { get { return _UILightFactorByTime; } }

		private static string _UIMainMenuButtons;
		/// <summary>
		/// 主界面所有功能都解锁后的按钮对应的menu_navigation表的ID
		/// </summary>
		public static string UIMainMenuButtons { get { return _UIMainMenuButtons; } }

		private static string _UIPnlAnnouncementAnnouncementURL;
		/// <summary>
		/// 获取公告信息的网址
		/// </summary>
		public static string UIPnlAnnouncementAnnouncementURL { get { return _UIPnlAnnouncementAnnouncementURL; } }

		private static string _UIPnlSignInAlreadySignLabel;
		/// <summary>
		/// 签到完毕显示文字
		/// </summary>
		public static string UIPnlSignInAlreadySignLabel { get { return _UIPnlSignInAlreadySignLabel; } }

		private static string _WorldBossDuration;
		/// <summary>
		/// 世界BOSS_BOSS阶段时间区间
		/// </summary>
		public static string WorldBossDuration { get { return _WorldBossDuration; } }

		private static string _WorldBossPrepareDuration;
		/// <summary>
		/// 世界BOSS_筹备阶段时间区间
		/// </summary>
		public static string WorldBossPrepareDuration { get { return _WorldBossPrepareDuration; } }

		private static string _WorldBossStoreRefreshTime;
		/// <summary>
		/// 世界BOSS_侠义商店自动刷新时间（|分割）
		/// </summary>
		public static string WorldBossStoreRefreshTime { get { return _WorldBossStoreRefreshTime; } }

		private static double _TestDouble;
		/// <summary>
		/// 0
		/// </summary>
		public static double TestDouble { get { return _TestDouble; } }

		public static void Initialize()
		{
#if UNITY_EDITOR
			try
			{
#endif
				attributeValueAP = Convert.ToSingle(GetValue("AttributeValueAP"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueAP");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				attributeValueASP = Convert.ToSingle(GetValue("AttributeValueASP"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueASP");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				attributeValueCDP = Convert.ToSingle(GetValue("AttributeValueCDP"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueCDP");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				attributeValueCP = Convert.ToSingle(GetValue("AttributeValueCP"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueCP");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueCRP = Convert.ToSingle(GetValue("AttributeValueCRP"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueCRP");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueDGP = Convert.ToSingle(GetValue("AttributeValueDGP"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueDGP");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueHITP = Convert.ToSingle(GetValue("AttributeValueHITP"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueHITP");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueHP = Convert.ToSingle(GetValue("AttributeValueHP"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueHP");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueIABP = Convert.ToSingle(GetValue("AttributeValueIABP"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueIABP");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueIDP = Convert.ToSingle(GetValue("AttributeValueIDP"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueIDP");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueOABP = Convert.ToSingle(GetValue("AttributeValueOABP"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueOABP");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueODP = Convert.ToSingle(GetValue("AttributeValueODP"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueODP");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueSP = Convert.ToSingle(GetValue("AttributeValueSP"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueSP");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttriubteValueAVOP = Convert.ToSingle(GetValue("AttriubteValueAVOP"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttriubteValueAVOP");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AudioRolloffMaxDistance = Convert.ToSingle(GetValue("AudioRolloffMaxDistance"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AudioRolloffMaxDistance");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AudioRolloffMinDistance = Convert.ToSingle(GetValue("AudioRolloffMinDistance"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AudioRolloffMinDistance");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_BaseRankActiveLevel = Convert.ToSingle(GetValue("BaseRankActiveLevel"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert BaseRankActiveLevel");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_BattleChangeTargetMaxSearchDistance = Convert.ToSingle(GetValue("BattleChangeTargetMaxSearchDistance"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert BattleChangeTargetMaxSearchDistance");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_BattleFarawayRoleAttackDistance = Convert.ToSingle(GetValue("BattleFarawayRoleAttackDistance"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert BattleFarawayRoleAttackDistance");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_BattleMaxDistanceForMoveToEnemyWhenStop = Convert.ToSingle(GetValue("BattleMaxDistanceForMoveToEnemyWhenStop"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert BattleMaxDistanceForMoveToEnemyWhenStop");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_BattleNearRoleAttackDistance = Convert.ToSingle(GetValue("BattleNearRoleAttackDistance"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert BattleNearRoleAttackDistance");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CameraMaxDistance = Convert.ToSingle(GetValue("CameraMaxDistance"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CameraMaxDistance");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CameraMaxDistanceBattle = Convert.ToSingle(GetValue("CameraMaxDistanceBattle"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CameraMaxDistanceBattle");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CameraMinDistance = Convert.ToSingle(GetValue("CameraMinDistance"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CameraMinDistance");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CameraMinDistanceBattle = Convert.ToSingle(GetValue("CameraMinDistanceBattle"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CameraMinDistanceBattle");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CameraTraceDistance = Convert.ToSingle(GetValue("CameraTraceDistance"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CameraTraceDistance");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CameraTraceDistanceBattle = Convert.ToSingle(GetValue("CameraTraceDistanceBattle"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CameraTraceDistanceBattle");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CameraXScaleToLoadingOtherPlayerAsset = Convert.ToSingle(GetValue("CameraXScaleToLoadingOtherPlayerAsset"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CameraXScaleToLoadingOtherPlayerAsset");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CameraYScaleToLoadingOtherPlayerAsset = Convert.ToSingle(GetValue("CameraYScaleToLoadingOtherPlayerAsset"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CameraYScaleToLoadingOtherPlayerAsset");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_ClickGroundEffectYOffset = Convert.ToSingle(GetValue("ClickGroundEffectYOffset"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert ClickGroundEffectYOffset");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CollectDistance = Convert.ToSingle(GetValue("CollectDistance"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CollectDistance");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CombatFormationRadius = Convert.ToSingle(GetValue("CombatFormationRadius"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CombatFormationRadius");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CombatReSelectTargetCurrentAddation = Convert.ToSingle(GetValue("CombatReSelectTargetCurrentAddation"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CombatReSelectTargetCurrentAddation");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CombatReSelectTargetDistance = Convert.ToSingle(GetValue("CombatReSelectTargetDistance"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CombatReSelectTargetDistance");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_ConnectGateServerTimeOutJudgmentTime = Convert.ToSingle(GetValue("ConnectGateServerTimeOutJudgmentTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert ConnectGateServerTimeOutJudgmentTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_ConstF = Convert.ToSingle(GetValue("ConstF"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert ConstF");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DefaultPotentialTargetRange = Convert.ToSingle(GetValue("DefaultPotentialTargetRange"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DefaultPotentialTargetRange");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DistanceToNpcDialog = Convert.ToSingle(GetValue("DistanceToNpcDialog"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DistanceToNpcDialog");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DropGravityYAcceleratedSpeed = Convert.ToSingle(GetValue("DropGravityYAcceleratedSpeed"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DropGravityYAcceleratedSpeed");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DropInitYSpeed = Convert.ToSingle(GetValue("DropInitYSpeed"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DropInitYSpeed");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DropMaxTime = Convert.ToSingle(GetValue("DropMaxTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DropMaxTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DropMinTime = Convert.ToSingle(GetValue("DropMinTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DropMinTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DropRadius = Convert.ToSingle(GetValue("DropRadius"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DropRadius");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DungeonGuideArrowVisibleDistance = Convert.ToSingle(GetValue("DungeonGuideArrowVisibleDistance"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DungeonGuideArrowVisibleDistance");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_KLightUpdateTimeSlice = Convert.ToSingle(GetValue("KLightUpdateTimeSlice"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert KLightUpdateTimeSlice");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_KUILightUpdateTimeSlice = Convert.ToSingle(GetValue("KUILightUpdateTimeSlice"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert KUILightUpdateTimeSlice");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MirrorArenaSyncNeedFightAddition = Convert.ToSingle(GetValue("MirrorArenaSyncNeedFightAddition"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MirrorArenaSyncNeedFightAddition");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MoveSpeedInCity = Convert.ToSingle(GetValue("MoveSpeedInCity"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MoveSpeedInCity");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MusicVolume = Convert.ToSingle(GetValue("MusicVolume"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MusicVolume");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_New1v1ScoreCoefficient = Convert.ToSingle(GetValue("New1v1ScoreCoefficient"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert New1v1ScoreCoefficient");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnHoldingNearDistance = Convert.ToSingle(GetValue("OnHoldingNearDistance"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnHoldingNearDistance");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnHoldingNearTimeSection = Convert.ToSingle(GetValue("OnHoldingNearTimeSection"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnHoldingNearTimeSection");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnHoldingSendPositionDeltaTime = Convert.ToSingle(GetValue("OnHoldingSendPositionDeltaTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnHoldingSendPositionDeltaTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_PvPInnerDamageScaler = Convert.ToSingle(GetValue("PvPInnerDamageScaler"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert PvPInnerDamageScaler");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_PvPOuterdamageScaler = Convert.ToSingle(GetValue("PvPOuterdamageScaler"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert PvPOuterdamageScaler");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_RunAnimMovementSpeed = Convert.ToSingle(GetValue("RunAnimMovementSpeed"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert RunAnimMovementSpeed");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_SoundVolume = Convert.ToSingle(GetValue("SoundVolume"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert SoundVolume");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TeamDungeonRefreshCoolTime = Convert.ToSingle(GetValue("TeamDungeonRefreshCoolTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TeamDungeonRefreshCoolTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TestFloat = Convert.ToSingle(GetValue("TestFloat"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TestFloat");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_UnitFollowDistance = Convert.ToSingle(GetValue("UnitFollowDistance"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert UnitFollowDistance");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_WorldBossHurtErrantryFactor = Convert.ToSingle(GetValue("WorldBossHurtErrantryFactor"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert WorldBossHurtErrantryFactor");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_WorldBossHurtGameMoneyFactor = Convert.ToSingle(GetValue("WorldBossHurtGameMoneyFactor"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert WorldBossHurtGameMoneyFactor");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_WorldBossRankErrantryFactor = Convert.ToSingle(GetValue("WorldBossRankErrantryFactor"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert WorldBossRankErrantryFactor");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueAPColor = Convert.ToInt32(GetValue("AttributeValueAPColor"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueAPColor");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueASPColor = Convert.ToInt32(GetValue("AttributeValueASPColor"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueASPColor");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueCDPColor = Convert.ToInt32(GetValue("AttributeValueCDPColor"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueCDPColor");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueCPColor = Convert.ToInt32(GetValue("AttributeValueCPColor"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueCPColor");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueCRPColor = Convert.ToInt32(GetValue("AttributeValueCRPColor"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueCRPColor");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueDGPColor = Convert.ToInt32(GetValue("AttributeValueDGPColor"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueDGPColor");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueHITPColor = Convert.ToInt32(GetValue("AttributeValueHITPColor"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueHITPColor");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueHPColor = Convert.ToInt32(GetValue("AttributeValueHPColor"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueHPColor");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueIABPColor = Convert.ToInt32(GetValue("AttributeValueIABPColor"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueIABPColor");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueIDPColor = Convert.ToInt32(GetValue("AttributeValueIDPColor"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueIDPColor");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueOABPColor = Convert.ToInt32(GetValue("AttributeValueOABPColor"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueOABPColor");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueODPColor = Convert.ToInt32(GetValue("AttributeValueODPColor"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueODPColor");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttributeValueSPColor = Convert.ToInt32(GetValue("AttributeValueSPColor"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttributeValueSPColor");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AttriubteValueAVOPColor = Convert.ToInt32(GetValue("AttriubteValueAVOPColor"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AttriubteValueAVOPColor");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AutoFightUseLevel = Convert.ToInt32(GetValue("AutoFightUseLevel"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AutoFightUseLevel");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_BaseRankConditionLevel = Convert.ToInt32(GetValue("BaseRankConditionLevel"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert BaseRankConditionLevel");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_BaseRankConditionLimit = Convert.ToInt32(GetValue("BaseRankConditionLimit"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert BaseRankConditionLimit");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_BaseRankFightValue = Convert.ToInt32(GetValue("BaseRankFightValue"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert BaseRankFightValue");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_BaseRankRankSize = Convert.ToInt32(GetValue("BaseRankRankSize"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert BaseRankRankSize");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_BattleAvatarTemplateId = Convert.ToInt32(GetValue("BattleAvatarTemplateId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert BattleAvatarTemplateId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_BattleEndShowingEffectTime = Convert.ToInt32(GetValue("BattleEndShowingEffectTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert BattleEndShowingEffectTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_BattleEnvPlayerTemplateId = Convert.ToInt32(GetValue("BattleEnvPlayerTemplateId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert BattleEnvPlayerTemplateId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_BattlePlayerTemplateId = Convert.ToInt32(GetValue("BattlePlayerTemplateId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert BattlePlayerTemplateId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CentryCitySceneId = Convert.ToInt32(GetValue("CentryCitySceneId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CentryCitySceneId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_ChatInputMaxCount = Convert.ToInt32(GetValue("ChatInputMaxCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert ChatInputMaxCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_ChatListLimitCount = Convert.ToInt32(GetValue("ChatListLimitCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert ChatListLimitCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CityAvatarTemplateId = Convert.ToInt32(GetValue("CityAvatarTemplateId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CityAvatarTemplateId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CombatAfterGameEndTimeOutTime = Convert.ToInt32(GetValue("CombatAfterGameEndTimeOutTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CombatAfterGameEndTimeOutTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CombatAllConnectionLosedTimeOutTime = Convert.ToInt32(GetValue("CombatAllConnectionLosedTimeOutTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CombatAllConnectionLosedTimeOutTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CombatBeforeStartGameTimeOutTime = Convert.ToInt32(GetValue("CombatBeforeStartGameTimeOutTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CombatBeforeStartGameTimeOutTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CombatFormationDeltaDegrees = Convert.ToInt32(GetValue("CombatFormationDeltaDegrees"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CombatFormationDeltaDegrees");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CombatMultiDungeonTimeLimit = Convert.ToInt32(GetValue("CombatMultiDungeonTimeLimit"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CombatMultiDungeonTimeLimit");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CombatRealTime1v1TimeLimit = Convert.ToInt32(GetValue("CombatRealTime1v1TimeLimit"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CombatRealTime1v1TimeLimit");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CombatWaitOtherPlayerConnectTimeOutTime = Convert.ToInt32(GetValue("CombatWaitOtherPlayerConnectTimeOutTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CombatWaitOtherPlayerConnectTimeOutTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_ConcentrateFireAbilityId = Convert.ToInt32(GetValue("ConcentrateFireAbilityId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert ConcentrateFireAbilityId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CreateAvatarTemplateId = Convert.ToInt32(GetValue("CreateAvatarTemplateId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CreateAvatarTemplateId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DamageTextOffsetY = Convert.ToInt32(GetValue("DamageTextOffsetY"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DamageTextOffsetY");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DartPeriod = Convert.ToInt32(GetValue("DartPeriod"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DartPeriod");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DefaultWeaponInCityID = Convert.ToInt32(GetValue("DefaultWeaponInCityID"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DefaultWeaponInCityID");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DefaultWeaponInDungeonID = Convert.ToInt32(GetValue("DefaultWeaponInDungeonID"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DefaultWeaponInDungeonID");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DemonstrationBattlePlayerDummyGroupId = Convert.ToInt32(GetValue("DemonstrationBattlePlayerDummyGroupId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DemonstrationBattlePlayerDummyGroupId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DemonstrationBattlePlayerGirlDummyGroupId = Convert.ToInt32(GetValue("DemonstrationBattlePlayerGirlDummyGroupId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DemonstrationBattlePlayerGirlDummyGroupId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DialogEableSendLevel = Convert.ToInt32(GetValue("DialogEableSendLevel"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DialogEableSendLevel");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DialogEableSendVipLevel = Convert.ToInt32(GetValue("DialogEableSendVipLevel"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DialogEableSendVipLevel");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DialogItemLimitCountFaction = Convert.ToInt32(GetValue("DialogItemLimitCountFaction"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DialogItemLimitCountFaction");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DialogItemLimitCountGM = Convert.ToInt32(GetValue("DialogItemLimitCountGM"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DialogItemLimitCountGM");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DialogItemLimitCountSystem = Convert.ToInt32(GetValue("DialogItemLimitCountSystem"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DialogItemLimitCountSystem");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DialogItemLimitCountTotal = Convert.ToInt32(GetValue("DialogItemLimitCountTotal"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DialogItemLimitCountTotal");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DialogItemLimitCountWorld = Convert.ToInt32(GetValue("DialogItemLimitCountWorld"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DialogItemLimitCountWorld");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DungeonFightConditionAutoHideTime = Convert.ToInt32(GetValue("DungeonFightConditionAutoHideTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DungeonFightConditionAutoHideTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DungeonGuideArrowRotationSpeed = Convert.ToInt32(GetValue("DungeonGuideArrowRotationSpeed"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DungeonGuideArrowRotationSpeed");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DungeonViewerAvatarTemplateId = Convert.ToInt32(GetValue("DungeonViewerAvatarTemplateId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DungeonViewerAvatarTemplateId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_EmailClientSize = Convert.ToInt32(GetValue("EmailClientSize"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert EmailClientSize");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_EmailMaxSize = Convert.ToInt32(GetValue("EmailMaxSize"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert EmailMaxSize");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_EquipmentBuyBelt = Convert.ToInt32(GetValue("EquipmentBuyBelt"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert EquipmentBuyBelt");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_EquipmentBuyCloth = Convert.ToInt32(GetValue("EquipmentBuyCloth"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert EquipmentBuyCloth");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_EquipmentBuyHat = Convert.ToInt32(GetValue("EquipmentBuyHat"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert EquipmentBuyHat");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_EquipmentBuyNecklace = Convert.ToInt32(GetValue("EquipmentBuyNecklace"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert EquipmentBuyNecklace");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_EquipmentBuyRing = Convert.ToInt32(GetValue("EquipmentBuyRing"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert EquipmentBuyRing");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_EquipmentBuyWeapon = Convert.ToInt32(GetValue("EquipmentBuyWeapon"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert EquipmentBuyWeapon");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_EquipmentPutonMinLevelBelt = Convert.ToInt32(GetValue("EquipmentPutonMinLevelBelt"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert EquipmentPutonMinLevelBelt");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_EquipmentPutonMinLevelCloth = Convert.ToInt32(GetValue("EquipmentPutonMinLevelCloth"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert EquipmentPutonMinLevelCloth");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_EquipmentPutonMinLevelHat = Convert.ToInt32(GetValue("EquipmentPutonMinLevelHat"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert EquipmentPutonMinLevelHat");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_EquipmentPutonMinLevelNecklace = Convert.ToInt32(GetValue("EquipmentPutonMinLevelNecklace"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert EquipmentPutonMinLevelNecklace");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_EquipmentPutonMinLevelRing = Convert.ToInt32(GetValue("EquipmentPutonMinLevelRing"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert EquipmentPutonMinLevelRing");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_EquipmentPutonMinLevelWeapon = Convert.ToInt32(GetValue("EquipmentPutonMinLevelWeapon"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert EquipmentPutonMinLevelWeapon");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_EquipmentRecastBeginCondition = Convert.ToInt32(GetValue("EquipmentRecastBeginCondition"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert EquipmentRecastBeginCondition");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_EquipmentRefineBeginCondition = Convert.ToInt32(GetValue("EquipmentRefineBeginCondition"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert EquipmentRefineBeginCondition");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_EquipmentRefineMaxLevel = Convert.ToInt32(GetValue("EquipmentRefineMaxLevel"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert EquipmentRefineMaxLevel");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_EquipmentReinforceBeginCondition = Convert.ToInt32(GetValue("EquipmentReinforceBeginCondition"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert EquipmentReinforceBeginCondition");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_EquipmentSlotCount = Convert.ToInt32(GetValue("EquipmentSlotCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert EquipmentSlotCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_Errantry = Convert.ToInt32(GetValue("Errantry"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert Errantry");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_Exp = Convert.ToInt32(GetValue("Exp"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert Exp");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_FastCleanDungeon = Convert.ToInt32(GetValue("FastCleanDungeon"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert FastCleanDungeon");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_FirstAcupointId = Convert.ToInt32(GetValue("FirstAcupointId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert FirstAcupointId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_FirstFakeBattleSceneId = Convert.ToInt32(GetValue("FirstFakeBattleSceneId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert FirstFakeBattleSceneId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_FriendsBlessDailyCount = Convert.ToInt32(GetValue("FriendsBlessDailyCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert FriendsBlessDailyCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_FriendsBlessGainStaminCount = Convert.ToInt32(GetValue("FriendsBlessGainStaminCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert FriendsBlessGainStaminCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_FriendsCdTime = Convert.ToInt32(GetValue("FriendsCdTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert FriendsCdTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_FriendsChatLimitCount = Convert.ToInt32(GetValue("FriendsChatLimitCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert FriendsChatLimitCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_FriendShipUpLimit = Convert.ToInt32(GetValue("FriendShipUpLimit"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert FriendShipUpLimit");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_FriendsLimitCount = Convert.ToInt32(GetValue("FriendsLimitCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert FriendsLimitCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_FriendsMsgCount = Convert.ToInt32(GetValue("FriendsMsgCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert FriendsMsgCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_FriendsPushLevel = Convert.ToInt32(GetValue("FriendsPushLevel"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert FriendsPushLevel");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_GameMoney = Convert.ToInt32(GetValue("GameMoney"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert GameMoney");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_GrowthFundBuyNeedRealMoney = Convert.ToInt32(GetValue("GrowthFundBuyNeedRealMoney"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert GrowthFundBuyNeedRealMoney");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_GrowthFundBuyNeedVip = Convert.ToInt32(GetValue("GrowthFundBuyNeedVip"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert GrowthFundBuyNeedVip");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_HeartPowerActiveLevel = Convert.ToInt32(GetValue("HeartPowerActiveLevel"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert HeartPowerActiveLevel");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_HeroFightMaxNum = Convert.ToInt32(GetValue("HeroFightMaxNum"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert HeroFightMaxNum");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_IdleOnceTimeSection = Convert.ToInt32(GetValue("IdleOnceTimeSection"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert IdleOnceTimeSection");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_InitialAttributeLevel = Convert.ToInt32(GetValue("InitialAttributeLevel"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert InitialAttributeLevel");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_InitRankGradId = Convert.ToInt32(GetValue("InitRankGradId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert InitRankGradId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_InvalidConfigValue = Convert.ToInt32(GetValue("InvalidConfigValue"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert InvalidConfigValue");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_LeagueApplyCount = Convert.ToInt32(GetValue("LeagueApplyCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert LeagueApplyCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_LeagueCreateLevel = Convert.ToInt32(GetValue("LeagueCreateLevel"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert LeagueCreateLevel");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_LeagueCreateRealMoney = Convert.ToInt32(GetValue("LeagueCreateRealMoney"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert LeagueCreateRealMoney");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_LeagueInviteCD = Convert.ToInt32(GetValue("LeagueInviteCD"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert LeagueInviteCD");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_LeagueMaxLevel = Convert.ToInt32(GetValue("LeagueMaxLevel"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert LeagueMaxLevel");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_LootExpItemId = Convert.ToInt32(GetValue("LootExpItemId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert LootExpItemId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_LootMoneyItemId = Convert.ToInt32(GetValue("LootMoneyItemId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert LootMoneyItemId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_LootRealMoneyItemId = Convert.ToInt32(GetValue("LootRealMoneyItemId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert LootRealMoneyItemId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_LootReputationItemId = Convert.ToInt32(GetValue("LootReputationItemId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert LootReputationItemId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MatrixCentreSlotCount = Convert.ToInt32(GetValue("MatrixCentreSlotCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MatrixCentreSlotCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MaxComeOnFxCount = Convert.ToInt32(GetValue("MaxComeOnFxCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MaxComeOnFxCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MaxConfigHint = Convert.ToInt32(GetValue("MaxConfigHint"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MaxConfigHint");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MaxInScreenShowingAvatarCount = Convert.ToInt32(GetValue("MaxInScreenShowingAvatarCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MaxInScreenShowingAvatarCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MaxLevel = Convert.ToInt32(GetValue("MaxLevel"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MaxLevel");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MaxStamina = Convert.ToInt32(GetValue("MaxStamina"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MaxStamina");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MirrorArenaBattleClearCoolCost = Convert.ToInt32(GetValue("MirrorArenaBattleClearCoolCost"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MirrorArenaBattleClearCoolCost");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MirrorArenaBattleCoolTime = Convert.ToInt32(GetValue("MirrorArenaBattleCoolTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MirrorArenaBattleCoolTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MirrorArenaBattleCount = Convert.ToInt32(GetValue("MirrorArenaBattleCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MirrorArenaBattleCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MirrorArenaBattleCountDownTime = Convert.ToInt32(GetValue("MirrorArenaBattleCountDownTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MirrorArenaBattleCountDownTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MirrorArenaBattleLimitTime = Convert.ToInt32(GetValue("MirrorArenaBattleLimitTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MirrorArenaBattleLimitTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MirrorArenaBattleRecordCount = Convert.ToInt32(GetValue("MirrorArenaBattleRecordCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MirrorArenaBattleRecordCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MirrorArenaBattleSceneId = Convert.ToInt32(GetValue("MirrorArenaBattleSceneId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MirrorArenaBattleSceneId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MirrorArenaBuyBattleCountCost = Convert.ToInt32(GetValue("MirrorArenaBuyBattleCountCost"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MirrorArenaBuyBattleCountCost");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MirrorArenaEachDayRewardCount = Convert.ToInt32(GetValue("MirrorArenaEachDayRewardCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MirrorArenaEachDayRewardCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MirrorArenaNormalRefreshCost = Convert.ToInt32(GetValue("MirrorArenaNormalRefreshCost"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MirrorArenaNormalRefreshCost");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MirrorArenaNormalRefreshCount = Convert.ToInt32(GetValue("MirrorArenaNormalRefreshCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MirrorArenaNormalRefreshCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MirrorArenaNormalRefreshRange = Convert.ToInt32(GetValue("MirrorArenaNormalRefreshRange"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MirrorArenaNormalRefreshRange");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MirrorArenaVipRefreshCost = Convert.ToInt32(GetValue("MirrorArenaVipRefreshCost"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MirrorArenaVipRefreshCost");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MirrorArenaVipRefreshRange = Convert.ToInt32(GetValue("MirrorArenaVipRefreshRange"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MirrorArenaVipRefreshRange");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MonsterCreaterTemplateId = Convert.ToInt32(GetValue("MonsterCreaterTemplateId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MonsterCreaterTemplateId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MonsterPatrolIdleMs = Convert.ToInt32(GetValue("MonsterPatrolIdleMs"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MonsterPatrolIdleMs");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MonthcardBuyCountLimit = Convert.ToInt32(GetValue("MonthcardBuyCountLimit"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MonthcardBuyCountLimit");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MultiDungeonMaxShowGroupCount = Convert.ToInt32(GetValue("MultiDungeonMaxShowGroupCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MultiDungeonMaxShowGroupCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MultiDungeonResetAwardCount = Convert.ToInt32(GetValue("MultiDungeonResetAwardCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MultiDungeonResetAwardCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_NameMaxLength = Convert.ToInt32(GetValue("NameMaxLength"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert NameMaxLength");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_New1v1ArenaSceneId = Convert.ToInt32(GetValue("New1v1ArenaSceneId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert New1v1ArenaSceneId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_New1v1FirstCombatDummyId = Convert.ToInt32(GetValue("New1v1FirstCombatDummyId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert New1v1FirstCombatDummyId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_New1v1FixPerTime = Convert.ToInt32(GetValue("New1v1FixPerTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert New1v1FixPerTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_New1v1FloorEmailId = Convert.ToInt32(GetValue("New1v1FloorEmailId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert New1v1FloorEmailId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_New1v1FloorScore = Convert.ToInt32(GetValue("New1v1FloorScore"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert New1v1FloorScore");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_New1v1LoseScore = Convert.ToInt32(GetValue("New1v1LoseScore"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert New1v1LoseScore");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_New1v1MatchTime = Convert.ToInt32(GetValue("New1v1MatchTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert New1v1MatchTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_New1v1PerTimeFixLevel = Convert.ToInt32(GetValue("New1v1PerTimeFixLevel"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert New1v1PerTimeFixLevel");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_New1v1RankMinScore = Convert.ToInt32(GetValue("New1v1RankMinScore"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert New1v1RankMinScore");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_New1v1SceneId = Convert.ToInt32(GetValue("New1v1SceneId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert New1v1SceneId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_New1v1WinScore = Convert.ToInt32(GetValue("New1v1WinScore"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert New1v1WinScore");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_NpcCollectionTemplateId = Convert.ToInt32(GetValue("NpcCollectionTemplateId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert NpcCollectionTemplateId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_NpcTemplateId = Convert.ToInt32(GetValue("NpcTemplateId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert NpcTemplateId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_NullDataValue = Convert.ToInt32(GetValue("NullDataValue"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert NullDataValue");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnevsoneArenaCooldown = Convert.ToInt32(GetValue("OnevsoneArenaCooldown"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnevsoneArenaCooldown");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnevsoneArenaLimitTime = Convert.ToInt32(GetValue("OnevsoneArenaLimitTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnevsoneArenaLimitTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnevsoneArenaMatchRangeFix = Convert.ToInt32(GetValue("OnevsoneArenaMatchRangeFix"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnevsoneArenaMatchRangeFix");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnevsoneArenaMatchTimeMax = Convert.ToInt32(GetValue("OnevsoneArenaMatchTimeMax"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnevsoneArenaMatchTimeMax");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnevsoneArenaMatchTimeNormal = Convert.ToInt32(GetValue("OnevsoneArenaMatchTimeNormal"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnevsoneArenaMatchTimeNormal");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnevsoneArenaMaxRecord = Convert.ToInt32(GetValue("OnevsoneArenaMaxRecord"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnevsoneArenaMaxRecord");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnevsoneArenaOpenEmail = Convert.ToInt32(GetValue("OnevsoneArenaOpenEmail"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnevsoneArenaOpenEmail");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnevsoneArenaRankShow = Convert.ToInt32(GetValue("OnevsoneArenaRankShow"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnevsoneArenaRankShow");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnevsoneArenaSceneId = Convert.ToInt32(GetValue("OnevsoneArenaSceneId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnevsoneArenaSceneId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnevsoneArenaScoreInit = Convert.ToInt32(GetValue("OnevsoneArenaScoreInit"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnevsoneArenaScoreInit");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnevsoneArenaScoreMax = Convert.ToInt32(GetValue("OnevsoneArenaScoreMax"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnevsoneArenaScoreMax");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnevsoneArenaScoreMin = Convert.ToInt32(GetValue("OnevsoneArenaScoreMin"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnevsoneArenaScoreMin");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnevsoneArenaSeasonDuration = Convert.ToInt32(GetValue("OnevsoneArenaSeasonDuration"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnevsoneArenaSeasonDuration");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnevsoneArenaStraightBroadcast = Convert.ToInt32(GetValue("OnevsoneArenaStraightBroadcast"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnevsoneArenaStraightBroadcast");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnevsOneCanMeetDummyCount = Convert.ToInt32(GetValue("OnevsOneCanMeetDummyCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnevsOneCanMeetDummyCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OneVsOneClearCoolTimeCost = Convert.ToInt32(GetValue("OneVsOneClearCoolTimeCost"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OneVsOneClearCoolTimeCost");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnevsOneOpenNextSeasonBreakTime = Convert.ToInt32(GetValue("OnevsOneOpenNextSeasonBreakTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnevsOneOpenNextSeasonBreakTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OpenChangeHero = Convert.ToInt32(GetValue("OpenChangeHero"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OpenChangeHero");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OpenFriends = Convert.ToInt32(GetValue("OpenFriends"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OpenFriends");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OpenHeartPower = Convert.ToInt32(GetValue("OpenHeartPower"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OpenHeartPower");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OpenHire = Convert.ToInt32(GetValue("OpenHire"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OpenHire");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OpenJewelCompose = Convert.ToInt32(GetValue("OpenJewelCompose"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OpenJewelCompose");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OpenRealm = Convert.ToInt32(GetValue("OpenRealm"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OpenRealm");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OpenStore = Convert.ToInt32(GetValue("OpenStore"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OpenStore");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OpenTimeLimitDungeon = Convert.ToInt32(GetValue("OpenTimeLimitDungeon"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OpenTimeLimitDungeon");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OpenTreasure = Convert.ToInt32(GetValue("OpenTreasure"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OpenTreasure");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OpenTutor = Convert.ToInt32(GetValue("OpenTutor"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OpenTutor");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OpenWorldChatLevel = Convert.ToInt32(GetValue("OpenWorldChatLevel"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OpenWorldChatLevel");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OpenWorldChatVIPLevel = Convert.ToInt32(GetValue("OpenWorldChatVIPLevel"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OpenWorldChatVIPLevel");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_PropertyRateDivisionFactor = Convert.ToInt32(GetValue("PropertyRateDivisionFactor"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert PropertyRateDivisionFactor");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_RandomMissionCanDoneCount = Convert.ToInt32(GetValue("RandomMissionCanDoneCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert RandomMissionCanDoneCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_RandomMissionReceiveCount = Convert.ToInt32(GetValue("RandomMissionReceiveCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert RandomMissionReceiveCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_RandomMissionRefreshApart = Convert.ToInt32(GetValue("RandomMissionRefreshApart"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert RandomMissionRefreshApart");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_RandomMissionRefreshCost = Convert.ToInt32(GetValue("RandomMissionRefreshCost"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert RandomMissionRefreshCost");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_RandomMissionRefreshCount = Convert.ToInt32(GetValue("RandomMissionRefreshCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert RandomMissionRefreshCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_RandomMissionRefreshFree = Convert.ToInt32(GetValue("RandomMissionRefreshFree"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert RandomMissionRefreshFree");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_RandomMissionRefreshTimesLimit = Convert.ToInt32(GetValue("RandomMissionRefreshTimesLimit"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert RandomMissionRefreshTimesLimit");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_RealMoney = Convert.ToInt32(GetValue("RealMoney"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert RealMoney");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_RealTime1v1BattleCountDownTime = Convert.ToInt32(GetValue("RealTime1v1BattleCountDownTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert RealTime1v1BattleCountDownTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_ReConnectAutoActionCutdownTime = Convert.ToInt32(GetValue("ReConnectAutoActionCutdownTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert ReConnectAutoActionCutdownTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_RedPacketsDailyPickCount = Convert.ToInt32(GetValue("RedPacketsDailyPickCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert RedPacketsDailyPickCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_RenameItemId = Convert.ToInt32(GetValue("RenameItemId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert RenameItemId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_Repution = Convert.ToInt32(GetValue("Repution"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert Repution");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_ResourceFloatingTextInterval = Convert.ToInt32(GetValue("ResourceFloatingTextInterval"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert ResourceFloatingTextInterval");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_RoleGameMoneyMax = Convert.ToInt32(GetValue("RoleGameMoneyMax"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert RoleGameMoneyMax");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_ServiceControl = Convert.ToInt32(GetValue("ServiceControl"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert ServiceControl");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_SignGoToNpcId = Convert.ToInt32(GetValue("SignGoToNpcId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert SignGoToNpcId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_SkillViewerBattleAvatarTemplateId = Convert.ToInt32(GetValue("SkillViewerBattleAvatarTemplateId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert SkillViewerBattleAvatarTemplateId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_StaminaId = Convert.ToInt32(GetValue("StaminaId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert StaminaId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_StaminaRecoverInterval = Convert.ToInt32(GetValue("StaminaRecoverInterval"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert StaminaRecoverInterval");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_StaminaRecoverQuantity = Convert.ToInt32(GetValue("StaminaRecoverQuantity"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert StaminaRecoverQuantity");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_StoreBlackRefreshCost = Convert.ToInt32(GetValue("StoreBlackRefreshCost"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert StoreBlackRefreshCost");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_StoreBlackUnlock = Convert.ToInt32(GetValue("StoreBlackUnlock"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert StoreBlackUnlock");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_StoreDeluxeBlackRefreshCost = Convert.ToInt32(GetValue("StoreDeluxeBlackRefreshCost"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert StoreDeluxeBlackRefreshCost");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_StoreDeluxeBlackUnlock = Convert.ToInt32(GetValue("StoreDeluxeBlackUnlock"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert StoreDeluxeBlackUnlock");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_StoreNormalRefreshCost = Convert.ToInt32(GetValue("StoreNormalRefreshCost"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert StoreNormalRefreshCost");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_StoreNormalUnlock = Convert.ToInt32(GetValue("StoreNormalUnlock"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert StoreNormalUnlock");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_StoreRefreshCountResetDayOfWeek = Convert.ToInt32(GetValue("StoreRefreshCountResetDayOfWeek"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert StoreRefreshCountResetDayOfWeek");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_StoryCompaignFightTimes = Convert.ToInt32(GetValue("StoryCompaignFightTimes"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert StoryCompaignFightTimes");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TalkContentRefreshTimeSecond = Convert.ToInt32(GetValue("TalkContentRefreshTimeSecond"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TalkContentRefreshTimeSecond");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TalkForbidEndTimeSecond = Convert.ToInt32(GetValue("TalkForbidEndTimeSecond"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TalkForbidEndTimeSecond");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TalkNoForbidVipLevel = Convert.ToInt32(GetValue("TalkNoForbidVipLevel"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TalkNoForbidVipLevel");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TimeLimitDungeonEmail = Convert.ToInt32(GetValue("TimeLimitDungeonEmail"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TimeLimitDungeonEmail");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TimeLimitDungeonFailedLimit = Convert.ToInt32(GetValue("TimeLimitDungeonFailedLimit"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TimeLimitDungeonFailedLimit");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TimeLimitDungeonFirstDefaultId = Convert.ToInt32(GetValue("TimeLimitDungeonFirstDefaultId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TimeLimitDungeonFirstDefaultId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TimeLimitDungeonMax = Convert.ToInt32(GetValue("TimeLimitDungeonMax"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TimeLimitDungeonMax");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TimeLimitDungeonRankCount = Convert.ToInt32(GetValue("TimeLimitDungeonRankCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TimeLimitDungeonRankCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TotemNotDamageableTemplateId = Convert.ToInt32(GetValue("TotemNotDamageableTemplateId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TotemNotDamageableTemplateId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TowerBattleTimesEachDay = Convert.ToInt32(GetValue("TowerBattleTimesEachDay"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TowerBattleTimesEachDay");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TowerFreeRefreshCount = Convert.ToInt32(GetValue("TowerFreeRefreshCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TowerFreeRefreshCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TowerGoldRefreshCost = Convert.ToInt32(GetValue("TowerGoldRefreshCost"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TowerGoldRefreshCost");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TowerRefreshTime = Convert.ToInt32(GetValue("TowerRefreshTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TowerRefreshTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TrialBossFailedRefreshMoney = Convert.ToInt32(GetValue("TrialBossFailedRefreshMoney"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TrialBossFailedRefreshMoney");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TrialBossKilledRefreshMoney = Convert.ToInt32(GetValue("TrialBossKilledRefreshMoney"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TrialBossKilledRefreshMoney");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TrialCount = Convert.ToInt32(GetValue("TrialCount"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TrialCount");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TrialFailedCooldown = Convert.ToInt32(GetValue("TrialFailedCooldown"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TrialFailedCooldown");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TrialKillCombatCooldown = Convert.ToInt32(GetValue("TrialKillCombatCooldown"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TrialKillCombatCooldown");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_UIPnlUseBoxMaxNum = Convert.ToInt32(GetValue("UIPnlUseBoxMaxNum"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert UIPnlUseBoxMaxNum");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_UIRoleInfoIgnoreCastingTime = Convert.ToInt32(GetValue("UIRoleInfoIgnoreCastingTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert UIRoleInfoIgnoreCastingTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_UIRoleInfoIgnoreControlledTime = Convert.ToInt32(GetValue("UIRoleInfoIgnoreControlledTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert UIRoleInfoIgnoreControlledTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_VoiceDialogEnableLength = Convert.ToInt32(GetValue("VoiceDialogEnableLength"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert VoiceDialogEnableLength");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_VoiceDialogEnableMaxLength = Convert.ToInt32(GetValue("VoiceDialogEnableMaxLength"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert VoiceDialogEnableMaxLength");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_WorldBossBattleCD = Convert.ToInt32(GetValue("WorldBossBattleCD"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert WorldBossBattleCD");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_WorldBossBattleCDClearCost = Convert.ToInt32(GetValue("WorldBossBattleCDClearCost"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert WorldBossBattleCDClearCost");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_WorldBossBattleSence = Convert.ToInt32(GetValue("WorldBossBattleSence"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert WorldBossBattleSence");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_WorldBossBattleTimeLimit = Convert.ToInt32(GetValue("WorldBossBattleTimeLimit"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert WorldBossBattleTimeLimit");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_WorldBossBeforeBattleTime = Convert.ToInt32(GetValue("WorldBossBeforeBattleTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert WorldBossBeforeBattleTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_WorldBossDefaultBossId = Convert.ToInt32(GetValue("WorldBossDefaultBossId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert WorldBossDefaultBossId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_WorldBossLastRankNum = Convert.ToInt32(GetValue("WorldBossLastRankNum"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert WorldBossLastRankNum");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_WorldBossPrepareBattleSence = Convert.ToInt32(GetValue("WorldBossPrepareBattleSence"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert WorldBossPrepareBattleSence");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_WorldBossPrepareBattleTimeLimit = Convert.ToInt32(GetValue("WorldBossPrepareBattleTimeLimit"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert WorldBossPrepareBattleTimeLimit");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_WorldBossPrepareMaxNum = Convert.ToInt32(GetValue("WorldBossPrepareMaxNum"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert WorldBossPrepareMaxNum");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_WorldBossPrepareMinParticipant = Convert.ToInt32(GetValue("WorldBossPrepareMinParticipant"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert WorldBossPrepareMinParticipant");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_WorldBossStoreRefreshCost = Convert.ToInt32(GetValue("WorldBossStoreRefreshCost"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert WorldBossStoreRefreshCost");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_WorldChatCDTime = Convert.ToInt32(GetValue("WorldChatCDTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert WorldChatCDTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_WorldMapAnimLimitLevel = Convert.ToInt32(GetValue("WorldMapAnimLimitLevel"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert WorldMapAnimLimitLevel");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_WorldMapInertiaReduceFrames = Convert.ToInt32(GetValue("WorldMapInertiaReduceFrames"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert WorldMapInertiaReduceFrames");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AutoFightEffect = Convert.ToString(GetValue("AutoFightEffect"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AutoFightEffect");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AvatarInformationMenu = Convert.ToString(GetValue("AvatarInformationMenu"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AvatarInformationMenu");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_AvatarMoveEffectName = Convert.ToString(GetValue("AvatarMoveEffectName"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert AvatarMoveEffectName");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_BackBuildingModeName = Convert.ToString(GetValue("BackBuildingModeName"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert BackBuildingModeName");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_BackBuildingPos = Convert.ToString(GetValue("BackBuildingPos"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert BackBuildingPos");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_BagPages = Convert.ToString(GetValue("BagPages"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert BagPages");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_BaseRankRefreshWeeklyTime = Convert.ToString(GetValue("BaseRankRefreshWeeklyTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert BaseRankRefreshWeeklyTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CentryCityName = Convert.ToString(GetValue("CentryCityName"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CentryCityName");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_ChatMessageSelfColor = Convert.ToString(GetValue("ChatMessageSelfColor"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert ChatMessageSelfColor");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_ClickGroundEffect = Convert.ToString(GetValue("ClickGroundEffect"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert ClickGroundEffect");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_ClientEquipmentLevelUpSuccessEffect = Convert.ToString(GetValue("ClientEquipmentLevelUpSuccessEffect"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert ClientEquipmentLevelUpSuccessEffect");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_ClientEquipmentStarUpFailedEffect = Convert.ToString(GetValue("ClientEquipmentStarUpFailedEffect"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert ClientEquipmentStarUpFailedEffect");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_ClientTreasureDescomposeSuccessEffect = Convert.ToString(GetValue("ClientTreasureDescomposeSuccessEffect"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert ClientTreasureDescomposeSuccessEffect");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_ClientTreasureRepairedFailedEffect = Convert.ToString(GetValue("ClientTreasureRepairedFailedEffect"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert ClientTreasureRepairedFailedEffect");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_ClientTreasureRepairedSuccessEffect = Convert.ToString(GetValue("ClientTreasureRepairedSuccessEffect"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert ClientTreasureRepairedSuccessEffect");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_ClientTreasureSelectedEffect = Convert.ToString(GetValue("ClientTreasureSelectedEffect"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert ClientTreasureSelectedEffect");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CollectItemText = Convert.ToString(GetValue("CollectItemText"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CollectItemText");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_CollectResourceText = Convert.ToString(GetValue("CollectResourceText"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert CollectResourceText");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DailyBenefitEffect = Convert.ToString(GetValue("DailyBenefitEffect"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DailyBenefitEffect");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DailyBenefitNpcId = Convert.ToString(GetValue("DailyBenefitNpcId"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DailyBenefitNpcId");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DailyMissionLivenessAndReward = Convert.ToString(GetValue("DailyMissionLivenessAndReward"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DailyMissionLivenessAndReward");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DailyMissionRefreshTime = Convert.ToString(GetValue("DailyMissionRefreshTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DailyMissionRefreshTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DamageParticleBone = Convert.ToString(GetValue("DamageParticleBone"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DamageParticleBone");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DamageTextAbsorb = Convert.ToString(GetValue("DamageTextAbsorb"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DamageTextAbsorb");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DamageTextBlock = Convert.ToString(GetValue("DamageTextBlock"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DamageTextBlock");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DamageTextDurDamageEnemy = Convert.ToString(GetValue("DamageTextDurDamageEnemy"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DamageTextDurDamageEnemy");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DamageTextDurDamageHero = Convert.ToString(GetValue("DamageTextDurDamageHero"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DamageTextDurDamageHero");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DamageTextDurHeal = Convert.ToString(GetValue("DamageTextDurHeal"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DamageTextDurHeal");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DamageTextHeal = Convert.ToString(GetValue("DamageTextHeal"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DamageTextHeal");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DamageTextHealCritical = Convert.ToString(GetValue("DamageTextHealCritical"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DamageTextHealCritical");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DamageTextMiss = Convert.ToString(GetValue("DamageTextMiss"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DamageTextMiss");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DamageTextReduce = Convert.ToString(GetValue("DamageTextReduce"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DamageTextReduce");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DamageTextReflect = Convert.ToString(GetValue("DamageTextReflect"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DamageTextReflect");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DamageTextSkill = Convert.ToString(GetValue("DamageTextSkill"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DamageTextSkill");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DamageTextSkillCritical = Convert.ToString(GetValue("DamageTextSkillCritical"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DamageTextSkillCritical");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DamageTextSkillInterrupted = Convert.ToString(GetValue("DamageTextSkillInterrupted"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DamageTextSkillInterrupted");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DamageTextWeapon = Convert.ToString(GetValue("DamageTextWeapon"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DamageTextWeapon");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DamageTextWeaponCritical = Convert.ToString(GetValue("DamageTextWeaponCritical"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DamageTextWeaponCritical");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DartDescription = Convert.ToString(GetValue("DartDescription"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DartDescription");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DungeonGuideArrowResName = Convert.ToString(GetValue("DungeonGuideArrowResName"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DungeonGuideArrowResName");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_DungeonResetTime = Convert.ToString(GetValue("DungeonResetTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert DungeonResetTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_EmailDefaultSender = Convert.ToString(GetValue("EmailDefaultSender"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert EmailDefaultSender");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_EmailDefaultSenderIcon = Convert.ToString(GetValue("EmailDefaultSenderIcon"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert EmailDefaultSenderIcon");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_ExchangePage = Convert.ToString(GetValue("ExchangePage"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert ExchangePage");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_FriendsBlessMessage = Convert.ToString(GetValue("FriendsBlessMessage"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert FriendsBlessMessage");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_GemComposeSuccessEffect = Convert.ToString(GetValue("GemComposeSuccessEffect"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert GemComposeSuccessEffect");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_GoodsYuanBao = Convert.ToString(GetValue("GoodsYuanBao"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert GoodsYuanBao");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_GoodsYueKa = Convert.ToString(GetValue("GoodsYueKa"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert GoodsYueKa");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_GoodsZengSong = Convert.ToString(GetValue("GoodsZengSong"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert GoodsZengSong");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_GraduateHigh = Convert.ToString(GetValue("GraduateHigh"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert GraduateHigh");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_GraduateMiddle = Convert.ToString(GetValue("GraduateMiddle"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert GraduateMiddle");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_GraduatePrimary = Convert.ToString(GetValue("GraduatePrimary"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert GraduatePrimary");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_IDamageFormula = Convert.ToString(GetValue("IDamageFormula"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert IDamageFormula");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_KShaderMixFactorName = Convert.ToString(GetValue("KShaderMixFactorName"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert KShaderMixFactorName");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_LeagueBossDungeonOpenTime = Convert.ToString(GetValue("LeagueBossDungeonOpenTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert LeagueBossDungeonOpenTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_LevelUpEffect = Convert.ToString(GetValue("LevelUpEffect"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert LevelUpEffect");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_LightFactorByTime = Convert.ToString(GetValue("LightFactorByTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert LightFactorByTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_LoginAndSelectAreaMusicName = Convert.ToString(GetValue("LoginAndSelectAreaMusicName"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert LoginAndSelectAreaMusicName");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_LootDropHeapCountPercent = Convert.ToString(GetValue("LootDropHeapCountPercent"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert LootDropHeapCountPercent");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MentalTypeUpEffect = Convert.ToString(GetValue("MentalTypeUpEffect"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MentalTypeUpEffect");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MirrorArenaBattleResetTime = Convert.ToString(GetValue("MirrorArenaBattleResetTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MirrorArenaBattleResetTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MirrorArenaDefaultDummyGroupRank = Convert.ToString(GetValue("MirrorArenaDefaultDummyGroupRank"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MirrorArenaDefaultDummyGroupRank");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MirrorArenaEachWeekRewardCalcTime = Convert.ToString(GetValue("MirrorArenaEachWeekRewardCalcTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MirrorArenaEachWeekRewardCalcTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MonsterCreaterActiveWaveAnimClip = Convert.ToString(GetValue("MonsterCreaterActiveWaveAnimClip"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MonsterCreaterActiveWaveAnimClip");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MonsterCreaterActiveWaveByOtherDeath = Convert.ToString(GetValue("MonsterCreaterActiveWaveByOtherDeath"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MonsterCreaterActiveWaveByOtherDeath");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_MonsterCreaterIdleAnimClip = Convert.ToString(GetValue("MonsterCreaterIdleAnimClip"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert MonsterCreaterIdleAnimClip");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_New1v1LevelSection = Convert.ToString(GetValue("New1v1LevelSection"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert New1v1LevelSection");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_New1v1LevelSubtractScore = Convert.ToString(GetValue("New1v1LevelSubtractScore"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert New1v1LevelSubtractScore");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_New1v1OpenTimeRange = Convert.ToString(GetValue("New1v1OpenTimeRange"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert New1v1OpenTimeRange");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_New1v1SendRewardTime = Convert.ToString(GetValue("New1v1SendRewardTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert New1v1SendRewardTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_ODamageFormula = Convert.ToString(GetValue("ODamageFormula"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert ODamageFormula");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnevsoneArenaChallengeTime = Convert.ToString(GetValue("OnevsoneArenaChallengeTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnevsoneArenaChallengeTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnevsoneArenaScoreFix = Convert.ToString(GetValue("OnevsoneArenaScoreFix"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnevsoneArenaScoreFix");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnevsOneDummyDetail = Convert.ToString(GetValue("OnevsOneDummyDetail"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnevsOneDummyDetail");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_OnevsOneMeetDummyCountRefreshTime = Convert.ToString(GetValue("OnevsOneMeetDummyCountRefreshTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert OnevsOneMeetDummyCountRefreshTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_PlayerGuideList = Convert.ToString(GetValue("PlayerGuideList"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert PlayerGuideList");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_QualityColorFive = Convert.ToString(GetValue("QualityColorFive"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert QualityColorFive");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_QualityColorFour = Convert.ToString(GetValue("QualityColorFour"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert QualityColorFour");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_QualityColorOne = Convert.ToString(GetValue("QualityColorOne"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert QualityColorOne");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_QualityColorThree = Convert.ToString(GetValue("QualityColorThree"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert QualityColorThree");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_QualityColorTwo = Convert.ToString(GetValue("QualityColorTwo"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert QualityColorTwo");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_QualityColorZero = Convert.ToString(GetValue("QualityColorZero"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert QualityColorZero");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_RandomMissionResetTime = Convert.ToString(GetValue("RandomMissionResetTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert RandomMissionResetTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_RoleDefaultShader = Convert.ToString(GetValue("RoleDefaultShader"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert RoleDefaultShader");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_RoleLevelRewardDetail = Convert.ToString(GetValue("RoleLevelRewardDetail"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert RoleLevelRewardDetail");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_RoleOutLineShader = Convert.ToString(GetValue("RoleOutLineShader"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert RoleOutLineShader");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_SelectDungonBGM = Convert.ToString(GetValue("SelectDungonBGM"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert SelectDungonBGM");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_SelectedAura = Convert.ToString(GetValue("SelectedAura"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert SelectedAura");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_SkillLevelUpEffect = Convert.ToString(GetValue("SkillLevelUpEffect"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert SkillLevelUpEffect");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_StaminaBuyResetTime = Convert.ToString(GetValue("StaminaBuyResetTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert StaminaBuyResetTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_StoreBlackDailyRefreshTime = Convert.ToString(GetValue("StoreBlackDailyRefreshTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert StoreBlackDailyRefreshTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_StoreCommonDailyRefreshTime = Convert.ToString(GetValue("StoreCommonDailyRefreshTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert StoreCommonDailyRefreshTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_StoreDailyRefreshTime = Convert.ToString(GetValue("StoreDailyRefreshTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert StoreDailyRefreshTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_StoreDeluxeBlackRefreshTime = Convert.ToString(GetValue("StoreDeluxeBlackRefreshTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert StoreDeluxeBlackRefreshTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TestString = Convert.ToString(GetValue("TestString"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TestString");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TextSkillEffect = Convert.ToString(GetValue("TextSkillEffect"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TextSkillEffect");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TextSkillNextLevelEffect = Convert.ToString(GetValue("TextSkillNextLevelEffect"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TextSkillNextLevelEffect");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TextSkillNextTypeEffect = Convert.ToString(GetValue("TextSkillNextTypeEffect"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TextSkillNextTypeEffect");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TimeLimitDungeonDesc = Convert.ToString(GetValue("TimeLimitDungeonDesc"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TimeLimitDungeonDesc");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TimeLimitDungeonEmailContent = Convert.ToString(GetValue("TimeLimitDungeonEmailContent"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TimeLimitDungeonEmailContent");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TimeLimitDungeonRefreshTime = Convert.ToString(GetValue("TimeLimitDungeonRefreshTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TimeLimitDungeonRefreshTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TimeZone = Convert.ToString(GetValue("TimeZone"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TimeZone");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TitleBlock = Convert.ToString(GetValue("TitleBlock"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TitleBlock");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TrialRefreshTime = Convert.ToString(GetValue("TrialRefreshTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TrialRefreshTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_UILightFactorByTime = Convert.ToString(GetValue("UILightFactorByTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert UILightFactorByTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_UIMainMenuButtons = Convert.ToString(GetValue("UIMainMenuButtons"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert UIMainMenuButtons");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_UIPnlAnnouncementAnnouncementURL = Convert.ToString(GetValue("UIPnlAnnouncementAnnouncementURL"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert UIPnlAnnouncementAnnouncementURL");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_UIPnlSignInAlreadySignLabel = Convert.ToString(GetValue("UIPnlSignInAlreadySignLabel"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert UIPnlSignInAlreadySignLabel");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_WorldBossDuration = Convert.ToString(GetValue("WorldBossDuration"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert WorldBossDuration");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_WorldBossPrepareDuration = Convert.ToString(GetValue("WorldBossPrepareDuration"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert WorldBossPrepareDuration");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_WorldBossStoreRefreshTime = Convert.ToString(GetValue("WorldBossStoreRefreshTime"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert WorldBossStoreRefreshTime");
			}
#endif
#if UNITY_EDITOR
			try
			{
#endif
				_TestDouble = Convert.ToDouble(GetValue("TestDouble"));
#if UNITY_EDITOR
			}
			catch
			{
				Debug.LogError("catch Exception when convert TestDouble");
			}
#endif
		}

	}
}
