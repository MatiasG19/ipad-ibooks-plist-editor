// See https://aka.ms/new-console-template for more information
using ExtensionMethods;

//Console.WriteLine("Enter path to .plist file:");
//string pathToPlist = Console.Read();

string plistContent = File.ReadAllText("Purchases.plist");

plistContent = plistContent.ReplaceLastOccurrence("</plist>", "")
                           .ReplaceLastOccurrence("</dict>", "")
                           .ReplaceLastOccurrence("</array>", "");

// TODO Filter files that already exist
string[] filePaths = Directory.GetFiles(".", "*.pdf", SearchOption.TopDirectoryOnly);

const string bookEntryTemplate = 
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
			<string>4B6C69B27ED443C5</string>
			<key>Persistent ID Generated On Device</key>
			<true/>
			<key>importDate</key>
			<date>2022-08-28T09:40:30Z</date>
		</dict>";

foreach (var filePath in filePaths)
{
    string fileName = filePath.Substring(filePath.LastIndexOf("/") + 1);

    string bookEntry = bookEntryTemplate;
    string bookNameWithExtenstion = fileName;
    string bookName = fileName.Substring(0, fileName.LastIndexOf(".pdf"));
    string packageHash = Guid.NewGuid().ToString().Replace("-", "");

    bookEntry = bookEntry.Replace("%bookNameWithExtension%", bookNameWithExtenstion);
    bookEntry = bookEntry.Replace("%bookName%", bookName);
    bookEntry = bookEntry.Replace("%packageHash%", packageHash); // E9245C7C48E4134D741E1939B04FB022

    plistContent = string.Concat(new string[] { plistContent, bookEntry + "\n" });    
}

plistContent = string.Concat(new string[] { plistContent, "</array>\n", "</dict>\n", "</plist>\n"});

Console.WriteLine(plistContent);