# Oppgave 2: Geografisk IT-utvikling - Gruppe 8
## Gruppemedlemmer
- Kevin Grefstad Gundersen
- Magi Natasha Nakangi
- Martin Brådland  
- Natasha Engemann 
- Truls Iver M. Iversen 

## Introduksjon
I denne oppgaven skal vi lage en webapplikasjon som viser informasjon om eksisterende vindkraftverk 
i Agder, og hvor det kan være gjennomførbart å lage nye vindkraftverk.

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

- Som kartgrunnlag har vi valgt å bruke kartdata fra Kartverket, som er en pålitelig kilde for geografiske data i Norge. Vi har valgt å bruke data fra N50-serien, som er en detaljert kartserie som dekker hele landet. Dette gir oss et godt grunnlag for å vise vindkraftverk og annen informasjon på et detaljert kartgrunnlag. Dette kan finnes her [Geonorge](https://kartkatalog.geonorge.no/metadata/ea192681-d039-42ec-b1bc-f3ce04c189ac)
- For visning av vindstyrke og terengkompleksitet har vi valgt å bruke data fra NVE (Norges vassdrags- og energidirektorat). Dette gir oss et godt grunnlag for å vise potensialet for vindkraftproduksjon i Agder, samtidig som vi kan vise informasjon om terrenget og vindforholdene i området. Dette kan finnes her [Geonorge](https://kartkatalog.geonorge.no/metadata/vindressurser/21079f3d-81b8-405b-bfb1-c213d732fcfb)
- For data om vindkraftverk har vi valgt å bruke data fra NVE, som gir oss informasjon om vindkraftverk i Norge. Dette gir oss et godt grunnlag for å vise informasjon om vindkraftverk i Agder, samtidig som vi kan vise informasjon om kapasitet, produksjon og andre relevante data. Dette kan finnes her [Geonorge](https://kartkatalog.geonorge.no/metadata/vindkraftverk/ac249604-cd82-490c-83cc-9cd24fe18088)
- For data om vernede naturområder har vi valgt å bruke data fra Miljødirektoratet, som gir oss informasjon om vernede naturområder i Norge. Dette gir oss et godt grunnlag for å vise informasjon om vernede naturområder i Agder, samtidig som vi kan vise informasjon om naturverdier og landskapsverdier. Dette kan finnes her [Geonorge](https://kartkatalog.geonorge.no/metadata/naturvernomraader/5857ec0a-8d2c-4cd8-baa2-0dc54ae213b4)
- For å vise navn for områder, bruker vi et kartgrunnlag fra Google, som kan hentes fra (https://mt1.google.com/vt/lyrs=h&x={x}&y={y}&z={z})



## Hvordan løsningen bli implementert

### Database
Vi laget en database på Supabase. Da laget vi en tabell, som kalles vi "Vindkraftverk", dette er datasett fra NVE og inneholder data om vindkraftverk i Agder. Deretter transformerte vi geometry data til geojson ved bruk av den innbygdt SQL editor. Først lagde vi en ny kolone til å holde denne nye datatypen. Data måtte gjøres om til riktig SRID, fordi geojson trenger 4623 og eksisterende data var i 32633. Da dataen har riktig SRID kunne den lagres som json datatype.

### Applikasjon
Først opprettet vi en .Net 8 MVC app i Visual Studio, da dette er noe vi har litt kjennskap til fra tidligere, det blir grunnlaget til applikasjonen. 
- Flere kartgrunnlag lastes inn som en tilkobling til en WMS tjeneste.
- Vindkraftverkene, og detaljert informasjon om dem lastes inn fra Supabase databasen.
- Søkefunksjonen er en leaflet plugin hentet [herfra](https://github.com/perliedman/leaflet-control-geocoder).

Brukeren kan deretter velge hvilke datasett de vil se ved hjelp av en "dropdown" med valgbokser.

Om man trykker på enkelte vindkraftverk på kartet vil man få mer detaljert informasjon.

Ved å velge både laget med vindresursser, og lag for terengkompleksitet kan man se hvor det kan være best egne for utbygging av nye vindkraftverk.

## Screenshots av løsningen

![Bilde1](https://github.com/user-attachments/assets/1a986557-f67f-4c48-b5e2-6da91673e033)

Her ser vi et grunnkart med forskjellige valgfunksjoner av hvilke lag man ønsker å se fra de forskjellige datasettene.

![Bilde2](https://github.com/user-attachments/assets/17e83f06-9b08-4687-832b-da485614e564)

Enkel søkefunksjon for stedsnavn

![Bilde3](https://github.com/user-attachments/assets/9227936a-bddf-46ed-b09d-77cc1b8a244c)

Eksisterende vindkraftverk

![Bilde4](https://github.com/user-attachments/assets/3b9b1906-3986-47e8-9cd6-41205d8112b8)

Ved valg av et vindkraftverk vises informasjon hentet fra database

![Bilde5](https://github.com/user-attachments/assets/29ae1a1a-b53b-4c55-898e-426ae84b0ea6)

Visning av beskyttede naturområder i agder. Det vises hvor det ikke er mulig å bygge nye vindkraftverk.

![Bilde6](https://github.com/user-attachments/assets/7661ed02-62ac-4195-81fa-3e1c2acee88f)

Visning av terreng kompleksitet, eller hvor det er lett eller vanskelig å opprette et vindkraftverk. Det er en farge-skale, fra grønn(lett) til rød(vanskeligst)

![Bilde7](https://github.com/user-attachments/assets/4c1aa022-bf7f-4e12-98f1-f930cad96327)

Vind styrke visning, viser gjennomsnittlig vindfart, dette gir muligheten til å vurdere hvor nye vindkraftverk kan bygges. De trenger nok vind, men for sterk vind kan skade turbiner.

![Bilde8](https://github.com/user-attachments/assets/622c0b7c-3194-4499-856c-1755ca878cb7)

Dette er en visning av sammensatt lag, eksisterende vindkraftverk, beskyttede naturområder, og vind resursser. Dette gir muligheten for vurdering av beste plass å bygge nye vindkraftverk.

![Bilde9](https://github.com/user-attachments/assets/c9e50d2c-a1d5-4521-ba15-56c50aeb4825)

Visning av sammensatt lag, eksisterende vindkraftverk, beskyttet områder, og terreng kompleksitet, dette gir også muligheten til å vurdere hvor man kan bygge nye vindkraftverk.


