Query below lists all tables that are not referenced by any object .

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
<span>select</span> schema_name(schema_id) <span>as</span> schema_name,
       <span>name</span> <span>as</span> table_name
<span>from</span> sys.tables tab
<span>left</span> <span>join</span> sys.sql_expression_dependencies dep
          <span>on</span> tab.object_id = dep.referenced_id
<span>where</span> dep.referenced_id <span>is</span> <span>null</span>
<span>order</span> <span>by</span> schema_name,
         table_name;
```

## Columns

-   **schema\_name** - schema name of the table
-   **table\_name** - table name

## Rows

-   **One row** represents one table that is not used by any other object
-   **Scope of rows:** all objects that are not used by any other object
-   **Ordered by** schema name and table name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/not_referenced_tables_by_object.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)