using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using UnityEngine;

namespace Koala
{
	public class Network
	{
		private const int MAX_TIMEOUT = 2147483647;
		private const int NUM_LENGTH_BYTES = 4;
		private const int MAX_RECEIVE = 1024;

		private TcpClient _client;
		private SslStream _stream;
		private readonly string _ip;
		private readonly int _port;
		private readonly int _timeout;

		public bool IsConnected
		{
			get { return _client != null && _client.Connected; }
		}


		public Network(string ip, int port, int timeout = MAX_TIMEOUT)
		{
			_ip = ip;
			_port = port;
			_timeout = timeout;
		}

		public async Task Connect(int connectTimeout = 3000)
		{
			await new WaitForBackgroundThread();

			try
			{
				_client = new TcpClient(_ip, _port);
				_stream = new SslStream(_client.GetStream(), true,
										new RemoteCertificateValidationCallback(ValidateServerCertificate), null);

				_stream.ReadTimeout = connectTimeout;
				await _stream.AuthenticateAsClientAsync(_ip);

				_stream.ReadTimeout = _timeout;
				_stream.WriteTimeout = _timeout;
			}
			catch (System.Exception e)
			{
				Debug.LogError(e.Message);
			}
		}

		public void Disconnect()
		{
			if (_stream != null)
				_stream.Close();
			if (_client != null)
				_client.Close();
		}

		public async Task<byte[]> Receive()
		{
			await new WaitForBackgroundThread();

			try
			{
				byte[] buffer = await FullReceive(NUM_LENGTH_BYTES);
				int messageLength = BitConverter.ToInt32(buffer, 0);

				buffer = await FullReceive(messageLength);
				return buffer;
			}
			catch
			{
				return null;
			}
		}

		private async Task<byte[]> FullReceive(int length)
		{
			try
			{
				int bytesReceived = 0;
				byte[] buffer = new byte[length];
				while (bytesReceived < length)
				{
					bytesReceived += await _stream.ReadAsync(buffer, bytesReceived, Math.Min(MAX_RECEIVE, length - bytesReceived));
				}

				return buffer;
			}
			catch
			{
				return null;
			}
		}

		public async Task Send(byte[] data)
		{
			await new WaitForBackgroundThread();

			byte[] numBytesBuffer = BitConverter.GetBytes(data.Length);

			byte[] buffer = new byte[NUM_LENGTH_BYTES + data.Length];
			Buffer.BlockCopy(numBytesBuffer, 0, buffer, 0, NUM_LENGTH_BYTES);
			Buffer.BlockCopy(data, 0, buffer, NUM_LENGTH_BYTES, data.Length);

			await _stream.WriteAsync(buffer, 0, buffer.Length);
		}

		private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}
	}
}
