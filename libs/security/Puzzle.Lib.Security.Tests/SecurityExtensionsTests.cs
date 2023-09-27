namespace Puzzle.Lib.Security.Tests
{
    public sealed class SecurityExtensionsTests
    {
        [Theory]
        [InlineData(null)]
        public void CreatePasswordHash_ThrowsArgumentNullException_WhenPasswordIsNull(string password)
        {
            Assert.Throws<ArgumentNullException>(() => password.CreatePasswordHash());
        }

        [Theory]
        [InlineData("")]

        public void CreatePasswordHash_ThrowsArgumentException_WhenPasswordIsNullOrEmpty(string password)
        {
            Assert.Throws<ArgumentException>(() => password.CreatePasswordHash());
        }

        [Fact]
        public void VerifyPasswordHash_ReturnsTrue_WhenPasswordMatches()
        {
            // Arrange
            string password = "password";
            string hashedPassword = password.CreatePasswordHash();

            // Act
            bool result = hashedPassword.VerifyPasswordHash(password);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyPasswordHash_ReturnsFalse_WhenPasswordDoesNotMatch()
        {
            // Arrange
            string password = "password";
            string hashedPassword = password.CreatePasswordHash();

            // Act
            bool result = hashedPassword.VerifyPasswordHash("wrongpassword");

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(null, "123456")]
        public void VerifyPasswordHash_ThrowsArgumentNullException_WhenHashedPasswordIsNull(string hashedPassword, string password)
        {
            Assert.Throws<ArgumentNullException>(() => hashedPassword.VerifyPasswordHash(password));
        }

        [Theory]
        [InlineData("", "123456")]
        public void VerifyPasswordHash_ThrowsArgumentException_WhenHashedPasswordIsEmpty(string hashedPassword, string password)
        {
            Assert.Throws<ArgumentException>(() => hashedPassword.VerifyPasswordHash(password));
        }

        [Theory]
        [InlineData("FtryDtJWPtvcGUjoN9HMCMy", null)]
        public void VerifyPasswordHash_ThrowsArgumentNullException_WhenPasswordIsNull(string hashedPassword, string password)
        {
            Assert.Throws<ArgumentNullException>(() => hashedPassword.VerifyPasswordHash(password));
        }

        [Theory]
        [InlineData("FtryDtJWPtvcGUjoN9HMCMy", "")]
        public void VerifyPasswordHash_ThrowsArgumentException_WhenPasswordIsEmpty(string hashedPassword, string password)
        {
            Assert.Throws<ArgumentException>(() => hashedPassword.VerifyPasswordHash(password));
        }

        //[Theory]
        //[InlineData(null)]
        //public void Encrypt_ThrowsArgumentNullException_WhenTextIsNull(string text)
        //{
        //    Assert.Throws<ArgumentNullException>(() => text.Encrypt());
        //}

        //[Theory]
        //[InlineData("")]
        //public void Encrypt_ThrowsArgumentException_WhenTextIsEmpty(string text)
        //{
        //    Assert.Throws<ArgumentException>(() => text.Encrypt());
        //}

        //[Fact]
        //public void Decrypt_ReturnsOriginalText_AfterEncrypting()
        //{
        //    // Arrange
        //    string originalText = "hello world";

        //    // Act
        //    string encryptedText = originalText.Encrypt();
        //    string decryptedText = encryptedText.Decrypt();

        //    // Assert
        //    Assert.Equal(originalText, decryptedText);
        //}

        //[Theory]
        //[InlineData(null)]
        //public void Decrypt_ThrowsArgumentNullException_WhenCipherTextIsNull(string cipherText)
        //{
        //    Assert.Throws<ArgumentNullException>(() => cipherText.Decrypt());
        //}

        //[Theory]
        //[InlineData("")]
        //public void Decrypt_ThrowsArgumentException_WhenCipherTextIsEmpty(string cipherText)
        //{
        //    Assert.Throws<ArgumentException>(() => cipherText.Decrypt());
        //}

        [Theory]
        [InlineData(null)]
        public void ToSHA512_ThrowsArgumentNullException_WhenTextIsNullOrEmpty(string text)
        {
            Assert.Throws<ArgumentNullException>(() => text.ToSHA512());
        }

        [Theory]
        [InlineData("")]
        public void ToSHA512_ThrowsArgumentException_WhenTextIsEmpty(string text)
        {
            Assert.Throws<ArgumentException>(() => text.ToSHA512());
        }

        [Fact]
        public void ToSHA512_ReturnsValidHash()
        {
            // Arrange
            string text = "hello world";
            string expectedHash = "MJ7MSJwS1utMxA9QyQLytNDtd+5RGnx6m808qG1M2G+YndNbxf9JlnDaNCVbRbDP2DDoH2Bdz33FVC6TrpzXbw==";

            // Act
            string actualHash = text.ToSHA512();

            // Assert
            Assert.Equal(expectedHash, actualHash);
        }
    }
}