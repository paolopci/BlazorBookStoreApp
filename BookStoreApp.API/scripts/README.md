## Reset e ripopolamento dati di test

1. **Azzerare il DB locale**  
   - Esegui lo script `delete_records_tabelle.sql` (ora droppa e ricrea `BookStoreDb`).  
   - In alternativa usa `dotnet ef database drop --project BookStoreApp.API/BookStoreApp.API.csproj`.

2. **Applicare le migration**  
   - Lancia `dotnet ef database update --project BookStoreApp.API/BookStoreApp.API.csproj --startup-project BookStoreApp.API/BookStoreApp.API.csproj` per ricreare schema e seed (autori, libri, ruoli, utenti).

3. **Verifica rapida**  
   - Esegui query di controllo, ad esempio:  
     ```sql
     SELECT COUNT(*) FROM Authors;
     SELECT UserName FROM AspNetUsers;
     ```
   - In alternativa genera lo script con `dotnet ef dbcontext script` e verifica che i valori combacino.

4. **Documenta l’esito**  
   - Annota nel README/PR che il DB è stato rigenerato con le migration e che i dati seed risultano corretti (autori/libri + utenti Administrator/User).

> Usa gli script SQL solo come supporto manuale: la fonte principale resta `dotnet ef database update`.
