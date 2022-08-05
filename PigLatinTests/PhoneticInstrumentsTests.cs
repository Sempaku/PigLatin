using PigLatin;
using System;
using System.Collections.Generic;
using Xunit;

namespace PigLatinTests
{
    public class PhoneticTests
    {

        [Theory]
        [InlineData("черная", "143344")]
        [InlineData("друзья", "2342ь4")]
        [InlineData("вода", "2424")]
        [InlineData("масло", "34134")]
        [InlineData("лодка", "34214")]
        [InlineData("лето", "3414")]
        [InlineData("стекло", "114134")]
        public void GetFlagsTests(string text, string expected)
        {
            PhoneticInstruments phoneticInstruments = new PhoneticInstruments();
            string result = phoneticInstruments.GetFlags(text);
            Assert.Equal(expected, result);
        }

        
        [Theory]
        [InlineData("окунь", new string[] { "о", "кунь" })]
        [InlineData("аир", new string[] { "а", "ир" })]
        [InlineData("болото", new string[] { "бо", "ло", "то" })]
        [InlineData("волокно", new string[] { "во", "ло", "кно" })]
        [InlineData("гвоздь", new string[] { "гвоздь" })]
        [InlineData("покров", new string[] { "по", "кров" })]
        [InlineData("голубь", new string[] { "го", "лубь" })]
        [InlineData("сторож", new string[] { "сто", "рож" })]
        [InlineData("паровоз", new string[] { "па", "ро", "воз" })]
        [InlineData("зеркало", new string[] { "зер", "ка", "ло" })]
        [InlineData("болтик", new string[] { "бол", "тик" })]
        [InlineData("сарайчик", new string[] { "са", "рай", "чик" })]
        [InlineData("сарафан", new string[] { "са", "ра", "фан" })]
        [InlineData("сом", new string[] { "сом" })]
        [InlineData("крот", new string[] { "крот" })]
        [InlineData("брод", new string[] { "брод" })]
        [InlineData("ворон", new string[] { "во", "рон" })]
        [InlineData("кулон", new string[] { "ку", "лон" })]
        [InlineData("корысть", new string[] { "ко", "рысть" })]
        [InlineData("кланяться", new string[] { "кла", "ня", "ться" })]
        [InlineData("ночник", new string[] { "но", "чник" })]
        [InlineData("кровля", new string[] { "кро", "вля" })]
        [InlineData("пробка", new string[] { "про", "бка" })]
        [InlineData("создание", new string[] { "со", "зда", "ни", "е" })]
        [InlineData("алфавит", new string[] { "ал", "фа", "вит" })]
        [InlineData("стройка", new string[] { "строй", "ка" })]
        [InlineData("квартал", new string[] { "квар", "тал" })]
        [InlineData("ландышевый", new string[] { "лан", "ды", "ше", "вый" })]
        [InlineData("бесшумный", new string[] { "бе", "cшум", "ный" })]
        [InlineData("программа", new string[] { "про", "гра", "мма" })]
        [InlineData("теннисный", new string[] { "те", "нни", "сный" })]
        [InlineData("кроссовки", new string[] { "кро", "ссо", "вки" })]
        [InlineData("рейка", new string[] { "рей", "ка" })]
        [InlineData("хлопья", new string[] { "хлопь", "я" })]
        [InlineData("отъезд", new string[] { "отъ", "езд" })]
        [InlineData("подъемный", new string[] { "подъ", "ем", "ный" })]
        public void GetSylaableTestsVar(string text, string[] expected)
        {
            PhoneticInstruments phoneticInstruments = new PhoneticInstruments();
            string[] result = phoneticInstruments.GetSyllable(text);
            
            Assert.Equal(expected, result);
        }
    }
}
