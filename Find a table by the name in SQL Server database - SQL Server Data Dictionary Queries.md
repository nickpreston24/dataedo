Query below finds tables with specific name in all schemas in a database. In this case it searches for 'customer' table.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
select schema_name(t.schema_id) as schema_name,
       t.name as table_name
from sys.tables t
where t.name = 'customer'
order by schema_name,
         table_name;
```

## Columns

-   **schema\_name** - name of schema table was found in
-   **table\_name** - name of table (redundant as it should be exactly the same as provided)

## Rows

-   **One row** represents a table
-   **Scope of rows:** all found tables
-   **Ordered by** schema name

## Notes

1.  There migh be more tables than one because different schemas in a database can have tables with the same names

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/find_tables_by_name.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)