using UnityEngine;

namespace DG
{
	/// <summary>
	/// Mono的缓存数据
	/// 在对应的MonoBehaviour中需要有这个属性
	///  private MonoBehaviourCache _MonoBehaviourCache;
	///  public MonoBehaviourCache MonoBehaviourCache { get { if (_MonoBehaviourCache == null) _MonoBehaviourCache = new MonoBehaviourCache(this); return _MonoBehaviourCache; } }
	///  </summary>
	public class MonoBehaviourCache : Cache
	{
		#region field

		protected MonoBehaviour _owner;

		#endregion


		#region property

		//与Component中的过时组件对应
		public GameObject gameObject => dict.GetOrAddDefault(typeof(GameObject), () => _owner.gameObject);

		public Rigidbody rigidbody => dict.GetOrAddDefault(typeof(Rigidbody), () => _owner.GetComponent<Rigidbody>());

		public Rigidbody2D rigidbody2D =>
			dict.GetOrAddDefault(typeof(Rigidbody2D), () => _owner.GetComponent<Rigidbody2D>());

		public Camera camera => dict.GetOrAddDefault(typeof(Camera), () => _owner.GetComponent<Camera>());

		public Light light => dict.GetOrAddDefault(typeof(Light), () => _owner.GetComponent<Light>());

		public Animation animation => dict.GetOrAddDefault(typeof(Animation), () => _owner.GetComponent<Animation>());

		public ConstantForce constantForce =>
			dict.GetOrAddDefault(typeof(ConstantForce), () => _owner.GetComponent<ConstantForce>());

		public Renderer renderer => dict.GetOrAddDefault(typeof(Renderer), () => _owner.GetComponent<Renderer>());

		public AudioSource audio => dict.GetOrAddDefault(typeof(AudioSource), () => _owner.GetComponent<AudioSource>());

		//  public GUIElement guiElement { get { return dict.GetOrAddDefault(typeof(GUIElement), () => { return owner.GetComponent<GUIElement>(); }); } }
		public Collider collider => dict.GetOrAddDefault(typeof(Collider), () => _owner.GetComponent<Collider>());

		public Collider2D collider2D =>
			dict.GetOrAddDefault(typeof(Collider2D), () => _owner.GetComponent<Collider2D>());

		public HingeJoint hingeJoint =>
			dict.GetOrAddDefault(typeof(HingeJoint), () => _owner.GetComponent<HingeJoint>());

		public Transform transform => dict.GetOrAddDefault(typeof(Transform), () => _owner.GetComponent<Transform>());

		public ParticleSystem particleSystem =>
			dict.GetOrAddDefault(typeof(ParticleSystem), () => _owner.GetComponent<ParticleSystem>());

		public RectTransform rectTransform =>
			dict.GetOrAddDefault(typeof(RectTransform), () => _owner.GetComponent<RectTransform>());

		public Animator animator => dict.GetOrAddDefault(typeof(Animator), () => _owner.GetComponent<Animator>());

		public BoxCollider boxCollider =>
			dict.GetOrAddDefault(typeof(BoxCollider), () => _owner.GetComponent<BoxCollider>());

		public SpriteRenderer spriteRenderer =>
			dict.GetOrAddDefault(typeof(SpriteRenderer), () => _owner.GetComponent<SpriteRenderer>());

		#endregion

		#region ctor

		public MonoBehaviourCache(MonoBehaviour owner)
		{
			this._owner = owner;
		}

		#endregion
	}
}