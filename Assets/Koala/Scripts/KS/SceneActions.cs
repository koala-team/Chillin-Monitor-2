using System;
using System.Linq;
using System.Collections.Generic;

namespace KS.SceneActions
{
	public partial class BaseAction : KSObject
	{
		public float? Cycle { get; set; }
		public int? Ref { get; set; }
		public string ChildRef { get; set; }
		public float? DurationCycles { get; set; }
		

		public BaseAction()
		{
		}
		
		public new const string NameStatic = "BaseAction";
		
		public override string Name() => "BaseAction";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize Cycle
			s.Add((byte)((Cycle == null) ? 0 : 1));
			if (Cycle != null)
			{
				s.AddRange(BitConverter.GetBytes((float)Cycle));
			}
			
			// serialize Ref
			s.Add((byte)((Ref == null) ? 0 : 1));
			if (Ref != null)
			{
				s.AddRange(BitConverter.GetBytes((int)Ref));
			}
			
			// serialize ChildRef
			s.Add((byte)((ChildRef == null) ? 0 : 1));
			if (ChildRef != null)
			{
				List<byte> tmp0 = new List<byte>();
				tmp0.AddRange(BitConverter.GetBytes((uint)ChildRef.Count()));
				while (tmp0.Count > 0 && tmp0.Last() == 0)
					tmp0.RemoveAt(tmp0.Count - 1);
				s.Add((byte)tmp0.Count);
				s.AddRange(tmp0);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(ChildRef));
			}
			
			// serialize DurationCycles
			s.Add((byte)((DurationCycles == null) ? 0 : 1));
			if (DurationCycles != null)
			{
				s.AddRange(BitConverter.GetBytes((float)DurationCycles));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize Cycle
			byte tmp1;
			tmp1 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp1 == 1)
			{
				Cycle = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Cycle = null;
			
			// deserialize Ref
			byte tmp2;
			tmp2 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp2 == 1)
			{
				Ref = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				Ref = null;
			
			// deserialize ChildRef
			byte tmp3;
			tmp3 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp3 == 1)
			{
				byte tmp4;
				tmp4 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp5 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp5, 0, tmp4);
				offset += tmp4;
				uint tmp6;
				tmp6 = BitConverter.ToUInt32(tmp5, (int)0);
				
				ChildRef = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp6).ToArray());
				offset += tmp6;
			}
			else
				ChildRef = null;
			
			// deserialize DurationCycles
			byte tmp7;
			tmp7 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp7 == 1)
			{
				DurationCycles = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				DurationCycles = null;
			
			return offset;
		}
	}
	
	public partial class Vector2 : KSObject
	{
		public float? X { get; set; }
		public float? Y { get; set; }
		

		public Vector2()
		{
		}
		
		public new const string NameStatic = "Vector2";
		
		public override string Name() => "Vector2";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize X
			s.Add((byte)((X == null) ? 0 : 1));
			if (X != null)
			{
				s.AddRange(BitConverter.GetBytes((float)X));
			}
			
			// serialize Y
			s.Add((byte)((Y == null) ? 0 : 1));
			if (Y != null)
			{
				s.AddRange(BitConverter.GetBytes((float)Y));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize X
			byte tmp8;
			tmp8 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp8 == 1)
			{
				X = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				X = null;
			
			// deserialize Y
			byte tmp9;
			tmp9 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp9 == 1)
			{
				Y = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Y = null;
			
			return offset;
		}
	}
	
	public partial class Vector3 : KSObject
	{
		public float? X { get; set; }
		public float? Y { get; set; }
		public float? Z { get; set; }
		

		public Vector3()
		{
		}
		
		public new const string NameStatic = "Vector3";
		
		public override string Name() => "Vector3";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize X
			s.Add((byte)((X == null) ? 0 : 1));
			if (X != null)
			{
				s.AddRange(BitConverter.GetBytes((float)X));
			}
			
			// serialize Y
			s.Add((byte)((Y == null) ? 0 : 1));
			if (Y != null)
			{
				s.AddRange(BitConverter.GetBytes((float)Y));
			}
			
			// serialize Z
			s.Add((byte)((Z == null) ? 0 : 1));
			if (Z != null)
			{
				s.AddRange(BitConverter.GetBytes((float)Z));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize X
			byte tmp10;
			tmp10 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp10 == 1)
			{
				X = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				X = null;
			
			// deserialize Y
			byte tmp11;
			tmp11 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp11 == 1)
			{
				Y = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Y = null;
			
			// deserialize Z
			byte tmp12;
			tmp12 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp12 == 1)
			{
				Z = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Z = null;
			
			return offset;
		}
	}
	
	public partial class Vector4 : KSObject
	{
		public float? X { get; set; }
		public float? Y { get; set; }
		public float? Z { get; set; }
		public float? W { get; set; }
		

		public Vector4()
		{
		}
		
		public new const string NameStatic = "Vector4";
		
		public override string Name() => "Vector4";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize X
			s.Add((byte)((X == null) ? 0 : 1));
			if (X != null)
			{
				s.AddRange(BitConverter.GetBytes((float)X));
			}
			
			// serialize Y
			s.Add((byte)((Y == null) ? 0 : 1));
			if (Y != null)
			{
				s.AddRange(BitConverter.GetBytes((float)Y));
			}
			
			// serialize Z
			s.Add((byte)((Z == null) ? 0 : 1));
			if (Z != null)
			{
				s.AddRange(BitConverter.GetBytes((float)Z));
			}
			
			// serialize W
			s.Add((byte)((W == null) ? 0 : 1));
			if (W != null)
			{
				s.AddRange(BitConverter.GetBytes((float)W));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize X
			byte tmp13;
			tmp13 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp13 == 1)
			{
				X = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				X = null;
			
			// deserialize Y
			byte tmp14;
			tmp14 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp14 == 1)
			{
				Y = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Y = null;
			
			// deserialize Z
			byte tmp15;
			tmp15 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp15 == 1)
			{
				Z = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Z = null;
			
			// deserialize W
			byte tmp16;
			tmp16 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp16 == 1)
			{
				W = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				W = null;
			
			return offset;
		}
	}
	
	public partial class Asset : KSObject
	{
		public string BundleName { get; set; }
		public string AssetName { get; set; }
		public int? Index { get; set; }
		

		public Asset()
		{
		}
		
		public new const string NameStatic = "Asset";
		
		public override string Name() => "Asset";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize BundleName
			s.Add((byte)((BundleName == null) ? 0 : 1));
			if (BundleName != null)
			{
				List<byte> tmp17 = new List<byte>();
				tmp17.AddRange(BitConverter.GetBytes((uint)BundleName.Count()));
				while (tmp17.Count > 0 && tmp17.Last() == 0)
					tmp17.RemoveAt(tmp17.Count - 1);
				s.Add((byte)tmp17.Count);
				s.AddRange(tmp17);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(BundleName));
			}
			
			// serialize AssetName
			s.Add((byte)((AssetName == null) ? 0 : 1));
			if (AssetName != null)
			{
				List<byte> tmp18 = new List<byte>();
				tmp18.AddRange(BitConverter.GetBytes((uint)AssetName.Count()));
				while (tmp18.Count > 0 && tmp18.Last() == 0)
					tmp18.RemoveAt(tmp18.Count - 1);
				s.Add((byte)tmp18.Count);
				s.AddRange(tmp18);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(AssetName));
			}
			
			// serialize Index
			s.Add((byte)((Index == null) ? 0 : 1));
			if (Index != null)
			{
				s.AddRange(BitConverter.GetBytes((int)Index));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize BundleName
			byte tmp19;
			tmp19 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp19 == 1)
			{
				byte tmp20;
				tmp20 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp21 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp21, 0, tmp20);
				offset += tmp20;
				uint tmp22;
				tmp22 = BitConverter.ToUInt32(tmp21, (int)0);
				
				BundleName = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp22).ToArray());
				offset += tmp22;
			}
			else
				BundleName = null;
			
			// deserialize AssetName
			byte tmp23;
			tmp23 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp23 == 1)
			{
				byte tmp24;
				tmp24 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp25 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp25, 0, tmp24);
				offset += tmp24;
				uint tmp26;
				tmp26 = BitConverter.ToUInt32(tmp25, (int)0);
				
				AssetName = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp26).ToArray());
				offset += tmp26;
			}
			else
				AssetName = null;
			
			// deserialize Index
			byte tmp27;
			tmp27 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp27 == 1)
			{
				Index = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				Index = null;
			
			return offset;
		}
	}
	
	public partial class BaseCreation : BaseAction
	{
		public int? ParentRef { get; set; }
		public string ParentChildRef { get; set; }
		

		public BaseCreation()
		{
		}
		
		public new const string NameStatic = "BaseCreation";
		
		public override string Name() => "BaseCreation";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize ParentRef
			s.Add((byte)((ParentRef == null) ? 0 : 1));
			if (ParentRef != null)
			{
				s.AddRange(BitConverter.GetBytes((int)ParentRef));
			}
			
			// serialize ParentChildRef
			s.Add((byte)((ParentChildRef == null) ? 0 : 1));
			if (ParentChildRef != null)
			{
				List<byte> tmp28 = new List<byte>();
				tmp28.AddRange(BitConverter.GetBytes((uint)ParentChildRef.Count()));
				while (tmp28.Count > 0 && tmp28.Last() == 0)
					tmp28.RemoveAt(tmp28.Count - 1);
				s.Add((byte)tmp28.Count);
				s.AddRange(tmp28);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(ParentChildRef));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize ParentRef
			byte tmp29;
			tmp29 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp29 == 1)
			{
				ParentRef = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				ParentRef = null;
			
			// deserialize ParentChildRef
			byte tmp30;
			tmp30 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp30 == 1)
			{
				byte tmp31;
				tmp31 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp32 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp32, 0, tmp31);
				offset += tmp31;
				uint tmp33;
				tmp33 = BitConverter.ToUInt32(tmp32, (int)0);
				
				ParentChildRef = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp33).ToArray());
				offset += tmp33;
			}
			else
				ParentChildRef = null;
			
			return offset;
		}
	}
	
	public partial class CreateEmptyGameObject : BaseCreation
	{
		

		public CreateEmptyGameObject()
		{
		}
		
		public new const string NameStatic = "CreateEmptyGameObject";
		
		public override string Name() => "CreateEmptyGameObject";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			return offset;
		}
	}
	
	public enum EDefaultParent
	{
		RootObject = 0,
		RootCanvas = 1,
	}
	
	public partial class InstantiateBundleAsset : BaseCreation
	{
		public Asset Asset { get; set; }
		public EDefaultParent? DefaultParent { get; set; }
		

		public InstantiateBundleAsset()
		{
		}
		
		public new const string NameStatic = "InstantiateBundleAsset";
		
		public override string Name() => "InstantiateBundleAsset";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize Asset
			s.Add((byte)((Asset == null) ? 0 : 1));
			if (Asset != null)
			{
				s.AddRange(Asset.Serialize());
			}
			
			// serialize DefaultParent
			s.Add((byte)((DefaultParent == null) ? 0 : 1));
			if (DefaultParent != null)
			{
				s.Add((byte)((sbyte)DefaultParent));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize Asset
			byte tmp34;
			tmp34 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp34 == 1)
			{
				Asset = new Asset();
				offset = Asset.Deserialize(s, offset);
			}
			else
				Asset = null;
			
			// deserialize DefaultParent
			byte tmp35;
			tmp35 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp35 == 1)
			{
				sbyte tmp36;
				tmp36 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				DefaultParent = (EDefaultParent)tmp36;
			}
			else
				DefaultParent = null;
			
			return offset;
		}
	}
	
	public enum EBasicObjectType
	{
		Sprite = 0,
		AudioSource = 1,
		Ellipse2D = 2,
		Polygon2D = 3,
		Line = 4,
		Light = 5,
	}
	
	public partial class CreateBasicObject : BaseCreation
	{
		public EBasicObjectType? Type { get; set; }
		

		public CreateBasicObject()
		{
		}
		
		public new const string NameStatic = "CreateBasicObject";
		
		public override string Name() => "CreateBasicObject";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize Type
			s.Add((byte)((Type == null) ? 0 : 1));
			if (Type != null)
			{
				s.Add((byte)((sbyte)Type));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize Type
			byte tmp37;
			tmp37 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp37 == 1)
			{
				sbyte tmp38;
				tmp38 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				Type = (EBasicObjectType)tmp38;
			}
			else
				Type = null;
			
			return offset;
		}
	}
	
	public enum EUIElementType
	{
		Canvas = 0,
		Text = 1,
		Slider = 2,
		Image = 3,
		RawImage = 4,
		Panel = 5,
	}
	
	public partial class CreateUIElement : BaseCreation
	{
		public EUIElementType? Type { get; set; }
		

		public CreateUIElement()
		{
		}
		
		public new const string NameStatic = "CreateUIElement";
		
		public override string Name() => "CreateUIElement";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize Type
			s.Add((byte)((Type == null) ? 0 : 1));
			if (Type != null)
			{
				s.Add((byte)((sbyte)Type));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize Type
			byte tmp39;
			tmp39 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp39 == 1)
			{
				sbyte tmp40;
				tmp40 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				Type = (EUIElementType)tmp40;
			}
			else
				Type = null;
			
			return offset;
		}
	}
	
	public partial class Destroy : BaseAction
	{
		

		public Destroy()
		{
		}
		
		public new const string NameStatic = "Destroy";
		
		public override string Name() => "Destroy";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			return offset;
		}
	}
	
	public partial class ChangeIsActive : BaseAction
	{
		public bool? IsActive { get; set; }
		

		public ChangeIsActive()
		{
		}
		
		public new const string NameStatic = "ChangeIsActive";
		
		public override string Name() => "ChangeIsActive";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize IsActive
			s.Add((byte)((IsActive == null) ? 0 : 1));
			if (IsActive != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)IsActive));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize IsActive
			byte tmp41;
			tmp41 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp41 == 1)
			{
				IsActive = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				IsActive = null;
			
			return offset;
		}
	}
	
	public partial class ChangeVisibility : BaseAction
	{
		public bool? IsVisible { get; set; }
		

		public ChangeVisibility()
		{
		}
		
		public new const string NameStatic = "ChangeVisibility";
		
		public override string Name() => "ChangeVisibility";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize IsVisible
			s.Add((byte)((IsVisible == null) ? 0 : 1));
			if (IsVisible != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)IsVisible));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize IsVisible
			byte tmp42;
			tmp42 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp42 == 1)
			{
				IsVisible = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				IsVisible = null;
			
			return offset;
		}
	}
	
	public partial class ChangeTransform : BaseAction
	{
		public Vector3 Position { get; set; }
		public Vector3 Rotation { get; set; }
		public Vector3 Scale { get; set; }
		public bool? ChangeLocal { get; set; }
		

		public ChangeTransform()
		{
		}
		
		public new const string NameStatic = "ChangeTransform";
		
		public override string Name() => "ChangeTransform";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize Position
			s.Add((byte)((Position == null) ? 0 : 1));
			if (Position != null)
			{
				s.AddRange(Position.Serialize());
			}
			
			// serialize Rotation
			s.Add((byte)((Rotation == null) ? 0 : 1));
			if (Rotation != null)
			{
				s.AddRange(Rotation.Serialize());
			}
			
			// serialize Scale
			s.Add((byte)((Scale == null) ? 0 : 1));
			if (Scale != null)
			{
				s.AddRange(Scale.Serialize());
			}
			
			// serialize ChangeLocal
			s.Add((byte)((ChangeLocal == null) ? 0 : 1));
			if (ChangeLocal != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)ChangeLocal));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize Position
			byte tmp43;
			tmp43 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp43 == 1)
			{
				Position = new Vector3();
				offset = Position.Deserialize(s, offset);
			}
			else
				Position = null;
			
			// deserialize Rotation
			byte tmp44;
			tmp44 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp44 == 1)
			{
				Rotation = new Vector3();
				offset = Rotation.Deserialize(s, offset);
			}
			else
				Rotation = null;
			
			// deserialize Scale
			byte tmp45;
			tmp45 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp45 == 1)
			{
				Scale = new Vector3();
				offset = Scale.Deserialize(s, offset);
			}
			else
				Scale = null;
			
			// deserialize ChangeLocal
			byte tmp46;
			tmp46 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp46 == 1)
			{
				ChangeLocal = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				ChangeLocal = null;
			
			return offset;
		}
	}
	
	public enum EAnimatorVariableType
	{
		Int = 0,
		Float = 1,
		Bool = 2,
		Trigger = 3,
	}
	
	public partial class ChangeAnimatorVariable : BaseAction
	{
		public string VarName { get; set; }
		public EAnimatorVariableType? VarType { get; set; }
		public int? IntValue { get; set; }
		public float? FloatValue { get; set; }
		public bool? BoolValue { get; set; }
		

		public ChangeAnimatorVariable()
		{
		}
		
		public new const string NameStatic = "ChangeAnimatorVariable";
		
		public override string Name() => "ChangeAnimatorVariable";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize VarName
			s.Add((byte)((VarName == null) ? 0 : 1));
			if (VarName != null)
			{
				List<byte> tmp47 = new List<byte>();
				tmp47.AddRange(BitConverter.GetBytes((uint)VarName.Count()));
				while (tmp47.Count > 0 && tmp47.Last() == 0)
					tmp47.RemoveAt(tmp47.Count - 1);
				s.Add((byte)tmp47.Count);
				s.AddRange(tmp47);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(VarName));
			}
			
			// serialize VarType
			s.Add((byte)((VarType == null) ? 0 : 1));
			if (VarType != null)
			{
				s.Add((byte)((sbyte)VarType));
			}
			
			// serialize IntValue
			s.Add((byte)((IntValue == null) ? 0 : 1));
			if (IntValue != null)
			{
				s.AddRange(BitConverter.GetBytes((int)IntValue));
			}
			
			// serialize FloatValue
			s.Add((byte)((FloatValue == null) ? 0 : 1));
			if (FloatValue != null)
			{
				s.AddRange(BitConverter.GetBytes((float)FloatValue));
			}
			
			// serialize BoolValue
			s.Add((byte)((BoolValue == null) ? 0 : 1));
			if (BoolValue != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)BoolValue));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize VarName
			byte tmp48;
			tmp48 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp48 == 1)
			{
				byte tmp49;
				tmp49 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp50 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp50, 0, tmp49);
				offset += tmp49;
				uint tmp51;
				tmp51 = BitConverter.ToUInt32(tmp50, (int)0);
				
				VarName = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp51).ToArray());
				offset += tmp51;
			}
			else
				VarName = null;
			
			// deserialize VarType
			byte tmp52;
			tmp52 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp52 == 1)
			{
				sbyte tmp53;
				tmp53 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				VarType = (EAnimatorVariableType)tmp53;
			}
			else
				VarType = null;
			
			// deserialize IntValue
			byte tmp54;
			tmp54 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp54 == 1)
			{
				IntValue = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				IntValue = null;
			
			// deserialize FloatValue
			byte tmp55;
			tmp55 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp55 == 1)
			{
				FloatValue = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FloatValue = null;
			
			// deserialize BoolValue
			byte tmp56;
			tmp56 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp56 == 1)
			{
				BoolValue = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				BoolValue = null;
			
			return offset;
		}
	}
	
	public partial class ChangeAnimatorState : BaseAction
	{
		public string StateName { get; set; }
		public int? Layer { get; set; }
		public float? NormalizedTime { get; set; }
		

		public ChangeAnimatorState()
		{
		}
		
		public new const string NameStatic = "ChangeAnimatorState";
		
		public override string Name() => "ChangeAnimatorState";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize StateName
			s.Add((byte)((StateName == null) ? 0 : 1));
			if (StateName != null)
			{
				List<byte> tmp57 = new List<byte>();
				tmp57.AddRange(BitConverter.GetBytes((uint)StateName.Count()));
				while (tmp57.Count > 0 && tmp57.Last() == 0)
					tmp57.RemoveAt(tmp57.Count - 1);
				s.Add((byte)tmp57.Count);
				s.AddRange(tmp57);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(StateName));
			}
			
			// serialize Layer
			s.Add((byte)((Layer == null) ? 0 : 1));
			if (Layer != null)
			{
				s.AddRange(BitConverter.GetBytes((int)Layer));
			}
			
			// serialize NormalizedTime
			s.Add((byte)((NormalizedTime == null) ? 0 : 1));
			if (NormalizedTime != null)
			{
				s.AddRange(BitConverter.GetBytes((float)NormalizedTime));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize StateName
			byte tmp58;
			tmp58 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp58 == 1)
			{
				byte tmp59;
				tmp59 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp60 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp60, 0, tmp59);
				offset += tmp59;
				uint tmp61;
				tmp61 = BitConverter.ToUInt32(tmp60, (int)0);
				
				StateName = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp61).ToArray());
				offset += tmp61;
			}
			else
				StateName = null;
			
			// deserialize Layer
			byte tmp62;
			tmp62 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp62 == 1)
			{
				Layer = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				Layer = null;
			
			// deserialize NormalizedTime
			byte tmp63;
			tmp63 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp63 == 1)
			{
				NormalizedTime = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				NormalizedTime = null;
			
			return offset;
		}
	}
	
	public partial class ChangeAudioSource : BaseAction
	{
		public Asset AudioClipAsset { get; set; }
		public float? Time { get; set; }
		public bool? Mute { get; set; }
		public bool? Loop { get; set; }
		public int? Priority { get; set; }
		public float? Volume { get; set; }
		public float? SpatialBlend { get; set; }
		public bool? Play { get; set; }
		public bool? Stop { get; set; }
		

		public ChangeAudioSource()
		{
		}
		
		public new const string NameStatic = "ChangeAudioSource";
		
		public override string Name() => "ChangeAudioSource";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize AudioClipAsset
			s.Add((byte)((AudioClipAsset == null) ? 0 : 1));
			if (AudioClipAsset != null)
			{
				s.AddRange(AudioClipAsset.Serialize());
			}
			
			// serialize Time
			s.Add((byte)((Time == null) ? 0 : 1));
			if (Time != null)
			{
				s.AddRange(BitConverter.GetBytes((float)Time));
			}
			
			// serialize Mute
			s.Add((byte)((Mute == null) ? 0 : 1));
			if (Mute != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)Mute));
			}
			
			// serialize Loop
			s.Add((byte)((Loop == null) ? 0 : 1));
			if (Loop != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)Loop));
			}
			
			// serialize Priority
			s.Add((byte)((Priority == null) ? 0 : 1));
			if (Priority != null)
			{
				s.AddRange(BitConverter.GetBytes((int)Priority));
			}
			
			// serialize Volume
			s.Add((byte)((Volume == null) ? 0 : 1));
			if (Volume != null)
			{
				s.AddRange(BitConverter.GetBytes((float)Volume));
			}
			
			// serialize SpatialBlend
			s.Add((byte)((SpatialBlend == null) ? 0 : 1));
			if (SpatialBlend != null)
			{
				s.AddRange(BitConverter.GetBytes((float)SpatialBlend));
			}
			
			// serialize Play
			s.Add((byte)((Play == null) ? 0 : 1));
			if (Play != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)Play));
			}
			
			// serialize Stop
			s.Add((byte)((Stop == null) ? 0 : 1));
			if (Stop != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)Stop));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize AudioClipAsset
			byte tmp64;
			tmp64 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp64 == 1)
			{
				AudioClipAsset = new Asset();
				offset = AudioClipAsset.Deserialize(s, offset);
			}
			else
				AudioClipAsset = null;
			
			// deserialize Time
			byte tmp65;
			tmp65 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp65 == 1)
			{
				Time = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Time = null;
			
			// deserialize Mute
			byte tmp66;
			tmp66 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp66 == 1)
			{
				Mute = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				Mute = null;
			
			// deserialize Loop
			byte tmp67;
			tmp67 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp67 == 1)
			{
				Loop = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				Loop = null;
			
			// deserialize Priority
			byte tmp68;
			tmp68 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp68 == 1)
			{
				Priority = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				Priority = null;
			
			// deserialize Volume
			byte tmp69;
			tmp69 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp69 == 1)
			{
				Volume = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Volume = null;
			
			// deserialize SpatialBlend
			byte tmp70;
			tmp70 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp70 == 1)
			{
				SpatialBlend = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				SpatialBlend = null;
			
			// deserialize Play
			byte tmp71;
			tmp71 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp71 == 1)
			{
				Play = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				Play = null;
			
			// deserialize Stop
			byte tmp72;
			tmp72 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp72 == 1)
			{
				Stop = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				Stop = null;
			
			return offset;
		}
	}
	
	public partial class ChangeRectTransform : BaseAction
	{
		public Vector3 Position { get; set; }
		public Vector3 Rotation { get; set; }
		public Vector3 Scale { get; set; }
		public Vector2 Pivot { get; set; }
		public Vector2 AnchorMin { get; set; }
		public Vector2 AnchorMax { get; set; }
		public Vector2 Size { get; set; }
		

		public ChangeRectTransform()
		{
		}
		
		public new const string NameStatic = "ChangeRectTransform";
		
		public override string Name() => "ChangeRectTransform";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize Position
			s.Add((byte)((Position == null) ? 0 : 1));
			if (Position != null)
			{
				s.AddRange(Position.Serialize());
			}
			
			// serialize Rotation
			s.Add((byte)((Rotation == null) ? 0 : 1));
			if (Rotation != null)
			{
				s.AddRange(Rotation.Serialize());
			}
			
			// serialize Scale
			s.Add((byte)((Scale == null) ? 0 : 1));
			if (Scale != null)
			{
				s.AddRange(Scale.Serialize());
			}
			
			// serialize Pivot
			s.Add((byte)((Pivot == null) ? 0 : 1));
			if (Pivot != null)
			{
				s.AddRange(Pivot.Serialize());
			}
			
			// serialize AnchorMin
			s.Add((byte)((AnchorMin == null) ? 0 : 1));
			if (AnchorMin != null)
			{
				s.AddRange(AnchorMin.Serialize());
			}
			
			// serialize AnchorMax
			s.Add((byte)((AnchorMax == null) ? 0 : 1));
			if (AnchorMax != null)
			{
				s.AddRange(AnchorMax.Serialize());
			}
			
			// serialize Size
			s.Add((byte)((Size == null) ? 0 : 1));
			if (Size != null)
			{
				s.AddRange(Size.Serialize());
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize Position
			byte tmp73;
			tmp73 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp73 == 1)
			{
				Position = new Vector3();
				offset = Position.Deserialize(s, offset);
			}
			else
				Position = null;
			
			// deserialize Rotation
			byte tmp74;
			tmp74 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp74 == 1)
			{
				Rotation = new Vector3();
				offset = Rotation.Deserialize(s, offset);
			}
			else
				Rotation = null;
			
			// deserialize Scale
			byte tmp75;
			tmp75 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp75 == 1)
			{
				Scale = new Vector3();
				offset = Scale.Deserialize(s, offset);
			}
			else
				Scale = null;
			
			// deserialize Pivot
			byte tmp76;
			tmp76 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp76 == 1)
			{
				Pivot = new Vector2();
				offset = Pivot.Deserialize(s, offset);
			}
			else
				Pivot = null;
			
			// deserialize AnchorMin
			byte tmp77;
			tmp77 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp77 == 1)
			{
				AnchorMin = new Vector2();
				offset = AnchorMin.Deserialize(s, offset);
			}
			else
				AnchorMin = null;
			
			// deserialize AnchorMax
			byte tmp78;
			tmp78 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp78 == 1)
			{
				AnchorMax = new Vector2();
				offset = AnchorMax.Deserialize(s, offset);
			}
			else
				AnchorMax = null;
			
			// deserialize Size
			byte tmp79;
			tmp79 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp79 == 1)
			{
				Size = new Vector2();
				offset = Size.Deserialize(s, offset);
			}
			else
				Size = null;
			
			return offset;
		}
	}
	
	public enum ETextAlignmentOption
	{
		TopLeft = 257,
		Top = 258,
		TopRight = 260,
		TopJustified = 264,
		TopFlush = 272,
		TopGeoAligned = 288,
		Left = 513,
		Center = 514,
		Right = 516,
		Justified = 520,
		Flush = 528,
		CenterGeoAligned = 544,
		BottomLeft = 1025,
		Bottom = 1026,
		BottomRight = 1028,
		BottomJustified = 1032,
		BottomFlush = 1040,
		BottomGeoAligned = 1056,
		BaselineLeft = 2049,
		Baseline = 2050,
		BaselineRight = 2052,
		BaselineJustified = 2056,
		BaselineFlush = 2064,
		BaselineGeoAligned = 2080,
		MidlineLeft = 4097,
		Midline = 4098,
		MidlineRight = 4100,
		MidlineJustified = 4104,
		MidlineFlush = 4112,
		MidlineGeoAligned = 4128,
		CaplineLeft = 8193,
		Capline = 8194,
		CaplineRight = 8196,
		CaplineJustified = 8200,
		CaplineFlush = 8208,
		CaplineGeoAligned = 8224,
	}
	
	public partial class ChangeText : BaseAction
	{
		public Asset FontAsset { get; set; }
		public string FontName { get; set; }
		public string Text { get; set; }
		public float? FontSize { get; set; }
		public ETextAlignmentOption? Alignment { get; set; }
		public float? WordWrappingRatios { get; set; }
		

		public ChangeText()
		{
		}
		
		public new const string NameStatic = "ChangeText";
		
		public override string Name() => "ChangeText";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize FontAsset
			s.Add((byte)((FontAsset == null) ? 0 : 1));
			if (FontAsset != null)
			{
				s.AddRange(FontAsset.Serialize());
			}
			
			// serialize FontName
			s.Add((byte)((FontName == null) ? 0 : 1));
			if (FontName != null)
			{
				List<byte> tmp80 = new List<byte>();
				tmp80.AddRange(BitConverter.GetBytes((uint)FontName.Count()));
				while (tmp80.Count > 0 && tmp80.Last() == 0)
					tmp80.RemoveAt(tmp80.Count - 1);
				s.Add((byte)tmp80.Count);
				s.AddRange(tmp80);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(FontName));
			}
			
			// serialize Text
			s.Add((byte)((Text == null) ? 0 : 1));
			if (Text != null)
			{
				List<byte> tmp81 = new List<byte>();
				tmp81.AddRange(BitConverter.GetBytes((uint)Text.Count()));
				while (tmp81.Count > 0 && tmp81.Last() == 0)
					tmp81.RemoveAt(tmp81.Count - 1);
				s.Add((byte)tmp81.Count);
				s.AddRange(tmp81);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(Text));
			}
			
			// serialize FontSize
			s.Add((byte)((FontSize == null) ? 0 : 1));
			if (FontSize != null)
			{
				s.AddRange(BitConverter.GetBytes((float)FontSize));
			}
			
			// serialize Alignment
			s.Add((byte)((Alignment == null) ? 0 : 1));
			if (Alignment != null)
			{
				s.AddRange(BitConverter.GetBytes((short)((short)Alignment)));
			}
			
			// serialize WordWrappingRatios
			s.Add((byte)((WordWrappingRatios == null) ? 0 : 1));
			if (WordWrappingRatios != null)
			{
				s.AddRange(BitConverter.GetBytes((float)WordWrappingRatios));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize FontAsset
			byte tmp82;
			tmp82 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp82 == 1)
			{
				FontAsset = new Asset();
				offset = FontAsset.Deserialize(s, offset);
			}
			else
				FontAsset = null;
			
			// deserialize FontName
			byte tmp83;
			tmp83 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp83 == 1)
			{
				byte tmp84;
				tmp84 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp85 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp85, 0, tmp84);
				offset += tmp84;
				uint tmp86;
				tmp86 = BitConverter.ToUInt32(tmp85, (int)0);
				
				FontName = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp86).ToArray());
				offset += tmp86;
			}
			else
				FontName = null;
			
			// deserialize Text
			byte tmp87;
			tmp87 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp87 == 1)
			{
				byte tmp88;
				tmp88 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp89 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp89, 0, tmp88);
				offset += tmp88;
				uint tmp90;
				tmp90 = BitConverter.ToUInt32(tmp89, (int)0);
				
				Text = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp90).ToArray());
				offset += tmp90;
			}
			else
				Text = null;
			
			// deserialize FontSize
			byte tmp91;
			tmp91 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp91 == 1)
			{
				FontSize = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FontSize = null;
			
			// deserialize Alignment
			byte tmp92;
			tmp92 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp92 == 1)
			{
				short tmp93;
				tmp93 = BitConverter.ToInt16(s, (int)offset);
				offset += sizeof(short);
				Alignment = (ETextAlignmentOption)tmp93;
			}
			else
				Alignment = null;
			
			// deserialize WordWrappingRatios
			byte tmp94;
			tmp94 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp94 == 1)
			{
				WordWrappingRatios = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				WordWrappingRatios = null;
			
			return offset;
		}
	}
	
	public enum ESliderDirection
	{
		LeftToRight = 0,
		RightToLeft = 1,
		BottomToTop = 2,
		TopToBottom = 3,
	}
	
	public partial class ChangeSlider : BaseAction
	{
		public float? Value { get; set; }
		public float? MaxValue { get; set; }
		public float? MinValue { get; set; }
		public ESliderDirection? Direction { get; set; }
		public Vector4 BackgroundColor { get; set; }
		public Vector4 FillColor { get; set; }
		

		public ChangeSlider()
		{
		}
		
		public new const string NameStatic = "ChangeSlider";
		
		public override string Name() => "ChangeSlider";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize Value
			s.Add((byte)((Value == null) ? 0 : 1));
			if (Value != null)
			{
				s.AddRange(BitConverter.GetBytes((float)Value));
			}
			
			// serialize MaxValue
			s.Add((byte)((MaxValue == null) ? 0 : 1));
			if (MaxValue != null)
			{
				s.AddRange(BitConverter.GetBytes((float)MaxValue));
			}
			
			// serialize MinValue
			s.Add((byte)((MinValue == null) ? 0 : 1));
			if (MinValue != null)
			{
				s.AddRange(BitConverter.GetBytes((float)MinValue));
			}
			
			// serialize Direction
			s.Add((byte)((Direction == null) ? 0 : 1));
			if (Direction != null)
			{
				s.AddRange(BitConverter.GetBytes((short)((short)Direction)));
			}
			
			// serialize BackgroundColor
			s.Add((byte)((BackgroundColor == null) ? 0 : 1));
			if (BackgroundColor != null)
			{
				s.AddRange(BackgroundColor.Serialize());
			}
			
			// serialize FillColor
			s.Add((byte)((FillColor == null) ? 0 : 1));
			if (FillColor != null)
			{
				s.AddRange(FillColor.Serialize());
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize Value
			byte tmp95;
			tmp95 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp95 == 1)
			{
				Value = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Value = null;
			
			// deserialize MaxValue
			byte tmp96;
			tmp96 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp96 == 1)
			{
				MaxValue = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				MaxValue = null;
			
			// deserialize MinValue
			byte tmp97;
			tmp97 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp97 == 1)
			{
				MinValue = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				MinValue = null;
			
			// deserialize Direction
			byte tmp98;
			tmp98 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp98 == 1)
			{
				short tmp99;
				tmp99 = BitConverter.ToInt16(s, (int)offset);
				offset += sizeof(short);
				Direction = (ESliderDirection)tmp99;
			}
			else
				Direction = null;
			
			// deserialize BackgroundColor
			byte tmp100;
			tmp100 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp100 == 1)
			{
				BackgroundColor = new Vector4();
				offset = BackgroundColor.Deserialize(s, offset);
			}
			else
				BackgroundColor = null;
			
			// deserialize FillColor
			byte tmp101;
			tmp101 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp101 == 1)
			{
				FillColor = new Vector4();
				offset = FillColor.Deserialize(s, offset);
			}
			else
				FillColor = null;
			
			return offset;
		}
	}
	
	public partial class ChangeImage : BaseAction
	{
		public Asset SpriteAsset { get; set; }
		public Vector4 Color { get; set; }
		public Asset MaterialAsset { get; set; }
		

		public ChangeImage()
		{
		}
		
		public new const string NameStatic = "ChangeImage";
		
		public override string Name() => "ChangeImage";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize SpriteAsset
			s.Add((byte)((SpriteAsset == null) ? 0 : 1));
			if (SpriteAsset != null)
			{
				s.AddRange(SpriteAsset.Serialize());
			}
			
			// serialize Color
			s.Add((byte)((Color == null) ? 0 : 1));
			if (Color != null)
			{
				s.AddRange(Color.Serialize());
			}
			
			// serialize MaterialAsset
			s.Add((byte)((MaterialAsset == null) ? 0 : 1));
			if (MaterialAsset != null)
			{
				s.AddRange(MaterialAsset.Serialize());
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize SpriteAsset
			byte tmp102;
			tmp102 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp102 == 1)
			{
				SpriteAsset = new Asset();
				offset = SpriteAsset.Deserialize(s, offset);
			}
			else
				SpriteAsset = null;
			
			// deserialize Color
			byte tmp103;
			tmp103 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp103 == 1)
			{
				Color = new Vector4();
				offset = Color.Deserialize(s, offset);
			}
			else
				Color = null;
			
			// deserialize MaterialAsset
			byte tmp104;
			tmp104 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp104 == 1)
			{
				MaterialAsset = new Asset();
				offset = MaterialAsset.Deserialize(s, offset);
			}
			else
				MaterialAsset = null;
			
			return offset;
		}
	}
	
	public partial class ChangeRawImage : BaseAction
	{
		public Asset TextureAsset { get; set; }
		public Asset MaterialAsset { get; set; }
		public Vector4 Color { get; set; }
		public Vector4 UvRect { get; set; }
		

		public ChangeRawImage()
		{
		}
		
		public new const string NameStatic = "ChangeRawImage";
		
		public override string Name() => "ChangeRawImage";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize TextureAsset
			s.Add((byte)((TextureAsset == null) ? 0 : 1));
			if (TextureAsset != null)
			{
				s.AddRange(TextureAsset.Serialize());
			}
			
			// serialize MaterialAsset
			s.Add((byte)((MaterialAsset == null) ? 0 : 1));
			if (MaterialAsset != null)
			{
				s.AddRange(MaterialAsset.Serialize());
			}
			
			// serialize Color
			s.Add((byte)((Color == null) ? 0 : 1));
			if (Color != null)
			{
				s.AddRange(Color.Serialize());
			}
			
			// serialize UvRect
			s.Add((byte)((UvRect == null) ? 0 : 1));
			if (UvRect != null)
			{
				s.AddRange(UvRect.Serialize());
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize TextureAsset
			byte tmp105;
			tmp105 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp105 == 1)
			{
				TextureAsset = new Asset();
				offset = TextureAsset.Deserialize(s, offset);
			}
			else
				TextureAsset = null;
			
			// deserialize MaterialAsset
			byte tmp106;
			tmp106 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp106 == 1)
			{
				MaterialAsset = new Asset();
				offset = MaterialAsset.Deserialize(s, offset);
			}
			else
				MaterialAsset = null;
			
			// deserialize Color
			byte tmp107;
			tmp107 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp107 == 1)
			{
				Color = new Vector4();
				offset = Color.Deserialize(s, offset);
			}
			else
				Color = null;
			
			// deserialize UvRect
			byte tmp108;
			tmp108 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp108 == 1)
			{
				UvRect = new Vector4();
				offset = UvRect.Deserialize(s, offset);
			}
			else
				UvRect = null;
			
			return offset;
		}
	}
	
	public partial class ChangeSiblingOrder : BaseAction
	{
		public int? NewIndex { get; set; }
		public bool? GotoFirst { get; set; }
		public bool? GotoLast { get; set; }
		public int? ChangeIndex { get; set; }
		public string SiblingRefAsBaseIndex { get; set; }
		

		public ChangeSiblingOrder()
		{
		}
		
		public new const string NameStatic = "ChangeSiblingOrder";
		
		public override string Name() => "ChangeSiblingOrder";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize NewIndex
			s.Add((byte)((NewIndex == null) ? 0 : 1));
			if (NewIndex != null)
			{
				s.AddRange(BitConverter.GetBytes((int)NewIndex));
			}
			
			// serialize GotoFirst
			s.Add((byte)((GotoFirst == null) ? 0 : 1));
			if (GotoFirst != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)GotoFirst));
			}
			
			// serialize GotoLast
			s.Add((byte)((GotoLast == null) ? 0 : 1));
			if (GotoLast != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)GotoLast));
			}
			
			// serialize ChangeIndex
			s.Add((byte)((ChangeIndex == null) ? 0 : 1));
			if (ChangeIndex != null)
			{
				s.AddRange(BitConverter.GetBytes((int)ChangeIndex));
			}
			
			// serialize SiblingRefAsBaseIndex
			s.Add((byte)((SiblingRefAsBaseIndex == null) ? 0 : 1));
			if (SiblingRefAsBaseIndex != null)
			{
				List<byte> tmp109 = new List<byte>();
				tmp109.AddRange(BitConverter.GetBytes((uint)SiblingRefAsBaseIndex.Count()));
				while (tmp109.Count > 0 && tmp109.Last() == 0)
					tmp109.RemoveAt(tmp109.Count - 1);
				s.Add((byte)tmp109.Count);
				s.AddRange(tmp109);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(SiblingRefAsBaseIndex));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize NewIndex
			byte tmp110;
			tmp110 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp110 == 1)
			{
				NewIndex = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				NewIndex = null;
			
			// deserialize GotoFirst
			byte tmp111;
			tmp111 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp111 == 1)
			{
				GotoFirst = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				GotoFirst = null;
			
			// deserialize GotoLast
			byte tmp112;
			tmp112 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp112 == 1)
			{
				GotoLast = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				GotoLast = null;
			
			// deserialize ChangeIndex
			byte tmp113;
			tmp113 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp113 == 1)
			{
				ChangeIndex = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				ChangeIndex = null;
			
			// deserialize SiblingRefAsBaseIndex
			byte tmp114;
			tmp114 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp114 == 1)
			{
				byte tmp115;
				tmp115 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp116 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp116, 0, tmp115);
				offset += tmp115;
				uint tmp117;
				tmp117 = BitConverter.ToUInt32(tmp116, (int)0);
				
				SiblingRefAsBaseIndex = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp117).ToArray());
				offset += tmp117;
			}
			else
				SiblingRefAsBaseIndex = null;
			
			return offset;
		}
	}
	
	public partial class ManageComponent : BaseAction
	{
		public string Type { get; set; }
		public bool? Add { get; set; }
		public bool? IsActive { get; set; }
		

		public ManageComponent()
		{
		}
		
		public new const string NameStatic = "ManageComponent";
		
		public override string Name() => "ManageComponent";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize Type
			s.Add((byte)((Type == null) ? 0 : 1));
			if (Type != null)
			{
				List<byte> tmp118 = new List<byte>();
				tmp118.AddRange(BitConverter.GetBytes((uint)Type.Count()));
				while (tmp118.Count > 0 && tmp118.Last() == 0)
					tmp118.RemoveAt(tmp118.Count - 1);
				s.Add((byte)tmp118.Count);
				s.AddRange(tmp118);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(Type));
			}
			
			// serialize Add
			s.Add((byte)((Add == null) ? 0 : 1));
			if (Add != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)Add));
			}
			
			// serialize IsActive
			s.Add((byte)((IsActive == null) ? 0 : 1));
			if (IsActive != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)IsActive));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize Type
			byte tmp119;
			tmp119 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp119 == 1)
			{
				byte tmp120;
				tmp120 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp121 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp121, 0, tmp120);
				offset += tmp120;
				uint tmp122;
				tmp122 = BitConverter.ToUInt32(tmp121, (int)0);
				
				Type = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp122).ToArray());
				offset += tmp122;
			}
			else
				Type = null;
			
			// deserialize Add
			byte tmp123;
			tmp123 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp123 == 1)
			{
				Add = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				Add = null;
			
			// deserialize IsActive
			byte tmp124;
			tmp124 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp124 == 1)
			{
				IsActive = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				IsActive = null;
			
			return offset;
		}
	}
	
	public partial class ChangeSprite : BaseAction
	{
		public Asset SpriteAsset { get; set; }
		public Vector4 Color { get; set; }
		public bool? FlipX { get; set; }
		public bool? FlipY { get; set; }
		public int? Order { get; set; }
		

		public ChangeSprite()
		{
		}
		
		public new const string NameStatic = "ChangeSprite";
		
		public override string Name() => "ChangeSprite";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize SpriteAsset
			s.Add((byte)((SpriteAsset == null) ? 0 : 1));
			if (SpriteAsset != null)
			{
				s.AddRange(SpriteAsset.Serialize());
			}
			
			// serialize Color
			s.Add((byte)((Color == null) ? 0 : 1));
			if (Color != null)
			{
				s.AddRange(Color.Serialize());
			}
			
			// serialize FlipX
			s.Add((byte)((FlipX == null) ? 0 : 1));
			if (FlipX != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)FlipX));
			}
			
			// serialize FlipY
			s.Add((byte)((FlipY == null) ? 0 : 1));
			if (FlipY != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)FlipY));
			}
			
			// serialize Order
			s.Add((byte)((Order == null) ? 0 : 1));
			if (Order != null)
			{
				s.AddRange(BitConverter.GetBytes((int)Order));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize SpriteAsset
			byte tmp125;
			tmp125 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp125 == 1)
			{
				SpriteAsset = new Asset();
				offset = SpriteAsset.Deserialize(s, offset);
			}
			else
				SpriteAsset = null;
			
			// deserialize Color
			byte tmp126;
			tmp126 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp126 == 1)
			{
				Color = new Vector4();
				offset = Color.Deserialize(s, offset);
			}
			else
				Color = null;
			
			// deserialize FlipX
			byte tmp127;
			tmp127 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp127 == 1)
			{
				FlipX = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				FlipX = null;
			
			// deserialize FlipY
			byte tmp128;
			tmp128 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp128 == 1)
			{
				FlipY = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				FlipY = null;
			
			// deserialize Order
			byte tmp129;
			tmp129 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp129 == 1)
			{
				Order = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				Order = null;
			
			return offset;
		}
	}
	
	public partial class ChangeMaterial : BaseAction
	{
		public Asset MaterialAsset { get; set; }
		public int? Index { get; set; }
		

		public ChangeMaterial()
		{
		}
		
		public new const string NameStatic = "ChangeMaterial";
		
		public override string Name() => "ChangeMaterial";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize MaterialAsset
			s.Add((byte)((MaterialAsset == null) ? 0 : 1));
			if (MaterialAsset != null)
			{
				s.AddRange(MaterialAsset.Serialize());
			}
			
			// serialize Index
			s.Add((byte)((Index == null) ? 0 : 1));
			if (Index != null)
			{
				s.AddRange(BitConverter.GetBytes((int)Index));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize MaterialAsset
			byte tmp130;
			tmp130 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp130 == 1)
			{
				MaterialAsset = new Asset();
				offset = MaterialAsset.Deserialize(s, offset);
			}
			else
				MaterialAsset = null;
			
			// deserialize Index
			byte tmp131;
			tmp131 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp131 == 1)
			{
				Index = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				Index = null;
			
			return offset;
		}
	}
	
	public partial class ChangeEllipse2D : BaseAction
	{
		public Vector4 FillColor { get; set; }
		public float? XRadius { get; set; }
		public float? YRadius { get; set; }
		

		public ChangeEllipse2D()
		{
		}
		
		public new const string NameStatic = "ChangeEllipse2D";
		
		public override string Name() => "ChangeEllipse2D";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize FillColor
			s.Add((byte)((FillColor == null) ? 0 : 1));
			if (FillColor != null)
			{
				s.AddRange(FillColor.Serialize());
			}
			
			// serialize XRadius
			s.Add((byte)((XRadius == null) ? 0 : 1));
			if (XRadius != null)
			{
				s.AddRange(BitConverter.GetBytes((float)XRadius));
			}
			
			// serialize YRadius
			s.Add((byte)((YRadius == null) ? 0 : 1));
			if (YRadius != null)
			{
				s.AddRange(BitConverter.GetBytes((float)YRadius));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize FillColor
			byte tmp132;
			tmp132 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp132 == 1)
			{
				FillColor = new Vector4();
				offset = FillColor.Deserialize(s, offset);
			}
			else
				FillColor = null;
			
			// deserialize XRadius
			byte tmp133;
			tmp133 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp133 == 1)
			{
				XRadius = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				XRadius = null;
			
			// deserialize YRadius
			byte tmp134;
			tmp134 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp134 == 1)
			{
				YRadius = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				YRadius = null;
			
			return offset;
		}
	}
	
	public partial class ChangePolygon2D : BaseAction
	{
		public Vector4 FillColor { get; set; }
		public List<Vector2> Vertices { get; set; }
		

		public ChangePolygon2D()
		{
		}
		
		public new const string NameStatic = "ChangePolygon2D";
		
		public override string Name() => "ChangePolygon2D";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize FillColor
			s.Add((byte)((FillColor == null) ? 0 : 1));
			if (FillColor != null)
			{
				s.AddRange(FillColor.Serialize());
			}
			
			// serialize Vertices
			s.Add((byte)((Vertices == null) ? 0 : 1));
			if (Vertices != null)
			{
				List<byte> tmp135 = new List<byte>();
				tmp135.AddRange(BitConverter.GetBytes((uint)Vertices.Count()));
				while (tmp135.Count > 0 && tmp135.Last() == 0)
					tmp135.RemoveAt(tmp135.Count - 1);
				s.Add((byte)tmp135.Count);
				s.AddRange(tmp135);
				
				foreach (var tmp136 in Vertices)
				{
					s.Add((byte)((tmp136 == null) ? 0 : 1));
					if (tmp136 != null)
					{
						s.AddRange(tmp136.Serialize());
					}
				}
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize FillColor
			byte tmp137;
			tmp137 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp137 == 1)
			{
				FillColor = new Vector4();
				offset = FillColor.Deserialize(s, offset);
			}
			else
				FillColor = null;
			
			// deserialize Vertices
			byte tmp138;
			tmp138 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp138 == 1)
			{
				byte tmp139;
				tmp139 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp140 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp140, 0, tmp139);
				offset += tmp139;
				uint tmp141;
				tmp141 = BitConverter.ToUInt32(tmp140, (int)0);
				
				Vertices = new List<Vector2>();
				for (uint tmp142 = 0; tmp142 < tmp141; tmp142++)
				{
					Vector2 tmp143;
					byte tmp144;
					tmp144 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp144 == 1)
					{
						tmp143 = new Vector2();
						offset = tmp143.Deserialize(s, offset);
					}
					else
						tmp143 = null;
					Vertices.Add(tmp143);
				}
			}
			else
				Vertices = null;
			
			return offset;
		}
	}
	
	public partial class ChangeLine : BaseAction
	{
		public Vector4 FillColor { get; set; }
		public List<Vector2> Vertices { get; set; }
		public float? Width { get; set; }
		public int? CornerVertices { get; set; }
		public int? EndCapVertices { get; set; }
		public bool? Loop { get; set; }
		

		public ChangeLine()
		{
		}
		
		public new const string NameStatic = "ChangeLine";
		
		public override string Name() => "ChangeLine";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize FillColor
			s.Add((byte)((FillColor == null) ? 0 : 1));
			if (FillColor != null)
			{
				s.AddRange(FillColor.Serialize());
			}
			
			// serialize Vertices
			s.Add((byte)((Vertices == null) ? 0 : 1));
			if (Vertices != null)
			{
				List<byte> tmp145 = new List<byte>();
				tmp145.AddRange(BitConverter.GetBytes((uint)Vertices.Count()));
				while (tmp145.Count > 0 && tmp145.Last() == 0)
					tmp145.RemoveAt(tmp145.Count - 1);
				s.Add((byte)tmp145.Count);
				s.AddRange(tmp145);
				
				foreach (var tmp146 in Vertices)
				{
					s.Add((byte)((tmp146 == null) ? 0 : 1));
					if (tmp146 != null)
					{
						s.AddRange(tmp146.Serialize());
					}
				}
			}
			
			// serialize Width
			s.Add((byte)((Width == null) ? 0 : 1));
			if (Width != null)
			{
				s.AddRange(BitConverter.GetBytes((float)Width));
			}
			
			// serialize CornerVertices
			s.Add((byte)((CornerVertices == null) ? 0 : 1));
			if (CornerVertices != null)
			{
				s.AddRange(BitConverter.GetBytes((int)CornerVertices));
			}
			
			// serialize EndCapVertices
			s.Add((byte)((EndCapVertices == null) ? 0 : 1));
			if (EndCapVertices != null)
			{
				s.AddRange(BitConverter.GetBytes((int)EndCapVertices));
			}
			
			// serialize Loop
			s.Add((byte)((Loop == null) ? 0 : 1));
			if (Loop != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)Loop));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize FillColor
			byte tmp147;
			tmp147 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp147 == 1)
			{
				FillColor = new Vector4();
				offset = FillColor.Deserialize(s, offset);
			}
			else
				FillColor = null;
			
			// deserialize Vertices
			byte tmp148;
			tmp148 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp148 == 1)
			{
				byte tmp149;
				tmp149 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp150 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp150, 0, tmp149);
				offset += tmp149;
				uint tmp151;
				tmp151 = BitConverter.ToUInt32(tmp150, (int)0);
				
				Vertices = new List<Vector2>();
				for (uint tmp152 = 0; tmp152 < tmp151; tmp152++)
				{
					Vector2 tmp153;
					byte tmp154;
					tmp154 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp154 == 1)
					{
						tmp153 = new Vector2();
						offset = tmp153.Deserialize(s, offset);
					}
					else
						tmp153 = null;
					Vertices.Add(tmp153);
				}
			}
			else
				Vertices = null;
			
			// deserialize Width
			byte tmp155;
			tmp155 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp155 == 1)
			{
				Width = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Width = null;
			
			// deserialize CornerVertices
			byte tmp156;
			tmp156 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp156 == 1)
			{
				CornerVertices = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				CornerVertices = null;
			
			// deserialize EndCapVertices
			byte tmp157;
			tmp157 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp157 == 1)
			{
				EndCapVertices = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				EndCapVertices = null;
			
			// deserialize Loop
			byte tmp158;
			tmp158 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp158 == 1)
			{
				Loop = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				Loop = null;
			
			return offset;
		}
	}
	
	public enum ELightType
	{
		Spot = 0,
		Directional = 1,
		Point = 2,
	}
	
	public enum ELightShadowType
	{
		Disabled = 0,
		Hard = 1,
		Soft = 2,
	}
	
	public partial class ChangeLight : BaseAction
	{
		public ELightType? Type { get; set; }
		public float? Range { get; set; }
		public float? SpotAngle { get; set; }
		public Vector4 Color { get; set; }
		public float? Intensity { get; set; }
		public float? IndirectMultiplier { get; set; }
		public ELightShadowType? ShadowType { get; set; }
		public float? ShadowStrength { get; set; }
		public float? ShadowBias { get; set; }
		public float? ShadowNormalBias { get; set; }
		public float? ShadowNearPlane { get; set; }
		public Asset CookieAsset { get; set; }
		public float? CookieSize { get; set; }
		public Asset FlareAsset { get; set; }
		

		public ChangeLight()
		{
		}
		
		public new const string NameStatic = "ChangeLight";
		
		public override string Name() => "ChangeLight";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize Type
			s.Add((byte)((Type == null) ? 0 : 1));
			if (Type != null)
			{
				s.Add((byte)((sbyte)Type));
			}
			
			// serialize Range
			s.Add((byte)((Range == null) ? 0 : 1));
			if (Range != null)
			{
				s.AddRange(BitConverter.GetBytes((float)Range));
			}
			
			// serialize SpotAngle
			s.Add((byte)((SpotAngle == null) ? 0 : 1));
			if (SpotAngle != null)
			{
				s.AddRange(BitConverter.GetBytes((float)SpotAngle));
			}
			
			// serialize Color
			s.Add((byte)((Color == null) ? 0 : 1));
			if (Color != null)
			{
				s.AddRange(Color.Serialize());
			}
			
			// serialize Intensity
			s.Add((byte)((Intensity == null) ? 0 : 1));
			if (Intensity != null)
			{
				s.AddRange(BitConverter.GetBytes((float)Intensity));
			}
			
			// serialize IndirectMultiplier
			s.Add((byte)((IndirectMultiplier == null) ? 0 : 1));
			if (IndirectMultiplier != null)
			{
				s.AddRange(BitConverter.GetBytes((float)IndirectMultiplier));
			}
			
			// serialize ShadowType
			s.Add((byte)((ShadowType == null) ? 0 : 1));
			if (ShadowType != null)
			{
				s.Add((byte)((sbyte)ShadowType));
			}
			
			// serialize ShadowStrength
			s.Add((byte)((ShadowStrength == null) ? 0 : 1));
			if (ShadowStrength != null)
			{
				s.AddRange(BitConverter.GetBytes((float)ShadowStrength));
			}
			
			// serialize ShadowBias
			s.Add((byte)((ShadowBias == null) ? 0 : 1));
			if (ShadowBias != null)
			{
				s.AddRange(BitConverter.GetBytes((float)ShadowBias));
			}
			
			// serialize ShadowNormalBias
			s.Add((byte)((ShadowNormalBias == null) ? 0 : 1));
			if (ShadowNormalBias != null)
			{
				s.AddRange(BitConverter.GetBytes((float)ShadowNormalBias));
			}
			
			// serialize ShadowNearPlane
			s.Add((byte)((ShadowNearPlane == null) ? 0 : 1));
			if (ShadowNearPlane != null)
			{
				s.AddRange(BitConverter.GetBytes((float)ShadowNearPlane));
			}
			
			// serialize CookieAsset
			s.Add((byte)((CookieAsset == null) ? 0 : 1));
			if (CookieAsset != null)
			{
				s.AddRange(CookieAsset.Serialize());
			}
			
			// serialize CookieSize
			s.Add((byte)((CookieSize == null) ? 0 : 1));
			if (CookieSize != null)
			{
				s.AddRange(BitConverter.GetBytes((float)CookieSize));
			}
			
			// serialize FlareAsset
			s.Add((byte)((FlareAsset == null) ? 0 : 1));
			if (FlareAsset != null)
			{
				s.AddRange(FlareAsset.Serialize());
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize Type
			byte tmp159;
			tmp159 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp159 == 1)
			{
				sbyte tmp160;
				tmp160 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				Type = (ELightType)tmp160;
			}
			else
				Type = null;
			
			// deserialize Range
			byte tmp161;
			tmp161 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp161 == 1)
			{
				Range = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Range = null;
			
			// deserialize SpotAngle
			byte tmp162;
			tmp162 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp162 == 1)
			{
				SpotAngle = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				SpotAngle = null;
			
			// deserialize Color
			byte tmp163;
			tmp163 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp163 == 1)
			{
				Color = new Vector4();
				offset = Color.Deserialize(s, offset);
			}
			else
				Color = null;
			
			// deserialize Intensity
			byte tmp164;
			tmp164 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp164 == 1)
			{
				Intensity = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Intensity = null;
			
			// deserialize IndirectMultiplier
			byte tmp165;
			tmp165 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp165 == 1)
			{
				IndirectMultiplier = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				IndirectMultiplier = null;
			
			// deserialize ShadowType
			byte tmp166;
			tmp166 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp166 == 1)
			{
				sbyte tmp167;
				tmp167 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				ShadowType = (ELightShadowType)tmp167;
			}
			else
				ShadowType = null;
			
			// deserialize ShadowStrength
			byte tmp168;
			tmp168 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp168 == 1)
			{
				ShadowStrength = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				ShadowStrength = null;
			
			// deserialize ShadowBias
			byte tmp169;
			tmp169 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp169 == 1)
			{
				ShadowBias = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				ShadowBias = null;
			
			// deserialize ShadowNormalBias
			byte tmp170;
			tmp170 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp170 == 1)
			{
				ShadowNormalBias = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				ShadowNormalBias = null;
			
			// deserialize ShadowNearPlane
			byte tmp171;
			tmp171 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp171 == 1)
			{
				ShadowNearPlane = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				ShadowNearPlane = null;
			
			// deserialize CookieAsset
			byte tmp172;
			tmp172 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp172 == 1)
			{
				CookieAsset = new Asset();
				offset = CookieAsset.Deserialize(s, offset);
			}
			else
				CookieAsset = null;
			
			// deserialize CookieSize
			byte tmp173;
			tmp173 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp173 == 1)
			{
				CookieSize = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				CookieSize = null;
			
			// deserialize FlareAsset
			byte tmp174;
			tmp174 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp174 == 1)
			{
				FlareAsset = new Asset();
				offset = FlareAsset.Deserialize(s, offset);
			}
			else
				FlareAsset = null;
			
			return offset;
		}
	}
	
	public enum ECameraClearFlag
	{
		Skybox = 1,
		SolidColor = 2,
		Depth = 3,
		Nothing = 4,
	}
	
	public partial class ChangeCamera : BaseAction
	{
		public ECameraClearFlag? ClearFlag { get; set; }
		public Vector4 BackgroundColor { get; set; }
		public bool? IsOrthographic { get; set; }
		public float? OrthographicSize { get; set; }
		public float? FieldOfView { get; set; }
		public float? NearClipPlane { get; set; }
		public float? FarClipPlane { get; set; }
		public Vector3 MinPosition { get; set; }
		public Vector3 MaxPosition { get; set; }
		public Vector2 MinRotation { get; set; }
		public Vector2 MaxRotation { get; set; }
		public Asset PostProcessProfileAsset { get; set; }
		

		public ChangeCamera()
		{
		}
		
		public new const string NameStatic = "ChangeCamera";
		
		public override string Name() => "ChangeCamera";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize ClearFlag
			s.Add((byte)((ClearFlag == null) ? 0 : 1));
			if (ClearFlag != null)
			{
				s.Add((byte)((sbyte)ClearFlag));
			}
			
			// serialize BackgroundColor
			s.Add((byte)((BackgroundColor == null) ? 0 : 1));
			if (BackgroundColor != null)
			{
				s.AddRange(BackgroundColor.Serialize());
			}
			
			// serialize IsOrthographic
			s.Add((byte)((IsOrthographic == null) ? 0 : 1));
			if (IsOrthographic != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)IsOrthographic));
			}
			
			// serialize OrthographicSize
			s.Add((byte)((OrthographicSize == null) ? 0 : 1));
			if (OrthographicSize != null)
			{
				s.AddRange(BitConverter.GetBytes((float)OrthographicSize));
			}
			
			// serialize FieldOfView
			s.Add((byte)((FieldOfView == null) ? 0 : 1));
			if (FieldOfView != null)
			{
				s.AddRange(BitConverter.GetBytes((float)FieldOfView));
			}
			
			// serialize NearClipPlane
			s.Add((byte)((NearClipPlane == null) ? 0 : 1));
			if (NearClipPlane != null)
			{
				s.AddRange(BitConverter.GetBytes((float)NearClipPlane));
			}
			
			// serialize FarClipPlane
			s.Add((byte)((FarClipPlane == null) ? 0 : 1));
			if (FarClipPlane != null)
			{
				s.AddRange(BitConverter.GetBytes((float)FarClipPlane));
			}
			
			// serialize MinPosition
			s.Add((byte)((MinPosition == null) ? 0 : 1));
			if (MinPosition != null)
			{
				s.AddRange(MinPosition.Serialize());
			}
			
			// serialize MaxPosition
			s.Add((byte)((MaxPosition == null) ? 0 : 1));
			if (MaxPosition != null)
			{
				s.AddRange(MaxPosition.Serialize());
			}
			
			// serialize MinRotation
			s.Add((byte)((MinRotation == null) ? 0 : 1));
			if (MinRotation != null)
			{
				s.AddRange(MinRotation.Serialize());
			}
			
			// serialize MaxRotation
			s.Add((byte)((MaxRotation == null) ? 0 : 1));
			if (MaxRotation != null)
			{
				s.AddRange(MaxRotation.Serialize());
			}
			
			// serialize PostProcessProfileAsset
			s.Add((byte)((PostProcessProfileAsset == null) ? 0 : 1));
			if (PostProcessProfileAsset != null)
			{
				s.AddRange(PostProcessProfileAsset.Serialize());
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize ClearFlag
			byte tmp175;
			tmp175 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp175 == 1)
			{
				sbyte tmp176;
				tmp176 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				ClearFlag = (ECameraClearFlag)tmp176;
			}
			else
				ClearFlag = null;
			
			// deserialize BackgroundColor
			byte tmp177;
			tmp177 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp177 == 1)
			{
				BackgroundColor = new Vector4();
				offset = BackgroundColor.Deserialize(s, offset);
			}
			else
				BackgroundColor = null;
			
			// deserialize IsOrthographic
			byte tmp178;
			tmp178 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp178 == 1)
			{
				IsOrthographic = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				IsOrthographic = null;
			
			// deserialize OrthographicSize
			byte tmp179;
			tmp179 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp179 == 1)
			{
				OrthographicSize = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				OrthographicSize = null;
			
			// deserialize FieldOfView
			byte tmp180;
			tmp180 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp180 == 1)
			{
				FieldOfView = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FieldOfView = null;
			
			// deserialize NearClipPlane
			byte tmp181;
			tmp181 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp181 == 1)
			{
				NearClipPlane = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				NearClipPlane = null;
			
			// deserialize FarClipPlane
			byte tmp182;
			tmp182 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp182 == 1)
			{
				FarClipPlane = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FarClipPlane = null;
			
			// deserialize MinPosition
			byte tmp183;
			tmp183 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp183 == 1)
			{
				MinPosition = new Vector3();
				offset = MinPosition.Deserialize(s, offset);
			}
			else
				MinPosition = null;
			
			// deserialize MaxPosition
			byte tmp184;
			tmp184 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp184 == 1)
			{
				MaxPosition = new Vector3();
				offset = MaxPosition.Deserialize(s, offset);
			}
			else
				MaxPosition = null;
			
			// deserialize MinRotation
			byte tmp185;
			tmp185 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp185 == 1)
			{
				MinRotation = new Vector2();
				offset = MinRotation.Deserialize(s, offset);
			}
			else
				MinRotation = null;
			
			// deserialize MaxRotation
			byte tmp186;
			tmp186 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp186 == 1)
			{
				MaxRotation = new Vector2();
				offset = MaxRotation.Deserialize(s, offset);
			}
			else
				MaxRotation = null;
			
			// deserialize PostProcessProfileAsset
			byte tmp187;
			tmp187 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp187 == 1)
			{
				PostProcessProfileAsset = new Asset();
				offset = PostProcessProfileAsset.Deserialize(s, offset);
			}
			else
				PostProcessProfileAsset = null;
			
			return offset;
		}
	}
	
	public partial class ClearScene : BaseAction
	{
		

		public ClearScene()
		{
		}
		
		public new const string NameStatic = "ClearScene";
		
		public override string Name() => "ClearScene";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			return offset;
		}
	}
	
	public partial class EndCycle : KSObject
	{
		

		public EndCycle()
		{
		}
		
		public new const string NameStatic = "EndCycle";
		
		public override string Name() => "EndCycle";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			return offset;
		}
	}
	
	public enum EAmbientMode
	{
		Skybox = 0,
		Trilight = 1,
		Flat = 3,
		Custom = 4,
	}
	
	public enum EDefaultReflectionMode
	{
		Skybox = 0,
		Custom = 1,
	}
	
	public enum EFogMode
	{
		Linear = 1,
		Exponential = 2,
		ExponentialSquared = 3,
	}
	
	public partial class ChangeRenderSettings : KSObject
	{
		public Vector4 AmbientEquatorColor { get; set; }
		public Vector4 AmbientGroundColor { get; set; }
		public float? AmbientIntensity { get; set; }
		public Vector4 AmbientLight { get; set; }
		public EAmbientMode? AmbientMode { get; set; }
		public Vector4 AmbientSkyColor { get; set; }
		public Asset CustomReflectionAsset { get; set; }
		public EDefaultReflectionMode? DefaultReflectionMode { get; set; }
		public int? DefaultReflectionResolution { get; set; }
		public float? FlareFadeSpeed { get; set; }
		public float? FlareStrength { get; set; }
		public bool? HasFog { get; set; }
		public EFogMode? FogMode { get; set; }
		public Vector4 FogColor { get; set; }
		public float? FogDensity { get; set; }
		public float? FogStartDistance { get; set; }
		public float? FogEndDistance { get; set; }
		public float? HaloStrength { get; set; }
		public int? ReflectionBounces { get; set; }
		public float? ReflectionIntensity { get; set; }
		public Asset SkyboxAsset { get; set; }
		public Vector4 SubtractiveShadowColor { get; set; }
		public int? SunRef { get; set; }
		public string SunChildRef { get; set; }
		

		public ChangeRenderSettings()
		{
		}
		
		public new const string NameStatic = "ChangeRenderSettings";
		
		public override string Name() => "ChangeRenderSettings";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize AmbientEquatorColor
			s.Add((byte)((AmbientEquatorColor == null) ? 0 : 1));
			if (AmbientEquatorColor != null)
			{
				s.AddRange(AmbientEquatorColor.Serialize());
			}
			
			// serialize AmbientGroundColor
			s.Add((byte)((AmbientGroundColor == null) ? 0 : 1));
			if (AmbientGroundColor != null)
			{
				s.AddRange(AmbientGroundColor.Serialize());
			}
			
			// serialize AmbientIntensity
			s.Add((byte)((AmbientIntensity == null) ? 0 : 1));
			if (AmbientIntensity != null)
			{
				s.AddRange(BitConverter.GetBytes((float)AmbientIntensity));
			}
			
			// serialize AmbientLight
			s.Add((byte)((AmbientLight == null) ? 0 : 1));
			if (AmbientLight != null)
			{
				s.AddRange(AmbientLight.Serialize());
			}
			
			// serialize AmbientMode
			s.Add((byte)((AmbientMode == null) ? 0 : 1));
			if (AmbientMode != null)
			{
				s.Add((byte)((sbyte)AmbientMode));
			}
			
			// serialize AmbientSkyColor
			s.Add((byte)((AmbientSkyColor == null) ? 0 : 1));
			if (AmbientSkyColor != null)
			{
				s.AddRange(AmbientSkyColor.Serialize());
			}
			
			// serialize CustomReflectionAsset
			s.Add((byte)((CustomReflectionAsset == null) ? 0 : 1));
			if (CustomReflectionAsset != null)
			{
				s.AddRange(CustomReflectionAsset.Serialize());
			}
			
			// serialize DefaultReflectionMode
			s.Add((byte)((DefaultReflectionMode == null) ? 0 : 1));
			if (DefaultReflectionMode != null)
			{
				s.Add((byte)((sbyte)DefaultReflectionMode));
			}
			
			// serialize DefaultReflectionResolution
			s.Add((byte)((DefaultReflectionResolution == null) ? 0 : 1));
			if (DefaultReflectionResolution != null)
			{
				s.AddRange(BitConverter.GetBytes((int)DefaultReflectionResolution));
			}
			
			// serialize FlareFadeSpeed
			s.Add((byte)((FlareFadeSpeed == null) ? 0 : 1));
			if (FlareFadeSpeed != null)
			{
				s.AddRange(BitConverter.GetBytes((float)FlareFadeSpeed));
			}
			
			// serialize FlareStrength
			s.Add((byte)((FlareStrength == null) ? 0 : 1));
			if (FlareStrength != null)
			{
				s.AddRange(BitConverter.GetBytes((float)FlareStrength));
			}
			
			// serialize HasFog
			s.Add((byte)((HasFog == null) ? 0 : 1));
			if (HasFog != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)HasFog));
			}
			
			// serialize FogMode
			s.Add((byte)((FogMode == null) ? 0 : 1));
			if (FogMode != null)
			{
				s.Add((byte)((sbyte)FogMode));
			}
			
			// serialize FogColor
			s.Add((byte)((FogColor == null) ? 0 : 1));
			if (FogColor != null)
			{
				s.AddRange(FogColor.Serialize());
			}
			
			// serialize FogDensity
			s.Add((byte)((FogDensity == null) ? 0 : 1));
			if (FogDensity != null)
			{
				s.AddRange(BitConverter.GetBytes((float)FogDensity));
			}
			
			// serialize FogStartDistance
			s.Add((byte)((FogStartDistance == null) ? 0 : 1));
			if (FogStartDistance != null)
			{
				s.AddRange(BitConverter.GetBytes((float)FogStartDistance));
			}
			
			// serialize FogEndDistance
			s.Add((byte)((FogEndDistance == null) ? 0 : 1));
			if (FogEndDistance != null)
			{
				s.AddRange(BitConverter.GetBytes((float)FogEndDistance));
			}
			
			// serialize HaloStrength
			s.Add((byte)((HaloStrength == null) ? 0 : 1));
			if (HaloStrength != null)
			{
				s.AddRange(BitConverter.GetBytes((float)HaloStrength));
			}
			
			// serialize ReflectionBounces
			s.Add((byte)((ReflectionBounces == null) ? 0 : 1));
			if (ReflectionBounces != null)
			{
				s.AddRange(BitConverter.GetBytes((int)ReflectionBounces));
			}
			
			// serialize ReflectionIntensity
			s.Add((byte)((ReflectionIntensity == null) ? 0 : 1));
			if (ReflectionIntensity != null)
			{
				s.AddRange(BitConverter.GetBytes((float)ReflectionIntensity));
			}
			
			// serialize SkyboxAsset
			s.Add((byte)((SkyboxAsset == null) ? 0 : 1));
			if (SkyboxAsset != null)
			{
				s.AddRange(SkyboxAsset.Serialize());
			}
			
			// serialize SubtractiveShadowColor
			s.Add((byte)((SubtractiveShadowColor == null) ? 0 : 1));
			if (SubtractiveShadowColor != null)
			{
				s.AddRange(SubtractiveShadowColor.Serialize());
			}
			
			// serialize SunRef
			s.Add((byte)((SunRef == null) ? 0 : 1));
			if (SunRef != null)
			{
				s.AddRange(BitConverter.GetBytes((int)SunRef));
			}
			
			// serialize SunChildRef
			s.Add((byte)((SunChildRef == null) ? 0 : 1));
			if (SunChildRef != null)
			{
				List<byte> tmp188 = new List<byte>();
				tmp188.AddRange(BitConverter.GetBytes((uint)SunChildRef.Count()));
				while (tmp188.Count > 0 && tmp188.Last() == 0)
					tmp188.RemoveAt(tmp188.Count - 1);
				s.Add((byte)tmp188.Count);
				s.AddRange(tmp188);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(SunChildRef));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize AmbientEquatorColor
			byte tmp189;
			tmp189 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp189 == 1)
			{
				AmbientEquatorColor = new Vector4();
				offset = AmbientEquatorColor.Deserialize(s, offset);
			}
			else
				AmbientEquatorColor = null;
			
			// deserialize AmbientGroundColor
			byte tmp190;
			tmp190 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp190 == 1)
			{
				AmbientGroundColor = new Vector4();
				offset = AmbientGroundColor.Deserialize(s, offset);
			}
			else
				AmbientGroundColor = null;
			
			// deserialize AmbientIntensity
			byte tmp191;
			tmp191 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp191 == 1)
			{
				AmbientIntensity = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				AmbientIntensity = null;
			
			// deserialize AmbientLight
			byte tmp192;
			tmp192 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp192 == 1)
			{
				AmbientLight = new Vector4();
				offset = AmbientLight.Deserialize(s, offset);
			}
			else
				AmbientLight = null;
			
			// deserialize AmbientMode
			byte tmp193;
			tmp193 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp193 == 1)
			{
				sbyte tmp194;
				tmp194 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				AmbientMode = (EAmbientMode)tmp194;
			}
			else
				AmbientMode = null;
			
			// deserialize AmbientSkyColor
			byte tmp195;
			tmp195 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp195 == 1)
			{
				AmbientSkyColor = new Vector4();
				offset = AmbientSkyColor.Deserialize(s, offset);
			}
			else
				AmbientSkyColor = null;
			
			// deserialize CustomReflectionAsset
			byte tmp196;
			tmp196 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp196 == 1)
			{
				CustomReflectionAsset = new Asset();
				offset = CustomReflectionAsset.Deserialize(s, offset);
			}
			else
				CustomReflectionAsset = null;
			
			// deserialize DefaultReflectionMode
			byte tmp197;
			tmp197 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp197 == 1)
			{
				sbyte tmp198;
				tmp198 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				DefaultReflectionMode = (EDefaultReflectionMode)tmp198;
			}
			else
				DefaultReflectionMode = null;
			
			// deserialize DefaultReflectionResolution
			byte tmp199;
			tmp199 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp199 == 1)
			{
				DefaultReflectionResolution = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				DefaultReflectionResolution = null;
			
			// deserialize FlareFadeSpeed
			byte tmp200;
			tmp200 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp200 == 1)
			{
				FlareFadeSpeed = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FlareFadeSpeed = null;
			
			// deserialize FlareStrength
			byte tmp201;
			tmp201 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp201 == 1)
			{
				FlareStrength = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FlareStrength = null;
			
			// deserialize HasFog
			byte tmp202;
			tmp202 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp202 == 1)
			{
				HasFog = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				HasFog = null;
			
			// deserialize FogMode
			byte tmp203;
			tmp203 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp203 == 1)
			{
				sbyte tmp204;
				tmp204 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				FogMode = (EFogMode)tmp204;
			}
			else
				FogMode = null;
			
			// deserialize FogColor
			byte tmp205;
			tmp205 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp205 == 1)
			{
				FogColor = new Vector4();
				offset = FogColor.Deserialize(s, offset);
			}
			else
				FogColor = null;
			
			// deserialize FogDensity
			byte tmp206;
			tmp206 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp206 == 1)
			{
				FogDensity = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FogDensity = null;
			
			// deserialize FogStartDistance
			byte tmp207;
			tmp207 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp207 == 1)
			{
				FogStartDistance = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FogStartDistance = null;
			
			// deserialize FogEndDistance
			byte tmp208;
			tmp208 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp208 == 1)
			{
				FogEndDistance = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FogEndDistance = null;
			
			// deserialize HaloStrength
			byte tmp209;
			tmp209 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp209 == 1)
			{
				HaloStrength = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				HaloStrength = null;
			
			// deserialize ReflectionBounces
			byte tmp210;
			tmp210 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp210 == 1)
			{
				ReflectionBounces = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				ReflectionBounces = null;
			
			// deserialize ReflectionIntensity
			byte tmp211;
			tmp211 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp211 == 1)
			{
				ReflectionIntensity = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				ReflectionIntensity = null;
			
			// deserialize SkyboxAsset
			byte tmp212;
			tmp212 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp212 == 1)
			{
				SkyboxAsset = new Asset();
				offset = SkyboxAsset.Deserialize(s, offset);
			}
			else
				SkyboxAsset = null;
			
			// deserialize SubtractiveShadowColor
			byte tmp213;
			tmp213 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp213 == 1)
			{
				SubtractiveShadowColor = new Vector4();
				offset = SubtractiveShadowColor.Deserialize(s, offset);
			}
			else
				SubtractiveShadowColor = null;
			
			// deserialize SunRef
			byte tmp214;
			tmp214 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp214 == 1)
			{
				SunRef = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				SunRef = null;
			
			// deserialize SunChildRef
			byte tmp215;
			tmp215 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp215 == 1)
			{
				byte tmp216;
				tmp216 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp217 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp217, 0, tmp216);
				offset += tmp216;
				uint tmp218;
				tmp218 = BitConverter.ToUInt32(tmp217, (int)0);
				
				SunChildRef = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp218).ToArray());
				offset += tmp218;
			}
			else
				SunChildRef = null;
			
			return offset;
		}
	}
} // namespace KS.SceneActions
