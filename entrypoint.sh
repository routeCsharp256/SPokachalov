#!/bin/bash

set -e
run_cmd="dotnet OzonEdu.MerchApi.Migrator.dll --no-build -v d"

dotnet OzonEdu.MerchApi.Migrator.dll --no-build -v d -- --dryrun

dotnet OzonEdu.MerchApi.Migrator.dll --no-build -v d

>&2 echo "MerchApi DB Migrations complete, starting app."
>&2 echo "Run MerchApi: $run_cmd"
exec $run_cmd