[CmdletBinding()]
param ()

class PackageManager {
    [string]$Name
    [string]$CommandFormat
    [string]$MissingError
    [Linter[]]$Linters

    PackageManager([string]$name, [string]$commandFormat, [string]$missingError, [Linter[]] $linters) {
        $this.Name = $name
        $this.CommandFormat = $commandFormat
        $this.MissingError = $missingError
        $this.Linters = $linters
    }
}

class Linter {
    [string]$DisplayName
    [string]$PackageName

    Linter([string]$displayName, [string]$packageName) {
        $this.DisplayName = $displayName
        $this.PackageName = $packageName
    }
}

enum InstallationResultKind {
    Failed
    Skipped
    Succeeded
    Verified
}

class InstallationResult {
    [string]$Name
    [InstallationResultKind]$Kind
    [InstallationResult[]]$DependentInstallationResults

    InstallationResult([string]$name, [InstallationResultKind]$kind) {
        $this.Name = $name
        $this.Kind = $kind
        $this.DependentInstallationResults = @()
    }

    InstallationResult([string]$name, [InstallationResultKind]$kind, [InstallationResult[]]$dependentInstallationResults) {
        $this.Name = $name
        $this.Kind = $kind
        $this.DependentInstallationResults = $dependentInstallationResults
    }
}

function Test-Executable {
    [CmdletBinding()]
    [OutputType([bool])]
    param (
        [Parameter(Mandatory)]
        [string]$Name
    )
    process {
        $null -ne (Get-Command -Name $Name -ErrorAction SilentlyContinue)
    }
}

function Write-InstallationResult {
    [CmdletBinding()]
    param (
        [Parameter(Mandatory)]
        [InstallationResult]$Result,

        [Parameter(Mandatory)]
        [int]$NameLength,

        [Parameter()]
        [int]$IndentCount = 0
    )
    process {
        for ($i = 0; $i -lt $IndentCount; $i++) {
            Write-Host '  ' -NoNewline
        }

        $displayName = $Result.Name.PadRight($NameLength, '.')
        $color = switch ($Result.Kind) {
            Failed { [System.ConsoleColor]::Red }
            Skipped { [System.ConsoleColor]::Red }
            Succeeded { [System.ConsoleColor]::Green }
            Verified { [System.ConsoleColor]::Green }
        }

        Write-Host "$displayName....." -NoNewline
        Write-Host $Result.Kind -ForegroundColor $color
    }
}

$packageManagers = @(
    [PackageManager]::new(
        'cargo',
        'cargo install {0}',
        'cargo was not found on the system. Please add it to the PATH or download it via Rust at https://rustup.rs.',
        @(
            [Linter]::new('typos', 'typos-cli')
        )
    ),
    [PackageManager]::new(
        'npm',
        'npm install --location=global {0}',
        'npm was not found on the system. Please add it to the PATH or download it via Node.JS at https://nodejs.org.',
        @(
            [Linter]::new('markdownlint', 'markdownlint-cli'),
            [Linter]::new('prettier', 'prettier')
        )
    ),
    [PackageManager]::new(
        'pip',
        'pip install {0}',
        'pip was not found on the system. Please add it to the PATH or download it via Python at https://python.org.',
        @(
            [Linter]::new('yamllint', 'yamllint')
        )
    )
)

$packageManagerDisplayLength = ($packageManagers |
    Select-Object -ExpandProperty Name |
    Sort-Object -Property Length -Descending |
    Select-Object -First 1).Length

$linterDisplayLength = ($packageManagers |
    Select-Object -ExpandProperty Linters |
    Select-Object -ExpandProperty DisplayName |
    Sort-Object -Property Length -Descending |
    Select-Object -First 1).Length

$installationResults = @()
$exitCode = 0

Write-Host 'EXECUTION:'
foreach ($packageManager in $packageManagers) {
    Write-Host "Package manager verification: $($packageManager.Name)"
    $packageManagerAvailable = Test-Executable -Name $packageManager.Name
    $packageManagerInstallationResultKind = $packageManagerAvailable `
        ? [InstallationResultKind]::Verified `
        : [InstallationResultKind]::Skipped

        if (-not $packageManagerAvailable) {
            Write-Error $packageManager.MissingError
        }

        $linterInstallationResults = @()
        foreach ($linter in $packageManager.Linters) {
            Write-Host "Linter installation: $($linter.DisplayName)"
            $linterAvailable = Test-Executable -Name $linter.DisplayName

            $linterInstallationResultKind = $null
            if (-not $packageManagerAvailable -and -not $linterAvailable) {
                $linterInstallationResultKind = [InstallationResultKind]::Skipped
                Write-Error "Unable to install $($linter.DisplayName) since the $($packageManager.Name) package manager is unavailable"
            } elseif ($linterAvailable) {
                $linterInstallationResultKind = [InstallationResultKind]::Verified
            } else {
                $linterInstallCommand = $packageManager.CommandFormat -f $linter.PackageName
                Invoke-Command -ScriptBlock ([scriptblock]::Create($linterInstallCommand))

                $linterInstallationResultKind = $LASTEXITCODE -eq 0 `
                    ? [InstallationResultKind]::Succeeded `
                    : [InstallationResultKind]::Failed
            }

            $linterInstallationResults += [InstallationResult]::new($linter.DisplayName, $linterInstallationResultKind)

            if ($linterInstallationResultKind -in [InstallationResultKind]::Skipped,[InstallationResultKind]::Failed) {
                $exitCode = 1
            }
        }

    $installationResults += [InstallationResult]::new(
        $packageManager.Name,
        $packageManagerInstallationResultKind,
        $linterInstallationResults)
}

Write-Host
Write-Host 'RESULTS:'
foreach ($packageManagerResult in $installationResults) {
    Write-InstallationResult -Result $packageManagerResult -NameLength $packageManagerDisplayLength
    foreach ($linterResult in $packageManagerResult.DependentInstallationResults) {
        Write-InstallationResult -Result $linterResult -NameLength $linterDisplayLength -IndentCount 1
    }
}

exit $exitCode
