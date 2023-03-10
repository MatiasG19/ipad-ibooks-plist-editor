using System.Text;
using ExtensionMethods;

Console.WriteLine("Enter path to .plist file (e.g. /myFolder/Purchases.plist):");
string? plistPath = Console.ReadLine();
if(String.IsNullOrEmpty(plistPath)) 
{	
	Console.WriteLine("Invalid path.");	
	return;
}

Console.WriteLine("Reading file content...");
string plistContent = File.ReadAllText(plistPath);
if(String.IsNullOrEmpty(plistContent)) 
{
	Console.WriteLine("Invalid file content.");	
	return;
}

plistContent = plistContent.ReplaceLastOccurrence("</plist>\n", "")
                           .ReplaceLastOccurrence("</dict>\n", "")
                           .ReplaceLastOccurrence("</array>\n", "");

// TODO Filter files that already exist
string[] filePaths = Directory.GetFiles(".", "*.pdf", SearchOption.TopDirectoryOnly);

const string bookEntryTemplate = 
		"\t" +
		 @"<dict>
			<key>Inserted-By-iBooks</key>
			<true/>
			<key>Name</key>
			<string>%bookName%</string>
			<key>Package Hash</key>
			<string>%packageHash%</string>
			<key>Path</key>
			<string>%bookNameWithExtension%</string>
			<key>Persistent ID</key>
			<string>%presistantId%</string>
			<key>Persistent ID Generated On Device</key>
			<false/>
			<key>importDate</key>
			<date>2022-08-28T09:40:30Z</date>
		</dict>";

Console.WriteLine("Creating entries...");
int i = 0;
foreach (var filePath in filePaths)
{
    string fileName = filePath.Substring(filePath.LastIndexOf("/") + 1);
	if(plistContent.Contains(fileName)) 
	{
		Console.WriteLine($"Skipping '{fileName}'. Entry already exists.");
		continue;
	}
	else 
	{
		Console.WriteLine($"Creating entry for: {fileName}");
		i++;
	}

	// TODO import date
    string bookEntry = bookEntryTemplate;
    string bookNameWithExtenstion = fileName;
    string bookName = fileName.Substring(0, fileName.LastIndexOf(".pdf"));
    string packageHash = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
    string presistantId = packageHash.Substring(0, 16);

    bookEntry = bookEntry.Replace("%bookNameWithExtension%", bookNameWithExtenstion);
    bookEntry = bookEntry.Replace("%bookName%", bookName);
    bookEntry = bookEntry.Replace("%packageHash%", packageHash); // e.g. E9245C7C48E4134D741E1939B04FB022
    bookEntry = bookEntry.Replace("%presistantId%", presistantId); // e.g. 4B6C69B27ED443C4

    plistContent = string.Concat(new string[] { plistContent, bookEntry + "\n" });    
}

if(i > 0) 
{
	plistContent = string.Concat(new string[] { plistContent, "\t</array>\n", "</dict>\n", "</plist>\n"});

	Console.WriteLine("Writing to plist file...");
	using (FileStream fs = File.Create(plistPath)) 
	{
		byte[] info = new UTF8Encoding(true).GetBytes(plistContent);
		fs.Write(info, 0, info.Length);
	}
} 

Console.WriteLine($"Finished. {i} entries written.");