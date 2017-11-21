using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PzdrAimaiKensaku
{
	public class Dat
	{

		/// <remarks/>
		[System.SerializableAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute( "code" )]
		[System.Xml.Serialization.XmlTypeAttribute( AnonymousType = true )]
		[System.Xml.Serialization.XmlRootAttribute( Namespace = "", IsNullable = false )]
		public partial class Kensaku
		{
			/// <remarks/>
			[System.Xml.Serialization.XmlElementAttribute( "Charactor" )]
			public KensakuCharactor[] Charactor { get; set; }
		}

		/// <remarks/>
		[System.SerializableAttribute()]
		[System.ComponentModel.DesignerCategoryAttribute( "code" )]
		[System.Xml.Serialization.XmlTypeAttribute( AnonymousType = true )]
		public partial class KensakuCharactor
		{

			private string nameField = "";

			[System.Xml.Serialization.XmlIgnore]
			public string nameKensaku { get; private set; } = "";


			private string alterField = "";

			[System.Xml.Serialization.XmlIgnore]
			public string alterKensaku { get; private set; } = "";


			/// <remarks/>
			[System.Xml.Serialization.XmlAttributeAttribute()]
			public string name
			{
				get
				{
					return this.nameField;
				}
				set
				{
					this.nameField = value.normalizeKC();
					this.nameKensaku = this.nameField.translate();
				}
			}

			/// <remarks/>
			[System.Xml.Serialization.XmlAttributeAttribute()]
			public string alter
			{
				get
				{
					return this.alterField;
				}
				set
				{
					this.alterField = value.normalizeKC();
					this.alterKensaku = this.alterField.translate();
				}
			}
		}


		public static IEnumerable<KensakuCharactor> Parse( string xml )
		{
			var reader = new StringReader(xml);

			var serializer = new  XmlSerializer( typeof(Kensaku) );

			var kensaku = serializer.Deserialize(reader) as Kensaku;

			foreach ( var charactor in kensaku.Charactor )
			{
				yield return charactor;
			}

		}
	}
}
