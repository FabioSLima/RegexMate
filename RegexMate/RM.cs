using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexMate
{
    public class RM
    {
        private StringBuilder current = new StringBuilder();

        private RegexOptions options = RegexOptions.None;

        public RM AlphaNumeric { get { current.Append("[A-Za-z0-9]"); return this; } }

        public RM Any { get { current.Append("."); return this; } }

        public RM Digit { get { current.Append(@"\d"); return this; } }

        public RM NonDigit { get { current.Append(@"\D"); return this; } }

        public RM Word { get { current.Append(@"\w"); return this; } }

        public RM NonWord { get { current.Append(@"\W"); return this; } }

        public RM Space { get { current.Append(@"\s"); return this; } }

        public RM NonSpace { get { current.Append(@"\S"); return this; } }

        public RM Boundary { get { current.Append(@"\b"); return this; } }

        public RM NonBoundary { get { current.Append(@"\B"); return this; } }


        public RM CarriageReturn { get { current.Append(@"\r"); return this; } }

        public RM FormFeed { get { current.Append(@"\f"); return this; } }

        public RM NewLine { get { current.Append(@"\n"); return this; } }

        public RM Tab { get { current.Append(@"\t"); return this; } }

        public RM VerticalTab { get { current.Append(@"\v"); return this; } }

        

        public RM OneOrMore { get { current.Append("+"); return this; } }

        public RM NoneOrMore { get { current.Append("*"); return this; } }

        public RM AsConditional { get { current.Append(@"?"); return this; } }

        public RM BeginLine { get { current.Append("^"); return this; } }

        public RM EndLine { get { current.Append("$"); return this; } }

        public RM BeginOfString { get { current.Append(@"\A"); return this; } }

        public RM EndOfString { get { current.Append(@"\Z"); return this; } }


        public RM MatchAny(string characters, bool sanitize = true)
        {
            if (string.IsNullOrWhiteSpace(characters))
                throw new ArgumentNullException("characters");

            if (sanitize)
                characters = Sanitize(characters);

            current.Append("[" + characters + "]");
            return this;
        }

        public RM Not(string characters, bool sanitize = true)
        {

            if (string.IsNullOrWhiteSpace(characters))
                throw new ArgumentNullException("characters");

            if (sanitize)
                characters = Sanitize(characters);

            current.Append("[^" + characters + "]");
            return this;
        }

        public RM WithOctal(string octalCharacter)
        {
            if (octalCharacter.Length <= 1 || octalCharacter.Length > 3)
                throw new ArgumentException("OctalCharacter must contains 2 or 3 characters");

            current.Append(@"\" + octalCharacter);
            return this;
        }

        public RM WithDecimal(string hexaCharacter)
        {
            if (hexaCharacter.Length != 2)
                throw new ArgumentException("HexadecimalCharacter must consists of exactly two characters");

            current.Append(@"\x" + hexaCharacter);
            return this;
        }

        public RM WithUnicode(string unicodeCharacter)
        {
            if (unicodeCharacter.Length != 4)
                throw new ArgumentException("unicodeCharacter must consists of exactly four characters");

            current.Append(@"\u" + unicodeCharacter);
            return this;
        }


        public RM Exactly(int times)
        {
            if (times <= 0)
                throw new ArgumentException("Invalid number of times");

            current.Append("{" + times + "}");
            return this;
        }

        public RM AtLeast(int times)
        {
            if (times <= 0)
                throw new ArgumentException("Invalid number of times");

            current.Append("{" + times + ",}");
            return this;
        }

        public RM AtMost(int times)
        {
            if (times <= 0)
                throw new ArgumentException("Invalid number of times");

            current.Append("{1," + times + "}");
            return this;
        }

        public RM Between(int initial, int final)
        {
            if (initial < 0)
                throw new ArgumentException("Initial is invalid");

            if (final > initial)
                throw new ArgumentException("Final cannot be bigger than initial");

            current.Append("{" + initial + "," + final + "}");
            return this;
        }

        private string Sanitize(string input)
        {
            return input.Replace("[", @"\[").Replace("]", @"\]").Replace("(", @"\(").Replace(")", @"\)");
        }

        public RM AsCompiled()
        {
            options = options | RegexOptions.Compiled;
            return this;
        }

        public RM AsExplicitCapture()
        {
            options = options | RegexOptions.ExplicitCapture;
            return this;
        }

        public RM IgnoreCase()
        {
            options = options | RegexOptions.IgnoreCase;
            return this;
        }

        public RM IgnorePatternWhitespace()
        {
            options = options | RegexOptions.IgnorePatternWhitespace;
            return this;
        }

        public RM AsMultiLine()
        {
            options = options | RegexOptions.Multiline;
            return this;
        }

        public RM AsSingleLine()
        {
            options = options | RegexOptions.Singleline;
            return this;
        }

        public Regex Build()
        {
            return new Regex(current.ToString());
        }
    }
}
