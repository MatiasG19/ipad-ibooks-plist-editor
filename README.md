# iPad eBook Plist editor

Generate .plist entries for eBooks on iPad with an interactive script.

```xml
<?xml version="1.0" encoding="UTF-8"?>
<!DOCTYPE plist PUBLIC "-//Apple//DTD PLIST 1.0//EN" "http://www.apple.com/DTDs/PropertyList-1.0.dtd">
<plist version="1.0">
<dict>
	<key>Books</key>
	<array>
		<dict>
			<key>Inserted-By-iBooks</key>
			<true/>
			<key>Name</key>
			<string>SQL - The complete reference</string>
			<key>Package Hash</key>
			<string>4BBF070DD0974E62AAD0E24F14AECEAA</string>
			<key>Path</key>
			<string>SQL - The complete reference.pdf</string>
			<key>Persistent ID</key>
			<string>4BBF070DD0974E62</string>
			<key>Persistent ID Generated On Device</key>
			<false/>
			<key>importDate</key>
			<date>2023-07-15T14:33:32Z</date>
		</dict>
	</array>
</dict>
</plist>
```

## Getting started

Install [.NET CLI](https://learn.microsoft.com/en-us/dotnet/core/install/linux).

Debug project:

`dotnet run`

Build project:

`dotnet build`

Publish:
`dotnet publish -c Release`

## Supported file types

pdf
