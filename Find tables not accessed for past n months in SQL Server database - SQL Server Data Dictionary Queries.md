Query below returns list of tables that was not accessed in the last _3_ month.

## Note

In SQL Server you can find out when table was last accessed by quering **dm\_db\_index\_usage\_stats** view, but note that this view is cleaned each time SQL Server is restarted.

### Do you need a fortune teller to tell you about the data you have?

If you visited a fortune teller at least once in the past 12 months we highly recommend reading this article. Learn how to see into your data yourself.

[Learn how](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/d36a7df6380a23152f19389890296cdc.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-fairy)

## Query

```
<span>select</span> [schema_name],
       table_name,
       <span>max</span>(last_access) <span>as</span> last_access
<span>from</span>(
    <span>select</span> schema_name(schema_id) <span>as</span> schema_name,
           <span>name</span> <span>as</span> table_name,
           (<span>select</span> <span>max</span>(last_access) 
            <span>from</span> (<span>values</span>(last_user_seek),
                        (last_user_scan),
                        (last_user_lookup), 
                        (last_user_update)) <span>as</span> tmp(last_access))
                <span>as</span> last_access
<span>from</span> sys.dm_db_index_usage_stats sta
<span>join</span> sys.objects obj
     <span>on</span> obj.object_id = sta.object_id
     <span>and</span> obj.type = <span>'U'</span>
     <span>and</span> sta.database_id = DB_ID()
) <span>usage</span>
<span>where</span> last_access &lt; <span>dateadd</span>(<span>month</span>, <span>-3</span>, <span>current_timestamp</span>)
<span>group</span> <span>by</span> schema_name,
         table_name
<span>order</span> <span>by</span> last_access <span>desc</span>;
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