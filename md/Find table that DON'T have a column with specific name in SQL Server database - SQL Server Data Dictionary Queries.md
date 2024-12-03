Databases often have standard columns. Examples of such standard columns can be _id_, _modified\_date_, _created\_by_ or _row\_version_.

Query below finds all tables that do not have a 'ModifiedDate' column.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/3187eed29ce5b9127613e8a72fc11156.png)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-confused)

## Query

```
select schema_name(t.schema_id) as schema_name,
       t.name as table_name
from sys.tables t
where t.object_id not in 
    (select c.object_id 
      from sys.columns c
     where c.name = 'ModifiedDate')
order by schema_name,
         table_name;
```

## Columns

-   **schema\_name** - name of schema of found table
-   **table\_name** - name of found table

## Rows

-   **One row** represents a table
-   **Scope of rows:** all found tables
-   **Ordered by** schema name

## Sample results

These tables do not have a 'ModifiedDate' column.

![](https://dataedo.com/asset/img/kb/query/sql-server/find_tables_without_column.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)