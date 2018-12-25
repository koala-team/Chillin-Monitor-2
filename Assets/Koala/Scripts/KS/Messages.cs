using System;
using System.Linq;
using System.Collections.Generic;

namespace KS.Messages
{
	public partial class Message : KSObject
	{
		public string Type { get; set; }
		public string Payload { get; set; }
		

		public Message()
		{
		}
		
		public new const string NameStatic = "Message";
		
		public override string Name() => "Message";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize Type
			s.Add((byte)((Type == null) ? 0 : 1));
			if (Type != null)
			{
				List<byte> tmp0 = new List<byte>();
				tmp0.AddRange(BitConverter.GetBytes((uint)Type.Count()));
				while (tmp0.Count > 0 && tmp0.Last() == 0)
					tmp0.RemoveAt(tmp0.Count - 1);
				s.Add((byte)tmp0.Count);
				s.AddRange(tmp0);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(Type));
			}
			
			// serialize Payload
			s.Add((byte)((Payload == null) ? 0 : 1));
			if (Payload != null)
			{
				List<byte> tmp1 = new List<byte>();
				tmp1.AddRange(BitConverter.GetBytes((uint)Payload.Count()));
				while (tmp1.Count > 0 && tmp1.Last() == 0)
					tmp1.RemoveAt(tmp1.Count - 1);
				s.Add((byte)tmp1.Count);
				s.AddRange(tmp1);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(Payload));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize Type
			byte tmp2;
			tmp2 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp2 == 1)
			{
				byte tmp3;
				tmp3 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp4 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp4, 0, tmp3);
				offset += tmp3;
				uint tmp5;
				tmp5 = BitConverter.ToUInt32(tmp4, (int)0);
				
				Type = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp5).ToArray());
				offset += tmp5;
			}
			else
				Type = null;
			
			// deserialize Payload
			byte tmp6;
			tmp6 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp6 == 1)
			{
				byte tmp7;
				tmp7 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp8 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp8, 0, tmp7);
				offset += tmp7;
				uint tmp9;
				tmp9 = BitConverter.ToUInt32(tmp8, (int)0);
				
				Payload = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp9).ToArray());
				offset += tmp9;
			}
			else
				Payload = null;
			
			return offset;
		}
	}
	
	public partial class Auth : KSObject
	{
		public bool? Authenticated { get; set; }
		

		public Auth()
		{
		}
		
		public new const string NameStatic = "Auth";
		
		public override string Name() => "Auth";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize Authenticated
			s.Add((byte)((Authenticated == null) ? 0 : 1));
			if (Authenticated != null)
			{
				s.AddRange(BitConverter.GetBytes((bool)Authenticated));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize Authenticated
			byte tmp10;
			tmp10 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp10 == 1)
			{
				Authenticated = BitConverter.ToBoolean(s, (int)offset);
				offset += sizeof(bool);
			}
			else
				Authenticated = null;
			
			return offset;
		}
	}
	
	public partial class GameInfo : KSObject
	{
		public string GameName { get; set; }
		public Dictionary<string, List<string>> Sides { get; set; }
		public float? GuiCycleDuration { get; set; }
		public Dictionary<string, string> GuiSideColors { get; set; }
		

		public GameInfo()
		{
		}
		
		public new const string NameStatic = "GameInfo";
		
		public override string Name() => "GameInfo";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize GameName
			s.Add((byte)((GameName == null) ? 0 : 1));
			if (GameName != null)
			{
				List<byte> tmp11 = new List<byte>();
				tmp11.AddRange(BitConverter.GetBytes((uint)GameName.Count()));
				while (tmp11.Count > 0 && tmp11.Last() == 0)
					tmp11.RemoveAt(tmp11.Count - 1);
				s.Add((byte)tmp11.Count);
				s.AddRange(tmp11);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(GameName));
			}
			
			// serialize Sides
			s.Add((byte)((Sides == null) ? 0 : 1));
			if (Sides != null)
			{
				List<byte> tmp12 = new List<byte>();
				tmp12.AddRange(BitConverter.GetBytes((uint)Sides.Count()));
				while (tmp12.Count > 0 && tmp12.Last() == 0)
					tmp12.RemoveAt(tmp12.Count - 1);
				s.Add((byte)tmp12.Count);
				s.AddRange(tmp12);
				
				foreach (var tmp13 in Sides)
				{
					s.Add((byte)((tmp13.Key == null) ? 0 : 1));
					if (tmp13.Key != null)
					{
						List<byte> tmp14 = new List<byte>();
						tmp14.AddRange(BitConverter.GetBytes((uint)tmp13.Key.Count()));
						while (tmp14.Count > 0 && tmp14.Last() == 0)
							tmp14.RemoveAt(tmp14.Count - 1);
						s.Add((byte)tmp14.Count);
						s.AddRange(tmp14);
						
						s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(tmp13.Key));
					}
					
					s.Add((byte)((tmp13.Value == null) ? 0 : 1));
					if (tmp13.Value != null)
					{
						List<byte> tmp15 = new List<byte>();
						tmp15.AddRange(BitConverter.GetBytes((uint)tmp13.Value.Count()));
						while (tmp15.Count > 0 && tmp15.Last() == 0)
							tmp15.RemoveAt(tmp15.Count - 1);
						s.Add((byte)tmp15.Count);
						s.AddRange(tmp15);
						
						foreach (var tmp16 in tmp13.Value)
						{
							s.Add((byte)((tmp16 == null) ? 0 : 1));
							if (tmp16 != null)
							{
								List<byte> tmp17 = new List<byte>();
								tmp17.AddRange(BitConverter.GetBytes((uint)tmp16.Count()));
								while (tmp17.Count > 0 && tmp17.Last() == 0)
									tmp17.RemoveAt(tmp17.Count - 1);
								s.Add((byte)tmp17.Count);
								s.AddRange(tmp17);
								
								s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(tmp16));
							}
						}
					}
				}
			}
			
			// serialize GuiCycleDuration
			s.Add((byte)((GuiCycleDuration == null) ? 0 : 1));
			if (GuiCycleDuration != null)
			{
				s.AddRange(BitConverter.GetBytes((float)GuiCycleDuration));
			}
			
			// serialize GuiSideColors
			s.Add((byte)((GuiSideColors == null) ? 0 : 1));
			if (GuiSideColors != null)
			{
				List<byte> tmp18 = new List<byte>();
				tmp18.AddRange(BitConverter.GetBytes((uint)GuiSideColors.Count()));
				while (tmp18.Count > 0 && tmp18.Last() == 0)
					tmp18.RemoveAt(tmp18.Count - 1);
				s.Add((byte)tmp18.Count);
				s.AddRange(tmp18);
				
				foreach (var tmp19 in GuiSideColors)
				{
					s.Add((byte)((tmp19.Key == null) ? 0 : 1));
					if (tmp19.Key != null)
					{
						List<byte> tmp20 = new List<byte>();
						tmp20.AddRange(BitConverter.GetBytes((uint)tmp19.Key.Count()));
						while (tmp20.Count > 0 && tmp20.Last() == 0)
							tmp20.RemoveAt(tmp20.Count - 1);
						s.Add((byte)tmp20.Count);
						s.AddRange(tmp20);
						
						s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(tmp19.Key));
					}
					
					s.Add((byte)((tmp19.Value == null) ? 0 : 1));
					if (tmp19.Value != null)
					{
						List<byte> tmp21 = new List<byte>();
						tmp21.AddRange(BitConverter.GetBytes((uint)tmp19.Value.Count()));
						while (tmp21.Count > 0 && tmp21.Last() == 0)
							tmp21.RemoveAt(tmp21.Count - 1);
						s.Add((byte)tmp21.Count);
						s.AddRange(tmp21);
						
						s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(tmp19.Value));
					}
				}
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize GameName
			byte tmp22;
			tmp22 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp22 == 1)
			{
				byte tmp23;
				tmp23 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp24 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp24, 0, tmp23);
				offset += tmp23;
				uint tmp25;
				tmp25 = BitConverter.ToUInt32(tmp24, (int)0);
				
				GameName = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp25).ToArray());
				offset += tmp25;
			}
			else
				GameName = null;
			
			// deserialize Sides
			byte tmp26;
			tmp26 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp26 == 1)
			{
				byte tmp27;
				tmp27 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp28 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp28, 0, tmp27);
				offset += tmp27;
				uint tmp29;
				tmp29 = BitConverter.ToUInt32(tmp28, (int)0);
				
				Sides = new Dictionary<string, List<string>>();
				for (uint tmp30 = 0; tmp30 < tmp29; tmp30++)
				{
					string tmp31;
					byte tmp33;
					tmp33 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp33 == 1)
					{
						byte tmp34;
						tmp34 = (byte)s[(int)offset];
						offset += sizeof(byte);
						byte[] tmp35 = new byte[sizeof(uint)];
						Array.Copy(s, offset, tmp35, 0, tmp34);
						offset += tmp34;
						uint tmp36;
						tmp36 = BitConverter.ToUInt32(tmp35, (int)0);
						
						tmp31 = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp36).ToArray());
						offset += tmp36;
					}
					else
						tmp31 = null;
					
					List<string> tmp32;
					byte tmp37;
					tmp37 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp37 == 1)
					{
						byte tmp38;
						tmp38 = (byte)s[(int)offset];
						offset += sizeof(byte);
						byte[] tmp39 = new byte[sizeof(uint)];
						Array.Copy(s, offset, tmp39, 0, tmp38);
						offset += tmp38;
						uint tmp40;
						tmp40 = BitConverter.ToUInt32(tmp39, (int)0);
						
						tmp32 = new List<string>();
						for (uint tmp41 = 0; tmp41 < tmp40; tmp41++)
						{
							string tmp42;
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
								
								tmp42 = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp46).ToArray());
								offset += tmp46;
							}
							else
								tmp42 = null;
							tmp32.Add(tmp42);
						}
					}
					else
						tmp32 = null;
					
					Sides[tmp31] = tmp32;
				}
			}
			else
				Sides = null;
			
			// deserialize GuiCycleDuration
			byte tmp47;
			tmp47 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp47 == 1)
			{
				GuiCycleDuration = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				GuiCycleDuration = null;
			
			// deserialize GuiSideColors
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
				
				GuiSideColors = new Dictionary<string, string>();
				for (uint tmp52 = 0; tmp52 < tmp51; tmp52++)
				{
					string tmp53;
					byte tmp55;
					tmp55 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp55 == 1)
					{
						byte tmp56;
						tmp56 = (byte)s[(int)offset];
						offset += sizeof(byte);
						byte[] tmp57 = new byte[sizeof(uint)];
						Array.Copy(s, offset, tmp57, 0, tmp56);
						offset += tmp56;
						uint tmp58;
						tmp58 = BitConverter.ToUInt32(tmp57, (int)0);
						
						tmp53 = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp58).ToArray());
						offset += tmp58;
					}
					else
						tmp53 = null;
					
					string tmp54;
					byte tmp59;
					tmp59 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp59 == 1)
					{
						byte tmp60;
						tmp60 = (byte)s[(int)offset];
						offset += sizeof(byte);
						byte[] tmp61 = new byte[sizeof(uint)];
						Array.Copy(s, offset, tmp61, 0, tmp60);
						offset += tmp60;
						uint tmp62;
						tmp62 = BitConverter.ToUInt32(tmp61, (int)0);
						
						tmp54 = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp62).ToArray());
						offset += tmp62;
					}
					else
						tmp54 = null;
					
					GuiSideColors[tmp53] = tmp54;
				}
			}
			else
				GuiSideColors = null;
			
			return offset;
		}
	}
	
	public partial class AgentJoined : KSObject
	{
		public string SideName { get; set; }
		public string AgentName { get; set; }
		public string TeamNickname { get; set; }
		

		public AgentJoined()
		{
		}
		
		public new const string NameStatic = "AgentJoined";
		
		public override string Name() => "AgentJoined";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize SideName
			s.Add((byte)((SideName == null) ? 0 : 1));
			if (SideName != null)
			{
				List<byte> tmp63 = new List<byte>();
				tmp63.AddRange(BitConverter.GetBytes((uint)SideName.Count()));
				while (tmp63.Count > 0 && tmp63.Last() == 0)
					tmp63.RemoveAt(tmp63.Count - 1);
				s.Add((byte)tmp63.Count);
				s.AddRange(tmp63);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(SideName));
			}
			
			// serialize AgentName
			s.Add((byte)((AgentName == null) ? 0 : 1));
			if (AgentName != null)
			{
				List<byte> tmp64 = new List<byte>();
				tmp64.AddRange(BitConverter.GetBytes((uint)AgentName.Count()));
				while (tmp64.Count > 0 && tmp64.Last() == 0)
					tmp64.RemoveAt(tmp64.Count - 1);
				s.Add((byte)tmp64.Count);
				s.AddRange(tmp64);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(AgentName));
			}
			
			// serialize TeamNickname
			s.Add((byte)((TeamNickname == null) ? 0 : 1));
			if (TeamNickname != null)
			{
				List<byte> tmp65 = new List<byte>();
				tmp65.AddRange(BitConverter.GetBytes((uint)TeamNickname.Count()));
				while (tmp65.Count > 0 && tmp65.Last() == 0)
					tmp65.RemoveAt(tmp65.Count - 1);
				s.Add((byte)tmp65.Count);
				s.AddRange(tmp65);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(TeamNickname));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize SideName
			byte tmp66;
			tmp66 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp66 == 1)
			{
				byte tmp67;
				tmp67 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp68 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp68, 0, tmp67);
				offset += tmp67;
				uint tmp69;
				tmp69 = BitConverter.ToUInt32(tmp68, (int)0);
				
				SideName = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp69).ToArray());
				offset += tmp69;
			}
			else
				SideName = null;
			
			// deserialize AgentName
			byte tmp70;
			tmp70 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp70 == 1)
			{
				byte tmp71;
				tmp71 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp72 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp72, 0, tmp71);
				offset += tmp71;
				uint tmp73;
				tmp73 = BitConverter.ToUInt32(tmp72, (int)0);
				
				AgentName = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp73).ToArray());
				offset += tmp73;
			}
			else
				AgentName = null;
			
			// deserialize TeamNickname
			byte tmp74;
			tmp74 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp74 == 1)
			{
				byte tmp75;
				tmp75 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp76 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp76, 0, tmp75);
				offset += tmp75;
				uint tmp77;
				tmp77 = BitConverter.ToUInt32(tmp76, (int)0);
				
				TeamNickname = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp77).ToArray());
				offset += tmp77;
			}
			else
				TeamNickname = null;
			
			return offset;
		}
	}
	
	public partial class AgentLeft : KSObject
	{
		public string SideName { get; set; }
		public string AgentName { get; set; }
		

		public AgentLeft()
		{
		}
		
		public new const string NameStatic = "AgentLeft";
		
		public override string Name() => "AgentLeft";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize SideName
			s.Add((byte)((SideName == null) ? 0 : 1));
			if (SideName != null)
			{
				List<byte> tmp78 = new List<byte>();
				tmp78.AddRange(BitConverter.GetBytes((uint)SideName.Count()));
				while (tmp78.Count > 0 && tmp78.Last() == 0)
					tmp78.RemoveAt(tmp78.Count - 1);
				s.Add((byte)tmp78.Count);
				s.AddRange(tmp78);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(SideName));
			}
			
			// serialize AgentName
			s.Add((byte)((AgentName == null) ? 0 : 1));
			if (AgentName != null)
			{
				List<byte> tmp79 = new List<byte>();
				tmp79.AddRange(BitConverter.GetBytes((uint)AgentName.Count()));
				while (tmp79.Count > 0 && tmp79.Last() == 0)
					tmp79.RemoveAt(tmp79.Count - 1);
				s.Add((byte)tmp79.Count);
				s.AddRange(tmp79);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(AgentName));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize SideName
			byte tmp80;
			tmp80 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp80 == 1)
			{
				byte tmp81;
				tmp81 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp82 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp82, 0, tmp81);
				offset += tmp81;
				uint tmp83;
				tmp83 = BitConverter.ToUInt32(tmp82, (int)0);
				
				SideName = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp83).ToArray());
				offset += tmp83;
			}
			else
				SideName = null;
			
			// deserialize AgentName
			byte tmp84;
			tmp84 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp84 == 1)
			{
				byte tmp85;
				tmp85 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp86 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp86, 0, tmp85);
				offset += tmp85;
				uint tmp87;
				tmp87 = BitConverter.ToUInt32(tmp86, (int)0);
				
				AgentName = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp87).ToArray());
				offset += tmp87;
			}
			else
				AgentName = null;
			
			return offset;
		}
	}
	
	public partial class StartGame : KSObject
	{
		public uint? StartTime { get; set; }
		

		public StartGame()
		{
		}
		
		public new const string NameStatic = "StartGame";
		
		public override string Name() => "StartGame";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize StartTime
			s.Add((byte)((StartTime == null) ? 0 : 1));
			if (StartTime != null)
			{
				s.AddRange(BitConverter.GetBytes((uint)StartTime));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize StartTime
			byte tmp88;
			tmp88 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp88 == 1)
			{
				StartTime = BitConverter.ToUInt32(s, (int)offset);
				offset += sizeof(uint);
			}
			else
				StartTime = null;
			
			return offset;
		}
	}
	
	public partial class EndGame : KSObject
	{
		public string WinnerSidename { get; set; }
		public Dictionary<string, Dictionary<string, string>> Details { get; set; }
		

		public EndGame()
		{
		}
		
		public new const string NameStatic = "EndGame";
		
		public override string Name() => "EndGame";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize WinnerSidename
			s.Add((byte)((WinnerSidename == null) ? 0 : 1));
			if (WinnerSidename != null)
			{
				List<byte> tmp89 = new List<byte>();
				tmp89.AddRange(BitConverter.GetBytes((uint)WinnerSidename.Count()));
				while (tmp89.Count > 0 && tmp89.Last() == 0)
					tmp89.RemoveAt(tmp89.Count - 1);
				s.Add((byte)tmp89.Count);
				s.AddRange(tmp89);
				
				s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(WinnerSidename));
			}
			
			// serialize Details
			s.Add((byte)((Details == null) ? 0 : 1));
			if (Details != null)
			{
				List<byte> tmp90 = new List<byte>();
				tmp90.AddRange(BitConverter.GetBytes((uint)Details.Count()));
				while (tmp90.Count > 0 && tmp90.Last() == 0)
					tmp90.RemoveAt(tmp90.Count - 1);
				s.Add((byte)tmp90.Count);
				s.AddRange(tmp90);
				
				foreach (var tmp91 in Details)
				{
					s.Add((byte)((tmp91.Key == null) ? 0 : 1));
					if (tmp91.Key != null)
					{
						List<byte> tmp92 = new List<byte>();
						tmp92.AddRange(BitConverter.GetBytes((uint)tmp91.Key.Count()));
						while (tmp92.Count > 0 && tmp92.Last() == 0)
							tmp92.RemoveAt(tmp92.Count - 1);
						s.Add((byte)tmp92.Count);
						s.AddRange(tmp92);
						
						s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(tmp91.Key));
					}
					
					s.Add((byte)((tmp91.Value == null) ? 0 : 1));
					if (tmp91.Value != null)
					{
						List<byte> tmp93 = new List<byte>();
						tmp93.AddRange(BitConverter.GetBytes((uint)tmp91.Value.Count()));
						while (tmp93.Count > 0 && tmp93.Last() == 0)
							tmp93.RemoveAt(tmp93.Count - 1);
						s.Add((byte)tmp93.Count);
						s.AddRange(tmp93);
						
						foreach (var tmp94 in tmp91.Value)
						{
							s.Add((byte)((tmp94.Key == null) ? 0 : 1));
							if (tmp94.Key != null)
							{
								List<byte> tmp95 = new List<byte>();
								tmp95.AddRange(BitConverter.GetBytes((uint)tmp94.Key.Count()));
								while (tmp95.Count > 0 && tmp95.Last() == 0)
									tmp95.RemoveAt(tmp95.Count - 1);
								s.Add((byte)tmp95.Count);
								s.AddRange(tmp95);
								
								s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(tmp94.Key));
							}
							
							s.Add((byte)((tmp94.Value == null) ? 0 : 1));
							if (tmp94.Value != null)
							{
								List<byte> tmp96 = new List<byte>();
								tmp96.AddRange(BitConverter.GetBytes((uint)tmp94.Value.Count()));
								while (tmp96.Count > 0 && tmp96.Last() == 0)
									tmp96.RemoveAt(tmp96.Count - 1);
								s.Add((byte)tmp96.Count);
								s.AddRange(tmp96);
								
								s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(tmp94.Value));
							}
						}
					}
				}
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize WinnerSidename
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
				
				WinnerSidename = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp100).ToArray());
				offset += tmp100;
			}
			else
				WinnerSidename = null;
			
			// deserialize Details
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
				
				Details = new Dictionary<string, Dictionary<string, string>>();
				for (uint tmp105 = 0; tmp105 < tmp104; tmp105++)
				{
					string tmp106;
					byte tmp108;
					tmp108 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp108 == 1)
					{
						byte tmp109;
						tmp109 = (byte)s[(int)offset];
						offset += sizeof(byte);
						byte[] tmp110 = new byte[sizeof(uint)];
						Array.Copy(s, offset, tmp110, 0, tmp109);
						offset += tmp109;
						uint tmp111;
						tmp111 = BitConverter.ToUInt32(tmp110, (int)0);
						
						tmp106 = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp111).ToArray());
						offset += tmp111;
					}
					else
						tmp106 = null;
					
					Dictionary<string, string> tmp107;
					byte tmp112;
					tmp112 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp112 == 1)
					{
						byte tmp113;
						tmp113 = (byte)s[(int)offset];
						offset += sizeof(byte);
						byte[] tmp114 = new byte[sizeof(uint)];
						Array.Copy(s, offset, tmp114, 0, tmp113);
						offset += tmp113;
						uint tmp115;
						tmp115 = BitConverter.ToUInt32(tmp114, (int)0);
						
						tmp107 = new Dictionary<string, string>();
						for (uint tmp116 = 0; tmp116 < tmp115; tmp116++)
						{
							string tmp117;
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
								
								tmp117 = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp122).ToArray());
								offset += tmp122;
							}
							else
								tmp117 = null;
							
							string tmp118;
							byte tmp123;
							tmp123 = (byte)s[(int)offset];
							offset += sizeof(byte);
							if (tmp123 == 1)
							{
								byte tmp124;
								tmp124 = (byte)s[(int)offset];
								offset += sizeof(byte);
								byte[] tmp125 = new byte[sizeof(uint)];
								Array.Copy(s, offset, tmp125, 0, tmp124);
								offset += tmp124;
								uint tmp126;
								tmp126 = BitConverter.ToUInt32(tmp125, (int)0);
								
								tmp118 = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp126).ToArray());
								offset += tmp126;
							}
							else
								tmp118 = null;
							
							tmp107[tmp117] = tmp118;
						}
					}
					else
						tmp107 = null;
					
					Details[tmp106] = tmp107;
				}
			}
			else
				Details = null;
			
			return offset;
		}
	}
	
	public partial class SceneActions : KSObject
	{
		public List<string> ActionTypes { get; set; }
		public List<string> ActionPayloads { get; set; }
		

		public SceneActions()
		{
		}
		
		public new const string NameStatic = "SceneActions";
		
		public override string Name() => "SceneActions";
		
		public override byte[] Serialize()
		{
			List<byte> s = new List<byte>();
			
			// serialize ActionTypes
			s.Add((byte)((ActionTypes == null) ? 0 : 1));
			if (ActionTypes != null)
			{
				List<byte> tmp127 = new List<byte>();
				tmp127.AddRange(BitConverter.GetBytes((uint)ActionTypes.Count()));
				while (tmp127.Count > 0 && tmp127.Last() == 0)
					tmp127.RemoveAt(tmp127.Count - 1);
				s.Add((byte)tmp127.Count);
				s.AddRange(tmp127);
				
				foreach (var tmp128 in ActionTypes)
				{
					s.Add((byte)((tmp128 == null) ? 0 : 1));
					if (tmp128 != null)
					{
						List<byte> tmp129 = new List<byte>();
						tmp129.AddRange(BitConverter.GetBytes((uint)tmp128.Count()));
						while (tmp129.Count > 0 && tmp129.Last() == 0)
							tmp129.RemoveAt(tmp129.Count - 1);
						s.Add((byte)tmp129.Count);
						s.AddRange(tmp129);
						
						s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(tmp128));
					}
				}
			}
			
			// serialize ActionPayloads
			s.Add((byte)((ActionPayloads == null) ? 0 : 1));
			if (ActionPayloads != null)
			{
				List<byte> tmp130 = new List<byte>();
				tmp130.AddRange(BitConverter.GetBytes((uint)ActionPayloads.Count()));
				while (tmp130.Count > 0 && tmp130.Last() == 0)
					tmp130.RemoveAt(tmp130.Count - 1);
				s.Add((byte)tmp130.Count);
				s.AddRange(tmp130);
				
				foreach (var tmp131 in ActionPayloads)
				{
					s.Add((byte)((tmp131 == null) ? 0 : 1));
					if (tmp131 != null)
					{
						List<byte> tmp132 = new List<byte>();
						tmp132.AddRange(BitConverter.GetBytes((uint)tmp131.Count()));
						while (tmp132.Count > 0 && tmp132.Last() == 0)
							tmp132.RemoveAt(tmp132.Count - 1);
						s.Add((byte)tmp132.Count);
						s.AddRange(tmp132);
						
						s.AddRange(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(tmp131));
					}
				}
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize ActionTypes
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
				
				ActionTypes = new List<string>();
				for (uint tmp137 = 0; tmp137 < tmp136; tmp137++)
				{
					string tmp138;
					byte tmp139;
					tmp139 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp139 == 1)
					{
						byte tmp140;
						tmp140 = (byte)s[(int)offset];
						offset += sizeof(byte);
						byte[] tmp141 = new byte[sizeof(uint)];
						Array.Copy(s, offset, tmp141, 0, tmp140);
						offset += tmp140;
						uint tmp142;
						tmp142 = BitConverter.ToUInt32(tmp141, (int)0);
						
						tmp138 = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp142).ToArray());
						offset += tmp142;
					}
					else
						tmp138 = null;
					ActionTypes.Add(tmp138);
				}
			}
			else
				ActionTypes = null;
			
			// deserialize ActionPayloads
			byte tmp143;
			tmp143 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp143 == 1)
			{
				byte tmp144;
				tmp144 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp145 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp145, 0, tmp144);
				offset += tmp144;
				uint tmp146;
				tmp146 = BitConverter.ToUInt32(tmp145, (int)0);
				
				ActionPayloads = new List<string>();
				for (uint tmp147 = 0; tmp147 < tmp146; tmp147++)
				{
					string tmp148;
					byte tmp149;
					tmp149 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp149 == 1)
					{
						byte tmp150;
						tmp150 = (byte)s[(int)offset];
						offset += sizeof(byte);
						byte[] tmp151 = new byte[sizeof(uint)];
						Array.Copy(s, offset, tmp151, 0, tmp150);
						offset += tmp150;
						uint tmp152;
						tmp152 = BitConverter.ToUInt32(tmp151, (int)0);
						
						tmp148 = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(s.Skip((int)offset).Take((int)tmp152).ToArray());
						offset += tmp152;
					}
					else
						tmp148 = null;
					ActionPayloads.Add(tmp148);
				}
			}
			else
				ActionPayloads = null;
			
			return offset;
		}
	}
} // namespace KS.Messages
