
namespace Tests.Generators
{
    public class TwoFloatData
    {
        public float a; public float b;
    }

    public class TwoFloatDataGenerator
    {
        private Faker<TwoFloatData> Faker = new Faker<TwoFloatData>();

        public TwoFloatDataGenerator(float min, float max)
        {
            Randomizer.Seed = new Random(6554523);
            Faker = new Faker<TwoFloatData>()
                .RuleFor(u => u.a, f => Fint.Epsilon * MathF.Floor((1 / Fint.Epsilon) * f.Random.Float(min, max)))
                .RuleFor(u => u.b, f => Fint.Epsilon * MathF.Floor((1 / Fint.Epsilon) * f.Random.Float(min, max)));
        }

        public TwoFloatData GenerateFloats()
        {
            return Faker.Generate();
        }
    }
}
