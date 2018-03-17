docker build -t sql-tfs https://raw.githubusercontent.com/Microsoft/mssql-docker/master/linux/preview/examples/mssql-agent-fts-ha-tools/Dockerfile

docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=yourStrong0Password' -p 1433:1433 -d sql-fts