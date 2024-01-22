try {
    Push-Location $PSScriptRoot
    dotnet run --project "./tools/Auxilium.Dev" -- $args
    if ($LASTEXITCODE) {
        throw "Build failed with exit code $LASTEXITCODE."
    }
}
finally {
    Pop-Location
}
