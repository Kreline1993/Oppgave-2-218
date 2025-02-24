# Oppgave 2: Geografisk IT-utvikling - Gruppe 8
## Gruppemedlemmer
- Kevin Grefstad Gundersen 
- Magi Natasha Nakangi 
- Natasha Engemann 
- Truls Iver Michalsen Iversen 
- Martin Brådland 

## Introduksjon
I denne oppgaven skal vi lage en webapplikasjon som viser informasjon om vindkraftverk 
i Agder.

## Problemstilling
Hvordan kan vindkraftressursene i Agder utnyttes på en bærekraftig og effektiv 
måte for kraftproduksjon, samtidig som hensynet til vernede naturområder og 
landskapsverdier ivaretas?

Denne problemstillingen tar for seg hvilke områder i Agder som har størst potensial for
vindkraftproduksjon, samt hvordan man kan balansere utbygging med miljøhensyn og lokal 
acceptans.

## Valg av teknologier
I prosjektet vårt har vi valgt å bruke Visual Studio, som utviklingsmiljø, ettersom vi 
allerede har erfaring med plattformen. Det var viktig for oss å bygge videre på vår 
eksisterende kunnskap for å arbeide mer effektivt og fokusere på selve utviklingsprosessen 
fremfor å bruke tid på å lære et nytt verktøy.

For kartvisning og håndtering av geografiske data har vi valgt Leaflet, ettersom 
vi også har tidligere erfaring med dette biblioteket. Leaflet er fleksibelt og godt 
egnet for vår bruk, samtidig som det gir oss frihet til å tilpasse kartløsningen etter 
behov.

Prosjektet skal utvikles som en .NET 8 MVC-applikasjon, så er det naturlig å bruke C#, 
hvor rammeverket er optimalisert for C#. Javascript brukes til interaktive kartfunksjoner,
og er et essensielt språk for Leaflet. I tillegg brukes det til å skape dynamiske 
nettsider.

For databasen har vi valgt Supabase, som er nytt for oss. Vi ser på dette som en 
spennende mulighet til å lære og utforske nye teknologier, samtidig som vi drar nytte 
av Supabase sine funksjoner for håndtering av data, autentisering og 
sanntidsoppdateringer. Selv om det er en ny plattform for oss, mener vi at den 
gir gode muligheter for en moderne og fleksibel løsning.

Samlet sett har vi valgt teknologier som balanserer mellom det vi har erfaring med, 
og nye verktøy vi ønsker å utforske. Dette gir oss et godt grunnlag for å utvikle 
en solid løsning samtidig som vi får muligheten til å utvide vår tekniske kompetanse.

## Valg av datasett

- Som kartgrunnlag har vi valgt å bruke kartdata fra Kartverket, som er en pålitelig kilde for geografiske data i Norge. Vi har valgt å bruke data fra N50-serien, som er en detaljert kartserie som dekker hele landet. Dette gir oss et godt grunnlag for å vise vindkraftverk og annen informasjon på et detaljert kartgrunnlag. Dette kan finnes her [Geonorge](https://kartkatalog.geonorge.no/metadata/ea192681-d039-42ec-b1bc-f3ce04c189ac)]
- For visning av vindstyrke og terengkompleksitet har vi valgt å bruke data fra NVE (Norges vassdrags- og energidirektorat). Dette gir oss et godt grunnlag for å vise potensialet for vindkraftproduksjon i Agder, samtidig som vi kan vise informasjon om terrenget og vindforholdene i området. Dette kan finnes her [Geonorge](https://kartkatalog.geonorge.no/metadata/vindressurser/21079f3d-81b8-405b-bfb1-c213d732fcfb))
- For data om vindkraftverk har vi valgt å bruke data fra NVE, som gir oss informasjon om vindkraftverk i Norge. Dette gir oss et godt grunnlag for å vise informasjon om vindkraftverk i Agder, samtidig som vi kan vise informasjon om kapasitet, produksjon og andre relevante data. Dette kan finnes her [Geonorge](https://kartkatalog.geonorge.no/metadata/vindkraftverk/ac249604-cd82-490c-83cc-9cd24fe18088))
- For data om vernede naturområder har vi valgt å bruke data fra Miljødirektoratet, som gir oss informasjon om vernede naturområder i Norge. Dette gir oss et godt grunnlag for å vise informasjon om vernede naturområder i Agder, samtidig som vi kan vise informasjon om naturverdier og landskapsverdier. Dette kan finnes her [Geonorge](https://kartkatalog.geonorge.no/metadata/naturvernomraader/5857ec0a-8d2c-4cd8-baa2-0dc54ae213b4))
- For å vise navn for områder, bruker vi et kartgrunnlag fra Google, som kan hentes fra (https://mt1.google.com/vt/lyrs=h&x={x}&y={y}&z={z})



## Hvordan løsningen bli implementert

## Screenshots av løsningen
