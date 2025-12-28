This is meant to be used with xi-tinkerer. https://github.com/InoUno/xi-tinkerer
Credit to InoUno for his work on converting dats to human readable formats.

This can also work with the xidb from the landsandboat private server(https://github.com/LandSandBoat/server). Credit to that team and its lineage all the way back to the Darkstar team.

This tool was built for personal use, so it's not very polished, but it might be useful for others...
It requires a Project folder and folder structure similarly to what xi-tinkerer builds out. The one change is that it also can use a yaml_patch directory which mirrors the other directories, but can contain files that are a subset of the full files. Their purpose is to save off your changes so they can be patched easily into new versions of the dats as retail changes them. The naming scheme of the patch files mirrors the xi-tinkerer naming scheme, but with _patch appended to the end of the name. armor_patch.yml, armor2_patch.yml. There is a menu option when running the app that lets you apply the patch to the files exported from dats.

This will also read the xidb from the landsandboat private server(https://github.com/LandSandBoat/server) and will patch most of the items and spells. This lets you save your private server item edits and apply those edits to new versions of the dats as they come out. Adjust the connection string in appSettings.json to connect to your instance.

xi_tinkerer's cli tool doesn't seem to export the general_items(118/106.dat) accurately and I couldn't figure out why. to work with it, I manually exported it with the xi-tinkerer windows app and dropped it in the appropriate directory in my tool. I was lazy...

There's an option to patch the xidb from the dat files. I think it's necessary to patch the database so that my tool will properly build the spell dat. There's some values in xidb that seem to break the dat as they are probably not accurate. Patch xidb. apply your spell changes to the db. patch yaml. generate dats.
