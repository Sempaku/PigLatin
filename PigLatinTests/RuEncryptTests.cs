using PigLatin;
using System;
using Xunit;

namespace PigLatinTests
{
    public class RuTests
    {
        [Fact]
        public void RuEncryptTests()
        {
            //Arrange
            IPigLatin pig = new RuPigLatin();

            //Act
            string result = pig.Encrypt("�");

            //Assert
            Assert.Equal("���", result);
        }

        

       
    }
}
