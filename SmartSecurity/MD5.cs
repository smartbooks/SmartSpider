
namespace Smart.Security
{
    using System;
    using System.Security.Cryptography;

	/// <summary>
    /// MD5加密类
	/// </summary>
	public class MD5
	{
		/// <summary>		
        /// 将一段字符串使用MD5算法加密，并返回加密后的字符串
		/// </summary>
		/// <param name="clearText">原始明文字符串</param>
		/// <param name="encryptedText">经过MD5算法加密后的密文字符串</param>
		public static void MD5Encrypt(string clearText, ref string encryptedText)
		{
            encryptedText = "";
			try
			{
                if (clearText == null)
                {
                    clearText = "";
                }
				MD5CryptoServiceProvider o_MD5 = new MD5CryptoServiceProvider();
				byte[] bytesEncrypt = System.Text.Encoding.Default.GetBytes(clearText);
				byte[] bytesEncrypted = o_MD5.ComputeHash(bytesEncrypt, 0, bytesEncrypt.Length);				
				for (int i = 0; i < bytesEncrypted.Length; i++)
				{
                    encryptedText += bytesEncrypted[i].ToString("x").PadLeft(2, '0');
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
        /// 将一段字符串使用MD5算法加密，并返回加密后的字符串
		/// </summary>
        /// <param name="clearText">原始明文字符串</param>
        /// <returns>经过MD5算法加密后的密文字符串</returns>
		public static string MD5Encrypt(string clearText)
		{
            string encryptedText = "";
            MD5Encrypt(clearText, ref encryptedText);
            return encryptedText;
		}
	}
}
