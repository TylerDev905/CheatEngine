# Setting up Code Designer

#### 1. Open a powershell window with elevated privledges

#### 2. Allow scripts to run on a windows machine
```powershell
set-executionpolicy remotesigned
```

#### 3. Execute install.ps1
```powershell
.\Install.ps1
```

﻿
## 1. Basic usage structure
```powershell
CodeDesigner <Verb> [Options]
```

## 2. Detailed Usage
```powershell

CodeDesigner MipsR9000 
	options: 
			 -a --Assemble
			 -h --OperationHex	 
			 -d --Disassemble
			 -o --Operation
			 
CodeDesigner Pcsx2
	options: 
			 -r --ReadOperation
			 -o --Operation
			 -w --WriteOperation
			 -a --Address
			 -s --StartProcess
			 -i --InstallAndConfig
			 -p --Pcsx2 version

CodeDesigner CDL
	options:
			 -c --Compile
			 -d --Decompile
			 -s --Source

CodeDesigner CheatEngine 
```
