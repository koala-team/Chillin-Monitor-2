using Koala;
using NodeCanvas.Framework;
using NodeCanvas.StateMachines;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace KS.SceneActions
{
	public partial class BaseAction
	{
		public string FullRef => References.GetFullRef(Ref, ChildRef);


		public virtual void Prepare()
		{
			Cycle = Cycle ?? 0;
			DurationCycles = DurationCycles ?? 0;
		}
	}

	public partial class Vector2
	{
		public static implicit operator Vector2(Vector3 v3)
		{
			return new Vector2
			{
				X = v3.X,
				Y = v3.Y,
			};
		}

		public static implicit operator Vector2(Vector4 v4)
		{
			return new Vector2
			{
				X = v4.X,
				Y = v4.Y,
			};
		}
	}

	public partial class Vector3
	{
		public static implicit operator Vector3(Vector2 v2)
		{
			return new Vector3
			{
				X = v2.X,
				Y = v2.Y,
				Z = 0,
			};
		}

		public static implicit operator Vector3(Vector4 v4)
		{
			return new Vector3
			{
				X = v4.X,
				Y = v4.Y,
				Z = v4.Z,
			};
		}
	}

	public partial class Vector4
	{
		public static implicit operator UnityEngine.Vector2(Vector4 v4)
		{
			return new UnityEngine.Vector2
			{
				x = v4.X ?? 0,
				y = v4.Y ?? 0,
			};
		}

		public static implicit operator UnityEngine.Vector3(Vector4 v4)
		{
			return new UnityEngine.Vector3
			{
				x = v4.X ?? 0,
				y = v4.Y ?? 0,
				z = v4.Z ?? 0,
			};
		}

		public static implicit operator UnityEngine.Vector4(Vector4 v4)
		{
			return new UnityEngine.Vector4
			{
				x = v4.X ?? 0,
				y = v4.Y ?? 0,
				z = v4.Z ?? 0,
				w = v4.W ?? 0,
			};
		}

		public static implicit operator Color(Vector4 v4)
		{
			return new Color
			{
				r = v4.X ?? 0,
				g = v4.Y ?? 0,
				b = v4.Z ?? 0,
				a = v4.W ?? 0,
			};
		}
	}

	public partial class LayerMask
	{
		public UnityEngine.LayerMask Mask
		{
			get
			{
				if (MasksInt.HasValue)
					return MasksInt.Value;
				return UnityEngine.LayerMask.GetMask(MasksString.ToArray());
			}
		}
	}

	public partial class BaseCreation
	{
		public GameObject GameObject { get; set; }
		public GameObject DefaultParentGO { get; set; }

		public string FullParentRef => References.GetFullRef(ParentRef, ParentChildRef);


		public override void Prepare()
		{
			base.Prepare();

			DefaultParentGO = Helper.RootGameObject;
		}
	}

	public partial class InstantiateBundleAsset
	{
		public override void Prepare()
		{
			base.Prepare();

			GameObject = BundleManager.Instance.LoadAsset<GameObject>(Asset);
			DefaultParentGO = (DefaultParent ?? EDefaultParent.RootObject) == EDefaultParent.RootObject ? Helper.RootGameObject : Helper.UserCanvasGameObject;
		}
	}

	public partial class CreateBasicObject
	{
		public override void Prepare()
		{
			base.Prepare();

			switch (Type)
			{
				case EBasicObjectType.Sprite:
				case EBasicObjectType.AudioSource:
				case EBasicObjectType.Ellipse2D:
				case EBasicObjectType.Polygon2D:
				case EBasicObjectType.Line:
				case EBasicObjectType.Light:
					GameObject = Resources.Load("BasicObjects/" + Type.ToString()) as GameObject;
					break;
				default:
					throw new System.NotSupportedException("type is not supported");
			}
		}
	}

	public partial class CreateUIElement
	{
		public override void Prepare()
		{
			base.Prepare();

			switch (Type)
			{
				case EUIElementType.Canvas:
				case EUIElementType.Text:
				case EUIElementType.Slider:
				case EUIElementType.Image:
				case EUIElementType.RawImage:
				case EUIElementType.Panel:
					GameObject = Resources.Load("UIElements/" + Type.ToString()) as GameObject;
					break;
				default:
					throw new System.NotSupportedException("type is not supported");
			}
			DefaultParentGO = Helper.UserCanvasGameObject;
		}
	}

	public partial class ChangeIsActive
	{
		public override void Prepare()
		{
			base.Prepare();

			IsActive = IsActive ?? true;
		}
	}

	public partial class ChangeVisibility
	{
		public Renderer[] ChildsRenderer { get; set; } // use when referenced gameobject hasn't renderer
		public bool[] ChildsIsVisible { get; set; } // use when referenced gameobject hasn't renderer
	}

	public partial class ChangeTransform
	{
		public override void Prepare()
		{
			base.Prepare();

			ChangeLocal = ChangeLocal ?? true;
		}
	}

	public partial class ChangeAnimatorState
	{
		public int? StateHash { get; set; }


		public override void Prepare()
		{
			base.Prepare();

			Layer = Layer ?? 0;
			NormalizedTime = NormalizedTime ?? 0;
		}
	}

	public partial class ChangeAudioSource
	{
		public AudioClip AudioClip { get; set; }


		public override void Prepare()
		{
			base.Prepare();

			AudioClip = BundleManager.Instance.LoadAsset<AudioClip>(AudioClipAsset);
		}
	}

	public partial class ChangeText
	{
		public TMPro.TMP_FontAsset Font { get; set; }


		public override void Prepare()
		{
			base.Prepare();

			if (FontAsset != null)
			{
				Font = BundleManager.Instance.LoadAsset<TMPro.TMP_FontAsset>(FontAsset);
			}
			else if (FontName != null)
			{
				Font = Helper.GetFont(FontName.ToLower());
			}
		}
	}

	public partial class ChangeImage
	{
		public Sprite Sprite { get; set; }
		public Material Material { get; set; }


		public override void Prepare()
		{
			base.Prepare();

			Sprite = BundleManager.Instance.LoadAsset<Sprite>(SpriteAsset);
			Material = BundleManager.Instance.LoadAsset<Material>(MaterialAsset);
		}
	}

	public partial class ChangeRawImage
	{
		public Texture Texture { get; set; }
		public Material Material { get; set; }


		public override void Prepare()
		{
			base.Prepare();

			Texture = BundleManager.Instance.LoadAsset<Texture>(TextureAsset);
			Material = BundleManager.Instance.LoadAsset<Material>(MaterialAsset);
		}
	}

	public partial class ChangeSprite
	{
		public Sprite Sprite { get; set; }


		public override void Prepare()
		{
			base.Prepare();

			Sprite = BundleManager.Instance.LoadAsset<Sprite>(SpriteAsset);
		}
	}

	public partial class ChangeRenderer
	{
		public Material Material { get; set; }


		public override void Prepare()
		{
			base.Prepare();

			Material = BundleManager.Instance.LoadAsset<Material>(MaterialAsset);
			MaterialIndex = MaterialIndex ?? 0;
		}
	}

	public partial class ChangePolygon2D
	{
		public bool SuddenChange { get; set; } = false;
	}

	public partial class ChangeLine
	{
		public bool SuddenChange { get; set; } = false;
	}

	public partial class ChangeLight
	{
		public Texture Cookie { get; set; }
		public Flare Flare { get; set; }


		public override void Prepare()
		{
			base.Prepare();

			Cookie = BundleManager.Instance.LoadAsset<Texture>(CookieAsset);
			Flare = BundleManager.Instance.LoadAsset<Flare>(FlareAsset);
		}
	}

	public partial class ChangeCamera
	{
		public PostProcessProfile PostProcessProfile { get; set; }


		public override void Prepare()
		{
			base.Prepare();

			Ref = Helper.MainCameraRef;
			ChildRef = null;

			PostProcessProfile = BundleManager.Instance.LoadAsset<PostProcessProfile>(PostProcessProfileAsset);
		}
	}

	public partial class ChangeRenderSettings
	{
		public Light Sun { get; set; }
		public Cubemap CustomReflection { get; set; }
		public Material Skybox { get; set; }


		public override void Prepare()
		{
			base.Prepare();

			BackwardChanges = BackwardChanges ?? false;
			CustomReflection = BundleManager.Instance.LoadAsset<Cubemap>(CustomReflectionAsset);
			Skybox = BundleManager.Instance.LoadAsset<Material>(SkyboxAsset);
		}
	}

    public partial class ChangeParadoxGraph
    {
        private static readonly Dictionary<EParadoxGraphType, Type> COMPONENT_TYPE = new Dictionary<EParadoxGraphType, Type>()
        {
            { EParadoxGraphType.Flow, typeof(FlowCanvas.FlowScriptController) },
            { EParadoxGraphType.BehaviourTree, typeof(NodeCanvas.BehaviourTrees.BehaviourTreeOwner) },
            { EParadoxGraphType.FSM, typeof(NodeCanvas.StateMachines.FSMOwner) },
        };

        public Type ComponentType => COMPONENT_TYPE[Type.Value];

        public Graph Graph { get; set; }
        public List<FSMState> FSMStates { get; set; }


        public override void Prepare()
        {
            base.Prepare();

            Graph = BundleManager.Instance.LoadAsset<Graph>(GraphAsset);
        }
    }

	public partial class ChangeParadoxFSM
	{
		public List<FSMState> States { get; set; }
	}

	public partial class ChangeParadoxBlackboard
	{
		public object EndValue { get; set; }
	}

    // New Actions
    public class AgentJoined : BaseAction
	{
		public new const string NameStatic = "AgentJoined";
		public override string Name() => "AgentJoined";

		public string Team { get; set; }
		public string Side { get; set; }
		public string AgentName { get; set; }
	}

	public class AgentLeft : BaseAction
	{
		public new const string NameStatic = "AgentLeft";
		public override string Name() => "AgentLeft";

		public string Side { get; set; }
		public string AgentName { get; set; }
		public bool IsLeft { get; set; } = true;
	}

	public class EndGame : BaseAction
	{
		public new const string NameStatic = "EndGame";
		public override string Name() => "EndGame";

		public GameObject EndGamePanel { get; set; }
	}
}
