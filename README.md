# Blackjack

Terminologi:
En hand är nuvarande runda
Ens egen hand är sin giv


# Regler för spelet:
* Man ska få 21
* Om man får 21 på första draw får man extra vinst
* Man förlorar om man får över 21
* Huset vinner oavgjorda situationer
* Man kan splitta sin hand för att öka/minska risk
* Vi kan kolla upp det
* Dealen stannar på ett värde
* Vi kan kolla upp det
* Alla kungliga valörer är lika med 10 i värde
* Ess är 11 & 1
* Siffervalörer är sin motsvarande siffra i värde
* Färger spelar ingen roll, bara estetiskt och att det finns 4 av varje valör.
* 52 kort totalt
* Blanda kortleken och dela ut kort, ett kort per spelare tills alla spelare har fått 2 kort var.
* Alla spelare spelar sin omgång tills de vinner/förlorar mot husets hand, ej turvis
* Man kan satsa pengar också

# Vilka klasser?

Rita upp spelplanen (“Graphics”)
animationer blir för svårt?
Kanske kan ha en “mid frame”, Sleep(1000)
Man ser kortens baksida innan de vänds åt rätt håll
Spelplanen är milt grön
Olika bildlägen. Kan växla mellan “fokusläge” och “vidvinkelläge”
Top down-grafik


# Kortklass (enum?) (Cards)
Ascii art?
https://www.asciiart.eu/miscellaneous/playing-cards
DrawCard-metod som tar emot enum
En array/lista för att “dra” kort från vår hög

# Regler för spelet (GameLogic)
Kollar all logik


# Spelarklass (Player)
Kan ställa in antal spelare som ska spela (max X antal spelare)
Egenskaper: namn, en wallet (kanske kan spara mellan omgångar), skuld, belåning,  “världsranking” = vem som har mest pengar/flest vinster
FileManager
Läsa och spara information mellan omgångar
