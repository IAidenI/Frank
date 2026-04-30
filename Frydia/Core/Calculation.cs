using NCalc;
using System.Security.Cryptography;
using System.Text;

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
        private decimal _a, _b, _c, _d, _e, _f, _g, _h, _r, _v;
        private string   _calcul;
        
        private UInt32 _runtimeKey;

        public Calculation()
        {
            this._pattern = Patterns.Pomni;
            this._calcul  = "";

            this._runtimeKey = this.Hash(typeof(Calculation).FullName!);
        }

        private Patterns GetRandomPattern()
        {
            var values = Enum.GetValues<Patterns>();
            var random = new Random();
            return values[random.Next(values.Length)];
        }

        private UInt32 Hash(string data)
        {
            using var sha = SHA256.Create();
            var h = sha.ComputeHash(Encoding.UTF8.GetBytes(data));
            return BitConverter.ToUInt32(h, 0);
        }

        private uint Cipher(uint input)
        {
            uint x = input;
            x ^= this._runtimeKey;
            x = (x << 7) | (x >> 25);
            x += 0X41424344;
            x ^= 0x9e3779b9;
            return x;
        }

        public bool CheckEmergency(long input)
        {
            if (input < int.MinValue || input > uint.MaxValue) return false;
            return this.Cipher(unchecked((uint)input)) == 0x180522fe; // Code secret : -8000 en cas d'urgence
        }

        private int GetMaxValue()
        {
            return this._pattern switch
            {
                Patterns.Pomni   => 999999,
                Patterns.Jax     => 20,
                Patterns.Ragatha => 5000,
                Patterns.Gangle  => 1000,
                Patterns.Kinger  => 10000,
                _ => 1
            };
        }

        public string Generate()
        {
            this._pattern = this.GetRandomPattern();

            var random = new Random();

            int N() => random.Next(1, this.GetMaxValue());

            switch (this._pattern)
            {
                case Patterns.Pomni:
                    this._a = N();
                    this._b = N();
                    this._c = N();
                    this._d = N();
                    this._e = N();
                    this._f = N();

                    this._calcul = $"(((((({this._a}+{this._b})*68741)-{this._c}))+(({this._d}-{this._e})*{this._f})))-1";
                    break;

                case Patterns.Jax:
                    this._a = N();
                    this._b = N();
                    this._c = N();
                    this._d = N();

                    this._calcul = $"(({this._a}/({this._b}*({this._b}+({this._b}*({this._b}+{this._b})))))+(({this._c}*{this._c}*{this._c}*{this._c}*{this._c})*({this._d}+{this._d})))";
                    break;

                case Patterns.Ragatha:
                    this._a = N();
                    this._b = N();
                    this._v = N();
                    this._d = N();
                    this._e = N();
                    this._f = N();

                    this._calcul = $"((42*((({this._a}+{this._b})*{this._v})-(({this._d}/2)+{this._e})))+(({this._f}*3)-9))";
                    break;

                case Patterns.Gangle:
                    this._a = N();
                    this._b = N();
                    this._c = N();
                    this._d = N();
                    this._e = N();
                    this._f = N();

                    this._calcul = $"(({this._a}-(3*({this._b}+(2*({this._c}-{this._d})))))+(({this._e}*{this._e})*(4-({this._f}/2))))";
                    break;

                case Patterns.Kinger:
                    this._a = N();
                    this._b = N();
                    this._c = N();
                    this._d = N();
                    this._r = N();
                    this._f = N();
                    this._g = N();
                    this._h = N();

                    this._calcul = $"((((({this._a}+{this._b})-{this._c})*2)+(({this._d}-({this._r}*3))*{this._f}))-(({this._g}+1)*({this._h}+2)))";
                    break;

                default:
                    this._calcul = "";
                    break;
            }

            return this._calcul;
        }

        public decimal GetResult()
        {
            decimal result = this._pattern switch
            {
                Patterns.Pomni => ((((this._a + this._b) * 68741m) - this._c) + ((this._d - this._e) * this._f)) - 1m,
                Patterns.Jax => (this._a / (this._b * (this._b + (this._b * (this._b + this._b))))) + ((this._c * this._c * this._c * this._c * this._c) * (this._d + this._d)),
                Patterns.Ragatha => (42m * (((this._a + this._b) * this._v) - ((this._d / 2m) + this._e))) + ((this._f * 3m) - 9m),
                Patterns.Gangle => (this._a - (3m * (this._b + (2m * (this._c - this._d))))) + ((this._e * this._e) * (4m - (this._f / 2m))),
                Patterns.Kinger => ((((this._a + this._b) - this._c) * 2m) + ((this._d - (this._r * 3m)) * this._f)) - ((this._g + 1m) * (this._h + 2m)),
                _ => 0m
            };
            return Math.Round(result, 2);
        }

        public bool CheckResult(decimal userValue)
        {
            return this.GetResult() == userValue;
        }
    }
}
