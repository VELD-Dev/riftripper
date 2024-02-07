# RiftRipper
![GitHub release (with filter)](https://img.shields.io/github/v/release/VELD-Dev/riftripper?label=stable)
![GitHub all releases](https://img.shields.io/github/downloads/VELD-Dev/riftripper/total)
![GitHub Workflow Status (with event)](https://img.shields.io/github/actions/workflow/status/VELD-Dev/riftripper/dotnet.yml?label=nightly-builds)
![GitHub lines count (Tokei)](https://tokei.rs/b1/github/VELD-Dev/riftripper?category=lines&type=CSharp)

Riftripper is a tool made in C# to view, extract and edit maps of R**atchet and Clank: Rift Apart**, **Spiderman (PS5/PC)** and potentially **Ratchet and Clank 2016**.  

## Summary
* 🌐 [**General information**](#-general-information)
  * ✅ [*Compatibility list*](#-compatibility-list)
  * 🌃 [*About Nightly Builds*](#-about-nightly-builds) (It's very important, **READ THIS PART**)
  * 🚧 [*TODO list (features working in Nightly builds)*](#-todo-list-features-working-in-nightly-builds)
  * 🔑 [*Key Features for the v1.0.0*](#-todo-list--features-for-the-v100)
* 📥 [**Installation**](#-installation)

---

# 🌐 General information
General information about the software, such as the compatibility list.

## ✅ Compatibility list
- ✅ Ratchet & Clank: Rift Apart (PC)
- ❔ Ratchet & Clank: Rift Apart (PS5)
- ❔ Ratchet & Clank (2016)
- ❔ Marvel's Wolverine<sup>1</sup> (2026)
- ❔ Marvel's Spider-Man 2 (2023)
- ❔ Marvel's Spider-Man: Miles Morales
- ❌ Marvel's Spider-Man (2018) 
  
✅: Compatibility will be ensured.  
❔: Compatibility will be defined depending on the game engine version, if it is close enough to what the RiftRipper extracting and editing library can support.  
❌: Compatibility will not be ensured or will not be checked, or the compatibility will be impossible because of the engine version.  
> <sup>1</sup><sub>Wolverine should be supported on its launch, but the compatibility status may change on release and become incompatible.</sub>

## 🌃 About Nightly Builds
Nightly builds are highly unstable builds that are updated on every single change made to the editor.  
Soon, an auto-updating system will be incorporated to RiftRipper and you will be able to choose your update channel: **nightly** or **release**.  
The nightly channel is recommended only for developers, because it has debug and symbol files, and it may slow your experience using it.  
**If you want to use stable versions of the level editor, please only take releases!**  

**For the ones who were too lazy to read the text above like me, basically it means that it's the very latest versions containing all the features working in the level editor actually, and that the list below here are all the features that have been made and that are working.**

## 🚧 TODO list *(features working in Nightly builds)*
All the features here are listed in the order they will be done.
- `[30-05-2023]` Project creation.
- [x] `[08-06-2023]` Project creation and loading.
- [x] `[12-06-2023]` Editor settings saving.
- *Little pause, waiting for the game to release on PC and some reverse engineering to be done.*
- [ ] 🚧 Level viewing.
  - [ ] *Probably a huge optimization part.*
- [ ] 🕰️ Read `DAT1` files without extracting those.
- [ ] Archives explorer without extraction.
- [ ] Archives selective extraction.
- [ ] Level extracting to wavefront (`.obj`) files.
- [ ] Level editing.
- [ ] Duplicating objects.
- [ ] Saving levels to original files.
- [ ] Make an actor explorer dockable page with a viewer and an exporter.
- [ ] Adding objects from the actors explorer.
- [ ] Saving level(s) editions to project file.
- [ ] Saving levels to new files + editing the `toc` and `dag` files to avoid overwriting original files.
- [ ] Actors instance variables/fields editing.
- [ ] Add new displacement portals on the map *(tagets of the rift glove)*.
- [ ] Create custom pocket-rift portals *(and link it with a target)*.
- [ ] *(Create locomotion portals, with the input and output portals.)*
- [ ] Create portals to move to different levels. *(It should probably be possible)*
- [ ] We'll see in the future when I'll get there.

## 🔑 TODO List / features for the `v1.0.0`
- ✅ Open and view a level.
- ✅ Extract models and levels as OBJ or FBX model files.
- ❔ Extract the textures.
- ❔ Repack the edits in a new file with new TOC/DAG files in order to just have to replace the orginal DAG and TOC instead of re-downloading the entire game.
- ❔ Edit the levels and save the edits in the original level or in a project file.
- ❌ Repack the edits in the orginal file without editing the DAG/TOC hyperlinks files.
- ❌ Edit instance parameters of entities.
- ❌ Make new portals of any kind (on Ratchet & Clank: Rift Apart).
- ❌ Bundle several mods without breaking other mods (for that, users will have to edit themselves the mods to bundle those together).
- ❌ Export animations.
- ❌ Edit gameplay features/game's code.
- ❌ Swap/change/edit environment models
- ❌ Swap/change/edit textures
- ⚔️ Swap/change/edit entities models<sup>**1**</sup>.  
  
✅: Will be supported, at least for **Ratchet & Clank: Rift Apart**.  
❔: Depending on what is being discovered and reversed, and depending on the events, it may possibly be supported. So, maybe.  
❌: Will for sure **not** be support for this version. Maybe in the future ?  
⚔️: Will not be handled by the level editor. **You better check for an other tool.**

---

# 📥 Installation
Here are some steps you should check before downloading **RiftRipper**.
- 🖥️ RiftRipper is actually only compatible with **Windows 8.1 and higher**, and **Linux/Ubuntu 21+** (older versions have not been tested).
- 📚 Check if you have **.NET 7.0** installed on your computer. If not, [**download it here**](https://download.visualstudio.microsoft.com/download/pr/4c0aaf08-3fa1-4fa0-8435-73b85eee4b32/e8264b3530b03b74b04ecfcf1666fe93/dotnet-sdk-7.0.306-win-x64.exe "Official link at https://download.visualstudio.microsoft.com/").
- ⚠️ The archive file you download from **Releases** page is not an installer, be careful and store the software somewhere you'll not lose it!
- ⚠️ Riftripper is still very work in progress. To see what is actually working in the latest version of the level editor, please refer to the next section.
