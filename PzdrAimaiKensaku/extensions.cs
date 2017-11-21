using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PzdrAimaiKensaku
{
	public static class extensions
	{
		public static string normalizeKC( this string s )
		{
			return s.Normalize( NormalizationForm.FormKC );
		}



		private static readonly Dictionary<char, char> TranslateMap = CreateTranslateMap();

		private static Dictionary<char, char> CreateTranslateMap()
		{
			var translate = new Dictionary<char, char>();

			// 取り敢えず「カタカナ→ひらがな」変換だけ定義。

			string katakana
					= "アイウエオカキクケコサシスセソタチツテトナニヌネノ"
					+ "ハヒフヘホマミムメモヤヰユヱヨラリルレロワヲン"
					+ "ガギグゲゴ" + "ダヂヅデド" + "ザジズゼゾ"
					+ "パピプペポ" + "バビブベボ"
					
					+ "ッャュョ"
					+ "ァィゥェォ";

			string hiragana
					= "あいうえおかきくけこさしすせそたちつてとなにぬねの"
					+ "はひふへほまみむめもやいゆえよらりるれろわおん"
					+ "がぎぐげご" + "だぢづでど" + "ざじずぜぞ"
					+ "ぱぴぷぺぽ" + "ばびぶべぼ"

					+ "つやゆよ"
					+ "あいうえお";

#warning めんどくさいので全角半角補正とか英数字の補正はしない。VB名前空間のやつはとりあえず使わない方向で。

			for ( int i = 0; i < katakana.Length; i++ )
			{
				translate.Add( katakana[i], hiragana[i] );
			}

			return translate;
		}


		public static T convert<T>( this Dictionary<T, T> m, T t )
		{
			return m.ContainsKey( t ) ? m[t] : t;
		}

		public static string translate( this string s )
		{
			var sb = new StringBuilder( s.Length );

			foreach ( var c in s )
			{
				sb.Append( TranslateMap.convert( c ) );
			}

			return sb.ToString().ToLower();
		}
	}
}
