This query returns columns in a database sorted by their name.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
<span>select</span> col.name <span>as</span> column_name,
    <span>len</span>(col.name) <span>as</span> column_name_length,
    schema_name(tab.schema_id) <span>as</span> schema_name,
    tab.name <span>as</span> table_name
 <span>from</span> sys.tables <span>as</span> tab
        <span>inner</span> <span>join</span> sys.columns <span>as</span> <span>col</span> 
        <span>on</span> tab.object_id = col.object_id
<span>order</span> <span>by</span> <span>len</span>(col.name) <span>desc</span>,
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