docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=mango-api" -p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2019-latest
