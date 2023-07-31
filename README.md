# RiftRipper
![GitHub release (with filter)](https://img.shields.io/github/v/release/VELD-Dev/riftripper?label=stable)
![GitHub all releases](https://img.shields.io/github/downloads/VELD-Dev/riftripper/total)
![GitHub Workflow Status (with event)](https://img.shields.io/github/actions/workflow/status/VELD-Dev/riftripper/dotnet.yml?label=nightly-builds)  

Riftripper is a tool made in C# to view, extract and edit maps of Ratchet and Clank Rift Apart, Spiderman (PS5/PC) and potentially Ratchet and Clank 2016.

## Compatibility list for the `v1.0.0`
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

## Key Features for the `v1.0.0`
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
- ⚔️ Swap/change/edit entities models<sup>**1**</sup>.  
  
✅: Will be supported, at least for **Ratchet & Clank: Rift Apart**.  
❔: Depending on what is being discovered and reversed, and depending on the events, it may possibly be supported. So maybe.  
❌: Will for sure **not** be support for this version. Maybe in the future ?  
⚔️: Will not be handled by the level editor. **You better check the other tool "[Ripped Apart](https://github.com/chaoticgd/ripped_apart)" made by <ins>chaoticgd</ins>.**  
  
> <sup>**1**</sup> <sub>You can do that with Ripped Apart, and as long as you have saved your edits in the game files, RiftRipper will show those anyway. Same for every custom entities and custom advanced objects, like custom particles, custom triggerboxes, custom activation objects or whatever.</sub>  

## Key Features planned (long term)
- Open, view, edit and repack one or several levels.
- Edit models and textures of environment models.
- Edit instance parameters of every entities (crates, portals, swingers, enemies, etc...)
- Add and place custom models in the world.
- Edit collisions of the terrain, of objects and of simple entities (the ones that do not have bones, like crates)
- Add custom simple objects, such as destructible objects, world assets (with collision).
- Allow team work on levels, using a project system that only saves the ID of edited object, and all their metadatas. *(absolute rotation, absolute position, absolute scale, type, model TUID and instance parameters)*
- For users who don't want to edit levels, bundle several edits in one file, as long as they do not edit the same map.

---

## About Nightly Builds
Nightly builds are highly unstable builds that are updated on every single change made to the editor.  
Soon, an auto-updating system will be incorporated to RiftRipper and you will be able to choose your update channel: **nightly** or **release**.  
The nightly channel is recommended only for developers, because it has debug and symbol files, and it may slow your experience using it.  
**If you want to use stable versions of the level editor, please only take releases!**

### TODO list *(features working in Nightly builds)*
All the features here are listed in the order they will be done.
- [x] `[30-05-2023]` Creating the project.
- [x] `[08-06-2023]` Project creation and loading.
- [x] `[12-06-2023]` Editor settings saving.
- [x] `[26-07-2023]` *Little pause, waiting for the game to release on PC and some reverse engineering to be done.*
- [ ] 🚧 Read `DAT1` files without extracting those.
- [ ] Archives explorer without extraction.
- [ ] Level viewing.
- [ ] *Probably a huge optimization part.*
- [ ] Level extracting to wavefront (`.obj`) files. - **Pre-Release v0.1.0**
- [ ] Level editing.
- [ ] Duplicating objects.
- [ ] Saving levels to original files. - **Release v1.0.0**
- [ ] Make an actor explorer dockable page with a viewer and an exporter. - **Pre-Release v1.0.1**
- [ ] Adding objects from the actors explorer. - **Release v1.1.0**
- [ ] Saving level(s) editions to project file. - **Release v1.2.0**
- [ ] Saving levels to new files + editing the `toc` and `dag` files to avoid overwriting original files. - **Release v1.3.0**
- [ ] Actors instance variables/fields editing. - **Release v1.4.0**
- [ ] Add new displacement portals on the map *(tagets of the rift glove)*.
- [ ] Create custom pocket-rift portals *(and link it with a target)*. - **Release v1.5.0**
- [ ] *(Create locomotion portals, with the input and output portals.)*
- [ ] Create portals to move to different levels. *(It should probably be possible)* - **Release v1.6.0**
- [ ] We'll see in the future when I'll get there.
