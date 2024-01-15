@pushd %~dp0
@dotnet run --project ".\tools\Auxilium.Build" -- %*
@popd
