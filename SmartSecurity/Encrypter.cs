
namespace Smart.Security
{
    using System;
    using System.Text;
    using System.Globalization;
    using System.Security.Cryptography;
    using System.IO;

	/// <summary>
	/// 安全类（字符串加、解密）
	/// </summary>
	public class Encrypter 
	{ 
		/// <summary> 
		/// 使用缺省密钥字符串加密 
		/// </summary> 
		/// <param name="original">明文</param> 
		/// <returns>密文</returns> 
		public static string Encrypt(string original) 
		{ 
			string key = "SMARTCAM"; // 密钥，只能为8位

			DESCryptoServiceProvider  des  =  new  DESCryptoServiceProvider();  
			//把字符串放到byte数组中  
			byte[]  inputByteArray  =  Encoding.Default.GetBytes(original);  
			
			//建立加密对象的密钥和偏移量  
			//原文使用ASCIIEncoding.ASCII方法的GetBytes方法  
			//使得输入密码必须输入英文文本  
			des.Key  =  ASCIIEncoding.ASCII.GetBytes(key);  
			des.IV  =  ASCIIEncoding.ASCII.GetBytes(key);  
			MemoryStream  ms  =  new  MemoryStream();  
			CryptoStream  cs  =  new  CryptoStream(ms,  des.CreateEncryptor(),CryptoStreamMode.Write); 			
			cs.Write(inputByteArray,  0,  inputByteArray.Length);  
			cs.FlushFinalBlock(); 			
			StringBuilder  ret  =  new  StringBuilder();  
			foreach(byte  b  in  ms.ToArray())  
			{  				
				ret.AppendFormat("{0:X2}",  b);  
			}  
			ret.ToString();  
			return  ret.ToString(); 
		} 

		/// <summary> 
		/// 使用缺省密钥解密 
		/// </summary> 
		/// <param name="original">密文</param> 
		/// <returns>明文</returns> 
		public static string Decrypt(string original) 
		{ 
			// --新方法开始
			string key = "SMARTCAM"; // 密钥，只能为8位

			DESCryptoServiceProvider  des  =  new  DESCryptoServiceProvider();  
 
			byte[]  inputByteArray  =  new  byte[original.Length  /  2];  
			for(int  x  =  0;  x  <  original.Length  /  2;  x++)  
			{  
				int  i  =  (Convert.ToInt32(original.Substring(x  *  2,  2),  16));  
				inputByteArray[x]  =  (byte)i;  
			}  

			//建立加密对象的密钥和偏移量，此值重要，不能修改  
			des.Key  =  ASCIIEncoding.ASCII.GetBytes(key);  
			des.IV  =  ASCIIEncoding.ASCII.GetBytes(key);  
			MemoryStream  ms  =  new  MemoryStream();  
			CryptoStream  cs  =  new  CryptoStream(ms,  des.CreateDecryptor(),CryptoStreamMode.Write);  
			
			cs.Write(inputByteArray,  0,  inputByteArray.Length);  
			cs.FlushFinalBlock();  

			//建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象  
			StringBuilder  ret  =  new  StringBuilder();  
             
			return  System.Text.Encoding.Default.GetString(ms.ToArray());
		} 

		/// <summary> 
		/// 使用给定密钥解密 
		/// </summary> 
		/// <param name="original">密文</param> 
		/// <param name="key">密钥</param> 
		/// <returns>明文</returns> 
		public static string Decrypt(string original, string key) 
		{ 
			return Decrypt(original,key,System.Text.Encoding.Default); 
		} 

		/// <summary> 
		/// 使用缺省密钥解密,返回指定编码方式明文 
		/// </summary> 
		/// <param name="original">密文</param> 
		/// <param name="encoding">编码方式</param> 
		/// <returns>明文</returns> 
		public static string Decrypt(string original,Encoding encoding) 
		{ 
			return Decrypt(original,"SMARTCAM",encoding); 
		} 

		/// <summary> 
		/// 使用给定密钥加密 
		/// </summary> 
		/// <param name="original">原始文字</param> 
		/// <param name="key">密钥</param> 
		/// <returns>密文</returns> 
		public static string Encrypt(string original, string key) 
		{ 
			byte[] buff = System.Text.Encoding.Default.GetBytes(original); 
			byte[] kb = System.Text.Encoding.Default.GetBytes(key); 
			return Convert.ToBase64String(Encrypt(buff,kb)); 
		} 

		/// <summary> 
		/// 使用给定密钥解密 
		/// </summary> 
		/// <param name="encrypted">密文</param> 
		/// <param name="key">密钥</param> 
		/// <param name="encoding">字符编码方案</param> 
		/// <returns>明文</returns> 
		public static string Decrypt(string encrypted, string key,Encoding encoding) 
		{ 
			byte[] buff = Convert.FromBase64String(encrypted); 
			byte[] kb = System.Text.Encoding.Default.GetBytes(key); 
			return encoding.GetString(Decrypt(buff,kb)); 
		} 

		/// <summary> 
		/// 生成MD5摘要 
		/// </summary> 
		/// <param name="original">数据源</param> 
		/// <returns>摘要</returns> 
		public static byte[] MakeMD5(byte[] original) 
		{ 
			MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider(); 
			byte[] keyhash = hashmd5.ComputeHash(original); 
			hashmd5 = null; 
			return keyhash; 
		} 

		/// <summary> 
		/// 使用给定密钥加密 
		/// </summary> 
		/// <param name="original">明文</param> 
		/// <param name="key">密钥</param> 
		/// <returns>密文</returns> 
		public static byte[] Encrypt(byte[] original, byte[] key) 
		{ 
			TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider(); 
			des.Key = MakeMD5(key); 
			des.Mode = CipherMode.ECB; 

			return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length); 
		} 

		/// <summary> 
		/// 使用给定密钥解密数据 
		/// </summary> 
		/// <param name="encrypted">密文</param> 
		/// <param name="key">密钥</param> 
		/// <returns>明文</returns> 
		public static byte[] Decrypt(byte[] encrypted, byte[] key) 
		{ 
			TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider(); 
			des.Key = MakeMD5(key); 
			des.Mode = CipherMode.ECB; 

			return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length); 
		} 

		/// <summary> 
		/// 使用缺省密钥加密 
		/// </summary> 
		/// <param name="original">原始数据</param> 
		/// <returns>密文</returns> 
		public static byte[] Encrypt(byte[] original) 
		{ 
			byte[] key = System.Text.Encoding.Default.GetBytes("SMARTCAM"); 
			return Encrypt(original,key); 
		} 

		/// <summary> 
		/// 使用缺省密钥解密数据 
		/// </summary> 
		/// <param name="encrypted">密文</param> 
		/// <returns>明文</returns> 
		public static byte[] Decrypt(byte[] encrypted) 
		{ 
			byte[] key = System.Text.Encoding.Default.GetBytes("SMARTCAM"); 
			return Decrypt(encrypted,key); 
		} 
	} 
}