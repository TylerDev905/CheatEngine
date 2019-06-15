param (
	[string] $version = "1.4.0",
	[switch] $force = $true,
	[switch] $pcsx2Run = $false,
	[switch] $pcsx2Configure = $true
)
$pcsx2 = "${env:ProgramFiles(x86)}\Pcsx2 $version"
$pcsx2ConfigPath = "${env:GetFolderPath(MyDocuments)}\Pcsx2"

if(-not (Test-Path $pcsx2) -or ($force))
{
	Write-Output "Installing/Updating chocolatey..."
	Set-ExecutionPolicy Bypass -Scope Process -Force; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))
	
	Write-Output "Installing/Updating Pcsx2..."
	choco install pcsx2 -y --version $version

	$bios = "$pcsx2ConfigPath\Bios"
	if(-not (Test-Path $bios))
	{
		Write-Output "Creating bios directory..."

		mkdir $bios
	}
	$tempZip = "$pcsx2\bios.zip"
    
	Write-Output "Downloading bios..."
	$client = new-object System.Net.WebClient
	$client.DownloadFile("https://srv-file2.gofile.io/download/awS6o3/bios.zip", $tempZip)

	Write-Output "Extracting bios to $bios"
	Expand-Archive $tempZip -DestinationPath $bios -Force
}

$iniPath = "$pcsx2ConfigPath\inis_$version"

if($pcsx2Configure)
{
	##$pcsx2UI_INI = Get-Content "$iniPath\PCSX2_ui.ini" | Select -Skip 1 | ConvertFrom-StringData
	##Write-Output $pcsx2UI_INI.Bios
}

if($pcsx2Run)
{
	Start-Process "$pcsx2\pcsx2.exe"
}