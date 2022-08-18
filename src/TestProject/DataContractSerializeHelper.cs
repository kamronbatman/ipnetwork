﻿// <copyright file="DataContractSerializeHelper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.Net.TestSerialization.NetFramework
{
    public static class DataContractSerializeHelper
    {
        public static string Serialize<T>(T obj, bool formatting = true)
            where T : new()
        {
            if (obj == null)
            {
                return string.Empty;
            }

            var serializer = new DataContractSerializer(typeof(T));
            var settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = formatting,
            };
            using (var textWriter = new StringWriter())
            {
                using (var xmlWriter = XmlWriter.Create(textWriter, settings))
                {
                    serializer.WriteObject(xmlWriter, obj);
                }

                var result = textWriter.ToString();
                return result;
            }
        }

        public static T Deserialize<T>(string xml)
            where T : new()
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                return new T();
            }

            using (var textReader = new StringReader(xml))
            using (var xmlReader = XmlReader.Create(textReader))
            {
                var serializer = new DataContractSerializer(typeof(T));
                var result = (T)serializer.ReadObject(xmlReader);
                return result;
            }
        }
    }
}