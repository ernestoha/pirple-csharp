using System;
using System.IO;
using System.Text;

namespace Test001
{
	public class CpCsv2Tsv
	{

		static StringBuilder ReadFile2Tsv001(string fileNameFrom)
		{
			StringBuilder tsvText = new StringBuilder();
			const Int32 BufferSize = 128;
			using (var fileStream = File.OpenRead(fileNameFrom))
			using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize)) {
				String line;
				while ((line = streamReader.ReadLine()) != null)
				{
					// Process line
					// Console.WriteLine(line);

					tsvText.AppendLine(line.Replace(",", "\t"));
				}
			}
			return tsvText;
		}

		static void WriteFile(string fileNameTo, StringBuilder tsvText)
		{
			
			// Check if file already exists. If yes, delete it.
			if (File.Exists(fileNameTo))
			{
				File.Delete(fileNameTo);
			}
			using(var streamWriter = new StreamWriter(fileNameTo)) {
				streamWriter.Write(tsvText.ToString());
			}
		}

		static void Main() {
            //https://www.kaggle.com/karangadiya/fifa19
			string fileNameFrom = @"data.csv";
			string fileNameTo = @"fifa-tab.tsv";
			WriteFile(fileNameTo, ReadFile2Tsv001(fileNameFrom));

			Console.WriteLine("------------------------------");
		}
	}
}

