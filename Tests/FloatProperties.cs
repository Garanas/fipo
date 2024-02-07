namespace Tests
{
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

                //(int)Math.Pow(2, 32 - (Fint.Offset + 2))

                return val;
            }
        }

        [TestMethod]
        [DynamicData(nameof(SmallOneFloatData))]
        public void Identity(float a)
        {
            Fint fa = new Fint(a);
            Fint fb = new Fint(0);
            Fint fr = fa + fb;
            Assert.AreEqual((float)(fr), a + 0, Fint.Epsilon);
        }

        [TestMethod]
        [DynamicData(nameof(SmallTwoFloatData))]
        public void AdditionSmall(float a, float b)
        {
            Fint fa = new Fint(a);
            Fint fb = new Fint(b);
            Fint fr = fa + fb;
            Assert.AreEqual((float)(fr), a + b, Fint.Epsilon);
        }

        [TestMethod]
        [DynamicData(nameof(MediumTwoFloatData))]
        public void AdditionMedium(float a, float b)
        {
            Fint fa = new Fint(a);
            Fint fb = new Fint(b);
            Fint fr = fa + fb;
            Assert.AreEqual((float)(fr), a + b,  Fint.Epsilon);
        }

        [TestMethod]
        [DynamicData(nameof(LargeTwoFloatData))]
        public void AdditionLarge(float a, float b)
        {
            Fint fa = new Fint(a);
            Fint fb = new Fint(b);
            Fint fr = fa + fb;
            Assert.AreEqual((float)(fr), a + b, Fint.Epsilon);
        }

        [TestMethod]
        [DynamicData(nameof(SmallTwoFloatData))]
        public void SubtractionSmall(float a, float b)
        {
            Fint fa = new Fint(a);
            Fint fb = new Fint(b);
            Fint fr = fa - fb;
            Assert.AreEqual((float)(fr), a - b, Fint.Epsilon);
        }

        [TestMethod]
        [DynamicData(nameof(MediumTwoFloatData))]
        public void SubtractionMedium(float a, float b)
        {
            Fint fa = new Fint(a);
            Fint fb = new Fint(b);
            Fint fr = fa - fb;
            Assert.AreEqual((float)(fr), a - b, Fint.Epsilon);
        }

        [TestMethod]
        [DynamicData(nameof(LargeTwoFloatData))]
        public void SubtractionLarge(float a, float b)
        {
            Fint fa = new Fint(a);
            Fint fb = new Fint(b);
            Fint fr = fa - fb;
            Assert.AreEqual((float)(fr), a - b, Fint.Epsilon);
        }
    }
}