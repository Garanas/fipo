using Bogus;

namespace FipoTests
{
    internal class OneFloatData
    {
        public float a;
    }

    internal class TwoFloatData
    {
        public float a; public float b;
    }

    internal class OneFloatDataGenerator
    {
        private Faker<OneFloatData> Faker = new Faker<OneFloatData>();

        public OneFloatDataGenerator(float min, float max)
        {
            Randomizer.Seed = new Random(6554523);
            Faker = new Faker<OneFloatData>()
                .RuleFor(u => u.a, f => Fipo.Epsilon * MathF.Floor((1 / Fipo.Epsilon) * f.Random.Float(min, max)));
        }

        public OneFloatData GenerateFloats()
        {
            return Faker.Generate();
        }
    }

    internal class TwoFloatDataGenerator
    {
        private Faker<TwoFloatData> Faker = new Faker<TwoFloatData>();

        public TwoFloatDataGenerator(float min, float max)
        {
            Randomizer.Seed = new Random(6554523);
            Faker = new Faker<TwoFloatData>()
                .RuleFor(u => u.a, f => Fipo.Epsilon * MathF.Floor((1 / Fipo.Epsilon) * f.Random.Float(min, max)))
                .RuleFor(u => u.b, f => Fipo.Epsilon * MathF.Floor((1 / Fipo.Epsilon) * f.Random.Float(min, max)));
        }

        public TwoFloatData GenerateFloats()
        {
            return Faker.Generate();
        }
    }

    [TestClass]
    public class FloatProperties
    {

        public static IEnumerable<object[]> SmallOneFloatData
        {
            get
            {
                var generator = new OneFloatDataGenerator((float)(-1 * Math.Pow(2, 8)), (float)(1 * Math.Pow(2, 8)));
                var iterations = 100;
                var val = new object[iterations][];
                for (int i = 0; i < iterations; i++)
                {
                    var generated = generator.GenerateFloats();
                    val[i] = new object[] { generated.a };
                }

                return val;
            }
        }

        public static IEnumerable<object[]> SmallTwoFloatData
        {
            get
            {
                var generator = new TwoFloatDataGenerator((float)(-1 * Math.Pow(2, 8)), (float)(1 * Math.Pow(2, 8)));
                var iterations = 100;
                var val = new object[iterations][];
                for (int i = 0; i < iterations; i++)
                {
                    var generated = generator.GenerateFloats();
                    val[i] = new object[] { generated.a, generated.b };
                }

                return val;
            }
        }

        public static IEnumerable<object[]> MediumTwoFloatData
        {
            get
            {
                var iterations = 1000;
                var generator = new TwoFloatDataGenerator(
                    (float)(-1 * Math.Pow(2, 16)),
                    (float)(1 * Math.Pow(2, 16)));

                var val = new object[iterations][];
                for (int i = 0; i < iterations; i++)
                {
                    var generated = generator.GenerateFloats();
                    val[i] = new object[] { generated.a, generated.b };
                }

                return val;
            }
        }

        public static IEnumerable<object[]> LargeTwoFloatData
        { 
            get
            {
                var generator = new TwoFloatDataGenerator(
                    (float)(-1 * Math.Pow(2, 22)),
                    (float)(1 * Math.Pow(2, 22)));
                var iterations = 4000;
                var val = new object[iterations][];
                for (int i = 0; i < iterations; i++)
                {
                    var generated = generator.GenerateFloats();
                    val[i] = new object[] { generated.a, generated.b };
                }

                //(int)Math.Pow(2, 32 - (Fipo.Offset + 2))

                return val;
            }
        }

        [TestMethod]
        [DynamicData(nameof(SmallOneFloatData))]
        public void Identity(float a)
        {
            Fipo fa = new Fipo(a);
            Fipo fb = new Fipo(0);
            Fipo fr = fa + fb;
            Assert.AreEqual((float)(fr), a + 0, Fipo.Epsilon);
        }

        [TestMethod]
        [DynamicData(nameof(SmallTwoFloatData))]
        public void AdditionSmall(float a, float b)
        {
            Fipo fa = new Fipo(a);
            Fipo fb = new Fipo(b);
            Fipo fr = fa + fb;
            Assert.AreEqual((float)(fr), a + b, Fipo.Epsilon);
        }

        [TestMethod]
        [DynamicData(nameof(MediumTwoFloatData))]
        public void AdditionMedium(float a, float b)
        {
            Fipo fa = new Fipo(a);
            Fipo fb = new Fipo(b);
            Fipo fr = fa + fb;
            Assert.AreEqual((float)(fr), a + b,  Fipo.Epsilon);
        }

        [TestMethod]
        [DynamicData(nameof(LargeTwoFloatData))]
        public void AdditionLarge(float a, float b)
        {
            Fipo fa = new Fipo(a);
            Fipo fb = new Fipo(b);
            Fipo fr = fa + fb;
            Assert.AreEqual((float)(fr), a + b, Fipo.Epsilon);
        }

        [TestMethod]
        [DynamicData(nameof(SmallTwoFloatData))]
        public void SubtractionSmall(float a, float b)
        {
            Fipo fa = new Fipo(a);
            Fipo fb = new Fipo(b);
            Fipo fr = fa - fb;
            Assert.AreEqual((float)(fr), a - b, Fipo.Epsilon);
        }

        [TestMethod]
        [DynamicData(nameof(MediumTwoFloatData))]
        public void SubtractionMedium(float a, float b)
        {
            Fipo fa = new Fipo(a);
            Fipo fb = new Fipo(b);
            Fipo fr = fa - fb;
            Assert.AreEqual((float)(fr), a - b, Fipo.Epsilon);
        }

        [TestMethod]
        [DynamicData(nameof(LargeTwoFloatData))]
        public void SubtractionLarge(float a, float b)
        {
            Fipo fa = new Fipo(a);
            Fipo fb = new Fipo(b);
            Fipo fr = fa - fb;
            Assert.AreEqual((float)(fr), a - b, Fipo.Epsilon);
        }
    }
}