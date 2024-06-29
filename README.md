# WillFromAfarDownloader
## GUI App for downloading TTS from Acapella Group
[![Github Downloads (total)](https://img.shields.io/github/downloads/weespin/WillFromAfarDownloader/total.svg)](https://tooomm.github.io/github-release-stats/?username=weespin&repository=WillFromAfarDownloader) [![Github Downloads (total)](https://ci.appveyor.com/api/projects/status/github/weespin/WillFromAfarDownloader?branch=master&svg=true)](https://ci.appveyor.com/project/weespin26279/willfromafardownloader)   

![Screenshot](https://weesp.in/i/45006d99.png)
## How to use CLI version
Launch WillFromAfarDownloader.exe using ```-t (text) -v (voice code) -o (output file)``` arguments

Use ```--voice-list``` argument to get all voice id's
#### Example: 
```
AcapellaDownloader.exe -t Test string 1 2 3 -v enu_willhappy_22k_ns.bvcu -o test.mp3
```
## Requirements
##### Works fully on:
Windows 8, Windows 10, Windows 11
##### Without changing pitch on download:
Windows 7, Linux (under Wine) 

## Installation guide for Linux

##### Debian 
```bash
sudo dpkg --add-architecture i386
sudo apt-get update
sudo apt-get install wine32
sudo apt-get install winetricks
winetricks dotnet45
wine AcapellaDownloader.exe 
```

