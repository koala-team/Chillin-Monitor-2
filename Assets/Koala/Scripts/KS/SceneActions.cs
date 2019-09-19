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
	
	public partial class LayerMask : KSObject
	{
		public int? MasksInt { get; set; }
		public List<string> MasksString { get; set; }
		

		public LayerMask()
		{
		}
		
		public new const string NameStatic = "LayerMask";
		
		public override string Name() => "LayerMask";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize MasksInt
			s.Add((byte)((MasksInt == null) ? 0 : 1));
			if (MasksInt != null)
			{
				s.AddRange(BitConverter.GetBytes((int)MasksInt));
			}
			
			// serialize MasksString
			s.Add((byte)((MasksString == null) ? 0 : 1));
			if (MasksString != null)
			{
				List<byte> tmp28 = new List<byte>();
				tmp28.AddRange(BitConverter.GetBytes((uint)MasksString.Count()));
				while (tmp28.Count > 0 && tmp28.Last() == 0)
					tmp28.RemoveAt(tmp28.Count - 1);
				s.Add((byte)tmp28.Count);
				s.AddRange(tmp28);
				
				foreach (var tmp29 in MasksString)
				{
					s.Add((byte)((tmp29 == null) ? 0 : 1));
					if (tmp29 != null)
					{
						List<byte> tmp30 = new List<byte>();
						tmp30.AddRange(BitConverter.GetBytes((uint)tmp29.Count()));
						while (tmp30.Count > 0 && tmp30.Last() == 0)
							tmp30.RemoveAt(tmp30.Count - 1);
						s.Add((byte)tmp30.Count);
						s.AddRange(tmp30);
						
						s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(tmp29));
					}
				}
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize MasksInt
			byte tmp31;
			tmp31 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp31 == 1)
			{
				MasksInt = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				MasksInt = null;
			
			// deserialize MasksString
			byte tmp32;
			tmp32 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp32 == 1)
			{
				byte tmp33;
				tmp33 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp34 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp34, 0, tmp33);
				offset += tmp33;
				uint tmp35;
				tmp35 = BitConverter.ToUInt32(tmp34, (int)0);
				
				MasksString = new List<string>();
				for (uint tmp36 = 0; tmp36 < tmp35; tmp36++)
				{
					string tmp37;
					byte tmp38;
					tmp38 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp38 == 1)
					{
						byte tmp39;
						tmp39 = (byte)s[(int)offset];
						offset += sizeof(byte);
						byte[] tmp40 = new byte[sizeof(uint)];
						Array.Copy(s, offset, tmp40, 0, tmp39);
						offset += tmp39;
						uint tmp41;
						tmp41 = BitConverter.ToUInt32(tmp40, (int)0);
						
						tmp37 = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp41).ToArray());
						offset += tmp41;
					}
					else
						tmp37 = null;
					MasksString.Add(tmp37);
				}
			}
			else
				MasksString = null;
			
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
				List<byte> tmp42 = new List<byte>();
				tmp42.AddRange(BitConverter.GetBytes((uint)ParentChildRef.Count()));
				while (tmp42.Count > 0 && tmp42.Last() == 0)
					tmp42.RemoveAt(tmp42.Count - 1);
				s.Add((byte)tmp42.Count);
				s.AddRange(tmp42);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(ParentChildRef));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize ParentRef
			byte tmp43;
			tmp43 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp43 == 1)
			{
				ParentRef = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				ParentRef = null;
			
			// deserialize ParentChildRef
			byte tmp44;
			tmp44 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp44 == 1)
			{
				byte tmp45;
				tmp45 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp46 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp46, 0, tmp45);
				offset += tmp45;
				uint tmp47;
				tmp47 = BitConverter.ToUInt32(tmp46, (int)0);
				
				ParentChildRef = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp47).ToArray());
				offset += tmp47;
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
			byte tmp48;
			tmp48 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp48 == 1)
			{
				Asset = new Asset();
				offset = Asset.Deserialize(s, offset);
			}
			else
				Asset = null;
			
			// deserialize DefaultParent
			byte tmp49;
			tmp49 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp49 == 1)
			{
				sbyte tmp50;
				tmp50 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				DefaultParent = (EDefaultParent)tmp50;
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
			byte tmp51;
			tmp51 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp51 == 1)
			{
				sbyte tmp52;
				tmp52 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				Type = (EBasicObjectType)tmp52;
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
			byte tmp53;
			tmp53 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp53 == 1)
			{
				sbyte tmp54;
				tmp54 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				Type = (EUIElementType)tmp54;
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
			byte tmp55;
			tmp55 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp55 == 1)
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
			byte tmp56;
			tmp56 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp56 == 1)
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
			byte tmp57;
			tmp57 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp57 == 1)
			{
				Position = new Vector3();
				offset = Position.Deserialize(s, offset);
			}
			else
				Position = null;
			
			// deserialize Rotation
			byte tmp58;
			tmp58 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp58 == 1)
			{
				Rotation = new Vector3();
				offset = Rotation.Deserialize(s, offset);
			}
			else
				Rotation = null;
			
			// deserialize Scale
			byte tmp59;
			tmp59 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp59 == 1)
			{
				Scale = new Vector3();
				offset = Scale.Deserialize(s, offset);
			}
			else
				Scale = null;
			
			// deserialize ChangeLocal
			byte tmp60;
			tmp60 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp60 == 1)
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
				List<byte> tmp61 = new List<byte>();
				tmp61.AddRange(BitConverter.GetBytes((uint)VarName.Count()));
				while (tmp61.Count > 0 && tmp61.Last() == 0)
					tmp61.RemoveAt(tmp61.Count - 1);
				s.Add((byte)tmp61.Count);
				s.AddRange(tmp61);
				
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
			byte tmp62;
			tmp62 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp62 == 1)
			{
				byte tmp63;
				tmp63 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp64 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp64, 0, tmp63);
				offset += tmp63;
				uint tmp65;
				tmp65 = BitConverter.ToUInt32(tmp64, (int)0);
				
				VarName = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp65).ToArray());
				offset += tmp65;
			}
			else
				VarName = null;
			
			// deserialize VarType
			byte tmp66;
			tmp66 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp66 == 1)
			{
				sbyte tmp67;
				tmp67 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				VarType = (EAnimatorVariableType)tmp67;
			}
			else
				VarType = null;
			
			// deserialize IntValue
			byte tmp68;
			tmp68 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp68 == 1)
			{
				IntValue = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				IntValue = null;
			
			// deserialize FloatValue
			byte tmp69;
			tmp69 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp69 == 1)
			{
				FloatValue = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FloatValue = null;
			
			// deserialize BoolValue
			byte tmp70;
			tmp70 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp70 == 1)
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
				List<byte> tmp71 = new List<byte>();
				tmp71.AddRange(BitConverter.GetBytes((uint)StateName.Count()));
				while (tmp71.Count > 0 && tmp71.Last() == 0)
					tmp71.RemoveAt(tmp71.Count - 1);
				s.Add((byte)tmp71.Count);
				s.AddRange(tmp71);
				
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
			byte tmp72;
			tmp72 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp72 == 1)
			{
				byte tmp73;
				tmp73 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp74 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp74, 0, tmp73);
				offset += tmp73;
				uint tmp75;
				tmp75 = BitConverter.ToUInt32(tmp74, (int)0);
				
				StateName = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp75).ToArray());
				offset += tmp75;
			}
			else
				StateName = null;
			
			// deserialize Layer
			byte tmp76;
			tmp76 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp76 == 1)
			{
				Layer = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				Layer = null;
			
			// deserialize NormalizedTime
			byte tmp77;
			tmp77 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp77 == 1)
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
			byte tmp78;
			tmp78 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp78 == 1)
			{
				AudioClipAsset = new Asset();
				offset = AudioClipAsset.Deserialize(s, offset);
			}
			else
				AudioClipAsset = null;
			
			// deserialize Time
			byte tmp79;
			tmp79 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp79 == 1)
			{
				Time = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Time = null;
			
			// deserialize Mute
			byte tmp80;
			tmp80 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp80 == 1)
			{
				Mute = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				Mute = null;
			
			// deserialize Loop
			byte tmp81;
			tmp81 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp81 == 1)
			{
				Loop = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				Loop = null;
			
			// deserialize Priority
			byte tmp82;
			tmp82 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp82 == 1)
			{
				Priority = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				Priority = null;
			
			// deserialize Volume
			byte tmp83;
			tmp83 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp83 == 1)
			{
				Volume = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Volume = null;
			
			// deserialize SpatialBlend
			byte tmp84;
			tmp84 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp84 == 1)
			{
				SpatialBlend = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				SpatialBlend = null;
			
			// deserialize Play
			byte tmp85;
			tmp85 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp85 == 1)
			{
				Play = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				Play = null;
			
			// deserialize Stop
			byte tmp86;
			tmp86 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp86 == 1)
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
			byte tmp87;
			tmp87 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp87 == 1)
			{
				Position = new Vector3();
				offset = Position.Deserialize(s, offset);
			}
			else
				Position = null;
			
			// deserialize Rotation
			byte tmp88;
			tmp88 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp88 == 1)
			{
				Rotation = new Vector3();
				offset = Rotation.Deserialize(s, offset);
			}
			else
				Rotation = null;
			
			// deserialize Scale
			byte tmp89;
			tmp89 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp89 == 1)
			{
				Scale = new Vector3();
				offset = Scale.Deserialize(s, offset);
			}
			else
				Scale = null;
			
			// deserialize Pivot
			byte tmp90;
			tmp90 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp90 == 1)
			{
				Pivot = new Vector2();
				offset = Pivot.Deserialize(s, offset);
			}
			else
				Pivot = null;
			
			// deserialize AnchorMin
			byte tmp91;
			tmp91 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp91 == 1)
			{
				AnchorMin = new Vector2();
				offset = AnchorMin.Deserialize(s, offset);
			}
			else
				AnchorMin = null;
			
			// deserialize AnchorMax
			byte tmp92;
			tmp92 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp92 == 1)
			{
				AnchorMax = new Vector2();
				offset = AnchorMax.Deserialize(s, offset);
			}
			else
				AnchorMax = null;
			
			// deserialize Size
			byte tmp93;
			tmp93 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp93 == 1)
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
				List<byte> tmp94 = new List<byte>();
				tmp94.AddRange(BitConverter.GetBytes((uint)FontName.Count()));
				while (tmp94.Count > 0 && tmp94.Last() == 0)
					tmp94.RemoveAt(tmp94.Count - 1);
				s.Add((byte)tmp94.Count);
				s.AddRange(tmp94);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(FontName));
			}
			
			// serialize Text
			s.Add((byte)((Text == null) ? 0 : 1));
			if (Text != null)
			{
				List<byte> tmp95 = new List<byte>();
				tmp95.AddRange(BitConverter.GetBytes((uint)Text.Count()));
				while (tmp95.Count > 0 && tmp95.Last() == 0)
					tmp95.RemoveAt(tmp95.Count - 1);
				s.Add((byte)tmp95.Count);
				s.AddRange(tmp95);
				
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
			byte tmp96;
			tmp96 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp96 == 1)
			{
				FontAsset = new Asset();
				offset = FontAsset.Deserialize(s, offset);
			}
			else
				FontAsset = null;
			
			// deserialize FontName
			byte tmp97;
			tmp97 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp97 == 1)
			{
				byte tmp98;
				tmp98 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp99 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp99, 0, tmp98);
				offset += tmp98;
				uint tmp100;
				tmp100 = BitConverter.ToUInt32(tmp99, (int)0);
				
				FontName = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp100).ToArray());
				offset += tmp100;
			}
			else
				FontName = null;
			
			// deserialize Text
			byte tmp101;
			tmp101 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp101 == 1)
			{
				byte tmp102;
				tmp102 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp103 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp103, 0, tmp102);
				offset += tmp102;
				uint tmp104;
				tmp104 = BitConverter.ToUInt32(tmp103, (int)0);
				
				Text = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp104).ToArray());
				offset += tmp104;
			}
			else
				Text = null;
			
			// deserialize FontSize
			byte tmp105;
			tmp105 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp105 == 1)
			{
				FontSize = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FontSize = null;
			
			// deserialize Alignment
			byte tmp106;
			tmp106 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp106 == 1)
			{
				short tmp107;
				tmp107 = BitConverter.ToInt16(s, (int)offset);
				offset += sizeof(short);
				Alignment = (ETextAlignmentOption)tmp107;
			}
			else
				Alignment = null;
			
			// deserialize WordWrappingRatios
			byte tmp108;
			tmp108 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp108 == 1)
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
			byte tmp109;
			tmp109 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp109 == 1)
			{
				Value = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Value = null;
			
			// deserialize MaxValue
			byte tmp110;
			tmp110 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp110 == 1)
			{
				MaxValue = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				MaxValue = null;
			
			// deserialize MinValue
			byte tmp111;
			tmp111 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp111 == 1)
			{
				MinValue = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				MinValue = null;
			
			// deserialize Direction
			byte tmp112;
			tmp112 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp112 == 1)
			{
				short tmp113;
				tmp113 = BitConverter.ToInt16(s, (int)offset);
				offset += sizeof(short);
				Direction = (ESliderDirection)tmp113;
			}
			else
				Direction = null;
			
			// deserialize BackgroundColor
			byte tmp114;
			tmp114 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp114 == 1)
			{
				BackgroundColor = new Vector4();
				offset = BackgroundColor.Deserialize(s, offset);
			}
			else
				BackgroundColor = null;
			
			// deserialize FillColor
			byte tmp115;
			tmp115 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp115 == 1)
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
			byte tmp116;
			tmp116 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp116 == 1)
			{
				SpriteAsset = new Asset();
				offset = SpriteAsset.Deserialize(s, offset);
			}
			else
				SpriteAsset = null;
			
			// deserialize Color
			byte tmp117;
			tmp117 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp117 == 1)
			{
				Color = new Vector4();
				offset = Color.Deserialize(s, offset);
			}
			else
				Color = null;
			
			// deserialize MaterialAsset
			byte tmp118;
			tmp118 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp118 == 1)
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
			byte tmp119;
			tmp119 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp119 == 1)
			{
				TextureAsset = new Asset();
				offset = TextureAsset.Deserialize(s, offset);
			}
			else
				TextureAsset = null;
			
			// deserialize MaterialAsset
			byte tmp120;
			tmp120 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp120 == 1)
			{
				MaterialAsset = new Asset();
				offset = MaterialAsset.Deserialize(s, offset);
			}
			else
				MaterialAsset = null;
			
			// deserialize Color
			byte tmp121;
			tmp121 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp121 == 1)
			{
				Color = new Vector4();
				offset = Color.Deserialize(s, offset);
			}
			else
				Color = null;
			
			// deserialize UvRect
			byte tmp122;
			tmp122 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp122 == 1)
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
				List<byte> tmp123 = new List<byte>();
				tmp123.AddRange(BitConverter.GetBytes((uint)SiblingRefAsBaseIndex.Count()));
				while (tmp123.Count > 0 && tmp123.Last() == 0)
					tmp123.RemoveAt(tmp123.Count - 1);
				s.Add((byte)tmp123.Count);
				s.AddRange(tmp123);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(SiblingRefAsBaseIndex));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize NewIndex
			byte tmp124;
			tmp124 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp124 == 1)
			{
				NewIndex = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				NewIndex = null;
			
			// deserialize GotoFirst
			byte tmp125;
			tmp125 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp125 == 1)
			{
				GotoFirst = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				GotoFirst = null;
			
			// deserialize GotoLast
			byte tmp126;
			tmp126 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp126 == 1)
			{
				GotoLast = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				GotoLast = null;
			
			// deserialize ChangeIndex
			byte tmp127;
			tmp127 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp127 == 1)
			{
				ChangeIndex = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				ChangeIndex = null;
			
			// deserialize SiblingRefAsBaseIndex
			byte tmp128;
			tmp128 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp128 == 1)
			{
				byte tmp129;
				tmp129 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp130 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp130, 0, tmp129);
				offset += tmp129;
				uint tmp131;
				tmp131 = BitConverter.ToUInt32(tmp130, (int)0);
				
				SiblingRefAsBaseIndex = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp131).ToArray());
				offset += tmp131;
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
				List<byte> tmp132 = new List<byte>();
				tmp132.AddRange(BitConverter.GetBytes((uint)Type.Count()));
				while (tmp132.Count > 0 && tmp132.Last() == 0)
					tmp132.RemoveAt(tmp132.Count - 1);
				s.Add((byte)tmp132.Count);
				s.AddRange(tmp132);
				
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
			byte tmp133;
			tmp133 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp133 == 1)
			{
				byte tmp134;
				tmp134 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp135 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp135, 0, tmp134);
				offset += tmp134;
				uint tmp136;
				tmp136 = BitConverter.ToUInt32(tmp135, (int)0);
				
				Type = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp136).ToArray());
				offset += tmp136;
			}
			else
				Type = null;
			
			// deserialize Add
			byte tmp137;
			tmp137 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp137 == 1)
			{
				Add = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				Add = null;
			
			// deserialize IsActive
			byte tmp138;
			tmp138 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp138 == 1)
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
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize SpriteAsset
			byte tmp139;
			tmp139 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp139 == 1)
			{
				SpriteAsset = new Asset();
				offset = SpriteAsset.Deserialize(s, offset);
			}
			else
				SpriteAsset = null;
			
			// deserialize Color
			byte tmp140;
			tmp140 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp140 == 1)
			{
				Color = new Vector4();
				offset = Color.Deserialize(s, offset);
			}
			else
				Color = null;
			
			// deserialize FlipX
			byte tmp141;
			tmp141 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp141 == 1)
			{
				FlipX = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				FlipX = null;
			
			// deserialize FlipY
			byte tmp142;
			tmp142 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp142 == 1)
			{
				FlipY = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				FlipY = null;
			
			return offset;
		}
	}
	
	public partial class ChangeRenderer : BaseAction
	{
		public bool? Enabled { get; set; }
		public Asset MaterialAsset { get; set; }
		public int? MaterialIndex { get; set; }
		public uint? RenderingLayerMask { get; set; }
		public int? SortingLayerId { get; set; }
		public int? SortingOrder { get; set; }
		public int? RendererPriority { get; set; }
		

		public ChangeRenderer()
		{
		}
		
		public new const string NameStatic = "ChangeRenderer";
		
		public override string Name() => "ChangeRenderer";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize Enabled
			s.Add((byte)((Enabled == null) ? 0 : 1));
			if (Enabled != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)Enabled));
			}
			
			// serialize MaterialAsset
			s.Add((byte)((MaterialAsset == null) ? 0 : 1));
			if (MaterialAsset != null)
			{
				s.AddRange(MaterialAsset.Serialize());
			}
			
			// serialize MaterialIndex
			s.Add((byte)((MaterialIndex == null) ? 0 : 1));
			if (MaterialIndex != null)
			{
				s.AddRange(BitConverter.GetBytes((int)MaterialIndex));
			}
			
			// serialize RenderingLayerMask
			s.Add((byte)((RenderingLayerMask == null) ? 0 : 1));
			if (RenderingLayerMask != null)
			{
				s.AddRange(BitConverter.GetBytes((uint)RenderingLayerMask));
			}
			
			// serialize SortingLayerId
			s.Add((byte)((SortingLayerId == null) ? 0 : 1));
			if (SortingLayerId != null)
			{
				s.AddRange(BitConverter.GetBytes((int)SortingLayerId));
			}
			
			// serialize SortingOrder
			s.Add((byte)((SortingOrder == null) ? 0 : 1));
			if (SortingOrder != null)
			{
				s.AddRange(BitConverter.GetBytes((int)SortingOrder));
			}
			
			// serialize RendererPriority
			s.Add((byte)((RendererPriority == null) ? 0 : 1));
			if (RendererPriority != null)
			{
				s.AddRange(BitConverter.GetBytes((int)RendererPriority));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize Enabled
			byte tmp143;
			tmp143 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp143 == 1)
			{
				Enabled = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				Enabled = null;
			
			// deserialize MaterialAsset
			byte tmp144;
			tmp144 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp144 == 1)
			{
				MaterialAsset = new Asset();
				offset = MaterialAsset.Deserialize(s, offset);
			}
			else
				MaterialAsset = null;
			
			// deserialize MaterialIndex
			byte tmp145;
			tmp145 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp145 == 1)
			{
				MaterialIndex = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				MaterialIndex = null;
			
			// deserialize RenderingLayerMask
			byte tmp146;
			tmp146 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp146 == 1)
			{
				RenderingLayerMask = BitConverter.ToUInt32(s, (int)offset);
				offset += sizeof(uint);
			}
			else
				RenderingLayerMask = null;
			
			// deserialize SortingLayerId
			byte tmp147;
			tmp147 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp147 == 1)
			{
				SortingLayerId = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				SortingLayerId = null;
			
			// deserialize SortingOrder
			byte tmp148;
			tmp148 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp148 == 1)
			{
				SortingOrder = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				SortingOrder = null;
			
			// deserialize RendererPriority
			byte tmp149;
			tmp149 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp149 == 1)
			{
				RendererPriority = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				RendererPriority = null;
			
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
			byte tmp150;
			tmp150 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp150 == 1)
			{
				FillColor = new Vector4();
				offset = FillColor.Deserialize(s, offset);
			}
			else
				FillColor = null;
			
			// deserialize XRadius
			byte tmp151;
			tmp151 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp151 == 1)
			{
				XRadius = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				XRadius = null;
			
			// deserialize YRadius
			byte tmp152;
			tmp152 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp152 == 1)
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
				List<byte> tmp153 = new List<byte>();
				tmp153.AddRange(BitConverter.GetBytes((uint)Vertices.Count()));
				while (tmp153.Count > 0 && tmp153.Last() == 0)
					tmp153.RemoveAt(tmp153.Count - 1);
				s.Add((byte)tmp153.Count);
				s.AddRange(tmp153);
				
				foreach (var tmp154 in Vertices)
				{
					s.Add((byte)((tmp154 == null) ? 0 : 1));
					if (tmp154 != null)
					{
						s.AddRange(tmp154.Serialize());
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
			byte tmp155;
			tmp155 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp155 == 1)
			{
				FillColor = new Vector4();
				offset = FillColor.Deserialize(s, offset);
			}
			else
				FillColor = null;
			
			// deserialize Vertices
			byte tmp156;
			tmp156 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp156 == 1)
			{
				byte tmp157;
				tmp157 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp158 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp158, 0, tmp157);
				offset += tmp157;
				uint tmp159;
				tmp159 = BitConverter.ToUInt32(tmp158, (int)0);
				
				Vertices = new List<Vector2>();
				for (uint tmp160 = 0; tmp160 < tmp159; tmp160++)
				{
					Vector2 tmp161;
					byte tmp162;
					tmp162 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp162 == 1)
					{
						tmp161 = new Vector2();
						offset = tmp161.Deserialize(s, offset);
					}
					else
						tmp161 = null;
					Vertices.Add(tmp161);
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
				List<byte> tmp163 = new List<byte>();
				tmp163.AddRange(BitConverter.GetBytes((uint)Vertices.Count()));
				while (tmp163.Count > 0 && tmp163.Last() == 0)
					tmp163.RemoveAt(tmp163.Count - 1);
				s.Add((byte)tmp163.Count);
				s.AddRange(tmp163);
				
				foreach (var tmp164 in Vertices)
				{
					s.Add((byte)((tmp164 == null) ? 0 : 1));
					if (tmp164 != null)
					{
						s.AddRange(tmp164.Serialize());
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
			byte tmp165;
			tmp165 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp165 == 1)
			{
				FillColor = new Vector4();
				offset = FillColor.Deserialize(s, offset);
			}
			else
				FillColor = null;
			
			// deserialize Vertices
			byte tmp166;
			tmp166 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp166 == 1)
			{
				byte tmp167;
				tmp167 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp168 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp168, 0, tmp167);
				offset += tmp167;
				uint tmp169;
				tmp169 = BitConverter.ToUInt32(tmp168, (int)0);
				
				Vertices = new List<Vector2>();
				for (uint tmp170 = 0; tmp170 < tmp169; tmp170++)
				{
					Vector2 tmp171;
					byte tmp172;
					tmp172 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp172 == 1)
					{
						tmp171 = new Vector2();
						offset = tmp171.Deserialize(s, offset);
					}
					else
						tmp171 = null;
					Vertices.Add(tmp171);
				}
			}
			else
				Vertices = null;
			
			// deserialize Width
			byte tmp173;
			tmp173 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp173 == 1)
			{
				Width = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Width = null;
			
			// deserialize CornerVertices
			byte tmp174;
			tmp174 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp174 == 1)
			{
				CornerVertices = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				CornerVertices = null;
			
			// deserialize EndCapVertices
			byte tmp175;
			tmp175 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp175 == 1)
			{
				EndCapVertices = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				EndCapVertices = null;
			
			// deserialize Loop
			byte tmp176;
			tmp176 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp176 == 1)
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
		public LayerMask CullingMask { get; set; }
		

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
			
			// serialize CullingMask
			s.Add((byte)((CullingMask == null) ? 0 : 1));
			if (CullingMask != null)
			{
				s.AddRange(CullingMask.Serialize());
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize Type
			byte tmp177;
			tmp177 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp177 == 1)
			{
				sbyte tmp178;
				tmp178 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				Type = (ELightType)tmp178;
			}
			else
				Type = null;
			
			// deserialize Range
			byte tmp179;
			tmp179 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp179 == 1)
			{
				Range = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Range = null;
			
			// deserialize SpotAngle
			byte tmp180;
			tmp180 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp180 == 1)
			{
				SpotAngle = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				SpotAngle = null;
			
			// deserialize Color
			byte tmp181;
			tmp181 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp181 == 1)
			{
				Color = new Vector4();
				offset = Color.Deserialize(s, offset);
			}
			else
				Color = null;
			
			// deserialize Intensity
			byte tmp182;
			tmp182 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp182 == 1)
			{
				Intensity = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				Intensity = null;
			
			// deserialize IndirectMultiplier
			byte tmp183;
			tmp183 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp183 == 1)
			{
				IndirectMultiplier = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				IndirectMultiplier = null;
			
			// deserialize ShadowType
			byte tmp184;
			tmp184 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp184 == 1)
			{
				sbyte tmp185;
				tmp185 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				ShadowType = (ELightShadowType)tmp185;
			}
			else
				ShadowType = null;
			
			// deserialize ShadowStrength
			byte tmp186;
			tmp186 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp186 == 1)
			{
				ShadowStrength = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				ShadowStrength = null;
			
			// deserialize ShadowBias
			byte tmp187;
			tmp187 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp187 == 1)
			{
				ShadowBias = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				ShadowBias = null;
			
			// deserialize ShadowNormalBias
			byte tmp188;
			tmp188 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp188 == 1)
			{
				ShadowNormalBias = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				ShadowNormalBias = null;
			
			// deserialize ShadowNearPlane
			byte tmp189;
			tmp189 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp189 == 1)
			{
				ShadowNearPlane = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				ShadowNearPlane = null;
			
			// deserialize CookieAsset
			byte tmp190;
			tmp190 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp190 == 1)
			{
				CookieAsset = new Asset();
				offset = CookieAsset.Deserialize(s, offset);
			}
			else
				CookieAsset = null;
			
			// deserialize CookieSize
			byte tmp191;
			tmp191 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp191 == 1)
			{
				CookieSize = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				CookieSize = null;
			
			// deserialize FlareAsset
			byte tmp192;
			tmp192 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp192 == 1)
			{
				FlareAsset = new Asset();
				offset = FlareAsset.Deserialize(s, offset);
			}
			else
				FlareAsset = null;
			
			// deserialize CullingMask
			byte tmp193;
			tmp193 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp193 == 1)
			{
				CullingMask = new LayerMask();
				offset = CullingMask.Deserialize(s, offset);
			}
			else
				CullingMask = null;
			
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
		public LayerMask CullingMask { get; set; }
		public bool? IsOrthographic { get; set; }
		public float? OrthographicSize { get; set; }
		public float? FieldOfView { get; set; }
		public float? NearClipPlane { get; set; }
		public float? FarClipPlane { get; set; }
		public Vector3 MinPosition { get; set; }
		public Vector3 MaxPosition { get; set; }
		public Vector2 MinRotation { get; set; }
		public Vector2 MaxRotation { get; set; }
		public float? MinZoom { get; set; }
		public float? MaxZoom { get; set; }
		public Asset PostProcessProfileAsset { get; set; }
		public LayerMask PostProcessLayers { get; set; }
		

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
			
			// serialize CullingMask
			s.Add((byte)((CullingMask == null) ? 0 : 1));
			if (CullingMask != null)
			{
				s.AddRange(CullingMask.Serialize());
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
			
			// serialize MinZoom
			s.Add((byte)((MinZoom == null) ? 0 : 1));
			if (MinZoom != null)
			{
				s.AddRange(BitConverter.GetBytes((float)MinZoom));
			}
			
			// serialize MaxZoom
			s.Add((byte)((MaxZoom == null) ? 0 : 1));
			if (MaxZoom != null)
			{
				s.AddRange(BitConverter.GetBytes((float)MaxZoom));
			}
			
			// serialize PostProcessProfileAsset
			s.Add((byte)((PostProcessProfileAsset == null) ? 0 : 1));
			if (PostProcessProfileAsset != null)
			{
				s.AddRange(PostProcessProfileAsset.Serialize());
			}
			
			// serialize PostProcessLayers
			s.Add((byte)((PostProcessLayers == null) ? 0 : 1));
			if (PostProcessLayers != null)
			{
				s.AddRange(PostProcessLayers.Serialize());
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize ClearFlag
			byte tmp194;
			tmp194 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp194 == 1)
			{
				sbyte tmp195;
				tmp195 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				ClearFlag = (ECameraClearFlag)tmp195;
			}
			else
				ClearFlag = null;
			
			// deserialize BackgroundColor
			byte tmp196;
			tmp196 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp196 == 1)
			{
				BackgroundColor = new Vector4();
				offset = BackgroundColor.Deserialize(s, offset);
			}
			else
				BackgroundColor = null;
			
			// deserialize CullingMask
			byte tmp197;
			tmp197 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp197 == 1)
			{
				CullingMask = new LayerMask();
				offset = CullingMask.Deserialize(s, offset);
			}
			else
				CullingMask = null;
			
			// deserialize IsOrthographic
			byte tmp198;
			tmp198 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp198 == 1)
			{
				IsOrthographic = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				IsOrthographic = null;
			
			// deserialize OrthographicSize
			byte tmp199;
			tmp199 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp199 == 1)
			{
				OrthographicSize = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				OrthographicSize = null;
			
			// deserialize FieldOfView
			byte tmp200;
			tmp200 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp200 == 1)
			{
				FieldOfView = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FieldOfView = null;
			
			// deserialize NearClipPlane
			byte tmp201;
			tmp201 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp201 == 1)
			{
				NearClipPlane = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				NearClipPlane = null;
			
			// deserialize FarClipPlane
			byte tmp202;
			tmp202 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp202 == 1)
			{
				FarClipPlane = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FarClipPlane = null;
			
			// deserialize MinPosition
			byte tmp203;
			tmp203 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp203 == 1)
			{
				MinPosition = new Vector3();
				offset = MinPosition.Deserialize(s, offset);
			}
			else
				MinPosition = null;
			
			// deserialize MaxPosition
			byte tmp204;
			tmp204 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp204 == 1)
			{
				MaxPosition = new Vector3();
				offset = MaxPosition.Deserialize(s, offset);
			}
			else
				MaxPosition = null;
			
			// deserialize MinRotation
			byte tmp205;
			tmp205 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp205 == 1)
			{
				MinRotation = new Vector2();
				offset = MinRotation.Deserialize(s, offset);
			}
			else
				MinRotation = null;
			
			// deserialize MaxRotation
			byte tmp206;
			tmp206 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp206 == 1)
			{
				MaxRotation = new Vector2();
				offset = MaxRotation.Deserialize(s, offset);
			}
			else
				MaxRotation = null;
			
			// deserialize MinZoom
			byte tmp207;
			tmp207 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp207 == 1)
			{
				MinZoom = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				MinZoom = null;
			
			// deserialize MaxZoom
			byte tmp208;
			tmp208 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp208 == 1)
			{
				MaxZoom = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				MaxZoom = null;
			
			// deserialize PostProcessProfileAsset
			byte tmp209;
			tmp209 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp209 == 1)
			{
				PostProcessProfileAsset = new Asset();
				offset = PostProcessProfileAsset.Deserialize(s, offset);
			}
			else
				PostProcessProfileAsset = null;
			
			// deserialize PostProcessLayers
			byte tmp210;
			tmp210 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp210 == 1)
			{
				PostProcessLayers = new LayerMask();
				offset = PostProcessLayers.Deserialize(s, offset);
			}
			else
				PostProcessLayers = null;
			
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
	
	public partial class ChangeRenderSettings : BaseAction
	{
		public bool? BackwardChanges { get; set; }
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
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize BackwardChanges
			s.Add((byte)((BackwardChanges == null) ? 0 : 1));
			if (BackwardChanges != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)BackwardChanges));
			}
			
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
				List<byte> tmp211 = new List<byte>();
				tmp211.AddRange(BitConverter.GetBytes((uint)SunChildRef.Count()));
				while (tmp211.Count > 0 && tmp211.Last() == 0)
					tmp211.RemoveAt(tmp211.Count - 1);
				s.Add((byte)tmp211.Count);
				s.AddRange(tmp211);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(SunChildRef));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize BackwardChanges
			byte tmp212;
			tmp212 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp212 == 1)
			{
				BackwardChanges = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				BackwardChanges = null;
			
			// deserialize AmbientEquatorColor
			byte tmp213;
			tmp213 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp213 == 1)
			{
				AmbientEquatorColor = new Vector4();
				offset = AmbientEquatorColor.Deserialize(s, offset);
			}
			else
				AmbientEquatorColor = null;
			
			// deserialize AmbientGroundColor
			byte tmp214;
			tmp214 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp214 == 1)
			{
				AmbientGroundColor = new Vector4();
				offset = AmbientGroundColor.Deserialize(s, offset);
			}
			else
				AmbientGroundColor = null;
			
			// deserialize AmbientIntensity
			byte tmp215;
			tmp215 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp215 == 1)
			{
				AmbientIntensity = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				AmbientIntensity = null;
			
			// deserialize AmbientLight
			byte tmp216;
			tmp216 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp216 == 1)
			{
				AmbientLight = new Vector4();
				offset = AmbientLight.Deserialize(s, offset);
			}
			else
				AmbientLight = null;
			
			// deserialize AmbientMode
			byte tmp217;
			tmp217 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp217 == 1)
			{
				sbyte tmp218;
				tmp218 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				AmbientMode = (EAmbientMode)tmp218;
			}
			else
				AmbientMode = null;
			
			// deserialize AmbientSkyColor
			byte tmp219;
			tmp219 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp219 == 1)
			{
				AmbientSkyColor = new Vector4();
				offset = AmbientSkyColor.Deserialize(s, offset);
			}
			else
				AmbientSkyColor = null;
			
			// deserialize CustomReflectionAsset
			byte tmp220;
			tmp220 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp220 == 1)
			{
				CustomReflectionAsset = new Asset();
				offset = CustomReflectionAsset.Deserialize(s, offset);
			}
			else
				CustomReflectionAsset = null;
			
			// deserialize DefaultReflectionMode
			byte tmp221;
			tmp221 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp221 == 1)
			{
				sbyte tmp222;
				tmp222 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				DefaultReflectionMode = (EDefaultReflectionMode)tmp222;
			}
			else
				DefaultReflectionMode = null;
			
			// deserialize DefaultReflectionResolution
			byte tmp223;
			tmp223 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp223 == 1)
			{
				DefaultReflectionResolution = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				DefaultReflectionResolution = null;
			
			// deserialize FlareFadeSpeed
			byte tmp224;
			tmp224 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp224 == 1)
			{
				FlareFadeSpeed = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FlareFadeSpeed = null;
			
			// deserialize FlareStrength
			byte tmp225;
			tmp225 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp225 == 1)
			{
				FlareStrength = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FlareStrength = null;
			
			// deserialize HasFog
			byte tmp226;
			tmp226 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp226 == 1)
			{
				HasFog = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				HasFog = null;
			
			// deserialize FogMode
			byte tmp227;
			tmp227 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp227 == 1)
			{
				sbyte tmp228;
				tmp228 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				FogMode = (EFogMode)tmp228;
			}
			else
				FogMode = null;
			
			// deserialize FogColor
			byte tmp229;
			tmp229 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp229 == 1)
			{
				FogColor = new Vector4();
				offset = FogColor.Deserialize(s, offset);
			}
			else
				FogColor = null;
			
			// deserialize FogDensity
			byte tmp230;
			tmp230 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp230 == 1)
			{
				FogDensity = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FogDensity = null;
			
			// deserialize FogStartDistance
			byte tmp231;
			tmp231 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp231 == 1)
			{
				FogStartDistance = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FogStartDistance = null;
			
			// deserialize FogEndDistance
			byte tmp232;
			tmp232 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp232 == 1)
			{
				FogEndDistance = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FogEndDistance = null;
			
			// deserialize HaloStrength
			byte tmp233;
			tmp233 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp233 == 1)
			{
				HaloStrength = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				HaloStrength = null;
			
			// deserialize ReflectionBounces
			byte tmp234;
			tmp234 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp234 == 1)
			{
				ReflectionBounces = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				ReflectionBounces = null;
			
			// deserialize ReflectionIntensity
			byte tmp235;
			tmp235 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp235 == 1)
			{
				ReflectionIntensity = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				ReflectionIntensity = null;
			
			// deserialize SkyboxAsset
			byte tmp236;
			tmp236 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp236 == 1)
			{
				SkyboxAsset = new Asset();
				offset = SkyboxAsset.Deserialize(s, offset);
			}
			else
				SkyboxAsset = null;
			
			// deserialize SubtractiveShadowColor
			byte tmp237;
			tmp237 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp237 == 1)
			{
				SubtractiveShadowColor = new Vector4();
				offset = SubtractiveShadowColor.Deserialize(s, offset);
			}
			else
				SubtractiveShadowColor = null;
			
			// deserialize SunRef
			byte tmp238;
			tmp238 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp238 == 1)
			{
				SunRef = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				SunRef = null;
			
			// deserialize SunChildRef
			byte tmp239;
			tmp239 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp239 == 1)
			{
				byte tmp240;
				tmp240 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp241 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp241, 0, tmp240);
				offset += tmp240;
				uint tmp242;
				tmp242 = BitConverter.ToUInt32(tmp241, (int)0);
				
				SunChildRef = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp242).ToArray());
				offset += tmp242;
			}
			else
				SunChildRef = null;
			
			return offset;
		}
	}
	
	public enum EParadoxGraphType
	{
		Flow = 0,
		BehaviourTree = 1,
		FSM = 2,
	}
	
	public partial class ChangeParadoxGraph : BaseAction
	{
		public EParadoxGraphType? Type { get; set; }
		public Asset GraphAsset { get; set; }
		public bool? Play { get; set; }
		public bool? Stop { get; set; }
		public bool? Restart { get; set; }
		

		public ChangeParadoxGraph()
		{
		}
		
		public new const string NameStatic = "ChangeParadoxGraph";
		
		public override string Name() => "ChangeParadoxGraph";
		
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
			
			// serialize GraphAsset
			s.Add((byte)((GraphAsset == null) ? 0 : 1));
			if (GraphAsset != null)
			{
				s.AddRange(GraphAsset.Serialize());
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
			
			// serialize Restart
			s.Add((byte)((Restart == null) ? 0 : 1));
			if (Restart != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)Restart));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize Type
			byte tmp243;
			tmp243 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp243 == 1)
			{
				sbyte tmp244;
				tmp244 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				Type = (EParadoxGraphType)tmp244;
			}
			else
				Type = null;
			
			// deserialize GraphAsset
			byte tmp245;
			tmp245 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp245 == 1)
			{
				GraphAsset = new Asset();
				offset = GraphAsset.Deserialize(s, offset);
			}
			else
				GraphAsset = null;
			
			// deserialize Play
			byte tmp246;
			tmp246 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp246 == 1)
			{
				Play = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				Play = null;
			
			// deserialize Stop
			byte tmp247;
			tmp247 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp247 == 1)
			{
				Stop = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				Stop = null;
			
			// deserialize Restart
			byte tmp248;
			tmp248 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp248 == 1)
			{
				Restart = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				Restart = null;
			
			return offset;
		}
	}
	
	public partial class ChangeParadoxBehaviourTree : BaseAction
	{
		public bool? Repeat { get; set; }
		public float? UpdateInterval { get; set; }
		

		public ChangeParadoxBehaviourTree()
		{
		}
		
		public new const string NameStatic = "ChangeParadoxBehaviourTree";
		
		public override string Name() => "ChangeParadoxBehaviourTree";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize Repeat
			s.Add((byte)((Repeat == null) ? 0 : 1));
			if (Repeat != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)Repeat));
			}
			
			// serialize UpdateInterval
			s.Add((byte)((UpdateInterval == null) ? 0 : 1));
			if (UpdateInterval != null)
			{
				s.AddRange(BitConverter.GetBytes((float)UpdateInterval));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize Repeat
			byte tmp249;
			tmp249 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp249 == 1)
			{
				Repeat = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				Repeat = null;
			
			// deserialize UpdateInterval
			byte tmp250;
			tmp250 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp250 == 1)
			{
				UpdateInterval = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				UpdateInterval = null;
			
			return offset;
		}
	}
	
	public partial class ChangeParadoxFSM : BaseAction
	{
		public List<string> TriggerStatesName { get; set; }
		public List<bool?> HardTrigger { get; set; }
		

		public ChangeParadoxFSM()
		{
		}
		
		public new const string NameStatic = "ChangeParadoxFSM";
		
		public override string Name() => "ChangeParadoxFSM";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize TriggerStatesName
			s.Add((byte)((TriggerStatesName == null) ? 0 : 1));
			if (TriggerStatesName != null)
			{
				List<byte> tmp251 = new List<byte>();
				tmp251.AddRange(BitConverter.GetBytes((uint)TriggerStatesName.Count()));
				while (tmp251.Count > 0 && tmp251.Last() == 0)
					tmp251.RemoveAt(tmp251.Count - 1);
				s.Add((byte)tmp251.Count);
				s.AddRange(tmp251);
				
				foreach (var tmp252 in TriggerStatesName)
				{
					s.Add((byte)((tmp252 == null) ? 0 : 1));
					if (tmp252 != null)
					{
						List<byte> tmp253 = new List<byte>();
						tmp253.AddRange(BitConverter.GetBytes((uint)tmp252.Count()));
						while (tmp253.Count > 0 && tmp253.Last() == 0)
							tmp253.RemoveAt(tmp253.Count - 1);
						s.Add((byte)tmp253.Count);
						s.AddRange(tmp253);
						
						s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(tmp252));
					}
				}
			}
			
			// serialize HardTrigger
			s.Add((byte)((HardTrigger == null) ? 0 : 1));
			if (HardTrigger != null)
			{
				List<byte> tmp254 = new List<byte>();
				tmp254.AddRange(BitConverter.GetBytes((uint)HardTrigger.Count()));
				while (tmp254.Count > 0 && tmp254.Last() == 0)
					tmp254.RemoveAt(tmp254.Count - 1);
				s.Add((byte)tmp254.Count);
				s.AddRange(tmp254);
				
				foreach (var tmp255 in HardTrigger)
				{
					s.Add((byte)((tmp255 == null) ? 0 : 1));
					if (tmp255 != null)
					{
						s.AddRange(BitConverter.GetBytes((bool)tmp255));
					}
				}
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize TriggerStatesName
			byte tmp256;
			tmp256 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp256 == 1)
			{
				byte tmp257;
				tmp257 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp258 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp258, 0, tmp257);
				offset += tmp257;
				uint tmp259;
				tmp259 = BitConverter.ToUInt32(tmp258, (int)0);
				
				TriggerStatesName = new List<string>();
				for (uint tmp260 = 0; tmp260 < tmp259; tmp260++)
				{
					string tmp261;
					byte tmp262;
					tmp262 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp262 == 1)
					{
						byte tmp263;
						tmp263 = (byte)s[(int)offset];
						offset += sizeof(byte);
						byte[] tmp264 = new byte[sizeof(uint)];
						Array.Copy(s, offset, tmp264, 0, tmp263);
						offset += tmp263;
						uint tmp265;
						tmp265 = BitConverter.ToUInt32(tmp264, (int)0);
						
						tmp261 = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp265).ToArray());
						offset += tmp265;
					}
					else
						tmp261 = null;
					TriggerStatesName.Add(tmp261);
				}
			}
			else
				TriggerStatesName = null;
			
			// deserialize HardTrigger
			byte tmp266;
			tmp266 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp266 == 1)
			{
				byte tmp267;
				tmp267 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp268 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp268, 0, tmp267);
				offset += tmp267;
				uint tmp269;
				tmp269 = BitConverter.ToUInt32(tmp268, (int)0);
				
				HardTrigger = new List<bool?>();
				for (uint tmp270 = 0; tmp270 < tmp269; tmp270++)
				{
					bool? tmp271;
					byte tmp272;
					tmp272 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp272 == 1)
					{
						tmp271 = BitConverter.ToBoolean(s, (int)offset);
						offset += sizeof(bool);
					}
					else
						tmp271 = null;
					HardTrigger.Add(tmp271);
				}
			}
			else
				HardTrigger = null;
			
			return offset;
		}
	}
	
	public enum EParadoxBlackboardVariableType
	{
		Simple = 0,
		List = 1,
		Dictionary = 2,
	}
	
	public enum EParadoxBlackboardOperationType
	{
		Edit = 0,
		Add = 1,
		Remove = 2,
	}
	
	public enum EParadoxBlackboardValueType
	{
		Int = 0,
		Float = 1,
		Bool = 2,
		String = 3,
		GameObject = 4,
		Vector2 = 5,
		Vector3 = 6,
		Vector4 = 7,
		Color = 8,
		LayerMask = 9,
		Asset = 10,
	}
	
	public partial class ChangeParadoxBlackboard : BaseAction
	{
		public string VarName { get; set; }
		public EParadoxBlackboardValueType? ValueType { get; set; }
		public EParadoxBlackboardVariableType? VarType { get; set; }
		public EParadoxBlackboardOperationType? OpType { get; set; }
		public int? ListIndex { get; set; }
		public string DictionaryKey { get; set; }
		public int? IntValue { get; set; }
		public float? FloatValue { get; set; }
		public bool? BoolValue { get; set; }
		public string StringValue { get; set; }
		public int? GameObjectRef { get; set; }
		public string GameObjectChildRef { get; set; }
		public Vector4 VectorValue { get; set; }
		public LayerMask LayerMaskValue { get; set; }
		public string AssetType { get; set; }
		public Asset AssetValue { get; set; }
		

		public ChangeParadoxBlackboard()
		{
		}
		
		public new const string NameStatic = "ChangeParadoxBlackboard";
		
		public override string Name() => "ChangeParadoxBlackboard";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize parents
			s.AddRange(base.Serialize());
			
			// serialize VarName
			s.Add((byte)((VarName == null) ? 0 : 1));
			if (VarName != null)
			{
				List<byte> tmp273 = new List<byte>();
				tmp273.AddRange(BitConverter.GetBytes((uint)VarName.Count()));
				while (tmp273.Count > 0 && tmp273.Last() == 0)
					tmp273.RemoveAt(tmp273.Count - 1);
				s.Add((byte)tmp273.Count);
				s.AddRange(tmp273);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(VarName));
			}
			
			// serialize ValueType
			s.Add((byte)((ValueType == null) ? 0 : 1));
			if (ValueType != null)
			{
				s.Add((byte)((sbyte)ValueType));
			}
			
			// serialize VarType
			s.Add((byte)((VarType == null) ? 0 : 1));
			if (VarType != null)
			{
				s.Add((byte)((sbyte)VarType));
			}
			
			// serialize OpType
			s.Add((byte)((OpType == null) ? 0 : 1));
			if (OpType != null)
			{
				s.Add((byte)((sbyte)OpType));
			}
			
			// serialize ListIndex
			s.Add((byte)((ListIndex == null) ? 0 : 1));
			if (ListIndex != null)
			{
				s.AddRange(BitConverter.GetBytes((int)ListIndex));
			}
			
			// serialize DictionaryKey
			s.Add((byte)((DictionaryKey == null) ? 0 : 1));
			if (DictionaryKey != null)
			{
				List<byte> tmp274 = new List<byte>();
				tmp274.AddRange(BitConverter.GetBytes((uint)DictionaryKey.Count()));
				while (tmp274.Count > 0 && tmp274.Last() == 0)
					tmp274.RemoveAt(tmp274.Count - 1);
				s.Add((byte)tmp274.Count);
				s.AddRange(tmp274);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(DictionaryKey));
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
			
			// serialize StringValue
			s.Add((byte)((StringValue == null) ? 0 : 1));
			if (StringValue != null)
			{
				List<byte> tmp275 = new List<byte>();
				tmp275.AddRange(BitConverter.GetBytes((uint)StringValue.Count()));
				while (tmp275.Count > 0 && tmp275.Last() == 0)
					tmp275.RemoveAt(tmp275.Count - 1);
				s.Add((byte)tmp275.Count);
				s.AddRange(tmp275);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(StringValue));
			}
			
			// serialize GameObjectRef
			s.Add((byte)((GameObjectRef == null) ? 0 : 1));
			if (GameObjectRef != null)
			{
				s.AddRange(BitConverter.GetBytes((int)GameObjectRef));
			}
			
			// serialize GameObjectChildRef
			s.Add((byte)((GameObjectChildRef == null) ? 0 : 1));
			if (GameObjectChildRef != null)
			{
				List<byte> tmp276 = new List<byte>();
				tmp276.AddRange(BitConverter.GetBytes((uint)GameObjectChildRef.Count()));
				while (tmp276.Count > 0 && tmp276.Last() == 0)
					tmp276.RemoveAt(tmp276.Count - 1);
				s.Add((byte)tmp276.Count);
				s.AddRange(tmp276);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(GameObjectChildRef));
			}
			
			// serialize VectorValue
			s.Add((byte)((VectorValue == null) ? 0 : 1));
			if (VectorValue != null)
			{
				s.AddRange(VectorValue.Serialize());
			}
			
			// serialize LayerMaskValue
			s.Add((byte)((LayerMaskValue == null) ? 0 : 1));
			if (LayerMaskValue != null)
			{
				s.AddRange(LayerMaskValue.Serialize());
			}
			
			// serialize AssetType
			s.Add((byte)((AssetType == null) ? 0 : 1));
			if (AssetType != null)
			{
				List<byte> tmp277 = new List<byte>();
				tmp277.AddRange(BitConverter.GetBytes((uint)AssetType.Count()));
				while (tmp277.Count > 0 && tmp277.Last() == 0)
					tmp277.RemoveAt(tmp277.Count - 1);
				s.Add((byte)tmp277.Count);
				s.AddRange(tmp277);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(AssetType));
			}
			
			// serialize AssetValue
			s.Add((byte)((AssetValue == null) ? 0 : 1));
			if (AssetValue != null)
			{
				s.AddRange(AssetValue.Serialize());
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize parents
			offset = base.Deserialize(s, offset);
			
			// deserialize VarName
			byte tmp278;
			tmp278 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp278 == 1)
			{
				byte tmp279;
				tmp279 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp280 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp280, 0, tmp279);
				offset += tmp279;
				uint tmp281;
				tmp281 = BitConverter.ToUInt32(tmp280, (int)0);
				
				VarName = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp281).ToArray());
				offset += tmp281;
			}
			else
				VarName = null;
			
			// deserialize ValueType
			byte tmp282;
			tmp282 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp282 == 1)
			{
				sbyte tmp283;
				tmp283 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				ValueType = (EParadoxBlackboardValueType)tmp283;
			}
			else
				ValueType = null;
			
			// deserialize VarType
			byte tmp284;
			tmp284 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp284 == 1)
			{
				sbyte tmp285;
				tmp285 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				VarType = (EParadoxBlackboardVariableType)tmp285;
			}
			else
				VarType = null;
			
			// deserialize OpType
			byte tmp286;
			tmp286 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp286 == 1)
			{
				sbyte tmp287;
				tmp287 = (sbyte)s[(int)offset];
				offset += sizeof(sbyte);
				OpType = (EParadoxBlackboardOperationType)tmp287;
			}
			else
				OpType = null;
			
			// deserialize ListIndex
			byte tmp288;
			tmp288 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp288 == 1)
			{
				ListIndex = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				ListIndex = null;
			
			// deserialize DictionaryKey
			byte tmp289;
			tmp289 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp289 == 1)
			{
				byte tmp290;
				tmp290 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp291 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp291, 0, tmp290);
				offset += tmp290;
				uint tmp292;
				tmp292 = BitConverter.ToUInt32(tmp291, (int)0);
				
				DictionaryKey = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp292).ToArray());
				offset += tmp292;
			}
			else
				DictionaryKey = null;
			
			// deserialize IntValue
			byte tmp293;
			tmp293 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp293 == 1)
			{
				IntValue = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				IntValue = null;
			
			// deserialize FloatValue
			byte tmp294;
			tmp294 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp294 == 1)
			{
				FloatValue = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				FloatValue = null;
			
			// deserialize BoolValue
			byte tmp295;
			tmp295 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp295 == 1)
			{
				BoolValue = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				BoolValue = null;
			
			// deserialize StringValue
			byte tmp296;
			tmp296 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp296 == 1)
			{
				byte tmp297;
				tmp297 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp298 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp298, 0, tmp297);
				offset += tmp297;
				uint tmp299;
				tmp299 = BitConverter.ToUInt32(tmp298, (int)0);
				
				StringValue = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp299).ToArray());
				offset += tmp299;
			}
			else
				StringValue = null;
			
			// deserialize GameObjectRef
			byte tmp300;
			tmp300 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp300 == 1)
			{
				GameObjectRef = BitConverter.ToInt32(s, (int)offset);
				offset += sizeof(int);
			}
			else
				GameObjectRef = null;
			
			// deserialize GameObjectChildRef
			byte tmp301;
			tmp301 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp301 == 1)
			{
				byte tmp302;
				tmp302 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp303 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp303, 0, tmp302);
				offset += tmp302;
				uint tmp304;
				tmp304 = BitConverter.ToUInt32(tmp303, (int)0);
				
				GameObjectChildRef = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp304).ToArray());
				offset += tmp304;
			}
			else
				GameObjectChildRef = null;
			
			// deserialize VectorValue
			byte tmp305;
			tmp305 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp305 == 1)
			{
				VectorValue = new Vector4();
				offset = VectorValue.Deserialize(s, offset);
			}
			else
				VectorValue = null;
			
			// deserialize LayerMaskValue
			byte tmp306;
			tmp306 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp306 == 1)
			{
				LayerMaskValue = new LayerMask();
				offset = LayerMaskValue.Deserialize(s, offset);
			}
			else
				LayerMaskValue = null;
			
			// deserialize AssetType
			byte tmp307;
			tmp307 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp307 == 1)
			{
				byte tmp308;
				tmp308 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp309 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp309, 0, tmp308);
				offset += tmp308;
				uint tmp310;
				tmp310 = BitConverter.ToUInt32(tmp309, (int)0);
				
				AssetType = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp310).ToArray());
				offset += tmp310;
			}
			else
				AssetType = null;
			
			// deserialize AssetValue
			byte tmp311;
			tmp311 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp311 == 1)
			{
				AssetValue = new Asset();
				offset = AssetValue.Deserialize(s, offset);
			}
			else
				AssetValue = null;
			
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
} // namespace KS.SceneActions
