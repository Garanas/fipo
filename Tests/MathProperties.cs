﻿namespace Tests
{
    [TestClass]
    internal class MathProperties
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

        [TestMethod]
        [DynamicData(nameof(SmallOneFloatData))]
        public void FloorSmall(float a)
        {
            Fint fa = new Fint(a);
            Fint fl = Fint.Floor(fa);
            Assert.AreEqual((float)(fl), MathF.Floor(a));
        }
    }
}
