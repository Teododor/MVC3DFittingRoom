using System.Security.Cryptography;

namespace Proiect.BusinessLogic.Implementation.Implementation.Account.Hashing
{
	public class AesEncryption
	{
		private static byte[] GenerateRandomKey()
		{
			using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
			{
				byte[] key = new byte[32]; // 32 bytes for 256-bit key
				rng.GetBytes(key);
				return key;
			}
		}

		private static byte[] GenerateRandomIV()
		{
			using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
			{
				byte[] iv = new byte[16]; // 16 bytes for 128-bit block size
				rng.GetBytes(iv);
				return iv;
			}
		}

		private static readonly byte[] AesKey = new byte[]
	{
		0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF,
		0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF,
		0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF,
		0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF
	};

		// Example hardcoded 16-byte AES IV (128-bit).
		private static readonly byte[] AesIV = new byte[]
		{
		0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF,
		0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF
		};
		/*GenerateRandomIV();*/

		public static string Encrypt(string plainText)
		{
			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = AesKey;
				aesAlg.IV = AesIV;

				ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

				using (MemoryStream msEncrypt = new MemoryStream())
				{
					using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
						{
							swEncrypt.Write(plainText);
						}
					}
					return Convert.ToBase64String(msEncrypt.ToArray());
				}
			}
		}

		public static string Decrypt(string cipherText)
		{
			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = AesKey;
				aesAlg.IV = AesIV;

				ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

				using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
				{
					using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader srDecrypt = new StreamReader(csDecrypt))
						{
							return srDecrypt.ReadToEnd();
						}
					}
				}
			}
		}
	}
}
