Query below list user created schemas in all databases.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
declare @query nvarchar(max)

set @query = 
(select 'select ''' + name + ''' as database_name,
              s.Name COLLATE DATABASE_DEFAULT as schema_name,
              u.name COLLATE DATABASE_DEFAULT as schema_owner 
        FROM ['+ name + '].sys.schemas s
        JOIN ['+ name + '].sys.sysusers u on u.uid = s.principal_id
        where u.issqluser = 1
              and u.name not in (''sys'', ''guest'', ''INFORMATION_SCHEMA'')
        union all '
    from sys.databases 
    where database_id &gt; 4
    for xml path(''), type).value('.', 'nvarchar(max)')

set @query = left(@query,len(@query)-10) 
                        + ' order by database_name, schema_name'

execute (@query)
```

## Columns

-   **database\_name** - name of the database within schema resides
-   **schema\_name** - name of the schema
-   **schema\_owner** - name of the schema owner

## Rows

-   **One row** represents one user schema
-   **Scope of rows:** all user schemas from all databases
-   **Ordered by** database name and schema name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/user_schemas_all_databases.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)