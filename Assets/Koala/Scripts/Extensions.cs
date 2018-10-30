﻿using DG.Tweening;
using System;
using UnityEngine;

namespace Koala
{
	public static class Extensions
	{
		public static Tween RegisterChronosTimeline(this Tween tween, bool forward = false)
		{
			TweenCallback endCallback = () =>
			{
				tween = null;
			};

			tween.OnKill(endCallback);
			tween.OnComplete(endCallback);

			float sign = forward ? 1 : -1;
			tween.OnUpdate(() =>
			{
				float timeScaleSign = System.Math.Sign(sign * Timeline.Instance.TimeScale);

				if (tween.IsActive())
				{
					if (timeScaleSign < 0 && !tween.IsBackwards())
						tween.PlayBackwards();
					else if (timeScaleSign > 0 && tween.IsBackwards())
						tween.PlayForward();
				}
			});

			return tween;
		}

		public static float GetFractionalPart(this float number)
		{
			return number - Mathf.Floor(number);
		}

		#region ChangeVectorConfig
		public static Vector4 ApplyChangeVector4Config(this Vector4 v, Director.ChangeVector4Config config)
		{
			if (config.X.HasValue)
				v.x = config.X.Value;
			if (config.Y.HasValue)
				v.y = config.Y.Value;
			if (config.Z.HasValue)
				v.z = config.Z.Value;
			if (config.W.HasValue)
				v.w = config.W.Value;

			return v;
		}

		public static Director.ChangeVector4Config ToChangeVector4Config(this Vector4 v)
		{
			var config = new Director.ChangeVector4Config
			{
				X = v.x,
				Y = v.y,
				Z = v.z,
				W = v.w,
			};

			return config;
		}

		public static Color ApplyChangeVector4Config(this Color c, Director.ChangeVector4Config config)
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

		public static Director.ChangeVector4Config ToChangeVector4Config(this Color c)
		{
			var config = new Director.ChangeVector4Config
			{
				X = c.r,
				Y = c.g,
				Z = c.b,
				W = c.a,
			};

			return config;
		}

		public static Rect ApplyChangeVector4Config(this Rect r, Director.ChangeVector4Config config)
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

		public static Director.ChangeVector4Config ToChangeVector4Config(this Rect r)
		{
			var config = new Director.ChangeVector4Config
			{
				X = r.x,
				Y = r.y,
				Z = r.width,
				W = r.height,
			};

			return config;
		}

		public static Vector3 ApplyChangeVector3Config(this Vector3 v, Director.ChangeVector3Config config)
		{
			if (config.X.HasValue)
				v.x = config.X.Value;
			if (config.Y.HasValue)
				v.y = config.Y.Value;
			if (config.Z.HasValue)
				v.z = config.Z.Value;

			return v;
		}

		public static Director.ChangeVector3Config ToChangeVector3Config(this Vector3 v)
		{
			var config = new Director.ChangeVector3Config
			{
				X = v.x,
				Y = v.y,
				Z = v.z,
			};

			return config;
		}

		public static Vector2 ApplyChangeVector2Config(this Vector2 v, Director.ChangeVector2Config config)
		{
			if (config.X.HasValue)
				v.x = config.X.Value;
			if (config.Y.HasValue)
				v.y = config.Y.Value;

			return v;
		}

		public static Director.ChangeVector2Config ToChangeVector2Config(this Vector2 v)
		{
			var config = new Director.ChangeVector2Config
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
	}
}
