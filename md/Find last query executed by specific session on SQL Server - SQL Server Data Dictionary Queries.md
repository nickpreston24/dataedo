SQL Server enables you to check last query executed by each session. Queries in this article will show you how to do it.

## Find a session

First you need to identify session. Find out more [here](https://dataedo.com/kb/query/sql-server/list-database-sessions):

```
exec sp_who
```

### Result

Result shows three sessions from host LS-MICHALWROBEL:

![sample results](https://dataedo.com/asset/img/kb/query/sql-server/find-last-query-executed-by-session-sp-who.png)

## Find last query

Now we will use session ID (spid) to find last query issued in this session:

```
dbcc inputbuffer(&lt;session ID&gt;)
```

## Columns

-   **EventType** - event type. **RPC Event** means procedure call. **Language Event** means sql query.
-   **Parameters** - number of query parameters.
-   **EventInfo** - last statement in session. Returns only first 4000 characters.

## Sample results

We can see that last query issued insession 67 was "select \* from mytable"

![sample results](https://dataedo.com/asset/img/kb/query/sql-server/find-last-query-executed-by-session.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)