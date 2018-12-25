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

			return await Task.Run<KS.KSObject>(() =>
			{
				Message message = new Message();
				message.Deserialize(buffer);

				var baseMessageType = Helper.Assembly.GetType("KS.Messages." + message.Type);
				KS.KSObject baseMessage = Activator.CreateInstance(baseMessageType) as KS.KSObject;
				baseMessage.Deserialize(message.Payload.GetBytes());

				if (baseMessage.Name() == SceneActions.NameStatic)
				{
					var sceneActions = (SceneActions)baseMessage;
					sceneActions.ParsedActions = new List<KS.KSObject>(sceneActions.ActionTypes.Count);

					for (int i = 0; i < sceneActions.ActionTypes.Count; i++)
					{
						var action = KS.KSObject.GetAction(sceneActions.ActionTypes[i], sceneActions.ActionPayloads[i]);
						sceneActions.ParsedActions.Add(action);
					}
				}

				return baseMessage;
			});
		}

		public async Task<bool> CheckToken()
		{
			await new WaitForBackgroundThread();

			await _network.Send(PlayerConfigs.Token.GetBytes());
			Auth auth = (Auth)await RecvMessage();

			return auth.Authenticated.Value;
		}
	}
}
