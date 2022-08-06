using PigLatin;
using System;
using Xunit;

namespace PigLatinTests
{
    public class RuTests
    {
        //� ���� ���� ������, �� � �����.��� ����� ����� ����, ��[���] � ����.

        //��� �������� �������� ������������, ���� ���� ���������.������� ��������� ��������� ��������, ���� ���� ��������.

        [Theory]
        [InlineData("�", "���")]
        [InlineData("����", "��������")]
        [InlineData("����", "��������")]
        [InlineData("������", "������������")]
        

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
