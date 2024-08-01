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
		public GameObject gameObject => _dict.GetOrAddByDefaultFunc(typeof(GameObject), () => _owner.gameObject);

		public Rigidbody rigidbody =>
			_dict.GetOrAddByDefaultFunc(typeof(Rigidbody), () => _owner.GetComponent<Rigidbody>());

		public Rigidbody2D rigidbody2D =>
			_dict.GetOrAddByDefaultFunc(typeof(Rigidbody2D), () => _owner.GetComponent<Rigidbody2D>());

		public Camera camera => _dict.GetOrAddByDefaultFunc(typeof(Camera), () => _owner.GetComponent<Camera>());

		public Light light => _dict.GetOrAddByDefaultFunc(typeof(Light), () => _owner.GetComponent<Light>());

		public Animation animation =>
			_dict.GetOrAddByDefaultFunc(typeof(Animation), () => _owner.GetComponent<Animation>());

		public ConstantForce constantForce =>
			_dict.GetOrAddByDefaultFunc(typeof(ConstantForce), () => _owner.GetComponent<ConstantForce>());

		public Renderer renderer =>
			_dict.GetOrAddByDefaultFunc(typeof(Renderer), () => _owner.GetComponent<Renderer>());

		public AudioSource audio =>
			_dict.GetOrAddByDefaultFunc(typeof(AudioSource), () => _owner.GetComponent<AudioSource>());

		//  public GUIElement guiElement { get { return _dict.GetOrAddByDefaultFunc(typeof(GUIElement), () => { return owner.GetComponent<GUIElement>(); }); } }
		public Collider collider =>
			_dict.GetOrAddByDefaultFunc(typeof(Collider), () => _owner.GetComponent<Collider>());

		public Collider2D collider2D =>
			_dict.GetOrAddByDefaultFunc(typeof(Collider2D), () => _owner.GetComponent<Collider2D>());

		public HingeJoint hingeJoint =>
			_dict.GetOrAddByDefaultFunc(typeof(HingeJoint), () => _owner.GetComponent<HingeJoint>());

		public Transform transform =>
			_dict.GetOrAddByDefaultFunc(typeof(Transform), () => _owner.GetComponent<Transform>());

		public ParticleSystem particleSystem =>
			_dict.GetOrAddByDefaultFunc(typeof(ParticleSystem), () => _owner.GetComponent<ParticleSystem>());

		public RectTransform rectTransform =>
			_dict.GetOrAddByDefaultFunc(typeof(RectTransform), () => _owner.GetComponent<RectTransform>());

		public Animator animator =>
			_dict.GetOrAddByDefaultFunc(typeof(Animator), () => _owner.GetComponent<Animator>());

		public BoxCollider boxCollider =>
			_dict.GetOrAddByDefaultFunc(typeof(BoxCollider), () => _owner.GetComponent<BoxCollider>());

		public SpriteRenderer spriteRenderer =>
			_dict.GetOrAddByDefaultFunc(typeof(SpriteRenderer), () => _owner.GetComponent<SpriteRenderer>());

		#endregion

		#region ctor

		public MonoBehaviourCache(MonoBehaviour owner)
		{
			this._owner = owner;
		}

		#endregion
	}
}