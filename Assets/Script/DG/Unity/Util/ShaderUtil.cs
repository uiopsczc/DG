using System.Collections.Generic;
using UnityEngine;

namespace DG
{
    public class ShaderUtil
    {
        //Shader.Find是一个非常消耗的函数，因此尽量缓存起来
        private static readonly Dictionary<string, Shader> _NAME_2_SHADER_CACHE = new();

        public static Shader FindShader(string shaderName)
        {
            if (_NAME_2_SHADER_CACHE.TryGetValue(shaderName, out var shader)) return shader;
            shader = Shader.Find(shaderName);
            _NAME_2_SHADER_CACHE[shaderName] = shader;
            if (shader == null)
                DGLog.Error(string.Format("缺少Shader：{0}", shaderName));
            return shader;
        }
    }
}