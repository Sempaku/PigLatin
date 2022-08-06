using PigLatin;
using System;
using Xunit;

namespace PigLatinTests
{
    public class RuTests
    {
        //У попа была собака, он её любил.Она съела кусок мяса, он[поп] её убил.

        //Усу посопаса бысыласа сособасакаса, осон есеёсё люсюбисил.Осонаса съеселаса кусусосок мясясаса, осон есеёсё усубисил.

        [Theory]
        [InlineData("У", "усу")]
        [InlineData("попа", "посопаса")]
        [InlineData("была", "бысыласа")]
        [InlineData("собака", "сособасакаса")]
        

        public void RuEncryptTests(string text, string expected)
        {
            //Arrange
            IPigLatin pig = new RuPigLatin();

            //Act
            string result = pig.Encrypt(text);

            //Assert
            Assert.Equal(expected, result);
        }
              
    }
}
