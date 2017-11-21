using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PzdrAimaiKensaku
{
	/// <summary>
	/// なんか面白そうだったのでパズドラの ver12.4 アプデで来た「あいまい検索」のもどきロジックを組んでみた。
	/// </summary>
	class Program
	{
		static void Main( string[] args )
		{
			try
			{
				AimaiKensaku();
			}
			catch ( Exception ex )
			{
				Console.WriteLine( ex.Message );
			}
			
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine( "何か押したら終了するぜ！" );
			Console.ReadKey();
		}

		/// <summary>
		/// パズドラのあいまい検索もどき
		/// </summary>
		private static void AimaiKensaku()
		{
			string xml = File.ReadAllText("Data.xml");
			List<Dat.KensakuCharactor> charactors = Dat.Parse(xml).ToList();

			Console.WriteLine( "■データ■■■■■■" );
			foreach ( var x in charactors )
			{
				Console.WriteLine( x.name );
			}
			Console.WriteLine( "■■■■■■■■■■" );




			Console.WriteLine();
			Console.WriteLine( "けんさく　する　わーど　を　にゅうりょく　しくされ。" );
			Console.Write( "わーど：" );

			string input = Console.ReadLine();
			string input_kc = input.normalizeKC().translate().ToLower();

			
			Console.WriteLine();
			Console.WriteLine( "  検索：" + input );
			Console.WriteLine( "  補正：" + input_kc );



			
			Console.WriteLine();
			Console.WriteLine( "■検索結果■" );

			var likes = Aimai( charactors, input_kc ).ToList();

			if ( 0 == likes.Count )
			{
				Console.WriteLine( "(*'▽')　ざんねん、いねーよ！！" );
			}
			else
			{
				foreach ( var x in likes )
				{
					Console.WriteLine( x.name );
				}
			}
		}

		private static IEnumerable<Dat.KensakuCharactor> Aimai( List<Dat.KensakuCharactor> charactors, string kensaku )
		{
			foreach ( var x in charactors )
			{
				if ( Like( x.nameKensaku, kensaku )
				  || Like( x.alterKensaku, kensaku )
				  || Like( x.name, kensaku )
				  || Like( x.alter, kensaku ) )
				{
					yield return x;
				}
			}
		}
		
		/// <summary>
		/// パズドラ風あいまい検索
		/// </summary>
		private static bool Like( string text, string word )
		{
			// そもそも文字数が足りないのは検索するまでもない。
			if ( text.Length < word.Length ) return false;


			int index = 0;
			int tail = word.Length - 1;

			foreach ( char c in text )
			{
				if ( c == word[index] )
				{
					if ( tail == index )
					{
						// 検索ワードの終端に到達した場合、あいまい検索成立。
						return true;
					}
					else
					{
						// 検索ワードの終端でない場合、インデックスを進める。
						index++;
					}
				}
			}

			// 最後まで来ちゃったら検索ワードヒットせず。
			return false;
		}
	}
}
