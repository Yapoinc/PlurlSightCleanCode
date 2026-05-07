# migrations add
dotnet ef migrations add "InitialMigration" \
--project src/Infrastructure/GloboTicket.TicketManagement.Persistence/ \
--startup-project src/API/GloboTicket.TicketManagement.Api/

# database update

dotnet ef database update \
--project src/Infrastructure/GloboTicket.TicketManagement.Persistence/ \
--startup-project src/API/GloboTicket.TicketManagement.Api/
