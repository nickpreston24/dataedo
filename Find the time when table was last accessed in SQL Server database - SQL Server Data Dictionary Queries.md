Query below returns list of tables and their last using date.

## Note

In SQL Server you can find out when table was last accessed by quering **dm\_db\_index\_usage\_stats** view, but note that this view is cleaned each time SQL Server is restarted.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

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
<span>group</span> <span>by</span> schema_name, 
         table_name
<span>order</span> <span>by</span> last_access <span>desc</span>;
```

## Columns

-   **schema\_name** - name of the schema
-   **table\_name** - name of the table
-   **LastAccess** - last access time to table

## Rows

-   **One row** represents one table in database
-   **Scope of rows:** all tables that was accessed in current SQL Server uptime
-   **Ordered by** last access time

## Sample results

![sample results](https://dataedo.com/asset/img/kb/query/sql-server/last-access-time.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)