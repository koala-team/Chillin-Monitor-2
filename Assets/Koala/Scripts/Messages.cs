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
				List<byte> tmp43 = new List<byte>();
				tmp43.AddRange(BitConverter.GetBytes((uint)SideName.Count()));
				while (tmp43.Count > 0 && tmp43.Last() == 0)
					tmp43.RemoveAt(tmp43.Count - 1);
				s.Add((byte)tmp43.Count);
				s.AddRange(tmp43);
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(SideName));
			}
			
			// serialize AgentName
			s.Add((byte)((AgentName == null) ? 0 : 1));
			if (AgentName != null)
			{
				List<byte> tmp44 = new List<byte>();
				tmp44.AddRange(BitConverter.GetBytes((uint)AgentName.Count()));
				while (tmp44.Count > 0 && tmp44.Last() == 0)
					tmp44.RemoveAt(tmp44.Count - 1);
				s.Add((byte)tmp44.Count);
				s.AddRange(tmp44);
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(AgentName));
			}
			
			// serialize TeamNickname
			s.Add((byte)((TeamNickname == null) ? 0 : 1));
			if (TeamNickname != null)
			{
				List<byte> tmp45 = new List<byte>();
				tmp45.AddRange(BitConverter.GetBytes((uint)TeamNickname.Count()));
				while (tmp45.Count > 0 && tmp45.Last() == 0)
					tmp45.RemoveAt(tmp45.Count - 1);
				s.Add((byte)tmp45.Count);
				s.AddRange(tmp45);
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(TeamNickname));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize SideName
			byte tmp46;
			tmp46 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp46 == 1)
			{
				byte tmp47;
				tmp47 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp48 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp48, 0, tmp47);
				offset += tmp47;
				uint tmp49;
				tmp49 = BitConverter.ToUInt32(tmp48, (int)0);
				
				SideName = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp49).ToArray());
				offset += tmp49;
			}
			else
				SideName = null;
			
			// deserialize AgentName
			byte tmp50;
			tmp50 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp50 == 1)
			{
				byte tmp51;
				tmp51 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp52 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp52, 0, tmp51);
				offset += tmp51;
				uint tmp53;
				tmp53 = BitConverter.ToUInt32(tmp52, (int)0);
				
				AgentName = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp53).ToArray());
				offset += tmp53;
			}
			else
				AgentName = null;
			
			// deserialize TeamNickname
			byte tmp54;
			tmp54 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp54 == 1)
			{
				byte tmp55;
				tmp55 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp56 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp56, 0, tmp55);
				offset += tmp55;
				uint tmp57;
				tmp57 = BitConverter.ToUInt32(tmp56, (int)0);
				
				TeamNickname = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp57).ToArray());
				offset += tmp57;
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
				List<byte> tmp58 = new List<byte>();
				tmp58.AddRange(BitConverter.GetBytes((uint)SideName.Count()));
				while (tmp58.Count > 0 && tmp58.Last() == 0)
					tmp58.RemoveAt(tmp58.Count - 1);
				s.Add((byte)tmp58.Count);
				s.AddRange(tmp58);
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(SideName));
			}
			
			// serialize AgentName
			s.Add((byte)((AgentName == null) ? 0 : 1));
			if (AgentName != null)
			{
				List<byte> tmp59 = new List<byte>();
				tmp59.AddRange(BitConverter.GetBytes((uint)AgentName.Count()));
				while (tmp59.Count > 0 && tmp59.Last() == 0)
					tmp59.RemoveAt(tmp59.Count - 1);
				s.Add((byte)tmp59.Count);
				s.AddRange(tmp59);
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(AgentName));
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize SideName
			byte tmp60;
			tmp60 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp60 == 1)
			{
				byte tmp61;
				tmp61 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp62 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp62, 0, tmp61);
				offset += tmp61;
				uint tmp63;
				tmp63 = BitConverter.ToUInt32(tmp62, (int)0);
				
				SideName = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp63).ToArray());
				offset += tmp63;
			}
			else
				SideName = null;
			
			// deserialize AgentName
			byte tmp64;
			tmp64 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp64 == 1)
			{
				byte tmp65;
				tmp65 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp66 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp66, 0, tmp65);
				offset += tmp65;
				uint tmp67;
				tmp67 = BitConverter.ToUInt32(tmp66, (int)0);
				
				AgentName = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp67).ToArray());
				offset += tmp67;
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
			byte tmp68;
			tmp68 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp68 == 1)
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
				List<byte> tmp69 = new List<byte>();
				tmp69.AddRange(BitConverter.GetBytes((uint)WinnerSidename.Count()));
				while (tmp69.Count > 0 && tmp69.Last() == 0)
					tmp69.RemoveAt(tmp69.Count - 1);
				s.Add((byte)tmp69.Count);
				s.AddRange(tmp69);
				
				s.AddRange(System.Text.Encoding.ASCII.GetBytes(WinnerSidename));
			}
			
			// serialize Details
			s.Add((byte)((Details == null) ? 0 : 1));
			if (Details != null)
			{
				List<byte> tmp70 = new List<byte>();
				tmp70.AddRange(BitConverter.GetBytes((uint)Details.Count()));
				while (tmp70.Count > 0 && tmp70.Last() == 0)
					tmp70.RemoveAt(tmp70.Count - 1);
				s.Add((byte)tmp70.Count);
				s.AddRange(tmp70);
				
				foreach (var tmp71 in Details)
				{
					s.Add((byte)((tmp71.Key == null) ? 0 : 1));
					if (tmp71.Key != null)
					{
						List<byte> tmp72 = new List<byte>();
						tmp72.AddRange(BitConverter.GetBytes((uint)tmp71.Key.Count()));
						while (tmp72.Count > 0 && tmp72.Last() == 0)
							tmp72.RemoveAt(tmp72.Count - 1);
						s.Add((byte)tmp72.Count);
						s.AddRange(tmp72);
						
						s.AddRange(System.Text.Encoding.ASCII.GetBytes(tmp71.Key));
					}
					
					s.Add((byte)((tmp71.Value == null) ? 0 : 1));
					if (tmp71.Value != null)
					{
						List<byte> tmp73 = new List<byte>();
						tmp73.AddRange(BitConverter.GetBytes((uint)tmp71.Value.Count()));
						while (tmp73.Count > 0 && tmp73.Last() == 0)
							tmp73.RemoveAt(tmp73.Count - 1);
						s.Add((byte)tmp73.Count);
						s.AddRange(tmp73);
						
						foreach (var tmp74 in tmp71.Value)
						{
							s.Add((byte)((tmp74.Key == null) ? 0 : 1));
							if (tmp74.Key != null)
							{
								List<byte> tmp75 = new List<byte>();
								tmp75.AddRange(BitConverter.GetBytes((uint)tmp74.Key.Count()));
								while (tmp75.Count > 0 && tmp75.Last() == 0)
									tmp75.RemoveAt(tmp75.Count - 1);
								s.Add((byte)tmp75.Count);
								s.AddRange(tmp75);
								
								s.AddRange(System.Text.Encoding.ASCII.GetBytes(tmp74.Key));
							}
							
							s.Add((byte)((tmp74.Value == null) ? 0 : 1));
							if (tmp74.Value != null)
							{
								List<byte> tmp76 = new List<byte>();
								tmp76.AddRange(BitConverter.GetBytes((uint)tmp74.Value.Count()));
								while (tmp76.Count > 0 && tmp76.Last() == 0)
									tmp76.RemoveAt(tmp76.Count - 1);
								s.Add((byte)tmp76.Count);
								s.AddRange(tmp76);
								
								s.AddRange(System.Text.Encoding.ASCII.GetBytes(tmp74.Value));
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
			byte tmp77;
			tmp77 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp77 == 1)
			{
				byte tmp78;
				tmp78 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp79 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp79, 0, tmp78);
				offset += tmp78;
				uint tmp80;
				tmp80 = BitConverter.ToUInt32(tmp79, (int)0);
				
				WinnerSidename = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp80).ToArray());
				offset += tmp80;
			}
			else
				WinnerSidename = null;
			
			// deserialize Details
			byte tmp81;
			tmp81 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp81 == 1)
			{
				byte tmp82;
				tmp82 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp83 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp83, 0, tmp82);
				offset += tmp82;
				uint tmp84;
				tmp84 = BitConverter.ToUInt32(tmp83, (int)0);
				
				Details = new Dictionary<string, Dictionary<string, string>>();
				for (uint tmp85 = 0; tmp85 < tmp84; tmp85++)
				{
					string tmp86;
					byte tmp88;
					tmp88 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp88 == 1)
					{
						byte tmp89;
						tmp89 = (byte)s[(int)offset];
						offset += sizeof(byte);
						byte[] tmp90 = new byte[sizeof(uint)];
						Array.Copy(s, offset, tmp90, 0, tmp89);
						offset += tmp89;
						uint tmp91;
						tmp91 = BitConverter.ToUInt32(tmp90, (int)0);
						
						tmp86 = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp91).ToArray());
						offset += tmp91;
					}
					else
						tmp86 = null;
					
					Dictionary<string, string> tmp87;
					byte tmp92;
					tmp92 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp92 == 1)
					{
						byte tmp93;
						tmp93 = (byte)s[(int)offset];
						offset += sizeof(byte);
						byte[] tmp94 = new byte[sizeof(uint)];
						Array.Copy(s, offset, tmp94, 0, tmp93);
						offset += tmp93;
						uint tmp95;
						tmp95 = BitConverter.ToUInt32(tmp94, (int)0);
						
						tmp87 = new Dictionary<string, string>();
						for (uint tmp96 = 0; tmp96 < tmp95; tmp96++)
						{
							string tmp97;
							byte tmp99;
							tmp99 = (byte)s[(int)offset];
							offset += sizeof(byte);
							if (tmp99 == 1)
							{
								byte tmp100;
								tmp100 = (byte)s[(int)offset];
								offset += sizeof(byte);
								byte[] tmp101 = new byte[sizeof(uint)];
								Array.Copy(s, offset, tmp101, 0, tmp100);
								offset += tmp100;
								uint tmp102;
								tmp102 = BitConverter.ToUInt32(tmp101, (int)0);
								
								tmp97 = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp102).ToArray());
								offset += tmp102;
							}
							else
								tmp97 = null;
							
							string tmp98;
							byte tmp103;
							tmp103 = (byte)s[(int)offset];
							offset += sizeof(byte);
							if (tmp103 == 1)
							{
								byte tmp104;
								tmp104 = (byte)s[(int)offset];
								offset += sizeof(byte);
								byte[] tmp105 = new byte[sizeof(uint)];
								Array.Copy(s, offset, tmp105, 0, tmp104);
								offset += tmp104;
								uint tmp106;
								tmp106 = BitConverter.ToUInt32(tmp105, (int)0);
								
								tmp98 = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp106).ToArray());
								offset += tmp106;
							}
							else
								tmp98 = null;
							
							tmp87[tmp97] = tmp98;
						}
					}
					else
						tmp87 = null;
					
					Details[tmp86] = tmp87;
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
				List<byte> tmp107 = new List<byte>();
				tmp107.AddRange(BitConverter.GetBytes((uint)ActionTypes.Count()));
				while (tmp107.Count > 0 && tmp107.Last() == 0)
					tmp107.RemoveAt(tmp107.Count - 1);
				s.Add((byte)tmp107.Count);
				s.AddRange(tmp107);
				
				foreach (var tmp108 in ActionTypes)
				{
					s.Add((byte)((tmp108 == null) ? 0 : 1));
					if (tmp108 != null)
					{
						List<byte> tmp109 = new List<byte>();
						tmp109.AddRange(BitConverter.GetBytes((uint)tmp108.Count()));
						while (tmp109.Count > 0 && tmp109.Last() == 0)
							tmp109.RemoveAt(tmp109.Count - 1);
						s.Add((byte)tmp109.Count);
						s.AddRange(tmp109);
						
						s.AddRange(System.Text.Encoding.ASCII.GetBytes(tmp108));
					}
				}
			}
			
			// serialize ActionPayloads
			s.Add((byte)((ActionPayloads == null) ? 0 : 1));
			if (ActionPayloads != null)
			{
				List<byte> tmp110 = new List<byte>();
				tmp110.AddRange(BitConverter.GetBytes((uint)ActionPayloads.Count()));
				while (tmp110.Count > 0 && tmp110.Last() == 0)
					tmp110.RemoveAt(tmp110.Count - 1);
				s.Add((byte)tmp110.Count);
				s.AddRange(tmp110);
				
				foreach (var tmp111 in ActionPayloads)
				{
					s.Add((byte)((tmp111 == null) ? 0 : 1));
					if (tmp111 != null)
					{
						List<byte> tmp112 = new List<byte>();
						tmp112.AddRange(BitConverter.GetBytes((uint)tmp111.Count()));
						while (tmp112.Count > 0 && tmp112.Last() == 0)
							tmp112.RemoveAt(tmp112.Count - 1);
						s.Add((byte)tmp112.Count);
						s.AddRange(tmp112);
						
						s.AddRange(System.Text.Encoding.ASCII.GetBytes(tmp111));
					}
				}
			}
			
			return s.ToArray();
		}
		
		public override uint Deserialize(byte[] s, uint offset = 0)
		{
			// deserialize ActionTypes
			byte tmp113;
			tmp113 = (byte)s[(int)offset];
			offset += sizeof(byte);
			if (tmp113 == 1)
			{
				byte tmp114;
				tmp114 = (byte)s[(int)offset];
				offset += sizeof(byte);
				byte[] tmp115 = new byte[sizeof(uint)];
				Array.Copy(s, offset, tmp115, 0, tmp114);
				offset += tmp114;
				uint tmp116;
				tmp116 = BitConverter.ToUInt32(tmp115, (int)0);
				
				ActionTypes = new List<string>();
				for (uint tmp117 = 0; tmp117 < tmp116; tmp117++)
				{
					string tmp118;
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
						
						tmp118 = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp122).ToArray());
						offset += tmp122;
					}
					else
						tmp118 = null;
					ActionTypes.Add(tmp118);
				}
			}
			else
				ActionTypes = null;
			
			// deserialize ActionPayloads
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
				
				ActionPayloads = new List<string>();
				for (uint tmp127 = 0; tmp127 < tmp126; tmp127++)
				{
					string tmp128;
					byte tmp129;
					tmp129 = (byte)s[(int)offset];
					offset += sizeof(byte);
					if (tmp129 == 1)
					{
						byte tmp130;
						tmp130 = (byte)s[(int)offset];
						offset += sizeof(byte);
						byte[] tmp131 = new byte[sizeof(uint)];
						Array.Copy(s, offset, tmp131, 0, tmp130);
						offset += tmp130;
						uint tmp132;
						tmp132 = BitConverter.ToUInt32(tmp131, (int)0);
						
						tmp128 = System.Text.Encoding.ASCII.GetString(s.Skip((int)offset).Take((int)tmp132).ToArray());
						offset += tmp132;
					}
					else
						tmp128 = null;
					ActionPayloads.Add(tmp128);
				}
			}
			else
				ActionPayloads = null;
			
			return offset;
		}
	}
} // namespace KS.Messages
