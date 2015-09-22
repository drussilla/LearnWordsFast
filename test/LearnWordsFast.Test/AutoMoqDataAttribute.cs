using System.Linq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Xunit2;

namespace LearnWordsFast.Test
{
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : base(new Fixture()
                  .Customize(new AutoMoqCustomization())
                  //.Customize(new OmitOnRecursionBehavior())
                  )
        {
        }
        
        private class OmitOnRecursionBehavior : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                var throwError = fixture.Behaviors.OfType<ThrowingRecursionBehavior>().FirstOrDefault();
                if (throwError != null)
                    fixture.Behaviors.Remove(throwError);

                fixture.Behaviors.Add(new Ploeh.AutoFixture.OmitOnRecursionBehavior());
            }
        }
    }
}