# Kiwi Lock
<img src="image4.png" alt="Ecovi" width="400" style="display:flex;justify-content:center;align-items:center;">
</div>

**Kiwi Lock** è il software prorietario di gestione del Kiwi PadLock. È stato creato per un progetto di gruppo.

## User Experience
All’apertura iniziale, l’utente deve scegliere il nome del lucchetto e inserire la password.
Successivamente alla creazione del lucchetto, a ogni apertura dell’app verrà richiesta l’autenticazione.
L’interfaccia presenta due pulsanti principali e tre secondari. I due principali possono essere usati dall’utente per bloccare e sbloccare il lucchetto.
Gli altri servono rispettivamente per generare il log delle azioni, per modificare la password e per eliminare il lucchetto registrato.
É importante notare che il lucchetto deve essere connesso al computer per eseguire operazioni sul programma. Molto importante è anche disattivare il Bluetooth prima di avviare il programma!

## Codici per la comunicazione
“s” -> (Ricevuto dal programma) Il Kiwi PadLock richiede l’inserimento della password
“C” -> (Ricevuto dal programma) Il Kiwi PadLock conferma al programma che tutto sia funzionante
“H” -> Richiesta della conferma che il dispositivo collegato sia il Kiwi PadLock
“0” -> Chiusura del Kiwi PadLock
“1” -> Apertura del Kiwi PadLock
“2” -> Invia il Kiwi PadLock in stato di attesa e disattiva il sensore
“4” -> Esce dallo stato di attesa e attivazione del sensore
“5” -> (Scartato) Entra nello stato per la ricezione di una stringa
