Query below returns list of tables that was not accessed in the last _3_ month.

## Note

In SQL Server you can find out when table was last accessed by quering **dm\_db\_index\_usage\_stats** view, but note that this view is cleaned each time SQL Server is restarted.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
select [schema_name],
       table_name,
       max(last_access) as last_access
from(
    select schema_name(schema_id) as schema_name,
           name as table_name,
           (select max(last_access) 
            from (values(last_user_seek),
                        (last_user_scan),
                        (last_user_lookup), 
                        (last_user_update)) as tmp(last_access))
                as last_access
from sys.dm_db_index_usage_stats sta
join sys.objects obj
     on obj.object_id = sta.object_id
     and obj.type = 'U'
     and sta.database_id = DB_ID()
) usage
where last_access &lt; dateadd(month, -3, current_timestamp)
group by schema_name,
         table_name
order by last_access desc;
```

## Columns

-   **schema\_name** - name of the database
-   **table\_name** - name of the table
-   **last\_access** - datetime of last access

## Rows

-   **One row** represents one table in database
-   **Scope of rows:** all tables not accessed for past 3 months in database
-   **Ordered by** last access time descending

## Sample results

![sample results](https://dataedo.com/asset/img/kb/query/sql-server/tables_not_accessed.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)