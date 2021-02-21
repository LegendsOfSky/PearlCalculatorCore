# PearlCalculatorCore
Just A Pearl Calculator Core Project Attempt

---

## PearlCalculationLib
this lib is for calculation. you can include this lib to your project. and use all the method with in.

the lib has been seperated in three parts.

## General
it is for calculating most of the case and traditional FTL.

## Manually
it is for manually input two TNT position and pearl position with pearl velocity to calculate how many TNT is need to go to a speicific location.

with TNT amount entered. you can calculate where it go and the velocity of it.

---

## Relay
it is **WIP** project.

look downward for more information


>PearlCalculatorCore is for debug and calculate with cmd. you can just ignore that.  
>PearlCalculatorWFA is a GUI for user to enter all the information and view on the result.  
>PearlCalculatorCP is a corss platform GUI for user to enter all the information and view on the result, base on [AvaloniaUI](https://github.com/AvaloniaUI/Avalonia)  
>RelayCalculatorWFA is a Gui for user to enter all the information and runs "Relay" part of the "PearlCalculationLib".

# How to use:

## PearlCalculationLib

* General

    in the class ***[Data](PearlCalculatorLib/General/Data.cs)***, you will need to enter all the value in order to get a correct result.

    1. for the four **TNT** data, you need to enter a coordinate which is the exploding location of the TNT.

    2. for the **destination** data, just enter a coordinate that you want your pearl to fly to.

    3. for the **pearl**. you need to fill in it's velocity(use *.WithVector()*) and position(use *.WithPosition()*).

    4. for the **BlueTNT** and **RedTNT**, fill in the amount of TNT that will explode.

    5. for the **TNTWeight**, fill in a number between **0 ~ 100**. this is for sorting result. high number mean result with higher TNT amount will be on the front of the result list.

    6. for the **MaxTick**, this indicates the maximium ticks you want your pearl to be flying.

    7. **MaxCalculateTNTMaxCalculateTNT** and **MaxCalculateDistance**, just leave it empty.

    8. **Direction**, this is for telling **Calculation** Where should the pearl fly.

    9. **DefaultRedDuper** and **DefaultBlueDuper**, they indicate where the TNT will land on the lava with out moving it. just enter direction for it.

    10. **TNTResult**, it is for keeping all the result that is calculated. it contains ticks(how many ticks the pearl need to fly), redTNT, blueTNT, distance(the distance between pearl and the destination), totalTNT(blueTNT + redTNT).

    > in the class ***[Calculation](PearlCalculatorLib/General/Calculation.cs)***, you will see two public method called *CalculateTNTAmount()* and *CalculatePearlTrace()*.  
    > they calculates how much TNT you need and the trace of the pearl.

* Manually  
    like "General", but your had to manually enter TNT location and information about pearl.

---

BTW, the calculator take TNT position and pearl Position. and calculates how TNT accelerates the pearl.

(how to get exploding location? just get download fabric carpet and use "/log tnt". beware of the spam)

