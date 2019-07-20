using System;
using System.Collections.Generic;

namespace ClientCommon
{
	[DbTable("mesh_assembly", "MeshAssembly", "", "")]
	sealed public class MeshAssembly : AutoCreateConfigElem
	{
#if UNITY_EDITOR
		private string _comment = "";
		[DbColumn(false, "comment")]
		public string Comment { get { return _comment; } set { _comment = value; } }

		private bool _abandoned = false;
		[DbColumn(false, "abandoned")]
		public bool Abandoned { get { return _abandoned; } set { _abandoned = value; } }

		private int _version = 0;
		[DbColumn(false, "version")]
		public int Version { get { return _version; } set { _version = value; } }

#endif
		private int _id = 0;
		[DbColumn(true, "id")]
		public int Id { get { return _id; } set { _id = value; } }

		private string _asset_name = "";
		[DbColumn(false, "asset_name")]
		public string AssetName { get { return _asset_name; } set { _asset_name = value; } }

		private int _body_type = 0;
		[DbColumn(false, "body_type")]
		public int BodyType { get { return _body_type; } set { _body_type = value; } }

		private int _body_part_type = 0;
		[DbColumn(false, "body_part_type")]
		public int BodyPartType { get { return _body_part_type; } set { _body_part_type = value; } }

		private int _assembly_type = 0;
		[DbColumn(false, "assembly_type")]
		public int AssemblyType { get { return _assembly_type; } set { _assembly_type = value; } }

		private string _mounting_marker = "";
		[DbColumn(false, "mounting_marker")]
		public string MountingMarker { get { return _mounting_marker; } set { _mounting_marker = value; } }

		private string _root_marker = "";
		[DbColumn(false, "root_marker")]
		public string RootMarker { get { return _root_marker; } set { _root_marker = value; } }

		private double _mount_off_x = 0;
		[DbColumn(false, "mount_off_x")]
		public double MountOffX { get { return _mount_off_x; } set { _mount_off_x = value; } }

		private double _mount_off_y = 0;
		[DbColumn(false, "mount_off_y")]
		public double MountOffY { get { return _mount_off_y; } set { _mount_off_y = value; } }

		private double _mount_off_z = 0;
		[DbColumn(false, "mount_off_z")]
		public double MountOffZ { get { return _mount_off_z; } set { _mount_off_z = value; } }

		private string _icon = "";
		[DbColumn(false, "icon")]
		public string Icon { get { return _icon; } set { _icon = value; } }

	}

	public class MeshAssemblyConfig : Configuration
	{
		private List<MeshAssembly> _mesh_assemblys = null;
		private Dictionary<int, MeshAssembly> _mesh_assemblyMap = new Dictionary<int, MeshAssembly>();
		private Dictionary<int, long> _refMap = new Dictionary<int, long>();
		private long listRefTime = long.MaxValue;
		private long lastCheckReleaseTime = long.MaxValue;

		public override void LoadAllData()
		{
			_mesh_assemblys = DbClassLoader.Instance.QueryAllData<MeshAssembly>(ConfigDataBase.Instance.DbAccessorFactory);
			foreach (var _mesh_assembly in _mesh_assemblys)
			{
				if (_mesh_assemblyMap.ContainsKey(_mesh_assembly.Id) == false)
					_mesh_assemblyMap.Add(_mesh_assembly.Id, _mesh_assembly);
				else
					_mesh_assemblyMap[_mesh_assembly.Id] = _mesh_assembly;

				if (_refMap.ContainsKey(_mesh_assembly.Id) == false)
					_refMap.Add(_mesh_assembly.Id, DateTime.Now.Ticks);
				else
					_refMap[_mesh_assembly.Id] = DateTime.Now.Ticks;
			}
		}

		public List<MeshAssembly> MeshAssemblys
		{
			get
			{
				if (_mesh_assemblys == null)
					LoadAllData();

				listRefTime = DateTime.Now.Ticks;
				return _mesh_assemblys;
			}
		}

		public MeshAssembly Get(int id)
		{
			if(id <= 0)
				return null;
			MeshAssembly mesh_assembly = null;
			if (_mesh_assemblyMap.TryGetValue(id, out mesh_assembly))
			{
				_refMap[mesh_assembly.Id] = GetCurrentTimeTick();
				ReleaseData(false);
				return mesh_assembly;
			}

			mesh_assembly = DbClassLoader.Instance.QueryData<MeshAssembly>(ConfigDataBase.Instance.DbAccessorFactory, id);
			if (mesh_assembly == null)
			{
#if UNITY_EDITOR
				LoggerManager.Instance.Warn("Invalid `id` value in table `mesh_assembly` : " + id);
#endif
				return null;
			}

			_mesh_assemblyMap.Add(id, mesh_assembly);
			if (_refMap.ContainsKey(mesh_assembly.Id) == false)
				_refMap.Add(mesh_assembly.Id, GetCurrentTimeTick());

			ReleaseData(false);
			return mesh_assembly;
		}

		public override void ReleaseData(bool isForce)
		{
			long nowtime = GetCurrentTimeTick();
			if (!isForce && nowtime - lastCheckReleaseTime < CheckReleaseTime)
				return;
			lastCheckReleaseTime = nowtime;


			var keys = new List<int>(_refMap.Keys);
			for (int index = 0; index < keys.Count; index++)
			{
				var key = keys[index];
				if (isForce || nowtime - _refMap[key] > MaxStayTime)
				{
					_mesh_assemblyMap.Remove(key);
					_refMap[key] = long.MaxValue;
				}
			}

			if (isForce || nowtime - listRefTime > MaxStayTime || _mesh_assemblyMap.Count <= 0)
				_mesh_assemblys = null;
		}

#if UNITY_EDITOR
		public void MemoryUpdate(int key, MeshAssembly mesh_assembly)
		{
			MeshAssemblys.RemoveAll(n => n.Id == key);
			if (_mesh_assemblyMap.ContainsKey(key))
			{
				_mesh_assemblyMap.Remove(key);
				if (_refMap.ContainsKey(key))
					_refMap.Remove(key);
			}

			if (mesh_assembly != null)
			{
				MeshAssemblys.Add(mesh_assembly);
				_mesh_assemblyMap.Add(key, mesh_assembly);
				_refMap.Add(key, DateTime.Now.Ticks);
			}
		}
#endif

	}
}
