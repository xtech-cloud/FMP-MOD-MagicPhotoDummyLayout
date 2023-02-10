
using System.Collections.Generic;
using UnityEngine;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.MagicPhotoDummyLayout.LIB.MVCS;
using Live2D.Cubism.Extend;
using UnityEditor.UIElements;

namespace XTC.FMP.MOD.MagicPhotoDummyLayout.LIB.Unity
{
    /// <summary>
    /// 运行时类
    /// </summary>
    ///<remarks>
    /// 存储模块运行时创建的对象
    ///</remarks>
    public class MyRuntime : MyRuntimeBase
    {
        public MyRuntime(MonoBehaviour _mono, MyConfig _config, MyCatalog _catalog, Dictionary<string, LibMVCS.Any> _settings, LibMVCS.Logger _logger, MyEntryBase _entry)
            : base(_mono, _config, _catalog, _settings, _logger, _entry)
        {
        }


        public override void ProcessRoot(GameObject _root, Transform _uiSlot, Transform _worldSlot)
        {
            base.ProcessRoot(_root, _uiSlot, _worldSlot);
            List<Material> live2D_BuiltinMaterials = new List<Material>();
            foreach(var renderer in rootAttachment.transform.Find("Live2DBuiltinMaterials").GetComponentsInChildren<MeshRenderer>())
            {
                var material = renderer.material;
                material.name = renderer.gameObject.name;
                live2D_BuiltinMaterials.Add(material);
            }
            Builder.RegisterBuiltinMaterial(live2D_BuiltinMaterials);
        }
    }
}

