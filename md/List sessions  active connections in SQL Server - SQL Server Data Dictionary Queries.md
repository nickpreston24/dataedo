There are two procedures useful in debugging session problems.

## Before you begin

Please note that you need **VIEW SERVER STATE** permission to view all the sessions. Otherwise you'll only see your own.

## Official command

### Query

```
exec sp_who
```

### Columns

-   **spid** - unique session ID
-   **status** - process status
-   **loginname** - login name associated with the session. You can use ti to identify application user
-   **hostname** - host name associated with the session. You can use ti to identify application user
-   **blk** - session ID of the blocking process (spid is blocked by blk)
-   **dbname** - database used by process

### Sample results

You can see session list on our test server. Note that user 68 is blocked by 70.

![sample results](https://dataedo.com/asset/img/kb/query/sql-server/list-database-sessions-sp-who.png)

## More verbose command

SQL Server has also second version of this procedure - **sp\_who2**. This procedure shows some more information that you can use to identify process.

### Query

```
exec sp_who2
```

### Columns

-   all columns from sp\_who, plus:
-   **ProgramName** - application associated with the session Many applications set this useful value
-   **LastBatch** - last activity associated with the session

### Sample results

You can see session list on our test server. Note that user 68 is blocked by 70. Both users are using Microsoft SQL Server Management Studio.

![sample results](https://dataedo.com/asset/img/kb/query/sql-server/list-database-sessions-sp-who2.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)