$publishFolderRoot = ".\artifacts";
$dotnetVer = "netcoreapp3.1"

$rids = "win-x64", "linux-x64", "osx-x64"

foreach ($r in $rids)
{
    Write-Host "====================================="
    Write-Host "build"${r}""
    dotnet publish .\PearlCalculatorCP\PearlCalculatorCP.csproj -c Release -f ${dotnetVer} --no-self-contained --nologo -r ${r} -o ${publishFolderRoot}\${r}
}