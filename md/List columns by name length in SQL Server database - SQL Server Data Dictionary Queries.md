This query returns columns in a database sorted by their name.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
select col.name as column_name,
    len(col.name) as column_name_length,
    schema_name(tab.schema_id) as schema_name,
    tab.name as table_name
 from sys.tables as tab
        inner join sys.columns as col 
        on tab.object_id = col.object_id
order by len(col.name) desc,
    col.name
```

## Columns

-   **column\_name** - column name
-   **column\_name\_length** - column name length
-   **schema\_name** - column table schema name
-   **table\_name** - column table name

## Rows

-   **One row** represents one column of each table in a database
-   **Scope of rows:** each column that exists in a database
-   **Ordered by** length descrending - from longhest to shortest

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/columns_by_length.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)