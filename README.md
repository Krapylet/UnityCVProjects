# Tickets Please
"I tickets please spiller du en togkonduktør der skal gå rundt på et tog og checke billeter. Togkonduktøren har kun et begrænset stykke tid inden næste station, og reglerne for hvad der er en valid billet skifter fra dag til dag og bliver gradvist mere og mere komplekst. Spilleren må derfor forsøge at lære dagens regler mens de ræser rundt mellem passagererne."

https://user-images.githubusercontent.com/37876827/129507773-c9747925-46c1-4f6f-9494-523be7c9f29e.mp4

Jeg udviklede Tickets Please sammen med 3 andre arrangører fra GDC remotely. Teamet bestod af 1 2d kunster, 1 lydmand og 2 programmører og blev udviklet i vores fitid over en måned i starten af 2019 . Projektet var en del af 2d'erens eksamensprojekt, og fokusset for projektet blev derfor at lave et minimum viable product rent gameplay messigt, og så ellers have et stort fokus på spillets grafiske udtryk. I den sammenhæng blev der implementeret et custom tool i unity der lod 2d'eren at automatisk generere passagererne ud fra grupper af kompatible krops- og beklædningstyper. Det blev vidst ikke udnyttet i det endelige build, men ligger stadig inde i unity projektet.

Der ligger både et build og alle source filerne i dette repo.
## Spil instruktioner
Gå - wasd

interager med passager - e

Når du interagerer med en passager får du deres billet og pas. Du skal nu finde ud af om deres billet er gyldig ved at se om alder passer, om de har fået det korrekte segel og om navnet passer.
For at give en bøde trækker du blot bøden hen til biletten. Træk tilsvarende hulmaskinen til billeten hvis den er gyldig. (I Det her build er det vidst kun alder der tæller for om en billet er gyldig eller ej)
