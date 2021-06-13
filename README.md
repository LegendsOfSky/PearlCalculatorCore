# PearlCalculatorCore

Just A Pearl Calculator Core Project Attempt

Bug Report Rules :
1. Any Bug report should be reported at GitHub Issue Page. Any report on other platform won't be considered;
2. Reporters should have experienced the problem himself otherwise won't be considered.

## Usages

This application supports multiple platforms (Windows, macOS, Linux). Please view the appropriate instructions for your platform.

### Prerequisites

Please install the dotnet Core runtime before proceeding.

- dotnet Core 3.1 (Link: https://dotnet.microsoft.com/download)

### Install Instructions

#### Windows

Run `cpbuild.ps1` using command prompt.

```powershell
> cpbuild.ps1
```

#### macOS, Linux

Run `cpbuild.sh` using terminal. You might need to change its file permissions before running this script, i.e. `chmod +x cpbuild.sh`

```powershell
sh cpbuild.sh
```

## Walkthrough on the core module

The core module of this application is the `PearlCalculationLib` module. The following gives a brief introduction for it.

### PearlCalculationLib

#### Overview

- Core module for the calcualtion.
- Can be included for your project
  - All methods are accessible and callable

#### Composition

The `PearlCalculationLib` is composed of three parts.

##### General

- Carrying out calculations for most cases and traditional FTL set-ups

##### Manually

- Receieves manual input of two TNT positions with peral velocity
  - To calculate how many TNTs are required for teleportation

##### Relay

- **Work in progress.**
- Purposes of modules included

## Composition

| Name                  | Purpose                                                      |
| --------------------- | ------------------------------------------------------------ |
| `PearlCalculatorCore` | Testing purposes                                             |
| `PearlCalculatorWFA`  | Windows-specific graphical frontend for receiving parameters and presenting the results |
| `PearlCalculatorCP`   | Cross-platform graphical frontend for receiving parameters and presenting the results, based on [AvaloniaUI](https://github.com/AvaloniaUI/Avalonia) |
| `RelayCalculatorWFA`  | A graphical frontend for receiving parameters and pass them to the "Relay" part of the `PearlCalculationLib` |

## API Manual

### PearlCalculationLib

#### General Part

You need to supply all values to get a valid result whenever you called the ***[Data](PearlCalculatorLib/General/Data.cs)*** class.

##### List of data to be filled

1. `TNT` data
   - You need to enter a coordinate as the origin of TNT explosion for all four **TNT** data.
2. `destination` Data
   - To coordinate that you would like to teleport to
3. `pearl`
   - Fill in its inital velocity (use `.WithVector()` method) and position (use `.WithPosition()` method)
4. `BlueTNT` and `RedTNT`
   - Amount of TNT to be exploded
5. `TNTWeight`
   - Number between 0 ~ 100.
   - Higher number means solutions with more TNTs will be shown on top.
   - For sorting the results
6. `MaxTNT`
   - The Max number of TNT in each side
7. `MaxCalculateTNT`, `MaxCalculateDistance`
   - Leave it empty
8. `Direction`
   - Specifying the flying direction of the pearl
9. `DefaultRedDuper` and `DefaultBlueDuper`
   - Supply `Direction` as its value
   - Indicate where the TNT will land on the lava with out moving it
10. `TNTResult`
    - Stores the calculated result
    - Contains
      - Flight duration in ticks
      - redTNT
      - blueTNT
      - distance (displacement betwen origin and destination)
      - totalTNT (blueTNT+redTNT)

##### Remarks

In the  ***[Calculation](PearlCalculatorLib/General/Calculation.cs)*** class, there are two public methods called `CalculateTNTAmount()` and `CalculatePearlTrace()` respetively.
They calculate how much TNT is required and the trace of the pearl.

#### Manual Part

Similar to  "General", but your had to manually enter TNT location and information about pearl.

## Tips

- The calculator take TNT position and pearl Position. and calculates how TNT accelerates the pearl.
- Use `/log tnt` to obtain the realtime location of TNT entities.
  - Provided you have fabric carpet mod installed
  - Beware of the spam
