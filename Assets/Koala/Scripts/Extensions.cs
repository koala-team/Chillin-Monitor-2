using DG.Tweening;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Koala
{
	public static class Extensions
	{
		public static readonly Encoding _encoding = Encoding.GetEncoding("ISO-8859-1");
		public const int NUM_LENGTH_BYTES = 4;
		public const int MAX_RECEIVE = 1024;


		public static Tween RegisterInTimeline(this Tween tween, float startTime, bool isForward)
		{
			TweensManager.Instance.AddTween(tween, isForward);

			TweenCallback endCallback = () =>
			{
				TweensManager.Instance.RemoveTween(tween, isForward);
				tween = null;
			};

			tween.OnKill(endCallback);
			tween.OnComplete(endCallback);

			tween.Goto(Math.Abs(Timeline.Instance.Time - startTime), true);

			return tween;
		}

		public static float GetFractionalPart(this float number)
		{
			return number - Mathf.Floor(number);
		}

		#region KSVectors
		public static Color ApplyKSVector4(this Color c, KS.SceneActions.Vector4 config)
		{
			if (config.X.HasValue)
				c.r = config.X.Value;
			if (config.Y.HasValue)
				c.g = config.Y.Value;
			if (config.Z.HasValue)
				c.b = config.Z.Value;
			if (config.W.HasValue)
				c.a = config.W.Value;

			return c;
		}

		public static KS.SceneActions.Vector4 ToKSVector4(this Color c)
		{
			var config = new KS.SceneActions.Vector4
			{
				X = c.r,
				Y = c.g,
				Z = c.b,
				W = c.a,
			};

			return config;
		}

		public static Rect ApplyKSVector4(this Rect r, KS.SceneActions.Vector4 config)
		{
			if (config.X.HasValue)
				r.x = config.X.Value;
			if (config.Y.HasValue)
				r.x = config.Y.Value;
			if (config.Z.HasValue)
				r.width = config.Z.Value;
			if (config.W.HasValue)
				r.height = config.W.Value;

			return r;
		}

		public static KS.SceneActions.Vector4 ToKSVector4(this Rect r)
		{
			var config = new KS.SceneActions.Vector4
			{
				X = r.x,
				Y = r.y,
				Z = r.width,
				W = r.height,
			};

			return config;
		}

		public static Vector3 ApplyKSVector3(this Vector3 v, KS.SceneActions.Vector3 config)
		{
			if (config.X.HasValue)
				v.x = config.X.Value;
			if (config.Y.HasValue)
				v.y = config.Y.Value;
			if (config.Z.HasValue)
				v.z = config.Z.Value;

			return v;
		}

		public static KS.SceneActions.Vector3 ToKSVector3(this Vector3 v)
		{
			var config = new KS.SceneActions.Vector3
			{
				X = v.x,
				Y = v.y,
				Z = v.z,
			};

			return config;
		}

		public static Vector2 ApplyKSVector2(this Vector2 v, KS.SceneActions.Vector2 config)
		{
			if (config.X.HasValue)
				v.x = config.X.Value;
			if (config.Y.HasValue)
				v.y = config.Y.Value;

			return v;
		}

		public static KS.SceneActions.Vector2 ToKSVector2(this Vector2 v)
		{
			var config = new KS.SceneActions.Vector2
			{
				X = v.x,
				Y = v.y,
			};

			return config;
		}
		#endregion

		public static float TruncateDecimal(this float number, int decimals)
		{
			float multiplier = Mathf.Pow(10, decimals);
			return (float)Math.Truncate(number * multiplier) / multiplier;
		}

		public static double TruncateDecimal(this double number, int decimals)
		{
			double multiplier = Math.Pow(10, decimals);
			return Math.Truncate(number * multiplier) / multiplier;
		}

		/// <summary>
		/// Extension that converts an array of Vector2 to an array of Vector3
		/// </summary>
		public static Vector3[] ToVector3(this Vector2[] vectors)
		{
			return Array.ConvertAll<Vector2, Vector3>(vectors, v => v);
		}

		public static WaitUntil WaitUntilComplete(this Task t)
		{
			return new WaitUntil(() => t.IsCompleted);
		}

		public static WaitUntil WaitUntilComplete(this AsyncOperation t)
		{
			return new WaitUntil(() => t.isDone);
		}

		public static byte[] GetBytes(this string s)
		{
			return _encoding.GetBytes(s);
		}

		public static Color ContrastColor(this Color color)
		{
			// Counting the perceptive luminance - human eye favors green color... 
			double luminance = 0.299 * color.r + 0.587 * color.g + 0.114 * color.b;

			return luminance > 0.5 ? Color.black : Color.white;
		}

		#region Stream
		public static async Task<byte[]> Receive(this Stream stream)
		{
			await new WaitForBackgroundThread();

			try
			{
				byte[] buffer = await stream.FullReceive(NUM_LENGTH_BYTES);
				int messageLength = BitConverter.ToInt32(buffer, 0);

				return await stream.FullReceive(messageLength);
			}
			catch
			{
				return null;
			}
		}

		private static async Task<byte[]> FullReceive(this Stream stream, int length)
		{
			try
			{
				int bytesReceived = 0;
				byte[] buffer = new byte[length];
				while (bytesReceived < length)
				{
					bytesReceived += await stream.ReadAsync(buffer, bytesReceived, Math.Min(MAX_RECEIVE, length - bytesReceived));
				}

				return buffer;
			}
			catch (Exception e)
			{
				await new WaitForMainThread();
				Debug.LogError(e.Message);
				return null;
			}
		}

		public static async Task Send(this Stream stream, byte[] data)
		{
			await new WaitForBackgroundThread();

			try
			{
				byte[] numBytesBuffer = BitConverter.GetBytes(data.Length);

				await stream.WriteAsync(numBytesBuffer, 0, numBytesBuffer.Length);
				await stream.WriteAsync(data, 0, data.Length);
			}
			catch (Exception e)
			{
				await new WaitForMainThread();
				Debug.LogError(e.Message);
			}
		}
		#endregion
	}
}
