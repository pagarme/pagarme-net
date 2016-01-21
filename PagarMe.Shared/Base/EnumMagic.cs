//
// EnumMagic.cs
//
// Author:
//       Jonathan Lima <jonathan@pagar.me>
//
// Copyright (c) 2015 Pagar.me
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace PagarMe.Base
{
    internal static class EnumMagic
    {

        private class EnumDescriptor
        {
            private Type _type;
            private Dictionary<object, string> _enum2name;
            private Dictionary<string, object> _name2enum;

            public EnumDescriptor(Type type)
            {
                _type = type;
                _enum2name = new Dictionary<object, string>();
                _name2enum = new Dictionary<string, object>();

                var names = Enum.GetNames(type);

                foreach (string name in names)
                {
                    #if NET40
                    var member = type.GetFields().Single(x => x.Name == name);
                    #elif !PCL
                    var member = type.GetTypeInfo().GetRuntimeFields().Single((x) => x.Name == name);
                    #else
                    var member = type.GetTypeInfo().GetDeclaredField(name);
                    #endif
                    var value = Enum.Parse(type, name);
                    var realName = name;

                    #if NET40
                    var attr = (EnumValueAttribute)member.GetCustomAttributes(typeof(EnumValueAttribute), false).FirstOrDefault();
                    #else
                    var attr = member.GetCustomAttribute<EnumValueAttribute>();
                    #endif

                    if (attr != null)
                        realName = attr.Value;

                    _enum2name[value] = realName;
                    _name2enum[realName] = value;
                }
            }

            public string ConvertToString(object value)
            {
                string result;

                if (_enum2name.TryGetValue(value, out result))
                    return result;

                return value.ToString();
            }

            public object ConvertFromString(string name)
            {
                object result;

                if (_name2enum.TryGetValue(name, out result))
                    return result;

                return Enum.Parse(_type, name);
            }
        }

        private static Dictionary<Type, EnumDescriptor> _descriptors;

        static EnumMagic()
        {
            _descriptors = new Dictionary<Type, EnumDescriptor>();
        }

        private static EnumDescriptor GetDescriptor(Type type)
        {
            lock (_descriptors)
            {
                if (!_descriptors.ContainsKey(type))
                    _descriptors.Add(type, new EnumDescriptor(type));

                return _descriptors[type];
            }
        }

        public static string ConvertToString(object value)
        {
            return GetDescriptor(value.GetType()).ConvertToString(value);
        }

        public static object ConvertFromString(Type type, string name)
        {
            return GetDescriptor(type).ConvertFromString(name);
        }
    }
}

