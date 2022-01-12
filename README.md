# PearlCalculatorCore

Just A Pearl Calculator Core Project Attempt.

![GitHub](https://img.shields.io/github/license/LegendsOfSky/PearlCalculatorCore)

[简体中文](README_zh-CN.md) | [繁體中文](README_zh-TW.md)

# Bug Report Rules

1. Any Bug report should be reported at GitHub Issue page. Any report on other platform won't be considered;
2. Reporters should have experienced the problem himself otherwise won't be considered;

# Prerequisites

Please install the dotnet runtime before proceeding.

Some apps run on [dotnet 6.0](https://dotnet.microsoft.com/download/dotnet/6.0/runtime)
- [PearlCalculatorCore](PearlCalculatorCore/)
- [PearlCalculatorCP](PearlCalculatorCP/)

Other apps run on [dotnet core 3.1](https://dotnet.microsoft.com/download/dotnet/3.1/runtime)

# PearlCalculatorCP

This is a cross-platform graphical frontend for receiving parameters and presenting the results, based on [AvaloniaUI](https://github.com/AvaloniaUI/Avalonia).

## Supported Language

- English
- 繁體中文
- 简体中文

## Install Instructions

### Windows

Run `cpbuild.ps1` using command prompt.

```powershell
> cpbuild.ps1
```

### macOS, Linux

Run `cpbuild.sh` using terminal. You might need to change its file permissions before running this script, i.e. `chmod +x cpbuild.sh`

```powershell
sh cpbuild.sh
```

## PearlCalculatorCP Command-line Arguments

```
options:
    -scale [ratio]      scale app based on the current screen dpi (e.g: PearlCalculatorCP -scale 1.25)
```

## Tips

- You can use `/log tnt` to obtain the location of the exploding TNT entities.
  - Provided you have fabric carpet mod installed
  - Beware of the spam
- You can use the `Options` in the top middle which hads the icon of three dots.
    - You can change the language in it. Do it twice sets the language as default.
    - You can import a json formated settings file as a default settings in it.
- This application **does not** taking care about the range limit of the explosion and might create a wrong result or trigger an exception.
- The `direction` and `angle` are displayed in the bottom right.
- In the `General` section under the `General FTL` tab
    - You should round up the `Pearl` coordinates.
    - The settings can be saved to serve multiple purposes like storing the default parameter of a FTL for a perdicular server/design or storing the current result for a server member.
        - You can name the json formated settings file whatever you want.
- In the `Advanced` section under the `General` tab
    - The offsets are the displacement between the pearl and `Chamber Center`. (Pearl - Chamber Center)
    - In the `Result Sort Control` section
        - The track bar is used to change the weight of TNT.
        - The radio buttons are used to change the sorting method.
- In the `Settings` section under the `General FTL` tab
    - The switches in the TNT sections must be enabled before changing the parameters inside it. The purpose of it is to prevent any unexpected change by accident.
    - X and Z axis are relatived to the `Chamber Center`.
    - Y Axis are global value.
    - `DefaultRedDuper` and `DefaultBlueDuper` must be `Ordinal Direction` and should be oppose to each other.
- In the `Display` tab, if it is displaying results about the TNT configuration, you can sort it by clicking the header of the `Distance`, `Ticks` and `Total` column. Smaller value will be on the top.
- In the `Manually` Tab
    - All the parameters are in global value.
    - You can Enter two sets of TNT parameter side by side.
    - If the application cannot find a possible TNT configuration, it will not give any notice.

# PearlCalculatorLib

## General

The classes under this namespace are mostly used for calculating regular 360 degree FTL and not designed for FTL which can only handle certain angles.

- ### [Data](PearlCalculatorLib/General/Data.cs)

    The purpose of this class is for storing most parameters. If the necessary field is empty when the method of the [Calculation](PearlCalculatorLib/General/Calculation.cs) class is called, it might create a wrong output or trigger an exception. The following section shows which parameters are needed for various methods in the [Calculation](PearlCalculatorLib/General/Calculation.cs) class. If the following items do not include the information about which method to be used, it will be referred by `CalculateTNTAmount`, `CalculatePearlTrace` and `CalculateTNTVector`.

    ### List of paramters to be supplied

    1. `TNT` data
        - You need to enter coordinates of the TNT.
            - X and Z axis are relatived to the `Chamber Center`.
            - Y Axis are global value.
    2. `destination` Data
        - To coordinate that you would like to teleport to
        - Required by the `CalculateTNTAmount` method.
    3. `pearl`
        - Fill in its inital velocity (use `.WithVector()` method) and position (use `.WithPosition()` method)
        - The coordinate of the pearl should be rounded up.
    4. `PearlOffset`
        - The offset between the actual pearl coordinate and the rounded up pearl coordinate.
    5. `BlueTNT` and `RedTNT`
        - Amount of TNT to be exploded
        - Not necessary and can be used for storing the data.
    6. `TNTWeight`
        - Number between 0 ~ 100.
        - Higher number means solutions with more TNTs will be shown on top.
        - For sorting the results
        - Required by the sorting comparer in the [TNTCalculationResultSortComparerByWeighted](PearlCalculatorLib/PearlCalculationLib/Result/TNTCalculationResultSortComparerByWeighted.cs) class.
    7. `MaxTNT`
        - The Max number of TNT on each side
        - Required by the `CalculateTNTAmount` method.
    8. `MaxCalculateTNT`, `MaxCalculateDistance`
        - Leave it empty
    9. `Direction`
        - Specifying the flying direction of the pearl(Only allowed for **Cardinal Direction**)
        - Required by the `CalculatePearlTrace` method.
    10. `DefaultRedDuper` and `DefaultBlueDuper`
        - Supply `Direction` as its value(Only allow for **Ordinal Direction**)
        - Indicate where the TNT will land on the lava with out moving it
    11. `TNTResult`
        - Stores the calculated result
        - Contains
            - Flight duration in ticks
            - redTNT
            - blueTNT
            - distance (displacement between the pearl and destination)
            - totalTNT (blueTNT + redTNT)

    #### Note:

    The `TNT` and `PearlOffset` parameters related to the X and Z axis, they are all relatived to the `Pearl`, also know as `Chamber Center` in `PCCSettingsGenerator`, which is considered as the origin. For the Y axis, they are all global value. The following picture provides an illustration.

    ![Figure 1](figure1.png)

    ### [Calculation](PearlCalculatorLib/General/Calculation.cs)

    This class is called for most calculations. It is composed of 4 differnt public methods which do different jobs. For the necessary parameters, please refer to [Data](PearlCalculatorLib/General/Data.cs) class.

    1. `CalculateTNTAmount` is designed to calculate the configuration of the TNT and return a `bool` value indicating whether the calculation is seccessfully finished. The results of the possible TNT configurations will be stored in the `TNTResult` which is one of the field in [Data](PearlCalculatorLib/General/Data.cs) class.

    2. `CalculatePearlTrace` is designed to simulate the pearl and retrieve its trace. It returns a list of `PearlEntity` which contains its motion and position in every tick.

    3. `CalculateTNTConfiguration` is prepared as helping users to program the cannon. By providing the value with the same order, this method can generate the programing sequences. The result is passed by reference through the `out` keyword. The return statment is returning the bool value indicating the seccessful of the calculation.

    4. `CalculateTNTVector` is designed to calculate the vectors of the pearl. For more information, please refer to the summary.

    ### [Settings](PearlCalculatorLib/General/Settings.cs)

    This class serves as a data structure only for storing data which used to serialize or deserialize a json formated settings file.

## Manually

This namespace is similar to `PearlCalculatorLib.General` namespace. They serve a similar purpose but with a different input and/or output. All coordinates under this namespace are global value unlike the `PearlCalculatorLib.General` namespace.

## PearlCalculationLib

This namespace is used for providing an useful tools for other classes to handle complex calculations.

- ### AABB

    This namespace is reserved for furture development.

- ### Entity, MathLib, Utility

    Those namespaces are used to provide some useful tools for other classes.

- ### Result

    This namespace is used for handling the result of `CalculateTNTAmount`. It contains sorting comparers for sorting with different order base on `TNT amount`, `ticks`, `distance`.

- ### World

    This namespace is used for providing the useful tools for handling the world coordinates and direction.

- ### [VectorCalculation](PearlCalculatorLib/PearlCalculationLib/VectorCalculation.cs)

    This class is aimed for calculating the acceleration of the pearl with the given input of TNT coordinates and pearl coordinates. Please note that it doesn't handle the distance limit set by **Mojang** and if it is over the raycasting range, it might return a wrong result or throw an exception.

# PCCSettingsGenerator (Folder name : PCCSettingsCalculator)

Sorry about the wrong file name, but changing it causes problem. 

What this project does is to help FTL designers to generate settings easily by providing some parameters taken from the game and generate the settings you want. It can also save the settings to a json formated settings file for PearlCalculatorCP to use.

## Supported language

- English
- 繁體中文
- 简体中文

If you want to provide a language support, feel free to provide a resx formated language file in [PCCSettingsCalculator/Resources/](PCCSettingsCalculator/Resources/) folder through pull request.

# PearlCalculatorWFA

This is more like a debug purposed version of `PearlCalculatorCP`. It have intergrated some useful commands for peeking and changing the hidden parameters. It `does not` features language support other than `English` and we are rarely maintain it.

# PearlCalculatorCore

This is purely make for `Debug Purposed` and not for anyone to used it. It is used to varify the correctness of the `PearlCalculatorLib`.