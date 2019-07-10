using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Conjure.Data.Client
{

    public class BindTarget : IBindTarget
    {
        private readonly object _root;
        private readonly string _path;
        private readonly PropertyInfo[] _props;

        public BindTarget(object root, string path)
        {
            _root = root;
            _path = path;
            _props = ResolvePath(_root, _path);
        }

        public Type ValueType => throw new NotImplementedException();

        public bool IsReadOnly => _props == null
            || !(ResolvePathFinal(_root, _props)?.CanWrite ?? false);

        public object Value
        {
            get => TryGetValue(_root, _props, out var value) ? value : null;
            set => TrySetValue(_root, _props, value);
        }

        public static PropertyInfo[] ResolvePath(object root, string path)
        {
            return ResolvePath(root, root.GetType(), path);
        }

        public static PropertyInfo[] ResolvePath(object root, Type rootType, string path)
        {
            var paths = path.Split('.');
            var type = rootType;
            var props = new List<PropertyInfo>();

            foreach (var pn in paths)
            {
                if (type == null)
                    return null;

                var p = type.GetProperty(pn, BindingFlags.Public | BindingFlags.Instance);
                if (p == null)
                    return null;

                props.Add(p);
                type = p.PropertyType;
            }

            return props.ToArray();
        }

        public static PropertyInfo ResolvePathFinal(object root, PropertyInfo[] props)
        {
            var val = root;
            PropertyInfo lastProp = null;
            foreach (var p in props)
            {
                if (val == null)
                    return null;

                val = p.GetValue(val);
                lastProp = p;
            }
            return lastProp;
        }

        public static bool TryGetValue(object root, PropertyInfo[] props, out object value)
        {
            value = root;
            if (props == null)
                return false;

            foreach (var p in props)
            {
                if (value == null)
                    return false;

                value = p.GetValue(value);
            }
            return true;
        }

        public static bool TrySetValue(object root, PropertyInfo[] props, object newValue)
        {
            var value = root;
            if (props == null)
                return false;

            var lastPropsIndex = props.Length - 1;
            for (var i = 0; i < lastPropsIndex; ++i)
            {
                var p = props[i];
                if (value == null)
                    return false;

                value = p.GetValue(value);
            }

            if (value != null)
            {
                props[lastPropsIndex].SetValue(value, newValue);
                return true;
            }

            return false;
        }
    }
}
