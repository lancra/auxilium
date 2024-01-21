@pushd %~dp0
@dotnet run --project ".\tools\Auxilium.Dev" -- %*
@popd
