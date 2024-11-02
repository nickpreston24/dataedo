SQL Server provides command to kill specific session on a server.

## Find session ID

First we will identify the session we want to end. We do it by listing all sessions on the server with this query:

```
exec sp_who
```

### Result

Result shows active sessions on server and three of them are from my computer - LS-MICHALWROBEL:

![sample results](https://dataedo.com/asset/img/kb/query/sql-server/find-last-query-executed-by-session-sp-who.png)

## Kill session

Now we will use session ID (spid) to kill the session (67 in our example):

```
kill 67
```

### Result

SQL Sevrer ends session and rolls back all transactions that are associated with it. This operation can take a while.

```
Command(s) completed successfully.
```

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)