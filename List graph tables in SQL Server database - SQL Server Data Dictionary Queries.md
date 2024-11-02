This query works on **SQL Server 2017** or newer.

SQL Server 2017 introduced graph tables.

Query below returns those tables.

### Do table names in your database always make sense? Honestly.

Yeah, ours neither. See what we did about that.

[Learn now](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

[![](https://dataedo.com/asset/img/markdown/docs/test-article/edca6a29318bb7640068f5c69a5af4ba.png#center)](https://dataedo.com/blog/confused-when-trying-to-work-with-databases?cta=kb-query-table-names)

## Query

```
select case when is_node = 1 then 'Node'
            when is_edge = 1 then 'Edge'
        end table_type,
        schema_name(schema_id) as schema_name,
        name as table_name
from sys.tables
where is_node = 1 or is_edge = 1
order by is_edge, schema_name, table_name
```

## Columns

-   **table\_type** - graph table type - **Node** and **Edge**
-   **schema\_name** - table schema name
-   **table\_name** - table name

## Rows

-   **One row** represents one graph table - node or edge
-   **Scope of rows:** only graph (node or edge) tables are included
-   **Ordered by** nodes first, then edeges. Within type by schema and table name

## Sample results

![](https://dataedo.com/asset/img/kb/query/sql-server/graph_tables.png)

### Create beautiful and useful documentation of your SQL Server

Generate convenient documentation of your databases in minutes and share it with your team. Capture and preserve tribal knowledge in shared repository.

[![](https://dataedo.com/asset/img/markdown/docs/test-article/30c11fa4b210f11740f56e85ca8bf9c6.gif)](https://demo.dataedo.com/)

[See how it works](https://demo.dataedo.com/)