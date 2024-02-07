using Bogus;

namespace FipoTests
{
    public class TwoIntegerData
    {
        public int a; public int b;
    }

    public class TwoIntegerDataGenerator
    {
        private Faker<TwoIntegerData> Faker = new Faker<TwoIntegerData>();

        public TwoIntegerDataGenerator(int min, int max)
        {
            Randomizer.Seed = new Random(6554523);
            Faker = new Faker<TwoIntegerData>()
                .RuleFor(u => u.a, f => f.Random.Int(min, max))
                .RuleFor(u => u.b, f => f.Random.Int(min, max));
        }

        public TwoIntegerData GenerateIntegers()
        {
            return Faker.Generate();
        }
    }

    [TestClass]
    public class IntegerProperties
    {

        public static IEnumerable<object[]> SmallTwoIntegerData
        {
            get
            {
                var generator = new TwoIntegerDataGenerator((int)(-1 * Math.Pow(2, 8)), (int)(1 * Math.Pow(2, 8)));
                var iterations = 100;
                var val = new object[iterations][];
                for (int i = 0; i < iterations; i++)
                {
                    var generated = generator.GenerateIntegers();
                    val[i] = new object[] { generated.a, generated.b };
                }

                //(int)Math.Pow(2, 32 - (Fint.Offset + 2))

                return val;
            }
        }

        public static IEnumerable<object[]> MediumTwoIntegerData
        {
            get
            {
                var iterations = 1000;
                var generator = new TwoIntegerDataGenerator(
                    (int)(-1 * Math.Pow(2, 16)),
                    (int)(1 * Math.Pow(2, 16)));

                var val = new object[iterations][];
                for (int i = 0; i < iterations; i++)
                {
                    var generated = generator.GenerateIntegers();
                    val[i] = new object[] { generated.a, generated.b };
                }

                //(int)Math.Pow(2, 32 - (Fint.Offset + 2))

                return val;
            }
        }

        public static IEnumerable<object[]> LargeTwoIntegerData
        { 
            get
            {
                var generator = new TwoIntegerDataGenerator(
                    (int)(-1 * Math.Pow(2, 22)),
                    (int)(1 * Math.Pow(2, 22)));
                var iterations = 4000;
                var val = new object[iterations][];
                for (int i = 0; i < iterations; i++)
                {
                    var generated = generator.GenerateIntegers();
                    val[i] = new object[] { generated.a, generated.b };
                }

                //(int)Math.Pow(2, 32 - (Fint.Offset + 2))

                return val;
            }
        }

        [TestMethod]
        [DynamicData(nameof(SmallTwoIntegerData))]
        public void AdditionSmall(int a, int b)
        {
            Fint fa = new Fint(a);
            Fint fb = new Fint(b);
            Fint fr = fa + fb;
            Assert.AreEqual((int)(fr), a + b);
        }

        [TestMethod]
        [DynamicData(nameof(MediumTwoIntegerData))]
        public void AdditionMedium(int a, int b)
        {
            Fint fa = new Fint(a);
            Fint fb = new Fint(b);
            Fint fr = fa + fb;
            Assert.AreEqual((int)(fr), a + b);
        }

        [TestMethod]
        [DynamicData(nameof(LargeTwoIntegerData))]
        public void AdditionLarge(int a, int b)
        {
            Fint fa = new Fint(a);
            Fint fb = new Fint(b);
            Fint fr = fa + fb;
            Assert.AreEqual((int)(fr), a + b);
        }

        [TestMethod]
        [DynamicData(nameof(SmallTwoIntegerData))]
        public void SubtractionSmall(int a, int b)
        {
            Fint fa = new Fint(a);
            Fint fb = new Fint(b);
            Fint fr = fa - fb;
            Assert.AreEqual((int)(fr), a - b);
        }

        [TestMethod]
        [DynamicData(nameof(MediumTwoIntegerData))]
        public void SubtractionMedium(int a, int b)
        {
            Fint fa = new Fint(a);
            Fint fb = new Fint(b);
            Fint fr = fa - fb;
            Assert.AreEqual((int)(fr), a - b);
        }

        [TestMethod]
        [DynamicData(nameof(LargeTwoIntegerData))]
        public void SubtractionLarge(int a, int b)
        {
            Fint fa = new Fint(a);
            Fint fb = new Fint(b);
            Fint fr = fa - fb;
            Assert.AreEqual((int)(fr), a - b);
        }
    }
}