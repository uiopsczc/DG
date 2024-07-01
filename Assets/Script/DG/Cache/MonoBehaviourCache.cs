using UnityEngine;

namespace DG
{
	/// <summary>
	/// Mono的缓存数据
	/// 在对应的MonoBehaviour中需要有这个属性
	///  private MONO_BEHAVIOUR_CACHE _MonoBehaviourCache;
	///  public MONO_BEHAVIOUR_CACHE MONO_BEHAVIOUR_CACHE { get { if (_MonoBehaviourCache == null) _MonoBehaviourCache = new MONO_BEHAVIOUR_CACHE(this); return _MonoBehaviourCache; } }
	///  </summary>
	public class MonoBehaviourCache : Cache
	{
		#region field

		protected MonoBehaviour _owner;

		#endregion


		#region property

		//与Component中的过时组件对应
		public GameObject gameObject => dict.GetOrAddByDefaultFunc(typeof(GameObject), () => _owner.gameObject);

		public Rigidbody rigidbody => dict.GetOrAddByDefaultFunc(typeof(Rigidbody), () => _owner.GetComponent<Rigidbody>());

		public Rigidbody2D rigidbody2D =>
			dict.GetOrAddByDefaultFunc(typeof(Rigidbody2D), () => _owner.GetComponent<Rigidbody2D>());

		public Camera camera => dict.GetOrAddByDefaultFunc(typeof(Camera), () => _owner.GetComponent<Camera>());

		public Light light => dict.GetOrAddByDefaultFunc(typeof(Light), () => _owner.GetComponent<Light>());

		public Animation animation => dict.GetOrAddByDefaultFunc(typeof(Animation), () => _owner.GetComponent<Animation>());

		public ConstantForce constantForce =>
			dict.GetOrAddByDefaultFunc(typeof(ConstantForce), () => _owner.GetComponent<ConstantForce>());

		public Renderer renderer => dict.GetOrAddByDefaultFunc(typeof(Renderer), () => _owner.GetComponent<Renderer>());

		public AudioSource audio => dict.GetOrAddByDefaultFunc(typeof(AudioSource), () => _owner.GetComponent<AudioSource>());

		//  public GUIElement guiElement { get { return dict.GetOrAddByDefaultFunc(typeof(GUIElement), () => { return owner.GetComponent<GUIElement>(); }); } }
		public Collider collider => dict.GetOrAddByDefaultFunc(typeof(Collider), () => _owner.GetComponent<Collider>());

		public Collider2D collider2D =>
			dict.GetOrAddByDefaultFunc(typeof(Collider2D), () => _owner.GetComponent<Collider2D>());

		public HingeJoint hingeJoint =>
			dict.GetOrAddByDefaultFunc(typeof(HingeJoint), () => _owner.GetComponent<HingeJoint>());

		public Transform transform => dict.GetOrAddByDefaultFunc(typeof(Transform), () => _owner.GetComponent<Transform>());

		public ParticleSystem particleSystem =>
			dict.GetOrAddByDefaultFunc(typeof(ParticleSystem), () => _owner.GetComponent<ParticleSystem>());

		public RectTransform rectTransform =>
			dict.GetOrAddByDefaultFunc(typeof(RectTransform), () => _owner.GetComponent<RectTransform>());

		public Animator animator => dict.GetOrAddByDefaultFunc(typeof(Animator), () => _owner.GetComponent<Animator>());

		public BoxCollider boxCollider =>
			dict.GetOrAddByDefaultFunc(typeof(BoxCollider), () => _owner.GetComponent<BoxCollider>());

		public SpriteRenderer spriteRenderer =>
			dict.GetOrAddByDefaultFunc(typeof(SpriteRenderer), () => _owner.GetComponent<SpriteRenderer>());

		#endregion

		#region ctor

		public MonoBehaviourCache(MonoBehaviour owner)
		{
			this._owner = owner;
		}

		#endregion
	}
}