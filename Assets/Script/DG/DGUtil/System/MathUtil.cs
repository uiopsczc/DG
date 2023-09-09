using System;
using UnityEngine;

namespace DG
{
	public static class MathUtil
	{
		public static Vector3 GetPoint(Vector3 sourcePoint, Vector3 dir, float distance)
		{
			dir.Normalize();
			return sourcePoint + dir * distance;
		}

		public static Vector2 GetPoint(Vector2 sourcePoint, Vector2 dir, float distance)
		{
			dir.Normalize();
			return sourcePoint + dir * distance;
		}

		//dir1��dir2�ļнǣ�˳ʱ�뷽��Ϊ������ʱ�뷽��Ϊ��
		public static float GetDiffAngle(Vector2 dir1, Vector2 dir2)
		{
			dir1.Normalize();
			dir2.Normalize();
			var cos = Mathf.Clamp(Vector2.Dot(dir1, dir2), -1, 1);
			var diffAngle = Mathf.Acos(cos) * Mathf.Rad2Deg; //�н�
			var sin = dir1.x * dir2.y - dir2.x * dir1.y;
			if (sin > 0)
				diffAngle = -diffAngle;
			return diffAngle;
		}


//		public static Line GetLine(Vector2 endPoint, Vector2 dir, float x)
//		{
//			var k = (endPoint.x - x) / dir.x;
//			var y = endPoint.y - k * dir.y;
//			var sourcePoint = new Vector2(x, y);
//			return new Line(sourcePoint, endPoint);
//		}

		public static float Area(Vector2 point1, Vector2 point2, Vector2 point3)
		{
			var point1point2 = point2 - point1;
			var point1point3 = point3 - point1;
			float cross = point1point2.Cross(point1point3);
			return Math.Abs(cross);
		}

		//��ȡ�����2�η�
		public static int GetNearestPowerOf2(int num)
		{
			return (int)(Mathf.Pow(2, Mathf.Ceil(Mathf.Log(num) / Mathf.Log(2))));
		}

		//�ж�һ�����Ƿ�2�Ĵη�
		public static bool IsPowerOf2(int num)
		{
			int i = 1;
			while (true)
			{
				if (i > num)
					return false;
				if (i == num)
					return true;
				i = i * 2;
			}
		}

		// ���Լ��
		public static int GetGCD(int a, int b)
		{
			if (a < b)
			{
				int t = a;
				a = b;
				b = t;
			}

			while (b > 0)
			{
				int t = a % b;
				a = b;
				b = t;
			}

			return a;
		}

		//����a*a+b*b�Ŀ���
		public static float Hypotenuse(params float[] args)
		{
			return Mathf.Sqrt(HypotenuseSquare(args));
		}

		//����a*a+b*b
		public static float HypotenuseSquare(params float[] args)
		{
			float result = 0;
			foreach (var arg in args)
				result += arg * arg;
			return result;
		}

		#region �������

		//�������
		/// <summary>
		///   n!:�׳�
		///   6��=6x5x4x3x2x1
		/// </summary>
		public static long Factorial(long n)
		{
			long nFac = 1;
			for (var i = n; i >= 1; i--)
				nFac *= i;
			return nFac;
		}

		/// <summary>
		///   ����:��n����ͬԪ���У���ȡm(m��n,m��n��Ϊ��Ȼ��,��ͬ����Ԫ�ذ���һ����˳���ų�һ�У�������n����ͬԪ����ȡ��m��Ԫ�ص�һ������
		///   A(m,n����n��ȡm��Ԫ�ص����е����и���
		///   A(m,n)= n!/(n-m)!
		///   6��=6x5x4x3x2x1
		/// </summary>
		public static long A(long m, long n)
		{
			var nFac = Factorial(n);
			var nmFac = Factorial(n - m);
			return nFac / nmFac;
		}

		/// <summary>
		///   ��ϣ���n����ͬԪ���У���ȡm(m��n����Ԫ�ز���һ�飬������n����ͬԪ����ȡ��m��Ԫ�ص�һ�����
		///   C(m,n����n��ȡm��Ԫ�ص���ϵ����и���
		///   C(m,n)=A(m,n)/m��=n!/[m!*(n-m)!]
		///   6��=6x5x4x3x2x1
		/// </summary>
		/// <returns></returns>
		public static long C(long m, long n)
		{
			return A(m, n) / Factorial(m);
		}

		#endregion

		#region LerpByT,SlerpByT

		public static Vector3 LerpByT(Vector3 from, Vector3 to, float t)
		{
			if (t <= 0)
				return from;
			if (t >= 1) return to;
			return t * to + (1 - t) * from;
		}

		public static Vector3 SlerpByT(Vector3 a, Vector3 b, float t)
		{
			if (t <= 0)
				return a;
			if (t >= 1) return b;


			var v = RotateTo(a, b, Vector3.Angle(a, b) * t);

			//�����ĳ��ȣ������Բ�ֵһ������
			var length = b.magnitude * t + a.magnitude * (1 - t);
			return v.normalized * length;
		}

		/// <summary>
		///   ������from������to��ת�Ƕ�angle
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <param name="angle"></param>
		/// <returns></returns>
		public static Vector3 RotateTo(Vector3 from, Vector3 to, float angle)
		{
			//����������Ƕ�Ϊ0
			if (Vector3.Angle(from, to) == 0) return from;

			//��ת��
			var n = Vector3.Cross(from, to);

			//��ת��淶��
			n.Normalize();

			//��ת����
			var rotateMatrix = new Matrix4x4();

			//��ת�Ļ���
			var radian = angle * Math.PI / 180;
			var cosAngle = (float)Math.Cos(radian);
			var sinAngle = (float)Math.Sin(radian);

			//���������
			//���￴���������п��վ���֪ʶ
			rotateMatrix.SetRow(0,
				new Vector4(n.x * n.x * (1 - cosAngle) + cosAngle, n.x * n.y * (1 - cosAngle) + n.z * sinAngle,
					n.x * n.z * (1 - cosAngle) - n.y * sinAngle, 0));
			rotateMatrix.SetRow(1,
				new Vector4(n.x * n.y * (1 - cosAngle) - n.z * sinAngle, n.y * n.y * (1 - cosAngle) + cosAngle,
					n.y * n.z * (1 - cosAngle) + n.x * sinAngle, 0));
			rotateMatrix.SetRow(2,
				new Vector4(n.x * n.z * (1 - cosAngle) + n.y * sinAngle, n.y * n.z * (1 - cosAngle) - n.x * sinAngle,
					n.z * n.z * (1 - cosAngle) + cosAngle, 0));
			rotateMatrix.SetRow(3, new Vector4(0, 0, 0, 1));

			var v = new Vector4(from.x, from.y, from.z, 0);
			var vector = new Vector3();
			for (var i = 0; i < 3; ++i)
				for (var j = 0; j < 3; j++)
					vector[i] += v[j] * rotateMatrix[j, i];
			return vector;
		}

		/// <summary>
		/// ����ƽ���м䶸��
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public static float Hermite(float start, float end, float value)
		{
			return Mathf.Lerp(start, end, value * value * (3.0f - 2.0f * value));
		}

		public static Vector3 Hermite(Vector3 start, Vector3 end, Vector3 value)
		{
			return new Vector3(Hermite(start.x, end.x, value.x), Hermite(start.z, end.z, value.z),
				Hermite(start.x, end.x, value.x));
		}

		#endregion

	}
}

