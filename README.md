# thc
## th executer from console
runs thcrap to launch touhou with less args than standard link to thcrap_loader.exe.

if no arguments, it launching standard touhou `th06`, language `en.js` and path is program's place. if standard th is `""`, then shows manual for this program. if other lines equals `""` it will be noticing you about it.

## installation
installs in downloaded from https://github.com/thpatch/thcrap directory, or another directory, but path to `thcrap` directory must be stored in `ThcrapPath`.

## configuration
### before first launch
place this program in thcrap directory with `bin`, `config`, `thcrap_loader.exe` and other. 

### after first launch
creating directory with name `thc config` by default in same directory as program. there can be found `thc.json` file where stored configuration for this program. also you can move `thc.exe` and `thc config` to another place, because program was configured in first launch, and in property `ThcrapPath` stores path to directory where was placed `thc.exe`.

### reconfigure
configurable by command like `.\thc --thcrap {path}` or by changing data in json file `thc config\thc.json` `ThcrapPath: "{path}"`.

## other information
recommend set `thcrap` directory with `thc.exe` and `thc config` files to PATH variable to easiely run `thc.exe` from everywhere.

for more information launch in console `.\thc.exe --help` in same folder in cmd or powershell as the program.
