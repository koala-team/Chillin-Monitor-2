using Koala;
using UnityEngine;

namespace KS.SceneActions
{
	public partial class BaseAction
	{
		public string FullRef
		{
			get { return Ref.ToString() + (ChildRef != null ? "/" + ChildRef : ""); }
		}


		public virtual void Prepare() { }
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

	public partial class BaseCreation
	{
		public GameObject GameObject { get; set; }
		public GameObject DefaultParent { get; set; }

		public string FullParentRef
		{
			get { return ParentRef.ToString() + (ParentChildRef != null ? "/" + ParentChildRef : ""); }
		}


		public override void Prepare()
		{
			base.Prepare();

			DefaultParent = Helper.RootGameObject;
		}
	}

	public partial class InstantiateBundleAsset
	{
		public override void Prepare()
		{
			base.Prepare();

			GameObject = BundleManager.Instance.LoadAsset<GameObject>(Asset);
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
				case EUIElementType.RawImage:
				case EUIElementType.Panel:
					GameObject = Resources.Load("UIElements/" + Type.ToString()) as GameObject;
					break;
				default:
					throw new System.NotSupportedException("type is not supported");
			}
			DefaultParent = Helper.UserCanvasGameObject;
		}
	}

	public partial class Destroy
	{
		public Transform Parent { get; set; }
	}

	public partial class ChangeVisibility
	{
		public Renderer[] ChildsRenderer { get; set; } // use when referenced gameobject hasn't renderer
		public bool[] ChildsIsVisible { get; set; } // use when referenced gameobject hasn't renderer
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

	public partial class ChangeMaterial
	{
		public Material Material { get; set; }


		public override void Prepare()
		{
			base.Prepare();

			Index = Index ?? 0;
			Material = BundleManager.Instance.LoadAsset<Material>(MaterialAsset);
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
}
