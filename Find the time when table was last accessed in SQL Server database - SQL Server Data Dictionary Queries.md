Query below returns list of tables and their last using date.

## Note

In SQL Server you can find out when table was last accessed by quering **dm\_db\_index\_usage\_stats** view, but note that this view is cleaned each time SQL Server is restarted.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

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
group by schema_name, 
         table_name
order by last_access desc;
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