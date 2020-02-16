using System;
using System.IO;
using System.Threading.Tasks;

namespace NotifierService
{
	public class FileReaderWriter
	{
		private string _filename { get; set; }
		private string _directory { get; set; }
		private string _filedirectory { get; set; }

		public FileReaderWriter(string filename, string directory)
		{
			this._filename = filename;
			this._directory = directory;
			this._filedirectory = $"{this._directory}/{this._filename}";
		}
		public async Task<string> ReadFromFile()
		{
			return await Task.Run(() => ReadTextFromFile());
		}

		public async Task WriteToFile(string text)
		{
			await Task.Run(() => WriteTextToFile(text));
		}

		public async Task<byte[]> ReadBytesFromFile()
		{
			return await Task.Run(() => ReadFromBytesToByteArray());
		}

		private string ReadTextFromFile()
		{
			return File.ReadAllText(this._filedirectory);
		}

		private void WriteTextToFile(string text)
		{
			File.WriteAllText(this._filedirectory, text);
		}

		private byte[] ReadFromBytesToByteArray()
		{
			int counter = 0;
			using(Stream stream = new FileStream(this._filedirectory, FileMode.Open, FileAccess.Read))
			{
				byte[] buffer = new byte[stream.Length];

				try
				{
					while(true)
					{
						counter = stream.Read(buffer, 0, (int)stream.Length);
						if(counter == 0)
						{
							return buffer;
						}
					}
				}
				catch(Exception ee)
				{
					Console.WriteLine(ee.ToString());
				}
				finally
				{
					stream.Close();
				}
			}
			return null;
		}
	}
}
