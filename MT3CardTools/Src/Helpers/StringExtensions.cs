using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace MT3CardTools.Src.Helpers
{
    static class StringExtensions
    {
        private static readonly string GAME_CHARS =
            "アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミムメモヤユヨ゛゜ラリルレロワヲン　ッァィゥェォャュョーあいうえおかきくけこさしすせそたちつてとなにぬねのはひふへほまみむめもやゆよ゛" +
            "゜らりるれろわをん　っぁぃぅぇぉゃゅょーＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ１２３４５６７８９０＜＞＋－＊÷＝；：／＼＿｜・＠！？＆★（）＾◇ａｂｃｄｅｆｇｈｉｊｋｌｍｎｏｐｑｒｓ" +
            "ｔｕｖｗｘｙｚガギグゲゴザジズゼゾダヂヅデドバビブベボパピプペポがぎぐげござじずぜぞだぢづでどばびぶべぼぱぴぷぺぽヴ　 ";

        public static string ToFullWidth(this string str) => Regex.Replace(Strings.StrConv(str, VbStrConv.Wide, 1041)
            , $"[^{GAME_CHARS}]", "");
        public static string MakeGameFriendly(this string str) => str.Replace(".", "・");
    }
}
