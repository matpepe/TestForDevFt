# Specify the NuGet packages to be installed
$packages = @(
    "Microsoft.EntityFrameworkCore.SqlServer/6.0.21",
    "Microsoft.EntityFrameworkCore.Design/6.0.21",
    "Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore/6.0.16",
    "Microsoft.AspNetCore.Authentication.Google/6.0.21",
    "Microsoft.AspNetCore.Identity.EntityFrameworkCore/6.0.20",
    "Microsoft.EntityFrameworkCore.Relational/6.0.21",
    "Microsoft.EntityFrameworkCore.Tools/6.0.21",
    "Microsoft.VisualStudio.Web.CodeGeneration.Design/6.0.16",
    "Microsoft.AspNetCore.Identity.UI/6.0.20"
    # Add more packages as needed
)

# Specify the NuGet package source
$nugetSource = "https://api.nuget.org/v3/index.json"

# Specify the path to the project file
$projectFilePath = "C:\Users\Mat\Desktop\RepoProj\testAPP\TestForDevFt\TestWebAppMulti\TestWebbAppMulti.csproj"

# Check if the project file exists
if (Test-Path $projectFilePath -PathType Leaf) {
    Write-Host "Project file found: $projectFilePath"

    # Get the project name from the file
    $projectName = (Get-Item $projectFilePath).BaseName

    # Loop through each package and install it for the project
    foreach ($package in $packages) {
        $packageName, $packageVersion = $package -split '/'
        Install-Package -Id $packageName -Version $packageVersion -Source $nugetSource -ProjectName $projectName
    }

    # Restore packages for the project (use dot-sourcing to run the script in the current scope)
    . $projectFilePath

    Write-Host "Packages installed and restored for $projectName"
} else {
    Write-Host "Project file not found: $projectFilePath"
}
