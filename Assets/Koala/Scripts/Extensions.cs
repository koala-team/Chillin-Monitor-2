using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Koala
{
	public static class Extensions
	{
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

		/// <summary>
		/// Extension that converts an array of Vector2 to an array of Vector3
		/// </summary>
		public static Vector3[] ToVector3(this Vector2[] vectors)
		{
			return Array.ConvertAll<Vector2, Vector3>(vectors, v => v);
		}
	}
}
