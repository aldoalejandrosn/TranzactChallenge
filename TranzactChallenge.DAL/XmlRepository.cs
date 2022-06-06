using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace TranzactChallenge.DAL
{
	public abstract class XmlRepository<T>
	{
		#region Fields
		private readonly string filename;
		#endregion

		#region Constructors
		public XmlRepository(string filename)
		{
			if (filename != default) this.filename = Path.Combine(path1: AppDomain.CurrentDomain.BaseDirectory, path2: filename);
			else throw new ArgumentNullException(paramName: nameof(filename));
		}
		#endregion

		#region Methods - Basic
		public IEnumerable<T> SelectAll()
		{
			XmlDocument document = new XmlDocument();
			document.Load(filename);
			XmlNode firstChild = document.ChildNodes.Cast<XmlNode>().FirstOrDefault(predicate: i => i.NodeType == XmlNodeType.Element);
			if (firstChild != default)
			{
				foreach (XmlNode node in firstChild.ChildNodes)
					yield return ReadFromXmlNode(node);
			}
		}
		#endregion

		#region Methods - Extensions
		protected abstract T ReadFromXmlNode(XmlNode node);
		#endregion
	}
	public static class XmlRepositoryUtils
	{
		public static byte? GetByteValue(this XmlAttributeCollection attributes, string name)
		{
			string value = GetValue(attributes, name);
			if (!string.IsNullOrEmpty(value))
				return byte.TryParse(s: value, out byte result) ? result : throw new ArgumentException(message: "Value is not a valid integer.");
			else return default;
		}
		public static decimal? GetDecimalValue(this XmlAttributeCollection attributes, string name)
		{
			string value = GetValue(attributes, name);
			if (!string.IsNullOrEmpty(value))
				return decimal.TryParse(s: value, out decimal result) ? result : throw new ArgumentException(message: "Value is not a valid decimal.");
			else return default;
		}
		public static string GetValue(this XmlAttributeCollection attributes, string name)
		{
			if (attributes.Cast<XmlAttribute>().FirstOrDefault(predicate: i => string.Equals(a: i.Name, b: name, comparisonType: StringComparison.OrdinalIgnoreCase)) is XmlAttribute attribute && attribute.Value is string value)
			{
				value = value?.Trim();
				return !string.IsNullOrEmpty(value) ? value : default;
			}
			else return default;
		}
	}
}