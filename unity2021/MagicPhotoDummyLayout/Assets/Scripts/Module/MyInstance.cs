

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using LibMVCS = XTC.FMP.LIB.MVCS;
using XTC.FMP.MOD.MagicPhotoDummyLayout.LIB.Proto;
using XTC.FMP.MOD.MagicPhotoDummyLayout.LIB.MVCS;
using Live2D.Cubism.Extend;
using System.IO;

namespace XTC.FMP.MOD.MagicPhotoDummyLayout.LIB.Unity
{
    /// <summary>
    /// 实例类
    /// </summary>
    public class MyInstance : MyInstanceBase
    {

        public MyInstance(string _uid, string _style, MyConfig _config, MyCatalog _catalog, LibMVCS.Logger _logger, Dictionary<string, LibMVCS.Any> _settings, MyEntryBase _entry, MonoBehaviour _mono, GameObject _rootAttachments)
            : base(_uid, _style, _config, _catalog, _logger, _settings, _entry, _mono, _rootAttachments)
        {
        }

        /// <summary>
        /// 当被创建时
        /// </summary>
        /// <remarks>
        /// 可用于加载主题目录的数据
        /// </remarks>
        public void HandleCreated()
        {
            var builder = new Builder();
            builder.Loader = this.loader;

            string model3JsonFile = "D:/rabbit-1/rabbit1.model3.json";
            var model = builder.BuildModel(model3JsonFile, (_model3Json) =>
            {
                Debug.Log(_model3Json.FileReferences.Moc);
            });
            Debug.Log(model);

            string motionFile = "D:/rabbit-1/motion/idle.motion3.json";
            var motion = builder.BuildMotion(motionFile);
            motion.legacy = true;
            Debug.Log(motion);

            var animation = model.AddComponent<Animation>();
            animation.wrapMode = WrapMode.Loop;
            animation.AddClip(motion, "idle.motion3");
            animation.Play("idle.motion3");
        }

        /// <summary>
        /// 当被删除时
        /// </summary>
        public void HandleDeleted()
        {
        }

        /// <summary>
        /// 当被打开时
        /// </summary>
        /// <remarks>
        /// 可用于加载内容目录的数据
        /// </remarks>
        public void HandleOpened(string _source, string _uri)
        {
            rootUI.gameObject.SetActive(true);
            rootWorld.gameObject.SetActive(true);
        }

        /// <summary>
        /// 当被关闭时
        /// </summary>
        public void HandleClosed()
        {
            rootUI.gameObject.SetActive(false);
            rootWorld.gameObject.SetActive(false);
        }

        private object loader(System.Type _assetType, string _path)
        {
            Debug.LogFormat("Type:{0} Path:{1}", _assetType.ToString(), _path);
            if (_assetType == typeof(byte[]))
            {
                return File.ReadAllBytes(_path);
            }
            else if (_assetType == typeof(string))
            {
                return File.ReadAllText(_path);
            }
            else if (_assetType == typeof(Texture2D))
            {
                var texture = new Texture2D(1, 1);
                texture.LoadImage(File.ReadAllBytes(_path));
                return texture;
            }
            throw new System.NotSupportedException();
        }
    }
}
