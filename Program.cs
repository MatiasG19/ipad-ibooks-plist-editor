// See https://aka.ms/new-console-template for more information

//Console.WriteLine("Enter path to .plist file:");
//string pathToPlist = Console.Read();
string fileContent = File.ReadAllText("Purchases.plist");

string[] filePaths = Directory.GetFiles(".", "*.pdf", SearchOption.TopDirectoryOnly);

foreach (string filePath in filePaths)
{
    //filePath = filePath.Substring();
}

const string bookNameWithExtenstion = "";
const string bookName = "";
const string packageHash = "";

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

string bookEntry = bookEntryTemplate;

bookEntry.Replace("%bookNameWithExtension%", bookNameWithExtenstion);
bookEntry.Replace("%bookName%", bookName);
bookEntry.Replace("packageHash", packageHash); // E9245C7C48E4134D741E1939B04FB022

//Console.WriteLine(fileContent);
foreach (var item in filePaths)
{
Console.WriteLine(item );
    
}