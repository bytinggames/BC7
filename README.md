# BC7 - Bot Challenge 7
## Installation and Build
Do the following, to be able to build the project:
| Windows | Linux |
| :--- | :--- |
| Install Visual Studio 2022 with package ".NET-Desktopentwicklung" selected: https://visualstudio.microsoft.com/de/vs/community/ | Install Visual Studio Code if you want. You could also use no IDE and work in a text editor. |

- Use the following command to clone this repository:<br>```git clone --recurse-submodules https://github.com/bytinggames/BC7.git```

| Windows | Linux |
| :--- | :--- |
| Run PullAllAndRun.bat in the command line (that file is located in the directory you just cloned) | Goto SE/SE/Linux/ and run Setup.sh in the terminal (this will install some stuff. Feel free to inspect the script or install stuff manually) |

- When the game runs, Everything worked! ???? <br/> Under the hood, the following happened:
  - BytingLib has been pulled from GitHub and is now located next to your BC7 folder. (BytingLib is the engine the game runs on. It is hosted in a separate repository)
  - BytingLib and BC7 have been built
  - The built game was started


## Update
If you want to pull the latest changes from BytingLib and BC7, do the following:

| Windows | Linux |
| :--- | :--- |
| Run PullAll.bat <br/>or PullAllAndRun.bat | Run PullAllLinux.sh <br/>or PullAllAndRunLinux.sh |
