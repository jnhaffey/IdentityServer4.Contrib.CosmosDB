using System;
using Bogus;
using FluentAssertions;
using IdentityServer4.Contrib.CosmosDB.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IdentityServer4.Contrib.CosmosDB.Constants;

namespace IdentityServer4.Contrib.CosmosDB.UnitTests
{
    [TestClass]
    [TestCategory("Guard.For")]
    public class GuardTests
    {
        private const string ParameterName = "UnitTestParameterName";
        private readonly Randomizer _randomizer = new Randomizer(DateTime.Now.GetHashCode());

        [TestMethod]
        public void ForNull_StringType_Null()
        {
            // ARRANGE
            string nullString = null;
            var expectedMessage =
                ErrorMessages.ParameterNull.Replace(Placeholders.ParameterName, ParameterName);

            // ACT
            Action actionToTest = () => Guard.ForNull(nullString, ParameterName);

            // ASSERT
            actionToTest.Should().ThrowExactly<ArgumentNullException>()
                .And.Message.Should().Contain(expectedMessage);
        }

        [TestMethod]
        public void ForNull_StringType_Empty()
        {
            // ARRANGE
            var emptyString = string.Empty;

            // ACT
            Action actionToTest = () => Guard.ForNull(emptyString, ParameterName);

            // ASSERT
            actionToTest.Should().NotThrow();
        }

        [TestMethod]
        public void ForNull_StringType_Default()
        {
            // ARRANGE
            var defaultString = default(string);
            var expectedMessage =
                ErrorMessages.ParameterNull.Replace(Placeholders.ParameterName, ParameterName);

            // ACT
            Action actionToTest = () => Guard.ForNull(defaultString, ParameterName);

            // ASSERT
            actionToTest.Should().ThrowExactly<ArgumentNullException>()
                .And.Message.Should().Contain(expectedMessage);
        }

        [TestMethod]
        public void ForNull_StringType_RandomText()
        {
            // ARRANGE
            var emptyString = _randomizer.String(5, 10);

            // ACT
            Action actionToTest = () => Guard.ForNull(emptyString, ParameterName);

            // ASSERT
            actionToTest.Should().NotThrow();
        }

        [TestMethod]
        public void ForNull_IntType_Null()
        {
            // ARRANGE
            int? nullInteger = null;
            var expectedMessage =
                ErrorMessages.ParameterNull.Replace(Placeholders.ParameterName, ParameterName);

            // ACT
            Action actionToTest = () => Guard.ForNull(nullInteger, ParameterName);

            // ASSERT
            actionToTest.Should().ThrowExactly<ArgumentNullException>()
                .And.Message.Should().Contain(expectedMessage);
        }

        [TestMethod]
        public void ForNull_IntType_Default()
        {
            // ARRANGE
            var defaultInteger = default(int);

            // ACT
            Action actionToTest = () => Guard.ForNull(defaultInteger, ParameterName);

            // ASSERT
            actionToTest.Should().NotThrow();
        }

        [TestMethod]
        public void ForNull_BoolType_Null()
        {
            // ARRANGE
            bool? nullBoolean = null;
            var expectedMessage =
                ErrorMessages.ParameterNull.Replace(Placeholders.ParameterName, ParameterName);

            // ACT
            Action actionToTest = () => Guard.ForNull(nullBoolean, ParameterName);

            // ASSERT
            actionToTest.Should().ThrowExactly<ArgumentNullException>()
                .And.Message.Should().Contain(expectedMessage);
        }

        [TestMethod]
        public void ForNull_BoolType_Default()
        {
            // ARRANGE
            var defaultBoolean = default(bool);

            // ACT
            Action actionToTest = () => Guard.ForNull(defaultBoolean, ParameterName);

            // ASSERT
            actionToTest.Should().NotThrow();
        }

        [TestMethod]
        public void ForNullOrDefault_StringType_Null()
        {
            // ARRANGE
            string nullString = null;
            var expectedMessage =
                ErrorMessages.ParameterNullOrDefault.Replace(Placeholders.ParameterName, ParameterName);

            // ACT
            Action actionToTest = () => Guard.ForNullOrDefault(nullString, ParameterName);

            // ASSERT
            actionToTest.Should().ThrowExactly<ArgumentNullOrDefaultException>()
                .And.Message.Should().Contain(expectedMessage);
        }

        [TestMethod]
        public void ForNullOrDefault_StringType_Empty()
        {
            // ARRANGE
            var emptyString = string.Empty;
            var expectedMessage =
                ErrorMessages.ParameterNullOrDefault.Replace(Placeholders.ParameterName, ParameterName);

            // ACT
            Action actionToTest = () => Guard.ForNullOrDefault(emptyString, ParameterName);

            // ASSERT
            actionToTest.Should().NotThrow();
        }

        [TestMethod]
        public void ForNullOrDefault_StringType_Default()
        {
            // ARRANGE
            var defaultString = default(string);
            var expectedMessage =
                ErrorMessages.ParameterNullOrDefault.Replace(Placeholders.ParameterName, ParameterName);

            // ACT
            Action actionToTest = () => Guard.ForNullOrDefault(defaultString, ParameterName);

            // ASSERT
            actionToTest.Should().ThrowExactly<ArgumentNullOrDefaultException>()
                .And.Message.Should().Contain(expectedMessage);
        }

        [TestMethod]
        public void ForNullOrDefault_StringType_RandomText()
        {
            // ARRANGE
            var emptyString = _randomizer.String(5, 10);

            // ACT
            Action actionToTest = () => Guard.ForNullOrDefault(emptyString, ParameterName);

            // ASSERT
            actionToTest.Should().NotThrow();
        }

        [TestMethod]
        public void ForNullOrDefault_IntType_Null()
        {
            // ARRANGE
            int? nullInteger = null;
            var expectedMessage =
                ErrorMessages.ParameterNullOrDefault.Replace(Placeholders.ParameterName, ParameterName);

            // ACT
            Action actionToTest = () => Guard.ForNullOrDefault(nullInteger, ParameterName);

            // ASSERT
            actionToTest.Should().ThrowExactly<ArgumentNullOrDefaultException>()
                .And.Message.Should().Contain(expectedMessage);
        }

        [TestMethod]
        public void ForNullOrDefault_IntType_Default()
        {
            // ARRANGE
            var defaultInteger = default(int);
            var expectedMessage =
                ErrorMessages.ParameterNullOrDefault.Replace(Placeholders.ParameterName, ParameterName);

            // ACT
            Action actionToTest = () => Guard.ForNullOrDefault(defaultInteger, ParameterName);

            // ASSERT
            actionToTest.Should().ThrowExactly<ArgumentNullOrDefaultException>()
                .And.Message.Should().Contain(expectedMessage);
        }

        [TestMethod]
        public void ForNullOrDefault_BoolType_Null()
        {
            // ARRANGE
            bool? nullBoolean = null;
            var expectedMessage =
                ErrorMessages.ParameterNullOrDefault.Replace(Placeholders.ParameterName, ParameterName);

            // ACT
            Action actionToTest = () => Guard.ForNullOrDefault(nullBoolean, ParameterName);

            // ASSERT
            actionToTest.Should().ThrowExactly<ArgumentNullOrDefaultException>()
                .And.Message.Should().Contain(expectedMessage);
        }

        [TestMethod]
        public void ForNullOrDefault_BoolType_Default()
        {
            // ARRANGE
            var defaultBoolean = default(bool);
            var expectedMessage =
                ErrorMessages.ParameterNullOrDefault.Replace(Placeholders.ParameterName, ParameterName);

            // ACT
            Action actionToTest = () => Guard.ForNullOrDefault(defaultBoolean, ParameterName);

            // ASSERT
            actionToTest.Should().ThrowExactly<ArgumentNullOrDefaultException>()
                .And.Message.Should().Contain(expectedMessage);
        }

        [TestMethod]
        public void ForValueLessThan_Below()
        {
            // ARRANGE
            var expectedMessage = ErrorMessages.ValueLessThan
                .Replace(Placeholders.ParameterName, ParameterName)
                .Replace(Placeholders.MinimumValue, "0");
            // ACT
            Action actionToTest = () => Guard.ForValueLessThan(-1, 0, ParameterName);

            // ASSERT
            actionToTest.Should().ThrowExactly<ArgumentException>()
                .And.Message.Should().Contain(expectedMessage);
        }

        [TestMethod]
        public void ForValueLessThan_Same()
        {
            // ARRANGE
            // ACT
            Action actionToTest = () => Guard.ForValueLessThan(0, 0, ParameterName);

            // ASSERT
            actionToTest.Should().NotThrow();
        }

        [TestMethod]
        public void ForValueLessThan_Above()
        {
            // ARRANGE
            // ACT
            Action actionToTest = () => Guard.ForValueLessThan(1, 0, ParameterName);

            // ASSERT
            actionToTest.Should().NotThrow();
        }
    }
}