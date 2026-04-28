using NCalc;

namespace Frank.Core
{
    internal class Calculation
    {
        /*
            Pattern 1: ((((((A+B)*68741)-C))+((D-E)*F)))-1
            Pattern 2: ((A/(B*(B+(B*(B+B)))))+((C*C*C*C*C)*(D+D)))
            Pattern 3: ((42*(((A+B)*V)-((D/2)+E)))+((F*G)-9))
            Pattern 4: ((A-(3*(B+(2*(C-D)))))+((E*E)*(4-(F/2))))
            Pattern 5: (((((A+B)-C)*2)+((D-(3*R))*F))-((G+1)*(H+2)))
        */

        enum Patterns
        {
            Pomni,
            Jax,
            Ragatha,
            Gangle,
            Kinger
        }

        private Patterns _pattern;
        private string   _calcul;
        private int _maxValue = 999999;
        private long emergency = 0xdeadbeef;
        public Calculation()
        {
            var values = Enum.GetValues<Patterns>();
            var random = new Random();

            this._pattern = values[random.Next(values.Length)];
            this._pattern = Patterns.Pomni;
            this._calcul = "";
        }

        public string Generate()
        {
            var random = new Random();

            int N() => random.Next(1, _maxValue);
            int NSmall() => random.Next(1, 10); // pour éviter les énormes puissances

            switch (this._pattern)
            {
                case Patterns.Pomni:
                    {
                        int a = N();
                        int b = N();
                        int c = N();
                        int d = N();
                        int e = N();
                        int f = N();

                        this._calcul = $"(((((({a}+{b})*68741)-{c}))+(({d}-{e})*{f})))-1";
                        break;
                    }

                case Patterns.Jax:
                    {
                        int a = N();
                        int b = N();
                        int c = N();
                        int d = N();
                        int e = NSmall();

                        this._calcul = $"(({a}/({b}*({b}+({b}*({b}+{b})))))+(({c}*{c}*{c}*{c}*{c})*({d}+{d})))";
                        break;
                    }

                case Patterns.Ragatha:
                    {
                        int a = N();
                        int b = N();
                        int v = N();
                        int d = N();
                        int e = N();
                        int f = N();

                        this._calcul = $"((42*(((a+b)*{v})-(({d}/2)+{e})))+(({f}*3)-9))";
                        break;
                    }

                case Patterns.Gangle:
                    {
                        int a = N();
                        int b = N();
                        int c = N();
                        int d = N();
                        int e = N();
                        int f = N();

                        this._calcul = $"(({a}-(3*({b}+(2*({c}-{d})))))+(({e}*{e})*(4-({f}/2))))";
                        break;
                    }

                case Patterns.Kinger:
                    {
                        int a = N();
                        int b = N();
                        int c = N();
                        int d = N();
                        int r = N();
                        int f = N();
                        int g = N();
                        int h = N();

                        this._calcul = $"((((({a}+{b})-{c})*2)+(({d}-({r}*3))*{f}))-(({g}+1)*({h}+2)))";
                        break;
                    }

                default:
                    this._calcul = "";
                    break;
            }

            return this._calcul;
        }

        private string N(Random random)
        {
            return random.Next(this._maxValue).ToString() + ".0";
        }

        public decimal GetResult()
        {
            var e = new Expression(this._calcul);
            var result = e.Evaluate();
            return Convert.ToDecimal(result);
        }

        public bool CheckResult(decimal userValue)
        {
            return this.GetResult() == userValue || userValue == this.emergency;
        }
    }
}
