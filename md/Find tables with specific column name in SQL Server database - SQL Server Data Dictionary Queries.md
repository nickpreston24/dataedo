Query below finds all tables that have 'ProductID' column.

See also [tables that don't have a column with specific name](https://dataedo.com/kb/query/sql-server/find-table-that-dont-have-a-column-with-specific-name).

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
select schema_name(t.schema_id) as schema_name,
       t.name as table_name
from sys.tables t
where t.object_id in 
    (select c.object_id 
      from sys.columns c
     where c.name = 'ProductID')
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

List of tables that have 'ProductID' column.

![](https://dataedo.com/asset/img/kb/query/sql-server/find_tables_with_column.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)