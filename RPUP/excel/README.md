```=IF(COUNT(C5:F5)=0,"Nije pisao",IF(COUNT(C5:F5)<>COUNTA(C5:F5),"Prepisivanje",VLOOKUP(G5,$J$3:$K$7,2,1)))```
![](https://github.com/kkeglje/university/blob/master/RPUP/excel/pictures/Slika1.png)

=====================================================================================

```
=COUNTIF($H$3:$H$79,K3) A
=L3/COUNTA($B$3:$B$79)  B
```
![](https://github.com/kkeglje/university/blob/master/RPUP/excel/pictures/Slika2.png)

=====================================================================================

```=COUNTIFS($C$3:$C$79,G4,$D$3:$D$79,1)``` Promjeni zadnji parametar ovisno koja je godina
![](https://github.com/kkeglje/university/blob/master/RPUP/excel/pictures/Slika3.png)

=====================================================================================

```
=IFERROR(IF(VLOOKUP(D3,$A$3:$B$95,2,FALSE)="Sway","Prezi",
IF(VLOOKUP(D3,$A$3:$B$95,2,TRUE)="Prezi","Sway")),
IF(RANDBETWEEN(0,1)=0,"Sway","Prezi"))```
```
Pretty print

```
IFERROR(
    IF(
        VLOOKUP(D3,$A$3:$B$95,2,FALSE)="Sway",",
        IF(
        VLOOKUP(D3,$A$3:$B$95,2,TRUE)="Prezi",
        "Sway")
     ),
IF(RANDBETWEEN(0,1)=0,"Prezi","Sway")
)
```
sway..prezi

=====================================================================================

```=VLOOKUP($A4,$M$5:$Q$20,B$2,0)```

![]("https://github.com/kkeglje/university/blob/master/RPUP/excel/pictures/Slika4.png")

=====================================================================================

```=VLOOKUP(F3,$A$3:$B$8,2,1)```

![](https://github.com/kkeglje/university/blob/master/RPUP/excel/pictures/Slika5.png)

=====================================================================================

```=VLOOKUP(A3,Skladište!A:E,4,0)```

![](https://github.com/kkeglje/university/blob/master/RPUP/excel/pictures/Slika6.png)

=====================================================================================

```=VLOOKUP(A20,A3:B14,2,0)```

![](https://github.com/kkeglje/university/blob/master/RPUP/excel/pictures/Slika7.png)

=====================================================================================


Formatiraj tako da bude € na kraju a - ispred negativnog broja

![](https://github.com/kkeglje/university/blob/master/RPUP/excel/pictures/FormatCells.png)

=====================================================================================

```=(TODAY()-C2)/365```

![](https://github.com/kkeglje/university/blob/master/RPUP/excel/pictures/BrojGod.png)


=====================================================================================
