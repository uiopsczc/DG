using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace DG
{
	public static class System_Xml_XmlNode_Extension
	{
		public static string GetNodeValue(this XmlNode self, string defaultValue)
		{
			return XmlNodeUtil.GetNodeValue(self, defaultValue);
		}

		public static bool SetNodeValue(this XmlNode self, string value)
		{
			return XmlNodeUtil.SetNodeValue(self, value);
		}

		public static string GetNodeCDataValue(this XmlNode self, string defaultValue)
		{
			return XmlNodeUtil.GetNodeCDataValue(self, defaultValue);
		}

		public static bool SetNodeCDataValue(this XmlNode self, string value)
		{
			return XmlNodeUtil.SetNodeCDataValue(self, value);
		}

		public static XmlAttribute GetNodeAttr(this XmlNode self, string name)
		{
			return XmlNodeUtil.GetNodeAttr(self, name);
		}

		public static string GetNodeAttrValue(this XmlNode self, string name, string defaultValue)
		{
			return XmlNodeUtil.GetNodeAttrValue(self, name, defaultValue);
		}

		public static Dictionary<string, string> GetNodeAttrs(this XmlNode self)
		{
			return XmlNodeUtil.GetNodeAttrs(self);
		}

		public static bool SetNodeAttrValue(this XmlNode self, string name, string value)
		{
			return XmlNodeUtil.SetNodeAttrValue(self, name, value);
		}

		public static XmlNode GetChildNode(this XmlNode self, string name)
		{
			return XmlNodeUtil.GetChildNode(self, name);
		}

		public static XmlNode GetChildNode(this XmlNode self, int pos)
		{
			return XmlNodeUtil.GetChildNode(self, pos);
		}

		public static XmlNode AddChildNode(this XmlNode self, string name, string value)
		{
			return XmlNodeUtil.AddChildNode(self, name, value);
		}

		public static void AddChildNode(this XmlNode self, Hashtable hashtable)
		{
			XmlNodeUtil.AddChildNode(self, hashtable);
		}
	}
}