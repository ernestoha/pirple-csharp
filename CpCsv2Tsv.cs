using System;
using System.IO;
using System.Text;

namespace Test001
{
	public class CpCsv2Tsv
	{

		public class IOResponse
		{
			public bool IsOk { get; private set;} 
			public String Response { get; private set;} 
			public StringBuilder TsvText { get; private set;} 
			public IOResponse(bool isOk, String response, StringBuilder tsvText)
			{
				this.IsOk = isOk;
				this.Response = response;
				this.TsvText = tsvText;
			}
			public IOResponse(bool isOk, String response)
			{
				this.IsOk = isOk;
				this.Response = response;
			}
		}

		static IOResponse ReadFile2Tsv001(string fileNameFrom)
		{
			bool isOk = false;
			String msg = null;
			StringBuilder tsvText = new StringBuilder();
			const Int32 BufferSize = 128;
			try
			{
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
				isOk = true;
				msg = "Ok";
			}
			catch(FileNotFoundException)
			{
				msg = fileNameFrom+" File do not exists.";// + e.Message;
			}
			catch(IOException e)
			{
				msg = e.GetType().Name+": The write operation could not " +
					"be performed because the specified " +
					"part of the file is locked.";
			}
			catch(Exception e)
			{
				msg = e.GetType().Name+": Exception. "+ e.Message;
			}
			return new IOResponse(isOk, msg, tsvText);
		}

		static IOResponse WriteFile(string fileNameTo, StringBuilder tsvText)
		{
			bool isOk = false;
			String msg = null;	
			// Check if file already exists. If yes, delete it.
			if (File.Exists(fileNameTo))
			{
				File.Delete(fileNameTo);
			}
			try
			{
				using(var streamWriter = new StreamWriter(fileNameTo)) {
					streamWriter.Write(tsvText.ToString());
				}
				isOk = true;
				msg = "Ok";
			}
			catch(Exception e)
			{
				msg = e.GetType().Name+": Exception. "+ e.Message;
			}
			return new IOResponse(isOk, msg);
		}

		static void Main() {
            //https://www.kaggle.com/karangadiya/fifa19
			string fileNameFrom = @"data.csv";
			string fileNameTo = @"fifa-tab.tsv";
			// WriteFile(fileNameTo, ReadFile2Tsv001(fileNameFrom)); //old v. without handle exception

            IOResponse resRead = ReadFile2Tsv001(fileNameFrom);
			IOResponse resWrite = (resRead.IsOk) ? WriteFile(fileNameTo, resRead.TsvText) : new IOResponse(false, "File CSV was not copied.");
			
			//Output
			Console.WriteLine("--------------READ-BEGIN---------------");
			Console.WriteLine("isOk = {0}",resRead.IsOk);
			Console.WriteLine("Response = {0}",resRead.Response);
			Console.WriteLine("---------------READ-END----------------\n\n");

			Console.WriteLine("--------------WRITE-BEGIN--------------");
			Console.WriteLine("isOk = {0}",resWrite.IsOk);
			Console.WriteLine("Response = {0}",resWrite.Response);
			Console.WriteLine("---------------WRITE-END---------------\n\n");
			
			Console.WriteLine("---------------------------------------");
		}
	}
}
