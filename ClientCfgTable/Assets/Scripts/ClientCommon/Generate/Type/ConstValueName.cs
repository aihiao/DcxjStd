namespace ClientCommon
{
    public sealed class ConstValueName : TypeNameContainer<ConstValueName>
    {
        /// <summary>
        /// 伤害属性价值
        /// </summary>
        public const int AttributeValueAP = 1;
        /// <summary>
        /// 攻击速度属性价值
        /// </summary>
        public const int AttributeValueASP = 2;
        /// <summary>
        /// 会心伤害属性价值
        /// </summary>
        public const int AttributeValueCDP = 3;
        /// <summary>
        /// 会心属性价值
        /// </summary>
        public const int AttributeValueCP = 4;
        /// <summary>
        /// 韧性属性价值
        /// </summary>
        public const int AttributeValueCRP = 5;
        /// <summary>
        /// 躲闪属性价值
        /// </summary>
        public const int AttributeValueDGP = 6;
        /// <summary>
        /// 命中属性价值
        /// </summary>
        public const int AttributeValueHITP = 7;
        /// <summary>
        /// 气血属性价值
        /// </summary>
        public const int AttributeValueHP = 8;
        /// <summary>
        /// 内功破防属性价值
        /// </summary>
        public const int AttributeValueIABP = 9;
        /// <summary>
        /// 内功防御属性价值
        /// </summary>
        public const int AttributeValueIDP = 10;
        /// <summary>
        /// 外功破防属性价值
        /// </summary>
        public const int AttributeValueOABP = 11;
        /// <summary>
        /// 外功防御属性价值
        /// </summary>
        public const int AttributeValueODP = 12;
        /// <summary>
        /// 移动速度属性价值
        /// </summary>
        public const int AttributeValueSP = 13;
        /// <summary>
        /// 免伤属性价值
        /// </summary>
        public const int AttriubteValueAVOP = 14;
        /// <summary>
        /// 战斗中声音的最大传播距离
        /// </summary>
        public const int AudioRolloffMaxDistance = 15;
        /// <summary>
        /// 战斗中声音开始衰减的起始距离
        /// </summary>
        public const int AudioRolloffMinDistance = 16;
        /// <summary>
        /// 排行榜活跃度系数
        /// </summary>
        public const int BaseRankActiveLevel = 17;
        /// <summary>
        /// 当没有目标时，点切换目标后最大的搜索范围
        /// </summary>
        public const int BattleChangeTargetMaxSearchDistance = 18;
        /// <summary>
        /// 远程的攻击距离，用于移动停止后的判断
        /// </summary>
        public const int BattleFarawayRoleAttackDistance = 19;
        /// <summary>
        /// 玩家,停止移动后，如果超过这距离，就不会再自动去向敌人移动
        /// </summary>
        public const int BattleMaxDistanceForMoveToEnemyWhenStop = 20;
        /// <summary>
        /// 近程的攻击距离，用于移动停止后的判断
        /// </summary>
        public const int BattleNearRoleAttackDistance = 21;
        /// <summary>
        /// 镜头最远距离
        /// </summary>
        public const int CameraMaxDistance = 22;
        /// <summary>
        /// 战斗中的摄像机最大距离
        /// </summary>
        public const int CameraMaxDistanceBattle = 23;
        /// <summary>
        /// 镜头最近距离
        /// </summary>
        public const int CameraMinDistance = 24;
        /// <summary>
        /// 战斗中的摄像机最小距离
        /// </summary>
        public const int CameraMinDistanceBattle = 25;
        /// <summary>
        /// 场景中摄像机与角色的距离
        /// </summary>
        public const int CameraTraceDistance = 26;
        /// <summary>
        /// 战斗中的摄像机默认距离
        /// </summary>
        public const int CameraTraceDistanceBattle = 27;
        /// <summary>
        /// 摄像机宽度倍数，其他玩家在这个摄像机范围内则加载模型
        /// </summary>
        public const int CameraXScaleToLoadingOtherPlayerAsset = 28;
        /// <summary>
        /// 摄像机高度倍数，其他玩家在这个摄像机范围内则加载模型
        /// </summary>
        public const int CameraYScaleToLoadingOtherPlayerAsset = 29;
        /// <summary>
        /// 点地特效在Y轴上的偏移, 防止被地面遮住
        /// </summary>
        public const int ClickGroundEffectYOffset = 30;
        /// <summary>
        /// 拾取物品的范围
        /// </summary>
        public const int CollectDistance = 31;
        /// <summary>
        /// 战斗中的阵型，队员与队长的距离（单位米，可以填小数）
        /// </summary>
        public const int CombatFormationRadius = 32;
        /// <summary>
        /// 重新寻找一次敌人以找到更近的时，应该更优先选中当前的target，这个值就是给当前的target的加成
        /// </summary>
        public const int CombatReSelectTargetCurrentAddation = 33;
        /// <summary>
        /// 当移动攻击时，如果距离目标超过这个值，应该重新寻找一次敌人以找到更近的
        /// </summary>
        public const int CombatReSelectTargetDistance = 34;
        /// <summary>
        /// 连接网关服务器超时的时间判定，单位秒
        /// </summary>
        public const int ConnectGateServerTimeOutJudgmentTime = 35;
        /// <summary>
        /// 公式中的常数F
        /// </summary>
        public const int ConstF = 36;
        /// <summary>
        /// 默认索敌范围
        /// </summary>
        public const int DefaultPotentialTargetRange = 37;
        /// <summary>
        /// 距离npc多远时打开对话菜单
        /// </summary>
        public const int DistanceToNpcDialog = 38;
        /// <summary>
        /// 物品掉落时重力加速度, 为负值
        /// </summary>
        public const int DropGravityYAcceleratedSpeed = 39;
        /// <summary>
        /// 物品掉落时初始的向上的速度, 为正值
        /// </summary>
        public const int DropInitYSpeed = 40;
        /// <summary>
        /// 战斗中物品掉到地面的最长时间
        /// </summary>
        public const int DropMaxTime = 41;
        /// <summary>
        /// 战斗中物品掉到地面的最短时间
        /// </summary>
        public const int DropMinTime = 42;
        /// <summary>
        /// 怪物掉落物品的范围, 以它自身位置为圆心
        /// </summary>
        public const int DropRadius = 43;
        /// <summary>
        /// 寻路引导 目标到达指定距离内则不显示箭头
        /// </summary>
        public const int DungeonGuideArrowVisibleDistance = 44;
        /// <summary>
        /// 受伤闪白的效果的更新时间片
        /// </summary>
        public const int KLightUpdateTimeSlice = 45;
        /// <summary>
        /// UI冷却结束闪白的效果的更新时间片
        /// </summary>
        public const int KUILightUpdateTimeSlice = 46;
        /// <summary>
        /// 是否提示进攻阵容是否同步到防守阵容条件（进战斗力比防多出5%）
        /// </summary>
        public const int MirrorArenaSyncNeedFightAddition = 47;
        /// <summary>
        /// 主城中其他角色的移动速度
        /// </summary>
        public const int MoveSpeedInCity = 48;
        /// <summary>
        /// 音乐的音量，取值区间（0~1）
        /// </summary>
        public const int MusicVolume = 49;
        /// <summary>
        /// 新1v1假人系数(用于积分计算)
        /// </summary>
        public const int New1v1ScoreCoefficient = 50;
        /// <summary>
        /// 持续点击的时候，点击点与人物近距离判断的距离
        /// </summary>
        public const int OnHoldingNearDistance = 51;
        /// <summary>
        /// 持续点击的时候，近距离的情况，发送位置的时间间隔(单位秒)
        /// </summary>
        public const int OnHoldingNearTimeSection = 52;
        /// <summary>
        /// 战斗移动时，手指Hold时向服务器发送位置的时间间隔
        /// </summary>
        public const int OnHoldingSendPositionDeltaTime = 53;
        /// <summary>
        /// 人打人时内功伤害缩放比例,1为100%不处理
        /// </summary>
        public const int PvPInnerDamageScaler = 54;
        /// <summary>
        /// 人打人时外功伤害缩放比例,1为100%不处理
        /// </summary>
        public const int PvPOuterdamageScaler = 55;
        /// <summary>
        /// 战斗奔跑动作对应的标准移动速度（米/秒）
        /// </summary>
        public const int RunAnimMovementSpeed = 56;
        /// <summary>
        /// 音效的音量，取值区间（0~1）
        /// </summary>
        public const int SoundVolume = 57;
        /// <summary>
        /// 多人副本刷新队伍冷却时间
        /// </summary>
        public const int TeamDungeonRefreshCoolTime = 58;
        /// <summary>
        /// 0
        /// </summary>
        public const int TestFloat = 59;
        /// <summary>
        /// 战斗中跟随移动的最远距离
        /// </summary>
        public const int UnitFollowDistance = 60;
        /// <summary>
        /// 伤害奖励侠义值系数
        /// </summary>
        public const int WorldBossHurtErrantryFactor = 61;
        /// <summary>
        /// 伤害奖励银两系数
        /// </summary>
        public const int WorldBossHurtGameMoneyFactor = 62;
        /// <summary>
        /// 排行奖励侠义值系数
        /// </summary>
        public const int WorldBossRankErrantryFactor = 63;
        /// <summary>
        /// 伤害属性对应显示颜色
        /// </summary>
        public const int AttributeValueAPColor = 64;
        /// <summary>
        /// 攻击速度属性对应显示颜色
        /// </summary>
        public const int AttributeValueASPColor = 65;
        /// <summary>
        /// 会心伤害属性对应显示颜色
        /// </summary>
        public const int AttributeValueCDPColor = 66;
        /// <summary>
        /// 会心属性对应显示颜色
        /// </summary>
        public const int AttributeValueCPColor = 67;
        /// <summary>
        /// 韧性属对应显示颜色
        /// </summary>
        public const int AttributeValueCRPColor = 68;
        /// <summary>
        /// 躲闪属性对应显示颜色
        /// </summary>
        public const int AttributeValueDGPColor = 69;
        /// <summary>
        /// 命中属性对应显示颜色
        /// </summary>
        public const int AttributeValueHITPColor = 70;
        /// <summary>
        /// 气血属性对应显示颜色
        /// </summary>
        public const int AttributeValueHPColor = 71;
        /// <summary>
        /// 内功破防属性对应显示颜色
        /// </summary>
        public const int AttributeValueIABPColor = 72;
        /// <summary>
        /// 内功防御属性对应显示颜色
        /// </summary>
        public const int AttributeValueIDPColor = 73;
        /// <summary>
        /// 外功破防属性对应显示颜色
        /// </summary>
        public const int AttributeValueOABPColor = 74;
        /// <summary>
        /// 外功防御属性对应显示颜色
        /// </summary>
        public const int AttributeValueODPColor = 75;
        /// <summary>
        /// 移动速度属性对应显示颜色
        /// </summary>
        public const int AttributeValueSPColor = 76;
        /// <summary>
        /// 免伤属性对应显示颜色
        /// </summary>
        public const int AttriubteValueAVOPColor = 77;
        /// <summary>
        /// 自动战斗开启等级
        /// </summary>
        public const int AutoFightUseLevel = 78;
        /// <summary>
        /// 进入排行榜的等级限制
        /// </summary>
        public const int BaseRankConditionLevel = 79;
        /// <summary>
        /// 榜的显示人数
        /// </summary>
        public const int BaseRankConditionLimit = 80;
        /// <summary>
        /// 进入排行榜的战斗力要求
        /// </summary>
        public const int BaseRankFightValue = 81;
        /// <summary>
        /// 显示排行的上下区间范围
        /// </summary>
        public const int BaseRankRankSize = 82;
        /// <summary>
        /// 0
        /// </summary>
        public const int BattleAvatarTemplateId = 83;
        /// <summary>
        /// 战斗结束之后慢动作并使用镜头效果的时间（毫秒）
        /// </summary>
        public const int BattleEndShowingEffectTime = 84;
        /// <summary>
        /// 战斗中创建的场景玩家模板
        /// </summary>
        public const int BattleEnvPlayerTemplateId = 85;
        /// <summary>
        /// 战斗中用于创建玩家的模板Id
        /// </summary>
        public const int BattlePlayerTemplateId = 86;
        /// <summary>
        /// 主城关卡名，请注意，要和sceneConfig里的id一致
        /// </summary>
        public const int CentryCitySceneId = 87;
        /// <summary>
        /// 0
        /// </summary>
        public const int ChatInputMaxCount = 88;
        /// <summary>
        /// 世界聊天最大显示条目
        /// </summary>
        public const int ChatListLimitCount = 89;
        /// <summary>
        /// 0
        /// </summary>
        public const int CityAvatarTemplateId = 90;
        /// <summary>
        /// 战斗结束后，战斗服延迟销毁的时间
        /// </summary>
        public const int CombatAfterGameEndTimeOutTime = 91;
        /// <summary>
        /// 所有玩家掉线后，战斗服等待时间，超时则结束
        /// </summary>
        public const int CombatAllConnectionLosedTimeOutTime = 92;
        /// <summary>
        /// 开始战斗前战斗服的超时时间（要考虑玩家的加载时间）
        /// </summary>
        public const int CombatBeforeStartGameTimeOutTime = 93;
        /// <summary>
        /// 战斗中的阵型，队员之间散开的夹角（单位度，不要超过90度则第三个小弟（召唤的怪物）会站到前面去）
        /// </summary>
        public const int CombatFormationDeltaDegrees = 94;
        /// <summary>
        /// 多人副本战斗限时
        /// </summary>
        public const int CombatMultiDungeonTimeLimit = 95;
        /// <summary>
        /// 1v1战斗限时
        /// </summary>
        public const int CombatRealTime1v1TimeLimit = 96;
        /// <summary>
        /// 有人连上战斗服，有人没连上时，等待他人连接的等待时间
        /// </summary>
        public const int CombatWaitOtherPlayerConnectTimeOutTime = 97;
        /// <summary>
        /// 集火技能Id
        /// </summary>
        public const int ConcentrateFireAbilityId = 98;
        /// <summary>
        /// 0
        /// </summary>
        public const int CreateAvatarTemplateId = 99;
        /// <summary>
        /// 伤害飘字偏移
        /// </summary>
        public const int DamageTextOffsetY = 100;
        /// <summary>
        /// 寻访持续时间（单位：小时）(作废）
        /// </summary>
        public const int DartPeriod = 101;
        /// <summary>
        /// 主城中，默认的武器ID
        /// </summary>
        public const int DefaultWeaponInCityID = 102;
        /// <summary>
        /// 副本中默认的武器
        /// </summary>
        public const int DefaultWeaponInDungeonID = 103;
        /// <summary>
        /// 演示关卡玩家数据假人组Id
        /// </summary>
        public const int DemonstrationBattlePlayerDummyGroupId = 104;
        /// <summary>
        /// 演示关假人组Id（女性角色）
        /// </summary>
        public const int DemonstrationBattlePlayerGirlDummyGroupId = 105;
        /// <summary>
        /// 聊天中允许发送的最小等级
        /// </summary>
        public const int DialogEableSendLevel = 106;
        /// <summary>
        /// 聊天中允许发送的最小的VIP等级
        /// </summary>
        public const int DialogEableSendVipLevel = 107;
        /// <summary>
        /// 聊天帮派频道最大数目
        /// </summary>
        public const int DialogItemLimitCountFaction = 108;
        /// <summary>
        /// GM频道最大的聊天数目
        /// </summary>
        public const int DialogItemLimitCountGM = 109;
        /// <summary>
        /// 聊天系统频道消息最大条数
        /// </summary>
        public const int DialogItemLimitCountSystem = 110;
        /// <summary>
        /// 聊天综合频道最大数目
        /// </summary>
        public const int DialogItemLimitCountTotal = 111;
        /// <summary>
        /// 聊天世界频道最大条数
        /// </summary>
        public const int DialogItemLimitCountWorld = 112;
        /// <summary>
        /// 剧情副本完美通关条件自动隐藏时间（单位：秒）
        /// </summary>
        public const int DungeonFightConditionAutoHideTime = 113;
        /// <summary>
        /// 寻路引导 箭头旋转到方向的时间（毫秒）
        /// </summary>
        public const int DungeonGuideArrowRotationSpeed = 114;
        /// <summary>
        /// 0
        /// </summary>
        public const int DungeonViewerAvatarTemplateId = 115;
        /// <summary>
        /// 0
        /// </summary>
        public const int EmailClientSize = 116;
        /// <summary>
        /// 0
        /// </summary>
        public const int EmailMaxSize = 117;
        /// <summary>
        /// 装备快捷购买--腰带
        /// </summary>
        public const int EquipmentBuyBelt = 118;
        /// <summary>
        /// 装备快捷购买--衣服
        /// </summary>
        public const int EquipmentBuyCloth = 119;
        /// <summary>
        /// 装备快捷购买--头盔
        /// </summary>
        public const int EquipmentBuyHat = 120;
        /// <summary>
        /// 装备快捷购买--项链
        /// </summary>
        public const int EquipmentBuyNecklace = 121;
        /// <summary>
        /// 装备快捷购买--戒指
        /// </summary>
        public const int EquipmentBuyRing = 122;
        /// <summary>
        /// 装备快捷购买--武器
        /// </summary>
        public const int EquipmentBuyWeapon = 123;
        /// <summary>
        /// 装备最低穿戴等级--腰带
        /// </summary>
        public const int EquipmentPutonMinLevelBelt = 124;
        /// <summary>
        /// 装备最低穿戴等级--衣服
        /// </summary>
        public const int EquipmentPutonMinLevelCloth = 125;
        /// <summary>
        /// 装备最低穿戴等级--头盔
        /// </summary>
        public const int EquipmentPutonMinLevelHat = 126;
        /// <summary>
        /// 装备最低穿戴等级--项链
        /// </summary>
        public const int EquipmentPutonMinLevelNecklace = 127;
        /// <summary>
        /// 装备最低穿戴等级--戒指
        /// </summary>
        public const int EquipmentPutonMinLevelRing = 128;
        /// <summary>
        /// 装备最低穿戴等级--武器
        /// </summary>
        public const int EquipmentPutonMinLevelWeapon = 129;
        /// <summary>
        /// 装备重铸开始条件（关卡Id）
        /// </summary>
        public const int EquipmentRecastBeginCondition = 130;
        /// <summary>
        /// 装备精炼开始条件（关卡Id）
        /// </summary>
        public const int EquipmentRefineBeginCondition = 131;
        /// <summary>
        /// 装备精炼最大等级(废弃)
        /// </summary>
        public const int EquipmentRefineMaxLevel = 132;
        /// <summary>
        /// 装备强化开始条件（关卡ID）
        /// </summary>
        public const int EquipmentReinforceBeginCondition = 133;
        /// <summary>
        /// 装备宝石的最大数量
        /// </summary>
        public const int EquipmentSlotCount = 134;
        /// <summary>
        /// 侠义值ID
        /// </summary>
        public const int Errantry = 135;
        /// <summary>
        /// 经验ID
        /// </summary>
        public const int Exp = 136;
        /// <summary>
        /// 副本快速扫荡次数
        /// </summary>
        public const int FastCleanDungeon = 137;
        /// <summary>
        /// 第一个穴位id
        /// </summary>
        public const int FirstAcupointId = 138;
        /// <summary>
        /// 演示关sceneid
        /// </summary>
        public const int FirstFakeBattleSceneId = 139;
        /// <summary>
        /// 每日允许祝福的最大好友数
        /// </summary>
        public const int FriendsBlessDailyCount = 140;
        /// <summary>
        /// 好友祝福后获得的体力
        /// </summary>
        public const int FriendsBlessGainStaminCount = 141;
        /// <summary>
        /// 好友邀请cd时间
        /// </summary>
        public const int FriendsCdTime = 142;
        /// <summary>
        /// 好友聊天的条数限制(作废，与FriendsMsgCount相同的功能）
        /// </summary>
        public const int FriendsChatLimitCount = 143;
        /// <summary>
        /// 好友度的等级上限
        /// </summary>
        public const int FriendShipUpLimit = 144;
        /// <summary>
        /// 好友数量限制
        /// </summary>
        public const int FriendsLimitCount = 145;
        /// <summary>
        /// 好友私聊记录数量
        /// </summary>
        public const int FriendsMsgCount = 146;
        /// <summary>
        /// 好友推送的最低等级
        /// </summary>
        public const int FriendsPushLevel = 147;
        /// <summary>
        /// 银币ID
        /// </summary>
        public const int GameMoney = 148;
        /// <summary>
        /// 成长基金的售价，元宝
        /// </summary>
        public const int GrowthFundBuyNeedRealMoney = 149;
        /// <summary>
        /// 可购买成长基金所需的VIP等级
        /// </summary>
        public const int GrowthFundBuyNeedVip = 150;
        /// <summary>
        /// 第二个心法激活等级
        /// </summary>
        public const int HeartPowerActiveLevel = 151;
        /// <summary>
        /// 最大上阵英雄数（废弃）
        /// </summary>
        public const int HeroFightMaxNum = 152;
        /// <summary>
        /// Idle状态下播放IdleOnce动画时间间隔（秒）
        /// </summary>
        public const int IdleOnceTimeSection = 153;
        /// <summary>
        /// 0
        /// </summary>
        public const int InitialAttributeLevel = 154;
        /// <summary>
        /// 排行榜的段位称号起始id
        /// </summary>
        public const int InitRankGradId = 155;
        /// <summary>
        /// 无效的策划配置值
        /// </summary>
        public const int InvalidConfigValue = 156;
        /// <summary>
        /// 每日申请公会次数限制
        /// </summary>
        public const int LeagueApplyCount = 157;
        /// <summary>
        /// 创建公会的等级限制
        /// </summary>
        public const int LeagueCreateLevel = 158;
        /// <summary>
        /// 创建公会需要花费的元宝数量
        /// </summary>
        public const int LeagueCreateRealMoney = 159;
        /// <summary>
        /// 公会邀请的CD时间（秒）
        /// </summary>
        public const int LeagueInviteCD = 160;
        /// <summary>
        /// 公会的最大等级
        /// </summary>
        public const int LeagueMaxLevel = 161;
        /// <summary>
        /// 经验的物品ID, 用于掉落（废弃，使用Exp）
        /// </summary>
        public const int LootExpItemId = 162;
        /// <summary>
        /// 游戏钱币的物品ID, 用于掉落（废弃，使用GameMoney）
        /// </summary>
        public const int LootMoneyItemId = 163;
        /// <summary>
        /// 元宝的物品ID（废弃，使用RealMoney）
        /// </summary>
        public const int LootRealMoneyItemId = 164;
        /// <summary>
        /// 声望的物品ID, 用于掉落（废弃，使用Reputation）
        /// </summary>
        public const int LootReputationItemId = 165;
        /// <summary>
        /// 阵眼宝石最大槽数
        /// </summary>
        public const int MatrixCentreSlotCount = 166;
        /// <summary>
        /// 最多同时存在的点地移动特效数量
        /// </summary>
        public const int MaxComeOnFxCount = 167;
        /// <summary>
        /// 策划统一把配置中用于线索的最大值配成该值
        /// </summary>
        public const int MaxConfigHint = 168;
        /// <summary>
        /// 同屏最多显示的玩家数量，包括主角
        /// </summary>
        public const int MaxInScreenShowingAvatarCount = 169;
        /// <summary>
        /// 最大等级
        /// </summary>
        public const int MaxLevel = 170;
        /// <summary>
        /// 最大体力值
        /// </summary>
        public const int MaxStamina = 171;
        /// <summary>
        /// 镜像竞技场_清除冷却元宝花费
        /// </summary>
        public const int MirrorArenaBattleClearCoolCost = 172;
        /// <summary>
        /// 镜像竞技场_挑战冷却时间（秒）
        /// </summary>
        public const int MirrorArenaBattleCoolTime = 173;
        /// <summary>
        /// 镜像竞技场_挑战次数上限
        /// </summary>
        public const int MirrorArenaBattleCount = 174;
        /// <summary>
        /// 镜像竞技场战前倒计时时长
        /// </summary>
        public const int MirrorArenaBattleCountDownTime = 175;
        /// <summary>
        /// 镜像竞技场_竞技场战斗时长限制（秒）
        /// </summary>
        public const int MirrorArenaBattleLimitTime = 176;
        /// <summary>
        /// 镜像竞技场_战斗报告保存条目数量
        /// </summary>
        public const int MirrorArenaBattleRecordCount = 177;
        /// <summary>
        /// 镜像竞技场_战斗场景（sence_config ID）
        /// </summary>
        public const int MirrorArenaBattleSceneId = 178;
        /// <summary>
        /// 购买战斗次数消耗(作废)
        /// </summary>
        public const int MirrorArenaBuyBattleCountCost = 179;
        /// <summary>
        /// 镜像竞技场_每日排名奖励名次数量
        /// </summary>
        public const int MirrorArenaEachDayRewardCount = 180;
        /// <summary>
        /// 镜像竞技场_普通刷新元宝花费
        /// </summary>
        public const int MirrorArenaNormalRefreshCost = 181;
        /// <summary>
        /// 镜像竞技场_普通刷新每日免费次数
        /// </summary>
        public const int MirrorArenaNormalRefreshCount = 182;
        /// <summary>
        /// 镜像竞技场_普通刷新比自己高的名次范围
        /// </summary>
        public const int MirrorArenaNormalRefreshRange = 183;
        /// <summary>
        /// 镜像竞技场_VIP刷新元宝花费
        /// </summary>
        public const int MirrorArenaVipRefreshCost = 184;
        /// <summary>
        /// 镜像竞技场_VIP刷新比自己高的名次范围
        /// </summary>
        public const int MirrorArenaVipRefreshRange = 185;
        /// <summary>
        /// 怪物生成器template Id, 程序用的
        /// </summary>
        public const int MonsterCreaterTemplateId = 186;
        /// <summary>
        /// 战斗中怪物巡逻间隔时的idle时间，单位毫秒
        /// </summary>
        public const int MonsterPatrolIdleMs = 187;
        /// <summary>
        /// 月卡最大持有期数限制
        /// </summary>
        public const int MonthcardBuyCountLimit = 188;
        /// <summary>
        /// 客户端多人副本最多显示队伍数
        /// </summary>
        public const int MultiDungeonMaxShowGroupCount = 189;
        /// <summary>
        /// 多人副本可获得奖励次数每日重置值
        /// </summary>
        public const int MultiDungeonResetAwardCount = 190;
        /// <summary>
        /// 0
        /// </summary>
        public const int NameMaxLength = 191;
        /// <summary>
        /// 新手1v1战斗场景id(废弃)
        /// </summary>
        public const int New1v1ArenaSceneId = 192;
        /// <summary>
        /// 首次战斗假人ID
        /// </summary>
        public const int New1v1FirstCombatDummyId = 193;
        /// <summary>
        /// 匹配扩展单位时间（单位秒）
        /// </summary>
        public const int New1v1FixPerTime = 194;
        /// <summary>
        /// 新1v1保底奖励邮件id
        /// </summary>
        public const int New1v1FloorEmailId = 195;
        /// <summary>
        /// 新1v1保底积分底限
        /// </summary>
        public const int New1v1FloorScore = 196;
        /// <summary>
        /// 新1v1战斗失败积分
        /// </summary>
        public const int New1v1LoseScore = 197;
        /// <summary>
        /// 新1v1匹配活人等待时间，单位秒
        /// </summary>
        public const int New1v1MatchTime = 198;
        /// <summary>
        /// 匹配单位时间扩展级别范围
        /// </summary>
        public const int New1v1PerTimeFixLevel = 199;
        /// <summary>
        /// 新1v1排名积分底限
        /// </summary>
        public const int New1v1RankMinScore = 200;
        /// <summary>
        /// 新1v1战斗场景id 
        /// </summary>
        public const int New1v1SceneId = 201;
        /// <summary>
        /// 新1v1战斗胜利积分
        /// </summary>
        public const int New1v1WinScore = 202;
        /// <summary>
        /// 0
        /// </summary>
        public const int NpcCollectionTemplateId = 203;
        /// <summary>
        /// 0
        /// </summary>
        public const int NpcTemplateId = 204;
        /// <summary>
        /// 0
        /// </summary>
        public const int NullDataValue = 205;
        /// <summary>
        /// 1v1竞技场_冷却时间（秒）
        /// </summary>
        public const int OnevsoneArenaCooldown = 206;
        /// <summary>
        /// 1v1竞技场_战斗限时（秒）
        /// </summary>
        public const int OnevsoneArenaLimitTime = 207;
        /// <summary>
        /// 1v1竞技场_每秒搜索范围扩大值（积分分数）
        /// </summary>
        public const int OnevsoneArenaMatchRangeFix = 208;
        /// <summary>
        /// 1v1竞技场_最大匹配时长（秒）
        /// </summary>
        public const int OnevsoneArenaMatchTimeMax = 209;
        /// <summary>
        /// 1v1竞技场_精准匹配时长（秒）
        /// </summary>
        public const int OnevsoneArenaMatchTimeNormal = 210;
        /// <summary>
        /// 1v1竞技场_最大日志数量
        /// </summary>
        public const int OnevsoneArenaMaxRecord = 211;
        /// <summary>
        /// 1v1竞技场_开启邮件ID
        /// </summary>
        public const int OnevsoneArenaOpenEmail = 212;
        /// <summary>
        /// 1v1竞技场_最大排行榜排名
        /// </summary>
        public const int OnevsoneArenaRankShow = 213;
        /// <summary>
        /// 1v1竞技场_战斗场景ID
        /// </summary>
        public const int OnevsoneArenaSceneId = 214;
        /// <summary>
        /// 1v1竞技场_初始积分
        /// </summary>
        public const int OnevsoneArenaScoreInit = 215;
        /// <summary>
        /// 1v1竞技场_最大积分
        /// </summary>
        public const int OnevsoneArenaScoreMax = 216;
        /// <summary>
        /// 1v1竞技场_最小积分
        /// </summary>
        public const int OnevsoneArenaScoreMin = 217;
        /// <summary>
        /// 1v1竞技场_赛季长度（天）
        /// </summary>
        public const int OnevsoneArenaSeasonDuration = 218;
        /// <summary>
        /// 1v1竞技场_广播连胜次数/终结连胜次数
        /// </summary>
        public const int OnevsoneArenaStraightBroadcast = 219;
        /// <summary>
        /// 1v1竞技场_每日挑战假人次数
        /// </summary>
        public const int OnevsOneCanMeetDummyCount = 220;
        /// <summary>
        /// 1v1竞技场清除冷却时间消耗元宝
        /// </summary>
        public const int OneVsOneClearCoolTimeCost = 221;
        /// <summary>
        /// 1v1关闭上赛季到开启下赛季的间隔时间（单位分钟）
        /// </summary>
        public const int OnevsOneOpenNextSeasonBreakTime = 222;
        /// <summary>
        /// 上阵功能开启条件（通关关卡Id）
        /// </summary>
        public const int OpenChangeHero = 223;
        /// <summary>
        /// 好友系统开启条件（通关关卡Id）
        /// </summary>
        public const int OpenFriends = 224;
        /// <summary>
        /// 心法系统开启条件（通关关卡Id）
        /// </summary>
        public const int OpenHeartPower = 225;
        /// <summary>
        /// 招募开启限制条件（通关关卡Id）
        /// </summary>
        public const int OpenHire = 226;
        /// <summary>
        /// 宝石合成开启条件（通关关卡Id）
        /// </summary>
        public const int OpenJewelCompose = 227;
        /// <summary>
        /// 境界系统开启条件（通关关卡Id）
        /// </summary>
        public const int OpenRealm = 228;
        /// <summary>
        /// 商店开启条件（通关关卡Id）
        /// </summary>
        public const int OpenStore = 229;
        /// <summary>
        /// 限时副本开启条件通（关关卡Id）
        /// </summary>
        public const int OpenTimeLimitDungeon = 230;
        /// <summary>
        /// 江湖奇宝功能开启条件（通关关卡Id）
        /// </summary>
        public const int OpenTreasure = 231;
        /// <summary>
        /// 拜师开启限制条件（通关关卡Id）
        /// </summary>
        public const int OpenTutor = 232;
        /// <summary>
        /// 世界聊天开启等级
        /// </summary>
        public const int OpenWorldChatLevel = 233;
        /// <summary>
        /// 世界聊天开vip启等级
        /// </summary>
        public const int OpenWorldChatVIPLevel = 234;
        /// <summary>
        /// f, 算属性百分比时的一个公式参数，比如 GetRateKnowing里调用
        /// </summary>
        public const int PropertyRateDivisionFactor = 235;
        /// <summary>
        /// 江湖缉拿默认每天能完成的次数
        /// </summary>
        public const int RandomMissionCanDoneCount = 236;
        /// <summary>
        /// 江湖缉拿接受任务数量
        /// </summary>
        public const int RandomMissionReceiveCount = 237;
        /// <summary>
        /// 江湖缉拿刷新间隔时间(秒)
        /// </summary>
        public const int RandomMissionRefreshApart = 238;
        /// <summary>
        /// 江湖缉拿刷新消耗元宝
        /// </summary>
        public const int RandomMissionRefreshCost = 239;
        /// <summary>
        /// 江湖缉拿刷新任务数量
        /// </summary>
        public const int RandomMissionRefreshCount = 240;
        /// <summary>
        /// 江湖缉拿免费刷新次数
        /// </summary>
        public const int RandomMissionRefreshFree = 241;
        /// <summary>
        /// 江湖缉拿刷新限定出现次数
        /// </summary>
        public const int RandomMissionRefreshTimesLimit = 242;
        /// <summary>
        /// 金币ID
        /// </summary>
        public const int RealMoney = 243;
        /// <summary>
        /// 实时1v1竞技场战前倒计时时长
        /// </summary>
        public const int RealTime1v1BattleCountDownTime = 244;
        /// <summary>
        /// 弹出断线重连后，多长时间后程序自动为玩家重连，毫秒
        /// </summary>
        public const int ReConnectAutoActionCutdownTime = 245;
        /// <summary>
        /// 玩家每日可领取的红包数量限制
        /// </summary>
        public const int RedPacketsDailyPickCount = 246;
        /// <summary>
        /// 改名道具id
        /// </summary>
        public const int RenameItemId = 247;
        /// <summary>
        /// 声望ID
        /// </summary>
        public const int Repution = 248;
        /// <summary>
        /// 拾取资源(经验,声望)冒字时间间隔, 毫秒
        /// </summary>
        public const int ResourceFloatingTextInterval = 249;
        /// <summary>
        /// 角色最大游戏币上限
        /// </summary>
        public const int RoleGameMoneyMax = 250;
        /// <summary>
        /// 客服按钮控制显示
        /// </summary>
        public const int ServiceControl = 251;
        /// <summary>
        /// 签到寻路NPC的ID
        /// </summary>
        public const int SignGoToNpcId = 252;
        /// <summary>
        /// 技能查看器创建角色用ID
        /// </summary>
        public const int SkillViewerBattleAvatarTemplateId = 253;
        /// <summary>
        /// 体力id
        /// </summary>
        public const int StaminaId = 254;
        /// <summary>
        /// 自然恢复体力的间隔时间(单位：秒)
        /// </summary>
        public const int StaminaRecoverInterval = 255;
        /// <summary>
        /// 每次自然恢复体力的量
        /// </summary>
        public const int StaminaRecoverQuantity = 256;
        /// <summary>
        /// 黑店手动刷新花费
        /// </summary>
        public const int StoreBlackRefreshCost = 257;
        /// <summary>
        /// 黑店开启条件，VIP等级 (废弃)
        /// </summary>
        public const int StoreBlackUnlock = 258;
        /// <summary>
        /// 高级黑店手动刷新花费
        /// </summary>
        public const int StoreDeluxeBlackRefreshCost = 259;
        /// <summary>
        /// 高级黑店开启条件，VIP等级 (废弃)
        /// </summary>
        public const int StoreDeluxeBlackUnlock = 260;
        /// <summary>
        /// 商店手动刷新花费
        /// </summary>
        public const int StoreNormalRefreshCost = 261;
        /// <summary>
        /// 普通商店开启条件，VIP等级 (废弃)
        /// </summary>
        public const int StoreNormalUnlock = 262;
        /// <summary>
        /// 0123456(日一二三四五六)
        /// </summary>
        public const int StoreRefreshCountResetDayOfWeek = 263;
        /// <summary>
        /// 剧情副本扫荡次数
        /// </summary>
        public const int StoryCompaignFightTimes = 264;
        /// <summary>
        /// 禁言生效的最小说话间隔
        /// </summary>
        public const int TalkContentRefreshTimeSecond = 265;
        /// <summary>
        /// 被禁言持续时间
        /// </summary>
        public const int TalkForbidEndTimeSecond = 266;
        /// <summary>
        /// 高于此VIP将不受禁言文字限制，但仍受敏感字限制
        /// </summary>
        public const int TalkNoForbidVipLevel = 267;
        /// <summary>
        /// 限时副本邮件ID
        /// </summary>
        public const int TimeLimitDungeonEmail = 268;
        /// <summary>
        /// 限时副本失败限制次数
        /// </summary>
        public const int TimeLimitDungeonFailedLimit = 269;
        /// <summary>
        /// 限时副本默认开始解锁关卡id
        /// </summary>
        public const int TimeLimitDungeonFirstDefaultId = 270;
        /// <summary>
        /// 限时副本最高层数
        /// </summary>
        public const int TimeLimitDungeonMax = 271;
        /// <summary>
        /// 限时副本排名人数
        /// </summary>
        public const int TimeLimitDungeonRankCount = 272;
        /// <summary>
        /// 图腾（不可攻击）模板ID
        /// </summary>
        public const int TotemNotDamageableTemplateId = 273;
        /// <summary>
        /// 守塔任务每日可进入的战斗次数
        /// </summary>
        public const int TowerBattleTimesEachDay = 274;
        /// <summary>
        /// 守塔任务每日免费刷新次数
        /// </summary>
        public const int TowerFreeRefreshCount = 275;
        /// <summary>
        /// 守塔任务每日消耗元宝数
        /// </summary>
        public const int TowerGoldRefreshCost = 276;
        /// <summary>
        /// 守塔任务自动刷新的时间间隔
        /// </summary>
        public const int TowerRefreshTime = 277;
        /// <summary>
        /// 试炼boss失败刷新元宝数
        /// </summary>
        public const int TrialBossFailedRefreshMoney = 278;
        /// <summary>
        /// 试炼boss成功刷新元宝数
        /// </summary>
        public const int TrialBossKilledRefreshMoney = 279;
        /// <summary>
        /// 每日挑战次数
        /// </summary>
        public const int TrialCount = 280;
        /// <summary>
        /// 战斗失败刷新时间(秒)
        /// </summary>
        public const int TrialFailedCooldown = 281;
        /// <summary>
        /// 战斗过程强杀刷新时间(秒)
        /// </summary>
        public const int TrialKillCombatCooldown = 282;
        /// <summary>
        /// 礼包单次使用最大数量限制
        /// </summary>
        public const int UIPnlUseBoxMaxNum = 283;
        /// <summary>
        /// 吟唱时间少于多少的话不显示吟唱条. 毫秒
        /// </summary>
        public const int UIRoleInfoIgnoreCastingTime = 284;
        /// <summary>
        /// 受控时间少于多少的话不显示受控进度条. 毫秒
        /// </summary>
        public const int UIRoleInfoIgnoreControlledTime = 285;
        /// <summary>
        /// 语音聊天允许发送的最小时间，单位毫秒（ms）
        /// </summary>
        public const int VoiceDialogEnableLength = 286;
        /// <summary>
        /// 语音聊天允许发送的最大时间，单位毫秒（ms）
        /// </summary>
        public const int VoiceDialogEnableMaxLength = 287;
        /// <summary>
        /// 世界BOSS_挑战冷却时间（秒
        /// </summary>
        public const int WorldBossBattleCD = 288;
        /// <summary>
        /// 世界BOSS_清除冷却花费元宝
        /// </summary>
        public const int WorldBossBattleCDClearCost = 289;
        /// <summary>
        /// 世界BOSS_BOSS阶段战斗场景	Sence_Config ID
        /// </summary>
        public const int WorldBossBattleSence = 290;
        /// <summary>
        /// 世界BOSS_BOSS阶段战斗限时(秒)
        /// </summary>
        public const int WorldBossBattleTimeLimit = 291;
        /// <summary>
        /// 世界BOSS_正式打boss前的时间
        /// </summary>
        public const int WorldBossBeforeBattleTime = 292;
        /// <summary>
        /// 世界BOSS默认boss id
        /// </summary>
        public const int WorldBossDefaultBossId = 293;
        /// <summary>
        /// 世界BOSS_BOSS上一次挑战显示的排名数量
        /// </summary>
        public const int WorldBossLastRankNum = 294;
        /// <summary>
        /// 世界BOSS_筹备阶段战斗场景 Sence_Config ID
        /// </summary>
        public const int WorldBossPrepareBattleSence = 295;
        /// <summary>
        /// 世界BOSS_筹备阶段战斗限时(秒)
        /// </summary>
        public const int WorldBossPrepareBattleTimeLimit = 296;
        /// <summary>
        /// 世界BOSS_筹备阶段怪最大数量
        /// </summary>
        public const int WorldBossPrepareMaxNum = 297;
        /// <summary>
        /// 世界BOSS_筹备阶段最小参与人数
        /// </summary>
        public const int WorldBossPrepareMinParticipant = 298;
        /// <summary>
        /// 世界BOSS_侠义商店刷新花费元宝
        /// </summary>
        public const int WorldBossStoreRefreshCost = 299;
        /// <summary>
        /// 世界聊天的cd时间
        /// </summary>
        public const int WorldChatCDTime = 300;
        /// <summary>
        /// 出城按钮动画显示限制等级
        /// </summary>
        public const int WorldMapAnimLimitLevel = 301;
        /// <summary>
        /// 大地图, 当停止滑动后，惯性滑动m帧，在这些帧内，滑动值根据 inertiaMovementX 里的最大值获取，并递减
        /// </summary>
        public const int WorldMapInertiaReduceFrames = 302;
        /// <summary>
        /// 自动战斗特效
        /// </summary>
        public const int AutoFightEffect = 303;
        /// <summary>
        /// 主界面角色按钮打开界面后的左侧标签
        /// </summary>
        public const int AvatarInformationMenu = 304;
        /// <summary>
        /// 角色跑动的时候的特效名称
        /// </summary>
        public const int AvatarMoveEffectName = 305;
        /// <summary>
        /// 回城建筑模型
        /// </summary>
        public const int BackBuildingModeName = 306;
        /// <summary>
        /// 回出城建筑坐标
        /// </summary>
        public const int BackBuildingPos = 307;
        /// <summary>
        /// 背包分页材料类型表
        /// </summary>
        public const int BagPages = 308;
        /// <summary>
        /// 排行榜每周刷新时间
        /// </summary>
        public const int BaseRankRefreshWeeklyTime = 309;
        /// <summary>
        /// 主城关卡名，请注意，要和sceneConfig里的id一致
        /// </summary>
        public const int CentryCityName = 310;
        /// <summary>
        /// 聊天的自己的颜色
        /// </summary>
        public const int ChatMessageSelfColor = 311;
        /// <summary>
        /// 点地特效
        /// </summary>
        public const int ClickGroundEffect = 312;
        /// <summary>
        /// 装备升级成功特效
        /// </summary>
        public const int ClientEquipmentLevelUpSuccessEffect = 313;
        /// <summary>
        /// 装备升星失败特效
        /// </summary>
        public const int ClientEquipmentStarUpFailedEffect = 314;
        /// <summary>
        /// 奇宝分解特效
        /// </summary>
        public const int ClientTreasureDescomposeSuccessEffect = 315;
        /// <summary>
        /// 奇宝修复失败特效
        /// </summary>
        public const int ClientTreasureRepairedFailedEffect = 316;
        /// <summary>
        /// 奇宝修复成功特效
        /// </summary>
        public const int ClientTreasureRepairedSuccessEffect = 317;
        /// <summary>
        /// 奇宝选中特效
        /// </summary>
        public const int ClientTreasureSelectedEffect = 318;
        /// <summary>
        /// 拾取物品飘字的prefab资源名
        /// </summary>
        public const int CollectItemText = 319;
        /// <summary>
        /// 拾取(经验, 声望)头顶飘字的prefab资源名
        /// </summary>
        public const int CollectResourceText = 320;
        /// <summary>
        /// 每日福利可领取特效
        /// </summary>
        public const int DailyBenefitEffect = 321;
        /// <summary>
        /// 每日福利npcid
        /// </summary>
        public const int DailyBenefitNpcId = 322;
        /// <summary>
        /// 每日活跃度积分以及档位奖励id(eg:25;xId|50;xx?Id|75;xxxId|100;xxxxId)必须是4个档位
        /// </summary>
        public const int DailyMissionLivenessAndReward = 323;
        /// <summary>
        /// 活跃度每日任务刷新时间
        /// </summary>
        public const int DailyMissionRefreshTime = 324;
        /// <summary>
        /// 伤害特效绑定的骨骼名
        /// </summary>
        public const int DamageParticleBone = 325;
        /// <summary>
        /// 显示吸收的飘字资源
        /// </summary>
        public const int DamageTextAbsorb = 326;
        /// <summary>
        /// 显示免伤的飘字资源
        /// </summary>
        public const int DamageTextBlock = 327;
        /// <summary>
        /// 对敌对的持续伤害
        /// </summary>
        public const int DamageTextDurDamageEnemy = 328;
        /// <summary>
        /// 对玩家的持续伤害
        /// </summary>
        public const int DamageTextDurDamageHero = 329;
        /// <summary>
        /// 持续加血
        /// </summary>
        public const int DamageTextDurHeal = 330;
        /// <summary>
        /// 治疗飘字的prefab名
        /// </summary>
        public const int DamageTextHeal = 331;
        /// <summary>
        /// 治疗暴击飘字的prefab名
        /// </summary>
        public const int DamageTextHealCritical = 332;
        /// <summary>
        /// Miss飘字的prefab名
        /// </summary>
        public const int DamageTextMiss = 333;
        /// <summary>
        /// 显示免伤的飘字资源
        /// </summary>
        public const int DamageTextReduce = 334;
        /// <summary>
        /// 被反弹伤害的飘字资源
        /// </summary>
        public const int DamageTextReflect = 335;
        /// <summary>
        /// 技能飘字的prefab名
        /// </summary>
        public const int DamageTextSkill = 336;
        /// <summary>
        /// 技能暴击飘字的prefab名
        /// </summary>
        public const int DamageTextSkillCritical = 337;
        /// <summary>
        /// 技能被打断
        /// </summary>
        public const int DamageTextSkillInterrupted = 338;
        /// <summary>
        /// 普攻飘字的prefab名
        /// </summary>
        public const int DamageTextWeapon = 339;
        /// <summary>
        /// 普攻暴击飘字的prefab名
        /// </summary>
        public const int DamageTextWeaponCritical = 340;
        /// <summary>
        /// 运镖说明
        /// </summary>
        public const int DartDescription = 341;
        /// <summary>
        /// 寻路引导 箭头的资源名
        /// </summary>
        public const int DungeonGuideArrowResName = 342;
        /// <summary>
        /// 关卡重置时间
        /// </summary>
        public const int DungeonResetTime = 343;
        /// <summary>
        /// 0
        /// </summary>
        public const int EmailDefaultSender = 344;
        /// <summary>
        /// 0
        /// </summary>
        public const int EmailDefaultSenderIcon = 345;
        /// <summary>
        /// 易物界面标签
        /// </summary>
        public const int ExchangePage = 346;
        /// <summary>
        /// 好友祝福语
        /// </summary>
        public const int FriendsBlessMessage = 347;
        /// <summary>
        /// 宝石合成成功特效
        /// </summary>
        public const int GemComposeSuccessEffect = 348;
        /// <summary>
        /// 充值：元宝
        /// </summary>
        public const int GoodsYuanBao = 349;
        /// <summary>
        /// 充值：月卡
        /// </summary>
        public const int GoodsYueKa = 350;
        /// <summary>
        /// 充值：赠送
        /// </summary>
        public const int GoodsZengSong = 351;
        /// <summary>
        /// 出师推荐战斗力百分比最高阶段（这个是百分比100以上）
        /// </summary>
        public const int GraduateHigh = 352;
        /// <summary>
        /// 出师推荐战斗力百分比中间阶段（这个是百分比50-100）
        /// </summary>
        public const int GraduateMiddle = 353;
        /// <summary>
        /// 出师推荐战斗力百分比最低阶段（这个是百分比0-50
        /// </summary>
        public const int GraduatePrimary = 354;
        /// <summary>
        /// 外攻伤害公式
        /// </summary>
        public const int IDamageFormula = 355;
        /// <summary>
        /// 受伤闪白时更改的shader变量，一般不要改
        /// </summary>
        public const int KShaderMixFactorName = 356;
        /// <summary>
        /// 帮派boss副本活动时间
        /// </summary>
        public const int LeagueBossDungeonOpenTime = 357;
        /// <summary>
        /// 主城主角升级特效
        /// </summary>
        public const int LevelUpEffect = 358;
        /// <summary>
        /// 受伤闪白在每个时间片里的闪白值，这个字符串也决定了闪白的总时间
        /// </summary>
        public const int LightFactorByTime = 359;
        /// <summary>
        /// 登陆和选区的时候的背景音乐名
        /// </summary>
        public const int LoginAndSelectAreaMusicName = 360;
        /// <summary>
        /// 掉落分队的默认几率, 格式: 数量|几率|数量|几率
        /// </summary>
        public const int LootDropHeapCountPercent = 361;
        /// <summary>
        /// 心法进阶特效
        /// </summary>
        public const int MentalTypeUpEffect = 362;
        /// <summary>
        /// 镜像竞技场_挑战次数重置时间
        /// </summary>
        public const int MirrorArenaBattleResetTime = 363;
        /// <summary>
        /// 镜像竞技场_默认假人排名
        /// </summary>
        public const int MirrorArenaDefaultDummyGroupRank = 364;
        /// <summary>
        /// 镜像竞技场_每周奖励结算时间
        /// </summary>
        public const int MirrorArenaEachWeekRewardCalcTime = 365;
        /// <summary>
        /// 怪物生成器刷怪时的动画名
        /// </summary>
        public const int MonsterCreaterActiveWaveAnimClip = 366;
        /// <summary>
        /// 怪物生成器提前刷新时的文本索引
        /// </summary>
        public const int MonsterCreaterActiveWaveByOtherDeath = 367;
        /// <summary>
        /// 怪物生成器idle时的动画名
        /// </summary>
        public const int MonsterCreaterIdleAnimClip = 368;
        /// <summary>
        /// 新1v1级别段分段及对应积分(！！！该字段作废！！！)
        /// </summary>
        public const int New1v1LevelSection = 369;
        /// <summary>
        /// 新1v1等级差对应积分
        /// </summary>
        public const int New1v1LevelSubtractScore = 370;
        /// <summary>
        /// 新1v1开启关闭时间(必须是当天的，不能跨天)
        /// </summary>
        public const int New1v1OpenTimeRange = 371;
        /// <summary>
        /// 新1v1奖励结算时间
        /// </summary>
        public const int New1v1SendRewardTime = 372;
        /// <summary>
        /// 内攻伤害公式
        /// </summary>
        public const int ODamageFormula = 373;
        /// <summary>
        /// 1v1竞技场_挑战时段
        /// </summary>
        public const int OnevsoneArenaChallengeTime = 374;
        /// <summary>
        /// 1v1竞技场_积分差对应修正值(向上取)
        /// </summary>
        public const int OnevsoneArenaScoreFix = 375;
        /// <summary>
        /// 1v1竞技场_假人组配置(玩家积分区间1;假人积区间1;假人组1,假人组2,假人组3|玩家积分区间2;假人积分区间2;假人组4,假人组5,假人组6|……)
        /// </summary>
        public const int OnevsOneDummyDetail = 376;
        /// <summary>
        /// 1v1竞技场_挑战假人次数刷新时间
        /// </summary>
        public const int OnevsOneMeetDummyCountRefreshTime = 377;
        /// <summary>
        /// 玩家指引顺序列表
        /// </summary>
        public const int PlayerGuideList = 378;
        /// <summary>
        /// 名字品质颜色5品质
        /// </summary>
        public const int QualityColorFive = 379;
        /// <summary>
        /// 名字品质颜色4品质
        /// </summary>
        public const int QualityColorFour = 380;
        /// <summary>
        /// 名字品质颜色1品质
        /// </summary>
        public const int QualityColorOne = 381;
        /// <summary>
        /// 名字品质颜色3品质
        /// </summary>
        public const int QualityColorThree = 382;
        /// <summary>
        /// 名字品质颜色2品质
        /// </summary>
        public const int QualityColorTwo = 383;
        /// <summary>
        /// 名字品质颜色0品质
        /// </summary>
        public const int QualityColorZero = 384;
        /// <summary>
        /// 江湖缉拿每日重置时间
        /// </summary>
        public const int RandomMissionResetTime = 385;
        /// <summary>
        /// 英雄默认shader
        /// </summary>
        public const int RoleDefaultShader = 386;
        /// <summary>
        /// 新手等级礼包,等级;奖励包id|等级;奖励包id
        /// </summary>
        public const int RoleLevelRewardDetail = 387;
        /// <summary>
        /// 英雄描边shader
        /// </summary>
        public const int RoleOutLineShader = 388;
        /// <summary>
        /// 选择副本场景的背景音乐
        /// </summary>
        public const int SelectDungonBGM = 389;
        /// <summary>
        /// 选中时脚下的光环
        /// </summary>
        public const int SelectedAura = 390;
        /// <summary>
        /// 技能升级特效
        /// </summary>
        public const int SkillLevelUpEffect = 391;
        /// <summary>
        /// 体力购买重置时间点
        /// </summary>
        public const int StaminaBuyResetTime = 392;
        /// <summary>
        /// 黑店每日刷新时间(|分割)
        /// </summary>
        public const int StoreBlackDailyRefreshTime = 393;
        /// <summary>
        /// 普通商店每日刷新时间(|分割)
        /// </summary>
        public const int StoreCommonDailyRefreshTime = 394;
        /// <summary>
        /// 商店每日刷新时间(废弃)
        /// </summary>
        public const int StoreDailyRefreshTime = 395;
        /// <summary>
        /// 高级黑店每日刷新时间(|分割)
        /// </summary>
        public const int StoreDeluxeBlackRefreshTime = 396;
        /// <summary>
        /// 0
        /// </summary>
        public const int TestString = 397;
        /// <summary>
        /// 技能升品界面标签
        /// </summary>
        public const int TextSkillEffect = 398;
        /// <summary>
        /// 技能升级界面标签
        /// </summary>
        public const int TextSkillNextLevelEffect = 399;
        /// <summary>
        /// 技能升品界面标签
        /// </summary>
        public const int TextSkillNextTypeEffect = 400;
        /// <summary>
        /// 限时副本描述文字
        /// </summary>
        public const int TimeLimitDungeonDesc = 401;
        /// <summary>
        /// 限时副本邮件奖励正文
        /// </summary>
        public const int TimeLimitDungeonEmailContent = 402;
        /// <summary>
        /// 限时副本每日刷新时间
        /// </summary>
        public const int TimeLimitDungeonRefreshTime = 403;
        /// <summary>
        /// 时区啊时区
        /// </summary>
        public const int TimeZone = 404;
        /// <summary>
        /// 称号组
        /// </summary>
        public const int TitleBlock = 405;
        /// <summary>
        /// 击杀boss刷新时间
        /// </summary>
        public const int TrialRefreshTime = 406;
        /// <summary>
        /// UI冷却结束闪白在每个时间片里的闪白值，这个字符串也决定了闪白的总时间
        /// </summary>
        public const int UILightFactorByTime = 407;
        /// <summary>
        /// 主界面所有功能都解锁后的按钮对应的menu_navigation表的ID
        /// </summary>
        public const int UIMainMenuButtons = 408;
        /// <summary>
        /// 获取公告信息的网址
        /// </summary>
        public const int UIPnlAnnouncementAnnouncementURL = 409;
        /// <summary>
        /// 签到完毕显示文字
        /// </summary>
        public const int UIPnlSignInAlreadySignLabel = 410;
        /// <summary>
        /// 世界BOSS_BOSS阶段时间区间
        /// </summary>
        public const int WorldBossDuration = 411;
        /// <summary>
        /// 世界BOSS_筹备阶段时间区间
        /// </summary>
        public const int WorldBossPrepareDuration = 412;
        /// <summary>
        /// 世界BOSS_侠义商店自动刷新时间（|分割）
        /// </summary>
        public const int WorldBossStoreRefreshTime = 413;
        /// <summary>
        /// 0
        /// </summary>
        public const int TestDouble = 414;

        public static void Initialize()
        {
            if (initialized)
                return;

            SetTextSectionName("TypeDef_ConstValueName");

            RegisterType("AttributeValueAP", AttributeValueAP, "AttributeValueAP");
            RegisterType("AttributeValueASP", AttributeValueASP, "AttributeValueASP");
            RegisterType("AttributeValueCDP", AttributeValueCDP, "AttributeValueCDP");
            RegisterType("AttributeValueCP", AttributeValueCP, "AttributeValueCP");
            RegisterType("AttributeValueCRP", AttributeValueCRP, "AttributeValueCRP");
            RegisterType("AttributeValueDGP", AttributeValueDGP, "AttributeValueDGP");
            RegisterType("AttributeValueHITP", AttributeValueHITP, "AttributeValueHITP");
            RegisterType("AttributeValueHP", AttributeValueHP, "AttributeValueHP");
            RegisterType("AttributeValueIABP", AttributeValueIABP, "AttributeValueIABP");
            RegisterType("AttributeValueIDP", AttributeValueIDP, "AttributeValueIDP");
            RegisterType("AttributeValueOABP", AttributeValueOABP, "AttributeValueOABP");
            RegisterType("AttributeValueODP", AttributeValueODP, "AttributeValueODP");
            RegisterType("AttributeValueSP", AttributeValueSP, "AttributeValueSP");
            RegisterType("AttriubteValueAVOP", AttriubteValueAVOP, "AttriubteValueAVOP");
            RegisterType("AudioRolloffMaxDistance", AudioRolloffMaxDistance, "AudioRolloffMaxDistance");
            RegisterType("AudioRolloffMinDistance", AudioRolloffMinDistance, "AudioRolloffMinDistance");
            RegisterType("BaseRankActiveLevel", BaseRankActiveLevel, "BaseRankActiveLevel");
            RegisterType("BattleChangeTargetMaxSearchDistance", BattleChangeTargetMaxSearchDistance, "BattleChangeTargetMaxSearchDistance");
            RegisterType("BattleFarawayRoleAttackDistance", BattleFarawayRoleAttackDistance, "BattleFarawayRoleAttackDistance");
            RegisterType("BattleMaxDistanceForMoveToEnemyWhenStop", BattleMaxDistanceForMoveToEnemyWhenStop, "BattleMaxDistanceForMoveToEnemyWhenStop");
            RegisterType("BattleNearRoleAttackDistance", BattleNearRoleAttackDistance, "BattleNearRoleAttackDistance");
            RegisterType("CameraMaxDistance", CameraMaxDistance, "CameraMaxDistance");
            RegisterType("CameraMaxDistanceBattle", CameraMaxDistanceBattle, "CameraMaxDistanceBattle");
            RegisterType("CameraMinDistance", CameraMinDistance, "CameraMinDistance");
            RegisterType("CameraMinDistanceBattle", CameraMinDistanceBattle, "CameraMinDistanceBattle");
            RegisterType("CameraTraceDistance", CameraTraceDistance, "CameraTraceDistance");
            RegisterType("CameraTraceDistanceBattle", CameraTraceDistanceBattle, "CameraTraceDistanceBattle");
            RegisterType("CameraXScaleToLoadingOtherPlayerAsset", CameraXScaleToLoadingOtherPlayerAsset, "CameraXScaleToLoadingOtherPlayerAsset");
            RegisterType("CameraYScaleToLoadingOtherPlayerAsset", CameraYScaleToLoadingOtherPlayerAsset, "CameraYScaleToLoadingOtherPlayerAsset");
            RegisterType("ClickGroundEffectYOffset", ClickGroundEffectYOffset, "ClickGroundEffectYOffset");
            RegisterType("CollectDistance", CollectDistance, "CollectDistance");
            RegisterType("CombatFormationRadius", CombatFormationRadius, "CombatFormationRadius");
            RegisterType("CombatReSelectTargetCurrentAddation", CombatReSelectTargetCurrentAddation, "CombatReSelectTargetCurrentAddation");
            RegisterType("CombatReSelectTargetDistance", CombatReSelectTargetDistance, "CombatReSelectTargetDistance");
            RegisterType("ConnectGateServerTimeOutJudgmentTime", ConnectGateServerTimeOutJudgmentTime, "ConnectGateServerTimeOutJudgmentTime");
            RegisterType("ConstF", ConstF, "ConstF");
            RegisterType("DefaultPotentialTargetRange", DefaultPotentialTargetRange, "DefaultPotentialTargetRange");
            RegisterType("DistanceToNpcDialog", DistanceToNpcDialog, "DistanceToNpcDialog");
            RegisterType("DropGravityYAcceleratedSpeed", DropGravityYAcceleratedSpeed, "DropGravityYAcceleratedSpeed");
            RegisterType("DropInitYSpeed", DropInitYSpeed, "DropInitYSpeed");
            RegisterType("DropMaxTime", DropMaxTime, "DropMaxTime");
            RegisterType("DropMinTime", DropMinTime, "DropMinTime");
            RegisterType("DropRadius", DropRadius, "DropRadius");
            RegisterType("DungeonGuideArrowVisibleDistance", DungeonGuideArrowVisibleDistance, "DungeonGuideArrowVisibleDistance");
            RegisterType("KLightUpdateTimeSlice", KLightUpdateTimeSlice, "KLightUpdateTimeSlice");
            RegisterType("KUILightUpdateTimeSlice", KUILightUpdateTimeSlice, "KUILightUpdateTimeSlice");
            RegisterType("MirrorArenaSyncNeedFightAddition", MirrorArenaSyncNeedFightAddition, "MirrorArenaSyncNeedFightAddition");
            RegisterType("MoveSpeedInCity", MoveSpeedInCity, "MoveSpeedInCity");
            RegisterType("MusicVolume", MusicVolume, "MusicVolume");
            RegisterType("New1v1ScoreCoefficient", New1v1ScoreCoefficient, "New1v1ScoreCoefficient");
            RegisterType("OnHoldingNearDistance", OnHoldingNearDistance, "OnHoldingNearDistance");
            RegisterType("OnHoldingNearTimeSection", OnHoldingNearTimeSection, "OnHoldingNearTimeSection");
            RegisterType("OnHoldingSendPositionDeltaTime", OnHoldingSendPositionDeltaTime, "OnHoldingSendPositionDeltaTime");
            RegisterType("PvPInnerDamageScaler", PvPInnerDamageScaler, "PvPInnerDamageScaler");
            RegisterType("PvPOuterdamageScaler", PvPOuterdamageScaler, "PvPOuterdamageScaler");
            RegisterType("RunAnimMovementSpeed", RunAnimMovementSpeed, "RunAnimMovementSpeed");
            RegisterType("SoundVolume", SoundVolume, "SoundVolume");
            RegisterType("TeamDungeonRefreshCoolTime", TeamDungeonRefreshCoolTime, "TeamDungeonRefreshCoolTime");
            RegisterType("TestFloat", TestFloat, "TestFloat");
            RegisterType("UnitFollowDistance", UnitFollowDistance, "UnitFollowDistance");
            RegisterType("WorldBossHurtErrantryFactor", WorldBossHurtErrantryFactor, "WorldBossHurtErrantryFactor");
            RegisterType("WorldBossHurtGameMoneyFactor", WorldBossHurtGameMoneyFactor, "WorldBossHurtGameMoneyFactor");
            RegisterType("WorldBossRankErrantryFactor", WorldBossRankErrantryFactor, "WorldBossRankErrantryFactor");
            RegisterType("AttributeValueAPColor", AttributeValueAPColor, "AttributeValueAPColor");
            RegisterType("AttributeValueASPColor", AttributeValueASPColor, "AttributeValueASPColor");
            RegisterType("AttributeValueCDPColor", AttributeValueCDPColor, "AttributeValueCDPColor");
            RegisterType("AttributeValueCPColor", AttributeValueCPColor, "AttributeValueCPColor");
            RegisterType("AttributeValueCRPColor", AttributeValueCRPColor, "AttributeValueCRPColor");
            RegisterType("AttributeValueDGPColor", AttributeValueDGPColor, "AttributeValueDGPColor");
            RegisterType("AttributeValueHITPColor", AttributeValueHITPColor, "AttributeValueHITPColor");
            RegisterType("AttributeValueHPColor", AttributeValueHPColor, "AttributeValueHPColor");
            RegisterType("AttributeValueIABPColor", AttributeValueIABPColor, "AttributeValueIABPColor");
            RegisterType("AttributeValueIDPColor", AttributeValueIDPColor, "AttributeValueIDPColor");
            RegisterType("AttributeValueOABPColor", AttributeValueOABPColor, "AttributeValueOABPColor");
            RegisterType("AttributeValueODPColor", AttributeValueODPColor, "AttributeValueODPColor");
            RegisterType("AttributeValueSPColor", AttributeValueSPColor, "AttributeValueSPColor");
            RegisterType("AttriubteValueAVOPColor", AttriubteValueAVOPColor, "AttriubteValueAVOPColor");
            RegisterType("AutoFightUseLevel", AutoFightUseLevel, "AutoFightUseLevel");
            RegisterType("BaseRankConditionLevel", BaseRankConditionLevel, "BaseRankConditionLevel");
            RegisterType("BaseRankConditionLimit", BaseRankConditionLimit, "BaseRankConditionLimit");
            RegisterType("BaseRankFightValue", BaseRankFightValue, "BaseRankFightValue");
            RegisterType("BaseRankRankSize", BaseRankRankSize, "BaseRankRankSize");
            RegisterType("BattleAvatarTemplateId", BattleAvatarTemplateId, "BattleAvatarTemplateId");
            RegisterType("BattleEndShowingEffectTime", BattleEndShowingEffectTime, "BattleEndShowingEffectTime");
            RegisterType("BattleEnvPlayerTemplateId", BattleEnvPlayerTemplateId, "BattleEnvPlayerTemplateId");
            RegisterType("BattlePlayerTemplateId", BattlePlayerTemplateId, "BattlePlayerTemplateId");
            RegisterType("CentryCitySceneId", CentryCitySceneId, "CentryCitySceneId");
            RegisterType("ChatInputMaxCount", ChatInputMaxCount, "ChatInputMaxCount");
            RegisterType("ChatListLimitCount", ChatListLimitCount, "ChatListLimitCount");
            RegisterType("CityAvatarTemplateId", CityAvatarTemplateId, "CityAvatarTemplateId");
            RegisterType("CombatAfterGameEndTimeOutTime", CombatAfterGameEndTimeOutTime, "CombatAfterGameEndTimeOutTime");
            RegisterType("CombatAllConnectionLosedTimeOutTime", CombatAllConnectionLosedTimeOutTime, "CombatAllConnectionLosedTimeOutTime");
            RegisterType("CombatBeforeStartGameTimeOutTime", CombatBeforeStartGameTimeOutTime, "CombatBeforeStartGameTimeOutTime");
            RegisterType("CombatFormationDeltaDegrees", CombatFormationDeltaDegrees, "CombatFormationDeltaDegrees");
            RegisterType("CombatMultiDungeonTimeLimit", CombatMultiDungeonTimeLimit, "CombatMultiDungeonTimeLimit");
            RegisterType("CombatRealTime1v1TimeLimit", CombatRealTime1v1TimeLimit, "CombatRealTime1v1TimeLimit");
            RegisterType("CombatWaitOtherPlayerConnectTimeOutTime", CombatWaitOtherPlayerConnectTimeOutTime, "CombatWaitOtherPlayerConnectTimeOutTime");
            RegisterType("ConcentrateFireAbilityId", ConcentrateFireAbilityId, "ConcentrateFireAbilityId");
            RegisterType("CreateAvatarTemplateId", CreateAvatarTemplateId, "CreateAvatarTemplateId");
            RegisterType("DamageTextOffsetY", DamageTextOffsetY, "DamageTextOffsetY");
            RegisterType("DartPeriod", DartPeriod, "DartPeriod");
            RegisterType("DefaultWeaponInCityID", DefaultWeaponInCityID, "DefaultWeaponInCityID");
            RegisterType("DefaultWeaponInDungeonID", DefaultWeaponInDungeonID, "DefaultWeaponInDungeonID");
            RegisterType("DemonstrationBattlePlayerDummyGroupId", DemonstrationBattlePlayerDummyGroupId, "DemonstrationBattlePlayerDummyGroupId");
            RegisterType("DemonstrationBattlePlayerGirlDummyGroupId", DemonstrationBattlePlayerGirlDummyGroupId, "DemonstrationBattlePlayerGirlDummyGroupId");
            RegisterType("DialogEableSendLevel", DialogEableSendLevel, "DialogEableSendLevel");
            RegisterType("DialogEableSendVipLevel", DialogEableSendVipLevel, "DialogEableSendVipLevel");
            RegisterType("DialogItemLimitCountFaction", DialogItemLimitCountFaction, "DialogItemLimitCountFaction");
            RegisterType("DialogItemLimitCountGM", DialogItemLimitCountGM, "DialogItemLimitCountGM");
            RegisterType("DialogItemLimitCountSystem", DialogItemLimitCountSystem, "DialogItemLimitCountSystem");
            RegisterType("DialogItemLimitCountTotal", DialogItemLimitCountTotal, "DialogItemLimitCountTotal");
            RegisterType("DialogItemLimitCountWorld", DialogItemLimitCountWorld, "DialogItemLimitCountWorld");
            RegisterType("DungeonFightConditionAutoHideTime", DungeonFightConditionAutoHideTime, "DungeonFightConditionAutoHideTime");
            RegisterType("DungeonGuideArrowRotationSpeed", DungeonGuideArrowRotationSpeed, "DungeonGuideArrowRotationSpeed");
            RegisterType("DungeonViewerAvatarTemplateId", DungeonViewerAvatarTemplateId, "DungeonViewerAvatarTemplateId");
            RegisterType("EmailClientSize", EmailClientSize, "EmailClientSize");
            RegisterType("EmailMaxSize", EmailMaxSize, "EmailMaxSize");
            RegisterType("EquipmentBuyBelt", EquipmentBuyBelt, "EquipmentBuyBelt");
            RegisterType("EquipmentBuyCloth", EquipmentBuyCloth, "EquipmentBuyCloth");
            RegisterType("EquipmentBuyHat", EquipmentBuyHat, "EquipmentBuyHat");
            RegisterType("EquipmentBuyNecklace", EquipmentBuyNecklace, "EquipmentBuyNecklace");
            RegisterType("EquipmentBuyRing", EquipmentBuyRing, "EquipmentBuyRing");
            RegisterType("EquipmentBuyWeapon", EquipmentBuyWeapon, "EquipmentBuyWeapon");
            RegisterType("EquipmentPutonMinLevelBelt", EquipmentPutonMinLevelBelt, "EquipmentPutonMinLevelBelt");
            RegisterType("EquipmentPutonMinLevelCloth", EquipmentPutonMinLevelCloth, "EquipmentPutonMinLevelCloth");
            RegisterType("EquipmentPutonMinLevelHat", EquipmentPutonMinLevelHat, "EquipmentPutonMinLevelHat");
            RegisterType("EquipmentPutonMinLevelNecklace", EquipmentPutonMinLevelNecklace, "EquipmentPutonMinLevelNecklace");
            RegisterType("EquipmentPutonMinLevelRing", EquipmentPutonMinLevelRing, "EquipmentPutonMinLevelRing");
            RegisterType("EquipmentPutonMinLevelWeapon", EquipmentPutonMinLevelWeapon, "EquipmentPutonMinLevelWeapon");
            RegisterType("EquipmentRecastBeginCondition", EquipmentRecastBeginCondition, "EquipmentRecastBeginCondition");
            RegisterType("EquipmentRefineBeginCondition", EquipmentRefineBeginCondition, "EquipmentRefineBeginCondition");
            RegisterType("EquipmentRefineMaxLevel", EquipmentRefineMaxLevel, "EquipmentRefineMaxLevel");
            RegisterType("EquipmentReinforceBeginCondition", EquipmentReinforceBeginCondition, "EquipmentReinforceBeginCondition");
            RegisterType("EquipmentSlotCount", EquipmentSlotCount, "EquipmentSlotCount");
            RegisterType("Errantry", Errantry, "Errantry");
            RegisterType("Exp", Exp, "Exp");
            RegisterType("FastCleanDungeon", FastCleanDungeon, "FastCleanDungeon");
            RegisterType("FirstAcupointId", FirstAcupointId, "FirstAcupointId");
            RegisterType("FirstFakeBattleSceneId", FirstFakeBattleSceneId, "FirstFakeBattleSceneId");
            RegisterType("FriendsBlessDailyCount", FriendsBlessDailyCount, "FriendsBlessDailyCount");
            RegisterType("FriendsBlessGainStaminCount", FriendsBlessGainStaminCount, "FriendsBlessGainStaminCount");
            RegisterType("FriendsCdTime", FriendsCdTime, "FriendsCdTime");
            RegisterType("FriendsChatLimitCount", FriendsChatLimitCount, "FriendsChatLimitCount");
            RegisterType("FriendShipUpLimit", FriendShipUpLimit, "FriendShipUpLimit");
            RegisterType("FriendsLimitCount", FriendsLimitCount, "FriendsLimitCount");
            RegisterType("FriendsMsgCount", FriendsMsgCount, "FriendsMsgCount");
            RegisterType("FriendsPushLevel", FriendsPushLevel, "FriendsPushLevel");
            RegisterType("GameMoney", GameMoney, "GameMoney");
            RegisterType("GrowthFundBuyNeedRealMoney", GrowthFundBuyNeedRealMoney, "GrowthFundBuyNeedRealMoney");
            RegisterType("GrowthFundBuyNeedVip", GrowthFundBuyNeedVip, "GrowthFundBuyNeedVip");
            RegisterType("HeartPowerActiveLevel", HeartPowerActiveLevel, "HeartPowerActiveLevel");
            RegisterType("HeroFightMaxNum", HeroFightMaxNum, "HeroFightMaxNum");
            RegisterType("IdleOnceTimeSection", IdleOnceTimeSection, "IdleOnceTimeSection");
            RegisterType("InitialAttributeLevel", InitialAttributeLevel, "InitialAttributeLevel");
            RegisterType("InitRankGradId", InitRankGradId, "InitRankGradId");
            RegisterType("InvalidConfigValue", InvalidConfigValue, "InvalidConfigValue");
            RegisterType("LeagueApplyCount", LeagueApplyCount, "LeagueApplyCount");
            RegisterType("LeagueCreateLevel", LeagueCreateLevel, "LeagueCreateLevel");
            RegisterType("LeagueCreateRealMoney", LeagueCreateRealMoney, "LeagueCreateRealMoney");
            RegisterType("LeagueInviteCD", LeagueInviteCD, "LeagueInviteCD");
            RegisterType("LeagueMaxLevel", LeagueMaxLevel, "LeagueMaxLevel");
            RegisterType("LootExpItemId", LootExpItemId, "LootExpItemId");
            RegisterType("LootMoneyItemId", LootMoneyItemId, "LootMoneyItemId");
            RegisterType("LootRealMoneyItemId", LootRealMoneyItemId, "LootRealMoneyItemId");
            RegisterType("LootReputationItemId", LootReputationItemId, "LootReputationItemId");
            RegisterType("MatrixCentreSlotCount", MatrixCentreSlotCount, "MatrixCentreSlotCount");
            RegisterType("MaxComeOnFxCount", MaxComeOnFxCount, "MaxComeOnFxCount");
            RegisterType("MaxConfigHint", MaxConfigHint, "MaxConfigHint");
            RegisterType("MaxInScreenShowingAvatarCount", MaxInScreenShowingAvatarCount, "MaxInScreenShowingAvatarCount");
            RegisterType("MaxLevel", MaxLevel, "MaxLevel");
            RegisterType("MaxStamina", MaxStamina, "MaxStamina");
            RegisterType("MirrorArenaBattleClearCoolCost", MirrorArenaBattleClearCoolCost, "MirrorArenaBattleClearCoolCost");
            RegisterType("MirrorArenaBattleCoolTime", MirrorArenaBattleCoolTime, "MirrorArenaBattleCoolTime");
            RegisterType("MirrorArenaBattleCount", MirrorArenaBattleCount, "MirrorArenaBattleCount");
            RegisterType("MirrorArenaBattleCountDownTime", MirrorArenaBattleCountDownTime, "MirrorArenaBattleCountDownTime");
            RegisterType("MirrorArenaBattleLimitTime", MirrorArenaBattleLimitTime, "MirrorArenaBattleLimitTime");
            RegisterType("MirrorArenaBattleRecordCount", MirrorArenaBattleRecordCount, "MirrorArenaBattleRecordCount");
            RegisterType("MirrorArenaBattleSceneId", MirrorArenaBattleSceneId, "MirrorArenaBattleSceneId");
            RegisterType("MirrorArenaBuyBattleCountCost", MirrorArenaBuyBattleCountCost, "MirrorArenaBuyBattleCountCost");
            RegisterType("MirrorArenaEachDayRewardCount", MirrorArenaEachDayRewardCount, "MirrorArenaEachDayRewardCount");
            RegisterType("MirrorArenaNormalRefreshCost", MirrorArenaNormalRefreshCost, "MirrorArenaNormalRefreshCost");
            RegisterType("MirrorArenaNormalRefreshCount", MirrorArenaNormalRefreshCount, "MirrorArenaNormalRefreshCount");
            RegisterType("MirrorArenaNormalRefreshRange", MirrorArenaNormalRefreshRange, "MirrorArenaNormalRefreshRange");
            RegisterType("MirrorArenaVipRefreshCost", MirrorArenaVipRefreshCost, "MirrorArenaVipRefreshCost");
            RegisterType("MirrorArenaVipRefreshRange", MirrorArenaVipRefreshRange, "MirrorArenaVipRefreshRange");
            RegisterType("MonsterCreaterTemplateId", MonsterCreaterTemplateId, "MonsterCreaterTemplateId");
            RegisterType("MonsterPatrolIdleMs", MonsterPatrolIdleMs, "MonsterPatrolIdleMs");
            RegisterType("MonthcardBuyCountLimit", MonthcardBuyCountLimit, "MonthcardBuyCountLimit");
            RegisterType("MultiDungeonMaxShowGroupCount", MultiDungeonMaxShowGroupCount, "MultiDungeonMaxShowGroupCount");
            RegisterType("MultiDungeonResetAwardCount", MultiDungeonResetAwardCount, "MultiDungeonResetAwardCount");
            RegisterType("NameMaxLength", NameMaxLength, "NameMaxLength");
            RegisterType("New1v1ArenaSceneId", New1v1ArenaSceneId, "New1v1ArenaSceneId");
            RegisterType("New1v1FirstCombatDummyId", New1v1FirstCombatDummyId, "New1v1FirstCombatDummyId");
            RegisterType("New1v1FixPerTime", New1v1FixPerTime, "New1v1FixPerTime");
            RegisterType("New1v1FloorEmailId", New1v1FloorEmailId, "New1v1FloorEmailId");
            RegisterType("New1v1FloorScore", New1v1FloorScore, "New1v1FloorScore");
            RegisterType("New1v1LoseScore", New1v1LoseScore, "New1v1LoseScore");
            RegisterType("New1v1MatchTime", New1v1MatchTime, "New1v1MatchTime");
            RegisterType("New1v1PerTimeFixLevel", New1v1PerTimeFixLevel, "New1v1PerTimeFixLevel");
            RegisterType("New1v1RankMinScore", New1v1RankMinScore, "New1v1RankMinScore");
            RegisterType("New1v1SceneId", New1v1SceneId, "New1v1SceneId");
            RegisterType("New1v1WinScore", New1v1WinScore, "New1v1WinScore");
            RegisterType("NpcCollectionTemplateId", NpcCollectionTemplateId, "NpcCollectionTemplateId");
            RegisterType("NpcTemplateId", NpcTemplateId, "NpcTemplateId");
            RegisterType("NullDataValue", NullDataValue, "NullDataValue");
            RegisterType("OnevsoneArenaCooldown", OnevsoneArenaCooldown, "OnevsoneArenaCooldown");
            RegisterType("OnevsoneArenaLimitTime", OnevsoneArenaLimitTime, "OnevsoneArenaLimitTime");
            RegisterType("OnevsoneArenaMatchRangeFix", OnevsoneArenaMatchRangeFix, "OnevsoneArenaMatchRangeFix");
            RegisterType("OnevsoneArenaMatchTimeMax", OnevsoneArenaMatchTimeMax, "OnevsoneArenaMatchTimeMax");
            RegisterType("OnevsoneArenaMatchTimeNormal", OnevsoneArenaMatchTimeNormal, "OnevsoneArenaMatchTimeNormal");
            RegisterType("OnevsoneArenaMaxRecord", OnevsoneArenaMaxRecord, "OnevsoneArenaMaxRecord");
            RegisterType("OnevsoneArenaOpenEmail", OnevsoneArenaOpenEmail, "OnevsoneArenaOpenEmail");
            RegisterType("OnevsoneArenaRankShow", OnevsoneArenaRankShow, "OnevsoneArenaRankShow");
            RegisterType("OnevsoneArenaSceneId", OnevsoneArenaSceneId, "OnevsoneArenaSceneId");
            RegisterType("OnevsoneArenaScoreInit", OnevsoneArenaScoreInit, "OnevsoneArenaScoreInit");
            RegisterType("OnevsoneArenaScoreMax", OnevsoneArenaScoreMax, "OnevsoneArenaScoreMax");
            RegisterType("OnevsoneArenaScoreMin", OnevsoneArenaScoreMin, "OnevsoneArenaScoreMin");
            RegisterType("OnevsoneArenaSeasonDuration", OnevsoneArenaSeasonDuration, "OnevsoneArenaSeasonDuration");
            RegisterType("OnevsoneArenaStraightBroadcast", OnevsoneArenaStraightBroadcast, "OnevsoneArenaStraightBroadcast");
            RegisterType("OnevsOneCanMeetDummyCount", OnevsOneCanMeetDummyCount, "OnevsOneCanMeetDummyCount");
            RegisterType("OneVsOneClearCoolTimeCost", OneVsOneClearCoolTimeCost, "OneVsOneClearCoolTimeCost");
            RegisterType("OnevsOneOpenNextSeasonBreakTime", OnevsOneOpenNextSeasonBreakTime, "OnevsOneOpenNextSeasonBreakTime");
            RegisterType("OpenChangeHero", OpenChangeHero, "OpenChangeHero");
            RegisterType("OpenFriends", OpenFriends, "OpenFriends");
            RegisterType("OpenHeartPower", OpenHeartPower, "OpenHeartPower");
            RegisterType("OpenHire", OpenHire, "OpenHire");
            RegisterType("OpenJewelCompose", OpenJewelCompose, "OpenJewelCompose");
            RegisterType("OpenRealm", OpenRealm, "OpenRealm");
            RegisterType("OpenStore", OpenStore, "OpenStore");
            RegisterType("OpenTimeLimitDungeon", OpenTimeLimitDungeon, "OpenTimeLimitDungeon");
            RegisterType("OpenTreasure", OpenTreasure, "OpenTreasure");
            RegisterType("OpenTutor", OpenTutor, "OpenTutor");
            RegisterType("OpenWorldChatLevel", OpenWorldChatLevel, "OpenWorldChatLevel");
            RegisterType("OpenWorldChatVIPLevel", OpenWorldChatVIPLevel, "OpenWorldChatVIPLevel");
            RegisterType("PropertyRateDivisionFactor", PropertyRateDivisionFactor, "PropertyRateDivisionFactor");
            RegisterType("RandomMissionCanDoneCount", RandomMissionCanDoneCount, "RandomMissionCanDoneCount");
            RegisterType("RandomMissionReceiveCount", RandomMissionReceiveCount, "RandomMissionReceiveCount");
            RegisterType("RandomMissionRefreshApart", RandomMissionRefreshApart, "RandomMissionRefreshApart");
            RegisterType("RandomMissionRefreshCost", RandomMissionRefreshCost, "RandomMissionRefreshCost");
            RegisterType("RandomMissionRefreshCount", RandomMissionRefreshCount, "RandomMissionRefreshCount");
            RegisterType("RandomMissionRefreshFree", RandomMissionRefreshFree, "RandomMissionRefreshFree");
            RegisterType("RandomMissionRefreshTimesLimit", RandomMissionRefreshTimesLimit, "RandomMissionRefreshTimesLimit");
            RegisterType("RealMoney", RealMoney, "RealMoney");
            RegisterType("RealTime1v1BattleCountDownTime", RealTime1v1BattleCountDownTime, "RealTime1v1BattleCountDownTime");
            RegisterType("ReConnectAutoActionCutdownTime", ReConnectAutoActionCutdownTime, "ReConnectAutoActionCutdownTime");
            RegisterType("RedPacketsDailyPickCount", RedPacketsDailyPickCount, "RedPacketsDailyPickCount");
            RegisterType("RenameItemId", RenameItemId, "RenameItemId");
            RegisterType("Repution", Repution, "Repution");
            RegisterType("ResourceFloatingTextInterval", ResourceFloatingTextInterval, "ResourceFloatingTextInterval");
            RegisterType("RoleGameMoneyMax", RoleGameMoneyMax, "RoleGameMoneyMax");
            RegisterType("ServiceControl", ServiceControl, "ServiceControl");
            RegisterType("SignGoToNpcId", SignGoToNpcId, "SignGoToNpcId");
            RegisterType("SkillViewerBattleAvatarTemplateId", SkillViewerBattleAvatarTemplateId, "SkillViewerBattleAvatarTemplateId");
            RegisterType("StaminaId", StaminaId, "StaminaId");
            RegisterType("StaminaRecoverInterval", StaminaRecoverInterval, "StaminaRecoverInterval");
            RegisterType("StaminaRecoverQuantity", StaminaRecoverQuantity, "StaminaRecoverQuantity");
            RegisterType("StoreBlackRefreshCost", StoreBlackRefreshCost, "StoreBlackRefreshCost");
            RegisterType("StoreBlackUnlock", StoreBlackUnlock, "StoreBlackUnlock");
            RegisterType("StoreDeluxeBlackRefreshCost", StoreDeluxeBlackRefreshCost, "StoreDeluxeBlackRefreshCost");
            RegisterType("StoreDeluxeBlackUnlock", StoreDeluxeBlackUnlock, "StoreDeluxeBlackUnlock");
            RegisterType("StoreNormalRefreshCost", StoreNormalRefreshCost, "StoreNormalRefreshCost");
            RegisterType("StoreNormalUnlock", StoreNormalUnlock, "StoreNormalUnlock");
            RegisterType("StoreRefreshCountResetDayOfWeek", StoreRefreshCountResetDayOfWeek, "StoreRefreshCountResetDayOfWeek");
            RegisterType("StoryCompaignFightTimes", StoryCompaignFightTimes, "StoryCompaignFightTimes");
            RegisterType("TalkContentRefreshTimeSecond", TalkContentRefreshTimeSecond, "TalkContentRefreshTimeSecond");
            RegisterType("TalkForbidEndTimeSecond", TalkForbidEndTimeSecond, "TalkForbidEndTimeSecond");
            RegisterType("TalkNoForbidVipLevel", TalkNoForbidVipLevel, "TalkNoForbidVipLevel");
            RegisterType("TimeLimitDungeonEmail", TimeLimitDungeonEmail, "TimeLimitDungeonEmail");
            RegisterType("TimeLimitDungeonFailedLimit", TimeLimitDungeonFailedLimit, "TimeLimitDungeonFailedLimit");
            RegisterType("TimeLimitDungeonFirstDefaultId", TimeLimitDungeonFirstDefaultId, "TimeLimitDungeonFirstDefaultId");
            RegisterType("TimeLimitDungeonMax", TimeLimitDungeonMax, "TimeLimitDungeonMax");
            RegisterType("TimeLimitDungeonRankCount", TimeLimitDungeonRankCount, "TimeLimitDungeonRankCount");
            RegisterType("TotemNotDamageableTemplateId", TotemNotDamageableTemplateId, "TotemNotDamageableTemplateId");
            RegisterType("TowerBattleTimesEachDay", TowerBattleTimesEachDay, "TowerBattleTimesEachDay");
            RegisterType("TowerFreeRefreshCount", TowerFreeRefreshCount, "TowerFreeRefreshCount");
            RegisterType("TowerGoldRefreshCost", TowerGoldRefreshCost, "TowerGoldRefreshCost");
            RegisterType("TowerRefreshTime", TowerRefreshTime, "TowerRefreshTime");
            RegisterType("TrialBossFailedRefreshMoney", TrialBossFailedRefreshMoney, "TrialBossFailedRefreshMoney");
            RegisterType("TrialBossKilledRefreshMoney", TrialBossKilledRefreshMoney, "TrialBossKilledRefreshMoney");
            RegisterType("TrialCount", TrialCount, "TrialCount");
            RegisterType("TrialFailedCooldown", TrialFailedCooldown, "TrialFailedCooldown");
            RegisterType("TrialKillCombatCooldown", TrialKillCombatCooldown, "TrialKillCombatCooldown");
            RegisterType("UIPnlUseBoxMaxNum", UIPnlUseBoxMaxNum, "UIPnlUseBoxMaxNum");
            RegisterType("UIRoleInfoIgnoreCastingTime", UIRoleInfoIgnoreCastingTime, "UIRoleInfoIgnoreCastingTime");
            RegisterType("UIRoleInfoIgnoreControlledTime", UIRoleInfoIgnoreControlledTime, "UIRoleInfoIgnoreControlledTime");
            RegisterType("VoiceDialogEnableLength", VoiceDialogEnableLength, "VoiceDialogEnableLength");
            RegisterType("VoiceDialogEnableMaxLength", VoiceDialogEnableMaxLength, "VoiceDialogEnableMaxLength");
            RegisterType("WorldBossBattleCD", WorldBossBattleCD, "WorldBossBattleCD");
            RegisterType("WorldBossBattleCDClearCost", WorldBossBattleCDClearCost, "WorldBossBattleCDClearCost");
            RegisterType("WorldBossBattleSence", WorldBossBattleSence, "WorldBossBattleSence");
            RegisterType("WorldBossBattleTimeLimit", WorldBossBattleTimeLimit, "WorldBossBattleTimeLimit");
            RegisterType("WorldBossBeforeBattleTime", WorldBossBeforeBattleTime, "WorldBossBeforeBattleTime");
            RegisterType("WorldBossDefaultBossId", WorldBossDefaultBossId, "WorldBossDefaultBossId");
            RegisterType("WorldBossLastRankNum", WorldBossLastRankNum, "WorldBossLastRankNum");
            RegisterType("WorldBossPrepareBattleSence", WorldBossPrepareBattleSence, "WorldBossPrepareBattleSence");
            RegisterType("WorldBossPrepareBattleTimeLimit", WorldBossPrepareBattleTimeLimit, "WorldBossPrepareBattleTimeLimit");
            RegisterType("WorldBossPrepareMaxNum", WorldBossPrepareMaxNum, "WorldBossPrepareMaxNum");
            RegisterType("WorldBossPrepareMinParticipant", WorldBossPrepareMinParticipant, "WorldBossPrepareMinParticipant");
            RegisterType("WorldBossStoreRefreshCost", WorldBossStoreRefreshCost, "WorldBossStoreRefreshCost");
            RegisterType("WorldChatCDTime", WorldChatCDTime, "WorldChatCDTime");
            RegisterType("WorldMapAnimLimitLevel", WorldMapAnimLimitLevel, "WorldMapAnimLimitLevel");
            RegisterType("WorldMapInertiaReduceFrames", WorldMapInertiaReduceFrames, "WorldMapInertiaReduceFrames");
            RegisterType("AutoFightEffect", AutoFightEffect, "AutoFightEffect");
            RegisterType("AvatarInformationMenu", AvatarInformationMenu, "AvatarInformationMenu");
            RegisterType("AvatarMoveEffectName", AvatarMoveEffectName, "AvatarMoveEffectName");
            RegisterType("BackBuildingModeName", BackBuildingModeName, "BackBuildingModeName");
            RegisterType("BackBuildingPos", BackBuildingPos, "BackBuildingPos");
            RegisterType("BagPages", BagPages, "BagPages");
            RegisterType("BaseRankRefreshWeeklyTime", BaseRankRefreshWeeklyTime, "BaseRankRefreshWeeklyTime");
            RegisterType("CentryCityName", CentryCityName, "CentryCityName");
            RegisterType("ChatMessageSelfColor", ChatMessageSelfColor, "ChatMessageSelfColor");
            RegisterType("ClickGroundEffect", ClickGroundEffect, "ClickGroundEffect");
            RegisterType("ClientEquipmentLevelUpSuccessEffect", ClientEquipmentLevelUpSuccessEffect, "ClientEquipmentLevelUpSuccessEffect");
            RegisterType("ClientEquipmentStarUpFailedEffect", ClientEquipmentStarUpFailedEffect, "ClientEquipmentStarUpFailedEffect");
            RegisterType("ClientTreasureDescomposeSuccessEffect", ClientTreasureDescomposeSuccessEffect, "ClientTreasureDescomposeSuccessEffect");
            RegisterType("ClientTreasureRepairedFailedEffect", ClientTreasureRepairedFailedEffect, "ClientTreasureRepairedFailedEffect");
            RegisterType("ClientTreasureRepairedSuccessEffect", ClientTreasureRepairedSuccessEffect, "ClientTreasureRepairedSuccessEffect");
            RegisterType("ClientTreasureSelectedEffect", ClientTreasureSelectedEffect, "ClientTreasureSelectedEffect");
            RegisterType("CollectItemText", CollectItemText, "CollectItemText");
            RegisterType("CollectResourceText", CollectResourceText, "CollectResourceText");
            RegisterType("DailyBenefitEffect", DailyBenefitEffect, "DailyBenefitEffect");
            RegisterType("DailyBenefitNpcId", DailyBenefitNpcId, "DailyBenefitNpcId");
            RegisterType("DailyMissionLivenessAndReward", DailyMissionLivenessAndReward, "DailyMissionLivenessAndReward");
            RegisterType("DailyMissionRefreshTime", DailyMissionRefreshTime, "DailyMissionRefreshTime");
            RegisterType("DamageParticleBone", DamageParticleBone, "DamageParticleBone");
            RegisterType("DamageTextAbsorb", DamageTextAbsorb, "DamageTextAbsorb");
            RegisterType("DamageTextBlock", DamageTextBlock, "DamageTextBlock");
            RegisterType("DamageTextDurDamageEnemy", DamageTextDurDamageEnemy, "DamageTextDurDamageEnemy");
            RegisterType("DamageTextDurDamageHero", DamageTextDurDamageHero, "DamageTextDurDamageHero");
            RegisterType("DamageTextDurHeal", DamageTextDurHeal, "DamageTextDurHeal");
            RegisterType("DamageTextHeal", DamageTextHeal, "DamageTextHeal");
            RegisterType("DamageTextHealCritical", DamageTextHealCritical, "DamageTextHealCritical");
            RegisterType("DamageTextMiss", DamageTextMiss, "DamageTextMiss");
            RegisterType("DamageTextReduce", DamageTextReduce, "DamageTextReduce");
            RegisterType("DamageTextReflect", DamageTextReflect, "DamageTextReflect");
            RegisterType("DamageTextSkill", DamageTextSkill, "DamageTextSkill");
            RegisterType("DamageTextSkillCritical", DamageTextSkillCritical, "DamageTextSkillCritical");
            RegisterType("DamageTextSkillInterrupted", DamageTextSkillInterrupted, "DamageTextSkillInterrupted");
            RegisterType("DamageTextWeapon", DamageTextWeapon, "DamageTextWeapon");
            RegisterType("DamageTextWeaponCritical", DamageTextWeaponCritical, "DamageTextWeaponCritical");
            RegisterType("DartDescription", DartDescription, "DartDescription");
            RegisterType("DungeonGuideArrowResName", DungeonGuideArrowResName, "DungeonGuideArrowResName");
            RegisterType("DungeonResetTime", DungeonResetTime, "DungeonResetTime");
            RegisterType("EmailDefaultSender", EmailDefaultSender, "EmailDefaultSender");
            RegisterType("EmailDefaultSenderIcon", EmailDefaultSenderIcon, "EmailDefaultSenderIcon");
            RegisterType("ExchangePage", ExchangePage, "ExchangePage");
            RegisterType("FriendsBlessMessage", FriendsBlessMessage, "FriendsBlessMessage");
            RegisterType("GemComposeSuccessEffect", GemComposeSuccessEffect, "GemComposeSuccessEffect");
            RegisterType("GoodsYuanBao", GoodsYuanBao, "GoodsYuanBao");
            RegisterType("GoodsYueKa", GoodsYueKa, "GoodsYueKa");
            RegisterType("GoodsZengSong", GoodsZengSong, "GoodsZengSong");
            RegisterType("GraduateHigh", GraduateHigh, "GraduateHigh");
            RegisterType("GraduateMiddle", GraduateMiddle, "GraduateMiddle");
            RegisterType("GraduatePrimary", GraduatePrimary, "GraduatePrimary");
            RegisterType("IDamageFormula", IDamageFormula, "IDamageFormula");
            RegisterType("KShaderMixFactorName", KShaderMixFactorName, "KShaderMixFactorName");
            RegisterType("LeagueBossDungeonOpenTime", LeagueBossDungeonOpenTime, "LeagueBossDungeonOpenTime");
            RegisterType("LevelUpEffect", LevelUpEffect, "LevelUpEffect");
            RegisterType("LightFactorByTime", LightFactorByTime, "LightFactorByTime");
            RegisterType("LoginAndSelectAreaMusicName", LoginAndSelectAreaMusicName, "LoginAndSelectAreaMusicName");
            RegisterType("LootDropHeapCountPercent", LootDropHeapCountPercent, "LootDropHeapCountPercent");
            RegisterType("MentalTypeUpEffect", MentalTypeUpEffect, "MentalTypeUpEffect");
            RegisterType("MirrorArenaBattleResetTime", MirrorArenaBattleResetTime, "MirrorArenaBattleResetTime");
            RegisterType("MirrorArenaDefaultDummyGroupRank", MirrorArenaDefaultDummyGroupRank, "MirrorArenaDefaultDummyGroupRank");
            RegisterType("MirrorArenaEachWeekRewardCalcTime", MirrorArenaEachWeekRewardCalcTime, "MirrorArenaEachWeekRewardCalcTime");
            RegisterType("MonsterCreaterActiveWaveAnimClip", MonsterCreaterActiveWaveAnimClip, "MonsterCreaterActiveWaveAnimClip");
            RegisterType("MonsterCreaterActiveWaveByOtherDeath", MonsterCreaterActiveWaveByOtherDeath, "MonsterCreaterActiveWaveByOtherDeath");
            RegisterType("MonsterCreaterIdleAnimClip", MonsterCreaterIdleAnimClip, "MonsterCreaterIdleAnimClip");
            RegisterType("New1v1LevelSection", New1v1LevelSection, "New1v1LevelSection");
            RegisterType("New1v1LevelSubtractScore", New1v1LevelSubtractScore, "New1v1LevelSubtractScore");
            RegisterType("New1v1OpenTimeRange", New1v1OpenTimeRange, "New1v1OpenTimeRange");
            RegisterType("New1v1SendRewardTime", New1v1SendRewardTime, "New1v1SendRewardTime");
            RegisterType("ODamageFormula", ODamageFormula, "ODamageFormula");
            RegisterType("OnevsoneArenaChallengeTime", OnevsoneArenaChallengeTime, "OnevsoneArenaChallengeTime");
            RegisterType("OnevsoneArenaScoreFix", OnevsoneArenaScoreFix, "OnevsoneArenaScoreFix");
            RegisterType("OnevsOneDummyDetail", OnevsOneDummyDetail, "OnevsOneDummyDetail");
            RegisterType("OnevsOneMeetDummyCountRefreshTime", OnevsOneMeetDummyCountRefreshTime, "OnevsOneMeetDummyCountRefreshTime");
            RegisterType("PlayerGuideList", PlayerGuideList, "PlayerGuideList");
            RegisterType("QualityColorFive", QualityColorFive, "QualityColorFive");
            RegisterType("QualityColorFour", QualityColorFour, "QualityColorFour");
            RegisterType("QualityColorOne", QualityColorOne, "QualityColorOne");
            RegisterType("QualityColorThree", QualityColorThree, "QualityColorThree");
            RegisterType("QualityColorTwo", QualityColorTwo, "QualityColorTwo");
            RegisterType("QualityColorZero", QualityColorZero, "QualityColorZero");
            RegisterType("RandomMissionResetTime", RandomMissionResetTime, "RandomMissionResetTime");
            RegisterType("RoleDefaultShader", RoleDefaultShader, "RoleDefaultShader");
            RegisterType("RoleLevelRewardDetail", RoleLevelRewardDetail, "RoleLevelRewardDetail");
            RegisterType("RoleOutLineShader", RoleOutLineShader, "RoleOutLineShader");
            RegisterType("SelectDungonBGM", SelectDungonBGM, "SelectDungonBGM");
            RegisterType("SelectedAura", SelectedAura, "SelectedAura");
            RegisterType("SkillLevelUpEffect", SkillLevelUpEffect, "SkillLevelUpEffect");
            RegisterType("StaminaBuyResetTime", StaminaBuyResetTime, "StaminaBuyResetTime");
            RegisterType("StoreBlackDailyRefreshTime", StoreBlackDailyRefreshTime, "StoreBlackDailyRefreshTime");
            RegisterType("StoreCommonDailyRefreshTime", StoreCommonDailyRefreshTime, "StoreCommonDailyRefreshTime");
            RegisterType("StoreDailyRefreshTime", StoreDailyRefreshTime, "StoreDailyRefreshTime");
            RegisterType("StoreDeluxeBlackRefreshTime", StoreDeluxeBlackRefreshTime, "StoreDeluxeBlackRefreshTime");
            RegisterType("TestString", TestString, "TestString");
            RegisterType("TextSkillEffect", TextSkillEffect, "TextSkillEffect");
            RegisterType("TextSkillNextLevelEffect", TextSkillNextLevelEffect, "TextSkillNextLevelEffect");
            RegisterType("TextSkillNextTypeEffect", TextSkillNextTypeEffect, "TextSkillNextTypeEffect");
            RegisterType("TimeLimitDungeonDesc", TimeLimitDungeonDesc, "TimeLimitDungeonDesc");
            RegisterType("TimeLimitDungeonEmailContent", TimeLimitDungeonEmailContent, "TimeLimitDungeonEmailContent");
            RegisterType("TimeLimitDungeonRefreshTime", TimeLimitDungeonRefreshTime, "TimeLimitDungeonRefreshTime");
            RegisterType("TimeZone", TimeZone, "TimeZone");
            RegisterType("TitleBlock", TitleBlock, "TitleBlock");
            RegisterType("TrialRefreshTime", TrialRefreshTime, "TrialRefreshTime");
            RegisterType("UILightFactorByTime", UILightFactorByTime, "UILightFactorByTime");
            RegisterType("UIMainMenuButtons", UIMainMenuButtons, "UIMainMenuButtons");
            RegisterType("UIPnlAnnouncementAnnouncementURL", UIPnlAnnouncementAnnouncementURL, "UIPnlAnnouncementAnnouncementURL");
            RegisterType("UIPnlSignInAlreadySignLabel", UIPnlSignInAlreadySignLabel, "UIPnlSignInAlreadySignLabel");
            RegisterType("WorldBossDuration", WorldBossDuration, "WorldBossDuration");
            RegisterType("WorldBossPrepareDuration", WorldBossPrepareDuration, "WorldBossPrepareDuration");
            RegisterType("WorldBossStoreRefreshTime", WorldBossStoreRefreshTime, "WorldBossStoreRefreshTime");
            RegisterType("TestDouble", TestDouble, "TestDouble");

            initialized = true;
        }
    }

}
