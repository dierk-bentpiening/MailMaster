# MailMaster
**Commandline SMTP Client with massmailing support!**
(Windows, Windows on Arm, MacOS, Linux)

## Introduction
MailMaster is an .NET Command line SMTP Client for SysCall usage.
Microsoft Office 365 Changed their Client Requirements and only allows Connections of Clients with TLS Support.

Microsoft Visual FoxPro and other legacy Development Environments and Software are not supporting Modern TLS Secured E-Mail sending.
Here comes MailMaster in the game, MailMaster can be called over an SysCall from FoxPro and Co, it supports TLS and SSL and is fully CLI Based.

All required Parameters will be passed through the CLI or the Syscall.

![mailmaster](https://user-images.githubusercontent.com/62020056/150222510-bbf7ad9f-221b-474b-a63b-29fbcd7803c1.png)

## Release
Actual Release Version is 1.1.0
I do Build Versions for Windows, MacOS and Linux, also there are Special Builds for Windows on Arm (WOA) ARM/ARM64.
Also Linux ARM/ARM64 and MUSL Versions are Available.

### [Goto actual Release Version](https://github.com/dierk-bentpiening/MailMaster/releases)


## Usage
All required arguments need to be given and passed in **the given order**, optional arguments can be passed by given keyword:<value> as an example cc:test@test.com or attachment:picture.jpg


```
MailMaster.exe <'required' host> <'required' email_address/username sender> <'required' password> <subject> <'required'body> <'optional' cc:<emailadress>> <'optional' attachment:<path>> 
```

#### How to send to multiple Recipients or CC Recipients ?
Sending to multiple Recipients is possible by passing it separated through an ",".
Your Memory is the limit 🤓

##### Example
`test1@test.com,test2@test.com,test3@test.com`



#### How to attach an File?
Use the 'attachment' keyword after the required parameters.

##### Example
`attachment:/path/to/file.jpg`

## Logging
MailMaster generates a Log File for every new day with the Date in the File name.
It is located inside of the Runtime Directory.
All Messages and Events get logged to the Log File.
## Miss a Feature ?
You miss a feature, then open an Issue on GitHub and i will give my best to implement it fastly, same for bugs ;-)

## Technical
MailMaster is Developed in C# .NET Core 6.0.
It runs under every Plattform supported by the .NET Runtime.

MailMaster does not use any Thirdparty Depencies only Plain Vanilla .NET it should be support every plattform where an .NET oder MONO Environment is available.




												Coded with ❤️ in 🇩🇪 
 


