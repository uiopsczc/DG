namespace DG
{
	//特点：
	//如果没有despawn的，则由currentNumber自动增加1，返回该值，否则取deSpawn中的
	public class IdPool : DGPool<ulong>
	{
		private ulong _currentNumber;

		public IdPool(string poolName = StringConst.STRING_EMPTY) : base(poolName)
		{
		}

		protected override ulong _Spawn()
		{
			_currentNumber++;
			return _currentNumber;
		}

		public void DeSpawnValue(string valueString)
		{
			if(ulong.TryParse(valueString, out var value))
				DeSpawnValue(value);
		}
	}
}