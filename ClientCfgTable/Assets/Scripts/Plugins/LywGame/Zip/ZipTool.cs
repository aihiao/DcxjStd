using System;
using System.IO;
using System.IO.Compression;
using UnityEngine;
using LywGames;

public class ZipTool 
{

	/// <summary>
	/// 将压缩的输入字节流解压
	/// </summary>
	/// <param name="inputBytes">压缩文件字节流</param>
	/// <param name="path">文件写入路径</param>
	/// <param name="useCSharp">是否使用C#自带解压算法</param>
	public static bool Decompress(byte[] inputBytes, string path, bool useCSharp = false)
	{
		if (useCSharp)
		{
			MemoryStream ms = new MemoryStream(inputBytes);
			GZipStream zipStream = new GZipStream(ms, CompressionMode.Decompress, false);

			//ms的后4个字节为文件原长
			byte[] byteOriginalLength = new byte[4];
			ms.Position = ms.Length - 4;
			ms.Read(byteOriginalLength, 0, 4);
			ms.Position = 0;
			//需要测试
			int fileLength = BitConverter.ToInt32(byteOriginalLength, 0);
			byte[] buffer = new byte[fileLength];
			zipStream.Read(buffer, 0, fileLength);
			ms.Close();
			zipStream.Close();
			return WriteBuffersToFile(buffer, path);
		}
		else
		{
			return WriteBuffersToFile(CLZF2.Decompress(inputBytes), path);
		}

	}

	/// <summary>
	/// 将字节数组写回文件
	/// </summary>
	public static bool WriteBuffersToFile(byte[] buffer, string path)
	{
		try
		{
			string tempPtah = string.Empty;
			tempPtah = path.Substring(0, path.LastIndexOf('/'));

			if (Directory.Exists(tempPtah) == false)	//不存在路径的话创建路径
				Directory.CreateDirectory(tempPtah);

			if (File.Exists(path))	//先删除源文件，保证下载最新的
				File.Delete(path);

			FileStream outFileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			outFileStream.Write(buffer, 0, buffer.Length);
			outFileStream.Close();
			return true;
		}
		catch (Exception e)
		{
			Debug.LogError(e.ToString());
			return false;		
		}

	}

	/// <summary>
	/// 从文件中取得字节数组
	/// </summary>
	public static byte[] ReadBuffersFromFile(string path)
	{
		FileStream inFileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
		byte[] buffer = new byte[inFileStream.Length];
		inFileStream.Read(buffer, 0, buffer.Length);
		inFileStream.Close();
		return buffer;
	}
	
}
