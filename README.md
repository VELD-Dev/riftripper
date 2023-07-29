# RiftRipper
Riftripper is a tool made in C# to view, extract and edit maps of Ratchet and Clank Rift Apart, Spiderman (PS5/PC) and potentially Ratchet and Clank 2016.

## Compatibility list for the `v0.1.0`
- ✅ Ratchet & Clank: Rift Apart (PC)
- ❔ Ratchet & Clank: Rift Apart (PS5)
- ❔ Ratchet & Clank (2016)
- ❌ Marvel's Spider-Man (2018)
- ❔ Marvel's Spider-Man: Miles Morales
- ❔ Marvel's Spider-Man 2 (2023)
- ❔ Marvel's Wolverine*  
  
✅: Compatibility will be ensured.  
❔: Compatibility will be defined depending on the game engine version, if it is close enough to RiftRipper extracting and editing library.  
❌: Compatibility will not be ensured or will not be checked, or the compatibility will be impossible because of the engine version.  

## Key Features for the `v0.1.0`
- ✅ Open and view a level.
- ✅ Extract the model as an OBJ or FBX model.
- ✅ Repack the edits in the orginal file without editing the DAG/TOC hyperlinks files.
- ❔ Extract the textures.
- ❔ Repack the edits in a new file with new TOC/DAG files in order to just have to replace the orginal DAG and TOC instead of re-downloading the entire game.
- ❔ Edit the levels and save the edits in the original level or in a project file.
- ❌ Edit instance parameters of entities.
- ❌ Make new portals of any kind (on Ratchet & Clank: Rift Apart).
- ❌ Bundle several mods without breaking other mods (for that, users will have to edit themselves the mods to bundle those together).
- ❌ Export animations.
- ❌ Edit gameplay features/game's code.
- ❌ Swap/change/edit environment models
- ❌ Swap/change/edit textures
- ⚔️ Swap/change/edit entities models*  
  
✅: Will be supported, at least for **Ratchet & Clank: Rift Apart**.  
❔: Depending on what is being discovered and reversed, and depending on the events, it may possibly be supported. So maybe.  
❌: Will for sure **not** be support for this version. Maybe in the future ?  
⚔️: Will not be handled by the level editor. **You better check the other tool "[Ripped Apart](https://github.com/chaoticgd/ripped_apart)" made by <ins>chaoticgd</ins>.**  

## Key Features planned (long term)
- Open, view, edit and repack one or several levels.
- Edit models and textures of environment models.
- Edit instance parameters of every entities (crates, portals, swingers, enemies, etc...)
- Add and place custom models in the world.
- Edit collisions of the terrain, of objects and of simple entities (the ones that do not have bones, like crates)
- Add custom simple objects, such as destructible objects, world assets (with collision).
- Allow team work on levels, using a project system that only saves the ID of edited object, and all their metadatas. *(absolute rotation, absolute position, absolute scale, type, model TUID and instance parameters)*
- For users who don't want to edit levels, bundle several edits in one file, as long as they do not edit the same map.
