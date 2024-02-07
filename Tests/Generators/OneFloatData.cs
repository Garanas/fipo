using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Generators
{
    internal class OneFloatData
    {
        public float a;
    }

    internal class OneFloatDataGenerator
    {
        private Faker<OneFloatData> Faker = new Faker<OneFloatData>();

        public OneFloatDataGenerator(float min, float max)
        {
            Randomizer.Seed = new Random(6554523);
            Faker = new Faker<OneFloatData>()
                .RuleFor(u => u.a, f => Fint.Epsilon * MathF.Floor((1 / Fint.Epsilon) * f.Random.Float(min, max)));
        }

        public OneFloatData GenerateFloats()
        {
            return Faker.Generate();
        }
    }
}
