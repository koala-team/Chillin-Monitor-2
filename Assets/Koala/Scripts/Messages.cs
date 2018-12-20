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
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(Type));
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
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(Payload));
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
				
				Type = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp5).ToArray());
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
				
				Payload = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp9).ToArray());
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
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(GameName));
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
						
						s.AddRange(System.Text.Encoding.ASCII.GetBytes(tmp13.Key));
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
								
								s.AddRange(System.Text.Encoding.ASCII.GetBytes(tmp16));
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
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize GameName
			byte tmp18;
			tmp18 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp18 == 1)
			{
				byte tmp19;
				tmp19 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp20 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp20, 0, tmp19);
				offset += tmp19;
				uint tmp21;
				tmp21 = BitConverter.ToUInt32(tmp20, (int)0);
				
				GameName = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp21).ToArray());
				offset += tmp21;
			}
			else
				GameName = null;
			
			// deserialize Sides
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
				
				Sides = new Dictionary<string, List<string>>();
				for (uint tmp26 = 0; tmp26 < tmp25; tmp26++)
				{
					string tmp27;
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
						
						tmp27 = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp32).ToArray());
						offset += tmp32;
					}
					else
						tmp27 = null;
					
					List<string> tmp28;
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
						
						tmp28 = new List<string>();
						for (uint tmp37 = 0; tmp37 < tmp36; tmp37++)
						{
							string tmp38;
							byte tmp39;
							tmp39 = (byte)s[(int)offset];
							offset += sizeof(byte);
							if (tmp39 == 1)
							{
								byte tmp40;
								tmp40 = (byte)s[(int)offset];
								offset += sizeof(byte);
								byte[] tmp41 = new byte[sizeof(uint)];
								Array.Copy(s, offset, tmp41, 0, tmp40);
								offset += tmp40;
								uint tmp42;
								tmp42 = BitConverter.ToUInt32(tmp41, (int)0);
								
								tmp38 = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp42).ToArray());
								offset += tmp42;
							}
							else
								tmp38 = null;
							tmp28.Add(tmp38);
						}
					}
					else
						tmp28 = null;
					
					Sides[tmp27] = tmp28;
				}
			}
			else
				Sides = null;
			
			// deserialize GuiCycleDuration
			byte tmp43;
			tmp43 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp43 == 1)
			{
				GuiCycleDuration = BitConverter.ToSingle(s, (int)offset);
				offset += sizeof(float);
			}
			else
				GuiCycleDuration = null;
			
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
				List<byte> tmp44 = new List<byte>();
				tmp44.AddRange(BitConverter.GetBytes((uint)SideName.Count()));
				while (tmp44.Count > 0 && tmp44.Last() == 0)
					tmp44.RemoveAt(tmp44.Count - 1);
				s.Add((byte)tmp44.Count);
				s.AddRange(tmp44);
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(SideName));
			}
			
			// serialize AgentName
			s.Add((byte)((AgentName == null) ? 0 : 1));
			if (AgentName != null)
			{
				List<byte> tmp45 = new List<byte>();
				tmp45.AddRange(BitConverter.GetBytes((uint)AgentName.Count()));
				while (tmp45.Count > 0 && tmp45.Last() == 0)
					tmp45.RemoveAt(tmp45.Count - 1);
				s.Add((byte)tmp45.Count);
				s.AddRange(tmp45);
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(AgentName));
			}
			
			// serialize TeamNickname
			s.Add((byte)((TeamNickname == null) ? 0 : 1));
			if (TeamNickname != null)
			{
				List<byte> tmp46 = new List<byte>();
				tmp46.AddRange(BitConverter.GetBytes((uint)TeamNickname.Count()));
				while (tmp46.Count > 0 && tmp46.Last() == 0)
					tmp46.RemoveAt(tmp46.Count - 1);
				s.Add((byte)tmp46.Count);
				s.AddRange(tmp46);
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(TeamNickname));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize SideName
			byte tmp47;
			tmp47 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp47 == 1)
			{
				byte tmp48;
				tmp48 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp49 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp49, 0, tmp48);
				offset += tmp48;
				uint tmp50;
				tmp50 = BitConverter.ToUInt32(tmp49, (int)0);
				
				SideName = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp50).ToArray());
				offset += tmp50;
			}
			else
				SideName = null;
			
			// deserialize AgentName
			byte tmp51;
			tmp51 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp51 == 1)
			{
				byte tmp52;
				tmp52 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp53 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp53, 0, tmp52);
				offset += tmp52;
				uint tmp54;
				tmp54 = BitConverter.ToUInt32(tmp53, (int)0);
				
				AgentName = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp54).ToArray());
				offset += tmp54;
			}
			else
				AgentName = null;
			
			// deserialize TeamNickname
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
				
				TeamNickname = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp58).ToArray());
				offset += tmp58;
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
				List<byte> tmp59 = new List<byte>();
				tmp59.AddRange(BitConverter.GetBytes((uint)SideName.Count()));
				while (tmp59.Count > 0 && tmp59.Last() == 0)
					tmp59.RemoveAt(tmp59.Count - 1);
				s.Add((byte)tmp59.Count);
				s.AddRange(tmp59);
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(SideName));
			}
			
			// serialize AgentName
			s.Add((byte)((AgentName == null) ? 0 : 1));
			if (AgentName != null)
			{
				List<byte> tmp60 = new List<byte>();
				tmp60.AddRange(BitConverter.GetBytes((uint)AgentName.Count()));
				while (tmp60.Count > 0 && tmp60.Last() == 0)
					tmp60.RemoveAt(tmp60.Count - 1);
				s.Add((byte)tmp60.Count);
				s.AddRange(tmp60);
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(AgentName));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize SideName
			byte tmp61;
			tmp61 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp61 == 1)
			{
				byte tmp62;
				tmp62 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp63 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp63, 0, tmp62);
				offset += tmp62;
				uint tmp64;
				tmp64 = BitConverter.ToUInt32(tmp63, (int)0);
				
				SideName = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp64).ToArray());
				offset += tmp64;
			}
			else
				SideName = null;
			
			// deserialize AgentName
			byte tmp65;
			tmp65 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp65 == 1)
			{
				byte tmp66;
				tmp66 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp67 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp67, 0, tmp66);
				offset += tmp66;
				uint tmp68;
				tmp68 = BitConverter.ToUInt32(tmp67, (int)0);
				
				AgentName = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp68).ToArray());
				offset += tmp68;
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
			byte tmp69;
			tmp69 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp69 == 1)
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
				List<byte> tmp70 = new List<byte>();
				tmp70.AddRange(BitConverter.GetBytes((uint)WinnerSidename.Count()));
				while (tmp70.Count > 0 && tmp70.Last() == 0)
					tmp70.RemoveAt(tmp70.Count - 1);
				s.Add((byte)tmp70.Count);
				s.AddRange(tmp70);
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(WinnerSidename));
			}
			
			// serialize Details
			s.Add((byte)((Details == null) ? 0 : 1));
			if (Details != null)
			{
				List<byte> tmp71 = new List<byte>();
				tmp71.AddRange(BitConverter.GetBytes((uint)Details.Count()));
				while (tmp71.Count > 0 && tmp71.Last() == 0)
					tmp71.RemoveAt(tmp71.Count - 1);
				s.Add((byte)tmp71.Count);
				s.AddRange(tmp71);
				
				foreach (var tmp72 in Details)
				{
					s.Add((byte)((tmp72.Key == null) ? 0 : 1));
					if (tmp72.Key != null)
					{
						List<byte> tmp73 = new List<byte>();
						tmp73.AddRange(BitConverter.GetBytes((uint)tmp72.Key.Count()));
						while (tmp73.Count > 0 && tmp73.Last() == 0)
							tmp73.RemoveAt(tmp73.Count - 1);
						s.Add((byte)tmp73.Count);
						s.AddRange(tmp73);
						
						s.AddRange(System.Text.Encoding.ASCII.GetBytes(tmp72.Key));
					}
					
					s.Add((byte)((tmp72.Value == null) ? 0 : 1));
					if (tmp72.Value != null)
					{
						List<byte> tmp74 = new List<byte>();
						tmp74.AddRange(BitConverter.GetBytes((uint)tmp72.Value.Count()));
						while (tmp74.Count > 0 && tmp74.Last() == 0)
							tmp74.RemoveAt(tmp74.Count - 1);
						s.Add((byte)tmp74.Count);
						s.AddRange(tmp74);
						
						foreach (var tmp75 in tmp72.Value)
						{
							s.Add((byte)((tmp75.Key == null) ? 0 : 1));
							if (tmp75.Key != null)
							{
								List<byte> tmp76 = new List<byte>();
								tmp76.AddRange(BitConverter.GetBytes((uint)tmp75.Key.Count()));
								while (tmp76.Count > 0 && tmp76.Last() == 0)
									tmp76.RemoveAt(tmp76.Count - 1);
								s.Add((byte)tmp76.Count);
								s.AddRange(tmp76);
								
								s.AddRange(System.Text.Encoding.ASCII.GetBytes(tmp75.Key));
							}
							
							s.Add((byte)((tmp75.Value == null) ? 0 : 1));
							if (tmp75.Value != null)
							{
								List<byte> tmp77 = new List<byte>();
								tmp77.AddRange(BitConverter.GetBytes((uint)tmp75.Value.Count()));
								while (tmp77.Count > 0 && tmp77.Last() == 0)
									tmp77.RemoveAt(tmp77.Count - 1);
								s.Add((byte)tmp77.Count);
								s.AddRange(tmp77);
								
								s.AddRange(System.Text.Encoding.ASCII.GetBytes(tmp75.Value));
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
				
				WinnerSidename = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp81).ToArray());
				offset += tmp81;
			}
			else
				WinnerSidename = null;
			
			// deserialize Details
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
				
				Details = new Dictionary<string, Dictionary<string, string>>();
				for (uint tmp86 = 0; tmp86 < tmp85; tmp86++)
				{
					string tmp87;
					byte tmp89;
					tmp89 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp89 == 1)
					{
						byte tmp90;
						tmp90 = (byte)s[(int)offset];
						offset += sizeof(byte);
						byte[] tmp91 = new byte[sizeof(uint)];
						Array.Copy(s, offset, tmp91, 0, tmp90);
						offset += tmp90;
						uint tmp92;
						tmp92 = BitConverter.ToUInt32(tmp91, (int)0);
						
						tmp87 = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp92).ToArray());
						offset += tmp92;
					}
					else
						tmp87 = null;
					
					Dictionary<string, string> tmp88;
					byte tmp93;
					tmp93 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp93 == 1)
					{
						byte tmp94;
						tmp94 = (byte)s[(int)offset];
						offset += sizeof(byte);
						byte[] tmp95 = new byte[sizeof(uint)];
						Array.Copy(s, offset, tmp95, 0, tmp94);
						offset += tmp94;
						uint tmp96;
						tmp96 = BitConverter.ToUInt32(tmp95, (int)0);
						
						tmp88 = new Dictionary<string, string>();
						for (uint tmp97 = 0; tmp97 < tmp96; tmp97++)
						{
							string tmp98;
							byte tmp100;
							tmp100 = (byte)s[(int)offset];
							offset += sizeof(byte);
							if (tmp100 == 1)
							{
								byte tmp101;
								tmp101 = (byte)s[(int)offset];
								offset += sizeof(byte);
								byte[] tmp102 = new byte[sizeof(uint)];
								Array.Copy(s, offset, tmp102, 0, tmp101);
								offset += tmp101;
								uint tmp103;
								tmp103 = BitConverter.ToUInt32(tmp102, (int)0);
								
								tmp98 = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp103).ToArray());
								offset += tmp103;
							}
							else
								tmp98 = null;
							
							string tmp99;
							byte tmp104;
							tmp104 = (byte)s[(int)offset];
							offset += sizeof(byte);
							if (tmp104 == 1)
							{
								byte tmp105;
								tmp105 = (byte)s[(int)offset];
								offset += sizeof(byte);
								byte[] tmp106 = new byte[sizeof(uint)];
								Array.Copy(s, offset, tmp106, 0, tmp105);
								offset += tmp105;
								uint tmp107;
								tmp107 = BitConverter.ToUInt32(tmp106, (int)0);
								
								tmp99 = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp107).ToArray());
								offset += tmp107;
							}
							else
								tmp99 = null;
							
							tmp88[tmp98] = tmp99;
						}
					}
					else
						tmp88 = null;
					
					Details[tmp87] = tmp88;
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
				List<byte> tmp108 = new List<byte>();
				tmp108.AddRange(BitConverter.GetBytes((uint)ActionTypes.Count()));
				while (tmp108.Count > 0 && tmp108.Last() == 0)
					tmp108.RemoveAt(tmp108.Count - 1);
				s.Add((byte)tmp108.Count);
				s.AddRange(tmp108);
				
				foreach (var tmp109 in ActionTypes)
				{
					s.Add((byte)((tmp109 == null) ? 0 : 1));
					if (tmp109 != null)
					{
						List<byte> tmp110 = new List<byte>();
						tmp110.AddRange(BitConverter.GetBytes((uint)tmp109.Count()));
						while (tmp110.Count > 0 && tmp110.Last() == 0)
							tmp110.RemoveAt(tmp110.Count - 1);
						s.Add((byte)tmp110.Count);
						s.AddRange(tmp110);
						
						s.AddRange(System.Text.Encoding.ASCII.GetBytes(tmp109));
					}
				}
			}
			
			// serialize ActionPayloads
			s.Add((byte)((ActionPayloads == null) ? 0 : 1));
			if (ActionPayloads != null)
			{
				List<byte> tmp111 = new List<byte>();
				tmp111.AddRange(BitConverter.GetBytes((uint)ActionPayloads.Count()));
				while (tmp111.Count > 0 && tmp111.Last() == 0)
					tmp111.RemoveAt(tmp111.Count - 1);
				s.Add((byte)tmp111.Count);
				s.AddRange(tmp111);
				
				foreach (var tmp112 in ActionPayloads)
				{
					s.Add((byte)((tmp112 == null) ? 0 : 1));
					if (tmp112 != null)
					{
						List<byte> tmp113 = new List<byte>();
						tmp113.AddRange(BitConverter.GetBytes((uint)tmp112.Count()));
						while (tmp113.Count > 0 && tmp113.Last() == 0)
							tmp113.RemoveAt(tmp113.Count - 1);
						s.Add((byte)tmp113.Count);
						s.AddRange(tmp113);
						
						s.AddRange(System.Text.Encoding.ASCII.GetBytes(tmp112));
					}
				}
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize ActionTypes
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
				
				ActionTypes = new List<string>();
				for (uint tmp118 = 0; tmp118 < tmp117; tmp118++)
				{
					string tmp119;
					byte tmp120;
					tmp120 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp120 == 1)
					{
						byte tmp121;
						tmp121 = (byte)s[(int)offset];
						offset += sizeof(byte);
						byte[] tmp122 = new byte[sizeof(uint)];
						Array.Copy(s, offset, tmp122, 0, tmp121);
						offset += tmp121;
						uint tmp123;
						tmp123 = BitConverter.ToUInt32(tmp122, (int)0);
						
						tmp119 = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp123).ToArray());
						offset += tmp123;
					}
					else
						tmp119 = null;
					ActionTypes.Add(tmp119);
				}
			}
			else
				ActionTypes = null;
			
			// deserialize ActionPayloads
			byte tmp124;
			tmp124 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp124 == 1)
			{
				byte tmp125;
				tmp125 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp126 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp126, 0, tmp125);
				offset += tmp125;
				uint tmp127;
				tmp127 = BitConverter.ToUInt32(tmp126, (int)0);
				
				ActionPayloads = new List<string>();
				for (uint tmp128 = 0; tmp128 < tmp127; tmp128++)
				{
					string tmp129;
					byte tmp130;
					tmp130 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp130 == 1)
					{
						byte tmp131;
						tmp131 = (byte)s[(int)offset];
						offset += sizeof(byte);
						byte[] tmp132 = new byte[sizeof(uint)];
						Array.Copy(s, offset, tmp132, 0, tmp131);
						offset += tmp131;
						uint tmp133;
						tmp133 = BitConverter.ToUInt32(tmp132, (int)0);
						
						tmp129 = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp133).ToArray());
						offset += tmp133;
					}
					else
						tmp129 = null;
					ActionPayloads.Add(tmp129);
				}
			}
			else
				ActionPayloads = null;
			
			return offset;
		}
	}
} // namespace KS.Messages
