using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KS.Messages;

namespace Koala
{
	public class Protocol
	{
		private Network _network;

		public Network Network => _network;


		public Protocol(Network network)
		{
			_network = network;
		}

		public async Task<KS.KSObject> RecvMessage()
		{
			await new WaitForBackgroundThread();

			byte[] buffer = await _network.Receive();

			if (buffer == null) return null;

			return await Helper.ProcessBuffer(buffer);
		}

		public async Task<bool> CheckToken()
		{
			await new WaitForBackgroundThread();

			await _network.Send(PlayerConfigs.Token.ISOGetBytes());
			Auth auth = (Auth)await RecvMessage();

			return auth.Authenticated.Value;
		}
	}
}
