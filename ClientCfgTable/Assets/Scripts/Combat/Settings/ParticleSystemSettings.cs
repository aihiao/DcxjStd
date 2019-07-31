using LywGames;
using ClientCommon;

namespace Combat
{
	public class ParticleSystemSettings
	{
		public class M
		{
			public const string Node = "ParticleSystemAbilitySettings";
			public const string Attri_ParticleName = "ParticleName";
			public const string Attri_CastKeyFrame = "CastKeyFrame";
			public const string Attri_PlacementType = "PlacementType";
			public const string Attri_FacingType = "FacingType";
			public const string Attri_BoneName = "BoneName";
			public const string Attri_AttachmentZOffset = "AttachmentZOffset";
			public const string Attri_UseLocalRelativePosition = "UseLocalRelativePosition";
			public const string Attri_PlacementRelativePosition = "PlacementRelativePosition";
			public const string Attri_PlacementWroldRelativePosition = "PlacementWorldRelativePosition";
			public const string Attri_InheritHostModelScalingWhenAttached = "InheritHostModelScalingWhenAttached";
			public const string Attri_MaintainPosition = "MaintainPosition";
			public const string Attri_MaintainFacing = "MaintainFacing";
			public const string Attri_RemoveOnUnApply = "RemoveOnUnApply";
			public const string Attri_PlacementRelativeEuler = "PlacementRelativeEuler";

			//Projectile
			public const string Attri_IsProjectile = "IsProjectileEffect";
			public const string Attri_ProjectileSpeed = "ProjectileSpeed";
			public const string Attri_ProjectileStartBoneName = "ProjectileParticleStartBone";
			public const string Attri_ProjectileTargetBoneName = "ProjectileParticleTargetBone";
			public const string Attri_ProjectileParticleName = "ProjectileParticleName";
		}

		public string particleName = string.Empty;
		public string castKeyFrame = string.Empty;
		public int facingType = CreateObjectFacingType.ToTarget;
		public int placementType = CreateObjectPlacementType.AtSource;
		public float attachmentZOffset = 0;
		public bool inheritHostModelScalingWhenAttached = false;
		public Vector3 placementWorldRelativePosition = default(Vector3);
		public Vector3 placementLocalRelativePosition = default(Vector3);
		public Vector3 placementRelativeEuler = default(Vector3);
		public string placementBoneName = string.Empty;
		public bool maintainPosition = false;
		public bool maintainFacing = false;
		public bool removeOnUnApply = false;
		public bool useLocalRelativePosition = true;

		//Projectile
		public bool isProjectileEffect = false;
		public float projectileSpeed = 1f;
		public string projectileStartBoneName = string.Empty;
		public string projectileTargetBoneName = string.Empty;
		public string projectileParticleName = string.Empty;

		public static ParticleSystemSettings Load(string xml)
		{
			var xmlParser = new TiXml.TiXmlDocument();
			AssertHelper.AssetFalse(xmlParser.Parse(xml));
			return Load(xmlParser.RootElement());
		}

		public static ParticleSystemSettings Load(TiXml.TiXmlElement xmlNode)
		{
			ParticleSystemSettings config = new ParticleSystemSettings();
			if (xmlNode != null)
			{
				config.particleName = StrParser.ParseStr(xmlNode.Attribute(M.Attri_ParticleName));
				config.castKeyFrame = StrParser.ParseStr(xmlNode.Attribute(M.Attri_CastKeyFrame));
				config.placementType = StrParser.ParseDecInt(xmlNode.Attribute(M.Attri_PlacementType), CreateObjectPlacementType.AtSource);
				config.facingType = StrParser.ParseDecInt(xmlNode.Attribute(M.Attri_FacingType), CreateObjectFacingType.ToTarget);
				config.placementBoneName = StrParser.ParseStr(xmlNode.Attribute(M.Attri_BoneName));
				config.projectileTargetBoneName = StrParser.ParseStr(xmlNode.Attribute(M.Attri_ProjectileTargetBoneName));
				config.attachmentZOffset = StrParser.ParseFloat(xmlNode.Attribute(M.Attri_AttachmentZOffset), 0);

				if (xmlNode.Attribute(M.Attri_PlacementRelativePosition) == null)
					config.placementLocalRelativePosition = Vector3.zero;
				else
					config.placementLocalRelativePosition = Vector3.Parse(xmlNode.Attribute(M.Attri_PlacementRelativePosition));

				if (xmlNode.Attribute(M.Attri_PlacementWroldRelativePosition) == null)
					config.placementWorldRelativePosition = Vector3.zero;
				else
					config.placementWorldRelativePosition = Vector3.Parse(xmlNode.Attribute(M.Attri_PlacementWroldRelativePosition));

				if (xmlNode.Attribute(M.Attri_PlacementRelativeEuler) == null)
					config.placementRelativeEuler = Vector3.zero;
				else
					config.placementRelativeEuler = Vector3.Parse(xmlNode.Attribute(M.Attri_PlacementRelativeEuler));

				config.inheritHostModelScalingWhenAttached = StrParser.ParseBool(xmlNode.Attribute(M.Attri_InheritHostModelScalingWhenAttached), true);
				config.maintainPosition = StrParser.ParseBool(xmlNode.Attribute(M.Attri_MaintainPosition), false);
				config.maintainFacing = StrParser.ParseBool(xmlNode.Attribute(M.Attri_MaintainFacing), false);
				config.removeOnUnApply = StrParser.ParseBool(xmlNode.Attribute(M.Attri_RemoveOnUnApply), false);
				config.useLocalRelativePosition = StrParser.ParseBool(xmlNode.Attribute(M.Attri_UseLocalRelativePosition), true);

				config.isProjectileEffect = StrParser.ParseBool(xmlNode.Attribute(M.Attri_IsProjectile));
				config.projectileSpeed = StrParser.ParseFloat(xmlNode.Attribute(M.Attri_ProjectileSpeed));
				config.projectileStartBoneName = StrParser.ParseStr(xmlNode.Attribute(M.Attri_ProjectileStartBoneName));
				config.projectileParticleName = StrParser.ParseStr(xmlNode.Attribute(M.Attri_ProjectileParticleName));
			}

			return config;
		}

#if UNITY_EDITOR
		public static System.Security.SecurityElement GetXmlNode(ParticleSystemSettings settings, string customNodeName = "")
		{
			Mono.Xml.SecurityParser parser = new Mono.Xml.SecurityParser();
			Mono.Xml.SmallXmlParser.AttrListImpl attrList = new Mono.Xml.SmallXmlParser.AttrListImpl();

			if (!string.IsNullOrEmpty(settings.particleName))
				attrList.Add(M.Attri_ParticleName, settings.particleName);

			if (!string.IsNullOrEmpty(settings.castKeyFrame))
				attrList.Add(M.Attri_CastKeyFrame, settings.castKeyFrame);

			if (settings.placementType != CreateObjectPlacementType.AtSource)
				attrList.Add(M.Attri_PlacementType, settings.placementType.ToString());

			if (settings.facingType != CreateObjectFacingType.ToTarget)
				attrList.Add(M.Attri_FacingType, settings.facingType.ToString());

			if (!string.IsNullOrEmpty(settings.placementBoneName))
				attrList.Add(M.Attri_BoneName, settings.placementBoneName);
			if (settings.attachmentZOffset != 0)
				attrList.Add(M.Attri_AttachmentZOffset, settings.attachmentZOffset.ToString());

			if (settings.placementLocalRelativePosition.IsEqual(Vector3.zero) == false)
				attrList.Add(M.Attri_PlacementRelativePosition, settings.placementLocalRelativePosition.ToString());

			if (settings.placementWorldRelativePosition.IsEqual(Vector3.zero) == false)
				attrList.Add(M.Attri_PlacementWroldRelativePosition, settings.placementWorldRelativePosition.ToString());

			if (settings.placementRelativeEuler.IsEqual(Vector3.zero) == false)
				attrList.Add(M.Attri_PlacementRelativeEuler, settings.placementRelativeEuler.ToString());

			if (settings.inheritHostModelScalingWhenAttached == false)
				attrList.Add(M.Attri_InheritHostModelScalingWhenAttached, settings.inheritHostModelScalingWhenAttached.ToString());

			if (settings.maintainPosition)
				attrList.Add(M.Attri_MaintainPosition, settings.maintainPosition.ToString());

			if (settings.maintainFacing)
				attrList.Add(M.Attri_MaintainFacing, settings.maintainFacing.ToString());

			if (settings.removeOnUnApply)
				attrList.Add(M.Attri_RemoveOnUnApply, settings.removeOnUnApply.ToString());

			if (settings.useLocalRelativePosition == false)
				attrList.Add(M.Attri_UseLocalRelativePosition, settings.useLocalRelativePosition.ToString());

			if (settings.isProjectileEffect)
				attrList.Add(M.Attri_IsProjectile, settings.isProjectileEffect.ToString());

			if (settings.projectileSpeed != 1)
				attrList.Add(M.Attri_ProjectileSpeed, settings.projectileSpeed.ToString());

			if (!string.IsNullOrEmpty(settings.projectileStartBoneName))
				attrList.Add(M.Attri_ProjectileStartBoneName, settings.projectileStartBoneName);

			if (!string.IsNullOrEmpty(settings.projectileTargetBoneName))
				attrList.Add(M.Attri_ProjectileTargetBoneName, settings.projectileTargetBoneName);

			if (!string.IsNullOrEmpty(settings.projectileParticleName))
				attrList.Add(M.Attri_ProjectileParticleName, settings.projectileParticleName);

			if (string.IsNullOrEmpty(customNodeName))
				parser.OnStartElement(M.Node, attrList);
			else
				parser.OnStartElement(customNodeName, attrList);


			parser.OnEndElement("");

			return parser.ToXml();
		}

		public static string GetXml(ParticleSystemSettings settings, string customNodeName = "")
		{
			return GetXmlNode(settings, customNodeName).ToString();
		}
#endif
	}
}
