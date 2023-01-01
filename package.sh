#!/bin/bash

if [ ! $1 ]; then
    echo "Error: The mod version is required."
    exit
fi

for VERSION in 1.2 1.3 1.4
do
    cp -av /rimworld/$VERSION/Mods/ZatGun/$VERSION/Assemblies/ZatGun.dll ZatGun/$VERSION/Assemblies/
done

# Make the release folder:
mkdir -p releases
cp -a ZatGun releases/

# Remove the incomplete research defs.
rm -rvf releases/ZatGun/Defs/ResearchDefs

# Add the LICENSE and README.
cp LICENSE README.md releases/ZatGun/

# Create the zip.
(
    cd releases
    zip -r ../ZatGun-$1.zip ZatGun
)
