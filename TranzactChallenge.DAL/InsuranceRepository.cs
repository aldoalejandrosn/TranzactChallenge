using System;
using System.Xml;
using TranzactChallenge.Entities;

namespace TranzactChallenge.DAL
{
	public sealed class InsuranceRepository : XmlRepository<Insurance>
	{
		#region Constructors
		public InsuranceRepository() : base(filename: "Insurances.xml") { }
		#endregion

		#region Methods
		protected override Insurance ReadFromXmlNode(XmlNode node)
		{
			XmlAttributeCollection attributes = node.Attributes;
			return new Insurance()
			{
				Carrier = attributes.GetValue(name: nameof(Insurance.Carrier)) ?? throw new Exception(message: "Carrier value has not been provided or is invalid.."),
				Plans = PlanUtils.ParseMultiple(value: attributes.GetValue(name: nameof(Insurance.Plans))) ?? throw new Exception(message: "Plans have not been provided."),
				State = State.Build(code: attributes.GetValue(name: nameof(Insurance.State))) ?? throw new Exception(message: "State has not been provided."),
				MonthOfBirth = attributes.GetValue(name: nameof(Insurance.MonthOfBirth)) is string monthOfBirth && MonthUtils.TryParse(value: monthOfBirth, out Month month) ? month : throw new Exception(message: "Month has not been provided or is invalid."),
				AgeRangeFrom = attributes.GetByteValue(name: nameof(Insurance.AgeRangeFrom)) ?? throw new Exception(message: "Age range has not been provided or is invalid.."),
				AgeRangeTo = attributes.GetByteValue(name: nameof(Insurance.AgeRangeTo)) ?? throw new Exception(message: "Age range has not been provided or is invalid.."),
				Premium = attributes.GetDecimalValue(name: nameof(Insurance.Premium)) ?? throw new Exception(message: "Premium value has not been provided or is invalid..")
			};
		}
		#endregion
	}
}