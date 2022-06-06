using System;
using System.Xml;
using TranzactChallenge.Entities;

namespace TranzactChallenge.DAL
{
	public sealed class StateRepository : XmlRepository<State>
	{
		#region Constructors
		public StateRepository() : base(filename: "States.xml") { }
		#endregion

		#region Methods
		protected override State ReadFromXmlNode(XmlNode node)
		{
			XmlAttributeCollection attributes = node.Attributes;
			return new State()
			{
				Code = attributes.GetValue(name: nameof(State.Code)) ?? throw new ArgumentException(message: "Code has not been provided."),
				Name = attributes.GetValue(name: nameof(State.Name)) ?? throw new ArgumentException(message: "Name has not been provided.")
			};
		}
		#endregion
	}
}