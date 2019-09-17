using System;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using KS.SceneActions;
using NodeCanvas.Framework;
using System.Reflection;
using System.Linq;
using System.Collections;

namespace Koala
{
	using TweenTypes = Tuple<Type, Type, Type>;

	public class ChangeParadoxBlackboardOccurrence : BaseOccurrence<ChangeParadoxBlackboardOccurrence, ChangeParadoxBlackboard>
	{
		private static readonly Type TWEEN_GETTER_TYPE = typeof(DG.Tweening.Core.DOGetter<>);
		private static readonly MethodInfo TWEEN_GETTER_METHOD_INFO = typeof(ChangeParadoxBlackboardOccurrence).GetMethod("_GetterDelegate");

		private static readonly Type TWEEN_SETTER_TYPE = typeof(DG.Tweening.Core.DOSetter<>);
		private static readonly MethodInfo TWEEN_SETTER_METHOD_INFO = typeof(ChangeParadoxBlackboardOccurrence).GetMethod("_SetterDelegate");

		private static readonly MethodInfo TWEEN_TO_METHOD_INFO = typeof(DOTween).GetMethods().First(method => method.Name == "To" && method.IsGenericMethod);

		// Types are: Getter/Setter Type, PlugOptions Type, ITweenPlugin Type
		private static readonly Dictionary<EParadoxBlackboardValueType, TweenTypes> TWEEN_TYPES = new Dictionary<EParadoxBlackboardValueType, TweenTypes>()
		{
			{ EParadoxBlackboardValueType.Int, new TweenTypes(typeof(int), typeof(DG.Tweening.Plugins.Options.NoOptions), typeof(DG.Tweening.Plugins.IntPlugin)) },
			{ EParadoxBlackboardValueType.Float, new TweenTypes(typeof(float), typeof(DG.Tweening.Plugins.Options.FloatOptions), typeof(DG.Tweening.Plugins.FloatPlugin)) },
			{ EParadoxBlackboardValueType.Vector2, new TweenTypes(typeof(UnityEngine.Vector2), typeof(DG.Tweening.Plugins.Options.VectorOptions), typeof(DG.Tweening.Plugins.Vector2Plugin)) },
			{ EParadoxBlackboardValueType.Vector3, new TweenTypes(typeof(UnityEngine.Vector3), typeof(DG.Tweening.Plugins.Options.VectorOptions), typeof(DG.Tweening.Plugins.Vector3Plugin)) },
			{ EParadoxBlackboardValueType.Vector4, new TweenTypes(typeof(UnityEngine.Vector4), typeof(DG.Tweening.Plugins.Options.VectorOptions), typeof(DG.Tweening.Plugins.Vector4Plugin)) },
			{ EParadoxBlackboardValueType.Color, new TweenTypes(typeof(UnityEngine.Color), typeof(DG.Tweening.Plugins.Options.ColorOptions), typeof(DG.Tweening.Plugins.ColorPlugin)) },
		};

		private GameObject _gameObject;
		private Blackboard _blackboard;
		private ChangeParadoxBlackboard _tweenConfig;

		public ChangeParadoxBlackboardOccurrence() { }

		protected override ChangeParadoxBlackboard CreateOldConfig()
		{
			_newConfig.EndValue = GetNewValue(_newConfig);

			var oldConfig = new ChangeParadoxBlackboard()
			{
				DurationCycles = _newConfig.DurationCycles,

				VarName = _newConfig.VarName,
				ValueType = _newConfig.ValueType,

				VarType = _newConfig.VarType,
				ListIndex = _newConfig.ListIndex,
				DictionaryKey = _newConfig.DictionaryKey,
			};

			switch (_newConfig.OpType)
			{
				case EParadoxBlackboardOperationType.Edit:
					oldConfig.OpType = EParadoxBlackboardOperationType.Edit;
					oldConfig.EndValue = GetBlackboardValue(_newConfig);
					break;

				case EParadoxBlackboardOperationType.Add:
					if (oldConfig.VarType.Value == EParadoxBlackboardVariableType.Simple)
						throw new NotSupportedException("Add operation not supported for Simple variable!");

					oldConfig.OpType = EParadoxBlackboardOperationType.Remove;

					if (oldConfig.VarType.Value == EParadoxBlackboardVariableType.List)
						oldConfig.ListIndex = (GetBlackboard()[oldConfig.VarName] as IList).Count;
					break;

				case EParadoxBlackboardOperationType.Remove:
					if (oldConfig.VarType.Value == EParadoxBlackboardVariableType.Simple)
						throw new NotSupportedException("Remove operation not supported for Simple variable!");

					oldConfig.OpType = EParadoxBlackboardOperationType.Add;
					oldConfig.EndValue = GetBlackboardValue(_newConfig);
					break;
			}

			return oldConfig;
		}

		protected override void ManageSuddenChanges(ChangeParadoxBlackboard config, bool isForward)
		{
			switch (config.OpType)
			{
				case EParadoxBlackboardOperationType.Edit:
					if (config.DurationCycles.Value == 0 || !TWEEN_TYPES.ContainsKey(config.ValueType.Value))
						EditBlackboardValue(config, config.EndValue);
					break;
				case EParadoxBlackboardOperationType.Add:
					AddBlackboardValue(config);
					break;
				case EParadoxBlackboardOperationType.Remove:
					RemoveBlackboardValue(config);
					break;
			}
		}

		protected override void ManageTweens(ChangeParadoxBlackboard config, bool isForward)
		{
			if (config.DurationCycles.Value != 0 && config.OpType == EParadoxBlackboardOperationType.Edit)
			{
				if (TWEEN_TYPES.TryGetValue(config.ValueType.Value, out TweenTypes types))
				{
					_tweenConfig = config;

					var getterMethodInfo = Helper.MakeGenericMethod(TWEEN_GETTER_METHOD_INFO, types.Item1);
					var setterMethodInfo = Helper.MakeGenericMethod(TWEEN_SETTER_METHOD_INFO, types.Item1);

					var getter = Helper.MakeGenericDelegate(TWEEN_GETTER_TYPE, this, getterMethodInfo, types.Item1);
					var setter = Helper.MakeGenericDelegate(TWEEN_SETTER_TYPE, this, setterMethodInfo, types.Item1);
					var tweenPlugin = Activator.CreateInstance(types.Item3);

					var to = Helper.MakeGenericMethod(TWEEN_TO_METHOD_INFO, types.Item1, types.Item1, types.Item2);
					(to.Invoke(null, new object[] { tweenPlugin, getter, setter, _tweenConfig.EndValue, _duration }) as Tween).RegisterInTimeline(_startTime, isForward);
				}
			}
		}

		public T _GetterDelegate<T>()
		{
			return (T)GetBlackboardValue(_tweenConfig);
		}

		public void _SetterDelegate<T>(T value)
		{
			EditBlackboardValue(_tweenConfig, value);
		}

		private object GetNewValue(ChangeParadoxBlackboard newConfig)
		{
			switch (newConfig.ValueType)
			{
				case EParadoxBlackboardValueType.Int:
					return newConfig.IntValue.Value;

				case EParadoxBlackboardValueType.Float:
					return newConfig.FloatValue.Value;

				case EParadoxBlackboardValueType.Bool:
					return newConfig.BoolValue.Value;

				case EParadoxBlackboardValueType.String:
					return newConfig.StringValue;

				case EParadoxBlackboardValueType.GameObject:
					return References.Instance.GetGameObject(References.GetFullRef(newConfig.GameObjectRef, newConfig.GameObjectChildRef));

				case EParadoxBlackboardValueType.Vector2:
					if (newConfig.OpType == EParadoxBlackboardOperationType.Edit)
						return ((UnityEngine.Vector2)GetBlackboard()[newConfig.VarName]).ApplyKSVector2(newConfig.VectorValue);
					return (UnityEngine.Vector2)newConfig.VectorValue;

				case EParadoxBlackboardValueType.Vector3:
					if (newConfig.OpType == EParadoxBlackboardOperationType.Edit)
						return ((UnityEngine.Vector3)GetBlackboard()[newConfig.VarName]).ApplyKSVector3(newConfig.VectorValue);
					return (UnityEngine.Vector3)newConfig.VectorValue;

				case EParadoxBlackboardValueType.Vector4:
					if (newConfig.OpType == EParadoxBlackboardOperationType.Edit)
						return ((UnityEngine.Vector4)GetBlackboard()[newConfig.VarName]).ApplyKSVector4(newConfig.VectorValue);
					return (UnityEngine.Vector4)newConfig.VectorValue;

				case EParadoxBlackboardValueType.Color:
					if (newConfig.OpType == EParadoxBlackboardOperationType.Edit)
						return ((Color)GetBlackboard()[newConfig.VarName]).ApplyKSVector4(newConfig.VectorValue);
					return (Color)newConfig.VectorValue;

				case EParadoxBlackboardValueType.LayerMask:
					return newConfig.LayerMaskValue.Mask;

				case EParadoxBlackboardValueType.Asset:
					return BundleManager.Instance.DynamicLoadAsset(Type.GetType(newConfig.AssetType, true), newConfig.AssetValue);

				default:
					throw new NotSupportedException("ChangeParadoxBlackboard.ValueType can't be null");
			}
		}

		private object GetBlackboardValue(ChangeParadoxBlackboard config)
		{
			var blackboard = GetBlackboard();

			switch (config.VarType)
			{
				case EParadoxBlackboardVariableType.Simple:
					return blackboard[config.VarName];
				case EParadoxBlackboardVariableType.List:
					return (blackboard[config.VarName] as IList)[config.ListIndex.Value];
				case EParadoxBlackboardVariableType.Dictionary:
					return (blackboard[config.VarName] as IDictionary)[config.DictionaryKey];
				default:
					throw new NotSupportedException("ChangeParadoxBlackboard.VarType can't be null");
			}
		}

		private void EditBlackboardValue(ChangeParadoxBlackboard config, object value)
		{
			var blackboard = GetBlackboard();

			switch (config.VarType)
			{
				case EParadoxBlackboardVariableType.Simple:
					blackboard[config.VarName] = value;
					break;
				case EParadoxBlackboardVariableType.List:
					(blackboard[config.VarName] as IList)[config.ListIndex.Value] = value;
					break;
				case EParadoxBlackboardVariableType.Dictionary:
					(blackboard[config.VarName] as IDictionary)[config.DictionaryKey] = value;
					break;
				default:
					throw new NotSupportedException("ChangeParadoxBlackboard.VarType can't be null");
			}
		}

		private void AddBlackboardValue(ChangeParadoxBlackboard config)
		{
			var blackboard = GetBlackboard();

			switch (config.VarType)
			{
				case EParadoxBlackboardVariableType.List:
					(blackboard[config.VarName] as IList).Add(config.EndValue);
					break;
				case EParadoxBlackboardVariableType.Dictionary:
					(blackboard[config.VarName] as IDictionary).Add(config.DictionaryKey, config.EndValue);
					break;
				default:
					throw new NotSupportedException("ChangeParadoxBlackboard.VarType can't be Simple for Add operation");
			}
		}

		private void RemoveBlackboardValue(ChangeParadoxBlackboard config)
		{
			var blackboard = GetBlackboard();

			switch (config.VarType)
			{
				case EParadoxBlackboardVariableType.List:
					(blackboard[config.VarName] as IList).RemoveAt(config.ListIndex.Value);
					break;
				case EParadoxBlackboardVariableType.Dictionary:
					(blackboard[config.VarName] as IDictionary).Remove(config.DictionaryKey);
					break;
				default:
					throw new NotSupportedException("ChangeParadoxBlackboard.VarType can't be Simple for Remove operation");
			}
		}

		private GameObject GetGameObject()
		{
			if (_gameObject == null)
				_gameObject = References.Instance.GetGameObject(_reference);

			return _gameObject;
		}

		private Blackboard GetBlackboard()
		{
			if (_blackboard == null)
				_blackboard = GetGameObject().GetComponent<Blackboard>();

			return _blackboard;
		}
	}
}
