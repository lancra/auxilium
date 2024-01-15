#!/bin/bash
set -euo pipefail
dotnet run --project "./tools/Auxilium.Build" -- $@
