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
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(ChildRef));
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
				
				ChildRef = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp6).ToArray());
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
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(BundleName));
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
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(AssetName));
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
				
				BundleName = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp22).ToArray());
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
				
				AssetName = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp26).ToArray());
				offset += tmp26;
			}
			else
				AssetName = null;
			
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
				List<byte> tmp27 = new List<byte>();
				tmp27.AddRange(BitConverter.GetBytes((uint)ParentChildRef.Count()));
				while (tmp27.Count > 0 && tmp27.Last() == 0)
					tmp27.RemoveAt(tmp27.Count - 1);
				s.Add((byte)tmp27.Count);
				s.AddRange(tmp27);
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(ParentChildRef));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize ParentRef
			byte tmp28;
			tmp28 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp28 == 1)
			{
				ParentRef = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				ParentRef = null;
			
			// deserialize ParentChildRef
			byte tmp29;
			tmp29 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp29 == 1)
			{
				byte tmp30;
				tmp30 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp31 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp31, 0, tmp30);
				offset += tmp30;
				uint tmp32;
				tmp32 = BitConverter.ToUInt32(tmp31, (int)0);
				
				ParentChildRef = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp32).ToArray());
				offset += tmp32;
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
	
	public partial class InstantiateBundleAsset : BaseCreation
	{
		public Asset Asset { get; set; }
		

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
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize Asset
			byte tmp33;
			tmp33 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp33 == 1)
			{
				Asset = new Asset();
				offset = Asset.Deserialize(s, offset);
			}
			else
				Asset = null;
			
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
			byte tmp34;
			tmp34 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp34 == 1)
			{
				sbyte tmp35;
				tmp35 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				Type = (EBasicObjectType)tmp35;
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
		RawImage = 3,
		Panel = 4,
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
			byte tmp36;
			tmp36 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp36 == 1)
			{
				sbyte tmp37;
				tmp37 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				Type = (EUIElementType)tmp37;
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
			byte tmp38;
			tmp38 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp38 == 1)
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
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize Position
			byte tmp39;
			tmp39 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp39 == 1)
			{
				Position = new Vector3();
				offset = Position.Deserialize(s, offset);
			}
			else
				Position = null;
			
			// deserialize Rotation
			byte tmp40;
			tmp40 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp40 == 1)
			{
				Rotation = new Vector3();
				offset = Rotation.Deserialize(s, offset);
			}
			else
				Rotation = null;
			
			// deserialize Scale
			byte tmp41;
			tmp41 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp41 == 1)
			{
				Scale = new Vector3();
				offset = Scale.Deserialize(s, offset);
			}
			else
				Scale = null;
			
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
				List<byte> tmp42 = new List<byte>();
				tmp42.AddRange(BitConverter.GetBytes((uint)VarName.Count()));
				while (tmp42.Count > 0 && tmp42.Last() == 0)
					tmp42.RemoveAt(tmp42.Count - 1);
				s.Add((byte)tmp42.Count);
				s.AddRange(tmp42);
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(VarName));
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
			byte tmp43;
			tmp43 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp43 == 1)
			{
				byte tmp44;
				tmp44 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp45 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp45, 0, tmp44);
				offset += tmp44;
				uint tmp46;
				tmp46 = BitConverter.ToUInt32(tmp45, (int)0);
				
				VarName = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp46).ToArray());
				offset += tmp46;
			}
			else
				VarName = null;
			
			// deserialize VarType
			byte tmp47;
			tmp47 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp47 == 1)
			{
				sbyte tmp48;
				tmp48 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				VarType = (EAnimatorVariableType)tmp48;
			}
			else
				VarType = null;
			
			// deserialize IntValue
			byte tmp49;
			tmp49 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp49 == 1)
			{
				IntValue = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				IntValue = null;
			
			// deserialize FloatValue
			byte tmp50;
			tmp50 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp50 == 1)
			{
				FloatValue = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FloatValue = null;
			
			// deserialize BoolValue
			byte tmp51;
			tmp51 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp51 == 1)
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
				List<byte> tmp52 = new List<byte>();
				tmp52.AddRange(BitConverter.GetBytes((uint)StateName.Count()));
				while (tmp52.Count > 0 && tmp52.Last() == 0)
					tmp52.RemoveAt(tmp52.Count - 1);
				s.Add((byte)tmp52.Count);
				s.AddRange(tmp52);
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(StateName));
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
			byte tmp53;
			tmp53 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp53 == 1)
			{
				byte tmp54;
				tmp54 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp55 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp55, 0, tmp54);
				offset += tmp54;
				uint tmp56;
				tmp56 = BitConverter.ToUInt32(tmp55, (int)0);
				
				StateName = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp56).ToArray());
				offset += tmp56;
			}
			else
				StateName = null;
			
			// deserialize Layer
			byte tmp57;
			tmp57 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp57 == 1)
			{
				Layer = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				Layer = null;
			
			// deserialize NormalizedTime
			byte tmp58;
			tmp58 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp58 == 1)
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
			byte tmp59;
			tmp59 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp59 == 1)
			{
				AudioClipAsset = new Asset();
				offset = AudioClipAsset.Deserialize(s, offset);
			}
			else
				AudioClipAsset = null;
			
			// deserialize Time
			byte tmp60;
			tmp60 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp60 == 1)
			{
				Time = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Time = null;
			
			// deserialize Mute
			byte tmp61;
			tmp61 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp61 == 1)
			{
				Mute = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				Mute = null;
			
			// deserialize Loop
			byte tmp62;
			tmp62 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp62 == 1)
			{
				Loop = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				Loop = null;
			
			// deserialize Priority
			byte tmp63;
			tmp63 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp63 == 1)
			{
				Priority = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				Priority = null;
			
			// deserialize Volume
			byte tmp64;
			tmp64 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp64 == 1)
			{
				Volume = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Volume = null;
			
			// deserialize SpatialBlend
			byte tmp65;
			tmp65 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp65 == 1)
			{
				SpatialBlend = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				SpatialBlend = null;
			
			// deserialize Play
			byte tmp66;
			tmp66 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp66 == 1)
			{
				Play = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				Play = null;
			
			// deserialize Stop
			byte tmp67;
			tmp67 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp67 == 1)
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
			byte tmp68;
			tmp68 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp68 == 1)
			{
				Position = new Vector3();
				offset = Position.Deserialize(s, offset);
			}
			else
				Position = null;
			
			// deserialize Rotation
			byte tmp69;
			tmp69 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp69 == 1)
			{
				Rotation = new Vector3();
				offset = Rotation.Deserialize(s, offset);
			}
			else
				Rotation = null;
			
			// deserialize Scale
			byte tmp70;
			tmp70 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp70 == 1)
			{
				Scale = new Vector3();
				offset = Scale.Deserialize(s, offset);
			}
			else
				Scale = null;
			
			// deserialize Pivot
			byte tmp71;
			tmp71 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp71 == 1)
			{
				Pivot = new Vector2();
				offset = Pivot.Deserialize(s, offset);
			}
			else
				Pivot = null;
			
			// deserialize AnchorMin
			byte tmp72;
			tmp72 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp72 == 1)
			{
				AnchorMin = new Vector2();
				offset = AnchorMin.Deserialize(s, offset);
			}
			else
				AnchorMin = null;
			
			// deserialize AnchorMax
			byte tmp73;
			tmp73 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp73 == 1)
			{
				AnchorMax = new Vector2();
				offset = AnchorMax.Deserialize(s, offset);
			}
			else
				AnchorMax = null;
			
			// deserialize Size
			byte tmp74;
			tmp74 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp74 == 1)
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
				List<byte> tmp75 = new List<byte>();
				tmp75.AddRange(BitConverter.GetBytes((uint)FontName.Count()));
				while (tmp75.Count > 0 && tmp75.Last() == 0)
					tmp75.RemoveAt(tmp75.Count - 1);
				s.Add((byte)tmp75.Count);
				s.AddRange(tmp75);
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(FontName));
			}
			
			// serialize Text
			s.Add((byte)((Text == null) ? 0 : 1));
			if (Text != null)
			{
				List<byte> tmp76 = new List<byte>();
				tmp76.AddRange(BitConverter.GetBytes((uint)Text.Count()));
				while (tmp76.Count > 0 && tmp76.Last() == 0)
					tmp76.RemoveAt(tmp76.Count - 1);
				s.Add((byte)tmp76.Count);
				s.AddRange(tmp76);
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(Text));
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
			byte tmp77;
			tmp77 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp77 == 1)
			{
				FontAsset = new Asset();
				offset = FontAsset.Deserialize(s, offset);
			}
			else
				FontAsset = null;
			
			// deserialize FontName
			byte tmp78;
			tmp78 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp78 == 1)
			{
				byte tmp79;
				tmp79 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp80 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp80, 0, tmp79);
				offset += tmp79;
				uint tmp81;
				tmp81 = BitConverter.ToUInt32(tmp80, (int)0);
				
				FontName = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp81).ToArray());
				offset += tmp81;
			}
			else
				FontName = null;
			
			// deserialize Text
			byte tmp82;
			tmp82 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp82 == 1)
			{
				byte tmp83;
				tmp83 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp84 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp84, 0, tmp83);
				offset += tmp83;
				uint tmp85;
				tmp85 = BitConverter.ToUInt32(tmp84, (int)0);
				
				Text = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp85).ToArray());
				offset += tmp85;
			}
			else
				Text = null;
			
			// deserialize FontSize
			byte tmp86;
			tmp86 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp86 == 1)
			{
				FontSize = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FontSize = null;
			
			// deserialize Alignment
			byte tmp87;
			tmp87 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp87 == 1)
			{
				short tmp88;
				tmp88 = BitConverter.ToInt16(s, (int)offset);
				offset += sizeof(short);
				Alignment = (ETextAlignmentOption)tmp88;
			}
			else
				Alignment = null;
			
			// deserialize WordWrappingRatios
			byte tmp89;
			tmp89 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp89 == 1)
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
			byte tmp90;
			tmp90 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp90 == 1)
			{
				Value = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Value = null;
			
			// deserialize MaxValue
			byte tmp91;
			tmp91 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp91 == 1)
			{
				MaxValue = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				MaxValue = null;
			
			// deserialize MinValue
			byte tmp92;
			tmp92 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp92 == 1)
			{
				MinValue = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				MinValue = null;
			
			// deserialize Direction
			byte tmp93;
			tmp93 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp93 == 1)
			{
				short tmp94;
				tmp94 = BitConverter.ToInt16(s, (int)offset);
				offset += sizeof(short);
				Direction = (ESliderDirection)tmp94;
			}
			else
				Direction = null;
			
			// deserialize BackgroundColor
			byte tmp95;
			tmp95 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp95 == 1)
			{
				BackgroundColor = new Vector4();
				offset = BackgroundColor.Deserialize(s, offset);
			}
			else
				BackgroundColor = null;
			
			// deserialize FillColor
			byte tmp96;
			tmp96 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp96 == 1)
			{
				FillColor = new Vector4();
				offset = FillColor.Deserialize(s, offset);
			}
			else
				FillColor = null;
			
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
			byte tmp97;
			tmp97 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp97 == 1)
			{
				TextureAsset = new Asset();
				offset = TextureAsset.Deserialize(s, offset);
			}
			else
				TextureAsset = null;
			
			// deserialize MaterialAsset
			byte tmp98;
			tmp98 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp98 == 1)
			{
				MaterialAsset = new Asset();
				offset = MaterialAsset.Deserialize(s, offset);
			}
			else
				MaterialAsset = null;
			
			// deserialize Color
			byte tmp99;
			tmp99 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp99 == 1)
			{
				Color = new Vector4();
				offset = Color.Deserialize(s, offset);
			}
			else
				Color = null;
			
			// deserialize UvRect
			byte tmp100;
			tmp100 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp100 == 1)
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
				List<byte> tmp101 = new List<byte>();
				tmp101.AddRange(BitConverter.GetBytes((uint)SiblingRefAsBaseIndex.Count()));
				while (tmp101.Count > 0 && tmp101.Last() == 0)
					tmp101.RemoveAt(tmp101.Count - 1);
				s.Add((byte)tmp101.Count);
				s.AddRange(tmp101);
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(SiblingRefAsBaseIndex));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize NewIndex
			byte tmp102;
			tmp102 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp102 == 1)
			{
				NewIndex = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				NewIndex = null;
			
			// deserialize GotoFirst
			byte tmp103;
			tmp103 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp103 == 1)
			{
				GotoFirst = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				GotoFirst = null;
			
			// deserialize GotoLast
			byte tmp104;
			tmp104 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp104 == 1)
			{
				GotoLast = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				GotoLast = null;
			
			// deserialize ChangeIndex
			byte tmp105;
			tmp105 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp105 == 1)
			{
				ChangeIndex = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				ChangeIndex = null;
			
			// deserialize SiblingRefAsBaseIndex
			byte tmp106;
			tmp106 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp106 == 1)
			{
				byte tmp107;
				tmp107 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp108 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp108, 0, tmp107);
				offset += tmp107;
				uint tmp109;
				tmp109 = BitConverter.ToUInt32(tmp108, (int)0);
				
				SiblingRefAsBaseIndex = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp109).ToArray());
				offset += tmp109;
			}
			else
				SiblingRefAsBaseIndex = null;
			
			return offset;
		}
	}
	
	public enum EComponentType
	{
		ParticleSystemManager = 0,
	}
	
	public partial class ManageComponents : BaseAction
	{
		public EComponentType? Type { get; set; }
		public bool? Add { get; set; }
		public bool? IsActive { get; set; }
		

		public ManageComponents()
		{
		}
		
		public new const string NameStatic = "ManageComponents";
		
		public override string Name() => "ManageComponents";
		
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
			byte tmp110;
			tmp110 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp110 == 1)
			{
				sbyte tmp111;
				tmp111 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				Type = (EComponentType)tmp111;
			}
			else
				Type = null;
			
			// deserialize Add
			byte tmp112;
			tmp112 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp112 == 1)
			{
				Add = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				Add = null;
			
			// deserialize IsActive
			byte tmp113;
			tmp113 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp113 == 1)
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
			byte tmp114;
			tmp114 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp114 == 1)
			{
				SpriteAsset = new Asset();
				offset = SpriteAsset.Deserialize(s, offset);
			}
			else
				SpriteAsset = null;
			
			// deserialize Color
			byte tmp115;
			tmp115 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp115 == 1)
			{
				Color = new Vector4();
				offset = Color.Deserialize(s, offset);
			}
			else
				Color = null;
			
			// deserialize FlipX
			byte tmp116;
			tmp116 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp116 == 1)
			{
				FlipX = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				FlipX = null;
			
			// deserialize FlipY
			byte tmp117;
			tmp117 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp117 == 1)
			{
				FlipY = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				FlipY = null;
			
			// deserialize Order
			byte tmp118;
			tmp118 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp118 == 1)
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
			byte tmp119;
			tmp119 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp119 == 1)
			{
				MaterialAsset = new Asset();
				offset = MaterialAsset.Deserialize(s, offset);
			}
			else
				MaterialAsset = null;
			
			// deserialize Index
			byte tmp120;
			tmp120 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp120 == 1)
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
			byte tmp121;
			tmp121 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp121 == 1)
			{
				FillColor = new Vector4();
				offset = FillColor.Deserialize(s, offset);
			}
			else
				FillColor = null;
			
			// deserialize XRadius
			byte tmp122;
			tmp122 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp122 == 1)
			{
				XRadius = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				XRadius = null;
			
			// deserialize YRadius
			byte tmp123;
			tmp123 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp123 == 1)
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
				List<byte> tmp124 = new List<byte>();
				tmp124.AddRange(BitConverter.GetBytes((uint)Vertices.Count()));
				while (tmp124.Count > 0 && tmp124.Last() == 0)
					tmp124.RemoveAt(tmp124.Count - 1);
				s.Add((byte)tmp124.Count);
				s.AddRange(tmp124);
				
				foreach (var tmp125 in Vertices)
				{
					s.Add((byte)((tmp125 == null) ? 0 : 1));
					if (tmp125 != null)
					{
						s.AddRange(tmp125.Serialize());
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
			byte tmp126;
			tmp126 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp126 == 1)
			{
				FillColor = new Vector4();
				offset = FillColor.Deserialize(s, offset);
			}
			else
				FillColor = null;
			
			// deserialize Vertices
			byte tmp127;
			tmp127 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp127 == 1)
			{
				byte tmp128;
				tmp128 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp129 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp129, 0, tmp128);
				offset += tmp128;
				uint tmp130;
				tmp130 = BitConverter.ToUInt32(tmp129, (int)0);
				
				Vertices = new List<Vector2>();
				for (uint tmp131 = 0; tmp131 < tmp130; tmp131++)
				{
					Vector2 tmp132;
					byte tmp133;
					tmp133 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp133 == 1)
					{
						tmp132 = new Vector2();
						offset = tmp132.Deserialize(s, offset);
					}
					else
						tmp132 = null;
					Vertices.Add(tmp132);
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
				List<byte> tmp134 = new List<byte>();
				tmp134.AddRange(BitConverter.GetBytes((uint)Vertices.Count()));
				while (tmp134.Count > 0 && tmp134.Last() == 0)
					tmp134.RemoveAt(tmp134.Count - 1);
				s.Add((byte)tmp134.Count);
				s.AddRange(tmp134);
				
				foreach (var tmp135 in Vertices)
				{
					s.Add((byte)((tmp135 == null) ? 0 : 1));
					if (tmp135 != null)
					{
						s.AddRange(tmp135.Serialize());
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
			byte tmp136;
			tmp136 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp136 == 1)
			{
				FillColor = new Vector4();
				offset = FillColor.Deserialize(s, offset);
			}
			else
				FillColor = null;
			
			// deserialize Vertices
			byte tmp137;
			tmp137 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp137 == 1)
			{
				byte tmp138;
				tmp138 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp139 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp139, 0, tmp138);
				offset += tmp138;
				uint tmp140;
				tmp140 = BitConverter.ToUInt32(tmp139, (int)0);
				
				Vertices = new List<Vector2>();
				for (uint tmp141 = 0; tmp141 < tmp140; tmp141++)
				{
					Vector2 tmp142;
					byte tmp143;
					tmp143 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp143 == 1)
					{
						tmp142 = new Vector2();
						offset = tmp142.Deserialize(s, offset);
					}
					else
						tmp142 = null;
					Vertices.Add(tmp142);
				}
			}
			else
				Vertices = null;
			
			// deserialize Width
			byte tmp144;
			tmp144 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp144 == 1)
			{
				Width = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Width = null;
			
			// deserialize CornerVertices
			byte tmp145;
			tmp145 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp145 == 1)
			{
				CornerVertices = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				CornerVertices = null;
			
			// deserialize EndCapVertices
			byte tmp146;
			tmp146 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp146 == 1)
			{
				EndCapVertices = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				EndCapVertices = null;
			
			// deserialize Loop
			byte tmp147;
			tmp147 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp147 == 1)
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
			byte tmp148;
			tmp148 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp148 == 1)
			{
				sbyte tmp149;
				tmp149 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				Type = (ELightType)tmp149;
			}
			else
				Type = null;
			
			// deserialize Range
			byte tmp150;
			tmp150 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp150 == 1)
			{
				Range = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Range = null;
			
			// deserialize SpotAngle
			byte tmp151;
			tmp151 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp151 == 1)
			{
				SpotAngle = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				SpotAngle = null;
			
			// deserialize Color
			byte tmp152;
			tmp152 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp152 == 1)
			{
				Color = new Vector4();
				offset = Color.Deserialize(s, offset);
			}
			else
				Color = null;
			
			// deserialize Intensity
			byte tmp153;
			tmp153 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp153 == 1)
			{
				Intensity = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Intensity = null;
			
			// deserialize IndirectMultiplier
			byte tmp154;
			tmp154 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp154 == 1)
			{
				IndirectMultiplier = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				IndirectMultiplier = null;
			
			// deserialize ShadowType
			byte tmp155;
			tmp155 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp155 == 1)
			{
				sbyte tmp156;
				tmp156 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				ShadowType = (ELightShadowType)tmp156;
			}
			else
				ShadowType = null;
			
			// deserialize ShadowStrength
			byte tmp157;
			tmp157 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp157 == 1)
			{
				ShadowStrength = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				ShadowStrength = null;
			
			// deserialize ShadowBias
			byte tmp158;
			tmp158 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp158 == 1)
			{
				ShadowBias = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				ShadowBias = null;
			
			// deserialize ShadowNormalBias
			byte tmp159;
			tmp159 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp159 == 1)
			{
				ShadowNormalBias = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				ShadowNormalBias = null;
			
			// deserialize ShadowNearPlane
			byte tmp160;
			tmp160 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp160 == 1)
			{
				ShadowNearPlane = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				ShadowNearPlane = null;
			
			// deserialize CookieAsset
			byte tmp161;
			tmp161 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp161 == 1)
			{
				CookieAsset = new Asset();
				offset = CookieAsset.Deserialize(s, offset);
			}
			else
				CookieAsset = null;
			
			// deserialize CookieSize
			byte tmp162;
			tmp162 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp162 == 1)
			{
				CookieSize = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				CookieSize = null;
			
			// deserialize FlareAsset
			byte tmp163;
			tmp163 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp163 == 1)
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
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize ClearFlag
			byte tmp164;
			tmp164 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp164 == 1)
			{
				sbyte tmp165;
				tmp165 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				ClearFlag = (ECameraClearFlag)tmp165;
			}
			else
				ClearFlag = null;
			
			// deserialize BackgroundColor
			byte tmp166;
			tmp166 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp166 == 1)
			{
				BackgroundColor = new Vector4();
				offset = BackgroundColor.Deserialize(s, offset);
			}
			else
				BackgroundColor = null;
			
			// deserialize IsOrthographic
			byte tmp167;
			tmp167 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp167 == 1)
			{
				IsOrthographic = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				IsOrthographic = null;
			
			// deserialize OrthographicSize
			byte tmp168;
			tmp168 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp168 == 1)
			{
				OrthographicSize = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				OrthographicSize = null;
			
			// deserialize FieldOfView
			byte tmp169;
			tmp169 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp169 == 1)
			{
				FieldOfView = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FieldOfView = null;
			
			// deserialize NearClipPlane
			byte tmp170;
			tmp170 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp170 == 1)
			{
				NearClipPlane = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				NearClipPlane = null;
			
			// deserialize FarClipPlane
			byte tmp171;
			tmp171 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp171 == 1)
			{
				FarClipPlane = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FarClipPlane = null;
			
			return offset;
		}
	}
	
	public partial class StoreBundleData : BaseAction
	{
		public string BundleName { get; set; }
		public string BundleData { get; set; }
		

		public StoreBundleData()
		{
		}
		
		public new const string NameStatic = "StoreBundleData";
		
		public override string Name() => "StoreBundleData";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize BundleName
			s.Add((byte)((BundleName == null) ? 0 : 1));
			if (BundleName != null)
			{
				List<byte> tmp172 = new List<byte>();
				tmp172.AddRange(BitConverter.GetBytes((uint)BundleName.Count()));
				while (tmp172.Count > 0 && tmp172.Last() == 0)
					tmp172.RemoveAt(tmp172.Count - 1);
				s.Add((byte)tmp172.Count);
				s.AddRange(tmp172);
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(BundleName));
			}
			
			// serialize BundleData
			s.Add((byte)((BundleData == null) ? 0 : 1));
			if (BundleData != null)
			{
				List<byte> tmp173 = new List<byte>();
				tmp173.AddRange(BitConverter.GetBytes((uint)BundleData.Count()));
				while (tmp173.Count > 0 && tmp173.Last() == 0)
					tmp173.RemoveAt(tmp173.Count - 1);
				s.Add((byte)tmp173.Count);
				s.AddRange(tmp173);
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(BundleData));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize BundleName
			byte tmp174;
			tmp174 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp174 == 1)
			{
				byte tmp175;
				tmp175 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp176 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp176, 0, tmp175);
				offset += tmp175;
				uint tmp177;
				tmp177 = BitConverter.ToUInt32(tmp176, (int)0);
				
				BundleName = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp177).ToArray());
				offset += tmp177;
			}
			else
				BundleName = null;
			
			// deserialize BundleData
			byte tmp178;
			tmp178 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp178 == 1)
			{
				byte tmp179;
				tmp179 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp180 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp180, 0, tmp179);
				offset += tmp179;
				uint tmp181;
				tmp181 = BitConverter.ToUInt32(tmp180, (int)0);
				
				BundleData = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp181).ToArray());
				offset += tmp181;
			}
			else
				BundleData = null;
			
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
} // namespace KS.SceneActions
