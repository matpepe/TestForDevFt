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

# Specify the solution path
$solutionPath = "C:\Users\Mat\Desktop\RepoProj\testAPP\TestForDevFt\TestForDevFt.sln"

# Get all project files in the solution
$projectFiles = Get-ChildItem -Path (Join-Path $solutionPath "*.csproj") -Recurse

# Loop through each project and install packages
foreach ($projectFile in $projectFiles) {
    $projectName = $projectFile.BaseName
    Write-Host "Installing packages for $projectName..."
    
    foreach ($package in $packages) {
        $packageName, $packageVersion = $package -split '/'
        Install-Package -Id $packageName -Version $packageVersion -Source $nugetSource -ProjectName $projectName
    }
}

# Restore packages for the solution
Restore-Package -ProjectName TestWebAppMulti
