# Discord-Nitro-Sniper
[![Current Version](https://img.shields.io/badge/version-1.0.1-green.svg)](https://github.com/Obscurely/PyPassMan)<br />
C# Discord Nitro Sniper. Multi-Threaded and Async.

## Table of contents
- [General info](#general-info)
- [Technologies](#technologies)
- [Program dependencies](#program-dependencies)
- [Setup](#setup)
  - [To run Discord Nitro Sniper](#to-run-discord-nitro-sniper)
    - [For master branch version](#for-master-branch-version)
    - [For release version](#for-release-versions)
  - [To install Discord Nitro Sniper](#to-install)
  - [To uninstall Discord Nitro Sniper](#to-uninstall)
- [How it works](#how-it-works)
- [If you want to use my work](#if-you-want-to-use-my-work)
- [Screenshots](#screenshots)
- [Other notes](#other-notes)

## General Info
A, quite simple, discord nitro sniper made in C#. It uses Multi Threading and Async for the Http Calls. By default it's made to use 16 threads which works very well and doesn't bottleneck with my 2 computers (one with 16 threads cpu and one with 8 threads cpu), but if you have problems remove some of them and recompile it.
Also this was made for fun and I am not responsible for anything that might happen to you. Acording to disocrd TOS this is not illegal, but your ISP seeing the constant requests made on your traffic may get into problems, personally i haven't got into any trouble ever for this stuff, but is always good to take into account the country where are you from. Also there might be bugs, it doesn't seem to be any from my tests, but who knows.
When the pogram is ran it will create 16 filse for the threads (by default) and when if finds a nitro code it will make an additional file called working_codes.txt and will output that code and all the next ones into it.

## Technologies
Project is created with:
* C# 9

## Program dependencies
Nothing extra needs to be installed. Only uses default C# libraries.

## Setup
### To run Discord Nitro Sniper:
#### For master branch version:
* Download the solution.
* Compile it.

#### For release versions:
* Download the archive for your os.
* Extract everything into a folder.
* Double-click DiscordNitroSniper.

#### To install:
* Very simple, download and run the installer (windows only).
* Click next as much as needed.
* Search for DiscordNitroSniper is start menu and that's it.

### To uninstall:
* Delete the folder if you have downloaded the portable version.
* Uninstall from control panel if you installed it.

## How it works
It has a constant string with all the printable chars, than uses Random to get an index and addes the char at that index to another string, repeates this process 16 times and it has a random code. Than it checks this code by making an Http Request using proxies (from this site: https://www.proxyscrape.com/free-proxy-list and the proxy is selected randomly), it has a timeout for 3 seconds so it skips the proxy if it's too slow. It check the status code of the reset if it was done in the time limit and if it gets and error will not print anything, but if it gets 404 which is Unknow Code or 200 for Good Code will print that to the console. If it hits a working code than it will create a file called working_codes.txt in which will append all of them.
This whole task is done continously using 16 threads in order to be fast. Also running multiple can work, but it may be slower.


## If you want to use my work
Just stick to the **license conditions**: Your program has to be **open-source**, **credit me**, use the **same license** and **state any changes**. Else you are free to use anything.

## Screenshots
![Main Window](https://github.com/Obscurely/Discord-Nitro-Sniper/blob/master/Screenshots/screenshot.png)

## Other notes
* Any encountered issues please post them in the issues section.
